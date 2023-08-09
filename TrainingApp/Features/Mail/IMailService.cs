namespace TrainingApp.Features.Mail;

public interface IMailService
{
    Task SendPasswordResetEmail(string recipient, string username, string userId, string resetToken);
}