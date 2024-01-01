using System.Text.RegularExpressions;

namespace FirstTest.Handler
{
    public static class Iso8601Duration
    {
        public static TimeSpan Parse(string input)
        {
            var match = Regex.Match(input, @"PT(?:(\d+)H)?(?:(\d+)M)?(?:(\d+(?:\.\d{1,3})?)S)?");

            if (!match.Success)
                throw new ArgumentException("Invalid ISO 8601 duration format", nameof(input));

            var hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
            var minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
            var seconds = match.Groups[3].Success ? double.Parse(match.Groups[3].Value) : 0;

            return new TimeSpan(0, hours, minutes, (int)seconds, (int)((seconds - (int)seconds) * 1000));
        }
    }

    public static class LiteralDuration
    {
        public static TimeSpan Parse(string input)
        {
            TimeSpan result = new TimeSpan();

            // Match and capture all parts of the input string that consist of a number followed by a letter
            foreach (Match match in Regex.Matches(input, @"(\d+)([a-zA-Z])"))
            {
                int value = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value;

                // Add to the result based on the unit of time
                switch (unit)
                {
                    case "s":
                        result = result.Add(TimeSpan.FromSeconds(value));
                        break;
                    case "j":
                        result = result.Add(TimeSpan.FromDays(value));
                        break;
                    case "h":
                        result = result.Add(TimeSpan.FromHours(value));
                        break;
                    case "m":
                        result = result.Add(TimeSpan.FromMinutes(value));
                        break;
                    default:
                        throw new FormatException("Unrecognized time unit: " + unit);
                }
            }

            return result;
        }
    }
}