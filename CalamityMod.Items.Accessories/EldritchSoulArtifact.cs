using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Plates;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class EldritchSoulArtifact : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 58;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().eArtifact = true;
		player.buffImmune[ModContent.BuffType<WhisperingDeath>()] = true;
		player.GetAttackSpeed<MeleeDamageClass>() += 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Necroplasm>(5).AddIngredient<Navyplate>(25).AddIngredient<ExodiumCluster>(25)
			.AddTile(134)
			.Register();
	}
}
