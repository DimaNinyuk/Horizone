
namespace HorizonSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Actor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actor()
        {
            FilmsActors = new HashSet<FilmsActor>();
        }

        public int ActorId { get; set; }

        [StringLength(50)]
        public string SNP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birtday { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmsActor> FilmsActors { get; set; }
    }
}
