using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace Audio
{
    class AudioVolumeController
    {
        private MMDeviceEnumerator deviceEnum;
        private MMDevice device;

        public AudioVolumeController()
        {
            deviceEnum = new MMDeviceEnumerator();
            device = deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
    }

        public async Task FindApplicationVolume(string processName, float volume)
        {
            for (int i = 0; i < device.AudioSessionManager.Sessions.Count; i++)
            {
                var session = device.AudioSessionManager.Sessions[i];
                if (session.GetProcessID != 0)
                {
                    var process = Process.GetProcessById((int) (session.GetProcessID));
                    if (process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase))
                    {
                        session.SimpleAudioVolume.Volume = volume;
                        break;
                    }
                }
            }
        }

        public float GetStartVolume(string processName)
        {
            for (int i = 0; i < device.AudioSessionManager.Sessions.Count; i++)
            {
                var session = device.AudioSessionManager.Sessions[i];
                if (session.GetProcessID != 0)
                {
                    var process = Process.GetProcessById((int)(session.GetProcessID));
                    if (process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase))
                    {
                        return session.SimpleAudioVolume.Volume * 100;
                    }
                }
            }
            return 0.0f;
        }
        public string GetActiveAudioDevice()
        {
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            var activeDevice = devices
                .OrderByDescending(d => d.AudioMeterInformation.MasterPeakValue)
                .FirstOrDefault();

            return activeDevice != null ? activeDevice.FriendlyName : "Не найдено";
        }
    }
}
