using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Script;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;

namespace OpenBots.Core.Utilities.CommonUtilities
{
    public static class VariableMethods
    {
        /// <summary>
        /// Replaces variable placeholders ({variable}) with variable text.
        /// </summary>
        /// <param name="sender">The script engine instance (AutomationEngineInstance) which contains session variables.</param>
        public static string ConvertUserVariableToString(this string userInputString, IAutomationEngineInstance engine, bool requiresMarkers = true)
        {
            if (string.IsNullOrEmpty(userInputString))
                return string.Empty;

            if (engine == null)
                return userInputString;

            if (userInputString.Length < 2)
                return userInputString;

            var variableList = engine.AutomationEngineContext.Variables;
            var systemVariables = CommonMethods.GenerateSystemVariables();
            var argumentsAsVariablesList = engine.AutomationEngineContext.Arguments.Select(arg => new ScriptVariable { 
                                                                                        VariableName = arg.ArgumentName, 
                                                                                        VariableType = arg.ArgumentType,
                                                                                        VariableValue = arg.ArgumentValue })
                                                                                    .ToList();

            var variableSearchList = new List<ScriptVariable>();
            variableSearchList.AddRange(variableList);
            variableSearchList.AddRange(systemVariables);
            variableSearchList.AddRange(argumentsAsVariablesList);

            //variable markers
            string startVariableMarker = "{";
            string endVariableMarker = "}";

            if ((!userInputString.Contains(startVariableMarker) || !userInputString.Contains(endVariableMarker)) && requiresMarkers)
            {
                return userInputString.CalculateVariables(engine);
            }
                
            //split by custom markers
            string[] potentialVariables = userInputString.Split(new string[] { startVariableMarker, endVariableMarker }, StringSplitOptions.None);

            foreach (var potentialVariable in potentialVariables)
            {
                if (potentialVariable.Length == 0) 
                    continue;

                string varcheckname = potentialVariable;
                bool isSystemVar = systemVariables.Any(vars => vars.VariableName == varcheckname);

                if (potentialVariable.Split('.').Length >= 2 && !isSystemVar)
                {
                    varcheckname = potentialVariable.Split('.')[0];
                }

                var varCheck = variableSearchList.Where(v => v.VariableName == varcheckname)
                                                 .FirstOrDefault();

                if (potentialVariable == "OpenBots.EngineContext") 
                    varCheck.VariableValue = engine.GetEngineContext();

                if (varCheck != null)
                {
                    var searchVariable = startVariableMarker + potentialVariable + endVariableMarker;

                    if (userInputString.Contains(searchVariable))
                    {
                        if (varCheck.VariableType == typeof(string) || varCheck.VariableType.IsPrimitive)
                        {
                            userInputString = userInputString.Replace(searchVariable, varCheck.VariableValue?.ToString());
                        }
                        else if (varCheck.VariableValue is List<string> && potentialVariable.Split('.').Length == 2)
                        {
                            //get data from a string list using the index
                            string listIndexString = potentialVariable.Split('.')[1].ConvertUserVariableToString(engine, false);
                            var list = varCheck.VariableValue as List<string>;

                            string listItem;
                            if (int.TryParse(listIndexString, out int listIndex))
                                listItem = list[listIndex].ToString();
                            else
                                return userInputString;

                            userInputString = userInputString.Replace(searchVariable, listItem);
                        }
                        else if (varCheck.VariableValue is DataRow && potentialVariable.Split('.').Length == 2)
                        {
                            //get data from a datarow using the column name/index
                            string columnName = potentialVariable.Split('.')[1].ConvertUserVariableToString(engine, false);
                            var row = varCheck.VariableValue as DataRow;

                            string cellItem;
                            if (int.TryParse(columnName, out var columnIndex))
                                cellItem = row[columnIndex].ToString();
                            else
                                cellItem = row[columnName].ToString();

                            userInputString = userInputString.Replace(searchVariable, cellItem);
                        }
                        else if (varCheck.VariableValue is DataTable && potentialVariable.Split('.').Length == 3)
                        {
                            //get data from datatable using the row index and column name/index	
                            string rowIndexString = potentialVariable.Split('.')[1].ConvertUserVariableToString(engine, false);
                            string columnName = potentialVariable.Split('.')[2].ConvertUserVariableToString(engine, false);
                            var dt = varCheck.VariableValue as DataTable;
                            string cellItem;

                            if (int.TryParse(rowIndexString, out int rowIndex))
                            {                               
                                if (int.TryParse(columnName, out int columnIndex))
                                    cellItem = dt.Rows[rowIndex][columnIndex].ToString();
                                else
                                    cellItem = dt.Rows[rowIndex][columnName].ToString();
                            }
                            else
                                return userInputString;

                            userInputString = userInputString.Replace(searchVariable, cellItem);
                        }
                        else if (varCheck.VariableValue?.GetType().Name == typeof(KeyValuePair<,>).Name && potentialVariable.Split('.').Length == 2)
                        {
                            //get data from a keyvaluepair using Key/Value
                            string resultType = potentialVariable.Split('.')[1].ConvertUserVariableToString(engine, false);

                            dynamic pair;
                            string resultItem;
                            pair = varCheck.VariableValue;
                            if (resultType.ToLower() == "key")
                                resultItem = pair.Key;
                            else if(resultType.ToLower() == "value")
                                resultItem = StringMethods.ConvertObjectToString(pair.Value, pair.Value.GetType());
                            else
                                throw new DataException("Only use of Key and Value is allowed using dot operater with KeyValuePair");
                            userInputString = userInputString.Replace(searchVariable, resultItem);
                        }
                    }
                    else if (!requiresMarkers)
                    {
                        if (varCheck.VariableValue is string)
                            userInputString = userInputString.Replace(potentialVariable, (string)varCheck.VariableValue);
                    }
                } 
                else if (varCheck == null && userInputString.Contains(startVariableMarker + varcheckname + endVariableMarker))
                    throw new ArgumentNullException($"No variable/argument with the name '{varcheckname}' was found.");
            }

            return userInputString.CalculateVariables(engine);
        }

