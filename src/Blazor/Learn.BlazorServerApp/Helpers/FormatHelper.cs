namespace Learn.BlazorServerApp.Helpers
{
    public class FormatHelper
    {
        public static string FormatDate(DateTime? dateTime, string format)
        {
            if (dateTime == DateTime.MinValue || dateTime == null)
                return "";
            return dateTime.Value.ToString(format);
        }

        public static string FormatBoolean(bool? value)
        {
            if (!value.HasValue || !value.Value)
                return "No";
            return "Yes";
        }
    }
}
