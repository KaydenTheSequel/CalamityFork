using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneHellfireball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(new Rectangle((int)base.Projectile.ai[0], (int)base.Projectile.ai[1], 20, 42)))
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 9)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 6)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.alpha > 5)
		{
			base.Projectile.alpha -= 15;
		}
		if (base.Projectile.alpha < 5)
		{
			base.Projectile.alpha = 5;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) - MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0f, 0f);
		int brimDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 170, default(Color), 1.1f);
		Main.dust[brimDust].noGravity = true;
		Dust obj = Main.dust[brimDust];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[brimDust];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 50, 50, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<HellfireExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.5f);
		return false;
	}
}
