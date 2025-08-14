using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace H.Hubs
{
	[IgnoreAntiforgeryToken]
	public class MachineManagerHub : Hub
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public MachineManagerHub(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public static int TotalMCView { get; set; } = 0;

		public async Task NewMachineView()
		{
			TotalMCView++;
		}

		public async Task SendMessageToCurrentUser(string message)
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			await Clients.All.SendAsync("notify", message + "-" + userId);
		}
	}
}