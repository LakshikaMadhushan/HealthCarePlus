using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCarePlus
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
            //Application.Run(new Registration());
            //Application.Run(new Patient());
            //Application.Run(new Appointment());
            //Application.Run(new Staff());
            //Application.Run(new Theater());
            //Application.Run(new Schedule());
            //Application.Run(new Report());           
            //Application.Run(new DashBoard());
            //Application.Run(new PatientPop());
        }
    }
}
