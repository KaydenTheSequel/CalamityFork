using System.Collections.Generic;
using System.IO;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories;

public class StatisNinjaBelt : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	private bool toggleEnabled = true;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.accessory = true;
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

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.FindAndReplace("[TOGGLE]", toggleEnabled ? this.GetLocalizedValue("ToggleEffect") : "");
		base.ModifyTooltips(tooltips);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		player.autoJump = true;
		player.jumpSpeedBoost += 1.6f;
		player.moveSpeed += 0.1f;
		player.noFallDmg = true;
		player.blackBelt = true;
		player.dashType = 0;
		player.Calamity().DashID = StatisNinjaBeltDash.ID;
		if (toggleEnabled)
		{
			player.spikedBoots = 2;
		}
		player.accFlipper = true;
		player.Calamity().statisNinjaBelt = true;
		player.MountedCenter.ToTileCoordinates();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(984).AddIngredient(3994).AddIngredient<PurifiedGel>(25)
			.AddIngredient<Necroplasm>(5)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient(977).AddIngredient(963).AddIngredient(3995)
			.AddIngredient<PurifiedGel>(25)
			.AddIngredient<Necroplasm>(5)
			.AddTile(134)
			.Register();
	}
}
