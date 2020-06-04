using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for BuyMedicine.xaml
    /// </summary>
    public partial class BuyMedicine : Window
    {
        ucareEntities1 _db = new ucareEntities1();
        int price;
        int Id_employee;
        int Id_medicine;
        public BuyMedicine(int Id_medicine, int Id_employee)
        {
            InitializeComponent();
            this.Id_employee = Id_employee;
            this.Id_medicine = Id_medicine;

            var query = (from m in _db.medicines
                         where m.Id_medicine == Id_medicine
                         select m).Single();
            price = int.Parse(query.price.ToString());
            txtCost.Text = price.ToString();
            txtName.Text = query.name_medicine.ToString().Trim();
            loadCat(int.Parse(query.fk_id_category.ToString()));


        }

        void loadCat(int Id_cat)
        {
            var query = (from c in _db.categories
                         where c.Id_category == Id_cat
                         select c).Single();
            txtCat.Text = query.name_category.ToString().Trim();
        }


        int getStock(int Id_medicine, int qtMedicine)
        {
            var query = (from m in _db.medicines
                         where m.Id_medicine == Id_medicine
                         select m).Single();
            int leftStock = int.Parse(query.stok.ToString()) - qtMedicine;
            return leftStock;
        }

        private void btn_buy(object sender, RoutedEventArgs e)
        {
            try
            {
                if(getStock(this.Id_medicine, int.Parse(txtAmounts.Text.ToString())) >= 0)
                {
                    DateTime timestamp = DateTime.Now.ToLocalTime();

                    transaction t = new transaction()
                    {
                        fk_id_employee = this.Id_employee,
                        fk_id_medicine = this.Id_medicine,
                        qty_medicine = int.Parse(txtAmounts.Text.ToString()),
                        amount = int.Parse(txtTotal.Text.ToString()),
                        time = timestamp
                    };

                    _db.transactions.Add(t);
                    _db.SaveChanges();


                    medicine med = (from m in _db.medicines
                                  where m.Id_medicine == this.Id_medicine
                                  select m).Single();
                    med.stok = getStock(this.Id_medicine, int.Parse(txtAmounts.Text.ToString()));
                    _db.SaveChanges();
                    
                    MessageBox.Show("Buy Medicine Success !");
                    this.Close();
                    MainWindow.g.Children.Clear();
                    MainWindow.g.Children.Add(new Transactions());
                    

                }
                else
                {
                    MessageBox.Show("Minimum Stock !");
                }
                
            }
            catch
            {
                MessageBox.Show("Buy Medicine Failed !");
            }


        }

        private void check_total(object sender, KeyEventArgs e)
        {
            int total=0;
            int price = this.price;
            try
            {
                if (txtAmounts.Text.ToString() == "")
                {
                    txtTotal.Text = "0";
                }
                else
                {
                    total = int.Parse(txtAmounts.Text.ToString());
                    total = total * price;
                    txtTotal.Text = total + "" ;
                }
                
                
            }
            catch
            {
                MessageBox.Show("Amounts Field must be numeric !");

            }

        }
    }
}
