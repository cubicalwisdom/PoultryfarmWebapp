using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolutryFarm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
         {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

             if (File.Exists("sysLL.txt"))
             {
                 Application.Run(new Main());
            }
             else
             {
                Application.Run(new Login());
            }
          
        }
    }
}
