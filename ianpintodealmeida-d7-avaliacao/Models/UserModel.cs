using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ianpintodealmeida_d7_avaliacao.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
