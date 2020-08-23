using System;
using System.ComponentModel.DataAnnotations;

namespace ReadLater.Entities {
	public class Bookmark : EntityBase  
    {
        [Key]
        public int ID { get; set; }

		[Required]
        [StringLength(maximumLength: 500)]
		[DataType(DataType.Url)]
        public string URL {get;set;}

		[DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
		
        public DateTime CreateDate { get; set; }
    }
}
