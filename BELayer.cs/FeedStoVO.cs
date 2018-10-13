using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class FeedStoVO
    {

        private int _FSid;
        private DateTime _FSsdate;
        private DateTime _FSedate;
        private string _FSfpurno;
        private string _FBrand;
        private int _FSfbn;
        private int _FSbalc;
        private string _FScbn;
        private string _FSnotes;


        public int FSid
        {
            get
            {
                return _FSid;
            }

            set
            {
                _FSid = value;
            }
        }
        public DateTime FSsdate
        {
            get
            {
                return _FSsdate;
            }

            set
            {
                _FSsdate = value;
            }
        }
        public DateTime FSedate
        {
            get
            {
                return _FSedate;
            }

            set
            {
                _FSedate = value;
            }
        }


       
        public string FSfpurno
        {
            get
            {
                return _FSfpurno;
            }

            set
            {
                _FSfpurno = value;
            }
        }
        public string FBrand
        {
            get
            {
                return _FBrand;
            }

            set
            {
                _FBrand = value;
            }
        }

        public int FSfbn
        {
            get
            {
                return _FSfbn;
            }

            set
            {
                _FSfbn = value;
            }
        }

    
        public int FSbalc
        {
            get
            {
                return _FSbalc;
            }

            set
            {
                _FSbalc = value;
            }
        }
        public string FScbn
        {
            get
            {
                return _FScbn;
            }

            set
            {
                _FScbn = value;
            }
        }

        public string FSnotes
        {
            get
            {
                return _FSnotes;
            }

            set
            {
                _FSnotes = value;
            }
        }

    }
}
