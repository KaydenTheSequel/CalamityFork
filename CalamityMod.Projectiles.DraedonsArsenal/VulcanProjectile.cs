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

public class VulcanProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 720;
		base.Projectile.extraUpdates = 8;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (time > 120f && base.Projectile.penetrate > 1)
		{
			base.Projectile.penetrate = 1;
		}
		if (time > 60f && base.Projectile.extraUpdates > 2 && time % 4f == 0f)
		{
			base.Projectile.extraUpdates--;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		if (num < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 6f, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 6, 0.15f, ArsenalEffects.ArsenalGaussColor * 0.9f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.9f, 0.6f, 0.8f));
			if (time % 17f == 0f)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), ArsenalEffects.ArsenalGaussDust);
				dust.velocity = Vector2.One.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(0.1f, 0.2f);
				dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalGaussColor;
				dust.noLightEmittence = time % 9f != 0f;
				dust.fadeIn = 0.3f;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		impactDust();
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = 4f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override void OnKill(int timeLeft)
	{
		impactDust();
	}

	public void impactDust()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquareDust>());
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(9f, 19f);
			dust.scale = Main.rand.NextFloat(0.6f, 0.85f);
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalGaussColor;
			dust.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		Texture2D proj = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanProjectile", (AssetRequestMode)2).Value;
		proj = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2).Value;
		Texture2D square = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSquareParticleThick", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		base.Projectile.rotation.ToRotationVector2();
		Color val = ArsenalEffects.ArsenalGaussColor;
		((Color)(ref val)).A = 0;
		Color drawColor = val;
		float drawRotation = base.Projectile.velocity.ToRotation();
		Vector2 rotationPoint = proj.Size() * 0.5f;
		int rate = 50;
		float shrink = Utils.GetLerpValue(rate - 1, 0f, time % (float)rate);
		float fade = Math.Min((float)Math.Pow(Utils.GetLerpValue(0f, (float)rate * 0.8f, time % (float)rate, clamped: true), 3.0), (float)Math.Pow(Utils.GetLerpValue(rate - 1, (float)rate * 0.8f, time % 30f, clamped: true), 3.0));
		float speed = Utils.GetLerpValue(2f, 10f, base.Projectile.extraUpdates);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(1f + 2f * speed, 1f - 0.3f * speed);
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(square, drawPosition, null, drawColor * fade * (1f - speed), drawRotation + (float)Math.PI / 4f, square.Size() * 0.5f, base.Projectile.scale * (0.05f + 0.2f * shrink), (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(proj, drawPosition, null, drawColor, drawRotation, rotationPoint, squash * base.Projectile.scale * 0.1f, (SpriteEffects)0);
		Texture2D texture = proj;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture, drawPosition, null, val, drawRotation, rotationPoint, squash * base.Projectile.scale * 0.06f, (SpriteEffects)0);
		return false;
	}
}