        public static object ConvertUserVariableToObject(this string varArgName, IAutomationEngineInstance engine, string parameterName, ScriptCommand parent)
        {
            var variableProperties = parent.GetType().GetProperties().Where(f => f.Name == parameterName).FirstOrDefault();
            var compatibleTypesAttribute = variableProperties.GetCustomAttributes(typeof(CompatibleTypes), true);
           
            Type[] compatibleTypes = null;

            if (compatibleTypesAttribute.Length > 0)
                compatibleTypes = ((CompatibleTypes[])compatibleTypesAttribute)[0].CompTypes;

            ScriptVariable requiredVariable;
            ScriptArgument requiredArgument;

            if (varArgName.StartsWith("{") && varArgName.EndsWith("}"))
            {
                //reformat and attempt
                var reformattedVarArg = varArgName.Replace("{", "").Replace("}", "");

                requiredVariable = engine.AutomationEngineContext.Variables
                                                .Where(var => var.VariableName == reformattedVarArg)
                                                .FirstOrDefault();

                if (requiredVariable != null && compatibleTypes!= null && !compatibleTypes.Any(x => x.IsAssignableFrom(requiredVariable.VariableType)))
                    throw new ArgumentException($"The type of variable '{requiredVariable.VariableName}' is not compatible.");

                requiredArgument = engine.AutomationEngineContext.Arguments
                                                .Where(arg => arg.ArgumentName == reformattedVarArg)
                                                .FirstOrDefault();

                if (requiredArgument != null && compatibleTypes != null && !compatibleTypes.Any(x => x.IsAssignableFrom(requiredArgument.ArgumentType)))
                    throw new ArgumentException($"The type of argument '{requiredArgument.ArgumentName}' is not compatible.");
            }
            else
                throw new Exception("Variable/Argument markers '{}' missing. Variable/Argument '" + varArgName + "' could not be found.");

            if (requiredVariable != null)
                return requiredVariable.VariableValue;
            else if (requiredArgument != null)
                return requiredArgument.ArgumentValue;
            else
                return null;
        }

