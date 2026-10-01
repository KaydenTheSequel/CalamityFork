using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class FuckYou : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 600;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0.8f, 0.6f);
		base.Projectile.ai[1] += 0.01f;
		base.Projectile.scale = base.Projectile.ai[1] * 0.5f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= (float)(3 * Main.projFrames[base.Type]))
		{
			base.Projectile.Kill();
			return;
		}
		int incrementer = base.Projectile.frameCounter + 1;
		base.Projectile.frameCounter = incrementer;
		if (incrementer >= 3)
		{
			base.Projectile.frameCounter = 0;
			incrementer = base.Projectile.frame + 1;
			base.Projectile.frame = incrementer;
			if (incrementer >= Main.projFrames[base.Type])
			{
				base.Projectile.hide = true;
			}
		}
		base.Projectile.alpha -= 63;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = (int)(52f * base.Projectile.scale));
			base.Projectile.Center = base.Projectile.position;
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
			for (int dustIndexA = 0; dustIndexA < 4; dustIndexA++)
			{
				int smoky = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[smoky].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			}
			for (int dustIndexB = 0; dustIndexB < 10; dustIndexB++)
			{
				int fireDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 200, default(Color), 2.7f);
				Dust obj = Main.dust[fireDust];
				obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				obj.noGravity = true;
				obj.velocity *= 3f;
				fireDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(Math.PI) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				obj.velocity *= 2f;
				obj.noGravity = true;
				obj.fadeIn = 2.5f;
			}
			for (int dustIndexC = 0; dustIndexC < 5; dustIndexC++)
			{
				int fireDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 0, default(Color), 2.7f);
				Dust obj2 = Main.dust[fireDust2];
				obj2.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(Math.PI).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
				obj2.noGravity = true;
				obj2.velocity *= 3f;
			}
			for (int dustIndexD = 0; dustIndexD < 10; dustIndexD++)
			{
				int smokier = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
				Dust obj3 = Main.dust[smokier];
				obj3.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(Math.PI).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
				obj3.noGravity = true;
				obj3.velocity *= 3f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return base.Projectile.ai[0] > 1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 127);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.direction = Main.player[base.Projectile.owner].direction;
	}
}
