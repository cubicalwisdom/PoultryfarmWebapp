using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class FeedVO
    {

        private int _Fid;
        private DateTime _Fdate; 
        private string _Fpurno;
        private string _Fbrand;
        private int _Fnob;
        private decimal _Fwt;
        private decimal _Fwav;
        private string _Fwmea;   
        private decimal _Ftotamt;
        private decimal _Favgamt;
        private string _Fnotes;


        public int Fid
        {
            get
            {
                return _Fid;
            }

            set
            {
                _Fid = value;
            }
        }
        public DateTime Fdate
        {
            get
            {
                return _Fdate;
            }

            set
            {
                _Fdate = value;
            }
        }

      

        public string Fpurno
        {
            get { return _Fpurno; }

            set { _Fpurno = value; }
        }

        public string Fbrand
        {
            get { return _Fbrand; }

            set { _Fbrand = value; }
        }
        public int Fnob
        {
            get
            {
                return _Fnob;
            }

            set
            {
                _Fnob = value;
            }
        }
        public decimal Fwt
        {
            get
            {
                return _Fwt;
            }

            set
            {
                _Fwt = value;
            }
        }
        public decimal Fwav
        {
            get
            {
                return _Fwav;
            }

            set
            {
                _Fwav = value;
            }
        }
        public string Fwmea
        {
            get
            {
                return _Fwmea;
            }

            set
            {
                _Fwmea = value;
            }
        }
        
        public decimal Ftotamt
        {
            get
            {
                return _Ftotamt;
            }

            set
            {
                _Ftotamt = value;
            }
        }
        public decimal Favgamt
        {
            get
            {
                return _Favgamt;
            }

            set
            {
                _Favgamt = value;
            }
        }
        public string Fnotes
        {
            get
            {
                return _Fnotes;
            }

            set
            {
                _Fnotes = value;
            }
        }

    }
}
