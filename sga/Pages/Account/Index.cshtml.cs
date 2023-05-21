using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace sga.Pages.Account
{
	[AllowAnonymous]
	public class IndexModel : PageModel
    {
		[BindProperty]
		public string email { set; get; }
		[BindProperty]
		public string password { set; get; }

		public async Task<IActionResult> OnGet(string returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Content("~/");

			if (User.Identity.IsAuthenticated)
			{
				return LocalRedirect(returnUrl);
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			Debug.WriteLine($"[OnPostAsync] {email} | {password}");

			returnUrl = returnUrl ?? Url.Content("~/");

			if (string.IsNullOrEmpty(email))
			{
				ViewData["message"] = $"Ingresa un correo.";
				return Page();
			}

			if (string.IsNullOrEmpty(password))
			{
				ViewData["message"] = $"Ingresa una contraseña.";
				return Page();
			}

			var user = await this.HttpContext.Auth(email, password);

			if (user == null)
			{
				ViewData["message"] = $"Usuario no encontrado.";
				return Page();
			}


			return LocalRedirect(returnUrl);
		}
    }
}
