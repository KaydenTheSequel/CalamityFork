using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class DreadmineStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 40;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item113;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DreadmineTurret>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Voidstone>(30).AddIngredient<DepthCells>(30).AddTile(134)
			.Register();
	}
}
