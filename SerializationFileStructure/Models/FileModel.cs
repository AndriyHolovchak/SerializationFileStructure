using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerializationFileStructure.Models
{
    [Serializable]
    public class FileModel
    {
        public string Path { get; set; }
        public byte[] Data { get; set; }
    }
}