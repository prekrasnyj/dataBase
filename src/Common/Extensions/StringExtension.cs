using System.Text;

namespace Common.Extensions;

public static partial class StringExtension
{
    private static readonly char[] NewLineChars = new char[] { '\r', '\n' };

    /// <summary>
    /// Проверка на то, что строка пустая или null
    /// </summary>
    /// <param name="source">строка, может быть null</param>
    /// <returns>true, если строка пустая</returns>
    public static bool IsEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    /// <summary>
    /// Форматирует строку как строковую константу для передачи в SQL. Строка принимает вид <c>"N'string chars'"</c>. 
    /// Значение <c>null</c> превращается в строку <c>"null"</c>.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SqlQuotedStr(this string str)
    {
        return (str == null) ? "null" : "N'" + str.Replace("'", "''") + "'";
    }

    /// <summary>
    /// Заменяет все символы конца строки на указанный разделитель
    /// </summary>
    /// <param name="str"></param>
    /// <param name="lineSeparator">разделитель строк</param>
    /// <returns></returns>
    public static string NormalizeCrLf(this string str, string lineSeparator)
    {
        if (str == null)
            return string.Empty;

        int count = 0;
        StringBuilder builder = new();

        foreach (string line in EnumLines(str))
        {
            if (count == 0)
            {
                // оптимизация на случай единственной строки
                if (ReferenceEquals(line, str))
                    return str;
            }
            else
                builder.Append(lineSeparator);
            builder.Append(line);
            count++;
        }

        if (count == 0)
            return string.Empty;

        return builder.ToString();
    }

    /// <summary>
    /// Перебирает все строки в многострочном тексте. Если текст заканчивается символом конца строки, то последняя
    /// строка возвращается как String.Empty. Если текст начинается с символа конца строки, то первая строка
    /// возвращается как String.Empty. Если текст - пустая строка, возвращается пустое множество. Если входной
    /// параметр равен null, дает ошибку.
    /// </summary>
    /// <param name="str"></param>
    public static IEnumerable<string> EnumLines(this string str)
    {
        if (str == null)
            throw new ArgumentNullException(nameof(str));

        int len = str.Length;

        if (len == 0)
            yield break;

        int index = 0;

        while (true)
        {
            if (index == len)
            {
                yield return string.Empty;
                break;
            }

            int pos = str.IndexOfAny(NewLineChars, index);

            if (pos == -1)
            {
                // для оптимизации возвращаем саму str, если возможно
                yield return (index == 0) ? str : str.Substring(index);
                break;
            }
            yield return str.Substring(index, pos - index);

            // eating \r\n sequence
            if (str[pos] == '\r' && pos + 1 < len && str[pos + 1] == '\n')
                pos++;

            index = pos + 1;
        }
    }

    public static bool TryGetSeconds(this string input, out TimeSpan? timeSpan)
    {
        timeSpan = null;
        if (input.EndsWith("s", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^1], out double seconds))
                return false;
            timeSpan = TimeSpan.FromSeconds(seconds);
            return true;
        }
        if (input.EndsWith("sec", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^3], out double seconds))
                return false;
            timeSpan = TimeSpan.FromSeconds(seconds);
            return true;
        }
        return false;
    }

    public static bool TryGetMinutes(this string input, out TimeSpan? timeSpan)
    {
        timeSpan = null;
        if (input.EndsWith("m", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^1], out double minutes))
                return false;
            timeSpan = TimeSpan.FromMinutes(minutes);
            return true;
        }
        if (input.EndsWith("min", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^3], out double minutes))
                return false;
            timeSpan = TimeSpan.FromMinutes(minutes);
            return true;
        }
        return false;
    }

    public static bool TryGetHours(this string input, out TimeSpan? timeSpan)
    {
        timeSpan = null;
        if (input.EndsWith("h", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^1], out double hours))
                return false;
            timeSpan = TimeSpan.FromHours(hours);
            return true;
        }
        if (input.EndsWith("st", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^2], out double hours))
                return false;
            timeSpan = TimeSpan.FromHours(hours);
            return true;
        }
        return false;
    }

    public static bool TryGetDays(this string input, out TimeSpan? timeSpan)
    {
        timeSpan = null;
        if (input.EndsWith("d", StringComparison.InvariantCultureIgnoreCase) || input.EndsWith("t", StringComparison.InvariantCultureIgnoreCase))
        {
            if (!double.TryParse(input[0..^1], out double days))
                return false;
            timeSpan = TimeSpan.FromDays(days);
            return true;
        }
        return false;
    }

    public static bool TryGetPerma(this string input, out TimeSpan? timeSpan)
    {
        switch (input.ToLower())
        {
            case "-1":
            case "-":
            case "perma":
            case "permamute":
            case "permaban":
            case "permanent":
            case "never":
            case "nie":
                timeSpan = TimeSpan.MaxValue;
                return true;

            default:
                timeSpan = null;
                return false;
        }
    }

    public static bool TryGetZero(this string input, out TimeSpan? timeSpan)
    {
        switch (input.ToLower())
        {
            case "0":
            case "unmute":
            case "unban":
            case "stop":
            case "no":
            case "null":
                timeSpan = TimeSpan.MinValue;
                return true;

            default:
                timeSpan = null;
                return false;
        }
    }

}
