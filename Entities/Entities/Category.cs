﻿using System.ComponentModel.DataAnnotations;

namespace ReadLater.Entities {
	public class Category : EntityBase
    {
        [Key]
        public int ID { get; set; }

		[Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

		public string AuthorId { get; set; }
		public virtual ApplicationUser Author { get; set; }
	}
}
