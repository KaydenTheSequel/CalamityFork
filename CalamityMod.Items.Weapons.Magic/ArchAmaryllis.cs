using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ArchAmaryllis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 68;
		base.Item.damage = 58;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 23;
		base.Item.useAnimation = 23;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BeamingBolt>();
		base.Item.shootSpeed = 20f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GleamingMagnolia>().AddIngredient(3457, 12).AddTile(412)
			.Register();
	}
}
