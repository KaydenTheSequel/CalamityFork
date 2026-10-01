using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Armor.Auric;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class AuricTeslaCuisses : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.12f;

	public static int CritBoost = 10;

	public static float MoveSpeedBoost = 0.1f;

	private bool toggleEnabled = true;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 42;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
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

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
		player.carpet = toggleEnabled;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		if (Main.LocalPlayer.Calamity().auricSet)
		{
			Main.LocalPlayer.armor[0].ModItem.ModifyTooltips(tooltips);
		}
		if (!toggleEnabled)
		{
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Tooltip2");
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GodSlayerLeggings>().AddIngredient<BloodflareCuisses>().AddIngredient<TarragonLeggings>()
			.AddIngredient(934)
			.AddIngredient<AuricBar>(15)
			.AddTile<CosmicAnvil>()
			.Register();
		CreateRecipe().AddIngredient<SilvaLeggings>().AddIngredient<BloodflareCuisses>().AddIngredient<TarragonLeggings>()
			.AddIngredient(934)
			.AddIngredient<AuricBar>(15)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
