using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using GachaBot.Services;
using GachaBot.Database;
namespace GachaBot
{
	public class Program
	{
		private string tokenfile = @"D:\GachaBot\GachaBot\bottoken.json";
		public static List<DateTimeOffset> stackCooldownTimer = new List<DateTimeOffset>();
		public static List<SocketGuildUser> stackCooldownTarget = new List<SocketGuildUser>();
		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
		private DiscordSocketClient _client;
		private CommandHandler _handler;
		public async Task MainAsync()
		{
			BotToken token = JsonConvert.DeserializeObject<BotToken>(File.ReadAllText(tokenfile));
			_client = new DiscordSocketClient();
			_client.Log += Log;
			await _client.LoginAsync(TokenType.Bot, token.Token);
			await _client.SetStatusAsync(UserStatus.Idle);
			await _client.SetGameAsync("Exchanging PHDCoin for Goods", "", ActivityType.Playing);
			await _client.StartAsync();
			_handler = new CommandHandler(_client);
			await Task.Delay(-1);
		}
		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
		public class BotToken
		{
			public string Token { get; set; }
		}
	}
}
