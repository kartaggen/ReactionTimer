using System;
using System.ComponentModel.DataAnnotations;

namespace ReactionTimerWebsite.Models
{
	public class GameScore
	{
		public int ID { get; set; }
		public User user { get; set; }
		public double gameScore { get; set; }
		[DataType(DataType.Date)]
		public DateTime gameDate { get; set; }
	}
}
