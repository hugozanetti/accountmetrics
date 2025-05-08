using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using accountmetrics.ViewModels;

namespace accountmetrics.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;

    }

    [HttpGet]
    public ActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl

        });
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel loginVM)
    {
        if(!ModelState.IsValid)
        {
            return View(loginVM);
        }

        var user = await _userManager.FindByNameAsync(loginVM.UserName);

        if(user != null)
        {
            // false, false = Nao vai persistir o cookie e nao vai bloquear o usuario.
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

            if(result.Succeeded)
            {
                if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }
        }
        
        ModelState.AddModelError("", "Falha ao realizar login!!");
        return View(loginVM);
        

    }
    
    [HttpGet]   
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost] 
    [ValidateAntiForgeryToken]  
    public async Task<IActionResult> Register(LoginViewModel registroVM)
    {
        if(ModelState.IsValid)
        {
            var usuario = new IdentityUser { UserName = registroVM.UserName};
            var result = await _userManager.CreateAsync(usuario, registroVM.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                this.ModelState.AddModelError("Registro", "Erro ao criar usuário!!");
            }
        }

        return View(registroVM);
    }

     [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}