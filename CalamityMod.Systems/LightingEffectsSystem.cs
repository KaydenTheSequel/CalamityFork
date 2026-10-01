using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.Skies;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class LightingEffectsSystem : ModSystem
{
	public const float MaxSignusDarkness = -0.4f;

	public const float MaxGFBSignusDarkness = -0.8f;

	public const float MaxAbyssDarkness = -0.7f;

	public override void ModifyLightingBrightness(ref float scale)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = Main.LocalPlayer.Calamity();
		float darkRatio = MathHelper.Clamp(calamityPlayer.caveDarkness, 0f, 1f);
		if (calamityPlayer.ZoneAbyss)
		{
			scale += -0.7f * darkRatio;
		}
		if (CalamityWorld.revenge && CalamityGlobalNPC.signus != -1 && Main.npc[CalamityGlobalNPC.signus].active && Vector2.Distance(Main.LocalPlayer.Center, Main.npc[CalamityGlobalNPC.signus].Center) <= 5200f)
		{
			float num = 1f - (float)(Main.npc[CalamityGlobalNPC.signus].life / Main.npc[CalamityGlobalNPC.signus].lifeMax);
			float multiplier = 1f;
			darkRatio = MathHelper.Clamp(num * multiplier, 0f, 1f);
			scale += (Main.zenithWorld ? (-0.8f) : (-0.4f)) * darkRatio;
		}
	}

	public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu)
		{
			BossRushEvent.StartTimer = 0;
		}
		float bossRushWhiteFade = (float)BossRushEvent.StartTimer / 120f;
		if (BossRushSky.ShouldDrawRegularly)
		{
			bossRushWhiteFade = 1f;
		}
		if (BossRushEvent.BossRushActive || BossRushEvent.StartTimer > 0 || BossRushSky.ShouldDrawRegularly)
		{
			float fadeToBlack = BossRushEvent.WhiteDimness;
			backgroundColor = Color.Lerp(backgroundColor, Color.LightGray, bossRushWhiteFade);
			backgroundColor = Color.Lerp(backgroundColor, Color.Black, fadeToBlack);
			tileColor = Color.Lerp(tileColor, Color.LightGray, bossRushWhiteFade);
			tileColor = Color.Lerp(tileColor, Color.Black, fadeToBlack);
			Main.ColorOfTheSkies = Color.Lerp(Main.ColorOfTheSkies, Color.Gray, bossRushWhiteFade);
			Main.ColorOfTheSkies = Color.Lerp(Main.ColorOfTheSkies, Color.Black, fadeToBlack);
		}
		else if (SkyManager.Instance["CalamityMod:ExoMechs"].IsActive())
		{
			float intensity = SkyManager.Instance["CalamityMod:ExoMechs"].Opacity;
			backgroundColor = Color.Lerp(backgroundColor, Color.DarkGray, intensity * 0.9f);
			backgroundColor = Color.Lerp(backgroundColor, Color.Black, intensity * 0.67f);
			tileColor = Color.Lerp(tileColor, Color.DarkGray, intensity * 0.8f);
			tileColor = Color.Lerp(tileColor, Color.Black, intensity * 0.3f);
			Main.ColorOfTheSkies = Color.Lerp(Main.ColorOfTheSkies, Color.DarkGray, intensity * 0.9f);
			Main.ColorOfTheSkies = Color.Lerp(Main.ColorOfTheSkies, Color.Black, intensity * 0.65f);
		}
		else
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer != null && localPlayer.Calamity()?.monolithAstralShader > 0)
			{
				float intensity2 = SkyManager.Instance["CalamityMod:Astral"].Opacity;
				backgroundColor = Color.Lerp(backgroundColor, Color.Purple, intensity2 * 0.4f);
				backgroundColor = Color.Lerp(backgroundColor, Color.Black, intensity2 * 0.75f);
			}
		}
	}
}
