using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class MagnusBoom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 20;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Magenta, Color.LightPink, Utils.GetLerpValue(0f, 5f, i, clamped: true)), "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.09f + 0.005f * (float)i, (int)(20f - (float)i * 1.5f), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Magenta, Color.LightPink, Utils.GetLerpValue(0f, 3f, j, clamped: true)), "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.09f + 0.01f * (float)j, (int)(20f - (float)j * 2f), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.PowderBlue, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.1f, 2f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.05f, 1f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			float offset = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int k = 0; k < 4; k++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)k / 4f + offset).ToRotationVector2();
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, velocity, affectedByGravity: false, 12, 0.4f, Color.Magenta, new Vector2(1f, 0.1f), quickShrink: true));
			}
		}
		base.Projectile.scale = MathHelper.Lerp(0f, 1f, CalamityUtils.PiecewiseAnimation(Time / 20f, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4)));
		if (Time < 10f)
		{
			for (int l = 0; l < 3; l++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.Next(71, 74), Main.rand.NextVector2CircularEdge(6f, 6f) * (Main.rand.NextFloat(1f, 1.2f) + base.Projectile.scale));
				dust.noGravity = true;
				dust.noLight = true;
				dust.scale = Main.rand.NextFloat(0.8f, 1.2f) + base.Projectile.scale;
				dust.alpha = Main.rand.Next(120, 181);
			}
		}
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 480);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (Owner.Center.X < target.Center.X).ToDirectionInt();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * base.Projectile.scale, targetHitbox);
	}
}
