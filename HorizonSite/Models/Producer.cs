

namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Producer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer()
        {
            FilmsProducers = new HashSet<FilmsProducer>();
        }

        [Key]
        public int ProdecerId { get; set; }

        [StringLength(60)]
        public string SNP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birtday { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmsProducer> FilmsProducers { get; set; }
    }
}