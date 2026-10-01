using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LuxorsGiftMelee : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Red;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.35f);
		base.Projectile.scale = ((base.Projectile.ai[0] == 5f) ? 0.5f : 0.75f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.973f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.ai[0] == 5f)
		{
			if (base.Projectile.timeLeft % 9 == 0 && base.Projectile.timeLeft > 30)
			{
				Vector2 dustVel = -(base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.5) * Main.rand.NextFloat(0.8f, 1.3f);
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f);
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity = dustVel * Main.rand.NextFloat(0.5f, 1.2f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.45f, 0.6f);
				dust.color = Color.Red;
				dust.alpha = 120;
				dust.noLightEmittence = true;
			}
		}
		else if (base.Projectile.timeLeft % 4 == 0 && base.Projectile.timeLeft > 30)
		{
			Vector2 dustVel2 = -(base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.5) * Main.rand.NextFloat(0.8f, 1.3f);
			Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
			int type2 = ModContent.DustType<LightDust>();
			Vector2? velocity2 = dustVel2 * Main.rand.NextFloat(0.5f, 1.2f);
			newColor = default(Color);
			Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity2, 0, newColor);
			dust2.noGravity = !Main.rand.NextBool(3);
			dust2.scale = Main.rand.NextFloat(0.45f, 0.6f);
			dust2.color = Color.Red;
			dust2.noLightEmittence = true;
		}
		if (base.Projectile.timeLeft > 90 && base.Projectile.timeLeft < 177 && base.Projectile.timeLeft % 2 == 0 && base.Projectile.ai[0] == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 2f, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 2f, affectedByGravity: false, 14, 0.02f, Color.Red * 0.45f, new Vector2(1.4f, 1f), quickShrink: true, glow: false, 0.6f));
		}
		if (Collision.SolidCollision(base.Projectile.Center, 9, 9) && base.Projectile.timeLeft <= 150)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.91f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.5f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		if (base.Projectile.numHits >= 1 && base.Projectile.timeLeft > 70)
		{
			base.Projectile.timeLeft = 70;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.ai[0] != 5f && base.Projectile.numHits < 2 && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Color drawColor = Color.Red;
		float fade = Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
		for (int i = 0; i < 8; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2() * 2f;
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + drawOffset;
			Color val = drawColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * 0.4f * (float)Math.Pow(fade, 3.0), base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, Color.White * fade, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