        public static object ConvertUserVariableToObject(this string varArgName, IAutomationEngineInstance engine, Type compatibleType)
        {
            ScriptVariable requiredVariable;
            ScriptArgument requiredArgument;

            if (varArgName.StartsWith("{") && varArgName.EndsWith("}"))
            {
                //reformat and attempt
                var reformattedVarArg = varArgName.Replace("{", "").Replace("}", "");

                requiredVariable = engine.AutomationEngineContext.Variables
                                                .Where(var => var.VariableName == reformattedVarArg)
                                                .FirstOrDefault();

                if (requiredVariable != null && !compatibleType.IsAssignableFrom(requiredVariable.VariableType))
                    throw new ArgumentException($"The type of variable '{requiredVariable.VariableName}' is not compatible.");

                requiredArgument = engine.AutomationEngineContext.Arguments
                                                .Where(arg => arg.ArgumentName == reformattedVarArg)
                                                .FirstOrDefault();

                if (requiredArgument != null && !compatibleType.IsAssignableFrom(requiredArgument.ArgumentType))
                    throw new ArgumentException($"The type of argument '{requiredArgument.ArgumentName}' is not compatible.");
            }
            else
                throw new Exception("Variable/Argument markers '{}' missing. Variable/Argument '" + varArgName + "' could not be found.");

            if (requiredVariable != null)
                return requiredVariable.VariableValue;
            else if (requiredArgument != null)
                return requiredArgument.ArgumentValue;
            else
                return null;
        }

        public static Type GetVarArgType(this string varArgName, IAutomationEngineInstance engine)
        {
            ScriptVariable requiredVariable;
            ScriptArgument requiredArgument;

            if (varArgName.StartsWith("{") && varArgName.EndsWith("}"))
            {
                //reformat and attempt
                var reformattedVarArg = varArgName.Replace("{", "").Replace("}", "");

                requiredVariable = engine.AutomationEngineContext.Variables
                                                .Where(var => var.VariableName == reformattedVarArg)
                                                .FirstOrDefault();              

                requiredArgument = engine.AutomationEngineContext.Arguments
                                                .Where(arg => arg.ArgumentName == reformattedVarArg)
                                                .FirstOrDefault();
            }
            else
                throw new Exception("Variable/Argument markers '{}' missing. Variable/Argument '" + varArgName + "' could not be found.");

            if (requiredVariable != null)
                return requiredVariable.VariableType;
            else if (requiredArgument != null)
                return requiredArgument.ArgumentType;
            else
                return null;
        }

