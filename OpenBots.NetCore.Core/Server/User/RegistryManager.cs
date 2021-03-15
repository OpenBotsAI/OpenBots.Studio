﻿using Microsoft.Win32;
using OpenBots.NetCore.Core.Utilities.CommonUtilities;
using System;

namespace OpenBots.NetCore.Core.Server.User
{
    public class RegistryManager
    {
        private RegistryKeys _registryKeys;

        public RegistryManager()
        {
            _registryKeys = new RegistryKeys();
        }

        public string AgentUsername
        {
            get
            {
                return GetKeyValue(_registryKeys.UsernameKey);
            }
            set
            {
                SetKeyValue(_registryKeys.UsernameKey, value);
            }
        }

        public string AgentPassword
        {
            get
            {
                string password = GetKeyValue(_registryKeys.PasswordKey);
                return string.IsNullOrEmpty(password) ? "" : StringMethods.DecryptText(password, SystemInfo.GetMacAddress());
            }
            set
            {
                SetKeyValue(_registryKeys.PasswordKey, StringMethods.EncryptText(value, SystemInfo.GetMacAddress()));
            }
        }

        private string GetKeyValue(string key)
        {
            string keyValue = null;
            var registryKey = Registry.CurrentUser.OpenSubKey(_registryKeys.SubKey, true);

            try
            {
                if (registryKey != null)
                    keyValue = registryKey.GetValue(key)?.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                registryKey?.Close();
            }

            return keyValue;
        }

        private void SetKeyValue(string key, string value)
        {
            var registryKey = Registry.CurrentUser.OpenSubKey(_registryKeys.SubKey, true);

            try
            {
                if (registryKey == null)
                    registryKey = Registry.CurrentUser.CreateSubKey(_registryKeys.SubKey);

                registryKey.SetValue(key, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                registryKey?.Close();
            }
        }
    }
}
