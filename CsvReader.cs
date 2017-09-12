using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

////////////////////////////////////////////////////////////////////////////
//	Copyright 2017 : Vladimir Novick    https://www.linkedin.com/in/vladimirnovick/  
//
//         https://github.com/Vladimir-Novick/CSharp-CSV
//
//    NO WARRANTIES ARE EXTENDED. USE AT YOUR OWN RISK. 
//
// To contact the author with suggestions or comments, use  :vlad.novick@gmail.com
//
////////////////////////////////////////////////////////////////////////////


namespace SGcombo.CsvUtils
{
    public class CsvReader
    {


        public string GetValue(String key)
        {
            int index = -1;
            _dictionary.TryGetValue(key, out index);
            if (index == -1)
            {
                throw new Exception($"invalid field name <{key}> on file {_fileName}");
            }
            return _row[index];
        }

        private String[] _row;

        private Dictionary<String, int> _dictionary;

        public List<String> GetKeys()
        {
            List<string> keyList = new List<string>(this._dictionary.Keys);
            return keyList;
        }

        private string _fileName;

        public Boolean SetRow(string line, String _seporator = null)
        {
            if (_seporator == null)
            {
                _row = Regex.Split(line, seporator);
            } else
            {
                _row = Regex.Split(line, _seporator);
            }
            if (_row.Length != _dictionary.Count - 1) return false;
            return true;

        }


        public String seporator = @"[|]";

        public Boolean SetSchema(string filepath, String _seporator = @"[|]")
        {
            _fileName = filepath;
            seporator = _seporator;

            Dictionary<String, int> dictionary = new Dictionary<string, int>();


            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = reader.ReadLine();
                    string[] fieldsName = Regex.Split(line, seporator);

                    for (int i = 0; i < fieldsName.Length; i++)
                    {
                        string key = fieldsName[i].Trim();
                        if (dictionary.ContainsKey(key))
                        {
                            return false;
                        }
                        dictionary.Add(, i);
                    }


                }
            }

            _dictionary = dictionary;

            return true;


        }






    }
}
