using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
   public class EggSoldVO
    {
        private int _ESid;
        private DateTime _ESdate;
        private int _ESqty;
        private Decimal _ESamt;
        private Decimal _EStotamt;
        private int _ESinstock;
       
        private string _ESnotes;


        public int ESid
        {
            get
            {
                return _ESid;
            }

            set
            {
                _ESid = value;
            }
        }
        public DateTime ESdate
        {
            get
            {
                return _ESdate;
            }

            set
            {
                _ESdate = value;
            }
        }
        public int ESqty
        {
            get
            {
                return _ESqty;
            }

            set
            {
                _ESqty = value;
            }
        }

        public decimal ESamt
        {
            get
            {
                return _ESamt;
            }

            set
            {
                _ESamt = value;
            }
        }

        public decimal EStotamt
        {
            get
            {
                return _EStotamt;
            }

            set
            {
                _EStotamt = value;
            }
        }


        public int ESinstock
        {
            get
            {
                return _ESinstock;
            }

            set
            {
                _ESinstock = value;
            }
        }

        public string ESnotes
        {
            get
            {
                return _ESnotes;
            }

            set
            {
                _ESnotes = value;
            }
        }
    }
}
