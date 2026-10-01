using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "PwnagehammerMelee", "PwnagehammerRogue" })]
public class Pwnagehammer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 150;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 50);
		base.Item.useStyle = 1;
		base.Item.knockBack = 10f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.shoot = ModContent.ProjectileType<PwnagehammerProj>();
		base.Item.shootSpeed = 22f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 yeetOffset = Vector2.Normalize(velocity) * 40f;
		if (Collision.CanHit(position, 0, 0, position + yeetOffset, 0, 0))
		{
			position += yeetOffset;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(367).AddIngredient(1225, 7).AddIngredient(547, 3)
			.AddIngredient(548, 3)
			.AddIngredient(549, 3)
			.AddTile(134)
			.Register();
	}
}
