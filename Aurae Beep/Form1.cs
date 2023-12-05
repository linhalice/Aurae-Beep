using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using System.Diagnostics;
using System.Media;
using System.Reflection;

namespace Aurae_Beep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveResourceToFile("Ring.mp3");



            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    
                    for (int i = 0; i < 3; i++)
                    {
                        PlayAudio("Ring.mp3");
                    }

                    int secondsInAnHour = 3600;
                    Console.WriteLine("Bắt đầu đếm ngược từng giây trong một giờ:");

                    for (int i = secondsInAnHour; i > 0; i--)
                    {
                        this.Invoke(new Action(() =>
                        {
                            Console.WriteLine($"Còn lại {i} giây.");
                            label1.Text = $"Còn lại {i} giây.";
                        }));
                        Thread.Sleep(1000); // Đợi 1 giây trước khi đếm tiếp.
                    }

                    Console.WriteLine("Kết thúc đếm ngược.");
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        static void PlayAudio(string audioFilePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(audioFilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi phát âm thanh: {ex.Message}");
            }
        }

        public static void SaveResourceToFile(string filename)
        {
            byte[] resourceData = Aurae_Beep.Resource1.Ring;
            File.WriteAllBytes(filename, resourceData);
        }
    }
}