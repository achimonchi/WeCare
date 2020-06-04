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

//<add name = "ucareEntities1" connectionString="metadata=res://*/csdl|res://*/ssdl|res://*/msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\MSSQLLOCALDB;initial catalog=ucare;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

namespace CAREIT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid g = new Grid();
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=ucare;Integrated Security=True");
        int Id_emp;
        public MainWindow(int Id)
        {
            conn.Open();
            string query = "SELECT * FROM employees JOIN roles ON roles.Id_role = employees.fk_id_role " +
                " WHERE Id_employee = '" + Id + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            string role = dt.Rows[0]["name_role"].ToString();
            string id_role = dt.Rows[0]["fk_id_role"].ToString();
            string name = dt.Rows[0]["name_employee"].ToString();
            Id_emp = int.Parse(dt.Rows[0]["Id_employee"].ToString().Trim());

            MessageBox.Show("Welcome, "+dt.Rows[0]["name_role"].ToString());

            string lbl = name.Trim() + "," + role.Trim();
            InitializeComponent();
            lblName.Content = lbl;
            GridPrincipal.Children.Add(new Employees());
        }

        private void btn_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            g = GridPrincipal;
            
            switch (index)
            {
                
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Employees());
                    g = GridPrincipal;
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Patients());
                    g = GridPrincipal;
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new QueueList());
                    g = GridPrincipal;
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Medicine(Id_emp));
                    g = GridPrincipal;
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Transactions());
                    g = GridPrincipal;
                    break;
                default:
                    GridPrincipal.Children.Clear();
                    break;

            }
        }

        private void MoveCursorMenu(int i)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (120+ (60 * i)), 0, 0);
        }

        private void btn_logout(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
        }
    }
}
