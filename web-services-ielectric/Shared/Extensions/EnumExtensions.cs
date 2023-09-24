using System.ComponentModel;
using System.Reflection;
namespace web_services_ielectric.Shared.Extensions;

public static class EnumExtensions
{
    public static string ToDescriptionString<TEnum>(this TEnum sourceEnum)
    {
        FieldInfo info = sourceEnum.GetType().GetField(sourceEnum.ToString());
        var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes?[0].Description ?? sourceEnum.ToString();
    }
}