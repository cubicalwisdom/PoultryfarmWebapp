using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class EggVO
    {

        private int _Eid;
        private DateTime _Edate;
        private string _Ecbatchno;
        private int _Eqty;
        private int _Einstock;    
        private string _Enotes;


        public int Eid
        {
            get
            {
                return _Eid;
            }

            set
            {
                _Eid = value;
            }
        }
        public DateTime Edate
        {
            get
            {
                return _Edate;
            }

            set
            {
                _Edate = value;
            }
        }
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

        public int Einstock
        {
            get
            {
                return _Einstock;
            }

            set
            {
                _Einstock = value;
            }
        }

    

        public string Enotes
        {
            get
            {
                return _Enotes;
            }

            set
            {
                _Enotes = value;
            }
        }
    }
}
 