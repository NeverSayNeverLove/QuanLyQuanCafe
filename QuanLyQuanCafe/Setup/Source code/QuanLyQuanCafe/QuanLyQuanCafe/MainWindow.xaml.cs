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

using Npgsql;
namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connstring = string.Format("Server=localhost;Port=5432;" + "User Id={0};Password={1};Database=QuanLyQuanCafe", tbUser.Text, pbPassword.Password);// Id=MD_Thuy;Password=20144373
                NpgsqlConnection conn = new NpgsqlConnection(connstring);

                conn.Open();
                Information Inf = new Information(conn);
                this.Close();
            }
            catch (Exception)
            {
              
                MessageBox.Show("Tên Đăng Nhập hoặc Mật Khẩu sai, mời nhập lại");
                //throw;
            }
        }
    }
}
