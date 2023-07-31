﻿using Lab.BackgroundTaskWithSignalR.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.BackgroundTaskWithSignalR.SignalR
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();

        public override async Task OnConnectedAsync()
        {
            // lấy  ra userId được gửi lên
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            // kiểm tra userId có null không?
            if (!string.IsNullOrEmpty(userId))
            {
                // kiểm tra user đã được connect trước đó chưa
                if (usersOnline.ContainsKey(userId))
                {
                    // nếu "rồi" thì add thêm connection id mới vào key của user đó
                    usersOnline[userId].Add(Context.ConnectionId);
                }
                else
                {
                    // nếu "chưa" thì tạo ra 1 key mới để quản lý connection id cho user đó
                    usersOnline.Add(userId, new List<string> { Context.ConnectionId });
                }

                // thông báo đến toàn bộ client biết user x đã connection
                await Clients.All.SendAsync("onConnected", new Response<object>(new { UserId = userId, isOnline = true }));

                // Logs thông tin trên server
                Console.WriteLine($"{userId} connected with connection id = {Context.ConnectionId}");

                Console.WriteLine($"There are {usersOnline.Count} users online");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // lấy  ra userId được gửi lên
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            // kiểm tra userId có null không?
            if (!string.IsNullOrEmpty(userId))
            {
                // kiểm tra user có tồn tại không và connection id của user đang kết nối có tồn tại không?
                if (usersOnline.ContainsKey(userId) && usersOnline[userId].Contains(Context.ConnectionId))
                {
                    // kiểm tra danh sách connection id của user có nhiều hơn 1 connection không?
                    if (GetConnectionIds(userId).Count > 1)
                    {
                        // nếu > 1 => xóa connection id disconnect khỏi danh sách quản lý connection id của user
                        usersOnline[userId].Remove(Context.ConnectionId);
                    }
                    else
                    {
                        // nếu = 1 => remove user key khỏi danh sách quản lý user online
                        // thông báo cho toàn bộ client đang online biết user này đã chính thức offline
                        usersOnline.Remove(userId);

                        await Clients.All.SendAsync("onDisconnected", new Response<object>(new { UserId = userId, isOnline = false }));

                        Console.WriteLine($"There are {usersOnline.Count} users online");
                    }

                    Console.WriteLine($"{userId} connected with connection id = {Context.ConnectionId}");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public async Task SendMessage(MessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                foreach (var connectionId in GetConnectionIds(userId))
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", new Response<object>(new { SenderId = userId, SenderName = request.SenderName, ReceiverId = request.ReceiverId, Content = request.Content, Timming = request.Timming }));
                }

                if (!userId.Equals(request.ReceiverId))
                {
                    foreach (var connectionId in GetConnectionIds(request.ReceiverId))
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", new Response<object>(new { SenderId = userId, SenderName = request.SenderName, ReceiverId = request.ReceiverId, Content = request.Content, Timming = request.Timming }));
                    }
                }
            }
        }

        private List<string> GetConnectionIds(string key)
        {
            return usersOnline.ContainsKey(key) ? usersOnline[key] : new List<string>();
        }

        public class MessageRequest
        {
            public string SenderName { get; set; }
            public string ReceiverId { get; set; }
            public string ReceiverName { get; set; }
            public string Content { get; set; }
            public DateTime? Timming { get; set; }
        }
    }
}
