using System.Net.Mime;
using System.Text;
using System.Text.Json;
using FluentValidation;
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
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Mutations;
using WebAppKey.Profiles;
using WebAppKey.Queries;
using WebAppKey.Types;
using WebAppKey.Validators;
using WebAppKey.Validators.Intefaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<DataContext>(options =>
{
   // options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLowerCaseNamingConvention();
   options.UseNpgsql(connectionString).UseLowerCaseNamingConvention();
});
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);  
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IUnidadeService, UnidadeService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ITipoProdutoService, TipoProdutoService>();
builder.Services.AddScoped<ITipoMovimentoService, TipoMovimentoService>();
builder.Services.AddScoped<IMovimentoService, MovimentoService>();
builder.Services.AddScoped<IMovimentoItemService, MovimentoItemService>();
builder.Services.AddScoped<ILavouraService, LavouraService>();
builder.Services.AddScoped<ISafraService, SafraService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUnidadeValidator, UnidadeValidator>();
builder.Services.AddScoped<IValidator<CreateUnidadeInput>, UnidadeFluentValidator>();

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

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<UnidadeDto, UnidadeType>();
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<UnidadeQuery>()
    .AddMutationType<UnidadeMutation>();

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGraphQL("/graphql");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
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



