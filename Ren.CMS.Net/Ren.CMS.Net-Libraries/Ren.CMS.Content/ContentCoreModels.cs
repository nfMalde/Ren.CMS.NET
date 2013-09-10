namespace Ren.CMS.Models.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class nContentIncludedModel
    {
        #region Properties

        public Ren.CMS.Content.nContent Entry
        {
            get;
            set;
        }

        #endregion Properties
    }

    public class nSliderModel
    {
        #region Properties

        public List<Ren.CMS.Content.nContent> ContentCollection
        {
            get; set;
        }

        public int SliderSize
        {
            get; set;
        }

        #endregion Properties
    }

    public class SocialSharing
    {
        #region Fields

        private int _height = 0;
        private int _width = 0;

        #endregion Fields

        #region Properties

        public string[] AddThisButtons
        {
            get; set;
        }

        public string ContainerClassesCSS
        {
            get; set;
        }

        public string ContainerStyle
        {
            get; set;
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

        #endregion Properties
    }
}