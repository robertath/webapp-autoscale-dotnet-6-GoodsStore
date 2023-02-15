using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Tests
{
    public class Source
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Status { get; set; }
    }

    public class Destination
    {
        public int Value1 { get; set; }
        public DateTime Value2 { get; set; }
        public Type Value3 { get; set; }
        public bool Status { get; set; }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
    }

    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source, Type destination, ResolutionContext context)
        {
            return Assembly.GetExecutingAssembly().GetType(source);
        }
    }

    public class BooleanTypeConverter : ITypeConverter<string, Boolean>
    {
        public Boolean Convert(string source, Boolean destination, ResolutionContext context)
        {
            if (source == "Active")
                return true;
            else
                return false;
        }
    }
}
