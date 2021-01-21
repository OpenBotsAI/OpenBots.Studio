﻿using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Properties;
using OpenBots.Core.Utilities.CommonUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using System.Data;
using Microsoft.Office.Interop.Outlook;
using MimeKit;
using OpenQA.Selenium;
using Exception = System.Exception;

namespace OpenBots.Commands.Dictionary
{
	[Serializable]
	[Category("Dictionary Commands")]
	[Description("This command adds an item (key and value pair) to a Dictionary.")]
	public class AddDictionaryItemCommand : ScriptCommand
	{

		[Required]
		[DisplayName("Dictionary")]
		[Description("Select the dictionary variable to add an item to.")]
		[SampleUsage("{vMyDictionary}")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public string v_DictionaryName { get; set; }

		[Required]
		[DisplayName("Keys and Values")]
		[Description("Enter Keys and Values required for the dictionary.")]
		[SampleUsage("[FirstName | John] || [{vKey} | {vValue}]")]
		[Remarks("")]
		[Editor("ShowVariableHelper", typeof(UIAdditionalHelperType))]
		public DataTable v_ColumnNameDataTable { get; set; }

		public AddDictionaryItemCommand()
		{
			CommandName = "AddDictionaryItemCommand";
			SelectionName = "Add Dictionary Item";
			CommandEnabled = true;
			CommandIcon = Resources.command_dictionary;

			//initialize Datatable
			v_ColumnNameDataTable = new DataTable
			{
				TableName = "ColumnNamesDataTable" + DateTime.Now.ToString("MMddyy.hhmmss")
			};

			v_ColumnNameDataTable.Columns.Add("Keys");
			v_ColumnNameDataTable.Columns.Add("Values");
		}

		public override void RunCommand(object sender)
		{
			var engine = (IAutomationEngineInstance)sender;
			var dictionaryVariable = v_DictionaryName.ConvertUserVariableToObject(engine);
			if (dictionaryVariable != null)
			{
				if (dictionaryVariable is Dictionary<string, string>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						((Dictionary<string, string>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine),
							rwColumnName.Field<string>("Values").ConvertUserVariableToString(engine));
					}
				}
				else if (dictionaryVariable is Dictionary<string, DataTable>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						DataTable dataTable;
						var dataTableVariable = rwColumnName.Field<string>("Values").ConvertUserVariableToObject(engine);
						if (dataTableVariable != null && dataTableVariable is DataTable)
							dataTable = (DataTable)dataTableVariable;
						else
							throw new DataException("Invalid dictionary value type, please provide valid dictionary value type.");
						((Dictionary<string, DataTable>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine), dataTable);
					}
				}
				else if (dictionaryVariable is Dictionary<string, MailItem>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						MailItem mailItem;
						var mailItemVariable = rwColumnName.Field<string>("Values").ConvertUserVariableToObject(engine);
						if (mailItemVariable != null && mailItemVariable is MailItem)
							mailItem = (MailItem)mailItemVariable;
						else
							throw new DataException("Invalid dictionary value type, please provide valid dictionary value type.");
						((Dictionary<string, MailItem>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine), mailItem);
					}
				}
				else if (dictionaryVariable is Dictionary<string, MimeMessage>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						MimeMessage mimeMessage;
						var mimeMessageVariable = rwColumnName.Field<string>("Values").ConvertUserVariableToObject(engine);
						if (mimeMessageVariable != null && mimeMessageVariable is MimeMessage)
							mimeMessage = (MimeMessage)mimeMessageVariable;
						else
							throw new DataException("Invalid dictionary value type, please provide valid dictionary value type.");
						((Dictionary<string, MimeMessage>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine), mimeMessage);
					}
				}
				else if (dictionaryVariable is Dictionary<string, IWebElement>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						IWebElement webElement;
						var webElementVariable = rwColumnName.Field<string>("Values").ConvertUserVariableToObject(engine);
						if (webElementVariable != null && webElementVariable is IWebElement)
							webElement = (IWebElement)webElementVariable;
						else
							throw new DataException("Invalid dictionary value type, please provide valid dictionary value type.");
						((Dictionary<string, IWebElement>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine), webElement);
					}
				}
				else if (dictionaryVariable is Dictionary<string, object>)
				{
					foreach (DataRow rwColumnName in v_ColumnNameDataTable.Rows)
					{
						object objectItem;
						var objectItemVariable = rwColumnName.Field<string>("Values").ConvertUserVariableToObject(engine);
						if (objectItemVariable != null && objectItemVariable is object)
							objectItem = (object)objectItemVariable;
						else
							throw new DataException("Invalid dictionary value type, please provide valid dictionary value type.");
						((Dictionary<string, object>)dictionaryVariable).Add(
							rwColumnName.Field<string>("Keys").ConvertUserVariableToString(engine), objectItem);
					}
				}
				else
				{
					throw new NotSupportedException("Dictionary type not supported");
				}

			((object)dictionaryVariable).StoreInUserVariable(engine, v_DictionaryName);
			}
			else
			{
				throw new NullReferenceException("Attempted to add data to a variable, but the variable was not found. Enclose variables within braces, ex. {vVariable}");
			}
		}

		public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
		{
			base.Render(editor, commandControls);

			RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_DictionaryName", this, editor));
			RenderedControls.AddRange(commandControls.CreateDataGridViewGroupFor("v_ColumnNameDataTable", this, editor));

			return RenderedControls;
		}

		public override string GetDisplayValue()
		{
			return base.GetDisplayValue() + $" [Add {v_ColumnNameDataTable.Rows.Count} Item(s) in '{v_DictionaryName}']";
		}      
	}
}