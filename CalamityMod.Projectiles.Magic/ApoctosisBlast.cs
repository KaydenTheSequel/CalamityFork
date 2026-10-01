using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ApoctosisBlast : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	public float sine;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public bool chargeShot => base.Projectile.ai[2] == 5f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.ignoreWater = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 40f / (float)Math.PI);
		if (onSpawn)
		{
			base.Projectile.penetrate = -1;
			base.Projectile.extraUpdates = 20;
			base.Projectile.tileCollide = false;
			onSpawn = false;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f && time > 3f)
		{
			Vector2 placement = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 60f;
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(placement, -base.Projectile.velocity * 0.1f, affectedByGravity: false, 35, 0.08f, Color.Crimson, new Vector2(0.4f, 1.3f), quickShrink: true, glow: false, 0f));
			Vector2 vel = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(3.5f, 14f);
			if (time % 2f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(placement + Main.rand.NextVector2Circular(9f, 9f), vel, "CalamityMod/Particles/GlowSquareParticle", affectedByGravity: false, Main.rand.Next(17, 42), Main.rand.NextFloat(1.4f, 3.5f), Main.rand.NextBool() ? Color.Lerp(Color.Crimson, Color.White, 0.5f) : Color.Crimson, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, (float)Math.PI / 4f));
			}
		}
		Vector2 center = base.Projectile.Center;
		Color crimson = Color.Crimson;
		Lighting.AddLight(center, ((Color)(ref crimson)).ToVector3() * 0.8f);
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		float minMult = 0.35f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		for (int i = 0; i < 15; i++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(7f, 25f), affectedByGravity: false, 35, Main.rand.NextFloat(0.5f, 0.9f), Main.rand.NextBool() ? Color.Lerp(Color.Crimson, Color.White, 0.5f) : Color.Crimson));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (time <= 1f)
		{
			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, Owner.Center, base.Projectile.width, ref _);
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			return false;
		}
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation - (float)Math.PI / 2f;
		float timeleftFade = Utils.GetLerpValue(0f, 190f, base.Projectile.timeLeft, clamped: true);
		for (int i = 0; i < 10; i++)
		{
			float iMult = 1f - 0.06f * (float)i;
			Vector2 scale = new Vector2((1.2f - 0.8f * iMult) * timeleftFade, 0.2f + 3f * iMult) * iMult * 0.04f;
			Vector2 bodyScale = new Vector2((1.2f - 0.8f * iMult) * timeleftFade, 0.2f + 5f * iMult) * iMult * 0.04f;
			Color val;
			for (int b = -1; b <= 1; b += 2)
			{
				Vector2 position = drawPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(0.35f * (float)b * (float)(15 - i) * 0.06f * sine) * 65f;
				val = Color.Lerp(Color.Crimson, Color.White, (float)i * 0.05f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(tex2, position, null, val * iMult * timeleftFade, drawRotation + 0.2f * (float)b * sine, tex2.Size() * 0.5f, scale, (SpriteEffects)0);
			}
			Vector2 position2 = drawPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 125f;
			val = Color.Lerp(Color.Crimson, Color.White, (float)i * 0.05f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(tex2, position2, null, val * iMult * timeleftFade, drawRotation, tex2.Size() * 0.5f, bodyScale, (SpriteEffects)0);
		}
		return false;
	}
}
