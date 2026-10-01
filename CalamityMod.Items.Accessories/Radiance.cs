using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "AstralArcanum", "Purity" })]
public class Radiance : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 7));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 44;
		base.Item.defense = 5;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		player.statLifeMax2 += 70;
		if (!player.HasBuff(48))
		{
			player.AddBuff(48, 2);
		}
		modPlayer.purity = true;
		modPlayer.honeyDewHalveDebuffs = true;
		modPlayer.livingDewHalveDebuffs = true;
		if (!modPlayer.rOoze && !modPlayer.aAmpoule && !hideVisual)
		{
			Lighting.AddLight(player.Center, new Vector3(1.32f, 1.32f, 1.82f));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		if (player != null)
		{
			list.FindAndReplace("[REGEN]", player.Calamity().purityRegen.ToString("0.##"));
			list.FindAndReplace("[DEFENSE]", player.Calamity().jewelBonusDefense.ToString());
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AmbrosialAmpoule>().AddIngredient<InfectedJewel>().AddIngredient<AuricBar>(5)
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
