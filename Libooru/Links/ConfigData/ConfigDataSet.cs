﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Libooru.Links.ConfigData
{
    [DataContract]
    public class ConfigDataSet
    {
        [DataMember]
        public General General { get; set; }

        [DataMember]
        public Folders Folders { get; set; }

        [DataMember]
        public Tags Tags { get; set; }

        [DataMember]
        public Externals Externals { get; set; }

        public ConfigDataSet()
        {
            this.General = new General();
            this.Folders = new Folders();
            this.Tags = new Tags();
            this.Externals = new Externals();

            this.Folders.PictureFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.Folders.NewPictureFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            this.Tags.SafetyLevel = 0;

            this.Externals.Danbooru.Login = "";
            this.Externals.Danbooru.ApiKey = "";
        }
    }
}