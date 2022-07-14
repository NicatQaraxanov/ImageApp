using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ViewPic.xaml
    /// </summary>
    /// 
    
    public partial class ViewPic : Page
    {
        public ViewPic()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            CurrentPic++;

            DataContext = this;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Picture = Picturess[CurrentPic].Picture;
            CurrentPic++;
            if (CurrentPic == Picturess.Count)
                CurrentPic = 0;
        }


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


        public string Picture
        {
            get { return (string)GetValue(PictureProperty); }
            set { SetValue(PictureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Picture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PictureProperty =
            DependencyProperty.Register("Picture", typeof(string), typeof(ViewPic));



        public int CurrentPic { get; set; }
        public ObservableCollection<Pics> Picturess { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            CurrentPic--;
            if (CurrentPic < 0)
                CurrentPic = Picturess.Count - 1;
            Picture = Picturess[CurrentPic].Picture;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            CurrentPic++;
            if (CurrentPic == Picturess.Count)
                CurrentPic = 0;
            Picture = Picturess[CurrentPic].Picture;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
