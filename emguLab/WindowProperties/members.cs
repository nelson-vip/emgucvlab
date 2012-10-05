using Emgu.CV;
using emguLab.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace emguLab
{
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


        void initModuleLoading()
        {
            //*
            modules.RemoveAll(String.IsNullOrEmpty);
            for (int i = 0; i < modules.Count; ++i)
                modules[i] = String.Format("{0}.dll", modules[i]);
            CvInvoke.LoadUnmanagedModules(null, modules.ToArray());
            //*/
        }

        void initKeyBindings()
        {
            // Bind Key
            InputBinding ib = new InputBinding(
                CommandSet.ToggleConsole,
                new KeyGesture(Key.OemTilde, ModifierKeys.Control));
            this.InputBindings.Add(ib);
            // Bind handler
            CommandBinding cb = new CommandBinding(CommandSet.ToggleConsole);
            cb.Executed += new ExecutedRoutedEventHandler(OnToggleConsole);
            this.CommandBindings.Add(cb);
        }
    }
}
