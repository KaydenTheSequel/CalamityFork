using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TheMaelstrom : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 12;
		base.Item.damage = 530;
		base.Item.useTime = 45;
		base.Item.useAnimation = 45;
		base.Item.useStyle = 5;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MaelstromHoldout>();
		base.Item.shootSpeed = 20f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.Calamity().donorItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.UnitX * (float)player.direction), ModContent.ProjectileType<MaelstromHoldout>(), 0, 0f, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TheStorm>().AddIngredient<ReaperTooth>(3).AddIngredient<DivineGeode>(20)
			.AddIngredient<Voidstone>(50)
			.AddTile(134)
			.Register();
	}
}
