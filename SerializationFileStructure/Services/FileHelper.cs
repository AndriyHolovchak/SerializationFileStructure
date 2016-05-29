using System;
using System.Collections.Generic;
using System.IO;
using SerializationFileStructure.Models;

namespace SerializationFileStructure.Services
{
    [Serializable]
    public class Filelist : List<FileModel>
    {
    }

    public class FileHelper
    {
        public static Filelist GetFilesRecursive(string path)
        {
            // Store results in the file results list.
            var result = new Filelist();

            // Store a stack of our directories.
            var stack = new Stack<string>();

            // Add initial directory.
            stack.Push(path);

            // Continue while there are directories to process
            while (stack.Count > 0)
            {
                // Get top directory
                var dir = stack.Pop();

                foreach (var i in Directory.GetFiles(dir, "*.*"))
                {
                    using (var fstream = File.OpenRead(i))
                    {
                        var array = new byte[fstream.Length];
                        fstream.Read(array, 0, array.Length);
                        result.Add(new FileModel {Path = i.Substring(path.Length), Data = array});
                    }
                }

                // Add all directories at this directory.
                foreach (var dn in Directory.GetDirectories(dir))
                {
                    stack.Push(dn);
                }
            }
            return result;
        }
    }
}