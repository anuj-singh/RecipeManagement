using Microsoft.EntityFrameworkCore;
using RecipeManagement.Data;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Repositories;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeDBContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRecipeRepository,RecipeRepository>();
builder.Services.AddTransient<IRecipeService,RecipeService>();

builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<IUserService,UserService>();

builder.Services.AddTransient<IAuthService,AuthService>();
builder.Services.AddTransient<IUserRoleRepository,UserRoleRepository>();

builder.Services.AddTransient<IAdminService,AdminService>(); 

builder.Services.AddTransient<ICategoryRepository,CategoryRepository>();
builder.Services.AddTransient<ICategoryService,CategoryService>();

 
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
