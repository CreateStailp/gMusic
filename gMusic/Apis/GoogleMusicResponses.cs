﻿using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;
using SQLite;
using Newtonsoft.Json;

namespace gMusic.Api.GoogleMusic
{
	public class RootApiObject
	{

        [JsonProperty("kind")]
		public string Kind { get; set; }

		public Error Error { get; set; }
		public string Id { get; set; }
	}

	public class RootTrackApiObject : RootApiObject
	{
		[JsonProperty("nextPageToken")]
		public string NextPageToken { get; set; }

		[JsonProperty("data")]
		public DataClass Data { get; set; }

		public class DataClass
		{

			[JsonProperty("items")]
			public List<SongItem> Items { get; set; } = new List<SongItem>();
		}

	}

	public partial class SongItem
	{
		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("clientId")]
		public string ClientId { get; set; }

		[JsonProperty("creationTimestamp")]
		public string CreationTimestamp { get; set; }

		[JsonProperty("lastModifiedTimestamp")]
		public long LastModifiedTimestamp { get; set; }

		[JsonProperty("recentTimestamp")]
		public string RecentTimestamp { get; set; }

		[JsonProperty("deleted")]
		public bool Deleted { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("artist")]
		public string Artist { get; set; }

		[JsonProperty("composer")]
		public string Composer { get; set; }

		[JsonProperty("album")]
		public string Album { get; set; }

		[JsonProperty("albumArtist")]
		public string AlbumArtist { get; set; }

		[JsonProperty("year")]
		public int Year { get; set; }

		[JsonProperty("comment")]
		public string Comment { get; set; }

		[JsonProperty("trackNumber")]
		public int Track { get; set; }

		[JsonProperty("genre")]
		public string Genre { get; set; }

		[JsonProperty("durationMillis")]
		public long Duration { get; set; }

		[JsonProperty("beatsPerMinute")]
		public int? BeatsPerMinute { get; set; }

		[JsonProperty("albumArtRef")]
		public ArtRef[] AlbumArtRef { get; set; } = new ArtRef[0];

		[JsonProperty("artistArtRef")]
		public ArtRef[] ArtistArtRef { get; set; } = new ArtRef[0];

		[JsonProperty("playCount")]
		public int PlayCount { get; set; }

		[JsonProperty("totalTrackCount")]
		public int? TotalTrackCount { get; set; }

		[JsonProperty("discNumber")]
		public int Disc { get; set; }

		[JsonProperty("totalDiscCount")]
		public int? TotalDiscCount { get; set; }

		[JsonProperty("rating")]
		public int Rating { get; set; }

		[JsonProperty("estimatedSize")]
		public string EstimatedSize { get; set; }

		[JsonProperty("storeId")]
		public string StoreId { get; set; }

		[JsonProperty("albumId")]
		public string AlbumId { get; set; }

		[JsonIgnore]
		public string ArtistMatchedId { get; set; }
		List<string> artistId;

		[Ignore]
		[JsonProperty("ArtistId")]
		public List<string> ArtistIdList
		{
			get { return artistId; }
			set
			{
				artistId = value;
				if (value != null && value.Count > 0)
					ArtistMatchedId = value.FirstOrDefault();
			}
		}

		public bool IsAllAccess
		{
			get { return Type == 7; }
			set { }
		}

		[JsonProperty("nid")]
		public string Nid { get; set; }

		[JsonProperty("primaryVideo")]
		public PrimaryVideo PrimaryVideo { get; set; }

		[JsonProperty("trackType")]
		public int Type { get; set; }

