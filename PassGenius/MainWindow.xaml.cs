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

            sliderBar.Minimum = 10;
            sliderBar.Maximum = 128;
            sliderBar.Value = 14;

            WordsSliderBar.Minimum = 3;
            WordsSliderBar.Maximum = 9;
            WordsSliderBar.Value = 4;

            LowerCheckbox.IsChecked = true;
            UpperCheckbox.IsChecked = true;
            NumbersCheckbox.IsChecked = true;
            SpecialsCheckbox.IsChecked = true;
        }

        private int SetPasswordCharSet()
        {
            if (LowerCheckbox.IsChecked == true && UpperCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 1;
            }
            if (LowerCheckbox.IsChecked == true && UpperCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true)
            {
                return 2;
            }
            if (LowerCheckbox.IsChecked == true && UpperCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 3;
            }
            if (LowerCheckbox.IsChecked == true && UpperCheckbox.IsChecked == true)
            {
                return 4;
            }
            if (LowerCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 5;
            }
            if (LowerCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true)
            {
                return 6;
            }
            if (LowerCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 7;
            }
            if (LowerCheckbox.IsChecked == true)
            {
                return 8;
            }
            if (UpperCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 9;
            }
            if (UpperCheckbox.IsChecked == true && NumbersCheckbox.IsChecked == true)
            {
                return 10;
            }
            if (UpperCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 11;
            }
            if (UpperCheckbox.IsChecked == true)
            {
                return 12;
            }
            if (NumbersCheckbox.IsChecked == true && SpecialsCheckbox.IsChecked == true)
            {
                return 13;
            }
            if (NumbersCheckbox.IsChecked == true)
            {
                return 14;
            }
            if (SpecialsCheckbox.IsChecked == true)
            {
                return 15;
            }
            return 1;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void WordsButton_Click(object sender, RoutedEventArgs e)
        {
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void GenButon_Click(object sender, RoutedEventArgs e)
        {
            var set = SetPasswordCharSet();
            DisplayPrettyPassword(PasswordGenerator.GetPassword((int)sliderBar.Value, set));
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

        private async void WordsResultLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WordsResultLabel.SelectAll();

            string str = WordsResultLabel.Text;

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
            var set = SetPasswordCharSet();
            DisplayPrettyPassword(PasswordGenerator.GetPassword((int)sliderBar.Value,set));
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

        private void WordsSliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Titlecase_Checked(object sender, RoutedEventArgs e)
        {
            Uppercase.IsEnabled = false;
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Uppercase_Checked(object sender, RoutedEventArgs e)
        {
            Titlecase.IsEnabled = false;
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Titlecase_Unchecked(object sender, RoutedEventArgs e)
        {
            Uppercase.IsEnabled = true;
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Uppercase_Unchecked(object sender, RoutedEventArgs e)
        {
            Titlecase.IsEnabled = true;
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Separator_Checked(object sender, RoutedEventArgs e)
        {
            wordSeparator = '-';
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void Separator_Unchecked(object sender, RoutedEventArgs e)
        {
            wordSeparator = ' ';
            WordsResultLabel.Text = WordGenerator.GetWords((int)WordsSliderBar.Value, wordSeparator, Uppercase.IsChecked.GetValueOrDefault(), Titlecase.IsChecked.GetValueOrDefault());
        }

        private void LowerCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void UpperCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void NumbersCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SpecialsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void LowerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            MakeSureLowerCaseAlwaysEnabled();

        }

        private void UpperCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            MakeSureLowerCaseAlwaysEnabled();
        }

        private void NumbersCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            MakeSureLowerCaseAlwaysEnabled();
        }

        private void SpecialsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            MakeSureLowerCaseAlwaysEnabled();
        }

        private void MakeSureLowerCaseAlwaysEnabled (){
            if (LowerCheckbox.IsChecked == false && UpperCheckbox.IsChecked == false && NumbersCheckbox.IsChecked == false && SpecialsCheckbox.IsChecked == false)
            {
                LowerCheckbox.IsChecked = true;
            }
            }
    }
}