using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jobs.Models
{
    [Table("LOAITK")]
    public class LOAITK
    {
        [Key]
        public int ID { get; set; }

        public string LoaiTK { get; set; }
    }
}