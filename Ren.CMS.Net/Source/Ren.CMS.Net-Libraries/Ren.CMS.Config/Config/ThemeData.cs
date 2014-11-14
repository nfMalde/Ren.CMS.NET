using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Samples.AspNet
{
    public class ThemeDataSection : ConfigurationSection
    {
        // Create a "remoteOnly" attribute.
        [ConfigurationProperty("title", DefaultValue = "Theme", IsRequired = true)]
        public string RemoteOnly
        {
            get
            {
                return (string)this["title"];
            }
            set
            {
                this["title"] = value;
            }
        }

        // Create a "font" element.
        [ConfigurationProperty("author")]
        public string Author
        {
            get
            {
                return (string)this["author"];
            }
            set
            { this["author"] = value; }
        }

        // Create a "color element."
        [ConfigurationProperty("themeName")]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string ThemeName
        {
            get
            {
                return (string)this["themeName"];
            }
            set
            { this["themeName"] = value; }
        }

        [ConfigurationProperty("description")]
        public string Description
        {
            get
            {
                return (string)this["description"];
            }
            set
            {
                this["description"] = value;
            }
        }
    }

    // Define the "font" element
    // with "name" and "size" attributes.
    public class FontElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "Arial", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("size", DefaultValue = "12", IsRequired = false)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 24, MinValue = 6)]
        public int Size
        {
            get
            { return (int)this["size"]; }
            set
            { this["size"] = value; }
        }
    }

    // Define the "color" element 
    // with "background" and "foreground" attributes.
    public class ColorElement : ConfigurationElement
    {
        [ConfigurationProperty("background", DefaultValue = "FFFFFF", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public String Background
        {
            get
            {
                return (String)this["background"];
            }
            set
            {
                this["background"] = value;
            }
        }

        [ConfigurationProperty("foreground", DefaultValue = "000000", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public String Foreground
        {
            get
            {
                return (String)this["foreground"];
            }
            set
            {
                this["foreground"] = value;
            }
        }

    }

}