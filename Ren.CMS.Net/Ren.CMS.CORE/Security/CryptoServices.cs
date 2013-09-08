namespace Ren.CMS.CORE.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CryptoServices
    {
        #region Methods

        /// <summary>
        /// Converts a string into a MD5 Hash
        /// </summary>
        /// <param name="stringToConvert">The String that has to be converted</param>
        /// <returns>Encrypted MD5 String</returns>
        public string ConvertToMD5(string stringToConvert)
        {
            //Umwandlung des Eingastring in den MD5 Hash
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(stringToConvert);
            byte[] result = md5.ComputeHash(textToHash);

            //MD5 Hash in String konvertieren
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }

        /// <summary>
        /// Converts a string into a SHA1 Hash
        /// </summary>
        /// <param name="stringToConvert">The String that has to be converted</param>
        /// <returns>Encrypted SHA1 String</returns>
        public string ConvertToSHA1(string stringToConvert)
        {
            //Umwandlung des Eingastring in den SHA1 Hash
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(stringToConvert);
            byte[] result = sha1.ComputeHash(textToHash);

            //SHA1 Hash in String konvertieren
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in result)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }

        #endregion Methods
    }
}