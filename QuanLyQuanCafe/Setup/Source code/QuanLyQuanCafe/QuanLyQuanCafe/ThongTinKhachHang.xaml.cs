using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for ThongTinKhachHang.xaml
    /// </summary>
    public partial class ThongTinKhachHang : Window
    {
        Information inf;
        NpgsqlConnection conn;
        public ThongTinKhachHang(Information inf,NpgsqlConnection conn)
        {
            this.inf=inf;
            this.conn = conn;
            InitializeComponent();
            this.ShowDialog();
        }

        private void ThongtinkhachhangbtOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mbrl = MessageBox.Show("Thanh Toán Sau?", "Thanh Toán Sau", MessageBoxButton.YesNo);
                switch (mbrl)
                {
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        {
                            DataRowView rowview = (DataRowView)inf.datagridviewThuNgan2.SelectedItem;
                            string IDKH = rowview["IDKH"].ToString();
                            string Ngay = String.Format("{0:yyyy-M-d}", rowview["Ngay"]);

                            string sql = string.Format("update \"Ngoi\" Set \"ThanhToanSau\"='true' where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                            command.ExecuteNonQuery();

                            KhachHang kh = new KhachHang(conn, IDKH, Ngay, ThongtinkhachhangtbTenKH.Text, ThongtinkhachhangtbDiaChi.Text, ThongtinkhachhangtbSDT.Text);
                            kh.ThongTinKhachHang();

                            break;
                        }
                }
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn bàn cần thanh toán sau");
            }
        }

        private void ThongtinkhachhangbtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
