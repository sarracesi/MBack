using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace Mobilis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessageAsync([FromBody] Notification request)
        {
            // Save the notification to the database
            _context.Notifications.Add(request);
            await _context.SaveChangesAsync();

            // Send FCM Notification
            await SendFcmNotification(request);

            return Ok("Message sent successfully!");
        }

        private async Task SendFcmNotification(Notification notification)
        {
            // Assuming you have a user-specific registration token
            var registrationToken = "<user's FCM registration token>";

            var firebaseNotification = new FirebaseAdmin.Messaging.Notification
            {
                Title = notification.Title,
                Body = notification.Body
            };

            var message = new Message
            {
                Notification = firebaseNotification,
                Token = registrationToken
            };

            try
            {
                var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"Successfully sent message: {result}");
            }
            catch (FirebaseException ex)
            {
                Console.Error.WriteLine($"Error sending message: {ex.Message}");
            }
        }
        //notification to specific users according to age, wilaya, city, gender.
        //feature: programme notification to be sent in a choosed date and time
    }
}
