using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiveLog.Models
{
    public class SaveEntry
    {
        public static void WriteToFile(Licenses data, string filepath)
        {
            Console.WriteLine("test write");
            using (var writer = XmlWriter.Create(filepath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("License");

                writer.WriteElementString("LicenseLevel", data.LicenseLevel);
                writer.WriteElementString("LicenseProvider", data.LicenseProvider);
                writer.WriteElementString("DiveCentre", data.DiveCentre);
                writer.WriteElementString("Location", data.Location);
                //writer.WriteElementString("IssuedDate", data.IssuedDate as string);
                writer.WriteElementString("Id", data.Id);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
