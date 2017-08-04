using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ttifa.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string NickName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
        [StringLength(20)]
        public string LastLoginIP { get; set; }
        public int UserStatus { get; set; }
    }
}
