using System;
using System.IO;
using CalamityMod.FluidSimulation;
using CalamityMod.Items;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using log4net;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod;

public class CalamityMod : Mod
{
	public static bool ExternalFlag_DisableDefenseDamage;

	private static CalamityMod _Instance;

	internal static CalamityMod Instance => _Instance ?? (_Instance = ModContent.GetInstance<CalamityMod>());

	internal static ILog Log => Instance.Logger;

	public static Season CurrentSeason
	{
		get
		{
			DateTime date = DateTime.Now;
			int day = date.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(date.Year) && date.DayOfYear > 59);
			if (day < 80 || day >= 355)
			{
				return Season.Winter;
			}
			if (day >= 80 && day < 172)
			{
				return Season.Spring;
			}
			if (day >= 172 && day < 266)
			{
				return Season.Summer;
			}
			return Season.Fall;
		}
	}

	public override void Load()
	{
		NPCStats.LoadDebuffs();
		CalamityGlobalItem.LoadTweaks();
		CalamityGlobalProjectile.LoadTweaks();
		if (!Main.dedServ)
		{
			Main.QueueMainThreadAction(delegate
			{
				Main.OnPreDraw += PrepareRenderTargets;
			});
		}
	}

	public override void Unload()
	{
		NPCStats.UnloadDebuffs();
		CalamityGlobalItem.UnloadTweaks();
		CalamityGlobalProjectile.UnloadTweaks();
		Main.QueueMainThreadAction(delegate
		{
			Main.OnPreDraw -= PrepareRenderTargets;
		});
		_Instance = null;
		base.Unload();
	}

	public static void PrepareRenderTargets(GameTime gameTime)
	{
		DeathAshParticle.PrepareRenderTargets();
		FluidFieldManager.Update();
	}

	public int? GetMusicFromMusicMod(string songFilename)
	{
		if (!ExternalMods.MusicAvailable)
		{
			return null;
		}
		return MusicLoader.GetMusicSlot(ExternalMods.musicMod, "Sounds/Music/" + songFilename);
	}

	public int? GetMusicFromVCMM(string songPath)
	{
		if (!ExternalMods.VCMMAvailable)
		{
			return null;
		}
		return MusicLoader.GetMusicSlot(ExternalMods.vcmm, "Assets/" + songPath);
	}

	public override object Call(params object[] args)
	{
		return ModCalls.Call(args);
	}

	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		CalamityNetcode.HandlePacket(this, reader, whoAmI);
	}
}
