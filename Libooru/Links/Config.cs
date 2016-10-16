﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Libooru.Links
{
    public class Config
    {
        public Core core { get; set; }

        public string AppFolderPath { get; set; }

        public ConfigDataSet Data { get; set; }

        public Config(Core core)
        {
            this.core = core;
        }

        public void GetConfig()
        {

            var path = AppFolderPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + @"/config.json";
            if (!File.Exists(filePath))
            {
                var nf = File.Create(filePath);
                var ns = JsonConvert.SerializeObject(new ConfigDataSet());
                using (StreamWriter w = new StreamWriter(nf))
                {
                    w.Write(ns);
                }
            }
            var f = File.OpenRead(filePath);
            string s;
            using (StreamReader r = new StreamReader(f))
            {
                s = r.ReadToEnd();
            }
            this.Data = JsonConvert.DeserializeObject<ConfigDataSet>(s);
        }

        public void ApplyChanges()
        {
            var path = AppFolderPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + @"/config.json";
            if (!File.Exists(filePath))
            {
                var nf = File.Create(filePath);
            }
            var f = File.OpenWrite(filePath);
            var ns = JsonConvert.SerializeObject(Data);
            using (StreamWriter w = new StreamWriter(f))
            {
                w.Write(ns);
            }

            GetConfig();
            core.Update();
        }
    }

    [DataContract]
    public class ConfigDataSet
    {
        [DataMember]
        public string pictureFolderPath { get; set; }
        [DataMember]
        public string newPictureFolderPath { get; set; }
        [DataMember]
        public int safetyLevel { get; set; }

        public ConfigDataSet()
        {
            this.pictureFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.newPictureFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.safetyLevel = 0;

        }
    }
}