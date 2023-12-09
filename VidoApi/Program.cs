
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Pos.Model.Model.Auth;
using Promotion.Application.Services;
using System.IO.Compression;
using Vido.Model.Model.Comon;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Vido.Core.Service;
using Vido.Core.Event;
using Microsoft.Extensions.Configuration;
using Vido.Model.Model;
using Vido.Core.Extensions.Provider;
using Vido.Model.Model.Table;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers().AddNewtonsoftJson(
                        opt =>
                        {
                            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        }
                    );
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1_06_10_2023"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IGiftcardService, GiftcardService>();
builder.Services.AddScoped<IIMSManageService, IMSManageService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IFileProvider, FileProvider>(); 

builder.Services.AddScoped<CategoryEvent>(); 
builder.Services.AddScoped<IMSManageEvent>();
builder.Services.AddScoped<ItemEvent>();
builder.Services.AddScoped<PaymentEvent>();
builder.Services.AddScoped<StoreEvent>();
builder.Services.AddScoped<TicketEvent>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
    options.MimeTypes =
                ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml" });
});


var app = builder.Build();
app.UseResponseCompression();
app.UseCors();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//var dashboardOptions = new DashboardOptions { IgnoreAntiforgeryToken = true };

//dashboardOptions.Authorization = new[] { new DashboardNoAuthorizationFilter() };

//app.UseHangfireDashboard("/hangfire", dashboardOptions);

//app.UseAuthorization();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.UseStaticFiles();
//app.UseC(Directory.GetCurrentDirectory());
AuthData.Configure(configuration.GetSection("Jwt"));
Const.POS_CONNECTION_STRING = configuration.GetConnectionString("POSContext");
RemoteSystemSetting.Configure(configuration.GetSection("RemoteSystem"));
PosAppSetting.Configure(configuration.GetSection("AppSettings"));

app.Run();
