using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class NidhoggRailgunBlast : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 80;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center) < 1400f && time > 7)
		{
			if (time % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 8, 0.15f, ArsenalEffects.ArsenalGaussColor, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.4f, 0.7f));
			}
			if (time % 3 == 0)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), ArsenalEffects.ArsenalGaussDust);
				dust.velocity = Vector2.One.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(0.1f, 0.2f);
				dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalGaussColor;
				dust.noLightEmittence = time % 9 != 0;
				dust.fadeIn = 0.3f;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 8, MathHelper.Clamp(0.06f - (float)base.Projectile.numHits * 0.01f, 0f, 0.06f), ArsenalEffects.ArsenalGaussColor * 0.9f, new Vector2(1f, 0.5f), quickShrink: true, glow: true, 0.9f));
		if (base.Projectile.numHits == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/GlowSquareParticleThick", affectedByGravity: false, 8, 1f, ArsenalEffects.ArsenalGaussColor * 0.6f, Vector2.One, useAddativeBlend: true, glowCenter: true, (float)Math.PI / 4f, fadeIn: false, affectedByLight: false, 0f, 1.15f, 0.5f));
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<NidhoggExplosion>() && p.owner == base.Projectile.owner && p.timeLeft > 10)
				{
					p.timeLeft = 10;
				}
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<NidhoggExplosion>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
		}
		impactDust();
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = 6f;
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
		for (int i = 0; i < MathHelper.Clamp(5 - base.Projectile.numHits, 1, 5); i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(8f, 13f);
			dust.scale = Main.rand.NextFloat(0.8f, 1.35f);
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalGaussColor;
			dust.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
