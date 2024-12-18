using Microsoft.AspNetCore.SignalR;

namespace Restaurant_API
{
	public class HubContext : Hub
	{
		public async Task SendOrder(string message)
		{
			await Clients.All.SendAsync("ReceiveOrder", message);
		}
	}
	
}
