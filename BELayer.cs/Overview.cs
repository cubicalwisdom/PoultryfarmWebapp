using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class Overview
    {
        private string _Ecbatchno;
        private int _Eqty;

        public string Ecbatchno
        {
            get
            {
                return _Ecbatchno;
            }

            set
            {
                _Ecbatchno = value;
            }
        }
        public int Eqty
        {
            get
            {
                return _Eqty;
            }

            set
            {
                _Eqty = value;
            }
        }

    }
}
