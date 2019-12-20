using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitor.WorkerService
{
    public static class Util
    {
        public static string GetLogInformation()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Request sent.");
            stringBuilder.AppendLine($"Time: {DateTimeOffset.Now}");

            return stringBuilder.ToString();
        }
    }
}
