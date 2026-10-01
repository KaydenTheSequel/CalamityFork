using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class NidhoggRailgunBigShot : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 900;
		base.Projectile.extraUpdates = 80;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (Vector2.Distance(Owner.Center, base.Projectile.Center) < 1400f && time > 7)
		{
			if (time % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 48, 0.375f, ArsenalEffects.ArsenalGaussColor, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.05f, 0.7f));
			}
			if (time % 3 == 0)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), ModContent.DustType<SquashDust>());
				dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(10f, 30f);
				dust.scale = Main.rand.NextFloat(1.3f, 1.7f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalGaussColor;
				dust.noLightEmittence = time % 6 != 0;
				dust.fadeIn = -0.6f;
			}
			if (time % 12 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center, -base.Projectile.velocity * 0.1f, -base.Projectile.velocity * 0.1f, "CalamityMod/Particles/GlowSquareFading", 35, 0.33f, ArsenalEffects.ArsenalGaussColor, new Vector2(1f, 0.8f)));
			}
		}
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (Vector2.Distance(base.Projectile.Center, projectile.Center) <= 20f && projectile.active && projectile.type == ModContent.ProjectileType<RicoshotCoin>())
			{
				projectile.Kill();
				base.Projectile.velocity = base.Projectile.Center.DirectionTo(Owner.Center) * 6f;
				base.Projectile.hostile = true;
				base.Projectile.friendly = false;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		impactDust();
		if (target.life <= 0 && target.realLife == -1 && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		modifiers.SetCrit();
		float critDamage = Math.Min(Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		float minMult = 0.25f;
		int hitsToMinMult = 7;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult + critDamage;
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = 20f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override void OnKill(int timeLeft)
	{
		impactDust();
	}

	public void impactDust()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < MathHelper.Clamp(12 - base.Projectile.numHits * 2, 1, 12); i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalGaussDust);
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(14f, 37f);
			dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
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
