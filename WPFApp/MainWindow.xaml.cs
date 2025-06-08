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
using DataLayer.JsonModels;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplyResolution();
        }

        private void ApplyResolution()
        {
            if (string.IsNullOrEmpty(ConfigFile.resolution))
            {
                return;
            }

            switch (ConfigFile.resolution)
            {
                case "Fullscreen":
                    WindowState = WindowState.Maximized;
                    break;
                case "1280 x 720":
                    Width = 1280;
                    Height = 720;
                    break;
                case "1600 x 900":
                    Width = 1600;
                    Height = 900;
                    break;
                case "1920 x 1080":
                    Width = 1920;
                    Height = 1080;
                    break;
            }
        }
    }
}
