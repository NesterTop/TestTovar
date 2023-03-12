using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace TestTovar
{
    public static class Extencion
    {
        public static void OpenNewForm(this Form mform, Form form, bool closeThis)
        {
            form.Show();
            if (closeThis)
            {
                mform.Hide();
            }
        }
    }
}
