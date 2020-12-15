using System;
using SQLite;

namespace gMusic.Models
{
	public class EqualizerPresetValue
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int EqualizerId { get; set; }
		public int Order { get; set; }
		public double Value { get; set; }
	}
}