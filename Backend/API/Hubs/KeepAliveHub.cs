using API.Interfaces.Hubs;
using API.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

/// <summary>
/// Hub for broadcasting news updates.
/// </summary>
public class KeepAliveHub : Hub<INewsHub>
{
    /// <summary>
    /// A keep alive method to keep the connection alive.
    /// </summary>
    public async Task KeepAlive()
    {
        Console.WriteLine("Keep alive");
        await Task.CompletedTask;
    }
}