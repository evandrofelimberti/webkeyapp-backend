using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using WebAppKey.Data;
using WebAppKey.Services;
using WebAppKey.Services.Interfaces;
using Newtonsoft.Json.Serialization;
using WebAppKey.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLowerCaseNamingConvention();
});
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);  
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IUnidadeService, UnidadeService>();
builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<ITipoProdutoService, TipoProdutoService>();
builder.Services.AddTransient<ITipoMovimentoService, TipoMovimentoService>();
builder.Services.AddTransient<IMovimentoService, MovimentoService>();
builder.Services.AddTransient<IMovimentoItemService, MovimentoItemService>();
builder.Services.AddTransient<ILavouraService, LavouraService>();
builder.Services.AddTransient<ISafraService, SafraService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();


/*var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};*/

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(o =>
{
    o.JsonSerializerOptions.WriteIndented = true;
});

#region JWT Config

//aqui vai nossa key secreta, o recomendado é guarda - la no arquivo de configuração
var secretKey = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.UseDeveloperExceptionPage();
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

//app.UseMvc();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

