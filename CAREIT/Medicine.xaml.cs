using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for Medicine.xaml
    /// </summary>
    public partial class Medicine : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=ucare;Integrated Security=True");
        ucareEntities1 _db = new ucareEntities1();
        public static DataGrid dgrid;
        public int Id_medicine;
        public int Id_category;
        int Id_emp;
        public Medicine(int Id_emp)
        {
            InitializeComponent();
            loadData();
            loadCbCategory();
            this.Id_emp = Id_emp;
        }

        public void loadData()
        {
            var query = (from m in _db.medicines
                         join c in _db.categories on m.fk_id_category equals c.Id_category
                         select m);

            dgridMedicines.ItemsSource = query.ToList();
            dgrid = dgridMedicines;
        }

        void loadCbCategory()
        {
            var query = (from c in _db.categories
                         select c);
            cbCategory.ItemsSource = query.ToList();
            cbCategory.DisplayMemberPath = "name_category";
            cbCategory.SelectedValuePath = "Id_category";
        }

        void loadCategory(int id)
        {
            var query = (from c in _db.categories
                         where c.Id_category == id
                         select c).Single();
            txtCategory.Text = query.name_category;

        }

        private void DgridMedicines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var medicine = (dgridMedicines.SelectedItem as medicine);
            if(medicine != null)
            {
                Id_medicine = medicine.Id_medicine;
                Id_category = (int)medicine.fk_id_category;
                txtName.Text = medicine.name_medicine.Trim();
                txtPrice.Text = medicine.price.ToString();
                txtStock.Text = medicine.stok.ToString();
                loadCategory(Id_category);
                
                
            }
        }

        private void btn_edit_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCat = Id_category;
                if (cbCategory.Text != "")
                {
                    idCat = int.Parse(cbCategory.SelectedValue.ToString());
                }
                else
                {
                    idCat = Id_category;
                }

                medicine updateMed = (from m in _db.medicines
                                      where m.Id_medicine == Id_medicine
                                      select m).Single();
                updateMed.fk_id_category = idCat;
                updateMed.name_medicine = txtName.Text;
                updateMed.price = int.Parse(txtPrice.Text);
                updateMed.stok = int.Parse(txtStock.Text);

                _db.SaveChanges();
                dgridMedicines.ItemsSource = _db.medicines.ToList();
                loadCategory(idCat);
                txtCategory.Text = cbCategory.Text.Trim();
                MessageBox.Show("Edit data "+txtName.Text+" suceess !");
            }
            catch
            {
                MessageBox.Show("Edit data Fail !");
            }
        }

        private void btn_add_click(object sender, RoutedEventArgs e)
        {
            AddMedicine add = new AddMedicine();
            add.Show();
        }

        private void btn_delete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteMed = (from m in _db.medicines
                                 where m.Id_medicine == Id_medicine
                                 select m).Single();
                _db.medicines.Remove(deleteMed);
                _db.SaveChanges();
                MessageBox.Show("Delete data " + txtName.Text.Trim() + " Success !");
                txtName.Text = "";
                txtCategory.Text = "";
                txtPrice.Text = "";
                txtStock.Text = "";

                dgridMedicines.ItemsSource = _db.medicines.ToList();
            }
            catch
            {
                MessageBox.Show("Delete Data Fail !");
            }
        }

        private void search_medicine(object sender, KeyEventArgs e)
        {
            string key = searchbox.Text.Trim();

            var query = from m in _db.medicines
                        where (m.name_medicine.Contains(key))
                        select m;
            dgridMedicines.ItemsSource = query.ToList();
            dgrid = dgridMedicines;
        }

        private void btn_buy_click(object sender, RoutedEventArgs e)
        {
            BuyMedicine bm = new BuyMedicine(Id_medicine, Id_emp);
            bm.Show();

        }
    }
}
