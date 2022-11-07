#region
using System.Collections.Generic;

#endregion

namespace BlockCypher.Objects
{
    public class InputInquiry
    {
        public string prev_hash { get; set; }
        public int output_index { get; set; }
        public string script { get; set; }
        public int output_value { get; set; }
        public object sequence { get; set; }
        public List<string> addresses { get; set; }
        public string script_type { get; set; }
        public int age { get; set; }
    }
}
