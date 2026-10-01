using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AugerPull : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float sine;

	public float shrink = 1f;

	public int strongTimer;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool strong => strongTimer < 10;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 250);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 90;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		if (strongTimer == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalGaussColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, 0f, 1.1f, 0.7f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 20; i++)
			{
				Vector2 dustVel2 = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(10f, 15.5f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel2.SafeNormalize(Vector2.UnitX) * 360f, ModContent.DustType<SquashDust>(), -dustVel2 * 1.5f, 0, default(Color), Main.rand.NextFloat(1.2f, 1.8f));
				dust.noGravity = true;
				dust.fadeIn = 0.3f;
				dust.color = ArsenalEffects.ArsenalGaussColor;
			}
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				NPC target = Main.npc[j];
				if (target != null && target.CanBeMoved(ignoreKBImmune: true) && Collision.CanHit(base.Projectile.Center, 1, 1, target.Center, 1, 1) && Vector2.Distance(target.Center, base.Projectile.Center) < 650f)
				{
					Vector2 moveDir = (target.velocity = target.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX));
					target.Center += moveDir * 3f;
				}
			}
		}
		Vector2 dustVel3 = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(3f, 8.5f);
		Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + dustVel3.SafeNormalize(Vector2.UnitX) * 120f, ArsenalEffects.ArsenalGaussDust, -dustVel3, 0, default(Color), Main.rand.NextFloat(0.5f, 1f));
		dust2.noGravity = true;
		dust2.fadeIn = 0.05f;
		dust2.color = ArsenalEffects.ArsenalGaussColor;
		if (base.Projectile.timeLeft > 2)
		{
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target2 = Main.npc[k];
				if (target2 != null && target2.CanBeMoved(ignoreKBImmune: true) && Collision.CanHit(base.Projectile.Center, 1, 1, target2.Center, 1, 1) && Vector2.Distance(target2.Center, base.Projectile.Center) > 15f && Vector2.Distance(target2.Center, base.Projectile.Center) < 300f)
				{
					Vector2 moveDir2 = target2.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					target2.velocity = Vector2.Lerp(target2.velocity, moveDir2 * 5f, 0.12f);
					target2.Center += moveDir2 * 2f;
				}
			}
		}
		base.Projectile.rotation += 0.05f;
		if (shrink > 0f)
		{
			shrink -= 0.05f;
		}
		else
		{
			shrink = 1f;
		}
		if (!strong)
		{
			time++;
		}
		strongTimer++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		float shrinkFade = Math.Min(Utils.GetLerpValue(1f, 0.7f, shrink, clamped: true), Utils.GetLerpValue(0f, 0.3f, shrink, clamped: true));
		float sine = Math.Abs(MathHelper.Lerp((float)Math.Sin((float)time * 0.1f / (float)Math.PI), 0.4f, 0.8f));
		float sine2 = Math.Abs(MathHelper.Lerp((float)Math.Sin((float)time * 0.3f / (float)Math.PI), 0.4f, 0.8f));
		float fade = (float)Math.Pow(Utils.GetLerpValue(0f, 10f, base.Projectile.timeLeft, clamped: true), 3.0);
		float areaScale = (strong ? 1f : 1f) * fade;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSquareParticleBig", (AssetRequestMode)2).Value;
		Color arsenalGaussColor = ArsenalEffects.ArsenalGaussColor;
		((Color)(ref arsenalGaussColor)).A = 0;
		Color drawColor = arsenalGaussColor * (strong ? 0.5f : 0.3f) * shrinkFade;
		Main.EntitySpriteDraw(tex2, base.Projectile.Center - Main.screenPosition, null, drawColor, (float)Math.PI / 4f - (float)Math.PI / 2f * sine * 3f, tex2.Size() / 2f, (areaScale + sine * 0.5f) * shrink, (SpriteEffects)0);
		Main.EntitySpriteDraw(tex2, base.Projectile.Center - Main.screenPosition, null, drawColor, (float)Math.PI / 2f * sine2 * 3f, tex2.Size() / 2f, (areaScale - sine2 * 0.5f) * (float)Math.Pow(shrink, 2.0), (SpriteEffects)0);
		return false;
	}
}
