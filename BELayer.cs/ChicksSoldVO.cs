using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class ChicksSoldVO
    {
        private int _CSid;
        private DateTime _CSdate;
        private string _CSbatchno;
        private int _CSqty;
        private int _CSBalqty;
        private decimal _CStotalwt;
        private decimal _CSavgwt;
        private string _CSwmea;
        private decimal _CStotalamt;
        private decimal _CSavgamt;
        private string _CSnotes;


        public int CSid
        {
            get
            {
                return _CSid;
            }

            set
            {
                _CSid = value;
            }
        }
        public DateTime CSdate
        {
            get
            {
                return _CSdate;
            }

            set
            {
                _CSdate = value;
            }
        }
        public string CSbatchno
        {
            get
            {
                return _CSbatchno;
            }

            set
            {
                _CSbatchno = value;
            }
        }
        public int CSqty
        {
            get
            {
                return _CSqty;
            }

            set
            {
                _CSqty = value;
            }
        }
       
        public int CSBalqty
        {
            get
            {
                return _CSBalqty;
            }

            set
            {
                _CSBalqty = value;
            }
        }
      

        public decimal CStotalwt
        {
            get
            {
                return _CStotalwt;
            }

            set
            {
                _CStotalwt = value;
            }
        }


        public decimal CSavgwt
        {
            get
            {
                return _CSavgwt;
            }

            set
            {
                _CSavgwt = value;
            }
        }
        public string CSwmea
        {
            get
            {
                return _CSwmea;

            }
            set
            {
                _CSwmea = value;
            }

        }
        public decimal CStotalamt
        {
            get
            {
                return _CStotalamt;
            }

            set
            {
                _CStotalamt = value;
            }
        }
        public decimal CSavgamt
        {
            get
            {
                return _CSavgamt;
            }

            set
            {
                _CSavgamt = value;
            }
        }

  
        public string CSnotes
        {
            get
            {
                return _CSnotes;
            }

            set
            {
                _CSnotes = value;
            }
        }
    }
}
