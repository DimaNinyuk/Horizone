namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int MessageId { get; set; }

        [StringLength(50)]
        public string SNP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Text { get; set; }

        [StringLength(50)]
        public string CallTime { get; set; }

        public string AdminId { get; set; }

        public virtual ApplicationUser Admin { get; set; }
    }
}