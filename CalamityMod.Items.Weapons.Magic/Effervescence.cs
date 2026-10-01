using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Effervescence : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/EffervescenceFire")
	{
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle BurstSound = new SoundStyle("CalamityMod/Sounds/Item/EffervescenceBurst")
	{
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle PopSound = new SoundStyle("CalamityMod/Sounds/Item/EffervescencePop")
	{
		PitchVariance = 0.1f
	};

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 26;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.75f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = FireSound;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 13f;
		base.Item.shoot = ModContent.ProjectileType<UberBubble>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		for (int randomBullets = 0; randomBullets < 4; randomBullets++)
		{
			Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.85f, 1.2f);
			Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2623).AddIngredient(3467, 5).AddIngredient(5349, 5)
			.AddTile(134)
			.Register();
	}
}
