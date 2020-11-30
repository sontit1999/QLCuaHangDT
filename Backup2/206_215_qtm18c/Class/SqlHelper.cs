using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _206_215_qtm18c
{
    class SqlHelper
    {
        public static int loainguoidung;
        public static string tennguoidung;
        public static string ConnectString = @"Data Source=123-PC\SQLEXPRESS;Initial Catalog=QuanLyCuaHangDT;Integrated Security=True";
        public static SqlConnection con;
    }
}
