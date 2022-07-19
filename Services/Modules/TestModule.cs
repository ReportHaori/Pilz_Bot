using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GachaBot.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GachaBot.Services.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        private readonly DBEntities _db;
        private readonly IConfiguration _config;
        public TestModule(IServiceProvider services)
        {
            _db = services.GetRequiredService<DBEntities>();
            _config = services.GetRequiredService<IConfiguration>();
        }
        [Command("coin")]
        public async Task CoinAsync()
        {
            Random rnd = new Random();
            string value = rnd.Next(0, 7).ToString();
            await ReplyAsync(value);
        }
    }
}
