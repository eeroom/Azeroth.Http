using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HttpFile.Model {
    [Table("FileEntity")]
    public class FileEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }


        [Required]
        public long Size { get; set; }

        [Required]
        public UploadStep UploadStepValue { get; set; }

        [Required]
        public string ClientHashValue { get; set; }

        [NotMapped]
        public string CC { get; set; }

        [NotMapped]
        public string Path { get; set; }
    }
}