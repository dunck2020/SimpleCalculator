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

namespace SimpleCalculator
{

    //**************************************************//
    // Title: Simple WPF Calculator for Running
    // Author: Dunckel, John
    // Created: 01-01-21
    // Updated: 01-30-21
    //**************************************************//


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declare variables
        private double _milesGoal;
        private double _totalWeeksRunning;
        private double _daysOffPerWeek;
        private string _units = "Miles";
        private double _answer;
        private string _per = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// determines which calculation to perform based on button licked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            Button operationButton = sender as Button;
            string operation = operationButton.Name;

            //validate all numbers are entered
            
            bool validation = ValidateTextBoxes();

            if (validation)
            {
                //determines if user has entered appropriate number of days
                if (_daysOffPerWeek < 7 && _daysOffPerWeek >= 0)
                {
                    CalculateMilesPer(operation);
                }
                else
                {
                    MessageBox.Show("Days off per week must be between 0 and 6.");
                    textBox_DaysOff.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please verify all entries are numbers.");
            }
        }

        /// <summary>
        /// quick validation to verify all boxes have number entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateTextBoxes()
        {
            //to avoid copy and paste of non numeric entry
            bool valid = double.TryParse(textBox_MileageGoal.Text, out _milesGoal) &&
                double.TryParse(textBox_Weeks.Text, out _totalWeeksRunning) &&
                double.TryParse(textBox_DaysOff.Text, out _daysOffPerWeek);
            return valid;
        }

        /// <summary>
        /// calculations for miles per day based on button clicked
        /// </summary>
        /// <param name="operation"></param>
        private void CalculateMilesPer(string operation)
        {
            //switch statement for per day or per week
            switch (operation)
            {
                case "button_MilesPerDay":
                    _per = "per day";
                    _answer = _milesGoal / (_totalWeeksRunning * (7 - _daysOffPerWeek));
                    break;
                case "button_MilesPerWeek":
                    _per = "per week";
                    _answer = _milesGoal / _totalWeeksRunning;
                    break;
                default:
                    _answer = 0;
                    break;
            }
            //show text box with calculation
            textBox_Mileage.Visibility = Visibility.Visible;
            textBox_Mileage.Text = $"Average {_answer:F2} {_units} {_per}";
        }