        private static string CalculateVariables(this string str, IAutomationEngineInstance engine)
        {
            if (!engine.AutoCalculateVariables)
                return str;
            else
            {
                //track math chars
                var mathChars = new List<char>();
                mathChars.Add('*');
                mathChars.Add('+');
                mathChars.Add('-');
                mathChars.Add('=');
                mathChars.Add('/');

                //if the string matches the char then return
                //as the user does not want to do math
                if (mathChars.Any(f => f.ToString() == str) || (mathChars.Any(f => str.StartsWith(f.ToString()))))
                    return str;

                //bypass math for types that are dates
                DateTime dateTest;
                if ((DateTime.TryParse(str, out dateTest) || DateTime.TryParse(str, CultureInfo.CreateSpecificCulture("es-ES"), DateTimeStyles.AssumeUniversal, out dateTest))
                    && (str.Split('/').Length == 3 || str.Split('-').Length == 3))
                    return str;

                //test if math is required
                if (mathChars.Any(f => str.Contains(f)))
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        var v = dt.Compute(str, "");
                        return v.ToString();
                    }
                    catch (Exception)
                    {
                        return str;
                    }
                }
                else
                    return str;
            }
        }

        /// <summary>
        /// Stores value of the object to a user-defined variable.
        /// </summary>
        /// <param name="sender">The script engine instance (AutomationEngineInstance) which contains session variables.</param>
        /// <param name="targetVariable">the name of the user-defined variable to override with new value</param>
        public static void StoreInUserVariable(this object varArgValue, IAutomationEngineInstance engine, string varArgName, string parameterName, ScriptCommand parent)
        {
            var variableProperties = parent.GetType().GetProperties().Where(f => f.Name == parameterName).FirstOrDefault();
            var compatibleTypesAttribute = variableProperties.GetCustomAttributes(typeof(CompatibleTypes), true);

            Type[] compatibleTypes = null;

            if (compatibleTypesAttribute.Length > 0)
                compatibleTypes = ((CompatibleTypes[])compatibleTypesAttribute)[0].CompTypes;

            if (varArgName.StartsWith("{") && varArgName.EndsWith("}"))
                varArgName = varArgName.Replace("{", "").Replace("}", "");           
            else
                throw new Exception("Variable markers '{}' missing. '" + varArgName + "' is an invalid output variable name.");

            var existingVariable = engine.AutomationEngineContext.Variables
                                               .Where(var => var.VariableName == varArgName)
                                               .FirstOrDefault();

            if (existingVariable != null && compatibleTypes != null && !compatibleTypes.Any(x => x.IsAssignableFrom(existingVariable.VariableType)))
                throw new ArgumentException($"The type of variable '{existingVariable.VariableName}' is not compatible.");
            else if (existingVariable != null)
            {
                existingVariable.VariableValue = varArgValue;
                return;
            }

            var existingArgument = engine.AutomationEngineContext.Arguments
                                            .Where(arg => arg.ArgumentName == varArgName)
                                            .FirstOrDefault();

            if (existingArgument != null && compatibleTypes != null && !compatibleTypes.Any(x => x.IsAssignableFrom(existingArgument.ArgumentType)))
                throw new ArgumentException($"The type of argument '{existingArgument.ArgumentName}' is not compatible.");
            else if (existingArgument != null)
            {
                existingArgument.ArgumentValue = varArgValue;
                return;
            }

            throw new ArgumentNullException($"No variable/argument with the name '{varArgName}' was found.");
        }

        public static void StoreInUserVariable(this object varArgValue, IAutomationEngineInstance engine, string varArgName, Type compatibleType)
        {
            if (varArgName.StartsWith("{") && varArgName.EndsWith("}"))
                varArgName = varArgName.Replace("{", "").Replace("}", "");
            else
                throw new Exception("Variable/Argument markers '{}' missing. '" + varArgName + "' is an invalid output variable/argument name.");

            var existingVariable = engine.AutomationEngineContext.Variables
                                               .Where(var => var.VariableName == varArgName)
                                               .FirstOrDefault();

            if (existingVariable != null && !compatibleType.IsAssignableFrom(existingVariable.VariableType))
                throw new ArgumentException($"The type of variable '{existingVariable.VariableName}' is not compatible.");
            else if (existingVariable != null)
            {
                existingVariable.VariableValue = varArgValue;
                return;
            }

            var existingArgument = engine.AutomationEngineContext.Arguments
                                            .Where(arg => arg.ArgumentName == varArgName)
                                            .FirstOrDefault();

            if (existingArgument != null && !compatibleType.IsAssignableFrom(existingArgument.ArgumentType))
                throw new ArgumentException($"The type of argument '{existingArgument.ArgumentName}' is not compatible.");
            else if (existingArgument != null)
            {
                existingArgument.ArgumentValue = varArgValue;
                return;
            }

            throw new ArgumentNullException($"No variable/argument with the name '{varArgName}' was found.");
        }

        /// <summary>
        /// Converts a string to SecureString
        /// </summary>
        /// <param name="value">The string to be converted to SecureString</param>
        public static SecureString ConvertStringToSecureString(this string value)
        {
            SecureString secureString = new NetworkCredential(string.Empty, value).SecurePassword;
            return secureString;
        }

        public static string ConvertSecureStringToString(this SecureString secureString)
        {
            string strValue = new NetworkCredential(string.Empty, secureString).Password;
            return strValue;
        }

        public static void CreateTestVariable(object variableValue, IAutomationEngineInstance engine, string variableName, Type variableType)
        {
            ScriptVariable newVar = new ScriptVariable();
            newVar.VariableName = variableName;
            newVar.VariableValue = variableValue;
            newVar.VariableType = variableType;
            engine.AutomationEngineContext.Variables.Add(newVar);
        }
    }
}
