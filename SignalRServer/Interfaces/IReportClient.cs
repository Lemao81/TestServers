using System.Threading.Tasks;

namespace SignalRServer.Interfaces
{
    public interface IReportClient
    {
        Task UpdateClientId(string clientId);
        Task OpenReport(string reportId);
    }
}