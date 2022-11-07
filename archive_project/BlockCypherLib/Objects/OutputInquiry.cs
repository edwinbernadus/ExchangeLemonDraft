#region
using System.Collections.Generic;

#endregion

namespace BlockCypher.Objects
{
    public class OutputInquiry
    {
        public int value { get; set; }
        public string script { get; set; }
        public List<string> addresses { get; set; }
        public string script_type { get; set; }
    }
}
