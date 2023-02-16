using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMS.REQUESTDATA.RequestModels
{
    public partial class FinanceType : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinanceType()
        {
            StudentFinanceDetails = new HashSet<StudentFinanceDetail>();
        }

        [StringLength(500)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFinanceDetail> StudentFinanceDetails { get; set; }
    }
}
