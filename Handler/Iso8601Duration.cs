using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTest.Handler
{
    using System;
using System.Text.RegularExpressions;

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

}