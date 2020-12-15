﻿using System;
using System.Collections.Generic;
using System.Text;
using gMusic.Managers;

namespace gMusic.Models
{
	public class SongPlaybackData
	{
		public SongPlaybackData()
		{
		}
		public bool IsVideo { get; set; }
		public Song Song { get; set; }
		public Track CurrentTrack { get; set; }
		public List<Track> Tracks { get; set; }
		public int CurrentTrackIndex { get; set; }

		public Uri Uri { get; set; }

		public bool IsLocal
		{
			get { 
				if (Uri == null)
					return false;
				return Uri.IsFile || Uri.Scheme == "ipod-library" ; }
		}
	}
}