using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class GleamingMagnolia : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 54;
		base.Item.damage = 53;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 11;
		base.Item.useTime = 27;
		base.Item.useAnimation = 27;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<GleamingBolt>();
		base.Item.shootSpeed = 14f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ManaRose>().AddIngredient(1225, 5).AddTile(134)
			.Register();
	}
}
