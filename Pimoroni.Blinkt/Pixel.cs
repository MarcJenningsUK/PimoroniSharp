using System;

namespace Pimoroni.Blinkt
{
    internal class Pixel
    {
        private int r = 0 , g = 0, b = 0;
        private decimal brightness = 0.0m;

        public int R
        {
            get
            {
                return r;
            }

            set
            {
                if(value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException("Red value must be between 0 and 255"); 
                }
                r = value;
            }
        }

        public int G
        {
            get
            {
                return g;
            }

            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException("Green value must be between 0 and 255");
                }
                g = value;
            }
        }

        public int B
        {
            get
            {
                return b;
            }

            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentOutOfRangeException("Blue value must be between 0 and 255");
                }
                b = value;
            }
        }

        public decimal Brightness
        {
            get
            {
                return brightness;
            }

            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("brightness value must be between 0 and 1");
                }
                brightness = value;
            }
        }
    }
}
