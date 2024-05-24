using System.ComponentModel.DataAnnotations;

namespace BE_exam_18.ViewModel
{
    public class RegisterVm
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
