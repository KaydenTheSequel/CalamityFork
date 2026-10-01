using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class RustyDrone : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 30;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		base.Projectile.velocity = -Vector2.UnitY * (float)Math.Sin((float)Math.PI * 2f * (float)base.Projectile.timeLeft / 96f) * 3f;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1000f, Owner);
		if (potentialTarget != null)
		{
			base.Projectile.spriteDirection = (base.Projectile.Center.X < potentialTarget.Center.X).ToDirectionInt();
		}
		if (base.Projectile.timeLeft % 120 != 60)
		{
			return;
		}
		SoundStyle style = SoundID.DD2_WitherBeastAuraPulse with
		{
			Volume = 1.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int pulse = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<RustyBeaconPulse>(), base.Projectile.damage, 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(pulse))
			{
				Main.projectile[pulse].originalDamage = base.Projectile.originalDamage;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
