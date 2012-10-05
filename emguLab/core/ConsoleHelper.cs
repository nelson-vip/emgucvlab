using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace emguLab.core
{
    [SuppressUnmanagedCodeSecurity]
    public static class NxConsole
    {
        private const string Kernel32_DllName = "kernel32.dll";
        [DllImport(Kernel32_DllName)]
        private static extern bool AllocConsole();
        [DllImport(Kernel32_DllName)]
        private static extern bool FreeConsole();
        [DllImport(Kernel32_DllName)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport(Kernel32_DllName)]
        private static extern int GetConsoleOutputCP();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static Boolean isShown = false;

        public static bool HasConsole
        {
            get
            {
                return GetConsoleWindow() != IntPtr.Zero;
            }
        }
        public static void Initialize(string title = "Lab Console")
        {
            if (!HasConsole)
            {
                AllocConsole();
                InvalidateOutAndError();
                
            }
            if (HasConsole)
            {
                ShowWindow(GetConsoleWindow(), SW_HIDE);
                Console.SetWindowSize(100, 40);
                Console.SetWindowPosition(0, 0);
                Console.Title = title;
                Print(ConsoleColor.Yellow, title + " initialized.");
                isShown = false;
            }
        }
        public static void Hide()
        {
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
            //ShowWindow(GetConsoleWindow(), SW_HIDE);
        }
        public static void Toggle()
        {
            if (HasConsole)
            {
                isShown = !isShown;
                if(isShown)
                    ShowWindow(GetConsoleWindow(), SW_HIDE);
                else
                    ShowWindow(GetConsoleWindow(), SW_SHOW);
            }
            //else
            {
              //  Initialize();
            }
        }
        public static void Print(ConsoleColor inColor, string message)
        {
            Console.ForegroundColor = inColor;
            Console.WriteLine(message);
        }

        static void InvalidateOutAndError()
        {
            Type type = typeof(System.Console);
            System.Reflection.FieldInfo _out = type.GetField("_out", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            System.Reflection.FieldInfo _error = type.GetField("_error", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod("InitializeStdOutError", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            _out.SetValue(null, null); _error.SetValue(null, null);
            _InitializeStdOutError.Invoke(null, new object[] { true });
        }
        static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }
    }
}