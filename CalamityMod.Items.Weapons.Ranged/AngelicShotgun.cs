using CalamityMod.Items.Ammo;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class AngelicShotgun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 38;
		base.Item.damage = 92;
		base.Item.knockBack = 3f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.useTime = 26;
		base.Item.useAnimation = 26;
		base.Item.UseSound = SoundID.Item38;
		base.Item.useStyle = 5;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<AngelicBeam>();
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-17f, -3f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		if (type == 14)
		{
			type = ModContent.ProjectileType<HallowPointRoundProj>();
			damage += HallowPointRound.BaseDamage - 7;
		}
		int NumBullets = Main.rand.Next(5, 7);
		Vector2 baseVelocity = velocity.SafeNormalize(Vector2.Zero) * ((Vector2)(ref velocity)).Length();
		for (int i = 0; i < NumBullets; i++)
		{
			Vector2 randomVelocity = baseVelocity.RotatedByRandom(MathHelper.ToRadians(12.5f)) * Main.rand.NextFloat(0.88f, 1.12f);
			Projectile.NewProjectile(source, position + velocity.SafeNormalize(Vector2.Zero) * 16f, randomVelocity, type, damage, knockback, player.whoAmI);
		}
		bool empowered = type == ModContent.ProjectileType<HallowPointRoundProj>();
		float laserSpeed = 8f;
		int laserDamage = (int)((float)damage * (empowered ? 3.5f : 2f));
		float laserKB = knockback * 1.6f;
		Vector2 newPos = default(Vector2);
		((Vector2)(ref newPos))._002Ector(player.ClampedMouseWorld().X + Main.rand.NextFloat(-160f, 160f), player.MountedCenter.Y - 1200f);
		Vector2 newVel = (player.ClampedMouseWorld() + Main.rand.NextVector2CircularEdge(8f, 8f) - newPos).SafeNormalize(Vector2.Zero) * laserSpeed;
		Projectile.NewProjectileDirect(source, newPos, newVel, base.Item.shoot, laserDamage, laserKB, player.whoAmI).scale = (empowered ? 1.75f : 1f);
		SoundEngine.PlaySound(in SoundID.Item72, player.Center);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(824, 75).AddIngredient<DivineGeode>(15).AddIngredient<EssenceofSunlight>(7)
			.AddTile(134)
			.Register();
	}
}
