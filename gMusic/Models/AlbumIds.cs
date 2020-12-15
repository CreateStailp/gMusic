﻿using System;
using System.Collections.Generic;
using System.Text;
using gMusic.Api;
using SQLite;

namespace gMusic.Models
{
	internal class TempAlbumIds : AlbumIds
	{
	}

	public class AlbumIds
	{
		[PrimaryKey]
		public string Id { get; set; }

		[Indexed]
		public string AlbumId { get; set; }

		[Indexed]
		public ServiceType ServiceType { get; set; }
	}
}