using Microsoft.AspNetCore.SignalR;
using PhoneBook.Dto;
using System.Collections.Generic;

namespace PhoneBook.Extensions.HubConfig
{
    public partial class HubConfigExtension
    {
        public async Task GetOnlineUsers()
        {
            Guid currentUserId = context.Connections.Where(c => c.SignalRId == Context.ConnectionId)
                                    .Select(s => s.UserId).SingleOrDefault();
            var onlineUserList = context.Connections.Where(c => c.UserId != currentUserId)
                                    .Select(s => new ConnectedUser(s.UserId, context.Users
                                    .Where(u => u.Id == s.UserId).Select(n => n.Name).SingleOrDefault(), s.SignalRId)).ToList();

            List<ConnectedUser> users = new List<ConnectedUser>();
            foreach (var item in onlineUserList)
            {
                var addUser = new ConnectedUser(
                    item.Id, 
                    item.Name,
                    item.ConnectionId);
                users.Add(addUser);
            }

            //List <ConnectedUser> onlineUsers = context.Connections.Where(c => c.UserId != currentUserId)
            //                        .Select(s => new ConnectedUser(s.UserId, context.Users.Where(u => u.Id == s.UserId)
            //                            .Select(s => s.Name).SingleOrDefault(), s.SignalRId)).ToList();

            await Clients.Caller.SendAsync("GetOnlineUsersResponse", users); //onlineUsers
        }

        public async Task SendMessage(string connId, string message)
        {
            await Clients.Client(connId).SendAsync("SendMesssageResponse", Context.ConnectionId, message);
        }
    }
}
