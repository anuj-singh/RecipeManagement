using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeManagement.Data;
using RecipeManagement.Data.Interfaces;
using RecipeManagement.Data.Repositories;
using RecipeManagement.Service.Interfaces;
using RecipeManagement.Service.Services;


var builder = WebApplication.CreateBuilder(args);
var key = Encoding.UTF8.GetBytes("your-256-bit-secret-key-which-is-32-bytes-long");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allows any origin (change as needed)
              .AllowAnyHeader() // Allows any header
              .AllowAnyMethod(); // Allows any HTTP method (GET, POST, etc.)
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo
    { Title="JWT Token Auth",
    Version ="v1"});
   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
	});
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			  new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string[] {}
		}
	});
});

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
builder.Services.AddTransient<ILogService,LogService>();
builder.Services.AddTransient<ILogRepository,LogRepository>();

builder.Services.AddTransient<IPasswordResetTokenRepository,PasswordResetTokenRepository>();

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
    //     ValidateIssuerSigningKey = true,
    //     IssuerSigningKey = new SymmetricSecurityKey(key),
    //     ValidIssuer = "false",
    //     ValidAudience = "false",
	// 	ClockSkew = TimeSpan.Zero,
    //     ValidateLifetime = true,
    // };
			ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourIssuer",
            ValidAudience = "yourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
});
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
