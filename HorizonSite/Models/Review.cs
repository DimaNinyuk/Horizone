

namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int ReviewId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public string Text { get; set; }

        public int? FilmId { get; set; }

        public string AdminId { get; set; }

        public virtual ApplicationUser Admin { get; set; }

        public virtual Film Film { get; set; }
    }
}