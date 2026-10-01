using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class LuxorsGiftRanged : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.35f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft == 300)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
		}
		if (base.Projectile.timeLeft > 30)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 val = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866) * (float)((i == 0) ? (-3) : 3);
				Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(2.5f, 8.5f);
				Vector2 position = val + Main.rand.NextVector2Circular(2f, 2f);
				int type = (Main.rand.NextBool() ? 278 : ModContent.DustType<LightDust>());
				Vector2? velocity = dustVel * Main.rand.NextFloat(0.5f, 1.2f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.3f, 0.4f);
				dust.color = Color.Cyan;
				dust.noLightEmittence = true;
			}
		}
		if (!Collision.SolidCollision(base.Projectile.Center, 35, 35) || base.Projectile.timeLeft < 200)
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.5f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 12; i++)
		{
			Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(2.5f, 8.5f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(11f, 11f), Main.rand.NextBool() ? 278 : ModContent.DustType<LightDust>(), dustVel * Main.rand.NextFloat(0.3f, 1.7f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.1f);
			dust.color = Color.Cyan;
			dust.noLightEmittence = true;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f, targetHitbox);
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
		Color drawColor = Color.Cyan;
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
