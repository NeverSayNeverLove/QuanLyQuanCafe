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
    /// Interaction logic for QuanLyNhanVien.xaml
    /// </summary>
    public partial class QuanLyNhanVien : Window
    {
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand command;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        NpgsqlDataAdapter da;
        NpgsqlDataReader dr;
        public QuanLyNhanVien(NpgsqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            SelectDataViewInNhanVien();
            this.ShowDialog();
        }
        private void ThemNV()
        {
            try
            {
                if (tbQuanLyNhanVienTenNV.Text == "") throw new Exception(string.Format("Cần nhập thông tin Nhân Viên mới !!!"));
                int luong;
                RadioButton rbtgioitinh = (rbtNam.IsChecked == true) ? rbtNam : rbtNu;
                bool rbtthoigian = (rbtpart.IsChecked == true) ? false : true;
                if (tbQuanLyNhanVienLuong.Text == "" || Int32.Parse(tbQuanLyNhanVienLuong.Text)>60000) luong = 60000;// mac dinh Luong 1 ca la 60k/5 tieng
                else luong=Int32.Parse(tbQuanLyNhanVienLuong.Text);

                if (dtpkQuanLyNhanVienNgaySinh.Text == "")
                    dtpkQuanLyNhanVienNgaySinh.SelectedDate = DateTime.MinValue;
                if (dtpkQuanLyNhanVienNgayVaoLam.Text == "")
                    dtpkQuanLyNhanVienNgayVaoLam.SelectedDate = DateTime.Today;
                
                NhanVien newnv = new NhanVien(conn,
                                              tbQuanLyNhanVienTenNV.Text,
                                              dtpkQuanLyNhanVienNgayVaoLam.SelectedDate.Value.ToString("yyyy-M-d"),
                                              luong,
                                              rbtgioitinh.Content.ToString(),
                                              rbtthoigian,
                                              tbQuanLyNhanVienDiaChi.Text,
                                              dtpkQuanLyNhanVienNgaySinh.SelectedDate.Value.ToString("yyyy-M-d"),
                                              tbQuanLyNhanVienSDT.Text);
                newnv.AddNV();
                ResetInf();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        private void btQuanLyNhanVienThem_Click(object sender, RoutedEventArgs e)
        {
            ThemNV();
            SelectDataViewInNhanVien();
        }
        private void SelectDataViewInNhanVien()
        {
            sql = "select \"IDNV\",\"TenNV\",\"ThoiGian\",\"NgayVaoLam\",\"SoDienThoai\",\"DiaChi\",\"Luong\",\"GioiTinh\",\"NgaySinh\" from \"NhanVien\" order by \"IDNV\"";
            command = new NpgsqlCommand(sql, conn);
            ds.Reset();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            //ds.Tables[0].Columns["NgayVaoLam"].DataType
            dt = ds.Tables[0];
            datagridviewQuanLyNhanVien.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }
        private void ResetInf()
        {
            tbQuanLyNhanVienTenNV.Text = "";
            tbQuanLyNhanVienDiaChi.Text = "";
            tbQuanLyNhanVienLuong.Text = "";
            tbQuanLyNhanVienSDT.Text = "";
            dtpkQuanLyNhanVienNgaySinh.SelectedDate = null;
            dtpkQuanLyNhanVienNgayVaoLam.SelectedDate = null;
        }

        private void XoaNV()
        {
            try
            {
                NhanVien NV = new NhanVien(conn, Int32.Parse(tbQuanLyNhanVienIDNV.Text));
                NV.SubNV();
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("Cần nhập mã nhân viên hoặc mã nhân viên không tồn tại!!!",MessageBoxButton.OKCancel,MessageBoxImage.Error));
            }
        }
        private void btQuanLyNhanVienXoa_Click(object sender, RoutedEventArgs e)
        {
            XoaNV();
            SelectDataViewInNhanVien();
        }

        private void btQuanLyTinhLuong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbQuanLyNhanVienIDNV.Text != "" && dtpkQuanLyNhanVienNgayKetThuc.SelectedDate!=null)
                {
                    sql = string.Format("select \"Luong\",\"TenNV\",\"NgayVaoLam\",\"ThoiGian\" from \"NhanVien\" where \"IDNV\"='{0}'", tbQuanLyNhanVienIDNV.Text);
                    command = new NpgsqlCommand(sql, conn);
                    dr = command.ExecuteReader();
                    
                    dr.Read();

                    int luong = Int32.Parse(dr[0].ToString());
                    DateTime ngaybatdau = DateTime.Parse(dr["NgayVaoLam"].ToString());
                    DateTime ngayketthuc = dtpkQuanLyNhanVienNgayKetThuc.SelectedDate.Value;
                    TimeSpan timespan = ngayketthuc.Subtract(ngaybatdau);
                    int songaylam = Convert.ToInt32(timespan.TotalDays);
                    if (songaylam < 0) throw (new Exception("Lỗi: \"Ngày kết thúc\" phải sau \"Ngày bắt đầu\""));
                    bool rbttime = Convert.ToBoolean(dr["ThoiGian"]);
                    decimal sum;
                    if (rbttime==true)
                    {
                        sum = luong * 2 * songaylam;
                    }
                    else sum = luong * songaylam;
                    string money = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:#,###}", sum);

                    MessageBoxResult msbr = MessageBox.Show(string.Format("Họ và tên: {0}\nMã số nhân viên: {1}\nNgày vào làm: {2}\nNgày kết thúc: {3}\nLương: {4}", dr["TenNV"].ToString(), tbQuanLyNhanVienIDNV.Text, ngaybatdau.ToString(), ngayketthuc.ToString(), money),"Tinh Luong",MessageBoxButton.YesNo);
                    switch (msbr)
                    {
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Yes:
                            {
                                string Sngayketthuc = String.Format("{0:yyyy-M-d}", dtpkQuanLyNhanVienNgayKetThuc.SelectedDate);
                                conn.Close();
                                conn.Open();
                                sql = String.Format("UPDATE \"NhanVien\" SET \"NgayVaoLam\"='{0}' where \"IDNV\"=\'{1}\'", Sngayketthuc, tbQuanLyNhanVienIDNV.Text);
                                command = new NpgsqlCommand(sql, conn);
                                command.ExecuteNonQuery();
                                SelectDataViewInNhanVien();
                                break;
                            }
                    }
                }
                else
                {
                    throw (new Exception("Cần nhập IDNV và ngày kết thúc để tính lương"));
                }
            }
            catch (Exception expluong)
            {
                MessageBox.Show(expluong.Message);
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
        }

        private void btQuanLyNhanVienTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tbQuanLyNhanVienTenNV.Text == "" && tbQuanLyNhanVienIDNV.Text == "") || (tbQuanLyNhanVienIDNV.Text != "" && tbQuanLyNhanVienTenNV.Text != ""))
                {
                    SelectDataViewInNhanVien();
                    throw (new Exception("Chương trình hỗ trợ:\n1. Tìm kiếm xem thông tin theo tên Nhân viên, chỉ nhập tên Nhân Viên\n2. Tìm kiếm, Sửa thông tin theo IDNV, chỉ nhập IDNV"));
                }
                else
                    if (tbQuanLyNhanVienTenNV.Text != "" && tbQuanLyNhanVienIDNV.Text == "")
                        sql = string.Format("select \"IDNV\",\"TenNV\",\"ThoiGian\",\"NgayVaoLam\",\"SoDienThoai\",\"DiaChi\",\"Luong\",\"GioiTinh\",\"NgaySinh\" from \"NhanVien\" where Lower(\"TenNV\") like lower('%{0}%')", tbQuanLyNhanVienTenNV.Text);
                    else
                        sql = string.Format("select \"IDNV\",\"TenNV\",\"ThoiGian\",\"NgayVaoLam\",\"SoDienThoai\",\"DiaChi\",\"Luong\",\"GioiTinh\",\"NgaySinh\" from \"NhanVien\" where  \"IDNV\"='{0}'", tbQuanLyNhanVienIDNV.Text);
                        command = new NpgsqlCommand(sql, conn);

                        ds.Reset();
                        da = new NpgsqlDataAdapter(sql, conn);
                        da.Fill(ds);
                        dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                            throw (new Exception("Không tồn tại nhân vien cần tìm"));
                        datagridviewQuanLyNhanVien.ItemsSource = dt.DefaultView;
                        da.Update(dt);
                    
            }
            catch (Exception sexp)
            {
                MessageBox.Show(sexp.Message);
            }
            
        }
        private void UpdateThongTinNhanVien()
        {
            try
            {
                if (tbQuanLyNhanVienIDNV.Text == "")
                    throw (new Exception("Cần nhập IDNV trước khi sửa"));

                sql = string.Format("select \"IDNV\",\"TenNV\",\"NgayVaoLam\",\"SoDienThoai\",\"DiaChi\",\"Luong\",\"GioiTinh\",\"NgaySinh\" from \"NhanVien\" where \"IDNV\"='{0}'", tbQuanLyNhanVienIDNV.Text);
                command = new NpgsqlCommand(sql, conn);
                ds.Reset();
                da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(ds);
                dt = ds.Tables[0];
                if (tbQuanLyNhanVienTenNV.Text == "") tbQuanLyNhanVienTenNV.Text = dt.Rows[0]["TenNV"].ToString();
                if (tbQuanLyNhanVienDiaChi.Text == "") tbQuanLyNhanVienDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                if (tbQuanLyNhanVienLuong.Text == "") tbQuanLyNhanVienLuong.Text = dt.Rows[0]["Luong"].ToString();
                if (tbQuanLyNhanVienSDT.Text == "") tbQuanLyNhanVienSDT.Text = dt.Rows[0]["SoDienThoai"].ToString();
                if (dtpkQuanLyNhanVienNgaySinh.SelectedDate == null) dtpkQuanLyNhanVienNgaySinh.SelectedDate = Convert.ToDateTime(dt.Rows[0]["NgaySinh"].ToString());
                if (dtpkQuanLyNhanVienNgayVaoLam.SelectedDate == null) dtpkQuanLyNhanVienNgayVaoLam.SelectedDate = Convert.ToDateTime(dt.Rows[0]["NgayVaoLam"].ToString());

                RadioButton rbt = (rbtNam.IsChecked == true) ? rbtNam : rbtNu;
                bool rbtthoigian = (rbtpart.IsChecked == true) ? false : true;
                string ngaysinh = String.Format("{0:yyyy-M-d}", dtpkQuanLyNhanVienNgaySinh.SelectedDate);
                string ngayvaolam = String.Format("{0:yyyy-M-d}", dtpkQuanLyNhanVienNgayVaoLam.SelectedDate);
                sql = String.Format("UPDATE \"NhanVien\" SET \"TenNV\"='{0}',\"DiaChi\"='{2}',\"NgaySinh\"='{1}',\"GioiTinh\"='{3}',\"SoDienThoai\"='{4}',\"Luong\"=\'{5}\',\"NgayVaoLam\"='{6}',\"ThoiGian\"='{8}' where \"IDNV\"=\'{7}\'", tbQuanLyNhanVienTenNV.Text, ngaysinh, tbQuanLyNhanVienDiaChi.Text, rbt.Content.ToString(), tbQuanLyNhanVienSDT.Text, Convert.ToInt32(tbQuanLyNhanVienLuong.Text), ngayvaolam, tbQuanLyNhanVienIDNV.Text,rbtthoigian);//Int32.Parse(tbQuanLyNhanVienLuong.Text)
                command = new NpgsqlCommand(sql, conn);
                command.ExecuteNonQuery();

                tbQuanLyNhanVienTenNV.Text = "";
                tbQuanLyNhanVienDiaChi.Text = "";
                tbQuanLyNhanVienLuong.Text = "";
                tbQuanLyNhanVienSDT.Text = "";
                dtpkQuanLyNhanVienNgaySinh.SelectedDate = null;
                dtpkQuanLyNhanVienNgayVaoLam.SelectedDate = null;
                

                SelectDataViewInNhanVien();
            }
            catch (Exception uexp)
            {
                MessageBox.Show(uexp.Message);
            }
        }

        private void btQuanLyNhanVienSua_Click(object sender, RoutedEventArgs e)
        {
            UpdateThongTinNhanVien();
            SelectDataViewInNhanVien();
        }
    }
}
