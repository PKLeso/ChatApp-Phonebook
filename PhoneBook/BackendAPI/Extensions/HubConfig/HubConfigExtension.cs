using Microsoft.AspNetCore.SignalR;
using PhoneBook.Data;
using PhoneBook.Dto;
using PhoneBook.Models;

namespace PhoneBook.Extensions.HubConfig
{
    public partial class HubConfigExtension : Hub
    {
        private readonly PhonebookDbContext context;
        private List<string> users = new List<string>();

        public HubConfigExtension(PhonebookDbContext dbContext)
        {
            context = dbContext;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Guid currentUserId = context.Connections.Where(c => c.SignalRId == Context.ConnectionId)
                                    .Select(s => s.UserId).FirstOrDefault();
            context.Connections.RemoveRange(context.Connections.Where(u => u.UserId == currentUserId).ToList());
            context.SaveChanges();

            Clients.Others.SendAsync("UserOff", currentUserId);
            return base.OnDisconnectedAsync(exception);
        }


        public async Task ChatAuth(User userInfo)
        {
            string currentSignalrId = Context.ConnectionId;
            User tempUser = context.Users.Where(u => u.Username == userInfo.Username && u.Password == userInfo.Password).SingleOrDefault();
            
            if(tempUser != null)
            {
                Console.WriteLine("\nLogging: " + tempUser + " logged in " + "\nSignalRId: " + currentSignalrId);

                Connection currentUser = new Connection
                {
                    UserId = tempUser.Id,
                    SignalRId = currentSignalrId,
                    TimeStamp = DateTime.Now,
                    EntryDate = DateTime.Now
                };

                await context.Connections.AddAsync(currentUser);
                await context.SaveChangesAsync();

                ConnectedUser newUser = new ConnectedUser
                (
                    tempUser.Id, tempUser.Name, currentSignalrId
                );

                await Clients.Caller.SendAsync("ChatAuthSuccessResponse", newUser);
                await Clients.Others.SendAsync("UserOn", newUser);
            }
            else
            {
                //await Clients.Client(currentSignalRId).SendAsync("chatAuthFailRespone");
                await Clients.Caller.SendAsync("ChatAuthFailResponse"); // send back to the client that inkoked this function
            }
        }

        public async Task ReauthChat(Guid userId)
        {
            string currentSignalrId = Context.ConnectionId;
            User tempUser = context.Users.Where(u => u.Id == userId).SingleOrDefault();

            if (tempUser != null)
            {
                Console.WriteLine("\nLogging: " + tempUser + " logged in " + "\nSignalrId: " + currentSignalrId);

                Connection currentUser = new Connection
                {
                    UserId = tempUser.Id,
                    SignalRId = currentSignalrId,
                    TimeStamp = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                await context.Connections.AddAsync(currentUser);
                await context.SaveChangesAsync();


                ConnectedUser newUser = new ConnectedUser
                (
                    tempUser.Id, tempUser.Name, currentSignalrId
                );

                await Clients.Caller.SendAsync("ChatAuthSuccessResponse", newUser);
                await Clients.Others.SendAsync("UserOn", newUser);
            }
        }


        public void Logout(Guid userId)
        {
            context.Connections.RemoveRange(context.Connections.Where(u => u.UserId == userId).ToList());
            context.SaveChanges();

            Clients.Caller.SendAsync("LogoutResponse");
            Clients.Others.SendAsync("UserOff", userId);
        }
    }
}
