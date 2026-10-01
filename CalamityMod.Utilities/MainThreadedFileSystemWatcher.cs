using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Utilities;

internal sealed class MainThreadedFileSystemWatcher : IDisposable
{
	private sealed class MainThreadedFileSystemWatcherSystem : ILoadable
	{
		private static MainThreadedFileSystemWatcher[] _Watchers = Array.Empty<MainThreadedFileSystemWatcher>();

		private static HashSet<MainThreadedFileSystemWatcher> _WatchersList = new HashSet<MainThreadedFileSystemWatcher>();

		public static void Register(MainThreadedFileSystemWatcher watcher)
		{
			_WatchersList.Add(watcher);
			_Watchers = _WatchersList.ToArray();
		}

		public static void Unregister(MainThreadedFileSystemWatcher watcher)
		{
			_WatchersList.Remove(watcher);
			_Watchers = _WatchersList.ToArray();
		}

		void ILoadable.Load(Mod mod)
		{
			Main.OnTickForThirdPartySoftwareOnly += Tick;
		}

		void ILoadable.Unload()
		{
			Main.OnTickForThirdPartySoftwareOnly -= Tick;
			_WatchersList?.Clear();
			_Watchers = Array.Empty<MainThreadedFileSystemWatcher>();
		}

		private void Tick()
		{
			MainThreadedFileSystemWatcher[] watchers = _Watchers;
			foreach (MainThreadedFileSystemWatcher watcher in watchers)
			{
				if (watcher._HasQueueInFrame)
				{
					HandleQueuedEvents(watcher);
				}
			}
		}

		private static void HandleQueuedEvents(MainThreadedFileSystemWatcher watcher)
		{
			lock (watcher._ChangedQueue)
			{
				foreach (FileSystemEventArgs changed in watcher._ChangedQueue.Values)
				{
					watcher.Changed?.Invoke(changed);
				}
				watcher._ChangedQueue.Clear();
			}
			lock (watcher._RenamedQueue)
			{
				foreach (RenamedEventArgs renamed in watcher._RenamedQueue.Values)
				{
					watcher.Renamed?.Invoke(renamed);
				}
				watcher._RenamedQueue.Clear();
			}
			watcher._HasQueueInFrame = false;
		}
	}

	private Dictionary<string, FileSystemEventArgs> _ChangedQueue = new Dictionary<string, FileSystemEventArgs>();

	private Dictionary<string, RenamedEventArgs> _RenamedQueue = new Dictionary<string, RenamedEventArgs>();

	private FileSystemWatcher _FSW;

	private bool _HasQueueInFrame;

	private bool _Disposed;

	public string Path
	{
		get
		{
			return _FSW.Path;
		}
		set
		{
			_FSW.Path = value;
		}
	}

	public string Filter
	{
		get
		{
			return _FSW.Filter;
		}
		set
		{
			_FSW.Filter = value;
		}
	}

	public Collection<string> Filters => _FSW.Filters;

	public NotifyFilters NotifyFilter
	{
		get
		{
			return _FSW.NotifyFilter;
		}
		set
		{
			_FSW.NotifyFilter = value;
		}
	}

	public bool IncludeSubdirectories
	{
		get
		{
			return _FSW.IncludeSubdirectories;
		}
		set
		{
			_FSW.IncludeSubdirectories = value;
		}
	}

	public bool EnableRaisingEvents
	{
		get
		{
			return _FSW.EnableRaisingEvents;
		}
		set
		{
			_FSW.EnableRaisingEvents = value;
		}
	}

	public Regex FileNameFilter { get; set; }

	public event Action<FileSystemEventArgs> Changed;

	public event Action<RenamedEventArgs> Renamed;

	public MainThreadedFileSystemWatcher()
	{
		_FSW = new FileSystemWatcher();
		_FSW.Changed += delegate(object o, FileSystemEventArgs arg)
		{
			if (FileNameFilter != null && !FileNameFilter.IsMatch(System.IO.Path.GetFileName(arg.Name)))
			{
				return;
			}
			lock (_ChangedQueue)
			{
				_ChangedQueue[arg.FullPath] = arg;
				_HasQueueInFrame = true;
			}
		};
		_FSW.Renamed += delegate(object o, RenamedEventArgs arg)
		{
			if (FileNameFilter != null && !FileNameFilter.IsMatch(System.IO.Path.GetFileName(arg.Name)))
			{
				return;
			}
			lock (_RenamedQueue)
			{
				_RenamedQueue[arg.FullPath] = arg;
				_HasQueueInFrame = true;
			}
		};
		MainThreadedFileSystemWatcherSystem.Register(this);
	}

	private void Dispose(bool disposing)
	{
		if (!_Disposed)
		{
			if (disposing)
			{
				_FSW?.Dispose();
			}
			_FSW = null;
			_Disposed = true;
			_ChangedQueue?.Clear();
			_RenamedQueue?.Clear();
			_ChangedQueue = null;
			_RenamedQueue = null;
			_HasQueueInFrame = false;
			MainThreadedFileSystemWatcherSystem.Unregister(this);
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
