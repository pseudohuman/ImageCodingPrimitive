
using System.Drawing;
using System;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ImageCodingDemo
{
    static class Program
    {
        const string filename = @"D:\line.png";
        static void Main(string[] args)
        {
            using (Bitmap bitmap = new Bitmap(200, 200))
            {
                //bitmap.Drawline(0, 0, 199, 0, Color.Blue);
                //bitmap.Drawline(199, 0, 199, 199, Color.Cyan);
                //bitmap.Drawline(199, 199, 0, 199, Color.Red);
                //bitmap.Drawline(0, 199, 0, 0, Color.Green);
                //bitmap.Drawline(0, 0, 199, 199, Color.Green);
                //bitmap.Drawline(0,199, 0, 199, Color.Green);
                bitmap.DrawCircle(100, 100, 10, Color.Red);
                bitmap.DrawEllipse(50,50,25,10,Color.Aqua);
                bitmap.Polynome(10, 10, Color.Red);
                bitmap.Save(filename, ImageFormat.Png);
                

            }
            RunViewer();
        }
        static void Drawline(this Bitmap bitmap,int StartX, int StartY, int EndX, int EndY,Color color)
        {
            
            int dx = EndX - StartX;
            int dy = EndY - StartY;
            int len = Math.Abs(dx) + Math.Abs(dy);
            double ndx = (double)dx / len;
            double ndy = (double)dy / len;
            for (int i=0;i<=len;i++)
            {
                int x = StartX + (int)Math.Round(i * ndx);
                int y = StartY + (int)Math.Round(i * ndy);
                bitmap.SetPixel(x,y,color);
            }
           
        }
        static void DrawlineD(this Bitmap bitmap, int StartX, int StartY, int EndX, int EndY, Color color)
        {
         
            int dx = EndX - StartX;
            int dy = EndY - StartY;
            int len = Math.Abs(dx) + Math.Abs(dy);
            double ndx = (double)dx / len;
            double ndy = (double)dy / len;
            for (int i = 0; i <= len; i++)
            {
                int x = StartX + (int)Math.Round(i * ndx);
                int y = StartY + (int)Math.Round(i * ndy);
                bitmap.SetPixel(x, y, color);
            }

        }
        static void DrawCircle(this Bitmap bitmap, int centerX, int centerY, int radius, Color color)
        {

            for (double i = 0; i < Math.PI*2; i += 0.1)
            {
                int x = (int)Math.Round((Math.Sin(i))* (radius/2) + centerX);
                int y = (int)Math.Round((Math.Cos(i))* (radius/2) + centerY);
                if (x < bitmap.Width && y < bitmap.Height)
                {
                    bitmap.SetPixel(x, y, color);
                    Console.Write($" {i}");
                }
            }

        }
        static void DrawEllipse(this Bitmap bitmap, int centerX, int centerY, int width, int height, Color color)
        {

            for (double i = 0; i < Math.PI * 2; i += 0.1)
            {
                int x = (int)Math.Round((Math.Sin(i)) * (width / 2) + centerX);
                int y = (int)Math.Round((Math.Cos(i)) * (height / 2) + centerY);
                if (x < bitmap.Width && y < bitmap.Height)
                {
                    bitmap.SetPixel(x, y, color);
                    Console.Write($" {i}");
                }
            }

        }
        static void Polynome(this Bitmap bitmap, int a, int b, Color color)
        {
            int lenght = bitmap.Width* bitmap.Width + bitmap.Height* bitmap.Height;
            for (double i = 0; i < Math.Sqrt(lenght); i=i+0.01)
            {
                int y = (int)Math.Round(Math.Pow(i ,2)/a + b*i);
                if (i<bitmap.Width && y<bitmap.Height) {
                    bitmap.SetPixel((int)Math.Round(Math.Abs(i)), bitmap.Height-y-1, color);
                    Console.WriteLine(y);
                }
            }
            

        }

        static void RunViewer()
        {
            Process process=new Process();
            process.StartInfo.FileName = @"C:\Windows\System32\rundll32.exe";
            process.StartInfo.Arguments = $"\"C:\\Program Files\\Windows Photo Viewer\\PhotoViewer.dll\" ImageView_Fullscreen {filename}";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }
    }
}
