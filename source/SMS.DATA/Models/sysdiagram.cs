using System;

namespace SMS.DATA.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class sysdiagram
    {
        [Required]
        [StringLength(128)]
        public string name { get; set; }

        public int principal_id { get; set; }

        [Key]
        public Guid diagram_id { get; set; }

        public Guid? version { get; set; }

        public byte[] definition { get; set; }
    }
}
