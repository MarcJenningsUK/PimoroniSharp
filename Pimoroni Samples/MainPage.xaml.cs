using Pimoroni.Blinkt;
using Pimoroni.Helpers;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pimoroni_Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Blinkt blinkt;

        public MainPage()
        {
            this.InitializeComponent();
            try
            {
                blinkt = new Blinkt();
            }
            catch(DeviceNotFoundException ex)
            {
                new MessageDialog(ex.Message, "Oops").ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (null == blinkt) return;
            Random rnd = new Random(DateTime.Now.Millisecond);
            var red = rnd.Next(0, 255);
            var green = rnd.Next(0, 255);
            var blue = rnd.Next(0, 255);
            for (int count = 1; count < 9; count++)
            {
                blinkt.SetBrightness(0.2m);
                blinkt.SetPixel(count, red, green, blue);
            }
            blinkt.Show();
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            if (null == blinkt) return;
            blinkt.SetAllPixels(255, 0, 0);
            blinkt.Show();
        }
        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (null == blinkt) return;
            blinkt.SetAllPixels(0, 255, 0);
            blinkt.Show();
        }
        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            if (null == blinkt) return;
            blinkt.SetAllPixels(0, 0, 255);
            blinkt.Show();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (null == blinkt) return;
            blinkt.Clear();
            blinkt.Show();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (null == blinkt) return;
            blinkt.Clear();
            blinkt.Show();
        }
    }
}
