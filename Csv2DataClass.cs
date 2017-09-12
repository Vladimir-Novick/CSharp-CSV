using System;
using System.Collections.Generic;
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

    /// <summary> Read Csv file and convert data to T class <summary>    
    public class Csv2DataClass<T>
    {
        private string rowNameSeporator = null;
        private string dataSeporator = null;
        private string csvFileName = null;
        private List<String> keyList = null;

        private List<string> errorLines = new List<string>();
        List<T> rows = new List<T>();

        CsvReader csvReader = new CsvReader();

        /// <param name="OpenFileName">Full name CSV file specification. Fist row - colum scification </param>
        public Boolean OpenFileName(string _csvFileName)
        {
            csvFileName = _csvFileName;
            errorLines.Clear();
            rows.Clear();
            Boolean ret = csvReader.SetSchema(csvFileName, rowNameSeporator);
            if (ret)
            {
                keyList = csvReader.GetKeys();
            }

            return ret;

        }


        public List<T> ReadFile()
        {


            using (FileStream fileStream = new FileStream(csvFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {

                    string line = reader.ReadLine();
                    if (csvReader.SetRow(line, dataSeporator))
                    {
                        addRow2List();
                    }
                    else
                    {
                        errorLines.Add(line);
                    }

                }
            }
            return rows;
        }

        private void addRow2List()
        {


            Object dst = (Object)Activator.CreateInstance(typeof(T));
            foreach (String column in keyList)
            {
                try
                {
                    var safeValue = csvReader.GetValue(column);

                    var p = dst.GetType().GetProperty(column);
                    if (p != null)
                    {
                        p.SetValue(dst, safeValue);
                    }
                    else
                    {


                    }
                }
                catch (Exception ex)
                {
                 
                }

            }
            T insert = (T)dst;
            rows.Add(insert);
         

        }


        /// <param name="_rowNameSeporator">Used to specify context and colums name seporate. ( as regular expression )</param>
        public Csv2DataClass(string _rowNameSeporator)
        {
            rowNameSeporator = _rowNameSeporator;
            dataSeporator = _rowNameSeporator;
            rows.Clear();
        }

        /// <param name="_rowNameSeporator">Used to seporate first row. ( as regular expression )</param>
        /// <param name="_dataSeporator">Used to specify context seporate.  ( as regular expression )</param>
        public Csv2DataClass(string _rowNameSeporator, string _dataSeporator)
        {
            rowNameSeporator = _rowNameSeporator;
            dataSeporator = _dataSeporator;
            rows = new List<T>();
        }


    }
}
