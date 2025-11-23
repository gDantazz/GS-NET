using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WorkTimePanelFull.Infrastructure.Data;
using WorkTimePanelFull.Infrastructure.Services;
using WorkTimePanelFull.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================================
//  BANCO DE DADOS
// ================================
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(conn))
{
    throw new Exception("❌ A connection string 'DefaultConnection' não está configurada no appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(conn));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Serviços
builder.Services.AddScoped<WorkTimePanelFull.Application.Services.IAuthService, AuthService>();
builder.Services.AddScoped<WorkTimePanelFull.Application.Services.IExternalPausaService, ExternalPausaService>();

// HttpClient para Java API
var javaApiBase = builder.Configuration["JavaApi:BaseUrl"];
if (string.IsNullOrWhiteSpace(javaApiBase))
{
    throw new Exception("❌ A configuração 'JavaApi:BaseUrl' está faltando no appsettings.json.");
}

builder.Services.AddHttpClient("javaApi", client =>
{
    client.BaseAddress = new Uri(javaApiBase);
});

// ================================
//  CONFIGURAÇÃO JWT
// ================================
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrWhiteSpace(jwtKey) ||
    string.IsNullOrWhiteSpace(jwtIssuer) ||
    string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new Exception("""
❌ Configurações JWT ausentes! 
Certifique-se que seu appsettings.json contém:

"Jwt": {
  "Key": "sua-chave",
  "Issuer": "WorkTimePanel",
  "Audience": "WorkTimeUsers"
}
""");
}

var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,

        ValidateAudience = true,
        ValidAudience = jwtAudience,

        ClockSkew = TimeSpan.Zero
    };
});

// ================================
//  BUILD DO APP
// ================================
var app = builder.Build();

// Aplicar migrações e seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
