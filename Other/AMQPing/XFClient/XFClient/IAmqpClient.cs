using System.Threading.Tasks;

namespace XFClient
{
    public interface IAmqpClient
    {
        Task<bool> Connect();

    }
}
