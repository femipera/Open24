using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Open24.Shared.DTOs.Responses;
using Open24.Shared.Constants;
using Open24.Shared.DTOs.Requests;

namespace Open24.Api.Controllers;

[Route("api/identity")]
[ApiController]
public class IdentityController : BaseController
{
    #region Initialization
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser>
                                signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    #endregion

    #region login
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        return await ExecuteAsync(async () =>
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return CreateErrorResponse<LoginResponse>(StatusCodes.Status401Unauthorized, ResponseMessages.InvalidCredentials);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = GenerateJwtToken(user, role);

            var response = new LoginResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email,
                    Role = role
                }
            };

            return CreateSuccessResponse(response, StatusCodes.Status200OK);
        });
    }
    #endregion

    #region register
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
    {
        return await ExecuteAsync(async () =>
        {
            var user = new IdentityUser { UserName = userRegisterRequest.Email, Email = userRegisterRequest.Email };
            var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);

            if (!result.Succeeded)
                return CreateErrorResponse<object>(StatusCodes.Status400BadRequest, "Failed to create user.");

            var role = "User";
            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                return CreateErrorResponse<object>(StatusCodes.Status400BadRequest, "Failed to assign role to user.");
            }

            return CreateSuccessResponse<object>(null, StatusCodes.Status200OK);
        });
    }
    #endregion

    #region delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return CreateErrorResponse<string>(StatusCodes.Status404NotFound, "User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return CreateErrorResponse<string>(StatusCodes.Status400BadRequest, "Error deleting user");
            }

            return CreateSuccessResponse(ResponseMessages.DeletedSuccessfully, StatusCodes.Status200OK);
        });
    }
    #endregion

    #region JwtToken
    private string GenerateJwtToken(IdentityUser user, string role)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, role)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}
