using System;
using System.Globalization;
using System.Windows.Data;


namespace HPCalculator
{
    // Converter used in the front end to generate a modifier for display
    // The object displayed to the user isn't used in backend logic, but it's useful for them to see it
    // Note also by convention a modifier of 0 is always displayed as "+0"
    class ScoreToModConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int score = (int)value;
            int mod = RuleBook.CalcModFromAbilityScore(score);
            return (mod >= 0 ? "+" : "") + mod.ToString();
        }

        // This should never be used because it's impossible to derive a score from a modifier
        // For example, a modifier of +0 could be a score of either 10 or 11
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
