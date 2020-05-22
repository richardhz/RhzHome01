using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RhzHome01.Shared
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [StringLength(1000,ErrorMessage ="Message Cannot exceed 1000 characters.")]
        public string Comment { get; set; }
    }
}
