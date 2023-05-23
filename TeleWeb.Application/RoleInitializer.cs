using Microsoft.AspNetCore.Identity;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application;

public class RoleInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("AuthorizedUser"));
    }
}