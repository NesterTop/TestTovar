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
    public partial class Form1 : Form
    {
        int currentRow = 0;
        string path = @"C:\Users\Andrey\Desktop\Демоэкзамен№1\Сессия 1\Товар_import\";

        DataTable products;
        DataTable productCategory;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void SetData()
        {
            var pdRow = products.Rows[currentRow].ItemArray;
            string picture = pdRow[11].ToString();
            textBox1.Text = pdRow[1].ToString(); //Наименование
            textBox2.Text = pdRow[9].ToString(); //Кол-во
            textBox3.Text = pdRow[2].ToString(); //Ед изм
            textBox4.Text = pdRow[5].ToString(); //Поставщик
            textBox5.Text = pdRow[3].ToString(); //Стоимость
            textBox6.Text = pdRow[10].ToString(); //Описание

            foreach (string item in comboBox1.Items)
            {
                if(item == pdRow[7].ToString())
                {
                    comboBox1.SelectedItem = item;
                }
            }

            try
            {
                pictureBox1.Image = Image.FromFile(path + picture);
                
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(path + "picture.png");
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentRow < products.Rows.Count - 1)
            {
                currentRow++;
                SetData();
                if (textBox1.Text == "Серьги")
                {
                    pictureBox1.Image = Image.FromFile(path + "1.jpg");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentRow > 0)
            {
                currentRow--;
                SetData();
                if (textBox1.Text == "Серьги")
                {
                    pictureBox1.Image = Image.FromFile(path + "1.jpg");
                }
            }
        }

        private void UpdateData()
        {
            using (DataBase db = new DataBase())
            {
                products = db.ExecuteSql(@"select * from product");
                productCategory = db.ExecuteSql("select distinct productname from Product");
            }

            foreach (DataRow dr in productCategory.Rows)
            {
                foreach (var item in dr.ItemArray)
                {
                    comboBox1.Items.Add(item);
                }
            }

            SetData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(DataBase db = new DataBase())
            {
                db.ExecuteSqlNonQuery($"delete from product where productdescription = '{textBox6.Text}'");
            }
            UpdateData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox5.Text.Replace(",", ".");
            var pdRow = products.Rows[currentRow].ItemArray;
            string article = pdRow[0].ToString();
            using (DataBase db = new DataBase())
            {
                db.ExecuteSqlNonQuery($"update product set ProductName = '{textBox1.Text}', ProductCategory = '{comboBox1.SelectedItem}', ProductQuantityInStock = '{textBox2.Text}', ProductUnit = '{textBox3.Text}', ProductProvider = '{textBox4.Text}', ProductCost = {textBox5.Text}, ProductDescription = '{textBox6.Text}' where ProductArticleNumber = '{article}'");
            }
            UpdateData();
        }
    }
}
