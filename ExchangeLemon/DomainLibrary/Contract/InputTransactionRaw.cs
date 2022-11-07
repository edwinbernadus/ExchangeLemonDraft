using System;

namespace BlueLight.Main
{
    public class InputTransactionRaw
    {
        public string rate { get; set; }
        public string amount { get; set; }

        public string mode { get; set; }
        public string current_pair { get; set; }

        public Guid GuidInput { get; set; } = Guid.NewGuid();

    }
}