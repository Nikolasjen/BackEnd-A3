using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly FoodAppG4Context _context;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApiUser> _userManager;
    private readonly SignInManager<ApiUser> _signInManager;

    public AccountController(
    FoodAppG4Context context,
    ILogger<AccountController> logger,
    IConfiguration configuration,
    UserManager<ApiUser> userManager,
    SignInManager<ApiUser> signInManager
    )
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("Register")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> Register(RegisterDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new ApiUser();
            newUser.UserName = input.Email;
            newUser.Email = input.Email;
            newUser.FullName = input.FullName;
            // TODO: Add the user id's - no, this is done manually by the admin in sql! 
            var result = await _userManager.CreateAsync(newUser, input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(
                    "User {userName} ({email}) has been created.",
                    newUser.UserName, newUser.Email);
                return StatusCode(201, $"User {newUser.UserName} has been created.");
            }
            else
            {
                throw new Exception(string.Format("Error: {0}", string.Join(" ", result.Errors.Select(e => e.Description))));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration.");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginDTO input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the user by username
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
                throw new Exception("Invalid login attempt.");

            // Retrieve the user's claims
            var userClaims = await _userManager.GetClaimsAsync(user);

            // Add the claims to the token
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("CookId", user.CookId.ToString() ?? string.Empty));
            claims.Add(new Claim("CyclistId", user.CyclistId.ToString() ?? string.Empty));
            claims.AddRange(userClaims);

            // Create signing credentials
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                    SecurityAlgorithms.HmacSha256);

            // Generate the JWT token
            var jwtObject = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(300),
                signingCredentials: signingCredentials);

            var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

            _logger.LogInformation("User {userName} has logged in.", user.UserName);
            return StatusCode(StatusCodes.Status200OK, jwtString);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login.");
            return StatusCode(500, ex.Message);
        }
    }
}