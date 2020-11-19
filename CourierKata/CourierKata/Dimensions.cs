using System;

namespace CourierKata
{
    public class Dimensions
    {
        public Dimensions(int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public int Length { get; }
        public int Width { get; }
        public int Height { get; }

        public bool IsInRange(int start, int end)
        {
            return Length >= start && Length < end &&
                   Width >= start && Width < end &&
                   Height >= start && Height < end;
        }

        public bool IsMinimum(int minimum)
        {
            return Length >= minimum &&
                   Width >= minimum &&
                   Height >= minimum;
        }
    }
}
