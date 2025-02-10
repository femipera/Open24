using Microsoft.AspNetCore.Identity;

namespace Open24.Api.Configuration
{
    public static class SeedConfig
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var defaultUserEmail = "adm@open.com.br";
            var defaultPassword = "admin123";

            var defaultRoles = new List<string> { "Admin", "User", "Manager", "Seller" };

            try
            {
                foreach (var role in defaultRoles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                        if (!roleResult.Succeeded)
                        {
                            var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                            throw new Exception($"Error creating role {role}: {roleErrors}");
                        }
                    }
                }

                var user = await userManager.FindByEmailAsync(defaultUserEmail);
                if (user == null)
                {
                    var newUser = new IdentityUser
                    {
                        UserName = defaultUserEmail,
                        Email = defaultUserEmail,
                        EmailConfirmed = true
                    };

                    var createUserResult = await userManager.CreateAsync(newUser, defaultPassword);
                    if (!createUserResult.Succeeded)
                    {
                        var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                        throw new Exception($"Error creating the user: {errors}");
                    }

                    foreach (var role in defaultRoles)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(newUser, role);
                        if (!addRoleResult.Succeeded)
                        {
                            var addRoleErrors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
                            throw new Exception($"Error assigning the {role} role to the user {defaultUserEmail}: {addRoleErrors}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing user seed: {ex.Message}", ex);
            }
        }
    }
}
