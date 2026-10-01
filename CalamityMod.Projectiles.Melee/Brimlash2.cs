using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Brimlash2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.aiStyle = 27;
		base.AIType = 156;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 120;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 90 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.5f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f);
		if (base.Projectile.timeLeft < 90)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 600f, 12f, 15f);
		}
		int redderDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100);
		Main.dust[redderDust].noGravity = true;
		Dust obj = Main.dust[redderDust];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[redderDust];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 115)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 4; i < 31; i++)
		{
			float dustX = base.Projectile.oldVelocity.X * (30f / (float)i);
			float dustY = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int deathDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - dustX, base.Projectile.oldPosition.Y - dustY), 8, 8, 235, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Main.dust[deathDust].noGravity = true;
			Dust obj = Main.dust[deathDust];
			obj.velocity *= 0.5f;
			deathDust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - dustX, base.Projectile.oldPosition.Y - dustY), 8, 8, 235, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			Dust obj2 = Main.dust[deathDust];
			obj2.velocity *= 0.05f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}
}
