using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;
namespace QuanLyQuanCafe
{
    class KhachHang
    {
        public string DiaChi { get; set; }
        public NpgsqlConnection conn { get; set; }
        public string SDT { get; set; }
        public string IDKH { get; set; }
        public string Ngay { get; set; }
        public string TenKH { get; set; }
        public KhachHang(NpgsqlConnection conn ,string idkh,string ngay,string tenkh,string diachi,string sdt)
        {
            this.conn = conn;
            TenKH = tenkh;
            DiaChi = diachi;
            SDT = sdt;
            IDKH = idkh;
            Ngay = ngay;
        }
        public void ThongTinKhachHang()
        {
            string sql = String.Format("update \"KhachHang\" set \"TenKH\"='{0}',\"DiaChi\"='{1}' , \"SoDienThoai\"='{2}' where \"IDKH\"='{3}' and \"Ngay\"='{4}'", TenKH, DiaChi, SDT, IDKH, Ngay);
            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}
