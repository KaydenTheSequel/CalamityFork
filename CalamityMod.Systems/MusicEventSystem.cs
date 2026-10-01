using System;
using System.Collections.Generic;
using System.Threading;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Systems;

public class MusicEventSystem : ModSystem
{
	public static MusicEventEntry CurrentEvent { get; set; } = null;

	public static DateTime? TrackStart { get; set; } = null;

	public static DateTime? TrackEnd { get; set; } = null;

	public static int LastPlayedEvent { get; set; } = -1;

	public static TimeSpan? OutroSilence { get; set; } = null;

	public static bool NoFade { get; set; } = false;

	public static Thread EventTrackerThread { get; set; } = null;

	public static HashSet<string> PlayedEvents { get; set; } = new HashSet<string>();

	public static List<MusicEventEntry> EventCollection { get; set; } = new List<MusicEventEntry>();

	private static bool oldWorld { get; set; } = true;

	public override void OnModLoad()
	{
		AddEntry("CloneDefeated", "Interlude1", TimeSpan.FromSeconds(214.577), () => DownedBossSystem.downedCalamitasClone, () => CalamityClientConfig.Instance.Interludes);
		TimeSpan length = TimeSpan.FromSeconds(191.912);
		Func<bool> shouldPlay = () => NPC.downedMoonlord;
		Func<bool> enabled = () => CalamityClientConfig.Instance.Interludes;
		TimeSpan? outroSilence = TimeSpan.FromSeconds(1.0);
		AddEntry("MLDefeated", "Interlude2", length, shouldPlay, enabled, null, outroSilence);
		AddEntry("YharonDefeated", "Interlude3", TimeSpan.FromSeconds(295.932), () => DownedBossSystem.downedYharon, () => CalamityClientConfig.Instance.Interludes);
		AddEntry("DoGDefeated", "DevourerofGodsEulogy", TimeSpan.FromSeconds(203.62), () => DownedBossSystem.downedDoG, () => CalamityClientConfig.Instance.DevourerofGodsEulogy, TimeSpan.FromSeconds(7.5));
		AddEntry("ScalDefeated", "CalamitasDefeat_LongFade", TimeSpan.FromSeconds(58.689), () => CalamityGlobalNPC.SCalAcceptance != -1, () => true);
		static void AddEntry(string eventId, string songName, TimeSpan length2, Func<bool> shouldPlay2, Func<bool> enabled2, TimeSpan? introSilence = null, TimeSpan? timeSpan = null)
		{
			MusicEventEntry entry = new MusicEventEntry(eventId, CalamityMod.Instance.GetMusicFromMusicMod(songName).Value, length2, introSilence ?? TimeSpan.Zero, timeSpan ?? TimeSpan.Zero, shouldPlay2, enabled2);
			EventCollection.Add(entry);
		}
	}

	public override void Unload()
	{
		EventCollection.Clear();
	}

	public override void PostUpdateTime()
	{
		if (BossRushEvent.BossRushActive)
		{
			foreach (MusicEventEntry entry in EventCollection)
			{
				if (entry.ShouldPlay())
				{
					PlayedEvents.Add(entry.Id);
				}
			}
			TrackStart = null;
			LastPlayedEvent = -1;
			OutroSilence = null;
			TrackEnd = null;
			CurrentEvent = null;
			return;
		}
		if (oldWorld)
		{
			foreach (MusicEventEntry entry2 in EventCollection)
			{
				if (entry2.ShouldPlay())
				{
					PlayedEvents.Add(entry2.Id);
				}
			}
			oldWorld = false;
		}
		if (TrackEnd.HasValue)
		{
			TimeSpan silence = OutroSilence.Value;
			if (DateTime.Now - TrackEnd.Value < silence)
			{
				Main.musicBox2 = MusicLoader.GetMusicSlot(base.Mod, "Sounds/Music/Silence");
				return;
			}
			LastPlayedEvent = -1;
			TrackEnd = null;
			OutroSilence = null;
			return;
		}
		if ((object)CurrentEvent == null)
		{
			foreach (MusicEventEntry musicEvent in EventCollection)
			{
				if (PlayedEvents.Contains(musicEvent.Id) || !musicEvent.ShouldPlay())
				{
					continue;
				}
				PlayedEvents.Add(musicEvent.Id);
				if (Main.dedServ || musicEvent.Enabled())
				{
					CurrentEvent = musicEvent;
					TrackStart = DateTime.Now + musicEvent.IntroSilence;
					if (!Main.dedServ)
					{
						EventTrackerThread = new Thread(WatchMusicEvent);
						EventTrackerThread.Start();
					}
					break;
				}
			}
		}
		if (!TrackStart.HasValue)
		{
			return;
		}
		if (TrackStart > DateTime.Now)
		{
			Main.musicBox2 = MusicLoader.GetMusicSlot(base.Mod, "Sounds/Music/Silence");
			NoFade = true;
			return;
		}
		Main.musicBox2 = CurrentEvent.Song;
		if (NoFade)
		{
			Main.musicFade[CurrentEvent.Song] = 1f;
			NoFade = false;
		}
		DateTime now = DateTime.Now;
		DateTime? trackStart = TrackStart;
		if (now - trackStart >= CurrentEvent.Length)
		{
			Main.musicBox2 = MusicLoader.GetMusicSlot(base.Mod, "Sounds/Music/Silence");
			Main.musicFade[CurrentEvent.Song] = 0f;
			TrackEnd = DateTime.Now;
			LastPlayedEvent = CurrentEvent.Song;
			OutroSilence = CurrentEvent.OutroSilence;
			TrackStart = null;
			CurrentEvent = null;
		}
	}

	public static void WatchMusicEvent()
	{
		DateTime? minimized = null;
		while ((object)CurrentEvent != null)
		{
			bool musicPaused = !((Game)Main.instance).IsActive;
			if (musicPaused && !minimized.HasValue)
			{
				minimized = DateTime.Now;
			}
			else if (!musicPaused && minimized.HasValue)
			{
				TrackStart += DateTime.Now - minimized.Value;
				minimized = null;
			}
		}
		EventTrackerThread = null;
	}

	public override void SaveWorldData(TagCompound tag)
	{
		tag["calamityPlayedMusicEventCount"] = PlayedEvents.Count;
		int i = 0;
		foreach (string playedEvent in PlayedEvents)
		{
			tag[$"calamityPlayedMusicEvent{i++}"] = playedEvent;
		}
	}

	public override void LoadWorldData(TagCompound tag)
	{
		PlayedEvents.Clear();
		if (tag.TryGet<int>("calamityPlayedMusicEventCount", out var playedMusicEventCount))
		{
			for (int i = 0; i < playedMusicEventCount; i++)
			{
				if (tag.TryGet<string>($"calamityPlayedMusicEvent{i}", out var playedEvent))
				{
					PlayedEvents.Add(playedEvent);
				}
			}
		}
		oldWorld = false;
	}

	public override void OnWorldUnload()
	{
		oldWorld = true;
		TrackStart = null;
		TrackEnd = null;
		CurrentEvent = null;
		PlayedEvents.Clear();
		NoFade = false;
		LastPlayedEvent = -1;
	}

	public static void SendSyncRequest()
	{
		MusicEventSyncRequestPacket.Send();
	}
}
