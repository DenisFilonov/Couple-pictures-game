using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Exam
{
    public enum Pictures
    {
        Aston = 1,
        Ferrari = 2,
        Hyundai = 3,
        Koenigsegg = 4,
        Lamborghini = 5,
        Mercedes = 6,
        Mitsuibishi = 7,
        Porsche = 8,
        Subaru = 9,
        Zenovo = 10
    }

    internal class CouplePictures
    {
        private int[] _testArray = new int[20];
        private int _conditionCounter = 0;
        private Button[] _xBox;
        private int _counter = 0;
        private int[] _pressed = new int[2];
        private bool[] _opened = new bool[20];


        public CouplePictures(Button[] box, int[] TArray)
        {
            _xBox = box;
            _testArray = TArray;

            for (int i = 0; i < 20; i++)
            {
                box[i].Content = "";
            }
        }

        public void ClassOnClick(Button[] box, int index)
        {
            if (_counter == 2 || _opened[index] || _counter == 1 && _pressed[0] == index) return;

            _xBox[index].Content = SetPicture((Pictures)_testArray[index]);
            _pressed[_counter] = index;
            _counter++;
            if (_counter == 2)
            {
                ButtonCompare(box, _pressed[0], _pressed[1]);
            }
        }

        public StackPanel SetPicture(Pictures test)
        {
            test.ToString();
            Image img = new Image();
            StackPanel stackPnl = new StackPanel();
            img.Source = new BitmapImage(new Uri(test.ToString() + ".jpg", UriKind.Relative));
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Margin = new Thickness(-2);
            stackPnl.Children.Add(img);
            return stackPnl;
        }

        public void ButtonCompare(Button[] box, int check1, int check2)
        {
            if (_testArray[check1] == _testArray[check2])
            {
                _conditionCounter++;
                _opened[check1] = true;
                _opened[check2] = true;

                _xBox[check1].Visibility = Visibility.Collapsed;
                _xBox[check2].Visibility = Visibility.Collapsed;

                if (_conditionCounter == 10)
                {
                    MessageBox.Show("You Won!!!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                _counter = 0;
            }
            else
            {
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    _xBox[check1].Content = "";
                    _xBox[check2].Content = "";
                    _counter = 0;
                };
            }
        }

        public void Reset(Button[] box)
        {
            for (int i = 0; i < 20; i++)
            {
                _xBox[i].Content = "";
                _xBox[i].Visibility = Visibility.Visible;
            }
        }

        public void Available(Button[] box)
        {
            foreach (var item in box)
            {
                item.IsEnabled = true;
            }
        }

        public void NotAvailable(Button[] box)
        {
            foreach (var item in box)
            {
                item.IsEnabled = false;
            }
        }
    }
}
