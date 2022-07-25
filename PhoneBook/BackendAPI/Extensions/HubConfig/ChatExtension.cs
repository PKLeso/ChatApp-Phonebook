using Microsoft.AspNetCore.SignalR;
using PhoneBook.Dto;

namespace PhoneBook.Extensions.HubConfig
{
    public partial class HubConfigExtension
    {
        public async Task GetOnlineUsers()
        {
            Guid currentUserId = context.Connections.Where(c => c.SignalRId == Context.ConnectionId)
                                    .Select(s => s.UserId).SingleOrDefault();
            List<ConnectedUser> onlineUsers = context.Connections.Where(c => c.UserId != currentUserId)
                                    .Select(s => new ConnectedUser(s.UserId, context.Users.Where(u => u.Id == s.UserId)
                                        .Select(s => s.Name).SingleOrDefault(), s.SignalRId)).ToList();

            await Clients.Caller.SendAsync("GetOnlineUsersResponse", onlineUsers);
        }

        public async Task SendMessage(string connId, string message)
        {
            await Clients.Client(connId).SendAsync("SendMesssageResponse", Context.ConnectionId, message);
        }
    }
}
