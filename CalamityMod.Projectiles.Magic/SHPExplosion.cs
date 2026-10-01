using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SHPExplosion : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 20;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 500;
		base.Projectile.height = 500;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 20;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		float lights = Main.rand.NextFloat(0.9f, 1.1f);
		lights *= Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 5f * lights, 1f * lights, 4f * lights);
		bool lightSoul = SHPB.GetSoulEffects((int)base.Projectile.ai[0]) == SHPB.SoulType.Light;
		bool nightSoul = SHPB.GetSoulEffects((int)base.Projectile.ai[0]) == SHPB.SoulType.Night;
		int explosionSize = 200 + (int)MathHelper.Clamp(Timer, 0f, 20f) * (int)Math.Ceiling(15f * (lightSoul ? 1.5f : 1f));
		base.Projectile.ExpandHitboxBy(explosionSize);
		if (base.Projectile.ai[1] == 0f)
		{
			Color particleColor = SHPB.FindColorForSoul((int)base.Projectile.ai[0]);
			float sizeScalar = (lightSoul ? 1.5f : 1f);
			CustomPulse inner = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.4f, 1.5f * sizeScalar, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			CustomPulse outer = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/BloomRing", Vector2.One, 0f, 0.4f, 2.5f * sizeScalar, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			CustomPulse particle = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/PlasmaExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.026f, 0.26f * sizeScalar, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			GeneralParticleHandler.SpawnParticle(inner);
			GeneralParticleHandler.SpawnParticle(outer);
			GeneralParticleHandler.SpawnParticle(particle);
			if (nightSoul)
			{
				base.Projectile.timeLeft = 50;
				CustomPulse nightInner = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.4f, 1.5f, 50, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
				CustomPulse nightOuter = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/BloomRing", Vector2.One, 0f, 0.4f, 2.5f, 50, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
				CustomPulse particle2 = new CustomPulse(base.Projectile.Center, Vector2.Zero, particleColor, "CalamityMod/Particles/PlasmaExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.026f, 0.26f, 50, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
				GeneralParticleHandler.SpawnParticle(nightInner);
				GeneralParticleHandler.SpawnParticle(nightOuter);
				GeneralParticleHandler.SpawnParticle(particle2);
			}
			base.Projectile.ai[1] = 1f;
		}
		Timer++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (SHPB.GetSoulEffects((int)base.Projectile.ai[0]) == SHPB.SoulType.Might && target.CanBeMoved())
		{
			Player owner = Main.player[base.Projectile.owner];
			Vector2 launchVel = owner.Center.DirectionTo(owner.Calamity().mouseWorld) - Vector2.UnitY * 5f;
			target.MoveNPC(launchVel, 20f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (SHPB.GetSoulEffects((int)base.Projectile.ai[0]) == SHPB.SoulType.Fright)
		{
			modifiers.SourceDamage.Flat += 20f;
		}
	}
}
