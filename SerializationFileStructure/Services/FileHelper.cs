using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            Filelist result = new Filelist();

            // Store a stack of our directories.
            Stack<string> stack = new Stack<string>();

            // Add initial directory.
            stack.Push(path);

            // Continue while there are directories to process
            while (stack.Count > 0)
            {
                // Get top directory
                string dir = stack.Pop();

               // try
                //{
                    // Add all files at this directory to the result List.
                    foreach (var i in Directory.GetFiles(dir, "*.*"))
                    {
                        using (FileStream fstream = File.OpenRead(i))
                        {
                            byte[] array = new byte[fstream.Length];
                            fstream.Read(array, 0, array.Length);
                            //string textFromFile = System.Text.Encoding.Default.GetString(array);

                            result.Add(new FileModel() { Path = i.Substring(path.Length), Data = array });
                        }
                    }

                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                //}
                /*catch
                {
                    // Could not open the directory
                }*/
            }
            return result;
        }
    }
}