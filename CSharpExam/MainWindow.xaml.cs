using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CSharpExam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Year.MaxLength = 4;
            Weigh.MaxLength = 3;
            Height.MaxLength = 3;
        }
        bool isMax = true;
        double year;

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            Year.Text = Height.Text = Weigh.Text = TBTDEE.Text = TBBMR.Text = string.Empty;
        }

        private void Clear()
        {
            Year.Text = Height.Text = Weigh.Text = TBTDEE.Text = TBBMR.Text = string.Empty;
        }

        private void BCalculate_Click(object sender, RoutedEventArgs e)
        {
            double result, ratio;
            TBBMR.Text = TBTDEE.Text = string.Empty;

            string error = "";
            try
            {
                if (string.IsNullOrWhiteSpace(Year.Text) == true || Year.Text.EndsWith(",") == true
                || !(Convert.ToDouble(DateTime.Now.Year - int.Parse(Year.Text)) <= 80 && Convert.ToDouble(DateTime.Now.Year - int.Parse(Year.Text)) >= 15))
                {
                    error += "-Введите корректный возраст\n(15 - 80 лет)\n";
                    Year.Text = string.Empty;
                    year = DateTime.Now.Year - int.Parse(Year.Text);
                }
                if (string.IsNullOrWhiteSpace(Weigh.Text) == true || Weigh.Text.EndsWith(",") == true
                    || !(Convert.ToDouble(Weigh.Text) >= 40 && Convert.ToDouble(Weigh.Text) <= 500))
                {
                    error += "-Введите корректный вес\n(40 - 500 кг)\n";
                    Weigh.Text = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(Height.Text) == true || Height.Text.EndsWith(",") == true
                    || !(Convert.ToDouble(Height.Text) <= 220 && Convert.ToDouble(Height.Text) >= 120))
                {
                    error += "-Введите корректный рост\n(120 - 220 см)\n";
                    Height.Text = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(error) == false)
                {
                    MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }



                if (Gender.SelectedIndex == 0)
                {
                    result = 66 + (13.7 * Convert.ToDouble(Weigh.Text.Replace(" ", ""))) + (5 * Convert.ToDouble(Height.Text.Replace(" ", "")) - (6.8 * Convert.ToInt32(year)));
                }
                else
                {
                    result = 655 + (9.6 * Convert.ToDouble(Weigh.Text.Replace(" ", ""))) + (1.8 * Convert.ToDouble(Height.Text.Replace(" ", ""))) - (4.7 * Convert.ToInt32(year));
                }
                ratio = GetRatio() * result;
                TBBMR.Text = result.ToString();
                TBTDEE.Text = ratio.ToString();
            }
            catch
            {
                MessageBox.Show($"Ошибка введенных данных \n + {error}");
                Clear();
            }
            
        }

        private double GetRatio()
        {
            switch (CBRatio.SelectedIndex)
            {
                case 0:
                    return 1.375;

                case 1:
                    return 1.55;

                case 2:
                    return 1.725;

                case 3:
                    return 1.9;

                default:
                    return 1;
            }
        }

        private void PreviewTextInputDig(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedDig(e.Text);
        }

        private static readonly Regex _regexDig = new Regex("[^0-9.-]+");
        private static bool IsTextAllowedDig(string text)
        {
            return !_regexDig.IsMatch(text);
        }

        private void PreviewTextInputLet(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedLet(e.Text);
        }

        private static readonly Regex _regexLet = new Regex("[^а-я]+");
        private static bool IsTextAllowedLet(string text)
        {
            return !_regexLet.IsMatch(text);
        }

    }
}
