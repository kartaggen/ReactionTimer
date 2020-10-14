using System;
using System.ComponentModel.DataAnnotations;

namespace ReactionTimerWebsite.Models
{
	public class HighScore
	{
		public int ID { get; set; }
		public User user { get; set; }
		public double highScore { get; set; }
		[DataType(DataType.Date)]
		public DateTime scoreDate { get; set; }
	}
}
