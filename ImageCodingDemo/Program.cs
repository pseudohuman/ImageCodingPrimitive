
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
                bitmap.Drawline2(0, 0, 199, 0, Color.Blue);
                bitmap.Drawline2(199, 0, 199, 199, Color.Cyan);
                bitmap.Drawline2(199, 199, 0, 199, Color.Red);
                bitmap.Drawline2(0, 199, 0, 0, Color.Green);
                bitmap.Drawline2(0, 0, 199, 199, Color.Green);
                bitmap.Drawline2(0,199, 0, 199, Color.Green);
                bitmap.DrawCircle2(100, 100, 10, Color.Red);
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
        static void Drawline2(this Bitmap bitmap, int StartX, int StartY, int EndX, int EndY, Color color)
        {
            bool steep = Math.Abs(EndY - StartY) > Math.Abs(EndX - StartX);
            if(steep)
            {
                int temp;
                temp = StartY;
                StartY = StartX;
                StartX = temp;

                temp = EndY;
                EndY = EndX;
                EndX = temp;
            }
            if (StartX > EndX)
            {
                int temp;
                temp = StartX;
                StartX = EndX;
                EndX = temp;

                temp = StartY;
                StartY = EndY;
                EndX = temp;


            }
            int dx = EndX - StartX;
            int dy = Math.Abs(EndY - StartY);
          

            int err = dx / 2;
   
            int ystep=(StartY<EndY)?1:-1;
            int y = StartY;

            for(int x = StartX;x<=EndX;x++)
            {
                bitmap.SetPixel(steep ? y : x, steep ? x : y,color);
                err-= dy;
                if (err < 0)
                {
                    y += ystep;
                    err += dx;
                }
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
       
        static void DrawCircle2(this Bitmap bitmap, int centerX, int centerY, int radius, Color color){
            int x = radius;
            int y = 0;
            int err = 1 - x;
            while (x >= y)
            {
                bitmap.SetPixel(x + centerX, y + centerY,color);
                bitmap.SetPixel(y + centerX, x + centerY, color);
                bitmap.SetPixel(-x + centerX, y + centerY, color);
                bitmap.SetPixel(-y + centerX, x + centerY, color);
                bitmap.SetPixel(x + centerX, -y + centerY, color);
                bitmap.SetPixel(y + centerX, -x + centerY, color);
                bitmap.SetPixel(-x + centerX, y + centerY, color);
                bitmap.SetPixel(-y + centerX, x + centerY, color);
                bitmap.SetPixel(-x + centerX, -y + centerY, color);
                bitmap.SetPixel(-y + centerX, -x + centerY, color);
                y++;
                if (err < 0)
                {
                    err += 2 * y + 1;
                }
                else
                {
                    x--;
                    err += 2 * (y - x + 1);
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
