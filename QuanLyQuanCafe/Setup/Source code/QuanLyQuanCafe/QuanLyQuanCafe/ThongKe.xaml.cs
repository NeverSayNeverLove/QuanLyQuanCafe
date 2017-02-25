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

using Npgsql;
using System.Data;
namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        NpgsqlConnection conn;
        NpgsqlCommand command;
        string sql;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        NpgsqlDataAdapter da;
        public ThongKe(NpgsqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            ThongTinThongKe();
            this.ShowDialog();
        }

        private void ThongTinThongKe()
        {
            sql = "select \"TenDoUong\",\"SoLuong\",\"DonGia\",\"NgayThang\" from \"ThongKe\" natural join \"LoaiDoUong\" order by \"SoLuong\" desc ";
            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            dt = ds.Tables[0];
            datagridviewThongKe.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }

        private void btThongKe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FromDate = String.Format("{0:yyyy-M-d}", dtpkThongKeTuNgay.SelectedDate);
                string ToDate = String.Format("{0:yyyy-M-d}", dtpkThongKeDenNgay.SelectedDate);
                sql = String.Format("select \"TenDoUong\",\"SoLuong\",\"DonGia\",\"NgayThang\" from \"ThongKe\" natural join \"LoaiDoUong\" where \"NgayThang\">='{0}' and \"NgayThang\"<='{1}' order by \"SoLuong\" desc ", FromDate, ToDate);
                ds.Reset();
                da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(ds);
                dt = ds.Tables[0];
                datagridviewThongKe.ItemsSource = dt.DefaultView;
                da.Update(dt);
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn \"Từ Ngày\" và \"Đến Ngày\"");
            }
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FromDate = String.Format("{0:yyyy-M-d}", dtpkThongKeTuNgay.SelectedDate);
                string ToDate = String.Format("{0:yyyy-M-d}", dtpkThongKeDenNgay.SelectedDate);
                sql = String.Format("Delete from \"ThongKe\" where \"NgayThang\">='{0}' and \"NgayThang\"<='{1}'", FromDate, ToDate);
                command = new NpgsqlCommand(sql, conn);
                command.ExecuteNonQuery();
                ThongTinThongKe();
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn \"Từ Ngày\" và \"Đến Ngày\"");
            }
        }
    }
}
