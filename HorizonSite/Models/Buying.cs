namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Buying")]
    public partial class Buying
    {
        [Key]
        public int BuyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? SeatId { get; set; }

        public int? SessionId { get; set; }

        public int? RowId { get; set; }

        public int? HallId { get; set; }

        public int? FilmId { get; set; }

        public virtual Seat Seat { get; set; }

        public virtual Session Session { get; set; }
    }
}