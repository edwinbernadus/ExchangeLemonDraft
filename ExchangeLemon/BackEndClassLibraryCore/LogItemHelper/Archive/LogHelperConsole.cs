//using System.Threading.Tasks;
//using BlueLight.Main;
//using Serilog;
//using Serilog.Core;

//namespace BlueLight.Main {

//    public static class LogHelperConsole {
//        static Logger log;

//        static Logger GenerateLogger () {
//            if (log == null) {

//                log = new LoggerConfiguration ()
//                    .WriteTo.Console ()
//                    .CreateLogger ();
//            }
//            return log;
//        }

//        //public static void SaveLogError (string v) {
//        //    var logger = GenerateLogger ();
//        //    logger.Error (v);
//        //}

//        //public static void SaveLog (string v) {

//        //    var logger = GenerateLogger ();
//        //    logger.Information (v);
//        //}

//        //public static void SaveLogWarning (string v) {

//        //    var logger = GenerateLogger ();
//        //    logger.Warning (v);
//        //}

//        //public static void SaveLogWarning (string v) {

//        //    var logger = GenerateLogger ();
//        //    logger.Warning (v);
//        //}

//    }
//}