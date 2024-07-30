using _22_MVC_Identity.Models.Entites.Concrete;
using _22_MVC_Identity.Models.Infradstructre.ContextDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//appcontextionda construcker optionsa gidiyor.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

//�ifre ve emial formatlar�n� buradan y�netebiliyoruz. Fakat normal projede denepency �njectionda ayazaca��z.
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{

    //�ifre ile giri�leri s�n�rland�rmak i�in yazd�k.
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
