using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTovar
{
    public partial class FormAuth : Form
    {
        int count = 0;

        public FormAuth()
        {
            InitializeComponent();
        }

        private void FormAuth_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckCountClick();

            string login = textBox1.Text;
            string password = textBox2.Text;

            using (DataBase db = new DataBase())
            {
                DataTable users = db.ExecuteSql($"select * from Users where UserLogin = '{login}' and UserPassword = '{password}'");

                if (users.Rows.Count > 0)
                {
                    this.OpenNewForm(new Form1(), true);
                }
                else MessageBox.Show("Пользователя не существует");
            }    
        }

        private void CheckCountClick()
        {
            if (count >= 3)
            {
                timer1.Start();
                button1.Enabled = false;
            }
            count++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        
    }
}
