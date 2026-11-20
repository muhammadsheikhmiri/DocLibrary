using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChaptereight.BlogWeb.CoreLayer.Utilities
{
    public static class DateHelper
    {

        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            try
            {
                return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime).ToString().PadLeft(4, '0'),
                    pc.GetMonth(dateTime).ToString().PadLeft(2, '0'),
                    pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'));
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// Sample Format = ds dd ms Y
        /// </summary>
        /// <param name="dateTime">تاریخ میلادی</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToPersianDate(this DateTime dateTime, string format)
        {
            //@DateTime.Now.ToPersianDate("ds dd ms Y")
            PersianCalendar pc = new PersianCalendar();
            try
            {
                string date = format.Replace("Y", pc.GetYear(dateTime).ToString().PadLeft(4, '0'))
                    .Replace("mm", pc.GetMonth(dateTime).ToString())
                    .Replace("MM", pc.GetMonth(dateTime).ToString().PadLeft(2, '0'))
                    .Replace("dd", pc.GetDayOfMonth(dateTime).ToString())
                    .Replace("DD", pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'))
                    .Replace("ds", GetDayOfWeekString((int)pc.GetDayOfWeek(dateTime)).ToString())
                    .Replace("ms", GetMonthString(pc.GetMonth(dateTime)).ToString())
                    ;
                return date;
            }
            catch
            {
                return "";
            }
        }

        private static string GetDayOfWeekString(int day)
        {
            string[] days = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            if (day <= days.Length)
            {
                return days[day];
            }
            return "";
        }
        private static string GetMonthString(int month)
        {
            string[] months = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            if (month <= months.Length)
            {
                return months[month - 1];
            }
            return "";
        }
    }
}
