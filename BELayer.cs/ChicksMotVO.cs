using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
    public class ChicksMotVO
    {

        private int _CMid;
        private DateTime _CMdate;
        private string _CMbatchno;
        private int _CMqty;
        private int _CMBalqty;
        private int _CMaid;
        private string _CMnotes;


        public int CMid
        {
            get
            {
                return _CMid;
            }

            set
            {
                _CMid = value;
            }
        }
        public DateTime CMdate
        {
            get
            {
                return _CMdate;
            }

            set
            {
                _CMdate = value;
            }
        }

      

        public string CMbatchno
        {
            get
            {
                return _CMbatchno;
            }

            set
            {
                _CMbatchno = value;
            }
        }
        public int CMqty
        {
            get
            {
                return _CMqty;
            }

            set
            {
                _CMqty = value;
            }
        }
        public int CMBalqty
        {
            get
            {
                return _CMBalqty;
            }

            set
            {
                _CMBalqty = value;
            }
        }

        public int CMaid
        {
            get
            {
                return _CMaid;
            }

            set
            {
                _CMaid = value;
            }
        }
        public string CMnotes
        {
            get
            {
                return _CMnotes;
            }

            set
            {
                _CMnotes = value;
            }
        }
    }
}
