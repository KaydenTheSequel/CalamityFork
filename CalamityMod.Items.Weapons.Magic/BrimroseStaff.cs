using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class BrimroseStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 34;
		base.Item.damage = 45;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 6;
		base.Item.useTime = 16;
		base.Item.useAnimation = 16;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item43;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BrimroseBeam>();
		base.Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(6).AddTile(134).Register();
	}
}
