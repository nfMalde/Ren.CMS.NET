using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogModule.Blog.Helpers
{
    public enum BlogTypeEnum
    {
        [Description("*")]
        all = 1,
        [Description("eNews")]
        news,
        [Description("eArticle")]
        article,

    }

    
public static class EnumHelper<T>
   {
       public static string GetEnumDescription(string value)
       {
           Type type = typeof(T);
           var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();
            
           if (name == null)
           {
               return string.Empty;
           }
           var field = type.GetField(name);
           var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
           return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
       }
   }
}