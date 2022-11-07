using ExchangeLemonCore;

namespace BlueLight.Main
{

    public class WorkingFolder //: WorkingFolderInput
    {
        internal bool includeLog { get; set; }

        internal LogSession sessionLogStart;
        internal ReplayValidationItem beforeItems;
        //internal InputTransaction inputTransaction;
        //internal string ModuleName { get; set; } = "no_init";

        internal InputTransaction inputTransaction { get; set; }
        //internal InputTransaction inputTransaction 
        //{
        //    get
        //    {
        //        var output = input.ConvertTo();
        //        return output;
        //    }
        //}

        internal string ModuleName
        {
            get
            {
                var moduleName = inputTransaction?.GetModuleName() ?? "no_init";
                return moduleName;
            }
        }
        internal UserProfile UserProfile { get; set; }
        internal Order CreatedOrder { get; set; }
    }
}