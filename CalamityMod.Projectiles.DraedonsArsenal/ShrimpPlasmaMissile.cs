using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class ShrimpPlasmaMissile : ModProjectile, ILocalizedModType, IModType
{
	public NPC closestNPC;

	public Vector2 offset;

	public bool hitTile;

	public float randomRate;

	public float randomSize;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.ArmorPenetration = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (time == 0f)
		{
			randomRate = Main.rand.NextFloat(1f, 3f);
			randomSize = Main.rand.NextFloat(1f, 5f);
		}
		float sine = (float)Math.Sin(time * 0.075f / (float)Math.PI * randomRate);
		if (time > 0f && base.Projectile.numHits == 0)
		{
			if (Owner.Center.Distance(base.Projectile.Center) < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * -0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 12, 0.18f, ArsenalEffects.ArsenalPlasmaColor, new Vector2(0.3f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.3f, 0.5f));
			}
			if (base.Projectile.ai[2] == 0f)
			{
				if (time > 10f)
				{
					if (closestNPC == null || closestNPC.life <= 0 || !closestNPC.CanBeChasedBy())
					{
						closestNPC = base.Projectile.Center.ClosestNPCAt(900f);
					}
					if (closestNPC != null)
					{
						base.Projectile.timeLeft++;
						float distMult = (float)Math.Pow(Utils.GetLerpValue(140f, 80f, base.Projectile.Center.Distance(closestNPC.Center), clamped: true), 3.0);
						CalamityUtils.HomeInOnSelectedNPC(base.Projectile, closestNPC, ignoreTiles: true, 0.2f + 0.3f * distMult, 8f, 0.99f - 0.1f * distMult, 0.95f, accelerate: true);
					}
				}
			}
			else
			{
				Vector2 goalPosition = Owner.Calamity().mouseWorld + offset;
				int fallTime = 300 - (int)base.Projectile.ai[2] * base.Projectile.extraUpdates;
				offset = Vector2.UnitX * sine * 15f * randomSize;
				if (time == (float)fallTime)
				{
					base.Projectile.timeLeft = 600;
					base.Projectile.extraUpdates = 6;
					base.Projectile.scale = 1.5f;
				}
				if (time > (float)fallTime)
				{
					Math.Pow(Utils.GetLerpValue(480f, 340f, time, clamped: true), 3.0);
					Vector2 moveTo = (goalPosition - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					if (base.Projectile.velocity.Y < 8f)
					{
						base.Projectile.velocity.Y += 0.1f;
					}
					base.Projectile.velocity.X += moveTo.X * 0.5f;
					base.Projectile.velocity.X *= 0.975f;
				}
				else
				{
					base.Projectile.timeLeft++;
				}
			}
			if (Main.rand.NextBool(5))
			{
				int dustStyle = ArsenalEffects.ArsenalPlasmaDust;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle);
				dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.3f);
				dust.noGravity = false;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
				dust.fadeIn = -0.2f;
			}
		}
		if (Collision.SolidCollision(base.Projectile.Center, 8, 8) && base.Projectile.numHits == 0 && base.Projectile.ai[2] > 0f && base.Projectile.Center.Y > Owner.Calamity().mouseWorld.Y && base.Projectile.scale > 1f)
		{
			hitTile = true;
			base.Projectile.numHits = 1;
			Explode();
		}
		time++;
	}

	public void Explode()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		float lifetimeMult = ((!hitTile) ? 1 : 5);
		if (hitTile)
		{
			base.Projectile.localNPCHitCooldown = 8;
		}
		base.Projectile.extraUpdates = 0;
		base.Projectile.timeLeft = (int)(12f * lifetimeMult);
		base.Projectile.velocity = Vector2.Zero;
		for (int i = 0; i < (int)(12f * base.Projectile.scale); i++)
		{
			bool noFall = !Main.rand.NextBool(5);
			int dustStyle = (noFall ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalPlasmaDust);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle);
			dust.scale = Main.rand.NextFloat(0.9f, 1.7f) * (noFall ? 2f : 0.75f) * base.Projectile.scale;
			dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(6.4f, 7.5f) * (noFall ? 0.7f : 1.2f) * base.Projectile.scale;
			dust.noGravity = noFall;
			dust.color = ArsenalEffects.ArsenalPlasmaColor;
			dust.fadeIn = (noFall ? (4.5f * base.Projectile.scale) : 1f);
		}
		SoundEngine.PlaySound(AqueousHunterDrone.Hit with
		{
			Volume = 0.4f * base.Projectile.scale,
			Pitch = Main.rand.NextFloat(-0.1f, 0.1f),
			MaxInstances = 30
		}, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPlasmaColor * 0.95f, "CalamityMod/Particles/SmokeExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.1f * base.Projectile.scale, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPlasmaColor * 0.75f, "CalamityMod/Particles/WaterFoam", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.77f * base.Projectile.scale, (int)(10f * lifetimeMult), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, (int)(17f * lifetimeMult), 0.6f * base.Projectile.scale, ArsenalEffects.ArsenalPlasmaColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true));
		if (hitTile)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, (int)(20f * lifetimeMult), 0.6f * base.Projectile.scale * lifetimeMult, ArsenalEffects.ArsenalPlasmaColor * 0.35f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 0.35f));
		}
		if (base.Projectile.scale > 1f)
		{
			Main.player[base.Projectile.owner].SetScreenshake(2.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.numHits == 0)
		{
			Explode();
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			float minMult = 0.5f;
			int hitsToMinMult = 8;
			float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
			modifiers.SourceDamage *= damageMult * 0.5f;
		}
	}

	public override bool? CanDamage()
	{
		if ((!(time > 5f) || base.Projectile.ai[2] != 0f) && !(base.Projectile.scale > 1f))
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)(base.Projectile.width * ((base.Projectile.numHits <= 0) ? 1 : 10)) * base.Projectile.scale, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public ShrimpPlasmaMissile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		offset = Vector2.Zero;
		base._002Ector();
	}
}
