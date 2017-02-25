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
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for QuanLyBan.xaml
    /// </summary>
    public partial class QuanLyBan : Window
    {
        NpgsqlConnection conn;
        NpgsqlCommand command;
        string sql;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        NpgsqlDataAdapter da;
        Information inf;
        public static int IDKH { get; set; }
        public QuanLyBan(Information inf, NpgsqlConnection conn)
        {
            this.conn = conn;
            this.inf = inf;
            InitializeComponent();
            dtpQuanLyBan.SelectedDate = DateTime.Today.Date;
            SelectDataViewInKhachHang();
            //AddColumn();
            this.ShowDialog();
        }
        //private void AddColumn()
        //{
        //    dt.Columns.Add(new DataColumn("IDKH", typeof(string)));
        //    dt.Columns.Add(new DataColumn("IDBan", typeof(string)));
        //    dt.Columns.Add(new DataColumn("SoNguoi", typeof(byte)));
        //    dt.Columns[1].Unique = true;
        //    datagridviewQuanLyBan.ItemsSource = dt.DefaultView;
        //}

        private void btThemBan_Click(object sender, RoutedEventArgs e)
        {

            int ktban = 0;
            sql = "select count(*) from \"KhachHang\"";
            command = new NpgsqlCommand(sql, conn);
            IDKH = Convert.ToInt32(command.ExecuteScalar());
            if (IDKH != 0)
            {
                sql = String.Format("select \"IDKH\" from \"KhachHang\" where \"Ngay\"='{0}' order by \"IDKH\" desc limit 1", dtpQuanLyBan.SelectedDate.Value.ToString("yyyy-M-d"));
                command = new NpgsqlCommand(sql, conn);
                IDKH = Convert.ToInt32(command.ExecuteScalar());
            }
            try
            {
                Exception eidban;
                sql = String.Format("select \"IDBan\" from \"Ban\" where \"IDBan\"=\'{0}\'", tbQuanLyBanMaBan.Text);
                command = new NpgsqlCommand(sql, conn);
                ktban = Convert.ToInt32(command.ExecuteScalar());
                if (ktban == 0)
                {
                    eidban = new Exception("Bàn không tồn tại, mời nhập bàn khác");
                    throw (eidban);
                }
                sql = String.Format("select \"IDBan\" from \"Ngoi\" where \"IDBan\"=\'{0}\' and \"Ngay\"=\'{1}\' and \"ThanhToanSau\"='false'", tbQuanLyBanMaBan.Text, dtpQuanLyBan.SelectedDate.Value.ToString("yyyy-M-d"));
                command = new NpgsqlCommand(sql, conn);
                ktban = Convert.ToInt32(command.ExecuteScalar());
                if (ktban!=0)
                {
                    eidban = new Exception("Bàn đã có người ngồi, mời chọn bàn khác");
                    throw(eidban);
                }

                //Tang IDKH len 1
                IDKH++;
                string dts = String.Format("{0:yyyy-M-d}", dtpQuanLyBan.SelectedDate.Value.ToString("yyyy-M-d"));
                DateTime datetime = Convert.ToDateTime(dts);
                //Insert thong tin IDKH vao bang KhachHang
                sql = "Insert into \"KhachHang\" (\"SoHD\",\"IDKH\",\"Ngay\") values (@SoHD,@IDKH,@Ngay)";
                command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@SoHD", IDKH.ToString());
                command.Parameters.AddWithValue("@IDKH", IDKH.ToString());
                command.Parameters.AddWithValue("@Ngay", datetime);
                command.ExecuteNonQuery();

                //Insert thong tin SoHD,IDKH vao bang HoaDon
                sql = "Insert into \"HoaDon\" (\"SoHD\",\"IDKH\",\"Ngay\") values (@SoHD,@IDKH,@Ngay)";
                command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@IDKH", IDKH.ToString());
                command.Parameters.AddWithValue("@SoHD", IDKH.ToString());
                command.Parameters.AddWithValue("@Ngay", datetime);
                command.ExecuteNonQuery();

                //Insert thong tin IDKH vao bang Ngoi
                sql = "Insert into \"Ngoi\" (\"IDKH\",\"IDBan\",\"Ngay\",\"ThanhToanSau\",\"SoKhach\") values (@IDKH,@IDBan,@Ngay,@ThanhToanSau,@SoKhach)";
                command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@IDKH", IDKH.ToString());
                command.Parameters.AddWithValue("@IDBan", tbQuanLyBanMaBan.Text);
                command.Parameters.AddWithValue("@Ngay", datetime);
                command.Parameters.AddWithValue("@ThanhToanSau", false);// true la thanh toan sau
                command.Parameters.AddWithValue("@SoKhach", Convert.ToInt32(tbQuanLyBanSoNguoi.Text));
                command.ExecuteNonQuery();

                SelectDataViewInKhachHang();
                inf.ThongTinBanChon();
            }
            catch (Exception eidban)
            {
                MessageBox.Show(eidban.Message);
            }
        }
        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            if (datagridviewQuanLyBan.SelectedItem != null)
            {
                foreach (DataRow row in dt.Select())
                {
                    row.Delete();
                }
                //dt.
                datagridviewQuanLyBan.Items.Remove(datagridviewQuanLyBan.SelectedItem);
            }  
        }

        private void btXoaBan_Click(object sender, RoutedEventArgs e)
        {
            Exception exoa;
            MessageBoxResult mbresult;
            string date = String.Format("{0:yyyy-M-d}", dtpQuanLyBan.SelectedDate);
            try
            {
                if (tbQuanLyBanMaBan.Text != "" && tbQuanLyBanIDKH.Text != "")
                {
                    exoa = new Exception("Chỉ được xoá hoặc theo Mã Bàn và ngày Hoặc theo Mã Khách Hàng và Ngày");
                    throw (exoa);
                }
                else
                    if (tbQuanLyBanMaBan.Text != "")
                        mbresult = MessageBox.Show(String.Format("Bạn có chắc muốn xoá Bàn số :{0} trong ngày: {1}", tbQuanLyBanMaBan.Text, date), "Cảnh Báo", MessageBoxButton.YesNo);
                    else
                        mbresult = MessageBox.Show(String.Format("Bạn có chắc muốn xoá Khách Hàng có Mã số :{0} trong ngày: {1}", tbQuanLyBanIDKH.Text, date), "Cảnh Báo", MessageBoxButton.YesNo);
                switch (mbresult)
                {
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        {
                            tbQuanLyBanMaBan.Text = null;
                            tbQuanLyBanIDKH.Text = null;
                            tbQuanLyBanSoNguoi.Text = null;
                        }
                        return;
                }


                sql = String.Format("DELETE FROM \"KhachHang\" as kh USING \"Ngoi\" as ng WHERE ng.\"ThanhToanSau\"='false' and kh.\"IDKH\" = ng.\"IDKH\" AND kh.\"Ngay\"=ng.\"Ngay\" AND \"IDBan\"=\'{0}\' and kh.\"Ngay\"=\'{1}\' or kh.\"IDKH\"=\'{2}\' and kh.\"Ngay\"=\'{3}\'", tbQuanLyBanMaBan.Text, date, tbQuanLyBanIDKH.Text, date);
                command = new NpgsqlCommand(sql, conn);
                command.ExecuteNonQuery();
                tbQuanLyBanMaBan.Text = null;
                tbQuanLyBanIDKH.Text = null;
                tbQuanLyBanSoNguoi.Text = null;
                SelectDataViewInKhachHang();
                inf.ThongTinBanChon();
            }
            catch (Exception edel)
            {
                MessageBox.Show(edel.Message);
            }
        }
        private void SelectDataViewInKhachHang()
        {
            sql = "select \"KhachHang\".\"IDKH\",\"Ngoi\".\"IDBan\",\"Ngoi\".\"SoKhach\",\"KhachHang\".\"Ngay\",\"Ngoi\".\"ThanhToanSau\" from \"KhachHang\" left join \"Ngoi\" on \"KhachHang\".\"IDKH\"=\"Ngoi\".\"IDKH\" and \"KhachHang\".\"Ngay\"=\"Ngoi\".\"Ngay\"";

            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            dt = ds.Tables[0];
            datagridviewQuanLyBan.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }
    }
}
