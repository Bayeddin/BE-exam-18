using Microsoft.AspNetCore.Identity;

namespace BE_exam_18.Models
{
    public class User:IdentityUser
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
    }
}
