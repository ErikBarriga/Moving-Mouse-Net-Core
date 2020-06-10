using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MouseCursorMovement
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(Point lpPoint);

        static void Main(string[] args)
        {
            /// With Timer
            // var timer = new Timer();
            //timer.Interval = (int)(TimeSpan.TicksPerMinute * .10 / TimeSpan.TicksPerMillisecond);//4 minutes
            //timer.Elapsed += (sender, args) =>
            //{
            //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    {
            //        if (GetCursorPos(out Point mouse))
            //        {
            //            mouse.X = (mouse.X < 101) ? 300 : 100;
            //            mouse.Y = (mouse.Y < 101) ? 300 : 100;
            //            SetCursorPos(mouse);
            //            Console.WriteLine($"Mouse X:{mouse.X} Y:{mouse.Y}");
            //        }
            //    }
            //    else {// How to do on Linux and OSX? }
            //};
            //timer.Start();
            //while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            //{
            //    System.Threading.Thread.Sleep(100);
            //}
            var active = true;
            Console.WriteLine("Press escape to stop...");
            var task = Task.Run(()=> {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    while (active)
                    {
                        if (GetCursorPos(out Point mouse))
                        {
                            mouse.X = (mouse.X < 101) ? 300 : 100;
                            //mouse.Y = (mouse.Y < 101) ? 300 : 100;
                            SetCursorPos(mouse);
                            Console.WriteLine($"mouse x:{mouse.X} time:{DateTime.Now.ToLongTimeString()}");
                        }
                        for (var i = 0; i < 12 && active; i++)
                        {
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                }
            });
            Console.ReadKey();
            active = false;
            task.Wait();
        }
    }
}
