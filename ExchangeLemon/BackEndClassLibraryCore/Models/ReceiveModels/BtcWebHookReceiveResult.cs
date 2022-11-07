using System;
using System.Collections.Generic;

namespace ExchangeLemonCore.Models.ReceiveModels
{

    public class BtcWebHookReceiveResult
    {
        public int block_height { get; set; }
        public int block_index { get; set; }
        public string hash { get; set; }
        public List<string> addresses { get; set; }
        public int total { get; set; }
        public int fees { get; set; }
        public int size { get; set; }
        public string preference { get; set; }
        public string relayed_by { get; set; }
        public DateTime received { get; set; }
        public int ver { get; set; }
        public bool double_spend { get; set; }
        public int vin_sz { get; set; }
        public int vout_sz { get; set; }
        public int confirmations { get; set; }
        public List<Input> inputs { get; set; }
        public List<Output> outputs { get; set; }

       
    }
}