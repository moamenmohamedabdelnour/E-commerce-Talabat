using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace AdminPanel.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name Is Required !! ")]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
