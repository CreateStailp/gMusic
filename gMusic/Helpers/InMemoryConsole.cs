﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using gMusic.Managers;
using System.Threading.Tasks;

namespace gMusic
{
	public class InMemoryConsole : TextWriter
	{
		public static InMemoryConsole Current { get; set; } = new InMemoryConsole();
		public FixedSizedQueue<Tuple<DateTime,string>> ConsoleOutput { get; set; } = new FixedSizedQueue<Tuple<DateTime, string>>(1000);
		public InMemoryConsole()
		{
			
		}
		Task NotifierTask;
		public void Activate()
		{
			//if(!Debugger.IsAttached)
				Console.SetOut(this);
			if (NotifierTask?.IsCompleted ?? true)
			{
				NotifierTask = Task.Run(async () =>
				{
					while (true)
					{
						if (hasMessages)
						{
							NotificationManager.Shared.ProcConsoleChanged();
							hasMessages = false;
						}
						await Task.Delay(1000);
					}
				});
			}

		}

		public override Encoding Encoding => Encoding.UTF8;
		bool hasMessages = false;
		public override void WriteLine(string value)
		{
			if (!string.IsNullOrWhiteSpace(currentLine))
			{
				ConsoleOutput.Enqueue(new Tuple<DateTime, string>(DateTime.Now, currentLine));
				currentLine = "";
			}

			if (!string.IsNullOrWhiteSpace(value))
			{
				ConsoleOutput.Enqueue(new Tuple<DateTime, string>(DateTime.Now, value));
				hasMessages = true;
			}
			Debug.WriteLine(value);
		}

		string currentLine = "";
		public override void Write(string value)
		{
			base.Write(value);
			currentLine += value;
		}

		public override string ToString()
		{
			return ConsoleOutput.ToString((x) => $"{x.Item1} : {x.Item2}");
		}

	}

}
