using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApiApp.Model
{
    [Table("UserDemo")]
    public class UserDemo
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}