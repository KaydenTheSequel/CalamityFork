using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PhantomicDagger : ModProjectile, ILocalizedModType, IModType
{
	private bool homing;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 38;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 200;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (homing)
		{
			return null;
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(homing);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		homing = reader.ReadBoolean();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (target.defense <= 999 && !(target.Calamity().DR >= 0.95f) && !target.Calamity().unbreakableDR)
		{
			float maxDRPenetration = 1.05f;
			modifiers.FinalDamage *= MathHelper.Clamp(1f / (1f - target.Calamity().DR), 1f, maxDRPenetration);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 4; d++)
		{
			int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
			Dust obj = Main.dust[shadow];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[shadow].scale = 0.5f;
				Main.dust[shadow].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 12; i++)
		{
			int shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 3f);
			Main.dust[shadow2].noGravity = true;
			Dust obj2 = Main.dust[shadow2];
			obj2.velocity *= 5f;
			shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
			Dust obj3 = Main.dust[shadow2];
			obj3.velocity *= 2f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityClientConfig.Instance.Afterimages)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		return true;
	}

	public override void AI()
	{
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dust.Length < 5997)
		{
			for (int i = 0; i < 3; i++)
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 3f);
				Main.dust[dust].noGravity = true;
			}
		}
		if (base.Projectile.alpha != 0)
		{
			base.Projectile.rotation -= 6f * MathF.Pow(base.Projectile.Opacity, 2f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= MathHelper.Lerp(1f, 0.9f, base.Projectile.Opacity);
			if (base.Projectile.alpha < 3)
			{
				base.Projectile.alpha = 0;
				homing = true;
			}
			else
			{
				base.Projectile.alpha -= 2;
			}
			return;
		}
		NPC target = base.Projectile.Center.MinionHoming(1500f, Main.player[base.Projectile.owner]);
		if (target != null)
		{
			float projVel = 40f;
			Vector2 projDirection = base.Projectile.Center;
			float targetXDist = target.Center.X - projDirection.X;
			float targetYDist = target.Center.Y - projDirection.Y;
			float targetDist = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
			if (targetDist < 100f)
			{
				projVel = 28f;
			}
			targetDist = projVel / targetDist;
			targetXDist *= targetDist;
			targetYDist *= targetDist;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 2f + targetXDist) / 3f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 2f + targetYDist) / 3f;
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.9f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.Atan(90.0);
	}
}
