using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[LegacyName(new string[] { "DrewsWings" })]
[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class WingsofRebirth : BaseWings
{
	public override float BonusAscentWhileFalling => 1f;

	public override float BonusAscentWhileRising => 0.17f;

	public override float RisingSpeedThreshold => 1.2f;

	public override float MaxAscentSpeed => 3.25f;

	public override float BaseAscent => 0.15f;

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(360, 11.5f, 2.9f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
		{
			int dustXOffset = 4;
			if (player.direction == 1)
			{
				dustXOffset = -40;
			}
			int flightDust = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)dustXOffset, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 244, 0f, 0f, 100, default(Color), 2.4f);
			Main.dust[flightDust].noGravity = true;
			Dust obj = Main.dust[flightDust];
			obj.velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[flightDust].fadeIn = 2f;
			}
			Main.dust[flightDust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
	}

	public override bool WingUpdate(Player player, bool inUse)
	{
		if (player.controlJump && player.wingTime > 0f && player.velocity.Y != 0f)
		{
			int frameRate = 5;
			int maxFrames = 9;
			player.wingFrameCounter++;
			if (player.wingFrame == 0)
			{
				player.wingFrame = 7;
			}
			if (player.wingFrameCounter % frameRate == 0)
			{
				player.wingFrame++;
			}
			if (player.wingFrame >= maxFrames)
			{
				player.wingFrameCounter = 0;
				player.wingFrame = 1;
			}
		}
		else
		{
			player.wingFrameCounter = 0;
			player.wingFrame = 0;
			if (player.velocity.Y != 0f)
			{
				player.wingFrame = 2;
				if (player.controlJump && player.velocity.Y > 0f)
				{
					player.wingFrame = 1;
				}
			}
		}
		return true;
	}
}
