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
    /// Interaction logic for AddPatients.xaml
    /// </summary>
    public partial class AddPatients : Window
    {
        ucareEntities1 _db = new ucareEntities1();

        public AddPatients()
        {
            InitializeComponent();
            loadCbInsurance();
        }

        public void loadCbInsurance()
        {
            var query = (from i in _db.insurances
                         select i);
            cbTypeInsurance.ItemsSource = query.ToList();
            cbTypeInsurance.DisplayMemberPath = "name_insurance";
            cbTypeInsurance.SelectedValuePath = "Id_insurance";
        }

        private void btn_add_patient(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string contact = txtContact.Text;
            string codeIdentity = txtCodeIdentity.Text;
            string typeInsurance = cbTypeInsurance.Text;
            string codeInsurance = txtCodeInsurance.Text;
            string address = txtAddress.Text;
            int id_insurance = (int)cbTypeInsurance.SelectedValue;
            //MessageBox.Show(id_insurance+"");

            patient p = new patient()
            {
                name_patient = name,
                contact = contact,
                code_identity = codeIdentity,
                code_insurance = codeInsurance,
                address = address,
                fk_id_insurance = id_insurance
            };

            try
            {
                _db.patients.Add(p);
                _db.SaveChanges();
                Patients.dgrid.ItemsSource = _db.patients.ToList();
                MessageBox.Show("Add Patient Success !");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Add Patient Fail !");
            }

            
        }
    }
}
