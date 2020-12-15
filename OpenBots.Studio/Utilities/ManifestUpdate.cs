﻿using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Forms;
namespace OpenBots.Utilities
{
    public class ManifestUpdate
    {
        //from manifest
        public string RemoteVersion { get; set; }
        public string PackageURL { get; set; }

        //helpers
        public bool RemoteVersionNewer { get; private set; }
        public Version RemoteVersionProper { get; private set; }
        public Version LocalVersionProper { get; private set; }

        public ManifestUpdate()
        {

        }

        public static ManifestUpdate GetManifest()
        {
            //create web client
            WebClient webClient = new WebClient();
            string manifestData;

            //get manifest
            try
            {
                manifestData = webClient.DownloadString("https://gallery.openbots.io/products/studio/latest.json");           
            }
            catch (Exception)
            {
                //unable to get the manifest
                throw;
            }

            //initialize config
            ManifestUpdate manifestConfig = new ManifestUpdate();

            try
            {
                 manifestConfig = JsonConvert.DeserializeObject<ManifestUpdate>(manifestData);
            }
            catch (Exception)
            {
                //bad json received
                throw;
            }

            //create versions
            manifestConfig.RemoteVersionProper = new Version(manifestConfig.RemoteVersion);
            manifestConfig.LocalVersionProper = new Version(Application.ProductVersion);

            //determine comparison
            int versionCompare = manifestConfig.LocalVersionProper.CompareTo(manifestConfig.RemoteVersionProper);

            if (versionCompare < 0)
            {
                manifestConfig.RemoteVersionNewer = true;
            }
            else
            {
                manifestConfig.RemoteVersionNewer = false;
            }

            return manifestConfig;
        }
    }
}
