using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class LuxorsGiftMagic : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public float velLerp => Utils.GetLerpValue(1f, 8f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 36;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 360;
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
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.35f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft == 360)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.1f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.01f;
		}
		if (base.Projectile.timeLeft > 30)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 offset = ((float)Math.PI * 2f * (float)i / 2f).ToRotationVector2().RotatedBy((float)base.Projectile.timeLeft * 0.1f) * 35f * velLerp;
				Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(5.5f, 6.5f);
				Vector2 position = base.Projectile.Center + offset;
				Vector2? velocity = dustVel;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 278, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.4f, 0.5f);
				dust.color = Color.Gold;
				dust.noLightEmittence = true;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		float minMult = 0.5f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		for (int g = 0; g < 4; g++)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 offset = ((float)Math.PI * 2f * (float)i / 2f).ToRotationVector2().RotatedBy((float)base.Projectile.timeLeft * 0.1f) * 35f * velLerp;
				Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<LightDust>(), dustVel.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(5.5f, 9.5f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.5f);
				dust.color = Color.Gold;
				dust.noLightEmittence = true;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 10f + 65f * velLerp, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Color drawColor = Color.Gold;
		float fade = Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
		for (int i = 0; i < 2; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 2f).ToRotationVector2().RotatedBy((float)base.Projectile.timeLeft * 0.1f) * 45f * velLerp;
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + drawOffset;
			Color val = drawColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * 0.6f * (float)Math.Pow(fade, 3.0), base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, Color.White * fade, base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(1f - 0.3f * velLerp, 1f + 0.8f * velLerp) * base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
