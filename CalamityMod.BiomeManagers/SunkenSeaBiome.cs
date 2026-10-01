using System;
using CalamityMod.Systems;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.BiomeManagers;

public class SunkenSeaBiome : ModBiome
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("SunkenSea") ?? 43;

	public override string BestiaryIcon => "CalamityMod/BiomeManagers/SunkenSeaIcon";

	public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer1";

	public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/AbyssBGLayer1";

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		IL_Main.DrawBlack += new Manipulator(ChangeBlackThreshold);
		On_Main.DrawBlack += new hook_DrawBlack(ForceDrawBlack);
	}

	private void ForceDrawBlack(orig_DrawBlack orig, Main self, bool force)
	{
		if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<SunkenSeaBiome>()) && Main.BackgroundEnabled)
		{
			orig.Invoke(self, true);
		}
		else
		{
			orig.Invoke(self, force);
		}
	}

	private float NewThreshold(float orig)
	{
		if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<SunkenSeaBiome>()) && Main.BackgroundEnabled)
		{
			return 0.1f;
		}
		return orig;
	}

	private void ChangeBlackThreshold(ILContext il)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (Main.BackgroundEnabled)
		{
			ILCursor c = new ILCursor(il);
			c.TryGotoNext(new Func<Instruction, bool>[2]
			{
				(Instruction n) => ILPatternMatchingExt.MatchLdloc(n, 6),
				(Instruction n) => ILPatternMatchingExt.MatchStloc(n, 13)
			});
			int index = c.Index;
			c.Index = index + 1;
			c.Emit(OpCodes.Ldloc, 3);
			c.EmitDelegate<Func<float, float>>((Func<float, float>)NewThreshold);
			c.Emit(OpCodes.Stloc, 3);
		}
	}

	public override bool IsBiomeActive(Player player)
	{
		if (BiomeTileCounterSystem.SunkenSeaBurrowsTiles <= 200 && BiomeTileCounterSystem.SunkenSeaPolypTiles <= 200 && BiomeTileCounterSystem.SunkenSeaReefsTiles <= 200)
		{
			return BiomeTileCounterSystem.SunkenSeaShoresTiles > 200;
		}
		return true;
	}
}
