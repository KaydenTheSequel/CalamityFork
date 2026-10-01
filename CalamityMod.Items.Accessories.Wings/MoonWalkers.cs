using System.Collections.Generic;
using System.IO;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[]
{
	EquipType.Wings,
	EquipType.Shoes
})]
[LegacyName(new string[] { "InfinityBoots", "TracersCelestial" })]
public class MoonWalkers : BaseWings
{
	public static int wingSlot;

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
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(160, 9f, 2.6f);
		wingSlot = base.Item.wingSlot;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		base.Item.SetNameOverride(CalamityUtils.GetTextValue("Items.Accessories.Wings.MoonWalkers." + (toggleEnabled ? "DisplayName" : "TreadsName")));
		CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, toggleEnabled);
	}

	public override void UpdateInventory(Player player)
	{
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual && toggleEnabled)
		{
			int dustXOffset = 4;
			if (player.direction == 1)
			{
				dustXOffset = -40;
			}
			int flightDust = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)dustXOffset, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 107, 0f, 0f, 100, default(Color), 2.4f);
			Main.dust[flightDust].noGravity = true;
			Dust obj = Main.dust[flightDust];
			obj.velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[flightDust].fadeIn = 2f;
			}
			Main.dust[flightDust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
		CalamityPlayer modPlayer = player.Calamity();
		player.accRunSpeed = 8f;
		player.moveSpeed += 0.14f;
		player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaImmune = true;
		player.buffImmune[24] = true;
		player.noFallDmg = true;
		if (!toggleEnabled)
		{
			player.rocketBoots = (player.vanityRocketBoots = 4);
			modPlayer.angelTreads = true;
		}
		modPlayer.tracersDust = !hideVisual && toggleEnabled;
		modPlayer.moonWalkers = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AngelTreads>().AddIngredient(575, 20).AddIngredient(3467, 5)
			.AddTile(412)
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		frame = tex.Frame(2, 1, (!toggleEnabled) ? 1 : 0);
		spriteBatch.Draw(tex, position, (Rectangle?)frame, Color.White, 0f, frame.Size() * 0.5f, Main.inventoryScale * 0.8f, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		Rectangle frame = tex.Frame(2, 1, (!toggleEnabled) ? 1 : 0);
		spriteBatch.Draw(tex, base.Item.Center - Main.screenPosition, (Rectangle?)frame, lightColor, rotation, frame.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
