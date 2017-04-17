using Pimoroni.Blinkt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            blinkt = new Blinkt();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            var red = rnd.Next(0, 255);
            var green = rnd.Next(0, 255);
            var blue = rnd.Next(0, 255);
            for (int count = 1; count < 9; count++)
            {
                blinkt.SetBrightness(0.2m);
                blinkt.SetPixel(count, red, green, blue);
            }
            blinkt.show();
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            for (int count = 1; count < 9; count++)
            {
                blinkt.SetBrightness(0.2m);
                blinkt.SetPixel(count, 255, 0, 0);
            }
            blinkt.show();
        }
        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            for (int count = 1; count < 9; count++)
            {
                blinkt.SetBrightness(0.2m);
                blinkt.SetPixel(count, 0, 255, 0);
            }
            blinkt.show();
        }
        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            for (int count = 1; count < 9; count++)
            {
                blinkt.SetBrightness(0.2m);
                blinkt.SetPixel(count, 0, 0, 255);
            }
            blinkt.show();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            blinkt.clear();
            blinkt.show();
        }
    }
}
