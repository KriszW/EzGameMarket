﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemRequirement.API.Models
{
    public class StorageNeeds
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public int Size { get; set; }
    }
}