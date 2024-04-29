using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace st10263992.Views.Home
{
    public class RegisterModel : PageModel
    {
        public RegisterModel()
        { var Out = Request.Form["txtName"]; 
           Console.WriteLine(Out);
        }
    }
}
