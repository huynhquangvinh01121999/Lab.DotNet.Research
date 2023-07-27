using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.BackgroundTaskWithSignalR.SignalR
{
    public class SignalHub : Hub
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();

        public override Task OnConnectedAsync()
        {
            lock (usersOnline)
            {
                var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

                if (usersOnline.ContainsKey(userId))
                {
                    usersOnline[userId].Add(Context.ConnectionId);
                }
                else
                {
                    usersOnline.Add(userId, new List<string> { Context.ConnectionId });
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId) && usersOnline.ContainsKey(userId) && usersOnline[userId].Contains(Context.ConnectionId))
            {
                usersOnline[userId].Remove(Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
