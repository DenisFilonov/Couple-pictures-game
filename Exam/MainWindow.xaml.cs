using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CouplePictures _currentGame;
        private Button[] _buttons;
        private Random _rndGenerate;
        private int[] _profInputList;
        private int _amountOfTry { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _buttons = new Button[20];
            _rndGenerate = new Random();
            _profInputList = new int[20];
            lblAmountOfTry.Content = _amountOfTry;
            lblAmountOfTry.Visibility = Visibility.Hidden;
        }
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            if (_currentGame == null)
            {
                var test = Enumerable.Range(1, 20).OrderBy(x => _rndGenerate.Next()).ToArray();
                for (int i = 0; i < 20; i++)
                {
                    _buttons[i] = gridPictures.Children[i] as Button;
                    _profInputList[i] = (test[i] - 1) % 10 + 1;
                }
                _currentGame = new CouplePictures(_buttons, _profInputList);
                startGame.IsEnabled = false;
                MessageBox.Show("The game started! Good luck", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                lblAmountOfTry.Visibility = Visibility.Visible;
                checkBoxEasy.Visibility = Visibility.Hidden;
                checkBoxEasy_Checked(sender, e);
            }
        }

        private void resetGame_Click(object sender, RoutedEventArgs e)
        {
            if (_currentGame != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    _currentGame.Reset(_buttons);
                }
                _currentGame.Available(_buttons);
                MessageBox.Show("The game restarted! Good luck", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                _currentGame = null;
                startGame.IsEnabled = true;
                _amountOfTry = 30;
                lblAmountOfTry.Content = _amountOfTry;
                lblAmountOfTry.Foreground = Brushes.Green;
                lblAmountOfTry.Visibility = Visibility.Visible;
                checkBoxEasy.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("First start the game", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void mtpButtonsOnclick(object sender, RoutedEventArgs e)
        {
            if (_currentGame == null)
            {
                MessageBox.Show("First press Start button to start the game", "Notification", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int buttonName = int.Parse((sender as Button).Name.Substring(6)) - 1;
                _currentGame.ClassOnClick(_buttons, buttonName);
                CheckTheAmount();
            }
        }

        private void CheckTheAmount()
        {
            lblAmountOfTry.Content = --_amountOfTry;
            if (_amountOfTry == 20)
            {
                lblAmountOfTry.Foreground = Brushes.Orange;
            }
            else if (_amountOfTry == 10)
            {
                lblAmountOfTry.Foreground = Brushes.Yellow;
            }
            else if (_amountOfTry == 5)
            {
                lblAmountOfTry.Foreground = Brushes.Red;
            }
            else if (_amountOfTry == 0)
            {
                lblAmountOfTry.Foreground = Brushes.Black;
                lblAmountOfTry.Content = "End";

                for (int i = 0; i < 20; i++)
                {
                    _currentGame.Reset(_buttons);
                }
                _currentGame.NotAvailable(_buttons);
                lblAmountOfTry.Visibility = Visibility.Hidden;
                MessageBox.Show("You have exhausted the number of tries, the game is over", "Defeat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void checkBoxEasy_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBoxEasy.IsChecked == true)
            {
                _amountOfTry = 50;                
                lblAmountOfTry.Content = _amountOfTry;
            }
            else
            {
                _amountOfTry = 30;
                lblAmountOfTry.Content = _amountOfTry;
            }
        }
    }
}
