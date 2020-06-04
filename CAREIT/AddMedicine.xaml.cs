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
    /// Interaction logic for AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Window
    {
        ucareEntities1 _db = new ucareEntities1();

        public AddMedicine()
        {
            InitializeComponent();
            loadCategory();
        }

        void loadCategory()
        {
            var category = (from c in _db.categories
                            select c);
            cbCategory.ItemsSource = category.ToList();
            cbCategory.DisplayMemberPath = "name_category";
            cbCategory.SelectedValuePath = "Id_category";
            
        }

        private void btn_add_employee(object sender, RoutedEventArgs e)
        {
            try
            {
                medicine m = new medicine()
                {
                    name_medicine = txtName.Text.Trim(),
                    price = int.Parse(txtPrice.Text),
                    stok = int.Parse(txtStock.Text),
                    fk_id_category = int.Parse(cbCategory.SelectedValue.ToString())
                };

                _db.medicines.Add(m);
                _db.SaveChanges();
                Medicine.dgrid.ItemsSource = _db.medicines.ToList();
                MessageBox.Show("Add data "+txtName.Text.Trim()+" success !");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Add data fail !");
            }

            
        }
    }
}
