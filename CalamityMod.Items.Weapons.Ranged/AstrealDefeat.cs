using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class AstrealDefeat : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 78;
		base.Item.damage = 160;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 4;
		base.Item.useAnimation = 20;
		base.Item.useLimitPerAnimation = 5;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item102;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AstrealArrow>();
		base.Item.shootSpeed = 4f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.consumeAmmoOnLastShotOnly = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float speed = ((Vector2)(ref velocity)).Length();
		if (speed > 8f)
		{
			velocity *= 8f / speed;
		}
		type = base.Item.shoot;
		float aiVar = Main.rand.Next(4);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, aiVar);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3779).AddIngredient(3052).AddIngredient(3467, 5)
			.AddIngredient<AshesofCalamity>(5)
			.AddTile(134)
			.Register();
	}
}
