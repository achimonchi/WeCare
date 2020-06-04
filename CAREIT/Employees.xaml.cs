using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        ucareEntities1 _db = new ucareEntities1();
        public static DataGrid dgrid;

        string pass = "";
        int Id = 0;
        int Id_role = 0;

        public Employees()
        {
            InitializeComponent();
            this.fillComboBox();
            LoadData();
        }

        public void LoadData()
        {
            var query = (from p in _db.employees
                         join r in _db.roles on p.fk_id_role equals r.Id_role
                         select p);
            dgridEmployees.ItemsSource = query.ToList();
            dgrid = dgridEmployees;
        }

        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            AddEmployee add = new AddEmployee();
            add.Show();
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            try
            {
                var hapusEmp = _db.employees.Where(emp => emp.Id_employee == Id).Single();
                _db.employees.Remove(hapusEmp);
                _db.SaveChanges();
                MessageBox.Show("Delete Data " + txtName.Text + " Success !");
                txtAddress.Text = "";
                txtCode.Text = "";
                txtContact.Text = "";
                txtName.Text = "";
                txtPassword.Password = "";
                txtUsername.Text = "";
                dgridEmployees.ItemsSource = _db.employees.ToList();
            }
            catch
            {
                MessageBox.Show("Delete Data Fail !");
            }
            
        }

        private void search_employee(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string keyword = searchbox.Text;

            var query = from emp in _db.employees
                        where emp.name_employee.Contains(keyword)
                        select emp;
            dgridEmployees.ItemsSource = query.ToList();
            dgrid = dgridEmployees;

        }

        private void DgridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var emp = (dgridEmployees.SelectedItem as employee);

            if(emp != null)
            {
                Id = emp.Id_employee;
                txtName.Text = emp.name_employee.Trim();
                txtContact.Text = emp.contact.Trim();
                txtAddress.Text = emp.address.Trim();
                txtCode.Text = emp.code_employee.Trim();
                txtUsername.Text = emp.username.Trim();
                pass = emp.password.Trim();
                Id_role = (int)emp.fk_id_role;
            }
            
        }

        public void fillComboBox()
        {
            var query = (from r in _db.roles
                         select r);
            cbRole.ItemsSource = query.ToList();
            cbRole.DisplayMemberPath = "name_role";
            cbRole.SelectedValue = "Id_role";
        }

        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            
            string name = txtName.Text;
            string contact = txtContact.Text;
            string address = txtAddress.Text;
            string code = txtCode.Text;
            string role = cbRole.Text.Trim();
            string password = "";
            int id_role = Id_role;

            if(role == "admin")
            {
                id_role = 9; 
            }
            else if(role == "receptionist")
            {
                id_role = 10;
            }
            else if(role == "pharmacist")
            {
                id_role = 11;
            }
            
            if(txtPassword.Password=="")
            {
                password = pass;
            }
            else
            {
                password = txtPassword.Password.Trim();
            }

            try
            {
                employee updateEmployee = (from emp in _db.employees
                                           where emp.Id_employee == Id
                                           select emp).Single();
                updateEmployee.name_employee = name;
                updateEmployee.address = address;
                updateEmployee.code_employee = code;
                updateEmployee.contact = contact;
                updateEmployee.password = password;
                updateEmployee.fk_id_role = id_role;

                _db.SaveChanges();
                dgrid.ItemsSource = _db.employees.ToList();
            }
            catch
            {
                MessageBox.Show("Edit Data Fail !");
            }

            


        }
    }
}
