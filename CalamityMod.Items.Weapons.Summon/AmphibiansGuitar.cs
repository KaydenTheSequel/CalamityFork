using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AmphibiansGuitar : ModItem, ILocalizedModType, IModType
{
	public override string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 64;
		base.Item.damage = 54;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.shoot = ModContent.ProjectileType<AmphibiansGuitarHoldout>();
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.useStyle = 4;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1305).AddIngredient(2121).AddIngredient<CoreofCalamity>(3)
			.AddIngredient<LivingShard>(8)
			.AddTile(134)
			.Register();
	}
}
