using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class BelladonnaSpiritStaff : ModItem, ILocalizedModType, IModType
{
	public const float EnemyDistanceDetection = 1200f;

	public const float FireRate = 75f;

	public const float PetalTimeBeforeTargetting = 60f;

	public const float PetalVelocity = 20f;

	public const float PetalGravityStrenght = 0.2f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 42;
		base.Item.damage = 22;
		base.Item.knockBack = 1f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<BelladonnaSpiritBuff>();
		base.Item.shoot = ModContent.ProjectileType<BelladonnaSpirit>();
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item44;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(210, 4).AddIngredient(331, 5).AddIngredient(209, 8)
			.AddIngredient(620, 25)
			.AddTile(16)
			.Register();
	}
}
