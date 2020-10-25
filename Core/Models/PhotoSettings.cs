using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class PhotoSettings
    {
        public int MaxLength { get; set; }
        public string[] AcceptedFileNames { get; set; }

        public bool IsSupported(string fileName) 
        {
            return AcceptedFileNames.Any(s => s == Path.GetExtension(fileName.ToLower()));
        }
    }
}
