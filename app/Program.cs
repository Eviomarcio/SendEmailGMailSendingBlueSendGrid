using app.Interface;
using app.Services;
using app.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// builder.Services.Configure<GmailSettings>(builder.Configuration.GetSection(nameof (GmailSettings)));
// builder.Services.AddSingleton<IEmailService, GMailService>();

//SendBlue
// builder.Services.Configure<SendingBlueSettings>(builder.Configuration.GetSection(nameof (SendingBlueSettings)));
// builder.Services.AddSingleton<IEmailService, SendingBlueService>();

//SendGrid
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection(nameof (SendGridSettings)));
builder.Services.AddSingleton<IEmailService, SendGridService>();


var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
