using System;

namespace Meme_Platform.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string GetTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}
