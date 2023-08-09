using MailKit.Net.Smtp;
using MimeKit;

namespace TrainingApp.Features.Mail;

public class MailService: IMailService
{
    public async Task SendPasswordResetEmail(string recipient, string username, string userId, string resetToken)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Eurostep Basketball", Environment.GetEnvironmentVariable("SMTP_SENDER")));
        message.To.Add(new MailboxAddress(username, recipient));
        message.Subject = "Password Reset Requested";

        var builder = new BodyBuilder();
        string redirectURL = $"https://milosost.github.io/trainingApp-redirect/?token={resetToken}&userId={userId}";
        builder.HtmlBody = string.Format (@"
        <div>
		    <h1> Hello {0}</h1>

		    <p>
			    A request has been recieved to change the password for your account. To reset your password, open the link below in the app.
		    </p>

            <a href='{1}'>Reset Password</a>
        </div>
        ", username, redirectURL);

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(Environment.GetEnvironmentVariable("SMTP_HOST"), 465, true);
        await client.AuthenticateAsync(Environment.GetEnvironmentVariable("SMTP_SENDER"),Environment.GetEnvironmentVariable("SMTP_PASSWORD"));

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}