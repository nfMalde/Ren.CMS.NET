using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Helper.LinkHelper
{

    public class LinkHelper
    {


        public string generateUniqueURL(Ren.CMS.Content.nContent nContentE)
        {

            string url = "";



            url = "/Link/UniqueLink/" + nContentE.ID;





            return url;
        }

        public string generateUniqueURL(string url, char delimiter, int uniqueIDindex, bool stripURL = true)
        {


            try
            {

                if (stripURL == true)
                {


                    url = url.Remove(0, url.LastIndexOf('/') + 1);






                }
                string[] buffer = url.Split(delimiter);

                url = "/Link/UniqueLink/" + buffer[uniqueIDindex];


            }
            catch (Exception e)
            {


                url = "ERROR for PARSING URL:       " + e.Message;


            }


            return url;
        }



    }






}
