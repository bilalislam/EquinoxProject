using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ReservationViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(6)]
        [MaxLength(100)]
        [DisplayName("Title")]
        public string Title { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The StartDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime, ErrorMessage = "Datetime is invalid")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The EndDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime, ErrorMessage = "Datetime is invalid")]
        [DisplayName("EndDate Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The TableNumber is Required")]
        [MinLength(1)]
        [MaxLength(10)]
        [DisplayName("Table Number")]
        public int TableId { get; set; }
    }
}
