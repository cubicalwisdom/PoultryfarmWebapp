using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class MedicineAppVO
    {

        private int _MAid;
        private DateTime _MAsdate;
        private DateTime __MAedate;
        private string _MApurno;
        private string _MAbrand;
        private string _MAcbatchno;
        private string _MAnotes;


        public int MAid
        {
            get
            {
                return _MAid;
            }

            set
            {
                _MAid = value;
            }
        }
        public DateTime MAsdate
        {
            get
            {
                return _MAsdate;
            }

            set
            {
                _MAsdate = value;
            }
        }
        public DateTime MAedate
        {
            get
            {
                return __MAedate;
            }

            set
            {
                __MAedate = value;
            }
        }


        public string MApurno
        {
            get { return _MApurno; }

            set { _MApurno = value; }
        }

        public string MAbrand
        {
            get { return _MAbrand; }

            set { _MAbrand = value; }
        }


        public string MAcbatchno
        {
            get
            {
                return _MAcbatchno;
            }

            set
            {
                _MAcbatchno = value;
            }
        }

        public string MAnotes
        {
            get
            {
                return _MAnotes;
            }

            set
            {
                _MAnotes = value;
            }
        }

    }
}
