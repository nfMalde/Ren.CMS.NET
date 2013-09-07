using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Models.Core
{
    public class nSliderModel
    {

        public List<Ren.CMS.Content.nContent> ContentCollection { get; set; }
        public int SliderSize { get; set; }





    }
    public class SocialSharing
    {
        private int _width = 0;
        private int _height = 0;

        public string[] AddThisButtons { get; set; }
        public int width
        {
            get
            {

                return this._width;


            }
            set
            {

                this._width = value;

            }
        }
        public int height
        {
            get
            {

                return this._height;


            }
            set
            {

                this._height = value;

            }
        }
        public string ContainerClassesCSS { get; set; }
        public string ContainerStyle { get; set; }

    }
    public class nContentIncludedModel
    {


        public Ren.CMS.Content.nContent Entry
        {

            get;
            set;

        }



    }
}
