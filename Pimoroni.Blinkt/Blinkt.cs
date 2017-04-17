/*
 * Library ported to C3 for Windows 10 iOt by Marc Jennings (marc-jennings.co.uk)
 * Source available at Github/MarcJenningsUK/PimoroniSharp
 */

using Pimoroni.Helpers;
using System;
using Windows.Devices.Gpio;

namespace Pimoroni.Blinkt
{
    public class Blinkt
    {
        private const int DAT = 23;
        private const int CLK = 24;
        private const int NUM_PIXELS = 8;
        private const decimal BRIGHTNESS = 0.2m;

        private bool _gpio_setup = false;
        private bool _clear_on_exit = true;

        Pixel[] pixels = new Pixel[NUM_PIXELS]; 

        GpioController ctrl;
        GpioPin ClockPin, DatPin;

        public Blinkt()
        {
            for(int count = 0; count < NUM_PIXELS; count++)
            {
                pixels[count] = new Pixel();
            }
            SetupGpio();
        }

        ~Blinkt()
        {
            if(_clear_on_exit)
            {
                Clear();
                Show();
                ClockPin.Dispose();
                DatPin.Dispose();
            }
        }

        private void SetupGpio()
        {
            if (_gpio_setup) return;

            ctrl = GpioController.GetDefault();
            if(null == ctrl)
            {
                throw new DeviceNotFoundException("No GPIO controller on this device");
            }

            /*
             If you get exceptions one the next couple of lines, particularly a message that the pin 
             is already in use, or inaccessible, then please ensure you add the line
                     <DeviceCapability Name="lowLevel" />
             to the capabilities section of package.appxmanifest in the calling app.
             */

            DatPin = ctrl.OpenPin(DAT);
            ClockPin = ctrl.OpenPin(CLK);
            ClockPin.SetDriveMode(GpioPinDriveMode.Output);
            DatPin.SetDriveMode(GpioPinDriveMode.Output);

            Show();

            _gpio_setup = true;
        }

        private void WritePixel(Pixel pixel)
        {
            var sendBright = (int)((31.0m * pixel.Brightness)) & 31;
            write_byte(Convert.ToByte(224 | sendBright));
            write_byte(Convert.ToByte(pixel.B));
            write_byte(Convert.ToByte(pixel.G));
            write_byte(Convert.ToByte(pixel.R));
        }

        private void write_byte(byte input)
        {
            int value;
            byte modded = Convert.ToByte(input);
            for(int count = 0; count < 8; count++)
            {
                value = modded & 128;
                DatPin.Write(value == 128 ? GpioPinValue.High : GpioPinValue.Low);
                ClockPin.Write(GpioPinValue.High);
                modded = Convert.ToByte((modded << 1) % 256);
                ClockPin.Write(GpioPinValue.Low);
            }
        }

        private void GetClockLock()
        {
            DatPin.Write(GpioPinValue.Low);
            for (int count = 0; count < 36; count++)
            {
                ClockPin.Write(GpioPinValue.High);
                ClockPin.Write(GpioPinValue.Low);
            }
        }

        private void ReleaseClockLock()
        {
            DatPin.Write(GpioPinValue.Low);
            for (int count = 0; count < 32; count++)
            {
                ClockPin.Write(GpioPinValue.High);
                ClockPin.Write(GpioPinValue.Low);
            }
        }
        
        /// <summary>
        /// Outputs the state of the pixels to the Blinkt hardware.
        /// </summary>
        public void Show()
        {
            GetClockLock();

            foreach (var pixel in pixels)
            {
                WritePixel(pixel);
            }

            ReleaseClockLock();
        }

        /// <summary>
        /// Sets the overall brightness of the pixels.
        /// </summary>
        /// <param name="bright"></param>
        public void SetBrightness(decimal bright)
        {
            if(bright < 0 || bright > 1)
            {
                throw new ArgumentOutOfRangeException("Brightness must be a value between 0 and 1");
            }
            for (int count = 0; count < NUM_PIXELS; count++)
            {
                pixels[count].Brightness = bright;
            }
        }

        /// <summary>
        /// Set values for all pixels in one call.
        /// </summary>
        /// <param name="r">Pixel red value</param>
        /// <param name="g">Pixel green value</param>
        /// <param name="b">Pixel blue value</param>
        /// <param name="bright">Pixel brightness</param>
        public void SetAllPixels(int r, int g, int b, decimal? bright = null)
        {
            for(int count = 1; count < NUM_PIXELS + 1; count++)
            {
                SetPixel(count, r, g, b, bright);
            }
        }

        /// <summary>
        /// Set an individual pixel value
        /// </summary>
        /// <param name="pixelNum">The index of the pixel to set (between 1 and the pixel count - NOT zero based!)</param>
        /// <param name="r">Pixel red value</param>
        /// <param name="g">Pixel green value</param>
        /// <param name="b">Pixel blue value</param>
        /// <param name="bright">pixel brightness</param>
        public void SetPixel(int pixelNum, int r, int g, int b, decimal? bright = null)
        {
            if(pixelNum < 1 || pixelNum > NUM_PIXELS)
            {
                throw new IndexOutOfRangeException("Invalid pixel number specified");
            }
            var pix = pixels[pixelNum - 1];
            pix.R = r;
            pix.G = g;
            pix.B = b;
            pix.Brightness = bright.HasValue ? bright.Value : BRIGHTNESS;
        }

        /// <summary>
        /// Clear all pixels.
        /// </summary>
        public void Clear()
        {
            SetAllPixels(0, 0, 0, null);
        }
    }
}
