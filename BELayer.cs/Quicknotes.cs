using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOLayer.cs
{
   public class Quicknotes
    {

        private int _notid;
        private DateTime _notdate;

        private string _notdes;
       
        public int Notid
        {
            get
            {
                return _notid;
            }

            set
            {
                _notid = value;
            }
        }
        public DateTime Notdate
        {
            get
            {
                return _notdate;
            }

            set
            {
                _notdate = value;
            }
        }
        public string Notdes
        {
            get
            {
                return _notdes;
            }

            set
            {
                _notdes = value;
            }
        }


       
    }
}
