using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using GachaBot.Database;
using Newtonsoft.Json;

namespace GachaBot.Services
{
    public class CommandHandler
    {
        private string tokenfile = @"D:\GachaBot\GachaBot\bottoken.json";
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _service;
        private DBEntities _db;
        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _command = new CommandService();
            _service = new ServiceCollection()
                .AddSingleton(_client)
                .AddDbContext<DBEntities>()
                .BuildServiceProvider();
            _client.MessageReceived += HandleCommandAsync;
            _db = _service.GetRequiredService<DBEntities>();
            _command.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _service);
        }
        private async Task HandleCommandAsync(SocketMessage s)
        {

            BotPrefix prefix = JsonConvert.DeserializeObject<BotPrefix>(File.ReadAllText(tokenfile));

            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (!(msg.HasStringPrefix(prefix.Prefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos)) || msg.Author.IsBot)
                return;
            else
            {
                if (Program.stackCooldownTarget.Contains(context.User as SocketGuildUser))
                {
                    if (Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)].AddSeconds(4) >= DateTimeOffset.Now)
                    {
                        int secondsLeft = (int)(Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)].AddSeconds(5) - DateTimeOffset.Now).TotalSeconds;
                        var notice = await context.Channel.SendMessageAsync($"**Try using commands again in {secondsLeft}, thank you!**");
                        await Task.Delay(2000);
                        await notice.DeleteAsync();
                        return;
                    }
                    else
                    {
                        Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(context.Message.Author as SocketGuildUser)] = DateTimeOffset.Now;
                        await _command.ExecuteAsync(context: context, argPos: argPos, services: _service);
                    }
                }
                else
                {
                    Program.stackCooldownTarget.Add(context.User as SocketGuildUser);
                    Program.stackCooldownTimer.Add(DateTimeOffset.Now);
                    await _command.ExecuteAsync(context: context, argPos: argPos, services: _service);
                }
            }

        }
    }
    public class BotPrefix
    {
        public string Prefix { get; set; }
    }
}