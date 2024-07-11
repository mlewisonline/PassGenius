using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PassGenius
{
    public partial class MainWindow : Window
    {
        public int characterSetFlag = 1;
        public char wordSeparator = '-';

        public MainWindow()
        {
            InitializeComponent();
            SetupInitialUIValues();
        }

        private void SetupInitialUIValues()
        {

            sliderBar.Minimum = 14;
            sliderBar.Maximum = 128;
            sliderBar.Value = 14;

            WordsSliderBar.Minimum = 3;
            WordsSliderBar.Maximum = 9;
            WordsSliderBar.Value = 4;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EnCheck_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void DeCheck_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void XKCDButton_Click(object sender, RoutedEventArgs e)
        {
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void GenButon_Click(object sender, RoutedEventArgs e)
        {
            DisplayPrettyPassword(PasswordGenerator.GetPassword((int)sliderBar.Value));
        }

        private void Radio1_Checked(object sender, RoutedEventArgs e)
        {
            characterSetFlag = 1;
           // DisplayPrettyPassword(StringGenerator.GetPasswordString((int)sliderBar.Value, characterSetFlag));
        }

        private void Radio2_Checked(object sender, RoutedEventArgs e)
        {
            characterSetFlag = 2;
           // DisplayPrettyPassword(StringGenerator.GetPasswordString((int)sliderBar.Value, characterSetFlag));
        }

        private void Radio3_Checked(object sender, RoutedEventArgs e)
        {
            characterSetFlag = 3;
           // DisplayPrettyPassword(StringGenerator.GetPasswordString((int)sliderBar.Value, characterSetFlag));
        }

        private async void GenPasswordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Select all text in textbox
            GenPasswordTextBox.SelectAll();

            // Copy to clipboard
            Clipboard.SetText(GenPasswordOverlay.Text);

            // Update status to show text in clip board 
            statusText.Text = "Copied to Clipboard";

            //  Task to clear status after 5 Seconds
            await ClearStatusAsync();
        }

        private async void XKCDResultLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           XKCDResultLabel.SelectAll();

            string str = XKCDResultLabel.Text;

            Clipboard.SetText(str);

            statusText.Text = "Copied to Clipboard";

            // Task to clear status after 5 Seconds
            await ClearStatusAsync();
        }

        private async Task ClearStatusAsync()
        {
            await Task.Delay(10000);
            statusText.Text = String.Empty;
        }

        private async Task ClearClipboardAfter90SecAsync()
        {
            await Task.Delay(90000);
            Clipboard.Clear();
        }

        private void SliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           DisplayPrettyPassword(PasswordGenerator.GetPassword((int)sliderBar.Value));
        }

        private void WordsSliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void DisplayPrettyPassword(string input)
        {
            GenPasswordOverlay.Text = input;
            var inlines = GenPasswordOverlay.Inlines;
            inlines.Clear();
            foreach (char ch in input)
            {
                if (Char.IsDigit(ch))
                {
                    var run = new Run(ch.ToString());
                    run.Foreground = Brushes.RoyalBlue;
                    run.FontWeight = FontWeights.Bold;
                    inlines.Add(run);
                }
                else if (Char.IsLetter(ch))
                {
                    var run = new Run(ch.ToString());
                    run.Foreground = Brushes.Black;
                    run.FontWeight = FontWeights.SemiBold;
                    inlines.Add(run);
                }
                else
                {
                    var run = new Run(ch.ToString());
                    run.Foreground = Brushes.Red;
                    run.FontWeight = FontWeights.Bold;
                    inlines.Add(run);
                }

            }
        }

        private void Titlecase_Checked(object sender, RoutedEventArgs e)
        {
                Uppercase.IsEnabled = false;
                XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void Uppercase_Checked(object sender, RoutedEventArgs e)
        {
            Titlecase.IsEnabled = false;
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void Titlecase_Unchecked(object sender, RoutedEventArgs e)
        {
            Uppercase.IsEnabled = true;
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void Uppercase_Unchecked(object sender, RoutedEventArgs e)
        {
            Titlecase.IsEnabled = true;
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void Separator_Checked(object sender, RoutedEventArgs e)
        {
            wordSeparator = '-';
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }

        private void Separator_Unchecked(object sender, RoutedEventArgs e)
        {
            wordSeparator = ' ';
            XKCDResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.Value, Titlecase.IsChecked.Value);
        }
    }
}