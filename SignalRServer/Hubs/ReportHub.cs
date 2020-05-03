using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Constants;
using SignalRServer.Interfaces;

namespace SignalRServer.Hubs
{
    public class ReportHub : Hub<IReportClient>
    {
        public async Task OpenReportAsync(string connectionId, Guid reportId)
        {
            await Clients.Client(connectionId).OpenReport(reportId.ToString());
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, SignalRGroups.RadioReportClients);
            await Clients.Caller.UpdateClientId(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, SignalRGroups.RadioReportClients);
            await base.OnDisconnectedAsync(exception);
        }
    }
}