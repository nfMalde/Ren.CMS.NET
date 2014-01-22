using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.CORE.Extras
{
    public class FormGatherer
    {

        private HttpRequestBase request = null;

        public FormGatherer(HttpRequestBase RequestBase)
        {

            this.request = RequestBase;
        
        }

        public void SetRequestBase(HttpRequestBase requestBase)
        {

            this.request = requestBase;
        }

        private T parseType<T>(string value) where T:new()
        {

            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(value))
            {

                return (T)converter.ConvertFrom(value);
            
            
            }

            object tx = Convert.ChangeType(value, typeof(T));
            if (tx != null)
                return (T)tx;

            throw new InvalidCastException("Cannot convert value  \"" + value + "\" to type \"" + typeof(T) + "\"");
        
        
        }

        public T[] GetValues<T>(string fieldName) where T:new()
        {
            if (request.Form[fieldName] == null)
                return new T[0];

            string[] values = request.Form.GetValues(fieldName);
            T[] typedValues = new T[values.Length];


            for (int i = 0; i < values.Length; i++)
            {

                typedValues[i] = parseType<T>(values[i]);
            }

            return typedValues;
        }


        public T GetValue<T>(string fieldName) where T:new()
        {

            if (request.Form[fieldName] == null)
                return new T();

            string value = request.Form.Get(fieldName);



            return this.parseType<T>(value);
        
        
        }
    }
}
