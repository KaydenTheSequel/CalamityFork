using System;
using CalamityMod.Items.Ammo;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HallowPointRoundProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, 0.7f, 0.5f, 0.3f);
		if (base.Projectile.timeLeft == 298)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.timeLeft <= 298 && Main.rand.NextBool(5))
		{
			int idx = Dust.NewDust(base.Projectile.Center, 1, 1, 228);
			Main.dust[idx].noGravity = true;
			Main.dust[idx].noLight = true;
			Main.dust[idx].position = base.Projectile.Center;
			Main.dust[idx].velocity = Vector2.Zero;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, Color.White);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage.Base += HallowPointRound.BonusDamageOnHit;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.Goldenrod, Color.White, Main.rand.NextFloat(0.7f, 1.2f), 12));
	}
}
