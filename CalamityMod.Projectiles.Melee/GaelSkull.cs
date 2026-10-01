using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GaelSkull : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.light = 1f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = GaelsGreatsword.ImmunityFrames;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.scale <= 1f)
		{
			NPC target = base.Projectile.Center.ClosestNPCAt(GaelsGreatsword.SearchDistance);
			base.Projectile.tileCollide = target != null;
			if (target != null)
			{
				float distanceFromTarget = base.Projectile.Distance(target.Center);
				float homingSpeed = ((Vector2)(ref base.Projectile.velocity)).Length() * ((distanceFromTarget < 220f) ? 1.25f : 1f);
				if (homingSpeed > 22f)
				{
					homingSpeed = 22f;
				}
				Vector2 idealVelocity = base.Projectile.SafeDirectionTo(target.Center) * homingSpeed;
				float inertia = ((base.Projectile.Distance(target.Center) < 240f) ? 4f : 13f);
				base.Projectile.velocity = (base.Projectile.velocity * inertia + idealVelocity) / (inertia + 1f);
				if (distanceFromTarget < 300f)
				{
					base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 2f);
				}
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= homingSpeed;
			}
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (-base.Projectile.velocity).ToRotation();
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % 20f == 0f)
		{
			for (int l = 0; l < 14; l++)
			{
				Vector2 spawnPositionAdditive = Vector2.UnitX * (0f - (float)base.Projectile.width) / 2f;
				spawnPositionAdditive += -Vector2.UnitY.RotatedBy((float)l * ((float)Math.PI * 2f) / 14f) * new Vector2(8f, 16f) * base.Projectile.scale;
				spawnPositionAdditive = spawnPositionAdditive.RotatedBy(base.Projectile.rotation);
				int dustIndex = Dust.NewDust(base.Projectile.Center, 0, 0, 218, 0f, 0f, 0, new Color(188, 126, 154), 1.5f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].position = base.Projectile.Center + spawnPositionAdditive;
				Main.dust[dustIndex].velocity = base.Projectile.velocity * 0.1f;
				Main.dust[dustIndex].velocity = Vector2.Normalize(base.Projectile.Center - base.Projectile.velocity * 3f - Main.dust[dustIndex].position) * 1.25f;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 5 == 0)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.penetrate = -1;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.05f;
			base.Projectile.alpha++;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		else if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 15;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 240);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		if (base.Projectile.scale == 1.75f)
		{
			base.Projectile.damage /= 2;
		}
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.NPCDeath52, base.Projectile.Center);
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 218, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int j = 0; j < 30; j++)
		{
			float angle = (float)Math.PI * 2f * (float)j / 30f;
			int dustIndex = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 218, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[dustIndex].noGravity = true;
			Dust obj = Main.dust[dustIndex];
			obj.velocity *= 3f;
			dustIndex = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 218, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[dustIndex];
			obj2.velocity *= 2f;
			Main.dust[dustIndex].noGravity = true;
			Dust.NewDust(base.Projectile.Center + angle.ToRotationVector2() * 160f, 0, 0, 218, 0f, 0f, 100, default(Color), 1.5f);
		}
	}
}
