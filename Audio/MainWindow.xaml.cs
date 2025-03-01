using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Windows.Media.Control;

namespace Audio;

public partial class MainWindow : Window
{
    private DispatcherTimer timer;
    private DispatcherTimer playTimer;
    private GlobalSystemMediaTransportControlsSessionManager smtc;
    private AudioVolumeController audioController;
    private int Position;

    public MainWindow()
    {
        InitializeComponent();
        Items();
        UpdateVolume();
        UpdateMarker();
        PlayOn();
    }

    private async Task Items()
    {
        audioController = new AudioVolumeController();
        smtc = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
        timer = new DispatcherTimer();
        this.Topmost = false;
        await smtc.GetCurrentSession().TryChangePlaybackPositionAsync((long)TimeSpan.FromSeconds(0).Ticks);
        Position = (int) smtc.GetCurrentSession().GetTimelineProperties().Position.TotalSeconds;
    }

    private async void UpdateMarker()
    {
        timer.Interval = TimeSpan.FromSeconds(0.5);
        timer.Tick += async (s, e) =>
        {
            await LoadMediaInfo();
            UpdateProgreesSlider();
        };
        timer.Start();
    }

    private async void PlayOn()
    {
        playTimer = new DispatcherTimer();
        playTimer.Interval = TimeSpan.FromSeconds(1);
        playTimer.Tick += async (s, e) =>
        {
            await PlayPosition();
        };
        playTimer.Start();
    }

    private void UpdateVolume()
    {
        VolumeSlider.Value = audioController.GetStartVolume("chrome");
        VolumeLevel.Text = audioController.GetStartVolume("chrome").ToString();
    }

    private async void Play_Click(object sender, RoutedEventArgs e)
    {
        if (smtc == null) return;
        var session = smtc.GetCurrentSession();

        var playInfo = session.GetPlaybackInfo();

        if (playInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
        {
            await session.TryPauseAsync(); 
            StartAndStop.Source = new BitmapImage(new Uri("Images/Start.png", UriKind.Relative));
        }
        else
        {
            await session.TryPlayAsync();
            StartAndStop.Source = new BitmapImage(new Uri("Images/Stop.png", UriKind.Relative));
        }
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ToFix_Click(object sender, RoutedEventArgs e)
    {
        this.Topmost = !this.Topmost;
        if (this.Topmost)
        {
            FixImage.Source = new BitmapImage(new Uri("Images/FixOn.png", UriKind.Relative));       
        }
        else
        {
            FixImage.Source = new BitmapImage(new Uri("Images/FixOff.png", UriKind.Relative));
        }
    }

    private async void NextButton_Click(object sender, RoutedEventArgs e)
    {
        if (smtc == null) return;

        var session = smtc.GetCurrentSession();

        if (session == null) return;

        await session.TrySkipNextAsync();

        NextTrack.IsEnabled = false;
        await Task.Delay(500);
        NextTrack.IsEnabled = true;
    }


    private async void ProgressSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (smtc == null) return;
        var session = smtc.GetCurrentSession();

        Position = (int) ProgressSlider.Value;

        await session.TryChangePlaybackPositionAsync((long)TimeSpan.FromSeconds(ProgressSlider.Value).Ticks);
    }

    private async void VolumeSlider_VolumeChanger(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (smtc == null) return;

        float volume = (float)(e.NewValue / 100.0);

        VolumeLevel.Text = e.NewValue.ToString();

        await audioController.FindApplicationVolume("chrome", volume);
    }

    private async void PrevButton_Click(object sender, RoutedEventArgs e)
    {
        if (smtc == null) return;

        var session = smtc.GetCurrentSession();

        if (session == null) return;

        await session.TrySkipPreviousAsync();

        PrevTrack.IsEnabled = false;
        await Task.Delay(500);
        PrevTrack.IsEnabled = true;
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private async Task LoadMediaInfo()
    {
        try
        {
            var session = smtc.GetCurrentSession();

            if (session != null)
            {
                var mediaProperties = await session.TryGetMediaPropertiesAsync();
                // изменяет имя Артиста
                var artist = mediaProperties.Artist;
                SoundArtist.Text = artist == null ? "Неизвестно" : artist;

                // изменяет название песни
                var name = mediaProperties.Title;
                SoundName.Text = name == null ? "Неизвестно" : name;

                // берет статус игры
                var playInfo = session.GetPlaybackInfo();
                string status = playInfo.PlaybackStatus.ToString();

                if (playInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
                { 
                    StartAndStop.Source = new BitmapImage(new Uri("Images/Stop.png", UriKind.Relative));
                }
                else
                {
                    StartAndStop.Source = new BitmapImage(new Uri("Images/Start.png", UriKind.Relative));
                }

                var thumbnail = mediaProperties.Thumbnail;
                if (thumbnail != null)
                {
                    using (var stream = await thumbnail.OpenReadAsync())
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = stream.AsStream();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        SoundImage.Source = bitmap;
                        Dispatcher.Invoke(() =>
                        {
                            BackgroundImage.Source = bitmap;
                        });
                    }
                }
            }
            else
            {
                SoundName.Text = "Нет песни";
                SoundArtist.Text = "";
                SoundImage.Source = new BitmapImage(new Uri("Images/LoadImage.png", UriKind.Relative));
            }
        }
        catch (Exception ex)
        {
            SoundName.Text = "Ошибка";
            SoundArtist.Text = ex.Message;
            SoundImage.Source = new BitmapImage(new Uri("Images/LoadImage.png", UriKind.Relative));
        }
    }

    private async void UpdateProgreesSlider()
    {
        if (smtc == null)return;

        var session = smtc.GetCurrentSession();

        if (session == null) return;

        var ts = session.GetTimelineProperties();

        if (ts.EndTime == TimeSpan.Zero || ts.Position == TimeSpan.Zero) return;

        Dispatcher.Invoke(() =>
        {
            ProgressSlider.Maximum = ts.EndTime.TotalSeconds;
            ProgressSlider.Value = Position;

            StartPositionText.Text = $"{Position/60:D2}:{Position%60:D2}";
            EndPositionText.Text = $"{(int)ts.EndTime.TotalMinutes:D2}:{ts.EndTime.Seconds:D2}";
        });
    }  

    private async Task PlayPosition()
    {
        if (smtc == null) return;

        var session = smtc.GetCurrentSession();

        if (session == null) return;

        var playInfo = session.GetPlaybackInfo();

        if (playInfo == null) return;

        session.MediaPropertiesChanged += (s, e) =>
        {
            Position = 0;
        };

        if (playInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
        {
            Position++;
        }
    }

}