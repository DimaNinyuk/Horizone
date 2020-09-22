namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FilmsProducer")]
    public partial class FilmsProducer
    {
        public int Id { get; set; }

        public int? FilmId { get; set; }

        public int? ProdecerId { get; set; }

        public virtual Film Film { get; set; }

        public virtual Producer Producer { get; set; }
    }
}