﻿using System.Net;
using System.Threading.Tasks;

namespace OpenpilotSdk.Hardware
{
    public class Comma2 : OpenpilotDevice
    {
        protected override string NotConnectedMessage { get; set; } = "No connection has been made to the Comma2";

        public virtual string OpenSettingsCommand { get; protected set; } = @"am start -a android.settings.SETTINGS";
        public virtual string CloseSettingsCommand { get; protected set; } = @"kill $(pgrep com.android.settings)";
        public override string DeviceName { get; protected set; } = @"Comma2";

        public Comma2(IPAddress hostAddress, bool isAuthenticated = true)
        {
            IsAuthenticated = isAuthenticated;
            IpAddress = hostAddress;
        }

        public async Task<bool> OpenSettingsAsync()
        {
            Connect();

            using (var command = SshClient.CreateCommand(OpenSettingsCommand))
            {
                var result = await Task.Factory.FromAsync(command.BeginExecute(), command.EndExecute).ConfigureAwait(false);
                var success = command.ExitStatus == 0;
                return success;
            }
        }

        public async Task<bool> CloseSettingsAsync()
        {
            Connect();

            using (var command = SshClient.CreateCommand(CloseSettingsCommand))
            {
                var result = await Task.Factory.FromAsync(command.BeginExecute(), command.EndExecute).ConfigureAwait(false);
                var success = command.ExitStatus == 0;
                return success;
            }
        }
    }
}