        /// <summary>
        /// validates user input as it is entered in the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="key"></param>
        private void ValidateUserInput(object sender, KeyEventArgs key)
        {
            TextBox userInputBox = sender as TextBox;
            //no message will display if user keys backspace or enter
            //no message will display if box is empty or null
            if (!(key.Key == Key.Back) && !(key.Key == Key.Enter) && !(userInputBox.Text == "") && !(userInputBox.Text == null))
                //determines if user has entered number
                if (!double.TryParse(userInputBox.Text, out double x))
                {
                    //if user presses space bar
                    if (key.Key == Key.Space)
                    {
                        MessageBox.Show($"Space bar is not a valid number.");
                    }
                    //user has entered a non number
                    else
                    {
                        MessageBox.Show($"{userInputBox.Text} is not a valid number.");
                    }
                }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            //exit application
            Environment.Exit(0);
        }
        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            //help window displayed
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            //clear button to reset text boxes
            textBox_MileageGoal.Text = "";
            textBox_Weeks.Text = "";
            textBox_DaysOff.Text = "";
            textBox_Mileage.Visibility = Visibility.Hidden;
            textBox_MileageGoal.Focus();
        }

        #region INTERFACE OPTIONS

        /// <summary>
        /// user can choose color settings from combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_InterFace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selection = ComboBox_InterFace.SelectedIndex;
            //switch based on selection of combo box index
            switch (selection)
            {
                case 0:
                    //cool color theme
                    ChangeToCoolColors();
                    break;
                case 1:
                    //dark color theme
                    ChangeToDarkColors();
                    break;
                case 2:
                    //default 
                    ChangeToDefaultColors();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// settings return to default colors
        /// </summary>
        private void ChangeToDefaultColors()
        {
            //return to defaults 
            //main background
            grid_main.Background = Brushes.BurlyWood;
            textBox_Mileage.Background = Brushes.BurlyWood;

            //button colors
            button_Help.Background = Brushes.CadetBlue;
            button_Exit.Background = Brushes.CadetBlue;
            button_Clear.Background = Brushes.CadetBlue;
            button_MilesPerDay.Background = Brushes.CadetBlue;
            button_MilesPerWeek.Background = Brushes.CadetBlue;

            //button text
            button_Help.Foreground = Brushes.BlanchedAlmond;
            button_Exit.Foreground = Brushes.BlanchedAlmond;
            button_Clear.Foreground = Brushes.BlanchedAlmond;
            button_MilesPerDay.Foreground = Brushes.BlanchedAlmond;
            button_MilesPerWeek.Foreground = Brushes.BlanchedAlmond;

            //other text
            label_Title.Foreground = Brushes.DarkOliveGreen;
            label_DaysOff.Foreground = Brushes.DarkOliveGreen;
            label_Interface.Foreground = Brushes.DarkOliveGreen;
            label_TotalMilesGoal.Foreground = Brushes.DarkOliveGreen;
            label_Weeks.Foreground = Brushes.DarkOliveGreen;
            radioButton_Kilometers.Foreground = Brushes.DarkOliveGreen;
            radioButton_Miles.Foreground = Brushes.DarkOliveGreen;
            textBox_Mileage.Foreground = Brushes.DarkOliveGreen;

            //text box input background
            textBox_MileageGoal.Background = Brushes.BlanchedAlmond;
            textBox_Weeks.Background = Brushes.BlanchedAlmond;
            textBox_DaysOff.Background = Brushes.BlanchedAlmond;


        }

        /// <summary>
        /// user changes theme to dark colors
        /// </summary>
        private void ChangeToDarkColors()
        {
            //change to dark colors
            //backgrounds
            grid_main.Background = Brushes.Maroon;
            textBox_Mileage.Background = Brushes.Maroon;

            //button background
            button_Help.Background = Brushes.DimGray;
            button_Exit.Background = Brushes.DimGray;
            button_Clear.Background = Brushes.DimGray;
            button_MilesPerDay.Background = Brushes.DimGray;
            button_MilesPerWeek.Background = Brushes.DimGray;

            //button text
            button_Help.Foreground = Brushes.NavajoWhite;
            button_Exit.Foreground = Brushes.NavajoWhite;
            button_Clear.Foreground = Brushes.NavajoWhite;
            button_MilesPerDay.Foreground = Brushes.NavajoWhite;
            button_MilesPerWeek.Foreground = Brushes.NavajoWhite;

            //labels text button color
            label_Title.Foreground = Brushes.Beige;
            label_DaysOff.Foreground = Brushes.Beige;
            label_Interface.Foreground = Brushes.Beige;
            label_TotalMilesGoal.Foreground = Brushes.Beige;
            label_Weeks.Foreground = Brushes.Beige;
            radioButton_Kilometers.Foreground = Brushes.Beige;
            radioButton_Miles.Foreground = Brushes.Beige;
            textBox_Mileage.Foreground = Brushes.Beige;

            //text boxes
            textBox_DaysOff.Background = Brushes.OldLace;
            textBox_MileageGoal.Background = Brushes.OldLace;
            textBox_Weeks.Background = Brushes.OldLace;

        }

        /// <summary>
        /// hange colors to blue/cool colors
        /// </summary>
        private void ChangeToCoolColors()
        {
            //change to cool colors
            //backgrounds
            grid_main.Background = Brushes.DeepSkyBlue;
            textBox_Mileage.Background = Brushes.DeepSkyBlue;

            //button background
            button_Help.Background = Brushes.MediumSlateBlue;
            button_Exit.Background = Brushes.MediumSlateBlue;
            button_Clear.Background = Brushes.MediumSlateBlue;
            button_MilesPerDay.Background = Brushes.MediumSlateBlue;
            button_MilesPerWeek.Background = Brushes.MediumSlateBlue;

            //button text
            button_Help.Foreground = Brushes.PowderBlue;
            button_Exit.Foreground = Brushes.PowderBlue;
            button_Clear.Foreground = Brushes.PowderBlue;
            button_MilesPerDay.Foreground = Brushes.PowderBlue;
            button_MilesPerWeek.Foreground = Brushes.PowderBlue;

            //labels text button color
            label_Title.Foreground = Brushes.DarkBlue;
            label_DaysOff.Foreground = Brushes.DarkBlue;
            label_Interface.Foreground = Brushes.DarkBlue;
            label_TotalMilesGoal.Foreground = Brushes.DarkBlue;
            label_Weeks.Foreground = Brushes.DarkBlue;
            radioButton_Kilometers.Foreground = Brushes.DarkBlue;
            radioButton_Miles.Foreground = Brushes.DarkBlue;
            textBox_Mileage.Foreground = Brushes.DarkBlue;

            //text boxes
            textBox_DaysOff.Background = Brushes.Gainsboro;
            textBox_MileageGoal.Background = Brushes.Gainsboro;
            textBox_Weeks.Background = Brushes.Gainsboro;

        }

        /// <summary>
        /// radio button to change miles and kilometers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((bool)radioButton_Miles.IsChecked)
                {
                    _units = "Miles";
                    label_TotalMilesGoal.Content = "Total Mile Goal:";
                    button_MilesPerDay.Content = "Miles per Day";
                    button_MilesPerWeek.Content = "Miles per Week";
                    textBox_Mileage.Text = $"Average {_answer:F2} {_units} {_per}";
                }
                else if ((bool)radioButton_Kilometers.IsChecked)
                {
                    _units = "Kilometers";
                    label_TotalMilesGoal.Content = "Total Kilometer Goal:";
                    button_MilesPerDay.Content = "Kilometers per Day";
                    button_MilesPerWeek.Content = "Kilometers per Week";
                    textBox_Mileage.Text = $"Average {_answer:F2} {_units} {_per}";

                }
            }

        }

        #endregion
    }
}


