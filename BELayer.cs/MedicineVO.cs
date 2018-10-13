using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class MedicineVO
    {

        private int _Mid;
        private DateTime _Mdate;
        private string _Mpurno;
        private string _Mbrand;
        private string _Msupplier;
        private string _Mnature;
        private int _Mqty;
        private decimal _Mamt;
        private string _Mnotes;


        public int Mid
        {
            get
            {
                return _Mid;
            }

            set
            {
                _Mid = value;
            }
        }
        public DateTime Mdate
        {
            get
            {
                return _Mdate;
            }

            set
            {
                _Mdate = value;
            }
        }



        public string Mpurno
        {
            get { return _Mpurno; }

            set { _Mpurno = value; }
        }
        public string Mbrand
        {
            get { return _Mbrand; }

            set { _Mbrand = value; }
        }

        public string Msupplier
        {
            get { return _Msupplier; }

            set { _Msupplier = value; }
        }

        public string Mnature
        {
            get { return _Mnature; }

            set { _Mnature = value; }
        }
        public int Mqty
        {
            get
            {
                return _Mqty;
            }

            set
            {
                _Mqty = value;
            }
        }

        public decimal Mamt
        {
            get
            {
                return _Mamt;
            }

            set
            {
                _Mamt = value;
            }
        }

        public string Mnotes
        {
            get { return _Mnotes; }

            set { _Mnotes = value; }
        }
    }
}
