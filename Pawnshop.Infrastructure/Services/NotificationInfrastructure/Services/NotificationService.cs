using Microsoft.AspNetCore.SignalR;
using Pawnshop.Application.NotificationApplication.Interfaces;
using Pawnshop.Infrastructure.Services.NotificationInfrastructure.Hubs;

namespace Pawnshop.Infrastructure.Services.NotificationInfrastructure.Services;

public sealed class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyReportReadyAsync(string userId, string reportUrl, string fileName, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveReportReady", new { reportUrl, fileName }, cancellationToken);
        await _hubContext.Clients.Group(userId).SendAsync("ReceiveReportReady", new { reportUrl, fileName }, cancellationToken);
    }

    public async Task NotifyErrorAsync(string userId, string message, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotificationError", new { message }, cancellationToken);
        await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotificationError", new { message }, cancellationToken);
    }
}
