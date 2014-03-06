using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperLib
{
    public class Event
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public bool IsPrimary { get; set; }

        public string Date { get; set; }

        public int SortDateValue { 
            get
            {
                const string months = "JANFEBMARAPRMAYJUNJULAUGSEPOCTNOVDEC";

                int dateValue = -1;

                if (Date.Length > 0)
                {
                    dateValue = 1;

                    // YYYY
                    if (Date.Length == 4)
                    {
                        dateValue = Convert.ToInt32(Date) * 10000;
                    }

                    // Q? YYYY
                    if (Date.Length == 7)
                    {
                        if (Date[0] == 'Q')
                        {
                            dateValue = Convert.ToInt32(Date.Substring(3, 4)) * 10000;
                            dateValue += ((Convert.ToInt32(Date.Substring(1, 1)) - 1) * 3) * 100;
                        }
                    }

                    // MMM YYYY
                    if (Date.Length == 8)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(4, 4)) * 10000;
                        dateValue += (months.IndexOf(Date.Substring(0, 3).ToUpper()))/3 * 100;
                    }

                    // DD MMM YYYY
                    if (Date.Length == 11)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(7, 4)) * 10000;
                        dateValue += (months.IndexOf(Date.Substring(3, 3).ToUpper())) / 3 * 100;
                        dateValue += Convert.ToInt32(Date.Substring(0, 2));
                    }

                    // YYYY or YYYY
                    if (Date.Length == 12)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(0, 4)) * 10000;
                    }

                    // bef DD MMM YYYY
                    // aft DD MMM YYYY
                    if (Date.Length == 15)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(11, 4)) * 10000;
                        dateValue += (months.IndexOf(Date.Substring(7, 3).ToUpper())) / 3 * 100;
                        dateValue += Convert.ToInt32(Date.Substring(4, 2));

                        if (Date.Substring(0, 3).ToUpper() == "BEF")
                        {
                            dateValue -= 1;
                        }
                        if (Date.Substring(0, 3).ToUpper() == "AFT")
                        {
                            dateValue += 1;
                        }
                    }

                    // MMM YYYY - MMM YYYY
                    if (Date.Length == 19)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(4, 4)) * 10000;
                        dateValue += (months.IndexOf(Date.Substring(0, 3).ToUpper())) / 3 * 100;
                    }

                    // DD MMM YYYY - DD MMM YYYY
                    if (Date.Length == 25)
                    {
                        dateValue = Convert.ToInt32(Date.Substring(7, 4)) * 10000;
                        dateValue += (months.IndexOf(Date.Substring(3, 3).ToUpper())) / 3 * 100;
                        dateValue += Convert.ToInt32(Date.Substring(0, 2));
                    }
                }

                return dateValue;
            } 

            private set {} 
        }
    }
}
