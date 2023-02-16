using System.ComponentModel.DataAnnotations;

namespace SMS.DTOs.DTOs
{
    public class FinanceType : DtoBaseEntity
    {
        [StringLength(500)]
        public string Type { get; set; }
    }
}
