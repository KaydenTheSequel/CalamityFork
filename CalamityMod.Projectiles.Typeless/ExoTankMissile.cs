using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ExoTankMissile : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> Glow;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void Load()
	{
		Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 90 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 1200f, 15f, 20f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		Color boomColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.4f) * 0.6f;
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, boomColor, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.08f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 5; i++)
		{
			Vector2 velocity = Vector2.UnitX.RotatedBy((float)Math.PI * 2f / 5f * ((float)i + Main.rand.NextFloat(-0.5f, 0.5f))) * Main.rand.NextFloat(3f, 10f);
			Color energyColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.4f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, velocity, Main.rand.NextFloat(0.1f, 0.5f), energyColor, 15, 1f, 2.5f));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Glow != null)
		{
			Rectangle frame = Glow.Value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
			Main.EntitySpriteDraw(Glow.Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
	}
}
