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
using System.Globalization;
namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    public partial class Information : Window
    {
        string sql;
        NpgsqlCommand command;
        NpgsqlConnection conn;
        public Information(NpgsqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            ThongTinBanChon();
            bangmonLoad();
            this.Show();
            
        }

        private void bangmonLoad()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            sql = "SELECT * FROM \"LoaiDoUong\"";

            //// data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            ds.Reset();

            // filling DataSet with result from NpgsqlDataAdapter
            da.Fill(ds);

            // since it C# DataSet can handle multiple tables, we will select first
            dt = ds.Tables[0];

            // connect and fill grid to DataTable
            datagridviewThuNgan1.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }
        public void ThongTinBanChon()
        {
            sql = "select \"KhachHang\".\"IDKH\",\"Ngoi\".\"IDBan\",\"KhachHang\".\"Ngay\", \"Ngoi\".\"ThanhToanSau\" from \"KhachHang\" left join \"Ngoi\" on \"KhachHang\".\"IDKH\"=\"Ngoi\".\"IDKH\" and \"KhachHang\".\"Ngay\"=\"Ngoi\".\"Ngay\"";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            dt = ds.Tables[0];
            datagridviewThuNgan2.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }

        #region checkban
		 
        public void ban101checked(object sender, RoutedEventArgs e)
        {
            imBan101.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
        }

        public void ckBan101unchecked(object sender, RoutedEventArgs e)
        {
            imBan101.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute ));
        }

        private void ckBan102unchecked(object sender, RoutedEventArgs e)
        {
            imBan102.BeginInit();
            imBan102.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan102.EndInit();
        }

        private void ckban102checked(object sender, RoutedEventArgs e)
        {
            imBan102.BeginInit();
            imBan102.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan102.EndInit();
        }
        private void ckban103checked(object sender, RoutedEventArgs e)
        {
            imBan103.BeginInit();
            imBan103.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan103.EndInit();
        }
        private void ckban103unchecked(object sender, RoutedEventArgs e)
        {
            imBan103.BeginInit();
            imBan103.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan103.EndInit();
        }
        private void ckban104checked(object sender, RoutedEventArgs e)
        {
            imBan104.BeginInit();
            imBan104.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan104.EndInit();
        }
        private void ckban104unchecked(object sender, RoutedEventArgs e)
        {
            imBan104.BeginInit();
            imBan104.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan104.EndInit();
        }
        private void ckban105checked(object sender, RoutedEventArgs e)
        {
            imBan105.BeginInit();
            imBan105.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan105.EndInit();
        }
        private void ckban105unchecked(object sender, RoutedEventArgs e)
        {
            imBan105.BeginInit();
            imBan105.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan105.EndInit();
        }
        private void ckban106checked(object sender, RoutedEventArgs e)
        {
            imBan106.BeginInit();
            imBan106.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan106.EndInit();
        }
        private void ckban106unchecked(object sender, RoutedEventArgs e)
        {
            imBan106.BeginInit();
            imBan106.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan106.EndInit();
        }
        private void ckban107checked(object sender, RoutedEventArgs e)
        {
            imBan107.BeginInit();
            imBan107.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan107.EndInit();
        }
        private void ckban107unchecked(object sender, RoutedEventArgs e)
        {
            imBan107.BeginInit();
            imBan107.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan107.EndInit();
        }
        private void ckban108checked(object sender, RoutedEventArgs e)
        {
            imBan108.BeginInit();
            imBan108.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan108.EndInit();
        }
        private void ckban108unchecked(object sender, RoutedEventArgs e)
        {
            imBan108.BeginInit();
            imBan108.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan108.EndInit();
        }
        private void ckban109checked(object sender, RoutedEventArgs e)
        {
            imBan109.BeginInit();
            imBan109.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan109.EndInit();
        }
        private void ckban109unchecked(object sender, RoutedEventArgs e)
        {
            imBan109.BeginInit();
            imBan109.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan109.EndInit();
        }
        private void ckban201checked(object sender, RoutedEventArgs e)
        {
            imBan201.BeginInit();
            imBan201.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan201.EndInit();
        }
        private void ckban201unchecked(object sender, RoutedEventArgs e)
        {
            imBan201.BeginInit();
            imBan201.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan201.EndInit();
        }
        private void ckban202checked(object sender, RoutedEventArgs e)
        {
            imBan202.BeginInit();
            imBan202.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan202.EndInit();
        }
        private void ckban202unchecked(object sender, RoutedEventArgs e)
        {
            imBan202.BeginInit();
            imBan202.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan202.EndInit();
        }
        private void ckban203checked(object sender, RoutedEventArgs e)
        {
            imBan203.BeginInit();
            imBan203.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan203.EndInit();
        }
        private void ckban203unchecked(object sender, RoutedEventArgs e)
        {
            imBan203.BeginInit();
            imBan203.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan203.EndInit();
        }
        private void ckban204checked(object sender, RoutedEventArgs e)
        {
            imBan204.BeginInit();
            imBan204.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan204.EndInit();
        }
        private void ckban204unchecked(object sender, RoutedEventArgs e)
        {
            imBan204.BeginInit();
            imBan204.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan204.EndInit();
        }
        private void ckban205checked(object sender, RoutedEventArgs e)
        {
            imBan205.BeginInit();
            imBan205.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan205.EndInit();
        }
        private void ckban205unchecked(object sender, RoutedEventArgs e)
        {
            imBan205.BeginInit();
            imBan205.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan205.EndInit();
        }
        private void ckban206checked(object sender, RoutedEventArgs e)
        {
            imBan206.BeginInit();
            imBan206.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan206.EndInit();
        }
        private void ckban206unchecked(object sender, RoutedEventArgs e)
        {
            imBan206.BeginInit();
            imBan206.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan206.EndInit();
        }
        private void ckban207checked(object sender, RoutedEventArgs e)
        {
            imBan207.BeginInit();
            imBan207.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan207.EndInit();
        }
        private void ckban207unchecked(object sender, RoutedEventArgs e)
        {
            imBan207.BeginInit();
            imBan207.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan207.EndInit();
        }
        private void ckban208checked(object sender, RoutedEventArgs e)
        {
            imBan208.BeginInit();
            imBan208.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan208.EndInit();
        }
        private void ckban208unchecked(object sender, RoutedEventArgs e)
        {
            imBan208.BeginInit();
            imBan208.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan208.EndInit();
        }
        private void ckban209checked(object sender, RoutedEventArgs e)
        {
            imBan209.BeginInit();
            imBan209.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan209.EndInit();
        }
        private void ckban209unchecked(object sender, RoutedEventArgs e)
        {
            imBan209.BeginInit();
            imBan209.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan209.EndInit();
        }
        private void ckban210checked(object sender, RoutedEventArgs e)
        {
            imBan210.BeginInit();
            imBan210.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan210.EndInit();
        }
        private void ckban210unchecked(object sender, RoutedEventArgs e)
        {
            imBan210.BeginInit();
            imBan210.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan210.EndInit();
        }

        private void ckban110unchecked(object sender, RoutedEventArgs e)
        {
            imBan110.BeginInit();
            imBan110.Source = new BitmapImage(new Uri("ResourceImage/Album1/cup-icon.png", UriKind.RelativeOrAbsolute));
            imBan110.EndInit();
        }

        private void ckban110checked(object sender, RoutedEventArgs e)
        {
            imBan110.BeginInit();
            imBan110.Source = new BitmapImage(new Uri("ResourceImage/Album1/Accept-icon.png", UriKind.RelativeOrAbsolute));
            imBan110.EndInit();
        }


        #endregion
        private void btQuanLyBan_Click(object sender, RoutedEventArgs e)
        {

            QuanLyBan qlb = new QuanLyBan(this, conn);
        }

        private void btThongKe_Click(object sender, RoutedEventArgs e)
        {
            ThongKe tk = new ThongKe(conn);
        }

        private void btQuanLyNhanVien_Click(object sender, RoutedEventArgs e)
        {
            QuanLyNhanVien qlnv = new QuanLyNhanVien(conn);
        }
        private void btQuanLyThucDon_Click(object sender, RoutedEventArgs e)
        {
             QuanLyThucDon qltd= new QuanLyThucDon(conn);
        }
        private void btTroGiup_Click(object sender, RoutedEventArgs e)
        {
            TroGiup tg = new TroGiup();
        }

        private void btTinhTien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)datagridviewThuNgan2.SelectedItem;
                if(rowview==null)
                    throw (new Exception("Chọn bàn cần thanh toán"));

                string IDKH = rowview["IDKH"].ToString();
                string Ngay = String.Format("{0:yyyy-M-d}", rowview["Ngay"]);

                sql = String.Format("select count(*) from \"Chon\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                command = new NpgsqlCommand(sql, conn);
                if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                    throw (new Exception("Bàn chưa gọi món"));

                sql = String.Format("select sum(\"DonGia\"*\"SoLuong\") from \"Chon\" natural join \"LoaiDoUong\" natural join \"HoaDon\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                command = new NpgsqlCommand(sql, conn);
                decimal sum = Convert.ToInt32(command.ExecuteScalar());
                string money = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:#,###}", sum);
                MessageBox.Show(String.Format("Ngày: {0}\nBàn: {1}\nTổng tiền là: {2} đồng", Ngay, rowview["IDBan"], money));
            }
            catch (Exception exptt)
            {
                MessageBox.Show(exptt.Message);
            }
        }

        private void btInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)datagridviewThuNgan2.SelectedItem;
                string IDKH = rowview["IDKH"].ToString();
                string Ngay = String.Format("{0:yyyy-M-d}", rowview["Ngay"]);
                sql = String.Format("select sum(\"DonGia\"*\"SoLuong\") from \"Chon\" natural join \"LoaiDoUong\" natural join \"HoaDon\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                command = new NpgsqlCommand(sql, conn);
                decimal sum = Convert.ToInt32(command.ExecuteScalar());
                string money = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:#,###}", sum);


                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                
                sql = String.Format("select * from \"Chon\" natural join \"LoaiDoUong\" natural join \"HoaDon\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                ds.Reset();
                da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(ds);
                dt = ds.Tables[0];
                List<InfoHoaDon> LinfHD = new List<InfoHoaDon>();
                foreach (DataRow row in dt.Rows)
                {
                    LinfHD.Add(new InfoHoaDon(row["TenDoUong"].ToString(), row["SoLuong"].ToString(), row["DonGia"].ToString()));
                }
                string InfoList = string.Format("Ngày: {0}\nIDKH: {1}\n{2,-30}{3,-30}{4}", Ngay, IDKH, "Tên Đồ Uống", "Đơn Giá", "Số Lượng");
                foreach (InfoHoaDon inf in LinfHD)
                {
                    InfoList += string.Format("\n{0,-30}{1,-30}{2}", inf.TenDoUong, inf.DonGia, inf.SoLuong);
                }
                InfoList += String.Format("\n Tổng tiền: {0}", money);
                MessageBoxResult mbrl = MessageBox.Show(InfoList, "In Hoá Đơn", MessageBoxButton.YesNo);
                switch (mbrl)
                {
                    case    MessageBoxResult.No:
                        break;
                    case    MessageBoxResult.Yes:
                        {
                            sql = string.Format("delete from \"KhachHang\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'",IDKH,Ngay);
                            command = new NpgsqlCommand(sql, conn);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Đã in hoá đơn");
                            ThongTinBanChon();
                            break;
                        }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn bàn cần thanh toán");
            }
        }

        private void btHuyGoiMon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)datagridviewThuNgan2.SelectedItem;
                string IDKH = rowview["IDKH"].ToString();
                string Ngay = String.Format("{0:yyyy-M-d}", rowview["Ngay"]);
                MessageBoxResult mbrl = MessageBox.Show("Huỷ Thực Đơn?", "Huỷ Gọi Món", MessageBoxButton.YesNo);
                switch (mbrl)
                {
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Yes:
                        {
                            sql = string.Format("delete from \"KhachHang\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                            command = new NpgsqlCommand(sql, conn);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Đã huỷ");
                            ThongTinBanChon();
                            break;
                        }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Chọn bàn cần huỷ");
            }
        }

        private void btNo_Click(object sender, RoutedEventArgs e)
        {

            ThongTinKhachHang TTKH = new ThongTinKhachHang(this, conn);
            ThongTinBanChon();
        }

        private void btThongTinKhachHang_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlDataReader datareader = null;
            try
            {
                DataRowView rowview = (DataRowView)datagridviewThuNgan2.SelectedItem;
                string IDKH = rowview["IDKH"].ToString();
                string Ngay = String.Format("{0:yyyy-M-d}", rowview["Ngay"]);
                sql = String.Format("select \"IDKH\",\"TenKH\",\"DiaChi\",\"SoDienThoai\",\"Ngay\" from \"KhachHang\" where \"IDKH\"='{0}' and \"Ngay\"='{1}'", IDKH, Ngay);
                command = new NpgsqlCommand(sql, conn);
                datareader = command.ExecuteReader();
                string TenKH;
                string DiaChi;
                string SoDienThoai;
                while(datareader.Read())
                {
                    TenKH = (string)datareader["TenKH"];
                    DiaChi = (string)datareader["DiaChi"];
                    SoDienThoai = (string)datareader["SoDienThoai"];
                    MessageBox.Show(String.Format("IDKH: {0}\nTen Khach Hang: {1}\nDia Chi: {2}\nSo Dien Thoai: {3}\nNgay: {4}", IDKH, TenKH, DiaChi, SoDienThoai, Ngay));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chọn Khách Hàng Thanh Toán Sau");
            }
            finally
            {
                if (datareader != null)
                    datareader.Close();
            }
        }
    }
    class InfoHoaDon
    {
        public string TenDoUong { get; set; }
        public string SoLuong { get; set; }
        public string DonGia { get; set; }
        public InfoHoaDon(string TenDoUong,string SoLuong,string DonGia)
        {
            this.TenDoUong = TenDoUong;
            this.SoLuong = SoLuong;
            this.DonGia = DonGia;
        }
    }
}
