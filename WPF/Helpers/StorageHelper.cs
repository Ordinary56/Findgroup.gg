using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Helpers
{
    public interface IStorageHelper
    {
        public void SaveData(string filename, string data);
        public string LoadData(string filename);
    }
    /// <summary>
    /// <para>This class is responsible for small user preferences/configuration.</para>
    /// <para>Smiliar to localStorage javascript</para>
    /// </summary>
    public class StorageHelper : IStorageHelper
    {
        private IsolatedStorageFile _isolatedStorageFile;
        public StorageHelper()
        {
            
            _isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
        }
        public void SaveData(string fileName, string data)
        {
            using IsolatedStorageFileStream stream = new(fileName, System.IO.FileMode.Create, _isolatedStorageFile);
            using StreamWriter writer = new(stream);
            writer.Write(data);
        }
        public string LoadData(string fileName) 
        {
            try
            {
                using IsolatedStorageFileStream stream = new(fileName, FileMode.Open, _isolatedStorageFile);
                using StreamReader reader = new(stream);
                return reader.ReadToEnd();
            }
            catch(FileNotFoundException)
            {
                return "-- NOT FOUND --";
            }
            catch(Exception ex)
            {
                throw new IOException("An error occured while reading the file.", ex);
            }
        }
    }
}
