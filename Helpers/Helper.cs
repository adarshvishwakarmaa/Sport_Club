using My_Sport_Club.Models.Domains;

namespace My_Sport_Club.Helpers
{
    public class Helper
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Global.BaseAddress);
            return Client;
        }
    }
}
