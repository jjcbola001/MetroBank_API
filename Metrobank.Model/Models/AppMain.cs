using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Metrobank.Model.Models
{
    public class AppMain
    {
        public int Id { get; set; }
        public int InputNumber { get; set; }
        public int OutputNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
