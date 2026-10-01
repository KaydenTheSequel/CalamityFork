using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class UndinesRetributionSpear : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 420;
	}

	public override void AI()
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		float velocityMult = 25f * base.Projectile.ai[1];
		float velocityScale = 5f * base.Projectile.ai[1];
		float homingRange = 500f;
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.1f, 0.7f);
		if (Main.player[base.Projectile.owner].active && !Main.player[base.Projectile.owner].dead)
		{
			if (base.Projectile.Distance(Main.player[base.Projectile.owner].Center) > homingRange)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[base.Projectile.owner].Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (velocityMult - 1f) + moveDirection * velocityScale) / velocityMult;
			}
			else
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, 9f, 20f);
			}
		}
		else if (base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 2;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
		}
		for (int j = 0; j < 6; j++)
		{
			int undust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 186, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
			Main.dust[undust].noGravity = true;
			Dust obj = Main.dust[undust];
			obj.velocity *= 3f;
			undust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 186, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			Dust obj2 = Main.dust[undust];
			obj2.velocity *= 2f;
			Main.dust[undust].noGravity = true;
		}
	}
}
