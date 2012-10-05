using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace emguLab.core
{
    public static class CommandSet
    {
        private static RoutedUICommand toggleConsole =
            new RoutedUICommand("ToggleConsole", "Toggle Console", typeof(CommandSet));

        public static RoutedUICommand ToggleConsole
        {
            get { return toggleConsole; }
        }
    }

}
