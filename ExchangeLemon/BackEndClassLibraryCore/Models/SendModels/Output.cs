using System.Collections.Generic;

namespace ExchangeLemonCore.Models.SendModels
{
    public class Output
    {
        public int value { get; set; }
        public string script { get; set; }
        public string spent_by { get; set; }
        public List<string> addresses { get; set; }
        public string script_type { get; set; }
    }
}