using System.Collections.Generic;
using CalamityMod.Particles;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class ElysianWings : BaseWings
{
	public override float BonusAscentWhileFalling => 1f;

	public override float BonusAscentWhileRising => 0.17f;

	public override float RisingSpeedThreshold => 1.2f;

	public override float MaxAscentSpeed => 3f;

	public override float BaseAscent => 0.15f;

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(240, 10f, 3f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 48;
		base.Item.height = 50;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void UpdateVanity(Player player)
	{
		DrawWingEffects(player);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			DrawWingEffects(player);
		}
	}

	private void DrawWingEffects(Player player)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		float rate = Main.GlobalTimeWrappedHourly * 2f;
		List<Color> eColors = new List<Color>
		{
			Color.Gold,
			Color.Khaki
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color val = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		Color usedColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		Vector2 spawnPos = player.Center + new Vector2((float)(-25 * player.direction), 0f);
		Lighting.AddLight(spawnPos, ((Color)(ref usedColor)).ToVector3() * 1.2f);
		if (!(player.wingTime > 0f) || player.jump != 0 || player.velocity.Y == 0f)
		{
			return;
		}
		spawnPos = player.Center + new Vector2((float)(-25 * player.direction), 0f) + Main.rand.NextVector2Circular(20f, 20f);
		Vector2 spawnPos2 = player.Center + new Vector2((float)(15 * player.direction), 0f) + Main.rand.NextVector2Circular(20f, 20f);
		float partScale = Main.rand.NextFloat(0.3f, 0.8f);
		Vector2 partVel = Utils.RotatedBy(new Vector2(0f, 5f), (double)(0.5f * (float)player.direction), default(Vector2)).RotatedByRandom(0.5) * Main.rand.NextFloat(0.5f, 0.8f);
		GeneralParticleHandler.SpawnParticle(new CustomSpark(spawnPos, partVel, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 13, partScale * 0.25f, usedColor * 0.4f, Vector2.One));
		if (Main.rand.NextBool(player.controlJump ? 2 : 4))
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(spawnPos, partVel, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 19, partScale, Main.rand.NextBool(4) ? Color.Khaki : Color.Goldenrod, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
		}
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(spawnPos2, partVel, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 13, partScale * 0.15f, usedColor * 0.4f, Vector2.One));
			if (Main.rand.NextBool(player.controlJump ? 2 : 4))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(spawnPos2, partVel, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 19, partScale * 0.7f, Main.rand.NextBool(4) ? Color.Khaki : Color.Goldenrod, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
			}
		}
	}
}
