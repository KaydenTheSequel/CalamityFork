using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CursedDagger : RogueWeapon
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/CursedDaggerThrow")
	{
		Volume = 0.3f,
		PitchVariance = 0.4f
	};

	public override float StealthDamageMultiplier => 0.75f;

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 48;
		base.Item.damage = 45;
		base.Item.useAnimation = (base.Item.useTime = 18);
		base.Item.shootSpeed = 19f;
		base.Item.knockBack = 4.5f;
		base.Item.shoot = ModContent.ProjectileType<CursedDaggerProj>();
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useStyle = 1;
		base.Item.UseSound = ThrowSound;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(23f)) * 0.6f;
		Vector2 newVel2 = velocity.RotatedByRandom(MathHelper.ToRadians(23f)) * 0.8f;
		if (!player.Calamity().StealthStrikeAvailable())
		{
			Projectile.NewProjectile(source, position, newVel, type, damage / 2, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, newVel2, type, damage / 2, knockback, player.whoAmI);
		}
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
				Main.projectile[stealth].usesLocalNPCImmunity = true;
			}
			return false;
		}
		return true;
	}
}
