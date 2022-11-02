using System.Globalization;

namespace Common.Extensions;

public static class CurrentCulture
{
    /// <summary>
    /// Форматирует строку согласно CurrentCulture. Отличается от <see cref="string.Format(string, object)"/> 
    /// только тем, что не имеет параметра типа <seealso cref="IFormatProvider"/>, 
    /// поэтому анализатор не генерирует предупреждение при его отсутствии (CA1305:Specify IFormatProvider).
    /// </summary>
    public static string Format(string format, object arg0)
    {
        return string.Format(CultureInfo.CurrentCulture, format, arg0);
    }

    /// <summary>
    /// Форматирует строку согласно CurrentCulture. Отличается от <see cref="string.Format(string, object, object)"/> 
    /// только тем, что не имеет параметра типа <seealso cref="IFormatProvider"/>, 
    /// поэтому анализатор не генерирует предупреждение при его отсутствии (CA1305:Specify IFormatProvider).
    /// </summary>
    public static string Format(string format, object arg0, object arg1)
    {
        return string.Format(CultureInfo.CurrentCulture, format, arg0, arg1);
    }

    /// <summary>
    /// Форматирует строку согласно CurrentCulture. Отличается от <see cref="string.Format(string, object, object, object)"/> 
    /// только тем, что не имеет параметра типа <seealso cref="IFormatProvider"/>, 
    /// поэтому анализатор не генерирует предупреждение при его отсутствии (CA1305:Specify IFormatProvider).
    /// </summary>
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
        return string.Format(CultureInfo.CurrentCulture, format, arg0, arg1, arg2);
    }

    /// <summary>
    /// Форматирует строку согласно CurrentCulture. Отличается от <see cref="string.Format(string, object?[])"/> 
    /// только тем, что не имеет параметра типа <seealso cref="IFormatProvider"/>, 
    /// поэтому анализатор не генерирует предупреждение при его отсутствии (CA1305:Specify IFormatProvider).
    /// </summary>
    public static string Format(string format, params object[] args)
    {
        return string.Format(CultureInfo.CurrentCulture, format, args);
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture и строке форматирования
    /// </summary>
    public static string ToLocalString<T>(this T value, string format) where T : struct, IFormattable
    {
        if (format == null)
            throw new ArgumentNullException(nameof(format));
        return value.ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture
    /// </summary>
    public static string ToLocalString<T>(this T value) where T : struct, IFormattable
    {
        return value.ToString(null, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture. Значение null преобразуется в пустую строку
    /// (так же поступает <see cref="Nullable{T}.ToString"/>).
    /// </summary>
    public static string ToLocalString<T>(this T? value) where T : struct, IFormattable
    {
        // В случае null возвращаем string.Empty точно так же, как делает Nullable<T>.ToString()
        return value.HasValue ? value.Value.ToString(null, CultureInfo.CurrentCulture) : string.Empty;
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture и строке форматирования.
    /// Значение null преобразуется в пустую строку (так же поступает <see cref="Nullable{T}.ToString"/>).
    /// </summary>
    public static string ToLocalString<T>(this T? value, string format) where T : struct, IFormattable
    {
        if (format == null)
            throw new ArgumentNullException(nameof(format));
        // В случае null возвращаем string.Empty точно так же, как делает Nullable<T>.ToString()
        return value.HasValue ? value.Value.ToString(format, CultureInfo.CurrentCulture) : string.Empty;
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture
    /// </summary>
    public static string ToLocalString(this IFormattable formattable)
    {
        return formattable.ToString(null, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Преобразует значение в строку согласно CurrentCulture и строке форматирования.
    /// </summary>
    public static string ToLocalString(this IFormattable formattable, string format)
    {
        if (format == null)
            throw new ArgumentNullException(nameof(format));
        return formattable.ToString(format, CultureInfo.CurrentCulture);
    }

}
