using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataLayer.JsonModels;
using QuickType;

namespace WPFApp
{
    public partial class PlayerControl : UserControl
    {
        public static readonly DependencyProperty PlayerNameProperty =
            DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(string), typeof(PlayerControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty PlayerImageProperty =
            DependencyProperty.Register("PlayerImage", typeof(ImageSource), typeof(PlayerControl), new PropertyMetadata(null));

        public static readonly DependencyProperty PlayerDataProperty =
            DependencyProperty.Register("PlayerData", typeof(StartingEleven), typeof(PlayerControl), new PropertyMetadata(null));

        public static readonly DependencyProperty GoalsProperty =
            DependencyProperty.Register("Goals", typeof(int), typeof(PlayerControl), new PropertyMetadata(0));

        public static readonly DependencyProperty YellowCardsProperty =
            DependencyProperty.Register("YellowCards", typeof(int), typeof(PlayerControl), new PropertyMetadata(0));

        public string PlayerName
        {
            get => (string)GetValue(PlayerNameProperty);
            set => SetValue(PlayerNameProperty, value);
        }

        public string ShirtNumber
        {
            get => (string)GetValue(ShirtNumberProperty);
            set => SetValue(ShirtNumberProperty, value);
        }

        public ImageSource PlayerImage
        {
            get => (ImageSource)GetValue(PlayerImageProperty);
            set => SetValue(PlayerImageProperty, value);
        }

        public StartingEleven PlayerData
        {
            get => (StartingEleven)GetValue(PlayerDataProperty);
            set => SetValue(PlayerDataProperty, value);
        }

        public int Goals
        {
            get => (int)GetValue(GoalsProperty);
            set => SetValue(GoalsProperty, value);
        }

        public int YellowCards
        {
            get => (int)GetValue(YellowCardsProperty);
            set => SetValue(YellowCardsProperty, value);
        }

        public PlayerControl()
        {
            InitializeComponent();
            DataContext = this;
            MouseEnter += PlayerControl_MouseEnter;
            MouseLeave += PlayerControl_MouseLeave;
            MouseLeftButtonDown += PlayerControl_MouseLeftButtonDown;
        }

        private void PlayerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            RenderTransform = new ScaleTransform(1.1, 1.1);
        }

        private void PlayerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            RenderTransform = new ScaleTransform(1.0, 1.0);
        }

        private void PlayerControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PlayerData != null)
            {
                var window = new PlayerInfoWindow(PlayerData, Goals, YellowCards);
                window.Show();
            }
        }
    }
} 