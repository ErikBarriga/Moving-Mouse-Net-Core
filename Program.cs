using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MouseCursorMovement
{
    class Program
    {
        static void Main(string[] args)
        {
            /// With Timer
            /* var timer = new Timer();
            timer.Interval = (int)(TimeSpan.TicksPerMinute * .10 / TimeSpan.TicksPerMillisecond);//4 minutes
            timer.Elapsed += (sender, args) =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (GetCursorPos(out Point mouse))
                    {
                        mouse.X = (mouse.X < 101) ? 300 : 100;
                        mouse.Y = (mouse.Y < 101) ? 300 : 100;
                        SetCursorPos(mouse);
                        Console.WriteLine($"Mouse X:{mouse.X} Y:{mouse.Y}");
                    }
                }
                //else {// Linux and OSX? }
            };
            timer.Start();
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                System.Threading.Thread.Sleep(100);
            }*/

            ///With Thread
            var active = true;
            var keyCode = ConsoleKey.LeftArrow;
            var keyCodeAlt = ConsoleKey.RightArrow;
            var alt = true;

            Console.WriteLine("Press escape to stop...");
            var task = Task.Run(()=> {
                if (WinUser32Dll.IsWindows())
                {
                    while (active)
                    {
                        var consoleText = "";
                        try        
                        {
                            if (WinUser32Dll.GetCursorPos(out Point mouse))
                            {
                                if (mouse.X > 1000)
                                    alt = true;
                                else if (mouse.X <= 1)
                                    alt = false;

                                mouse.X += (alt) switch
                                {
                                    true => -1,
                                    _ => 1
                                };
                                
                                //mouse.Y = (mouse.Y < 101) ? 300 : 100;
                                WinUser32Dll.SetCursorPos(mouse);
                                consoleText += $" 'mouse x:{mouse.X}'";
                            }
                        }
                        catch {
                            consoleText += $" 'mouse x:ERROR'";
                        }
                        try
                        {
                            WinUser32Dll.SendKey((int)(alt ? keyCode : keyCodeAlt));
                            consoleText += $" 'key :{(int)(alt ? keyCode : keyCodeAlt)}'";
                        }
                        catch
                        {
                            consoleText += " 'key :ERROR'";
                        }

                        Console.WriteLine($"{consoleText} 'time:{DateTime.Now.ToLongTimeString()}'");

                        for ( var i = 0; i < 1  && active; i++)
                        {
                            System.Threading.Thread.Sleep(1000); //12 Seconds (1 second in a bucle
                        }
                    }
                }
                active = false;//
            });
             var key = Console.ReadKey(true); 
            while (key.Key == keyCode || key.Key == keyCodeAlt)
            {
                 key = Console.ReadKey(true);
              }
            if (active) {
                active = false;
                task.Wait();
            }
        }
    }
}
