using System;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CosmicKunai : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.5f;

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 48;
		base.Item.damage = 92;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 2;
		base.Item.useAnimation = 10;
		base.Item.reuseDelay = 1;
		base.Item.useLimitPerAnimation = 5;
		base.Item.useStyle = 1;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<CosmicKunaiProj>();
		base.Item.shootSpeed = 28f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable())
		{
			Main.projectile[stealth].Calamity().stealthStrike = true;
			Main.projectile[stealth].penetrate = 3;
			SoundEngine.PlaySound(in SoundID.Item73, player.Center);
			for (float i = 0f; i < 5f; i++)
			{
				float angle = (float)Math.PI * 2f / 5f * i;
				Projectile.NewProjectile(source, player.Center, angle.ToRotationVector2() * 8f, ModContent.ProjectileType<CosmicScythe>(), (int)((float)damage * 1.55f), knockback, player.whoAmI, angle);
			}
		}
		return false;
	}
}
