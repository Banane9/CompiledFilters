CompiledFilters
===============

[![Build status](https://ci.appveyor.com/api/projects/status/a16du0dg4thgul5u?svg=true)](https://ci.appveyor.com/project/Banane9/compiledfilters)

Write reusable filter expressions that get compiled at runtime!

## How to Use

[Example for Telegram.Bot](https://github.com/TelegramBotExtensions/Telegram.Bot.Extensions.Filters/tree/develop/src/Telegram.Bot.Extensions.Filters)

``` CSharp
/// <summary>
/// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> came from a bot.
/// </summary>
public static readonly Filter<Message> FromBot = UserFilters.IsBot.Using((Message msg) => msg.From);

/// <summary>
/// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> was forward from a Channel.
/// </summary>
public static readonly Filter<Message> ForwardedFromChannel = Filter.With<Message>(msg => msg.ForwardFromChat != null);

/// <summary>
/// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> was forwarded at all.
/// </summary>
public static readonly Filter<Message> IsForwarded = ForwardedFromUser | ForwardedFromChannel;

internal sealed class MessageRegexFilter : CustomFilter<Message>
{
    private readonly Regex regex;
    private readonly bool text;
    private readonly bool caption;

    private MessageRegexFilter(bool checkText, bool checkCaption)
    {
        text = checkText;
        caption = checkCaption;
    }

    public MessageRegexFilter(Regex regex, bool checkText, bool checkCaption)
        : this(checkText, checkCaption)
    {
        this.regex = regex ?? throw new ArgumentNullException(nameof(regex), "Regex can't be null!");
    }

    public MessageRegexFilter(string text, bool checkText, bool checkCaption)
        :this(checkText, checkCaption)
    {
        regex = new Regex(Regex.Escape(text ?? throw new ArgumentNullException(nameof(text), "Pattern can't be null!")));
    }

    protected override bool Matches(Message message)
    {
        return (text && message.Text != null && regex.IsMatch(message.Text))
            || (caption && message.Caption != null && regex.IsMatch(message.Caption));
    }
}
```
