using mainDemo;
using mainDemo.Bots;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add bot service
builder.Services.AddBot<FlowerShopBot>(options =>
{
    options.CredentialProvider = new ConfigurationCredentialProvider(builder.Configuration);

    var conversationState = new ConversationState(new MemoryStorage());
    options.State.Add(conversationState);
});
builder.Services.AddSingleton(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
    var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();

    var accessors = new BotAccessors(conversationState)
    {
        DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateAccessorName),
        FlowerShopStateStateAccessor = conversationState.CreateProperty<FlowerShopState>(BotAccessors.FlowerShopBotStateAccessorName)
    };

    return accessors;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseBotFramework();

app.MapControllers();

app.Run();
