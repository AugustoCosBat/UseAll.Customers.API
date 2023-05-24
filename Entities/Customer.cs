using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseAll.Customers.API.Entities
{
    [Table ("Customers", Schema = "public")]
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
    }
}
