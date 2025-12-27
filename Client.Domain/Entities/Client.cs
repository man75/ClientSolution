using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSi.Domain.Entities
{
    public class Client
    {
     
        public Guid Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public bool IsPro { get; set; }
        public string? Email { get; set; }
    }
}
