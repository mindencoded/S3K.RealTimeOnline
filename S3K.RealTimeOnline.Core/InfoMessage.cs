﻿using System.Runtime.Serialization;

namespace S3K.RealTimeOnline.Core
{
    [DataContract]
    public class InfoMessage
    {
        public InfoMessage(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message { get; set; }
    }
}