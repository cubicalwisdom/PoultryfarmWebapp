using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class ChicksVO
    {

        private int _Cid;
        private DateTime _Cdate;
        private string _Cbatchno;
        private string _Cbreed;
        private int _Cqty;
        private decimal _Ctotalwt;
    

        private decimal _Cavgwt;
        private string _Cwmea;
        private decimal _Ctotalamt;
        private decimal _Cavgamt;
        private DateTime _Cbate;
       
        private int _Caid;
        private string _Cnotes;


        public int Cid
        {
            get
            {
                return _Cid;
            }

            set
            {
                _Cid = value;
            }
        }
        public DateTime Cdate
        {
            get
            {
                return _Cdate;
            }

            set
            {
                _Cdate = value;
            }
        }
        public string Cbatchno
        {
            get
            {
                return _Cbatchno;
            }

            set
            {
                _Cbatchno = value;
            }
        }
        public string Cbreed
        {
            get
            {
                return _Cbreed;
            }

            set
            {
                _Cbreed = value;
            }
        }
        public int Cqty
        {
            get
            {
                return _Cqty;
            }

            set
            {
                _Cqty = value;
            }
        }

        public  decimal Ctotalwt
        {
            get
            {
                return _Ctotalwt;
            }

            set
            {
                _Ctotalwt = value;
            }
        }

        
        public decimal Cavgwt
        {
            get
            {
                return _Cavgwt;
            }

            set
            {
                _Cavgwt = value;
            }
        }
        public string Cwmea
        {
            get
            {
                return _Cwmea;

            }
            set
            {
                _Cwmea = value;
            }

        }
        public decimal Ctotalamt
        {
            get
            {
                return _Ctotalamt;
            }

            set
            {
                _Ctotalamt = value;
            }
        }
        public decimal Cavgamt
        {
            get
            {
                return _Cavgamt;
            }

            set
            {
                _Cavgamt = value;
            }
        }

        public DateTime Cbate
        {
            get
            {
                return _Cbate;
            }

            set
            {
                _Cbate = value;
            }
        }

        public int Caid
        {
            get
            {
                return _Caid;
            }

            set
            {
                _Caid = value;
            }
        }
        public string Cnotes
        {
            get
            {
                return _Cnotes;
            }

            set
            {
                _Cnotes = value;
            }
        }
    }
}
