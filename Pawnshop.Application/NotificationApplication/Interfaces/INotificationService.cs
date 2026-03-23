namespace Pawnshop.Application.NotificationApplication.Interfaces;

public interface INotificationService
{
    Task NotifyReportReadyAsync(string userId, string reportUrl, string fileName, CancellationToken cancellationToken);
    Task NotifyErrorAsync(string userId, string message, CancellationToken cancellationToken);
}
