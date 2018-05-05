﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanShot.Models
{
    public class Settings
    {
        public static Settings Current { get; set; } = new Settings();

        public string SaveFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\CleanShot\\";
        public bool SaveToDisk { get; set; } = true;
        public bool CopyToClipboard { get; set; } = true;
        public bool OpenInEditor { get; set; } = true;
        public bool AlwaysOnTop { get; set; } = true;
        public bool StartWithWindows { get; set; } = true;
        public bool CreateDesktopShortcut { get; set; } = true;
        public bool CreateStartMenuItem { get; set; } = true;
        public string MemeFontFamily { get; set; }
        public int MemeMaxFontSize { get; set; } = 36;
        public bool CaptureCursor { get; set; } = true;
        public bool Uninstalled { get; set; } = false;
        public bool IsTrayNotificationEnabled { get; set; } = true;
        public bool IsCloseToSystemTrayEnabled { get; set; } = true;

        public static void Load()
        {
            var fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CleanShot\Settings.json");
            if (fileInfo.Exists)
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Settings));
                using (var fs = new FileStream(fileInfo.FullName, System.IO.FileMode.OpenOrCreate))
                {
                    var settings = (Settings)serializer.ReadObject(fs);
                    foreach (var prop in typeof(Settings).GetProperties())
                    {
                        prop.SetValue(Settings.Current, prop.GetValue(settings));
                    }
                }
            }
        }
        public static void Save()
        {
            if (!Settings.Current.Uninstalled)
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Settings));
                var strSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CleanShot";
                Directory.CreateDirectory(strSaveFolder);
                var fs = new FileStream(strSaveFolder + @"\Settings.json", FileMode.Create);
                serializer.WriteObject(fs, Settings.Current);
                fs.Close();
            }
        }
    }
}
