using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataFormats = System.Windows.Forms.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AllImagesPage.xaml
    /// </summary>
    public partial class AllImagesPage : Page
    {

        public ObservableCollection<Pics> Picturess { get; set; } = new ObservableCollection<Pics>();

        public static List<Image> Images { get; set; } = new List<Image>();

        public AllImagesPage()
        {
            InitializeComponent();

            //Picturess = new ObservableCollection<Pics>() {
            //new Pics() { Picture = "Avatar.png" },
            //new Pics() { Picture = "SamsungWatch.png" }
            //};
            DataContext = this;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var view = new ViewPic();
            view.Picturess = Picturess;
            view.Picture = (Pictures.SelectedItem as Pics).Picture;
            view.Picturess = Picturess;
            view.CurrentPic = Pictures.SelectedIndex;
            NavigationService.Navigate(view);
        }

        private void Pictures_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                {
                    foreach (var item in fileNames)
                    {
                        Picturess.Add(new Pics() { Picture = item });

                        BitmapImage bi3 = new BitmapImage();
                        bi3.BeginInit();
                        bi3.UriSource = new Uri(item, UriKind.Relative);
                        bi3.EndInit();
                        Images.Add(new Image() { Source = bi3, Stretch = Stretch.Uniform });

                    }
                }
            }
        }







        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            if (save.ShowDialog() == true)
            {
                if (!File.Exists(save.FileName))
                    Directory.CreateDirectory(save.FileName);

                int a = 1;

                foreach (var item in Images)
                {
                    using (FileStream stream = new FileStream(save.FileName + "\\" + a + ".png", FileMode.Create, FileAccess.Write))
                    {
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)item.Source));
                        encoder.Save(stream);
                    }
                    a++;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var file = new FolderBrowserDialog();

            int a = 1;

            if (file.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in Images)
                {
                repeat:
                    if (File.Exists(file.SelectedPath + "\\" + a + ".png"))
                    {
                        a++;
                        goto repeat;
                    }
                    using (FileStream stream = new FileStream(file.SelectedPath + "\\" + a + ".png", FileMode.Create, FileAccess.Write))
                    {
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)item.Source));
                        encoder.Save(stream);
                    }
                    a++;
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var folder = new System.Windows.Forms.OpenFileDialog();
            folder.Filter = "|*.png";

            if (folder.ShowDialog() == DialogResult.OK)
            {

                Picturess.Add(new Pics() { Picture = folder.FileName });

                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(folder.FileName, UriKind.Relative);
                bi3.EndInit();
                Images.Add(new Image() { Source = bi3, Stretch = Stretch.Uniform });
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            var file = new FolderBrowserDialog();

            if (file.ShowDialog() == DialogResult.OK)
            {
                var files = Directory.GetFiles(file.SelectedPath, "*.png", SearchOption.AllDirectories);

                foreach (string filename in files)
                {
                    Picturess.Add(new Pics() { Picture = filename });

                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(filename, UriKind.Relative);
                    bi3.EndInit();
                    Images.Add(new Image() { Source = bi3, Stretch = Stretch.Uniform });
                }
            }
        }
    }
}
