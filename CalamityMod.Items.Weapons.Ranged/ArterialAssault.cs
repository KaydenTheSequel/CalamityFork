using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class ArterialAssault : ModItem, ILocalizedModType, IModType
{
	private int shotNum;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 100;
		base.Item.damage = 256;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 6;
		base.Item.useAnimation = 40;
		base.Item.reuseDelay = 10;
		base.Item.useLimitPerAnimation = 5;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.25f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item102;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 30f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		float rotateBy = 0.75f * (float)(shotNum * 3 % 5 - 2);
		position += velocity.SafeNormalize(Vector2.Zero).RotatedBy(rotateBy) * 64f;
		velocity = position.DirectionTo(Main.MouseWorld) * ((Vector2)(ref velocity)).Length() * 2f;
		type = ModContent.ProjectileType<BloodfireArrowProj>();
		Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
		projectile.noDropItem = true;
		projectile.tileCollide = false;
		(projectile.ModProjectile as BloodfireArrowProj).DisableEffects = true;
		projectile.Calamity().conditionalHomingRange = 175f;
		projectile.Calamity().BloodstoneOrbValue = 15;
		shotNum++;
		if (shotNum > 4)
		{
			shotNum = 0;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(5).AddTile(134).Register();
	}
}
