//SecurityController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Task1.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        //dictionary to store users usernames and passwords
        static Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "Michael", "1234" },
            { "John", "5678" }
        };

        //method to authenticate the user
        [HttpPost]
        public IActionResult Login(String UID, String PWD)
        {
            if (users.ContainsKey(UID) && users[UID] == PWD)
            {
                // Create claims (store the name) for the authenticated user
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, UID) };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //call sign in aync method to authenticate the user
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();
                // Redirect to the home page or dashboard after successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Return to the login view with an error message
                ViewBag.ErrorMessage = "Incorrect username or password.";
                return Login();
            }
        }

        //method to log out the user
        public IActionResult Logout()
        {
            // Sign out the user
            HttpContext.SignOutAsync().Wait();
            // Redirect to the login page after logout
            return RedirectToAction("Login");
        }
    }
}