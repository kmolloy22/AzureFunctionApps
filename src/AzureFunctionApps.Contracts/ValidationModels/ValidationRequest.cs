using System;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionApps.Contracts.ValidationModels
{
    public class ValidationRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$")]
        public string Email { get; set; }
    }
}
