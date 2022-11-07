using System;
using Newtonsoft.Json;
using Serilog;

namespace BlueLight.Main {
    static class ExtendedLogic {
        // public static string TestOne (this string value) {
        //     return value;
        // }
        // public static object TestTwo (this object value) {
        //     return value;
        // }

        public static string DumpExt (this object value) {
            var output = JsonConvert.SerializeObject (value);

            var type1 = value.GetType ().ToString ();
            Log.Information (type1);
            Log.Information (output);
            return output;
        }
    }
}