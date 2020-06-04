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
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : UserControl
    {
        public static DataGrid dgrid;
        ucareEntities1 _db = new ucareEntities1();
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=ucare;Integrated Security=True");

        public Transactions()
        {
            InitializeComponent();
            loadData();
        }

        void loadData()
        {
            conn.Open();
            string query = "SELECT * FROM transactions JOIN medicines ON transactions.fk_id_medicine =  medicines.Id_medicine " +
                "JOIN employees ON employees.Id_employee = transactions.fk_id_employee";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dgridTransactions.ItemsSource = dt.DefaultView;
            dgrid = dgridTransactions;

        }
    }
}
