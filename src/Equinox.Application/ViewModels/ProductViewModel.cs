using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Equinox.Domain.Models;

namespace Equinox.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        [DisplayName("Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(6)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("LastUpdateDate")]
        public DateTime? LastUpdateDate { get; set; }
    }

    public class SearchResponse
    {
        public IEnumerable<ProductViewModel> Documents { get; set; }
        public long Total { get; set; }
    }
}
