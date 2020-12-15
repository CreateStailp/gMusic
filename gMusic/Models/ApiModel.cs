﻿using System;
using SQLite;
using gMusic.Api;

namespace gMusic
{
	public class ApiModel
	{
		public int Id { get; set; }

		public ServiceType Service { get; set; }

		public string Description { get; set; }

		public string UserName { get; set; }

		public string DeviceId { get; set; }

		[MaxLength(500)]
		public string ExtraData { get; set; }
	}
}