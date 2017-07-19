using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FileHelpers;

namespace Data
{
    class Helper
    {
        /// <summary>
        /// Checks for value of null or DBNull and returns appropriate type.
        /// </summary>
        /// <returns>
        /// If null then returns the default value for the specified type else returns 
        /// the value cast to the specified type.
        /// </returns>
        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        public static DateTime DateConvertFromDBVal<DateTime>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(DateTime);
            }
            else
            {
                return (DateTime)obj;
            }
        }

        /// <summary>
        /// Parses a csv file.
        /// </summary>
        /// <param name="path">Path to the csv file.</param>
        /// <returns></returns>
        public static Domain.ImportedLead[] ParseCSVFile(string path)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(Domain.ImportedLead));
            Domain.ImportedLead[] leads = engine.ReadFile(path) as Domain.ImportedLead[];

            return leads;
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static string ConverttoStringDate(DateTime date)
        {
            string monthvalue = null;
            if (date.Month == 1)
            {
                monthvalue = "Jan";
            }

            if (date.Month == 2)
            {
                monthvalue = "Feb";
            }

            if (date.Month == 3)
            {
                monthvalue = "Mar";
            }

            if (date.Month == 4)
            {
                monthvalue = "Apr";
            }

            if (date.Month == 5)
            {
                monthvalue = "May";
            }

            if (date.Month == 6)
            {
                monthvalue = "Jun";
            }

            if (date.Month == 7)
            {
                monthvalue = "Jul";
            }

            if (date.Month == 8)
            {
                monthvalue = "Aug";
            }

            if (date.Month == 9)
            {
                monthvalue = "Sept";
            }

            if (date.Month == 10)
            {
                monthvalue = "Oct";
            }

            if (date.Month == 11)
            {
                monthvalue = "Nov";
            }

            if (date.Month == 12)
            {
                monthvalue = "Dec";
            }
            return monthvalue;
        }
    }
}
