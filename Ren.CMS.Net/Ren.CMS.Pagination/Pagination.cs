using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Pagination
{
    public class nPagingCollection : IEnumerable
    {
        private int mpages = 0;
        public int MaxPages { get { return this.mpages; } }
        public string getDebugInfo
        {

            get
            {

                Ren.CMS.CORE.Extras.Extras EX = new Ren.CMS.CORE.Extras.Extras();
                return EX.var_dump(this, 10);

            }



        }

        private List<nPage> pCol = new List<nPage>();
        public nPagingCollection(int totalRows, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = totalRows;

            }
            decimal i = Convert.ToDecimal(totalRows);
            decimal xi = Convert.ToDecimal(pageSize);
            decimal y = i / xi;
            y = Math.Ceiling(y);
            int z = Convert.ToInt32(y);



            int pages = z;
            this.mpages = pages;

            for (int x = 0; x < pages; x++)
            {

                nPage P = new nPage((x + 1), pageSize);
                pCol.Add(P);


            }




        }

        public int Count { get { return this.pCol.Count; } }
        public IEnumerator<nPage> GetEnumerator()
        {
            return this.pCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class nPage
    {

        private int ppage = 0;
        private int prowstart = 0;
        private int prowend = 0;


        public nPage(int page, int pagesize)
        {
            int pageStart = page * pagesize + 1;
            pageStart = pageStart - pagesize;
            int pageEnd = page * pagesize;
            this.ppage = page;
            this.prowstart = pageStart;
            this.prowend = pageEnd;
        }



        public int Index
        {

            get { return this.ppage; }
            set { this.ppage = value; }

        }


        public int RowStart
        {

            get { return this.prowstart; }
            set { this.prowstart = value; }

        }


        public int RowEnd
        {


            get { return this.prowend; }
            set { this.prowend = value; }

        }


    }





}
