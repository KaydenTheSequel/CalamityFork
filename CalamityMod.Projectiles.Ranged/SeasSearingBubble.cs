using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SeasSearingBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 480;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		if (base.Projectile.timeLeft < 475)
		{
			for (int i = 0; i < 2; i++)
			{
				float dustVelX = base.Projectile.velocity.X / 3f * (float)i;
				float dustVelY = base.Projectile.velocity.Y / 3f * (float)i;
				int four = 4;
				int dustID = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)four, base.Projectile.position.Y + (float)four), base.Projectile.width - four * 2, base.Projectile.height - four * 2, 202, 0f, 0f, 150, new Color(60, Main.DiscoG, 190), 1.2f);
				Dust obj = Main.dust[dustID];
				obj.noGravity = true;
				obj.velocity *= 0.1f;
				obj.velocity += base.Projectile.velocity * 0.1f;
				obj.position.X -= dustVelX;
				obj.position.Y -= dustVelY;
			}
			if (Main.rand.NextBool(10))
			{
				int otherFour = 4;
				int otherDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)otherFour, base.Projectile.position.Y + (float)otherFour), base.Projectile.width - otherFour * 2, base.Projectile.height - otherFour * 2, 202, 0f, 0f, 150, new Color(60, Main.DiscoG, 190), 0.6f);
				Dust obj2 = Main.dust[otherDust];
				obj2.velocity *= 0.25f;
				Dust obj3 = Main.dust[otherDust];
				obj3.velocity += base.Projectile.velocity * 0.5f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(60, Main.DiscoG, 190, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item96, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 202, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 0, new Color(60, Main.DiscoG, 190));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center);
		target.AddBuff(103, 300);
		target.AddBuff(70, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center);
		target.AddBuff(103, 300);
		target.AddBuff(70, 180);
	}

	private void OnHitEffects(Vector2 targetPos)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item96, base.Projectile.Center);
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int x = 0; x < 2; x++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				float angle = Main.rand.NextFloat((float)Math.PI * 2f);
				Projectile projectile = CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 1000f, 1400f, 80f, 900f, Main.rand.NextFloat(20f, 25f), ModContent.ProjectileType<SeasSearingBubble>(), base.Projectile.damage / 2, 1f, base.Projectile.owner);
				projectile.rotation = angle;
				projectile.tileCollide = false;
			}
		}
	}
}
