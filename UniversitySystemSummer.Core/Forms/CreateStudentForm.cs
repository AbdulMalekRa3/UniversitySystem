using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystemSummer.Core.Forms
{
    public class CreateStudentForm
    {
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
    
    }
}
