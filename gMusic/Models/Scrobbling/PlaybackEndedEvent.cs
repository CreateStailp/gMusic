﻿using System;
using System.Collections.Generic;
using System.Text;
using gMusic.Api;
using gMusic.Data;
using gMusic.Managers;
using SQLite;

namespace gMusic.Models.Scrobbling
{
	public class PlaybackEndedEvent : SongPlaybackEvent
	{
		public PlaybackEndedEvent()
		{
		}

		public PlaybackEndedEvent(Song song)
		{
			if (song == null)
				return;
			SongId = song.Id;
			Album = song.AlbumId;
			Artist = song.ArtistId;
			Title = song.Name;
			Time = DateTime.UtcNow;
		}

		[Indexed]
		public bool Scrobbled { get; set; }

		[Indexed]
		public bool Submitted { get; set; }

		public double Position { get; set; }

		public double Duration { get; set; }

		public PlaybackEndedReason Reason { get; set; }
		public ServiceType ServiceType { get; internal set; }

		public void Save()
		{
			//if (Context != null)
			//{
			//	if (Context.Id > 0)
			//		ScrobbleDatabase.Main.Update(Context);
			//	else
			//	{
			//		ScrobbleDatabase.Main.Insert(Context);
			//	}
			//}
			//if (Id > 0)
			//	ScrobbleDatabase.Main.Update(this);
			//else
			//{
			//	ScrobbleDatabase.Main.Insert(this);
			//}
		}
	}
}