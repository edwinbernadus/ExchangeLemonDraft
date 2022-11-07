using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    public interface IRemittanceOutgoingValidatorService
    {
        
        Task Execute(long pendingTransferId,bool isManual);
        //bool IsManual { get; set; }
    }
}