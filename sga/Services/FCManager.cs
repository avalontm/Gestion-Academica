using FCM.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace sga
{
    public static class FCManager
    {
        public static async Task<bool> Send(string title, string body)
        {
            string serverKey = Program.Configuration["FireBase:token"];

            using (var sender = new Sender(serverKey))
            {
                var message = new Message
                {
                    To = "/topics/general",
                    Priority = Priority.High,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    }
                };

                var result = await sender.SendAsync(message);

                if (result.MessageResponse.Failure == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public static async Task<bool> Send(List<string> registrationIds, string title, string  body)
        {
            string serverKey = Program.Configuration["FireBase:token"];

            using (var sender = new Sender(serverKey))
            {
                var message = new Message
                {
                    RegistrationIds = registrationIds,
                    Priority = Priority.High,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    }
                };

                var result = await sender.SendAsync(message);

                if (result.MessageResponse.Success == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
