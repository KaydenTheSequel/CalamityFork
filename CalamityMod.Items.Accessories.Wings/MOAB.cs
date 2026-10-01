using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class MOAB : BaseWings
{
	public static int wingSlot = 0;

	public static float BoostPower = 2f;

	public override float BonusAscentWhileFalling => 0.75f;

	public override float BonusAscentWhileRising => 0.15f;

	public override float RisingSpeedThreshold => 1f;

	public override float MaxAscentSpeed => 2.5f;

	public override float BaseAscent => 0.125f;

	private bool toggleEnabled
	{
		get
		{
			return base.Item.wingSlot != -1;
		}
		set
		{
			if (value)
			{
				base.Item.wingSlot = wingSlot;
			}
			else
			{
				base.Item.wingSlot = -1;
			}
		}
	}

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(75, 6.5f);
		wingSlot = base.Item.wingSlot;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		if (!toggleEnabled)
		{
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Tooltip0");
		}
		base.ModifyTooltips(tooltips);
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Main.keyState.PressingShift();
	}

	public override void RightClick(Player player)
	{
		toggleEnabled = !toggleEnabled;
		base.Item.NetStateChanged();
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("toggleEffect", toggleEnabled);
	}

	public override void LoadData(TagCompound tag)
	{
		toggleEnabled = tag.GetBool("toggleEffect");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(toggleEnabled);
	}

	public override void NetReceive(BinaryReader reader)
	{
		toggleEnabled = reader.ReadBoolean();
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, toggleEnabled);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		player.DisableWingFlapSound();
		if (toggleEnabled && player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
		{
			player.rocketDelay2--;
			if (player.rocketDelay2 <= 0)
			{
				SoundEngine.PlaySound(in SoundID.Item13, player.Center);
				player.rocketDelay2 = 60;
			}
			int dustAmt = 2;
			if (player.controlUp)
			{
				dustAmt = 4;
			}
			for (int index = 0; index < dustAmt; index++)
			{
				int type = 6;
				if (player.head == 41)
				{
					_ = player.body;
				}
				float scale = 1.75f;
				int alpha = 100;
				float xStart = player.Center.X + 16f;
				if (player.direction > 0)
				{
					xStart = player.Center.X - 26f;
				}
				float yStart = player.position.Y + (float)player.height - 18f;
				if (index == 1 || index == 3)
				{
					xStart = player.Center.X + 8f;
					if (player.direction > 0)
					{
						xStart = player.Center.X - 20f;
					}
					yStart += 6f;
				}
				if (index > 1)
				{
					yStart += player.velocity.Y;
				}
				int boosterDust = Dust.NewDust(new Vector2(xStart, yStart), 8, 8, type, 0f, 0f, alpha, default(Color), scale);
				Dust dust = Main.dust[boosterDust];
				dust.velocity.X *= 0.1f;
				dust.velocity.Y = Main.dust[boosterDust].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f;
				dust.noGravity = true;
				dust.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
				if (dustAmt == 4)
				{
					dust.velocity.Y += 6f;
				}
			}
		}
		player.GetJumpState(ExtraJump.CloudInABottle).Enable();
		player.GetJumpState(ExtraJump.SandstormInABottle).Enable();
		player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
		player.jumpBoost = true;
		player.autoJump = true;
		player.jumpSpeedBoost += 1.6f;
		player.noFallDmg = true;
		player.Calamity().calamityBonusLuck += 0.05f;
	}

	public override void AdditionalFlightMovement(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		if (player.TryingToHoverUp)
		{
			ascentWhenFalling *= BoostPower;
			ascentWhenRising *= BoostPower;
			maxCanAscendMultiplier *= BoostPower;
			maxAscentMultiplier *= BoostPower;
			constantAscend *= BoostPower;
			player.wingTime -= BoostPower - 1f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2423).AddIngredient(5331).AddIngredient(748)
			.AddIngredient(547)
			.AddIngredient(548)
			.AddIngredient(549)
			.AddTile(134)
			.Register();
	}
}
