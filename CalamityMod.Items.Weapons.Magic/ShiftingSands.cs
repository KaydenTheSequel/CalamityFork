using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ShiftingSands : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 58);
		base.Item.damage = 81;
		base.Item.knockBack = 5f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.mana = 20;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 7f;
		base.Item.shoot = ModContent.ProjectileType<ShiftingSandsProj>();
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item20;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override bool CanUseItem(Player player)
	{
		return !player.channel;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(113).AddIngredient(3794, 5).AddIngredient<GrandScale>()
			.AddTile(134)
			.Register();
	}
}
