using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace QuanLyQuanCafe
{
    class NhanVien
    {
        string sql;
        NpgsqlCommand command;
        NpgsqlConnection conn;
        public int IDNV { get; set; }
        public string TenNV { get; set; }
        public string NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string NgayVaoLam { get; set; }
        public int Luong { get; set; }
        public string GioiTinh { get; set; }
        public bool ThoiGian { get; set; }
        public string SoDienThoai { get; set; }
        private void GetIDVN(NpgsqlConnection conn)
        {
            sql = "select count(*) from \"NhanVien\"";
            command = new NpgsqlCommand(sql, conn);
            IDNV = Convert.ToInt32(command.ExecuteScalar());
            if (IDNV != 0)
            {
                sql = String.Format("select \"IDNV\"  from \"NhanVien\" group by \"IDNV\" having CAST(\"IDNV\" AS int)>= all(select CAST(\"IDNV\" AS int) from \"NhanVien\")");
                command = new NpgsqlCommand(sql, conn);
                IDNV = Convert.ToInt32(command.ExecuteScalar());

            }
            IDNV++;
        }
        public NhanVien(NpgsqlConnection conn,string tennv,string ngayvaolam,int luong,string gioitinh,bool thoigian,string diachi,string ngaysinh,string sodienthoai)
        {
            this.conn = conn;
            GetIDVN(conn);
            TenNV = tennv;
            NgayVaoLam = ngayvaolam;
            Luong = luong;
            GioiTinh = gioitinh;
            DiaChi = diachi;
            NgaySinh = ngaysinh;
            SoDienThoai = sodienthoai;
            ThoiGian = thoigian;
        }
        public NhanVien(NpgsqlConnection conn, int idnv)
        {
            IDNV = idnv;
            this.conn = conn;
        }
        public void AddNV()
        {
            string dts;
            DateTime datetime;
            sql = "Insert into \"NhanVien\" (\"IDNV\",\"TenNV\",\"NgayVaoLam\",\"Luong\",\"GioiTinh\",\"DiaChi\",\"NgaySinh\",\"SoDienThoai\",\"ThoiGian\") values (@IDNV,@TenNV,@NgayVaoLam,@Luong,@GioiTinh,@DiaChi,@NgaySinh,@SoDienThoai,@ThoiGian)";
            command = new NpgsqlCommand(sql, conn);
            command.Parameters.AddWithValue("@IDNV", IDNV.ToString());
            command.Parameters.AddWithValue("@TenNV", TenNV);

            dts = String.Format("{0:yyyy-M-d}", NgayVaoLam);
            datetime = Convert.ToDateTime(dts);
            command.Parameters.AddWithValue("@NgayVaoLam", datetime);

            command.Parameters.AddWithValue("@ThoiGian", ThoiGian);

            command.Parameters.AddWithValue("@Luong", Luong);
            command.Parameters.AddWithValue("@GioiTinh", GioiTinh);
            command.Parameters.AddWithValue("@DiaChi", DiaChi);

            dts = String.Format("{0:yyyy-M-d}", NgaySinh);
            datetime = Convert.ToDateTime(dts);
            command.Parameters.AddWithValue("@NgaySinh", datetime);

            command.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
            command.ExecuteNonQuery();

        }
        public void SubNV()
        {
            sql = string.Format("Delete from \"NhanVien\" where \"IDNV\"='{0}'", IDNV);
            command = new NpgsqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}
