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

namespace emguLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> modules = new List<String> 
        {
            CvInvoke.OPENCV_CORE_LIBRARY
            ,CvInvoke.OPENCV_IMGPROC_LIBRARY

            ,CvInvoke.OPENCV_VIDEO_LIBRARY
            //,CvInvoke.OPENCV_FLANN_LIBRARY
            //,CvInvoke.OPENCV_ML_LIBRARY

            //,CvInvoke.OPENCV_HIGHGUI_LIBRARY
            //,CvInvoke.OPENCV_OBJDETECT_LIBRARY
            //,CvInvoke.OPENCV_FEATURES2D_LIBRARY
            //,CvInvoke.OPENCV_CALIB3D_LIBRARY
              
            //,CvInvoke.OPENCV_LEGACY_LIBRARY

            //,CvInvoke.OPENCV_CONTRIB_LIBRARY
            //,CvInvoke.OPENCV_NONFREE_LIBRARY
            //,CvInvoke.OPENCV_PHOTO_LIBRARY
            //,CvInvoke.OPENCV_VIDEOSTAB_LIBRARY
 
            //,CvInvoke.OPENCV_FFMPEG_LIBRARY 
            //,CvInvoke.OPENCV_GPU_LIBRARY 
            //,CvInvoke.OPENCV_STITCHING_LIBRARY
            
            //,CvInvoke.EXTERN_GPU_LIBRARY
            //,CvInvoke.EXTERN_LIBRARY
        };

        public MainWindow()
        {
            //*
            modules.RemoveAll(String.IsNullOrEmpty);
            for (int i = 0; i < modules.Count; ++i)
                modules[i] = String.Format("{0}.dll", modules[i]);
            CvInvoke.LoadUnmanagedModules(null, modules.ToArray());
            //*/
            InitializeComponent();
            resizeDemo();
        }


        void resizeDemo()
        {
            string filepath = Directory.GetFiles(@"img\resize", "*.png")[0];
            Image<Bgr, Byte> target = new Image<Bgr, byte>(filepath);

            imgProc0.Source = GetBitmapSource(target);
            imgProc1.Source = GetBitmapSource(target.Resize(
                    (int)(target.Width * 0.5), (int)(target.Height * 0.5), Emgu.CV.CvEnum.INTER.CV_INTER_NN));
            imgProc2.Source = GetBitmapSource(target.Resize(
                    (int)(target.Width * 0.5), (int)(target.Height * 0.5), Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
            imgProc3.Source = GetBitmapSource(target.Resize(
                    (int)(target.Width * 0.5), (int)(target.Height * 0.5), Emgu.CV.CvEnum.INTER.CV_INTER_AREA));
        }

        private System.Windows.Media.Imaging.BitmapSource GetBitmapSource(Image<Bgr, Byte> _image)
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
    }
}
