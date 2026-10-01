using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class HadarianWings : BaseWings
{
	public override float BonusAscentWhileFalling => 0.8f;

	public override float BonusAscentWhileRising => 0.15f;

	public override float RisingSpeedThreshold => 1f;

	public override float MaxAscentSpeed => 2f;

	public override float BaseAscent => 0.135f;

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(108, 9f, 2f, hasHoldDownHoverFeatures: true, 11.6f, 11.6f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 24;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if (!player.controlJump || !(player.wingTime > 0f) || player.jump != 0 || player.velocity.Y == 0f || hideVisual)
		{
			return;
		}
		float xOffset = 4f;
		if (player.direction == 1)
		{
			xOffset = -40f;
		}
		if (!player.TryingToHoverDown || Main.rand.NextBool(3))
		{
			int idx = Dust.NewDust(new Vector2(player.Center.X + xOffset, player.Center.Y - 15f), 30, 30, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[idx].fadeIn = 2f;
			}
			Main.dust[idx].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
	}

	public override void AdditionalFlightMovement(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		if (player.TryingToHoverDown && player.controlJump && player.wingTime > 0f && !player.merman)
		{
			player.wingTime += 0.5f;
			player.velocity.Y *= 0.8f;
			if (player.velocity.Y > -2f && player.velocity.Y < 1f)
			{
				player.velocity.Y = 1E-05f;
			}
			ascentWhenFalling *= 0f;
			ascentWhenRising *= 0f;
			constantAscend *= 0f;
		}
	}
}
