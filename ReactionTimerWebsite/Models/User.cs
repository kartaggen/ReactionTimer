using System;
using System.ComponentModel.DataAnnotations;

namespace ReactionTimerWebsite.Models
{
	public class User
	{
		public int ID { get; set; }
		[Display(Name = "First Name")]
		public string firstName { get; set; }
		[Display(Name = "Last Name")]
		public string lastName { get; set; }
		[Display(Name = "Username")]
		public string username { get; set; }
		[Display(Name = "Password")]
		public string password { get; set; }
		[DataType(DataType.Date)]
		public DateTime regDate { get; set; }
	}
}
