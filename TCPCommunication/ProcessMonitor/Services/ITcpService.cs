using System.Threading.Tasks;

namespace ProcessMonitor.Services
{
    public interface ITcpService
    {
        byte[] GetReceivedMessages();
        Task SendMessage(byte message);

        byte[] GetReceivedMessagesByte2();
        byte[] GetReceivedMessagesByte1();
    }
}