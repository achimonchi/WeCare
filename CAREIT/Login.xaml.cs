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
using System.Windows.Shapes;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ucareEntities1 _db = new ucareEntities1();
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=ucare;Integrated Security=True");

        public Login()
        {
            InitializeComponent();
        }

        private void btn_login(object sender, RoutedEventArgs e)
        {
            conn.Open();
            string uname = txtUsername.Text.Trim();
            string pass = txtPassword.Password.Trim();

            string query = "SELECT * FROM employees WHERE username ='"+uname+"' AND password = '"+pass+"' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable();
            sda.Fill(dt);

            

            if (dt.Rows.Count == 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int Id = int.Parse(dt.Rows[i]["Id_employee"].ToString());
                    if (int.Parse(dt.Rows[i]["fk_id_role"].ToString()) == 9)
                    {
                        MainWindow m = new MainWindow(Id);
                        m.Show();
                        this.Close();
                    }
                    else if (int.Parse(dt.Rows[i]["fk_id_role"].ToString()) == 10)
                    {
                        Receptionist r = new Receptionist(Id);
                        r.Show();
                        this.Close();
                    }
                    else if (int.Parse(dt.Rows[i]["fk_id_role"].ToString()) == 11)
                    {
                        Pharmacist p = new Pharmacist(Id);
                        p.Show();
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Login Fail !");
            }
            conn.Close();
        }

        private void btn_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
