using ControlzEx.Theming;
using System.Windows.Controls;

namespace HPCalculator
{
    public partial class MainWindow
    {
        private string Theme;
        private string ThemeAccent;
        private Character Character;

        public MainWindow()
        {
            // This is left here for future expansion
            // Encapsulating character data into an object allows for multiple characters to be managed if the feature is added
            Character = new Character();

            // To see the default settings, reference the Properties.Settings.Default configuration
            InitializeComponent();
        }

        private void LoadSettings()
        {
            var settings = Properties.Settings.Default;

            // Character
            HitDieComboBox.SelectedIndex = settings.hit_die_index;
            LevelUpDown.Value = settings.levels;
            ConUpDown.Value = settings.con_score;
            FavoredUpDown.Value = settings.favored_level_bonus;
            ToughnessCheckBox.IsChecked = settings.has_toughness;

            // Options
            FirstLevelCheckBox.IsChecked = settings.max_hp_first_level;
            MultiplierComboBox.SelectedIndex = settings.multiplier_index;
            AccentComboBox.SelectedIndex = settings.accent_index;
            ThemeComboBox.SelectedIndex = settings.theme_index;
        }

        // Settings save the application's current state, allowing the user to pick up from wherever they left off
        private void SaveSettings()
        {
            var settings = Properties.Settings.Default;

            // Character
            settings.hit_die_index = HitDieComboBox.SelectedIndex;
            settings.levels = (int)LevelUpDown.Value;
            settings.con_score = (int)ConUpDown.Value;
            settings.favored_level_bonus = (int)FavoredUpDown.Value;
            settings.has_toughness = (bool)ToughnessCheckBox.IsChecked;

            // Options
            settings.max_hp_first_level = (bool)FirstLevelCheckBox.IsChecked;
            settings.multiplier_index = MultiplierComboBox.SelectedIndex;
            settings.theme_index = ThemeComboBox.SelectedIndex;
            settings.accent_index = AccentComboBox.SelectedIndex;

            settings.Save();
        }

        private void UpdateHitPoints()
        {
            if (IsInitialized)
            {
                HPLabel.Content = RuleBook.CalculateHitPoints(Character);
            }
        }

        // Seperate event handler since the sender and routed event objects are necessary, even if never used
        // The reason we reset all character data on any object change is due to bindings
        // For instance, a character cannot have a higher favored level than their current level, so decreasing their level may decrease their favored level
        private void UpdateCharacterEventHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                // If the user deletes the value, it will be null
                // In that case, we don't want to bother updating the value in the character object
                if (LevelUpDown.Value != null) Character.Levels = (int)LevelUpDown.Value;
                if (ConUpDown.Value != null) Character.ConScore = (int)ConUpDown.Value;
                Character.HitDie = (HitDie)HitDieComboBox.SelectedItem;
                Character.HasToughness = (bool)ToughnessCheckBox.IsChecked;
                if (FavoredUpDown.Value != null) Character.FavoredLevelBonus = (int)FavoredUpDown.Value;

                UpdateHitPoints();
            }
        }
 
        private void ChangeTheme()
        {
            // The theme is changed by passing in a string like "Dark.Green"
            ThemeManager.Current.ChangeTheme(this, $"{Theme}.{ThemeAccent}");
        }

        private void AccentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ThemeAccent = comboBox.SelectedItem.ToString();
            ChangeTheme();
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            Theme = comboBox.SelectedItem.ToString();
            ChangeTheme();
        }

        private void MultiplierComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;

            // This is included on the presentation layer because the presentation of the data should be translated as soon as possible
            var multiplier = (comboBox.SelectedValue) switch
            {
                "1/4" => 0.25,
                "1/2" => 0.5,
                "3/4" => 0.75,
                _ => 1,
            };

            RuleBook.GameSettings.HitPointMultiplier = multiplier;
            UpdateHitPoints();
        }

        private void FirstLevelCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            RuleBook.GameSettings.MaxHitPointsAtFirstLevel = (bool) checkBox.IsChecked;
            UpdateHitPoints();
        }

        private void MainView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }

        private void MainView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadSettings();
        }
    }
}
