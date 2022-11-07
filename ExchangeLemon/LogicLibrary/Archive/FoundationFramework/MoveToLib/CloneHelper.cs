//using AutoMapper;
//using Newtonsoft.Json;

//namespace BlueLight.Main
//{
//    public class CloneHelper
//    {

//        public static T2 Generate<T1, T2>(T1 item1)
//        {
//            var config = new MapperConfiguration(cfg => cfg.CreateMap<T1, T2>());
//            var w = JsonConvert.SerializeObject(item1);
//            var w2 = JsonConvert.DeserializeObject<T2>(w);
//            return w2;
//        }



//        static T2 ObsoleteVer1Generate<T1, T2>(T1 item1)
//        {
//            var w = JsonConvert.SerializeObject(item1);
//            var w2 = JsonConvert.DeserializeObject<T2>(w);
//            return w2;
//        }


//        static T1 ObsoleteVer1Generate<T1>(object item1)
//        {
//            var w = JsonConvert.SerializeObject(item1);
//            var w2 = JsonConvert.DeserializeObject<T1>(w);
//            return w2;
//        }

//        void SampleTest()
//        {

//            //var item1 = new CurrencyPair();
//            //var w2 = CloneHelper.Generate<VM_CurrencyPair>(item1);
//            //var w3 = CloneHelper.Generate<CurrencyPair,VM_CurrencyPair>(item1);
//        }
//    }
//}