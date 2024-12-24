using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiveLog.Models
{
    public class LicensesManager
    {
        public ObservableCollection<Licenses> Licenses { get; } = new ObservableCollection<Licenses>();

        public event Action LicenseAdded;

        public void AddLicense(Licenses license)
        {
            Licenses.Add(license);
            license.WriteToFile(license);
            LicenseAdded?.Invoke();
        }

        public DateTime GetEarliestDate()
        {
            DateTime earliestDate = Licenses.Any() ? Licenses.Min(p => p.IssuedDate) : DateTime.Now;
            return earliestDate;
        }

        public string ConvertDateToText(DateTime diverSinceDate)
        {
            DateTime currentDatetime = DateTime.Now;

            int yearDiff = currentDatetime.Year - diverSinceDate.Year;
            int monthDiff = currentDatetime.Month - diverSinceDate.Month;
            int dayDiff = currentDatetime.Day - diverSinceDate.Day;

            // Adjust for negative months
            if (monthDiff < 0)
            {
                yearDiff--;
                monthDiff += 12;
            }

            // Adjust for negative days
            if (dayDiff < 0)
            {
                monthDiff--;
                DateTime previousMonth = currentDatetime.AddMonths(-1);
                dayDiff += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            }

            // Create the total text
            string yearText = yearDiff > 1 ? "years" : "year";
            string monthText = monthDiff > 1 ? "months" : "month";
            string dayText = dayDiff > 1 ? "days" : "day";

            string diverSinceText = $"You have diving for {yearDiff} {yearText}, {monthDiff} {monthText}, and {dayDiff} {dayText}!";

            return diverSinceText;
        }
    }
}
