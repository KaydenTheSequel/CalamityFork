using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AetherBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int DoubleDamageTime = 90;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public ref float BeamLength => ref base.Projectile.localAI[0];

	public bool mainBeam => base.Projectile.ai[0] == 0f;

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 6;
		base.Projectile.timeLeft = 30 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (mainBeam)
		{
			base.Projectile.damage += base.Projectile.originalDamage / 90;
		}
		else
		{
			base.Projectile.tileCollide = false;
		}
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 25, 0, 255);
		BeamLength = MathHelper.Clamp(BeamLength + 2f, 0f, 100f);
		Lighting.AddLight(base.Projectile.Center, 1f, 0f, 0.7f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 50, 200, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.DrawBeam(100f, 2f, lightColor);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer && mainBeam)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - (float)Math.PI / 8f).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 1f);
			}
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 600);
	}
}
