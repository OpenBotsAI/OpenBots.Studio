﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace OpenBots.Updater
{
    public partial class frmUpdating : Form
    {
        string topLevelFolder = Application.StartupPath;
        public frmUpdating(string packageURL)
        {
            InitializeComponent();
            bgwUpdate.RunWorkerAsync(packageURL);
        }

        private void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            //get package
            bgwUpdate.ReportProgress(0, "Setting Up...");

            //define update folder
            var tempUpdateFolder = topLevelFolder + "\\temp\\";

            //delete existing
            if (Directory.Exists(tempUpdateFolder))
                Directory.Delete(tempUpdateFolder, true);

            //create folder
            Directory.CreateDirectory(tempUpdateFolder);
          
            //cast arg to string
            string packageURL = (string)e.Argument;

            bgwUpdate.ReportProgress(0, "Downloading Update...");

            //create uri and download package
            Uri uri = new Uri(packageURL);
            string localPackagePath = Path.Combine(tempUpdateFolder, Path.GetFileName(uri.LocalPath));

            //if package exists for some reason then delete
            if (File.Exists(localPackagePath))
                File.Delete(localPackagePath);

            //create web client
            WebClient newWebClient = new WebClient();

            //download file
            newWebClient.DownloadFile(uri, localPackagePath);

            bgwUpdate.ReportProgress(0, "Extracting Update...");

            using (FileStream zipToOpen = new FileStream(localPackagePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    ExtractZipToDirectory(archive, tempUpdateFolder, true);
            }

            //create deployment folder reference
            var deploymentFolder = tempUpdateFolder + "OpenBotsStudio\\";

            bgwUpdate.ReportProgress(0, "Deployed to " + deploymentFolder);
            bgwUpdate.ReportProgress(0, "Updating Files...");

            //copy deployed files to top level
            CopyDirectory(deploymentFolder, topLevelFolder);

            //clean up old folder
            // Directory.Delete(tempUpdateFolder);
        }

        public void ExtractZipToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }

        public void CopyDirectory(string source, string target)
        {
            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                    File.Copy(file, Path.Combine(folders.Target, Path.GetFileName(file)), true);

                foreach (var folder in Directory.GetDirectories(folders.Source))
                    stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
            }
        }

        private void frmUpdating_Load(object sender, EventArgs e)
        {
        }

        private void bgwUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblUpdate.Text = e.UserState.ToString();
            //MessageBox.Show(e.UserState.ToString());
        }

        private void bgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error is null)
            {
                Process.Start(topLevelFolder + "\\OpenBots.exe");

                Close();
                //MessageBox.Show("All Done");
                lblUpdate.Text = "All Done!";
            }
            else
                MessageBox.Show(e.Error.ToString());
        }
    }

    
}
