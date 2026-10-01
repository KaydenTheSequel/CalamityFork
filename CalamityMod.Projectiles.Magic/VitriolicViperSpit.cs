using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VitriolicViperSpit : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int direction;

	public Vector2 lastPos;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 13;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.timeLeft = 300 * base.Projectile.MaxUpdates;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
		Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 4.5f * MathHelper.Clamp(base.Projectile.ai[2], 0.25f, 1f);
		if (time == 0)
		{
			direction = (Main.rand.NextBool() ? 1 : (-1));
			lastPos = base.Projectile.Center;
		}
		if (time > 3)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += offset * (float)direction * Utils.GetLerpValue(3f, 10f, time, clamped: true);
		}
		if (time > 3 && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, base.Projectile.Center.DirectionTo(lastPos), affectedByGravity: false, (int)(15f * MathHelper.Clamp(base.Projectile.ai[2], 0.5f, 1f)), 0.07f * MathHelper.Clamp(base.Projectile.ai[2], 0.25f, 1f), Color.Chartreuse, new Vector2(1f, 1.4f)));
			if (Main.rand.NextBool((int)MathHelper.Clamp((1f - base.Projectile.ai[2]) * 5f, 1f, 15f)))
			{
				int area = (int)(25f * MathHelper.Clamp(base.Projectile.ai[2], 0.5f, 1f));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(area, area), 75);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.9f, 1.3f);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 0.7f);
			}
		}
		time++;
		lastPos = base.Projectile.Center;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Irradiated>(), 230);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/NuclearTerrorHit");
		style.Volume = 0.5f;
		style.Pitch = 0.7f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < MathHelper.Clamp(6 - base.Projectile.numHits * 2, 1, 10); i++)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(target.Center, base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f), Main.rand.NextBool() ? Color.Chartreuse : Color.GreenYellow, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.28f, 0.44f) * MathHelper.Clamp(base.Projectile.ai[2], 0.5f, 1f), 0f, 50));
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 60f * MathHelper.Clamp(base.Projectile.ai[2], 0.25f, 1f), targetHitbox);
	}
}
