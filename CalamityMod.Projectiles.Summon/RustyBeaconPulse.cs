using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class RustyBeaconPulse : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public float LifetimeCompletion => 1f - (float)base.Projectile.timeLeft / 95f;

	public override void SetDefaults()
	{
		base.Projectile.width = 96;
		base.Projectile.height = 96;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 30;
		base.Projectile.timeLeft = 95;
		base.Projectile.scale = 0.001f;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			base.Projectile.localAI[0] = Main.rand.NextBool().ToDirectionInt();
			base.Projectile.netUpdate = true;
		}
		base.Projectile.Opacity = 1f - (float)Math.Pow(LifetimeCompletion, 1.56);
		base.Projectile.scale = MathHelper.Lerp(0.5f, 7f, LifetimeCompletion);
		base.Projectile.rotation += base.Projectile.localAI[0] * 0.012f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Color val = new Color(153, 226, 104, 0);
		Color c2 = default(Color);
		((Color)(ref c2))._002Ector(158, 128, 175, 92);
		return Color.Lerp(val, c2, 1f - base.Projectile.Opacity) * base.Projectile.Opacity * 0.67f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Color drawColor = base.Projectile.GetAlpha(lightColor) * 0.33f;
		for (int i = 0; i < 8; i++)
		{
			float rotation = base.Projectile.rotation;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2() * base.Projectile.scale;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + drawOffset;
			if (i % 2 == 1)
			{
				rotation *= -1f;
			}
			Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return !target.CountsAsACritter && !target.friendly && target.chaseable;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.scale * 48f, targetHitbox);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
