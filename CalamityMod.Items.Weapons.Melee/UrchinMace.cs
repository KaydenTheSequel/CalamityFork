using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.MaceFlails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "RedtideSword", "UrchinFlail" })]
public class UrchinMace : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 48;
		base.Item.damage = 15;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 19;
		base.Item.useStyle = 1;
		base.Item.useTime = 19;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.shoot = ModContent.ProjectileType<UrchinMaceProj>();
		base.Item.shootSpeed = 9f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(3).AddTile(16).Register();
	}
}
