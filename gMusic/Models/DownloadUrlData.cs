﻿using System;
using System.Collections.Generic;
using System.Text;

namespace gMusic.Models
{
	public class DownloadUrlData
	{
		public string Url { get; set; }
		public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
	}
}