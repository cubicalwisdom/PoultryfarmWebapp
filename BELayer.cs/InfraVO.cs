using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class InfraVO
    {

        private int _Iid;
        private DateTime _Idate;
        private string _Iexpensetype;
        private decimal _Iamount;
        private string _Inotes;


        public int Iid
        {
            get
            {
                return _Iid;
            }

            set
            {
                _Iid = value;
            }
        }
        public DateTime Idate
        {
            get
            {
                return _Idate;
            }

            set
            {
                _Idate = value;
            }
        }
        public string Iexpensetype
        {
            get
            {
                return _Iexpensetype;
            }

            set
            {
                _Iexpensetype = value;
            }
        }

        public decimal Iamount
        {
            get
            {
                return _Iamount;
            }

            set
            {
                _Iamount = value;
            }
        }



        public string Inotes
        {
            get
            {
                return _Inotes;
            }

            set
            {
                _Inotes = value;
            }
        }
    }
}
