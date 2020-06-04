using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for QueueList.xaml
    /// </summary>
    public partial class QueueList : UserControl
    {
        ucareEntities1 _db = new ucareEntities1();
        public static DataGrid dgrid;
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=ucare;Integrated Security=True");


        public QueueList()
        {
            InitializeComponent();
            LoadData();
            //DateTime timestamp = DateTime.Now;

            //string format = "yyyy-MM-dd HH:mm:ss";

            //MessageBox.Show(timestamp.ToString(format));
        }

        public void LoadData()
        {
            conn.Open();
            string query = "SELECT * FROM queues JOIN patients ON queues.fk_id_patient = patients.Id_patient ORDER BY queues.type_queue ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            cmd.Dispose();
            sda.Dispose();
            conn.Close();

            dgridQueue.ItemsSource = dt.DefaultView;
        }
    }
}
