using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace QuanLyQuanCafe
{
    /// <summary>
    /// Interaction logic for TroGiup.xaml
    /// </summary>
    public partial class TroGiup : Window
    {
        public TroGiup()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("http://www.google.com");
        }
    }
}
