using Bll.Service;
using Dal.Entites;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;
using User = Dal.Entites.User;

namespace TgBot_UserInfo;

public class TelegramBotListener
{
    private static string BotToken = "8054982411:AAEpJB8lYlm9Lb8TOzI7az9wwqq5SePHqDI";
    private long AdminID = 597339158;

    private List<long> allChat = new List<long>();

    private TelegramBotClient BotClient = new TelegramBotClient(BotToken);

    private Dictionary<long, string> UserForUserInfo = new Dictionary<long, string>();

    private Dictionary<long, UserInfo> UserInfos = new Dictionary<long, UserInfo>();

    private readonly IUserService _userService;
    private readonly IUserInfoService _userInfoService;

    public TelegramBotListener(IUserInfoService userInfoService, IUserService userService)
    {
        _userInfoService = userInfoService;
        _userService = userService;
    }
    // shirift uchun 
    public string EscapeMarkdownV2(string text)
    {
        string[] specialChars = { "[", "]", "(", ")", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!" };
        foreach (var ch in specialChars)
        {
            text = text.Replace(ch, "\\" + ch);
        }
        return text;
    }

    // validatsiya
    private bool ValidateFNameAndLName(string name)
    {
        foreach (var l in name)
        {
            if (!char.IsLetter(l) || l == ' ')
            {
                return false;
            }
        }
        return !string.IsNullOrEmpty(name) && name.Length <= 50;
    }
    private bool ValidatePhone(string phone)
    {
        foreach (var l in phone)
        {
            if (!char.IsDigit(l) || l == ' ')
            {
                return false;
            }
        }
        return phone.Length == 9;
    }
    private bool ValidateEmail(string email)
    {
        email.ToLower();

        return email.EndsWith("@gmail.com") && !string.IsNullOrEmpty(email) && email.Length <= 200 && email.Length > 10;
    }
    public async Task StartBot()
    {
        var receiverOptions = new ReceiverOptions { AllowedUpdates = new[] { UpdateType.Message, UpdateType.InlineQuery } };

        Console.WriteLine("Your bot is starting");

        BotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions
            );

        Console.ReadKey();
    }



    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {

        if (update.Type == UpdateType.Message)
        {

            var message = update.Message;
            var user = message.Chat;
            User userObject;
            try
            {
                userObject = await _userService.GetUserByID(user.Id);
            }
            catch (Exception ex)
            {
                userObject = null;
            }

            Console.WriteLine($"{user.Id},  {user.FirstName}, {message.Text}");
            // bu notificatsion
            if (message.Text == "AllChat")
            {
                if (userObject.TelegramUserId == AdminID)
                {
                    await bot.SendTextMessageAsync(user.Id, "So'zni Kiriting : ", cancellationToken: cancellationToken);
                    allChat.Add(AdminID);
                }
            }
            else if (allChat.Contains(AdminID))
            {
                var users = await _userService.GetAllUser();
                foreach (var u in users)
                {
                    await bot.SendTextMessageAsync(u.TelegramUserId, message.Text, cancellationToken: cancellationToken);
                }
            }

            if (message.Text == "Informatsiya Kiritish")
            {
                if (userObject.UserInfo is null)
                {
                    try
                    {
                        UserInfos.Add(user.Id, new UserInfo());
                        UserForUserInfo.Add(user.Id, "Ism");
                    }
                    catch (Exception ex)
                    {
                        UserInfos.Remove(user.Id);
                        UserInfos.Add(user.Id, new UserInfo());

                        UserForUserInfo.Remove(user.Id);
                        UserForUserInfo.Add(user.Id, "Ism");
                    }

                    await bot.SendTextMessageAsync(user.Id, "Isminggizni Kiriting : ", cancellationToken: cancellationToken);
                }
                else if (userObject.UserInfo is not null)
                {
                    var userInformation = await _userInfoService.GetUserInfByID(userObject.BotUserId);
                    var userInfo = $"~You Has Already Have Informations~\n\n*Ism* : _{userInformation.FirstNamee}_\n" +
                        $"*Familiya* : _{userInformation.LastNamee}_\n" +
                        $"*Email* : {userInformation.Email}\n" +
                        $"*PhoneNumber* : {userInformation.PhoneNumber}\n" +
                        $"*Adress* : `{userInformation.Address}`\n" +
                        $"*Summary* : *{userInformation.Summary}*";

                    await bot.SendTextMessageAsync(user.Id, EscapeMarkdownV2(userInfo), cancellationToken: cancellationToken, parseMode: ParseMode.MarkdownV2);
                    return;
                }
            }
            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Ism")
            {
                var validate = ValidateFNameAndLName(message.Text);
                if (!validate)
                {
                    await bot.SendTextMessageAsync(user.Id, "Isminggizni To'g'ri Kiriting !!!", cancellationToken: cancellationToken);
                    return;
                }
                var info = UserInfos[user.Id];
                info.FirstNamee = message.Text;
                var ch = info.FirstNamee[0];
                info.FirstNamee = info.FirstNamee.Remove(0, 1);
                info.FirstNamee = char.ToUpper(ch) + info.FirstNamee;
                UserForUserInfo[user.Id] = "Fam";
                await bot.SendTextMessageAsync(user.Id, "Familiyanggizni Kiriting : ", cancellationToken: cancellationToken);
            }

            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Fam")
            {
                var validate = ValidateFNameAndLName(message.Text);
                if (!validate)
                {
                    await bot.SendTextMessageAsync(user.Id, "Familiyanggizni To'g'ri Kiriting !!!", cancellationToken: cancellationToken);
                    return;
                }
                var info = UserInfos[user.Id];
                info.LastNamee = message.Text;
                var ch = info.LastNamee[0];
                info.LastNamee = info.LastNamee.Remove(0, 1);
                info.LastNamee = char.ToUpper(ch) + info.LastNamee;
                UserForUserInfo[user.Id] = "Ema";
                await bot.SendTextMessageAsync(user.Id, "Emailinggizni Kiriting : ", cancellationToken: cancellationToken);
            }

            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Ema")
            {
                var validate = ValidateEmail(message.Text);
                if (!validate)
                {
                    await bot.SendTextMessageAsync(user.Id, "Emailni To'g'ri Kiriting !!!", cancellationToken: cancellationToken);
                    return;
                }
                var info = UserInfos[user.Id];
                info.Email = message.Text;
                info.Email.ToLower();
                UserForUserInfo[user.Id] = "Pho";
                await bot.SendTextMessageAsync(user.Id, "Telefon raqamni kiriting (909009090 formatida):", cancellationToken: cancellationToken);
            }

            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Pho")
            {
                var validate = ValidatePhone(message.Text);
                if (!validate)
                {
                    await bot.SendTextMessageAsync(user.Id, "Telefon Nomerni To'g'ri Kiriting !!!", cancellationToken: cancellationToken);
                    return;
                }
                var info = UserInfos[user.Id];
                info.PhoneNumber = message.Text;
                info.PhoneNumber = "+998" + info.PhoneNumber;
                UserForUserInfo[user.Id] = "Adr";
                await bot.SendTextMessageAsync(user.Id, "Adresinggizni Kiriting : ", cancellationToken: cancellationToken);
            }

            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Adr")
            {
                if (message.Text.Length > 200 && !string.IsNullOrEmpty(message.Text))
                {
                    await bot.SendTextMessageAsync(user.Id, "Adressni To'g'ri Kiriting !!!", cancellationToken: cancellationToken);
                    return;
                }
                var info = UserInfos[user.Id];
                info.Address = message.Text;
                UserForUserInfo[user.Id] = "Sum";
                await bot.SendTextMessageAsync(user.Id, "Summary Kiriting : ", cancellationToken: cancellationToken);
            }

            else if (UserForUserInfo.ContainsKey(user.Id) && UserForUserInfo[user.Id] == "Sum")
            {
                var info = UserInfos[user.Id];
                info.UserId = userObject.BotUserId;
                info.Summary = message.Text;

                await _userInfoService.AddUserInfo(info);

                UserInfos.Remove(user.Id);
                UserForUserInfo.Remove(user.Id);
                await bot.SendTextMessageAsync(user.Id, "User Info Saqlandi", cancellationToken: cancellationToken);
            }

            if (message.Text == "Informatsiyani Ko'rish")
            {
                UserInfo userInformation;
                try
                {
                    userInformation = await _userInfoService.GetUserInfByID(userObject.BotUserId);//userID
                }
                catch (Exception ex)
                {
                    await bot.SendTextMessageAsync(user.Id, "User Info Topilmadi", cancellationToken: cancellationToken);
                    return;
                }

                var userInfo = $"*FirstName* : _{userInformation.FirstNamee}_\n" +
                    $"*LastName* : _{userInformation.LastNamee}_\n" +
                    $"*Email* : {userInformation.Email}\n" +
                    $"*PhoneNumber* : {userInformation.PhoneNumber}\n" +
                    $"*Adress* : `{userInformation.Address}`\n" +
                    $"*Summary* : *{userInformation.Summary}*";

                await bot.SendTextMessageAsync(user.Id, EscapeMarkdownV2(userInfo), cancellationToken: cancellationToken, parseMode: ParseMode.MarkdownV2);
            }


            if (message.Text == "Delete Information")
            {
                var userInformation = await _userService.GetUserByID(user.Id);//tguid
                if (userInformation.UserInfo is null)
                {
                    await bot.SendTextMessageAsync(user.Id, "To Delete Information\nFirst Add Information", cancellationToken: cancellationToken);
                    return;
                }
                else
                {
                    await _userInfoService.DeleteUserInfo(userInformation.UserInfo.UserId);

                    await bot.SendTextMessageAsync(user.Id, "Information Deleted", cancellationToken: cancellationToken);
                }
            }

            if (message.Text == "/start")
            {

                if (userObject == null)
                {
                    userObject = new User()
                    {
                        CreatedAt = DateTime.UtcNow,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IsBlocked = false,
                        PhoneNumberr = null,
                        TelegramUserId = user.Id,
                        UpdatedAt = DateTime.UtcNow,
                        Username = user.Username
                    };


                    await _userService.AddUser(userObject);
                }
                else
                {
                    if (user.FirstName != userObject.FirstName || user.LastName != userObject.LastName || user.Username != userObject.Username)
                    {
                        userObject.UpdatedAt = DateTime.UtcNow;
                    };
                    userObject.FirstName = user.FirstName;
                    userObject.LastName = user.LastName;
                    userObject.Username = user.Username;
                    await _userService.UpdateUser(userObject);
                }

                var keyboard = new ReplyKeyboardMarkup(new[]
            {
                    new[]
                    {
                        new KeyboardButton("Fill Data"),
                        new KeyboardButton("Get Data"),
                    },
                    new[]
                    {
                        new KeyboardButton("Delete Data"),
                    },
                })
                { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(user.Id, "Assalomu Alaykum 👋", replyMarkup: keyboard);
                return;
            }
        }
        else if (update.Type == UpdateType.CallbackQuery)
        {
            var id = update.CallbackQuery.From.Id;

            var text = update.CallbackQuery.Data;

            CallbackQuery res = update.CallbackQuery;
        }
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);
    }
}
