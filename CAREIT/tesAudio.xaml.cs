using System.Windows;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;


namespace CAREIT
{
    /// <summary>
    /// Interaction logic for tesAudio.xaml
    /// </summary>
    public partial class tesAudio : Window
    {
        MediaPlayer media = new MediaPlayer();
        String path;
        String title;
        public tesAudio()
        {
            InitializeComponent();
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog() == true)
            {
                media.Open(new Uri(op.FileName));
            }

            path = op.FileName;
            title = Path.GetFileName(path);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_tick;
            timer.Start();

        }

        void timer_tick(object sender, EventArgs e)
        {
            if(media.Source != null)
            {
                judul.Content = title;
                text.Content = String.Format("{0} / {1}",
                    media.Position.ToString(@"mm\:ss"),
                    media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
            {
                judul.Content = "Tidak ada file";
            }
        }
    }
}
