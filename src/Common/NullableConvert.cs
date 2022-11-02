namespace Common;

/// <summary>
/// Статический класс является аналогом стандартного класса <see cref="Convert"/> с поправкой на nullable-типы.
/// Все методы конвертируют значения, равные null, в null. Значения, отличные от null, конвертируются так же, 
/// как в обычном <see cref="Convert"/>.
/// </summary>
public static class NullableConvert
{
    public static bool? ToBoolean(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToBoolean);
    }

    public static bool? ToBoolean(byte? value) => ConvertIt(value, Convert.ToBoolean);
    public static bool? ToBoolean(int? value) => ConvertIt(value, Convert.ToBoolean);
    public static bool? ToBoolean(uint? value) => ConvertIt(value, Convert.ToBoolean);
    public static bool? ToBoolean(long? value) => ConvertIt(value, Convert.ToBoolean);
    public static bool? ToBoolean(ulong? value) => ConvertIt(value, Convert.ToBoolean);

    public static DateTime? ToDateTime(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToDateTime);
    }

    public static double? ToDouble(decimal? value) => ConvertIt(value, Convert.ToDouble);
    public static double? ToDouble(long? value) => ConvertIt(value, Convert.ToDouble);
    public static double? ToDouble(ulong? value) => ConvertIt(value, Convert.ToDouble);

    public static double? ToDouble(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToDouble);
    }

    public static decimal? ToDecimal(double? value) => ConvertIt(value, Convert.ToDecimal);

    public static decimal? ToDecimal(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToDecimal);
    }

    public static int? ToInt32(bool? value) => ConvertIt(value, Convert.ToInt32);
    public static int? ToInt32(double? value) => ConvertIt(value, Convert.ToInt32);
    public static int? ToInt32(decimal? value) => ConvertIt(value, Convert.ToInt32);
    public static int? ToInt32(uint? value) => ConvertIt(value, Convert.ToInt32);
    public static int? ToInt32(long? value) => ConvertIt(value, Convert.ToInt32);
    public static int? ToInt32(ulong? value) => ConvertIt(value, Convert.ToInt32);

    public static int? ToInt32(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToInt32);
    }

    public static long? ToInt64(bool? value) => ConvertIt(value, Convert.ToInt64);
    public static long? ToInt64(double? value) => ConvertIt(value, Convert.ToInt64);
    public static long? ToInt64(decimal? value) => ConvertIt(value, Convert.ToInt64);
    public static long? ToInt64(ulong? value) => ConvertIt(value, Convert.ToInt64);

    public static long? ToInt64(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToInt64);
    }

    public static uint? ToUInt32(bool? value) => ConvertIt(value, Convert.ToUInt32);
    public static uint? ToUInt32(double? value) => ConvertIt(value, Convert.ToUInt32);
    public static uint? ToUInt32(decimal? value) => ConvertIt(value, Convert.ToUInt32);
    public static uint? ToUInt32(int? value) => ConvertIt(value, Convert.ToUInt32);
    public static uint? ToUInt32(long? value) => ConvertIt(value, Convert.ToUInt32);
    public static uint? ToUInt32(ulong? value) => ConvertIt(value, Convert.ToUInt32);

    public static uint? ToUInt32(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToUInt32);
    }

    public static ulong? ToUInt64(bool? value) => ConvertIt(value, Convert.ToUInt64);
    public static ulong? ToUInt64(double? value) => ConvertIt(value, Convert.ToUInt64);
    public static ulong? ToUInt64(decimal? value) => ConvertIt(value, Convert.ToUInt64);
    public static ulong? ToUInt64(int? value) => ConvertIt(value, Convert.ToUInt64);
    public static ulong? ToUInt64(long? value) => ConvertIt(value, Convert.ToUInt64);

    public static ulong? ToUInt64(object value, IFormatProvider formatProvider)
    {
        return ConvertIt(value, formatProvider, Convert.ToUInt64);
    }

    public static Guid? ToGuid(string value)
    {
        return (value == null) ? (Guid?) null : Guid.Parse(value);
    }

    public static Guid? ToGuid(object value)
    {
        switch (value)
        {
            case null:
                return null;
            case string str:
                return Guid.Parse(str);
            default:
                return (Guid) value;
        }
    }

    private static T2? ConvertIt<T1, T2>(T1? value, Func<T1, T2> convert)
        where T1 : struct
        where T2 : struct
    {
        return (value == null) ? (T2?) null : convert(value.Value);
    }

    private static T? ConvertIt<T>(object value, IFormatProvider formatProvider, Func<object, IFormatProvider, T> convert)
        where T : struct
    {
        return (value == null) ? (T?) null : convert(value, formatProvider);
    }

}

