using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(6)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime, ErrorMessage = "Datetime is invalid")]
        [DisplayName("Last Update Date")]
        public DateTime LastUpdateDate { get; set; }
    }
}
