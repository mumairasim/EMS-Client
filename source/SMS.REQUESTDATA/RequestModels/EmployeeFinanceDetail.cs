using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    public partial class EmployeeFinanceDetail : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeFinanceDetail()
        {
            EmployeeFinances = new HashSet<EmployeeFinance>();
        }
        public Guid? EmployeeId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        public virtual ICollection<EmployeeFinance> EmployeeFinances { get; set; }
    }
}
