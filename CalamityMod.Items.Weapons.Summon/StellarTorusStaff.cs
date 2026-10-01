using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class StellarTorusStaff : ModItem, ILocalizedModType, IModType
{
	public static float EnemyDetectionDistance = 1200f;

	public static int IFrames = 10;

	public static float TimeBeforeCharging = 45f;

	public static float TimeCharging = 60f;

	public static float TimeShooting = 120f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 42;
		base.Item.damage = 142;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.knockBack = 4f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<StellarTorusBuff>();
		base.Item.shoot = ModContent.ProjectileType<StellarTorusSummon>();
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.Calamity().donorItem = true;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item15;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2Circular(2f, 2f), ModContent.ProjectileType<StellarTorusSummon>(), damage, knockback, player.whoAmI).originalDamage = base.Item.OriginalDamage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2749).AddIngredient(3459, 6).AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
