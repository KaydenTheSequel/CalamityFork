using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class DoGExtremeGravity : ModBuff
{
	public static int MaxFlightTimeCap = 400;

	public static float FlightTimeLossPercent = 0.25f;

	public override LocalizedText Description => base.Description.WithFormatArgs(FlightTimeLossPercent.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().DoGExtremeGravity = true;
		if (player.miscCounter % 10 == 0)
		{
			float halfWidth = (float)player.width * 0.5f;
			float halfHeight = (float)player.height * 0.5f;
			for (int i = 0; i < 1; i++)
			{
				StatChangeArrow statChangeArrow = new StatChangeArrow(player.Center + new Vector2(Main.rand.NextFloat(0f - halfWidth, halfWidth), Main.rand.NextFloat(0f - halfHeight, halfHeight)), -(Vector2.UnitY * 5f).RotatedByRandom(1.0), (float)Math.PI / 2f, Color.Fuchsia, Color.Fuchsia * 0f, 0.75f, 60);
				statChangeArrow.AffectedByGravity = true;
				GeneralParticleHandler.SpawnParticle(statChangeArrow);
			}
		}
	}
}
