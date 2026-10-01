using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class DarkSpark : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<DarkSparkPrism>();
		base.Item.shootSpeed = 30f;
		base.Item.useStyle = 4;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3541).AddIngredient<DarkPlasma>(10).AddIngredient<RuinousSoul>(20)
			.AddIngredient<DivineGeode>(30)
			.AddTile(134)
			.Register();
	}
}
