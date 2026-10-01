using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class IcicleArrowProj : ModProjectile, ILocalizedModType, IModType
{
	public bool falling;

	public Vector2 startVelocity;

	public bool setFallingStats;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/IcicleArrow";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 6;
		base.Projectile.timeLeft = 1000;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.localAI[0] == 0f)
		{
			startVelocity = base.Projectile.velocity;
		}
		base.Projectile.localAI[0]++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (!falling)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 2f, Main.rand.NextBool(3) ? 135 : 279, -base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.05f, 0.4f) - new Vector2(0f, 1f));
			dust.scale = Main.rand.NextFloat(0.35f, 0.6f);
			dust.noGravity = false;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.99f;
			base.Projectile.alpha++;
			if (base.Projectile.localAI[0] == 200f)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
					dust2.scale = Main.rand.NextFloat(1.2f, 2.2f);
					dust2.noGravity = true;
				}
				base.Projectile.alpha = 255;
				base.Projectile.velocity = new Vector2(0f, 0.3f);
				falling = true;
			}
			return;
		}
		if (!setFallingStats)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.penetrate = 2;
			base.Projectile.numHits = 1;
			base.Projectile.ExpandHitboxBy(50);
			setFallingStats = true;
		}
		if (base.Projectile.Calamity().conditionalHomingRange > 0f)
		{
			base.Projectile.Calamity().conditionalHomingRange = 0f;
		}
		if (targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity * 0.1f, affectedByGravity: false, 2, 1.2f, Color.SkyBlue));
			if (base.Projectile.localAI[0] % 3f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, base.Projectile.velocity * 0.01f, affectedByGravity: false, 4, 1.1f, Color.SkyBlue));
			}
		}
		if (base.Projectile.localAI[0] < 320f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.033f;
		}
		else
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(324, 180);
		if (base.Projectile.Calamity().conditionalHomingRange > 0f)
		{
			base.Projectile.Calamity().conditionalHomingRange = 0f;
		}
		if (!falling)
		{
			base.Projectile.localAI[0] = 100f;
			SoundStyle style = SoundID.Item50 with
			{
				Volume = 0.35f,
				Pitch = -0.2f,
				PitchVariance = 0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.velocity = Utils.RotatedByRandom(new Vector2(0f, -6.5f), 0.25) * Main.rand.NextFloat(0.75f, 1.1f);
			for (int k = 0; k < 2; k++)
			{
				Vector2 velocity = startVelocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.3f, 0.85f);
				GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(base.Projectile.Center + velocity * 3f, velocity * 0.5f, affectedByGravity: false, 8, 0.65f, Color.SkyBlue));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 1 && falling)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item27 with
		{
			Volume = 0.3f,
			Pitch = 0.8f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int k = 0; k < 12; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
			dust.scale = Main.rand.NextFloat(0.65f, 0.85f);
			dust.noGravity = false;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (!falling)
		{
			base.Projectile.localAI[0] = 100f;
			SoundStyle style = SoundID.Item50 with
			{
				Volume = 0.35f,
				Pitch = -0.2f,
				PitchVariance = 0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int k = 0; k < 7; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 135 : 279, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
				dust.scale = Main.rand.NextFloat(0.75f, 0.95f);
				dust.noGravity = false;
			}
			base.Projectile.velocity = Utils.RotatedByRandom(new Vector2(0f, -6.5f), 0.25) * Main.rand.NextFloat(0.75f, 1.1f);
			base.Projectile.tileCollide = false;
			return false;
		}
		return true;
	}

	public override bool? CanDamage()
	{
		if (falling || base.Projectile.numHits < 1)
		{
			return null;
		}
		return false;
	}
}
