using BlazoriumWASMS.Client.Pages;
using BlazoriumWASMS.Components;
using BlazoriumWASMS.Components.Account;
using BlazoriumWASMS.Data;
using BlazoriumWASMS.Repository;
using BlazoriumWASMS.Repository.IRepository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazoriumWASMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddAuthenticationStateSerialization();

            builder.Services.AddCascadingAuthenticationState();

            // Register ICategory
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}

// Comments
// Μέρος 1
// -----------------------------------------------------------------------------------------------------
// 0. During creation for AuthenticationType = Individual Accounts
// 1. Create folder ViewModels
// 2. Create folder Repository
// 3. Create folder Repository\IRepository
// 4. Create folder Services
// 5. Create folder Extensions
// 6. In wwwroot, the bootstrap is included
// 7. Create folder wwwroot\images
// 8. Create folder wwwroot\js
// 9. In appsettings.json, correct the connection string
//      or get the (localdb)\mssqllocaldb (από το default που προτείνει) and test. Θα δεις ότι συνδέθηκε!!
// 10. update-database (Επειδή έχω ενεργοποιήσει το Authentication Type, έχει πράγματα για τη βάση)

// Μέρος 2  
// -----------------------------------------------------------------------------------------------------
// 1.   Create Models in the Models folder
// 2.   Add DataAnnotations to Models
// 3.   Create the I<Model>Repository interface in IRepository folder
//      3.1  Add method signatures (Create, Update, Delete, Get, GetAll)
// 4.   Create the <Model>Repository class in Repository folder
//      4.1  Implement the I<Model>Repository interface
//      4.2  Inject the ApplicationDbContext in the constructor
//      4.3  Implement the methods using Entity Framework Core
// 5.   In ApplicationDbContext, add DbSet properties for the models and Seed data if needed

// Μέρος 3
// -----------------------------------------------------------------------------------------------------
//  1.  In (NonInteractivity project) Pages/Category folder, add the CategoryList.razor file
//          Show layout
//  2.  Κανω @inject το ICategoryRepository στο Page Component
//      2.1 In _imports ,add the using for IRepository and Data
//      2.2 Use ICategoryRepository object in methods of PageComponent
//      2.3 The methods must be Async and Sync
//  3.  In Program.cs, register ICategoryRepository with the CategoryRepository
//  4.  Create the UpsertCategory.razor file 
//  5.  Import JQUERY Library, 
//  6.  Import Bootstrap (Υπάρχει by default), Bootstrap Javascript, Bootstrap Icons, Toastr JS
//  7. In wwwroot/js folder add javascript functions in notification.js
//      7.1 In App.razor add the script
//  8. In Extensions folder, add extension methods for IJSRuntime
//  9. In any razor file inject the IJSRuntime and use the extension methods
