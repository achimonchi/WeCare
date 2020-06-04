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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : UserControl
    {
        ucareEntities1 _db = new ucareEntities1();
        public static DataGrid dgrid;
        int Id_patient;
        int id_insurance;
        public Patients()
        {
            InitializeComponent();
            loadData();
            loadCbInsurance();
        }

        public void loadData()
        {
            var query = (from p in _db.patients
                         join i in _db.insurances on p.fk_id_insurance equals i.Id_insurance
                         select p);

            dgridPatients.ItemsSource = query.ToList();
            dgrid = dgridPatients;

        }

        public void loadCbInsurance()
        {
            var query = (from i in _db.insurances
                         select i);
            cbInsurance.ItemsSource = query.ToList();
            cbInsurance.DisplayMemberPath = "name_insurance";
            cbInsurance.SelectedValuePath = "Id_insurance";
        }



        private void DgridPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var patient = (dgridPatients.SelectedItem as patient);
            if(patient != null)
            {
                Id_patient = patient.Id_patient;
                txtName.Text = patient.name_patient;
                txtAddress.Text = patient.address;
                txtCodeIdentity.Text = patient.code_identity;
                txtCodeInsurance.Text = patient.code_insurance;
                txtContact.Text = patient.contact;
                id_insurance = (int) patient.fk_id_insurance;
                
                if(id_insurance == 1)
                {
                    txtInsuranceNow.Text = "BPJS";
                }
                else if(id_insurance == 2)
                {
                    txtInsuranceNow.Text = "AXA";
                }
            }
        }

        private void search_patients(object sender, KeyEventArgs e)
        {
            string key = searchbox.Text;
            var query = from p in _db.patients
                        where (p.name_patient.Contains(key)
                        || p.address.Contains(key) || p.code_identity.Contains(key)
                        )
                        select p;
            dgridPatients.ItemsSource = query.ToList();
        }

        private void btn_add_patient(object sender, RoutedEventArgs e)
        {
            AddPatients ap = new AddPatients();
            ap.Show();
        }

        private void btn_delete_patient(object sender, RoutedEventArgs e)
        {
            try
            {
                var hapusPatient = _db.patients.Where(p => p.Id_patient == Id_patient).Single();
                _db.patients.Remove(hapusPatient);
                _db.SaveChanges();
                MessageBox.Show("Delete Data " + txtName.Text.Trim() + " success !");
                txtName.Text = "";
                txtAddress.Text = "";
                txtCodeIdentity.Text = "";
                txtCodeInsurance.Text = "";
                txtContact.Text = "";
                txtInsuranceNow.Text = "";
                dgridPatients.ItemsSource = _db.patients.ToList();
            }
            catch
            {
                MessageBox.Show("Delete Data Fail !");
            }
        }

        private void btn_edit_patient(object sender, RoutedEventArgs e)
        {


            //MessageBox.Show(id_insurance + "");
            
            try
            {
                string name = txtName.Text.Trim();
                string contact = txtContact.Text.Trim();
                string codeIdentity = txtCodeIdentity.Text.Trim();
                string codeInsurance = txtCodeInsurance.Text.Trim();
                string address = txtAddress.Text.Trim();
                int idInsurance;
                if (cbInsurance.Text != "")
                {
                    idInsurance = (int)cbInsurance.SelectedValue;
                }
                else
                {
                    idInsurance = id_insurance;
                }

                //MessageBox.Show(idInsurance+"");
                

                patient updatePatient = (from p in _db.patients
                                         where p.Id_patient == Id_patient
                                         select p).Single();
                updatePatient.address = address;
                updatePatient.code_identity = codeIdentity;
                updatePatient.code_insurance = codeInsurance;
                updatePatient.contact = contact;
                updatePatient.fk_id_insurance = idInsurance;
                updatePatient.name_patient = name;

                _db.SaveChanges();
                dgridPatients.ItemsSource = _db.patients.ToList();
                txtInsuranceNow.Text = cbInsurance.Text.Trim();
                
            }
            catch
            {
                MessageBox.Show("Edit Data Fail !");
            }
        }

        private void btn_add_queue(object sender, RoutedEventArgs e)
        {
            

            try
            {
                //MessageBox.Show(Id_patient+"");

                DateTime timestamp = DateTime.Now.ToLocalTime();

                //string format = "yyyy-MM-dd HH:mm:ss";
                //MessageBox.Show(timestamp + "");

                queue q = new queue()
                {
                    fk_id_patient = Id_patient,
                    time_queue = timestamp,
                    status_queue = "in queue",
                    type_queue = 2
                };

                _db.queues.Add(q);
                _db.SaveChanges();
                MessageBox.Show("Patient now go to queue list !");

                MainWindow.g.Children.Clear();
                MainWindow.g.Children.Add(new QueueList());


            }
            catch
            {
                MessageBox.Show("Add to queue Fail !");

            }
        }
    }
}
