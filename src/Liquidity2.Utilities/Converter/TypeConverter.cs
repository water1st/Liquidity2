using System;

namespace Liquidity2.Utilities
{
    public static class TypeConverter
    {
        public static dynamic Convert(dynamic obj, Type type)
        {
            return System.Convert.ChangeType(obj, type);
        }
    }
}
