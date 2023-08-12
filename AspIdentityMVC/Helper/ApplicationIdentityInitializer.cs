using Microsoft.AspNetCore.Identity;

namespace AspIdentityMVC.Helper;

internal static class ApplicationIdentityInitializer
{
    internal static async Task InitializeIdentity(this WebApplication app)
    {
        var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
        var scope = factory.CreateScope();
        
        var userId = app.Configuration.GetValue<string>("IdentityDefaults:PrimaryUser:Id");
        var userName = app.Configuration.GetValue<string>("IdentityDefaults:PrimaryUser:UserName");
        var userEmail = app.Configuration.GetValue<string>("IdentityDefaults:PrimaryUser:Email");
        var userPass = app.Configuration.GetValue<string>("IdentityDefaults:PrimaryUser:Password");

        var userManagerService = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var user = await userManagerService.FindByIdAsync(userId);
        if(user == null)
        {
            await userManagerService
                .CreateAsync(
                new IdentityUser { Id = userId, Email = userEmail, UserName = userName},
                userPass
                );
            user = await userManagerService.FindByIdAsync(userId);
        }
    }
}
