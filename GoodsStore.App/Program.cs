using EmailService;
using GoodsStore.App.Infra;
using GoodsStore.App.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using GoodsStore.App.Infra.Factory;
using GoodsStore.App.Models.AccessManagement;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility;

var builder = WebApplication.CreateBuilder(args);

// Services to the container.

//Database Service
string connectionString = builder.Configuration.GetConnectionString("DBContext");
builder.Services.AddDbContext<DBContext>(opt =>
    opt.UseSqlServer(connectionString));

//User authentication by token
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<DBContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
               opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromDays(3));

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

builder.Services.AddAutoMapper(typeof(Program));

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Business services
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMainMenuRepository, MainMenuRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();

//Operational services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.MaxAge = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueCountLimit = int.MaxValue;
});

builder.Services.AddMvc()
           .AddNewtonsoftJson(options =>
           options.SerializerSettings.ContractResolver =
              new DefaultContractResolver());

builder.Services
    .AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services
    .AddControllers().AddNewtonsoftJson();
builder.Services
    .AddApplicationInsightsTelemetry();
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

#if !DEBUG
builder.Services.AddApplicationInsightsTelemetry();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    #if DEBUG
        TelemetryConfiguration.Active.DisableTelemetry = true;
        TelemetryDebugWriter.IsTracingDisabled = true;
    #endif
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Carousel}/{id?}");

app.Use(async (context, next) =>
{
    string sessionCookie;
    if (/*add here expiration check &&*/ context.Request.Cookies.TryGetValue("SessionCookieName", out sessionCookie))
    {
        context.Response.Cookies.Append("SessionCookieName", sessionCookie, new CookieOptions
        {
            MaxAge = TimeSpan.FromSeconds(10),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Path = "/"
        });
    }
    await next.Invoke();
});

using (var scope = app.Services.CreateAsyncScope())
{
    scope.ServiceProvider.GetService<IDataService>()?.InicializaDB().Wait();    
}

app.Run();

