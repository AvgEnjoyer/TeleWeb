using System.Data;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TeleBot
{
    public class BloggingBot
    {
        private enum BotState
        {
            WAITING_FOR_COMMAND_DEFAULT = 0,      //00
            WAITING_FOR_CHANNEL_POST    = 1 << 0, //01
            WAITING_FOR_WEB_POST        = 1 << 1, //10
            WAITING_FOR_BOTH_POST = WAITING_FOR_CHANNEL_POST | WAITING_FOR_WEB_POST, //11
            CHANNEL_REGISTRATION = 1 << 2
        }


        BotState state;

        private readonly TelegramBotClient _botClient;

        public BloggingBot(string APIkey)
        {
            TelegramBotClient client = new TelegramBotClient(APIkey);
            _botClient = client;
            state = BotState.WAITING_FOR_COMMAND_DEFAULT;
        }

        public async void StartBot()
        {
            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"Hello, I am {me.FirstName} and my username is @{me.Username}");

            _botClient.StartReceiving(UpdateHandler, ErrorHandler);
        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var message = update.Message;
                    if (message == null || message.Text==null) return;
                    if (message.Text[0] == '/')
                        state = BotState.WAITING_FOR_COMMAND_DEFAULT;
                    
                    switch (state)
                    {
                        case BotState.WAITING_FOR_COMMAND_DEFAULT:
                            await ProcessDefaultAsync(botClient, update, cancellationToken);
                            break;
                        case BotState.WAITING_FOR_CHANNEL_POST:
                            await ProcessChannelPostAsync(botClient, update, cancellationToken);
                            break;
                        case BotState.WAITING_FOR_WEB_POST:
                            await ProcessWebPostAsync(botClient, update, cancellationToken);
                            break;
                        case BotState.WAITING_FOR_BOTH_POST:
                            await ProcessChannelPostAsync(botClient, update, cancellationToken);
                            await ProcessWebPostAsync(botClient, update, cancellationToken);
                            break;
                        case BotState.CHANNEL_REGISTRATION:
                            await ProcessChannelRegistration(botClient, update, cancellationToken);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


        private async Task<int> ProcessDefaultAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var command = message.Text;
            if (string.IsNullOrEmpty(command))
            { return 2; }
            switch (command)
            {
                case "/start":
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Hello, I am a bot. I can help you to post your articles to your blog.");
                    break;
                    
                case "/help":
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "I can help you to post your articles to your blog.");
                    break;
                    
                case "/postChannelToWeb":
                    state |= BotState.WAITING_FOR_WEB_POST;
                      break;
                    
                case "/postToChannel":
                    state |= BotState.WAITING_FOR_CHANNEL_POST;
                      break;
                case "/registerChannel":
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Please provide the invitation link to the channel:", cancellationToken: cancellationToken);
                    state = BotState.CHANNEL_REGISTRATION;
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "I don't understand you", cancellationToken: cancellationToken);
                      break;
            }
            return 0;
        }
        private async Task ProcessChannelRegistration(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            try { var chat = await botClient.GetChatAsync(update.Message.Text, cancellationToken); 
            if (chat == null)
            {
                return;
                // Do something with the chatId...
            }
            if(
                    (await botClient.GetChatAdministratorsAsync(chat.Id)).FirstOrDefault(x=>x.User.Id ==botClient.BotId) != null
                &&  (await botClient.GetChatAdministratorsAsync(chat.Id)).FirstOrDefault(x => x.User.Id == update.Message.From.Id) != null)
            {
                await botClient.SendTextMessageAsync(chat.Id, "great");
            }
                else { await _botClient.SendTextMessageAsync(update.Message.Chat.Id, "It seems to be you are not the administrator of this channel"); }
            }
            catch
            {
                // The bot is not an administrator of the channel
                    await _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Please make sure that you provided correct @'link_to_channel' and added me as administrator");
            }

        }

        private Task ProcessWebPostAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task ProcessChannelPostAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private async Task ProcessPostAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

        }



        private async Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            
        }

    }
}