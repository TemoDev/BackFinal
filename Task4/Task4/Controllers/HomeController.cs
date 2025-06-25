//HomeController.cs
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task Talk()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var sock = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await DoTalking(sock);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        //respond to the clients websocket request
        private async Task DoTalking(WebSocket sock)
        {
            var buffer = new byte[1024 * 4];
            var result = await sock.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            var receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            var reversedText = new string(receivedText.Reverse().ToArray());

            var sendBuffer = System.Text.Encoding.UTF8.GetBytes(reversedText);
            await sock.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}