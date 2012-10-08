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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using emguLab.core;
using System.Diagnostics;

namespace emguLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            initModuleLoading();
            initKeyBindings();
            loadSelector();
            
            NxConsole.Initialize();
            this.Activate(); //bring this to front
        }

        void OnToggleConsole(object sender, ExecutedRoutedEventArgs e)
        {
            NxConsole.Toggle();
            this.Activate();
        }

        System.Windows.Media.Imaging.BitmapSource GetBitmapSource(Image<Bgr, Byte> _image)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            _image.ToBitmap().Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            //Using the freeze function to avoid cross thread operations 
            bi.Freeze();
            return bi;
        }

        void setImage(string target, System.Windows.Media.Imaging.BitmapSource bmp, string info)
        {
            System.Windows.Controls.Image _img = this.FindName(target) as System.Windows.Controls.Image;
            if (_img != null)
            {
                _img.Source = bmp;
                _img.ToolTip = info;
            }
        }

        void loadSelector()
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach (var path in Directory.GetFiles(@"img\resize", "*.png").ToList<string>())
            {
                files.Add(new FileInfo(path));
            }
            imgSelector.ItemsSource = files;
            imgSelector.DisplayMemberPath = "Name";
            imgSelector.SelectedIndex = files.Count > 0 ? 0 : -1;
        }

        void imgSelector_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FileInfo fio = imgSelector.SelectedItem as FileInfo;
            Image<Bgr, Byte> srcImg = new Image<Bgr, byte>(fio.FullName);
            int scale = 3;
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Image {1}:{2}*{3} loaded.", sw.Elapsed.Milliseconds, fio.Name, srcImg.Width, srcImg.Height));

            setImage("imgProc0", GetBitmapSource(srcImg), "Origin");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering Origin.", sw.Elapsed.Milliseconds));

            setImage("imgProc1", GetBitmapSource(srcImg.Resize(srcImg.Width * scale, srcImg.Height * scale, Emgu.CV.CvEnum.INTER.CV_INTER_NN)), "Nearest Neighbor");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc1.", sw.Elapsed.Milliseconds));
            
            setImage("imgProc2", GetBitmapSource(srcImg.Resize(srcImg.Width * scale, srcImg.Height * scale, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR)), "Bilinear");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc2.", sw.Elapsed.Milliseconds));
            
            setImage("imgProc3", GetBitmapSource(srcImg.Resize(srcImg.Width * scale, srcImg.Height * scale, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC)), "Cubic");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc3.", sw.Elapsed.Milliseconds));
        }

        void imgSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FileInfo fio = imgSelector.SelectedItem as FileInfo;
            Image<Bgr, Byte> srcImg = new Image<Bgr, byte>(fio.FullName);
            int scale = 3;
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Image {1}:{2}*{3} loaded.", sw.Elapsed.Milliseconds, fio.Name, srcImg.Width, srcImg.Height));

            ;

            setImage("imgProc0", GetBitmapSource(srcImg), "Origin");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering Origin.", sw.Elapsed.Milliseconds));

            setImage("imgProc1", GetBitmapSource(srcImg.Rotate(90, new Bgr(System.Drawing.Color.GhostWhite))), "Nearest Neighbor");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc1.", sw.Elapsed.Milliseconds));

            setImage("imgProc2", GetBitmapSource(srcImg.Rotate(180, new Bgr(System.Drawing.Color.GhostWhite))), "Bilinear");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc2.", sw.Elapsed.Milliseconds));

            setImage("imgProc3", GetBitmapSource(srcImg.Rotate(270, new Bgr(System.Drawing.Color.GhostWhite))), "Cubic");
            NxConsole.Print(ConsoleColor.Green,
                string.Format("[{0,5}ms] Finished rendering proc3.", sw.Elapsed.Milliseconds));
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

    }
}
