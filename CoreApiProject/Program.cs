using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeManagement.Data;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Repositories;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes("your-256-bit-secret-key-which-is-32-bytes-long");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidIssuer = "false",
        ValidAudience = "false"
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<RecipeDBContext>(options => 
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<RecipeDBContext>(options => {});

builder.Services.AddTransient<IRecipeRepository,RecipeRepository>();
builder.Services.AddTransient<IRecipeService,RecipeService>();

builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<IUserService,UserService>();

builder.Services.AddTransient<IAuthService,AuthService>();
builder.Services.AddTransient<IUserRoleRepository,UserRoleRepository>();

builder.Services.AddTransient<IAdminService,AdminService>(); 

builder.Services.AddTransient<ICategoryRepository,CategoryRepository>();
builder.Services.AddTransient<ICategoryService,CategoryService>();
builder.Services.AddTransient<ICommonService,CommonService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
