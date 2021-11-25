﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Http.File.Model {
    [Table("FileEntity")]
    public class FileEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Folder { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public UploadStep UploadStepValue { get; set; }

        [Required]
        public string ClientHashValue { get; set; }
    }
}