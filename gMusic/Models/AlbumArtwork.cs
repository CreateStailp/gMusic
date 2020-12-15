﻿using System;
using System.Collections.Generic;
using System.Text;
using gMusic.Models;
using SQLite;

namespace gMusic.Models
{
	internal class TempAlbumArtwork : AlbumArtwork
	{
	}

	public class AlbumArtwork : Artwork
	{
		[Indexed]
		public string AlbumId { get; set; }


		public override void SetId()
		{
			Id = $"{AlbumId} - {Url}";
		}
	}
}