		[JsonProperty("explicitType")]
		public string ExplicitType { get; set; }
	}

	public partial class ArtRef
	{
		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("aspectRatio")]
		public int AspectRatio { get; set; }

		[JsonProperty("autogen")]
		public bool? Autogen { get; set; }
	}

	public partial class PrimaryVideo
	{
		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("thumbnails")]
		public Thumbnail[] Thumbnails { get; set; } = new Thumbnail[0];
	}

	public partial class Thumbnail
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("width")]
		public long Width { get; set; }

		[JsonProperty("height")]
		public long Height { get; set; }
	}


	public class Datum
	{
		public string Domain { get; set; }
		public string Reason { get; set; }
		public string Message { get; set; }
	}
	public class Error
	{
		public int Code { get; set; }
		public string Message { get; set; }
		public List<Datum> Data { get; set; } = new List<Datum>();
		public override string ToString()
		{
			var data = string.Join(" , ", Data);
			return $"Error: {Message} {Code} - {data}";
		}
	}


	public class RootConfigApiObject : RootApiObject
	{

        [JsonProperty("data")]
		public DataClass Data { get; set; }

		public partial class DataClass
		{
			[JsonProperty("entries")]
			public Entry[] Entries { get; set; } = new Entry[0];
		}

		public partial class Entry
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("key")]
			public string Key { get; set; }

			[JsonProperty("value")]
			public string Value { get; set; }
		}
	}

	public class RootSearchApiObject : RootApiObject
	{
		public ResultClass Result { get; set; }

		public class ResultClass
		{
			public string Kind { get; set; }
			public List<Entry> Entries { get; set; }

			public class Entry
			{
				public string Type { get; set; }
				public ArtistClass Artist { get; set; }
				public double Score { get; set; }
				public AlbumClass Album { get; set; }
				public SongItem Track { get; set; }
			}
		}

		public class ArtistClass
		{
			public string Kind { get; set; }
			public string Name { get; set; }
			public string ArtistArtRef { get; set; }
			public string ArtistId { get; set; }
		}

		public class AlbumClass
		{
			public string Kind { get; set; }
			public string Name { get; set; }
			public string AlbumArtist { get; set; }
			public string AlbumArtRef { get; set; }
			public string AlbumId { get; set; }
			public string Artist { get; set; }
			public List<string> ArtistId { get; set; }
			public int Year { get; set; }
		}
	}

	public class RootRadioStationsApiObject : RootApiObject
	{
		public DataClass Data { get; set; }
		public string NextPageToken { get; set; }
		public class DataClass
		{
			public List<Station> Items { get; set; }
		}
	}

	public class Station
	{

		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("clientId")]
		public string ClientId { get; set; }

		[JsonProperty("lastModifiedTimestamp")]
		public long LastModifiedTimestamp { get; set; }

		[JsonProperty("recentTimestamp")]
		public long RecentTimestamp { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("seed")]
		public SeedClass Seed { get; set; }

		[JsonProperty("imageUrls")]
		public List<ImageUrl> ImageUrls { get; set; }

		[JsonProperty("deleted")]
		public bool Deleted { get; set; }

		[JsonProperty("skipEventHistory")]
		public List<object> SkipEventHistory { get; set; }

		[JsonProperty("inLibrary")]
		public bool InLibrary { get; set; }

		[JsonProperty("compositeArtRefs")]
		public List<ArtRef> CompositeArtRefs { get; set; }

		[JsonProperty("stationSeeds")]
		public List<SeedClass> StationSeeds { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("byline")]
		public string Byline { get; set; }

		[JsonProperty("tracks")]
		public List<SongItem> Tracks { get; set; }

		public partial class SeedClass
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("artistId")]
			public string ArtistId { get; set; }

			[JsonProperty("seedType")]
			public string SeedType { get; set; }

			[JsonProperty("albumId")]
			public string AlbumId { get; set; }

			[JsonProperty("metadataSeed")]
			public SeedMetadataSeed MetadataSeed { get; set; }

			[JsonProperty("genreId")]
			public string GenreId { get; set; }

			[JsonProperty("trackId")]
			public string TrackId { get; set; }

			[JsonProperty("trackLockerId")]
			public string TrackLockerId { get; set; }

			[JsonProperty("curatedStationId")]
			public string CuratedStationId { get; set; }

			public string Id
			{
				get
				{
					if (!string.IsNullOrWhiteSpace(ArtistId))
						return ArtistId;
					if (!string.IsNullOrWhiteSpace(TrackId))
						return TrackId;
					if (!string.IsNullOrWhiteSpace(CuratedStationId))
						return CuratedStationId;

					if (!string.IsNullOrWhiteSpace(TrackLockerId))
						return TrackLockerId;

					if (!string.IsNullOrWhiteSpace(AlbumId))
						return AlbumId;

					if (!string.IsNullOrWhiteSpace(GenreId))
						return GenreId;
					return "";
				}
			}
		
		}

		public partial class SeedMetadataSeed
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("artist")]
			public Artist Artist { get; set; }

			[JsonProperty("genre")]
			public Genre Genre { get; set; }

			[JsonProperty("album")]
			public Album Album { get; set; }

			[JsonProperty("track")]
			public PurpleTrack Track { get; set; }
		}
		public partial class Album
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("albumArtist")]
			public string AlbumArtist { get; set; }

			[JsonProperty("albumArtRef")]
			public string AlbumArtRef { get; set; }

			[JsonProperty("albumId")]
			public string AlbumId { get; set; }

			[JsonProperty("artist")]
			public string Artist { get; set; }

			[JsonProperty("artistId")]
			public List<string> ArtistId { get; set; }

			[JsonProperty("description")]
			public string Description { get; set; }

			[JsonProperty("year")]
			public long Year { get; set; }

			[JsonProperty("explicitType")]
			public string ExplicitType { get; set; }

			[JsonProperty("description_attribution")]
			public DescriptionAttribution DescriptionAttribution { get; set; }
		}

		public partial class DescriptionAttribution
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("source_title")]
			public string SourceTitle { get; set; }
		}

		public partial class Artist
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("artistArtRef")]
			public string ArtistArtRef { get; set; }

			[JsonProperty("artistId")]
			public string ArtistId { get; set; }

			[JsonProperty("artistBio")]
			public string ArtistBio { get; set; }

			[JsonProperty("total_albums")]
			public long? TotalAlbums { get; set; }

			[JsonProperty("artistArtRefs")]
			public List<ArtRef> ArtistArtRefs { get; set; }

			[JsonProperty("artist_bio_attribution")]
			public ArtistBioAttribution ArtistBioAttribution { get; set; }
		}

		public partial class ArtistBioAttribution
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("source_title")]
			public string SourceTitle { get; set; }

			[JsonProperty("source_url")]
			public string SourceUrl { get; set; }

			[JsonProperty("license_title")]
			public string LicenseTitle { get; set; }

			[JsonProperty("license_url")]
			public string LicenseUrl { get; set; }
		}

		public partial class Genre
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("images")]
			public List<Image> Images { get; set; }
		}

		public partial class Image
		{
			[JsonProperty("url")]
			public string Url { get; set; }
		}

		public partial class PurpleTrack
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("clientId")]
			public string ClientId { get; set; }

			[JsonProperty("creationTimestamp")]
			public string CreationTimestamp { get; set; }

			[JsonProperty("lastModifiedTimestamp")]
			public string LastModifiedTimestamp { get; set; }

			[JsonProperty("recentTimestamp")]
			public string RecentTimestamp { get; set; }

			[JsonProperty("deleted")]
			public bool? Deleted { get; set; }

			[JsonProperty("title")]
			public string Title { get; set; }

			[JsonProperty("artist")]
			public string Artist { get; set; }

			[JsonProperty("composer")]
			public string Composer { get; set; }

			[JsonProperty("album")]
			public string Album { get; set; }

			[JsonProperty("albumArtist")]
			public string AlbumArtist { get; set; }

			[JsonProperty("year")]
			public long? Year { get; set; }

			[JsonProperty("comment")]
			public string Comment { get; set; }

			[JsonProperty("trackNumber")]
			public long TrackNumber { get; set; }

			[JsonProperty("genre")]
			public string Genre { get; set; }

			[JsonProperty("durationMillis")]
			public string DurationMillis { get; set; }

			[JsonProperty("beatsPerMinute")]
			public long? BeatsPerMinute { get; set; }

			[JsonProperty("albumArtRef")]
			public List<ArtRef> AlbumArtRef { get; set; }

			[JsonProperty("playCount")]
			public long? PlayCount { get; set; }

			[JsonProperty("totalTrackCount")]
			public long? TotalTrackCount { get; set; }

			[JsonProperty("discNumber")]
			public long DiscNumber { get; set; }

			[JsonProperty("totalDiscCount")]
			public long? TotalDiscCount { get; set; }

			[JsonProperty("rating")]
			public string Rating { get; set; }

			[JsonProperty("estimatedSize")]
			public string EstimatedSize { get; set; }

			[JsonProperty("trackType")]
			public string TrackType { get; set; }

			[JsonProperty("albumId")]
			public string AlbumId { get; set; }

			[JsonProperty("artistId")]
			public List<string> ArtistId { get; set; }

			[JsonProperty("nid")]
			public string Nid { get; set; }

			[JsonProperty("storeId")]
			public string StoreId { get; set; }

			[JsonProperty("trackAvailableForSubscription")]
			public bool? TrackAvailableForSubscription { get; set; }

			[JsonProperty("trackAvailableForPurchase")]
			public bool? TrackAvailableForPurchase { get; set; }

			[JsonProperty("albumAvailableForPurchase")]
			public bool? AlbumAvailableForPurchase { get; set; }

			[JsonProperty("explicitType")]
			public string ExplicitType { get; set; }

			[JsonProperty("primaryVideo")]
			public PrimaryVideo PrimaryVideo { get; set; }
		}

		public partial class PrimaryVideo
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("thumbnails")]
			public List<Thumbnail> Thumbnails { get; set; }
		}

		public partial class Thumbnail
		{
			[JsonProperty("url")]
			public string Url { get; set; }

			[JsonProperty("width")]
			public long Width { get; set; }

			[JsonProperty("height")]
			public long Height { get; set; }
		}


		public partial class StationSeedMetadataSeed
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("track")]
			public FluffyTrack Track { get; set; }
		}

		public partial class FluffyTrack
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("clientId")]
			public string ClientId { get; set; }

			[JsonProperty("creationTimestamp")]
			public string CreationTimestamp { get; set; }

			[JsonProperty("lastModifiedTimestamp")]
			public string LastModifiedTimestamp { get; set; }

			[JsonProperty("recentTimestamp")]
			public string RecentTimestamp { get; set; }

			[JsonProperty("deleted")]
			public bool Deleted { get; set; }

			[JsonProperty("title")]
			public string Title { get; set; }

			[JsonProperty("artist")]
			public string Artist { get; set; }

			[JsonProperty("composer")]
			public string Composer { get; set; }

			[JsonProperty("album")]
			public string Album { get; set; }

			[JsonProperty("albumArtist")]
			public string AlbumArtist { get; set; }

			[JsonProperty("year")]
			public long Year { get; set; }

			[JsonProperty("comment")]
			public string Comment { get; set; }

			[JsonProperty("trackNumber")]
			public long TrackNumber { get; set; }

			[JsonProperty("genre")]
			public string Genre { get; set; }

			[JsonProperty("durationMillis")]
			public string DurationMillis { get; set; }

			[JsonProperty("beatsPerMinute")]
			public long BeatsPerMinute { get; set; }

			[JsonProperty("albumArtRef")]
			public List<ArtRef> AlbumArtRef { get; set; }

			[JsonProperty("playCount")]
			public long PlayCount { get; set; }

			[JsonProperty("totalTrackCount")]
			public long TotalTrackCount { get; set; }

			[JsonProperty("discNumber")]
			public long DiscNumber { get; set; }

			[JsonProperty("totalDiscCount")]
			public long TotalDiscCount { get; set; }

			[JsonProperty("rating")]
			public string Rating { get; set; }

			[JsonProperty("estimatedSize")]
			public string EstimatedSize { get; set; }

			[JsonProperty("trackType")]
			public string TrackType { get; set; }

			[JsonProperty("albumId")]
			public string AlbumId { get; set; }

			[JsonProperty("artistId")]
			public List<string> ArtistId { get; set; }

			[JsonProperty("nid")]
			public string Nid { get; set; }
		}
		public partial class ImageUrl
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("url")]
			public string Url { get; set; }

			[JsonProperty("aspectRatio")]
			public int AspectRatio { get; set; }

			[JsonProperty("autogen")]
			public bool? Autogen { get; set; }

			[JsonProperty("colorStyles")]
			public ColorStyles ColorStyles { get; set; }
		}

		public partial class ColorStyles
		{
			[JsonProperty("primary")]
			public Accent Primary { get; set; }

			[JsonProperty("scrim")]
			public Accent Scrim { get; set; }

			[JsonProperty("accent")]
			public Accent Accent { get; set; }
		}

		public partial class Accent
		{
			[JsonProperty("red")]
			public long Red { get; set; }

			[JsonProperty("green")]
			public long Green { get; set; }

			[JsonProperty("blue")]
			public long Blue { get; set; }
		}


	}

	public class RootRadioStationsTracksApiObject : RootApiObject
	{
		public DataClass Data { get; set; }

		public class DataClass
		{
			public long CurrentTimestampMillis { get; set; }
			public List<Station> Stations { get; set; }
		}
	}


	public class RootCreateRadioStationsTracksApiObject : RootApiObject
	{
		public ResultClass Result { get; set; }

		public class ResultClass
		{
			public string currentTimestampMillis { get; set; }

			[JsonProperty("mutate_response")]
			public List<MutateResponseClass> MutateResponse { get; set; }

			public class MutateResponseClass
			{
				public string id { get; set; }
				public string client_id { get; set; }
				public string response_code { get; set; }
				public Station Station { get; set; }
			}
		}
	}

	public class RootOnlinePlaylistApiObject : RootApiObject
	{

		[JsonProperty("entries")]
		public List<Entry> Entries { get; set; }
		public class Item
		{
			public string Kind { get; set; }
			public string Id { get; set; }
			public long CreationTimestamp { get; set; }
			public long LastModifiedTimestamp { get; set; }
			public long RecentTimestamp { get; set; }
			public bool Deleted { get; set; }
			public string Name { get; set; }
			public string Type { get; set; }
			public string ShareToken { get; set; }
			public string OwnerName { get; set; }
			public string OwnerProfilePhotoUrl { get; set; }
			public bool AccessControlled { get; set; }
			public string ClientId { get; set; }
			//Playlist tracks only
			public string PlaylistId { get; set; }
			public string TrackId { get; set; }
			public long AbsolutePosition { get; set; }
			public string Source { get; set; }

			public SongItem Track { get; set; }
		}

		public class Entry
		{
			public string responseCode { get; set; }
			public string shareToken { get; set; }
			public List<Item> playlistEntry { get; set; } = new List<Item>();
		}


	}
	public class RootPlaylistApiObject : RootApiObject
	{
			public DataClass Data { get; set; }
			public string NextPageToken { get; set; }

		public partial class DataClass
		{
			[JsonProperty("items")]
			public List<Item> Items { get; set; } = new List<Item>();
		}

		public partial class Item
		{
			[JsonProperty("kind")]
			public string Kind { get; set; }

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("creationTimestamp")]
			public string CreationTimestamp { get; set; }

			[JsonProperty("lastModifiedTimestamp")]
			public long LastModifiedTimestamp { get; set; }

			[JsonProperty("recentTimestamp")]
			public long RecentTimestamp { get; set; }

			[JsonProperty("deleted")]
			public bool Deleted { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("type")]
			public string Type { get; set; }

			[JsonProperty("shareToken")]
			public string ShareToken { get; set; }

			[JsonProperty("ownerName")]
			public string OwnerName { get; set; }

			[JsonProperty("ownerProfilePhotoUrl")]
			public string OwnerProfilePhotoUrl { get; set; }

			[JsonProperty("accessControlled")]
			public bool AccessControlled { get; set; }

			[JsonProperty("clientId")]
			public string ClientId { get; set; }

			[JsonProperty("description")]
			public string Description { get; set; }

			//Playlist tracks only
			[JsonProperty("playlistId")]
			public string PlaylistId { get; set; }

			[JsonProperty("absolutePosition")]
			public long AbsolutePosition { get; set; }

			[JsonProperty("trackId")]
			public string TrackId { get; set; }

			[JsonProperty("source")]
			public string Source { get; set; }

			[JsonProperty("track")]
			public SongItem Track { get; set; }
		}
	}


	public class RootMutateApiObject : RootApiObject
	{
		public Result result { get; set; }


		public class MutateResponse
		{
			public string id { get; set; }
			public string client_id { get; set; }
			public string response_code { get; set; }
		}

		public class Result
		{
			public List<MutateResponse> mutate_response { get; set; }
		}
	}


	public class OAuthRootObject : RootApiObject
	{
		public string Token { get; set; }

		public long ExpiresIn { get; set; }

		public string Uri { get; set; }
	}


	public class ArtistDataResultObject : RootApiObject
	{
		public ArtistDataResult result { get; set; }


		public class ArtistDataResult
		{
			public ArtistDataResult()
			{
				albums = new List<Album>();
				topTracks = new List<TopTrack>();
				related_artists = new List<RelatedArtist>();
			}

			public string kind { get; set; }
			public string name { get; set; }
			public string artistArtRef { get; set; }
			public string artistId { get; set; }
			public string artistBio { get; set; }
			public List<Album> albums { get; set; }
			public List<TopTrack> topTracks { get; set; }
			public List<RelatedArtist> related_artists { get; set; }
			public int total_albums { get; set; }

			public class Album
			{
				public Album()
				{
					artistId = new List<string>();
				}

				public string kind { get; set; }
				public string name { get; set; }
				public string albumArtist { get; set; }
				public string albumArtRef { get; set; }
				public string albumId { get; set; }
				public string artist { get; set; }
				public List<string> artistId { get; set; }
				public int year { get; set; }
			}

			public class RelatedArtist
			{
				public string kind { get; set; }
				public string name { get; set; }
				public string artistArtRef { get; set; }
				public string artistId { get; set; }
			}

			public class TopTrack
			{
				public TopTrack()
				{
					artistId = new List<string>();
					albumArtRef = new List<ArtRef>();
				}

				public string kind { get; set; }
				public string title { get; set; }
				public string artist { get; set; }
				public string composer { get; set; }
				public string album { get; set; }
				public string albumArtist { get; set; }
				public int year { get; set; }
				public int trackNumber { get; set; }
				public string genre { get; set; }
				public double durationMillis { get; set; }
				public List<ArtRef> albumArtRef { get; set; }
				public int playCount { get; set; }
				public int discNumber { get; set; }
				public int totalDiscCount { get; set; }
				public int rating { get; set; }
				public string estimatedSize { get; set; }
				public int trackType { get; set; }
				public string storeId { get; set; }
				public string albumId { get; set; }
				public List<string> artistId { get; set; }
				public string nid { get; set; }
				public bool trackAvailableForSubscription { get; set; }
				public bool trackAvailableForPurchase { get; set; }
				public bool albumAvailableForPurchase { get; set; }
				public string contentType { get; set; }
				public PrimaryVideo primaryVideo { get; set; }
			}
		}
	}
	[Serializable]
	public class AlbumDataResultObject : RootApiObject
	{
		public AlbumDataResult result { get; set; }


		public class AlbumDataResult
		{
			public AlbumDataResult()
			{
				Tracks = new List<SongItem>();
			}

			public List<SongItem> Tracks { get; set; } 
			
		}
	}

	public class RecordPlaybackResponse : RootApiObject
	{
		public ResultClass result {get;set;}
		public class ResultClass
		{
			public List< EventResult> eventResults { get; set; }
		}
		public class EventResult
		{
			public string code { get; set; }
			public bool Success => code == "OK";
			public string eventId {get; set; }
		}
	}

	public class ArtistDetailsResponse : RootApiObject
	{
		public ResultClass Result { get; set; }

		public class ResultClass
		{
			public string kind { get; set; }
			public string name { get; set; }
			public string artistId { get; set; }
			public List<SongItem> topTracks { get; set; }
			public List<SearchResultResponse.ResultClass.Artist> related_artists { get; set; }
			public List<SearchResultResponse.ResultClass.Album> albums { get; set; }
			public int total_albums { get; set; }
		}
	}
	public class SearchResultResponse : RootApiObject
	{
		public ResultClass Result {get;set;}

		public class ResultClass
		{
			public string kind { get; set; }
			public List<Entry> entries { get; set; }
			public List<string> clusterOrder { get; set; }

			public class Entry
			{
				public string type { get; set; }
				public Artist artist { get; set; }
				public double score { get; set; }
				public bool best_result { get; set; }
				public bool navigational_result { get; set; }
				public Album album { get; set; }
				public SongItem track { get; set; }
				public Situation situation { get; set; }
				public YoutubeVideo youtube_video { get; set; }
				public Playlist playlist { get; set; }
				public Station station { get; set; }
			}
			public class ArtistArtRef
			{
				public string kind { get; set; }
				public string url { get; set; }
				public string aspectRatio { get; set; }
				public bool autogen { get; set; }
			}

			public class ArtistBioAttribution
			{
				public string kind { get; set; }
				public string source_title { get; set; }
				public string source_url { get; set; }
				public string license_title { get; set; }
				public string license_url { get; set; }
			}

			public class Artist
			{
				public string kind { get; set; }
				public string name { get; set; }
				public string artistArtRef { get; set; }
				public List<ArtistArtRef> artistArtRefs { get; set; }
				public string artistId { get; set; }
				public ArtistBioAttribution artist_bio_attribution { get; set; }
			}

			public class DescriptionAttribution
			{
				public string kind { get; set; }
				public string source_title { get; set; }
				public string source_url { get; set; }
				public string license_title { get; set; }
				public string license_url { get; set; }
			}

			public class Album
			{
				public string kind { get; set; }
				public string name { get; set; }
				public string albumArtist { get; set; }
				public string albumArtRef { get; set; }
				public string albumId { get; set; }
				public string artist { get; set; }
				public List<string> artistId { get; set; }
				public int year { get; set; }
				public DescriptionAttribution description_attribution { get; set; }
			}

			public class AlbumArtRef
			{
				public string kind { get; set; }
				public string url { get; set; }
				public string aspectRatio { get; set; }
				public bool autogen { get; set; }
			}

			public class Thumbnail
			{
				public string url { get; set; }
				public int width { get; set; }
				public int height { get; set; }
			}

			public class PrimaryVideo
			{
				public string kind { get; set; }
				public string id { get; set; }
				public List<Thumbnail> thumbnails { get; set; }
			}
			public class Situation
			{
				public string id { get; set; }
				public string title { get; set; }
				public string description { get; set; }
				public string imageUrl { get; set; }
				public string wideImageUrl { get; set; }
			}
			public class YoutubeVideo
			{
				public string kind { get; set; }
				public string id { get; set; }
				public string title { get; set; }
				public List<Thumbnail> thumbnails { get; set; }
			}

			public class Playlist
			{
				public string kind { get; set; }
				public string name { get; set; }
				public List<AlbumArtRef> albumArtRef { get; set; }
				public string type { get; set; }
				public string shareToken { get; set; }
				public string ownerName { get; set; }
				public string ownerProfilePhotoUrl { get; set; }
				public string description { get; set; }
			}
		}
	}
}