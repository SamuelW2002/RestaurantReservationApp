using System.Globalization;

namespace Utility
{
    public static class DateParser
    {
        public static DateTime ParseDate(string date)
        {
            Contracts.Require(DateTime.TryParseExact(
                date,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsedDate),
                "This date is not of valid format, please use dd/MM/yyyy");

            return parsedDate;
        }
    }
}
