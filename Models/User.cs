using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CutFileWeb.Models
{
    public class User : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
    }
}
