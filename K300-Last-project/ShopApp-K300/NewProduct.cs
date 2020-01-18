using ShopApp_K300.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApp_K300
{
    public partial class NewProduct : Form
    {
        ShopDB db;
        public NewProduct()
        {
            db = new ShopDB();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string productName = txtProduct.Text;
            string price = txtPrice.Text;
            string Amount = txtAmount.Text;
            string Category = cmbCategory.Text;
            string Size = cmbSize.Text;
            string Desc = rcDescription.Text;
            string[] emp ={productName, price,Amount,Category,Size,};

            if (mainExtensions.IsEmpty(emp, string.Empty))
            {

                int CatId = db.Categories.First(ct => ct.Name == Category).Id;
                int SizeId = db.ProductSizes.First(ct => ct.Size == Size).Id;

                db.Products.Add(new Product()
                {
                    ProductName = productName,
                    Price=Convert.ToDouble(price),
                    Amount=Convert.ToInt32(Amount),
                    CategoryId=CatId,
                    SizeId=SizeId,
                });
                db.SaveChanges();
                MessageBox.Show("Product create successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

            }
            NewProduct np = new NewProduct();
            np.ShowDialog();
        }
        
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar<47 || e.KeyChar > 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void NewProduct_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.Add("Selected...");
            cmbCategory.SelectedIndex = 0;
            cmbCategory.Items.AddRange(db.Categories.Select(ct => ct.Name).ToArray());
            cmbSize.Items.AddRange(db.ProductSizes.Select(ct => ct.Size).ToArray());
        }



        private void cmbCategory_DropDown(object sender, EventArgs e)
        {
            cmbCategory.Items.Remove("Selected...");
        }
    }
}
