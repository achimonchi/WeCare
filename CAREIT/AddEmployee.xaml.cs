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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        ucareEntities1 _db = new ucareEntities1();
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_add_employee(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string code = txtCode.Text;
            string address = txtAddress.Text;
            string contact = txtContact.Text;
            string role = cbRole.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            int id_role =0 ;
            if(role == "Admin")
            {
                id_role = 9;
            }
            else if(role=="Pharmacist")
            {
                id_role = 11;
            }
            else if(role == "Receptioinist")
            {
                id_role = 10;
            }

            employee emp = new employee()
            {
                name_employee = name,
                code_employee = code,
                address = address,
                contact = contact,
                fk_id_role = id_role,
                username = username,
                password = password
            };

            try
            {
                _db.employees.Add(emp);
                _db.SaveChanges();
                Employees.dgrid.ItemsSource = _db.employees.ToList();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Must fill the blank box !");
            }
            
            
        }

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
