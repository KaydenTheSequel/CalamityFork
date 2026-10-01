using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Nucleogenesis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 10));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 52;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.nucleogenesis = true;
		calamityPlayer.shadowMinions = true;
		calamityPlayer.holyMinions = true;
		calamityPlayer.voltaicJelly = true;
		calamityPlayer.starTaintedGenerator = true;
		player.GetKnockback<SummonDamageClass>() += 3f;
		player.GetDamage<SummonDamageClass>() += 0.15f;
		player.buffImmune[ModContent.BuffType<Shadowflame>()] = true;
		player.buffImmune[ModContent.BuffType<Irradiated>()] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StarTaintedGenerator>().AddIngredient<StatisCurse>().AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
