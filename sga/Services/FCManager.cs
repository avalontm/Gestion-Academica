using FCM.Net;

namespace sga
{
    public static class FCManager
    {
        public static async Task<bool> Send(List<string> registrationIds, string title, string  body)
        {
            string serverKey = Program.Configuration["FireBase:token"];

            using (var sender = new Sender(serverKey))
            {
                var message = new Message
                {
                    RegistrationIds = registrationIds,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    }
                };

                var result = await sender.SendAsync(message);
                Console.WriteLine($"[Notificacion Push] {result.StatusCode} | {registrationIds.Count}");
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
