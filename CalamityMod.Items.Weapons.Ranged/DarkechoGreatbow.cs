using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class DarkechoGreatbow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 62;
		base.Item.damage = 45;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 33;
		base.Item.useAnimation = 33;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
			int index = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[index].noDropItem = true;
		}
		int projectile = Projectile.NewProjectile(source, position, velocity, 477, damage / 2, knockback, player.whoAmI);
		Main.projectile[projectile].penetrate = 3;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(8).AddTile(134).Register();
	}
}
