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
    /// Interaction logic for QuanLyThucDon.xaml
    /// </summary>
    public partial class QuanLyThucDon : Window
    {
        public int IDKH { get; set; }
        DateTime datetime;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        NpgsqlDataAdapter da;
        string sql;
        NpgsqlCommand command;
        NpgsqlConnection conn;
        public QuanLyThucDon(NpgsqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            dtpQuanLyThucDon.SelectedDate = DateTime.Today.Date;
            SelectDataViewInThucDon();
            this.ShowDialog();
        }

        private void btQuanLyThucDonTimKiem_Click(object sender, RoutedEventArgs e)
        {
            SearchLoaiDoUong();
        }
        private void SearchLoaiDoUong()
        {
            if (tbQuanLyThucDonTenMon.Text != "")
                sql = string.Format("select * from \"LoaiDoUong\" where Lower(\"TenDoUong\") like lower('%{0}%')", tbQuanLyThucDonTenMon.Text);
            else
                sql = "select * from \"LoaiDoUong\"";
            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            dt = ds.Tables[0];
            datagridviewQuanLyThucDon.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }
        private void SelectDataViewInThucDon()
        {
            sql = "select * from \"Chon\" natural join \"Ngoi\" order by \"Ngay\", \"IDBan\", \"IDDoUong\"";
            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            dt = ds.Tables[0];
            datagridviewQuanLyThucDon.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }

        private void btQuanLyThucDonThem_Click(object sender, RoutedEventArgs e)
        {
                sql = string.Format("select \"IDKH\" from \"Ngoi\" where \"IDBan\"='{0}' and \"Ngay\"='{1}' and \"ThanhToanSau\"='0'", tbQuanLyThucDonIDBan.Text, dtpQuanLyThucDon.SelectedDate.Value.ToString("yyyy-M-d"));
                command = new NpgsqlCommand(sql, conn);
                IDKH = Convert.ToInt32(command.ExecuteScalar());

            

            string dts = String.Format("{0:yyyy-M-d}", dtpQuanLyThucDon.SelectedDate.Value.ToString("yyyy-M-d"));
            datetime = Convert.ToDateTime(dts);
            try
            {

                if (IDKH == 0)
                    throw (new Exception("Bàn không tồn tại"));

                if (tbQuanLyThucDonIDBan.Text == "" || tbQuanLyThucDonMaMon.Text == "" || tbQuanLyThucDonSoLuong.Text == "")
                    throw (new Exception("Cần nhập đầy đủ thông tin (IDBan, Mã đồ uống, số lượng, lựa chọn ngày)"));
                //Insert thong tin bang chon

                sql = "Insert into \"Chon\" (\"IDKH\",\"Ngay\",\"SoLuong\",\"IDDoUong\") values (@IDKH,@Ngay,@SoLuong,@IDDoUong)";
                
                command = new NpgsqlCommand(sql, conn);

                try
                {
                    command.Parameters.AddWithValue("@IDKH", IDKH.ToString());
                    command.Parameters.AddWithValue("@Ngay", datetime);
                    command.Parameters.AddWithValue("@IDDoUong", tbQuanLyThucDonMaMon.Text);
                    command.Parameters.AddWithValue("@SoLuong", Int32.Parse(tbQuanLyThucDonSoLuong.Text));
                    command.ExecuteNonQuery();
                    SelectDataViewInThucDon();
                }
                catch (Exception)
                {
                    throw (new Exception("Mã đồ uống nhập sai"));
                }
            }
            catch (Exception expThucDon)
            {
                MessageBox.Show(expThucDon.Message);
            }
        }

        private void btQuanLyThucDonXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = String.Format("{0:yyyy-M-d}", dtpQuanLyThucDon.SelectedDate);
                if (tbQuanLyThucDonMaMon.Text == "")
                    sql = string.Format("delete from \"Chon\" as ch USING \"Ngoi\" as ng where ch.\"IDKH\"=ng.\"IDKH\" and ch.\"Ngay\"=ch.\"Ngay\" and \"IDBan\"=\'{0}\' and ch.\"Ngay\"=\'{1}\'", tbQuanLyThucDonIDBan.Text, date);
                else
                    sql = string.Format("delete from \"Chon\" as ch USING \"Ngoi\" as ng where ch.\"IDKH\"=ng.\"IDKH\" and ch.\"Ngay\"=ch.\"Ngay\" and \"IDBan\"=\'{0}\' and ch.\"Ngay\"=\'{1}\' and ch.\"IDDoUong\"=\'{2}\'", tbQuanLyThucDonIDBan.Text, date,tbQuanLyThucDonMaMon.Text);
                command = new NpgsqlCommand(sql, conn);
                command.ExecuteNonQuery();
                SelectDataViewInThucDon();
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("Bàn {0} chưa được đặt trong ngày {1}", tbQuanLyThucDonIDBan.Text, dtpQuanLyThucDon.SelectedDate.ToString()));
            }
        }

        private void btQuanLyThucDonSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = String.Format("{0:yyyy-M-d}", dtpQuanLyThucDon.SelectedDate);
                sql = String.Format("UPDATE \"Chon\" SET \"SoLuong\"=\'{0}\' WHERe \"IDDoUong\"=\'{1}\'  and \"Ngay\"=\'{2}\' and \"IDKH\" in(	select \"IDKH\"	from \"Chon\" as ch natural join \"Ngoi\" as ng	where ng.\"IDBan\"=\'{3}\' and \"IDDoUong\"=\'{1}\'  and \"Ngay\"=\'{2}\' )", tbQuanLyThucDonSoLuong.Text, tbQuanLyThucDonMaMon.Text, date, tbQuanLyThucDonIDBan.Text);
                command = new NpgsqlCommand(sql, conn);
                command.ExecuteNonQuery();
                SelectDataViewInThucDon();
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập đầy đủ Mã Món, IDBan và số lượng cần chỉnh sửa (Lựa chọn ngày nếu cần thiết)");
            }
        }
    }
}