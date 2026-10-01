using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SpatialSpear : ModProjectile, ILocalizedModType, IModType
{
	private int TimeLeft = (Main.zenithWorld ? 300 : 180);

	private int TotalSplits = (Main.zenithWorld ? 30 : 6);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = TimeLeft;
		base.Projectile.tileCollide = !Main.zenithWorld;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.05f, 1f, 0.05f);
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 4f;
		if (base.Projectile.timeLeft < TimeLeft / 2)
		{
			base.Projectile.velocity.Y += 0.16f;
			if (base.Projectile.velocity.Y > 16f)
			{
				base.Projectile.velocity.Y = 16f;
			}
			base.Projectile.velocity.X *= 0.995f;
		}
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.scale -= 0.01f;
			base.Projectile.alpha += 15;
			if (base.Projectile.alpha >= 125)
			{
				base.Projectile.alpha = 130;
				base.Projectile.localAI[1] = 1f;
			}
		}
		else if (base.Projectile.localAI[1] == 1f)
		{
			base.Projectile.scale += 0.01f;
			base.Projectile.alpha -= 15;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.localAI[1] = 0f;
			}
		}
		int dust = Dust.NewDust(base.Projectile.oldPosition + base.Projectile.oldVelocity, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, default(Color), 1.25f);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
		Main.dust[dust].noLightEmittence = true;
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] >= (float)(TimeLeft / TotalSplits)))
		{
			return;
		}
		base.Projectile.localAI[0] = 0f;
		int numProj = 2;
		float rotation = MathHelper.ToRadians(5f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<SpatialSpear2>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(128, 255, 128);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > TimeLeft - 5)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int i = 4; i < 31; i++)
		{
			float projOldX = base.Projectile.oldVelocity.X * (30f / (float)i);
			float projOldY = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 107, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLightEmittence = true;
			dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 107, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
			Main.dust[dust].noLightEmittence = true;
		}
	}
}
