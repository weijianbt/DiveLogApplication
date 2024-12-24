using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DiveLog.Models
{
    public class Licenses
    {
        private readonly string _licensesDirectory;
  
        public Licenses() 
        {
            _licensesDirectory = @"C:\Users\twjbr\Documents\DiveLogs\Licenses.xml";
        }

        public string LicenseLevel { get; set; }

        public string LicenseProvider { get; set; }

        public string DiveCentre { get; set; }

        public string Location { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime ObtainedDuration { get; set; }

        public string Id { get; set; }

        public string LicensesDirectory { get; set; }


        public void WriteToFile(Licenses data)
        {
            if (File.Exists(_licensesDirectory))
            {
                var doc = XDocument.Load(_licensesDirectory);

                var newElement = new XElement("License",
                    new XElement("LicenseLevel", LicenseLevel),
                    new XElement("LicenseProvider", LicenseProvider),
                    new XElement("DiveCentre", DiveCentre),
                    new XElement("Location", Location),
                    new XElement("IssuedDate", IssuedDate.ToString("MM/dd/yyyy")),
                    new XElement("Id", Id)
                );

                doc.Root.Add(newElement);
                doc.Save(_licensesDirectory);
            }
            else
            {
                var doc = new XDocument(
                            new XElement("Licenses",
                            new XElement("License",
                            new XElement("LicenseLevel", LicenseLevel),
                            new XElement("LicenseProvider", LicenseProvider),
                            new XElement("DiveCentre", DiveCentre),
                            new XElement("Location", Location),
                            new XElement("IssuedDate", IssuedDate.ToString("MM/dd/yyyy")),
                            new XElement("Id", Id)
                        )
                    )
                );

                doc.Save(_licensesDirectory);
            }
        }

        public void LoadData()
        {

        }




    }
}
