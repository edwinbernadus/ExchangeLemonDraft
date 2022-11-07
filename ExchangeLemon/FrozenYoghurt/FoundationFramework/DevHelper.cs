using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main {
    public class DevHelper {
        public static string Log (string message, string module) {
            var output = $"[{module}]: {message}";
            Serilog.Log.Information (output);
            return output;
        }

        // public static async Task<string> ExecuteAsync (string message, string module) {
        //     await Task.Delay (0);
        //     var output = $"[{module}]: {message}";
        //     Log.Information (output);
        //     return output;
        // }

        public static bool IsDebug () {
            var isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            return isDebug;
        }
    }
}