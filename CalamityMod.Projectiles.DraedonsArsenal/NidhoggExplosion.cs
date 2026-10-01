using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class NidhoggExplosion : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 420;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dustVel = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(3f, 8.5f);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel.SafeNormalize(Vector2.UnitX) * 120f, ModContent.DustType<SquashDust>(), -dustVel, 0, default(Color), Main.rand.NextFloat(0.5f, 1f));
		dust.noGravity = true;
		dust.fadeIn = 0.05f;
		dust.color = ArsenalEffects.ArsenalGaussColor;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			NPC target = Main.npc[i];
			if (target != null && target.CanBeMoved(ignoreKBImmune: true) && Vector2.Distance(target.Center, base.Projectile.Center) > 15f && Vector2.Distance(target.Center, base.Projectile.Center) < 500f)
			{
				Vector2 moveDir = target.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX);
				target.velocity = Vector2.Lerp(target.velocity, moveDir * 8f, 0.12f);
				target.Center += moveDir * 1.5f;
			}
		}
		base.Projectile.rotation += 0.07f;
		if (shrink > 0f)
		{
			shrink -= (strong ? 0.03f : 0.03f);
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

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (!strong)
		{
			modifiers.SourceDamage *= 0.1f;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f * base.Projectile.scale, targetHitbox);
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
		float sine = Math.Abs(MathHelper.Lerp((float)Math.Sin((float)time * 0.5f / (float)Math.PI), 0.4f, 0.8f));
		float sine2 = Math.Abs(MathHelper.Lerp((float)Math.Sin((float)time * 0.7f / (float)Math.PI), 0.4f, 0.8f));
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
