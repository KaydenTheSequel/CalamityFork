using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GammaCanister : ModProjectile, ILocalizedModType, IModType
{
	public const float Gravity = 0.2f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1450f, Main.player[base.Projectile.owner]);
		if (base.Projectile.timeLeft >= 180)
		{
			base.Projectile.velocity.Y += 0.2f;
		}
		else if (potentialTarget != null)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 18f, 0.18f);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 22, 0, 255);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			for (int i = 0; i < 6; i++)
			{
				Vector2 shootVelocity = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2() * Main.rand.NextFloat(6f, 17f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + shootVelocity * 2f, shootVelocity, ModContent.ProjectileType<HomingGammaBullet>(), base.Projectile.damage, base.Projectile.knockBack * 0.4f, base.Projectile.owner);
			}
		}
	}
}
