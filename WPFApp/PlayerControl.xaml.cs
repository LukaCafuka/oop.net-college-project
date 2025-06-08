using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFApp
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(PlayerControl), new PropertyMetadata(""));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(string), typeof(PlayerControl), new PropertyMetadata(""));

        public string ShirtNumber
        {
            get => (string)GetValue(ShirtNumberProperty);
            set => SetValue(ShirtNumberProperty, value);
        }

        public static readonly DependencyProperty PlayerImageProperty =
            DependencyProperty.Register("PlayerImage", typeof(ImageSource), typeof(PlayerControl), new PropertyMetadata(null));

        public ImageSource PlayerImage
        {
            get => (ImageSource)GetValue(PlayerImageProperty);
            set => SetValue(PlayerImageProperty, value);
        }
    }
} 