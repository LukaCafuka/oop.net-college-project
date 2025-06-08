using System;
using System.Windows;
using System.Windows.Media.Animation;
using QuickType;

namespace WPFApp
{
    public partial class TeamInfoWindow : Window
    {
        public TeamInfoWindow(TeamResults team)
        {
            InitializeComponent();
            PopulateFields(team);
            Opacity = 0; // Start transparent for animation
        }

        private void PopulateFields(TeamResults team)
        {
            txtTitle.Text = team.Country ?? "";
            txtFifaCode.Text = team.FifaCode ?? "";
            txtGamesPlayed.Text = team.GamesPlayed.ToString();
            txtWins.Text = team.Wins.ToString();
            txtDraws.Text = team.Draws.ToString();
            txtLosses.Text = team.Losses.ToString();
            txtGoalsFor.Text = team.GoalsFor.ToString();
            txtGoalsAgainst.Text = team.GoalsAgainst.ToString();
            txtGoalDiff.Text = team.GoalDifferential.ToString();
        }

        public void AnimateIn()
        {
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            BeginAnimation(Window.OpacityProperty, fadeIn);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
} 