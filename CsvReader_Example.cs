using Crawler.Config;
using System;
using System.Collections.Generic;
using System.IO;
using SGcombo.CsvUtils;

namespace Example_Converter
{
    public class FileConverter
    {
        public FileConverter()
        {
        }

        private List<Destination> Distenations = new List<Destination>();

        private List<Attr> attr = new List<Attr>();

        internal void Run()
        {
            String fileName = CrawlerConfig.GetConfigData.TargetFolder + "data" + Path.DirectorySeparatorChar + "tst1.csv";
            LoadDestinations(Distenations, fileName);

            fileName = CrawlerConfig.GetConfigData.TargetFolder + "data" + Path.DirectorySeparatorChar + "tst2.csv";
            LoadAttr(attr, fileName);

        }

        private void LoadAttr(List<Attr> attributes, string fileName)
        {
            ConvertUtils converter = new ConvertUtils();

            converter.SetSchema(fileName);

            String line;

            int counter = 0;


            using (Stream stream = File.OpenRead(fileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(stream))
                {
                    while ((line = file.ReadLine()) != null)
                    {

                        if (counter == 0) { counter++; continue; }

                        while (!converter.SetRow(line))
                        {
                            String line2 = file.ReadLine();
                            line = line + " " + line2;
                        }
                        converter.SetRow(line);

                        Attr attr = new Attr()
                        {
                            HOTELCODE = converter.GetValue("HOTELCODE"),
                            ATTRIBUTECODE = converter.GetValue("ATTRIBUTECODE"),
                            ATTRIBUTEDESCRIPTION = converter.GetValue("ATTRIBUTEDESCRIPTION"),
                            ATTRIBUTESERVICE = converter.GetValue("ATTRIBUTESERVICE"),
                            ATTRIBUTEDETAILCODE = converter.GetValue("ATTRIBUTEDETAILCODE"),
                            ATTRIBUTEDETAILDESCRIPTION = converter.GetValue("ATTRIBUTEDETAILDESCRIPTION"),
                            ATTRIBUTEFREE = converter.GetValue("ATTRIBUTEFREE")
                        };

                        attributes.Add(attr);

                    }

                }



            }
        }

        private void LoadDestinations(List<Destination> distenations, string fileName)
        {

            ConvertUtils converter = new ConvertUtils();

            converter.SetSchema(fileName);

            String line;

            int counter = 0;


            using (Stream stream = File.OpenRead(fileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(stream))
                {
                    while ((line = file.ReadLine()) != null)
                    {

                        if (counter == 0) { counter++; continue; }

                        while (!converter.SetRow(line))
                        {
                            String line2 = file.ReadLine();
                            line = line + " " + line2;
                        }
                        converter.SetRow(line);

                        Destination destination = new Destination()
                        {
                            ID_COUNTRY = converter.GetValue("ID_COUNTRY"),
                            NAME_COUNTRY = converter.GetValue("NAME_COUNTRY"),
                            ID_PROVINCE = converter.GetValue("ID_PROVINCE"),
                            NAME_PROVINCE = converter.GetValue("NAME_PROVINCE"),
                            ID_TOWN = converter.GetValue("ID_TOWN"),
                            NAME_TOWN = converter.GetValue("NAME_TOWN")
                        };

                        distenations.Add(destination);

                    }

                }



            }

        }
    }
}