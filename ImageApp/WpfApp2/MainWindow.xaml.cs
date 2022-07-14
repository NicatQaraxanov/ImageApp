using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    //////////////////MALLIM SAVE, SAVE AS FALAN JSON-ILE ISHLEMIR NORMAL SHEKIL KIMI SAVE EDIR. (KODA BAXIN DAHA YAXSHI BASA DUSESSIZ). OPEN DE NE ELEMELIDI BILMEDIM DEYE YAZMADIM :(
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            var AllImages = new AllImagesPage();
            frm.Navigate(AllImages);

            DataContext = this;
        }
    }

    public class Pics
    {
        public string Picture { get; set; }
    }
}
