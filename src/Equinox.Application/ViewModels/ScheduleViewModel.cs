using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ScheduleViewModel
    {
        public string Time { get; set; }
        public int TableId { get; set; }

        public bool Status { get; set; }
        
        public ScheduleViewModel()
        {
          Status = true;   
        }
    }
}
