using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SpatialSpear3 : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 75;

	private const int TimeToFall = 37;

	private const int TotalSplits = 2;

	private const int SplitTime = 37;

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
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 75;
		base.Projectile.tileCollide = !Main.zenithWorld;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0.05f, 0.05f);
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 4f;
		if (base.Projectile.timeLeft < 37)
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
		int dust = Dust.NewDust(base.Projectile.oldPosition + base.Projectile.oldVelocity, base.Projectile.width, base.Projectile.height, 73, 0f, 0f, 100, default(Color), 1.25f);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
		Main.dust[dust].noLightEmittence = true;
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] >= 37f))
		{
			return;
		}
		base.Projectile.localAI[0] = 0f;
		int numProj = 2;
		float rotation = MathHelper.ToRadians(20f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<SpatialSpear4>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 128, 128);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 70)
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 4; i < 31; i++)
		{
			float projOldX = base.Projectile.oldVelocity.X * (30f / (float)i);
			float projOldY = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 73, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLightEmittence = true;
			dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 73, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
			Main.dust[dust].noLightEmittence = true;
		}
	}
}
