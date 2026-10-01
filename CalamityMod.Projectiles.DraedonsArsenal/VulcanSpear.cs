using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class VulcanSpear : ModProjectile, ILocalizedModType, IModType
{
	public NPC targeted;

	public bool stuckInGround;

	public bool canStick = true;

	public int stuckTimer = 420;

	public Vector2 placementCenter;

	public new string LocalizationCategory => "Projectiles.Misc";

	public ref float time => ref base.Projectile.ai[0];

	public bool canDamage
	{
		get
		{
			if (targeted == null)
			{
				return !stuckInGround;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 35);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 1200;
		base.Projectile.extraUpdates = 30;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalGaussColor)).ToVector3() * 0.5f);
		if (num < 1400f && canDamage && time % 2f == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14f, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 12, 0.065f, ArsenalEffects.ArsenalGaussColor, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.4f, 0.7f));
		}
		if (!stuckInGround && targeted == null && Collision.SolidCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f, 6, 6))
		{
			base.Projectile.extraUpdates = 0;
			stuckInGround = true;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit3");
			style.Volume = 0.8f;
			style.Pitch = Main.rand.NextFloat(0.1f, 0.2f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (targeted != null && (!targeted.active || targeted.life <= 0))
		{
			targeted = null;
		}
		if (targeted != null)
		{
			base.Projectile.Center = targeted.Center - placementCenter;
		}
		else if (base.Projectile.numHits > 0)
		{
			base.Projectile.timeLeft = 1;
		}
		if (stuckInGround || targeted != null)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				float dist = base.Projectile.Center.Distance(p.Center);
				if (p.type == ModContent.ProjectileType<VulcanProjectile>() && dist <= 900f && p.ai[0] > 120f)
				{
					if (((Vector2)(ref p.velocity)).Length() < 6f)
					{
						p.velocity += p.Center.DirectionTo(base.Projectile.Center) * Utils.GetLerpValue(1500f, 50f, dist) * 0.9f;
					}
					else
					{
						p.velocity *= 0.9f;
					}
					if (p.ai[0] % 2f == 0f)
					{
						p.timeLeft += p.MaxUpdates;
					}
				}
			}
		}
		if (!canDamage && base.Projectile.timeLeft > 300)
		{
			base.Projectile.timeLeft = 300;
		}
		time++;
		if (base.Projectile.timeLeft == 35)
		{
			SoundStyle sound2 = new SoundStyle("CalamityMod/Sounds/Item/VulcanRampUp");
			for (int i = 0; i < 2; i++)
			{
				SoundEngine.PlaySound(sound2 with
				{
					Volume = 0.9f,
					MaxInstances = 5,
					Pitch = -0.4f
				}, base.Projectile.Center);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit2");
		style.Volume = 0.8f;
		style.Pitch = Main.rand.NextFloat(0.4f, 0.5f);
		style.MaxInstances = 5;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		SoundStyle sound2 = new SoundStyle("CalamityMod/Sounds/Item/NidhoggFire");
		for (int i = 0; i < 2; i++)
		{
			SoundEngine.PlaySound(sound2 with
			{
				Volume = 0.8f,
				MaxInstances = 5,
				Pitch = -0.1f
			}, base.Projectile.Center);
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			float dist = base.Projectile.Center.Distance(p.Center);
			if (p.type == ModContent.ProjectileType<VulcanProjectile>() && dist <= 500f)
			{
				NPC closeTarget = p.Center.ClosestNPCAt(800f);
				if (closeTarget != null)
				{
					p.velocity = p.Center.DirectionTo(closeTarget.Center) * 7f;
				}
				else
				{
					p.velocity = base.Projectile.Center.DirectionTo(p.Center) * 7f;
				}
				p.extraUpdates = 8;
				p.ai[0] = 0f;
				p.penetrate = 5;
				if (p.ai[1] < 4f)
				{
					p.damage = (int)((float)p.damage * 2f);
					p.ai[1]++;
				}
				p.timeLeft = Main.rand.Next(100, 121);
			}
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalGaussColor, "CalamityMod/Particles/GlowSquareParticleThick", Vector2.One, (float)Math.PI / 4f, 0.1f, 0.7f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalGaussColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, 0f, 0.3f, 1.15f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		impactDust();
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = 12f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (target.life <= 0 && target.realLife == -1)
		{
			base.Projectile.numHits--;
		}
		else if (targeted == null && !stuckInGround)
		{
			base.Projectile.extraUpdates = 0;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit1");
			style.Volume = 0.8f;
			style.Pitch = Main.rand.NextFloat(0.1f, 0.2f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			targeted = target;
			placementCenter = targeted.Center - base.Projectile.Center;
		}
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool ShouldUpdatePosition()
	{
		return canDamage;
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
		for (int i = 0; i < 7; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(12f, 19f);
			dust.scale = Main.rand.NextFloat(0.8f, 1.35f);
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalGaussColor;
			dust.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		Texture2D proj = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanSpear", (AssetRequestMode)2).Value;
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanSpearGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		base.Projectile.rotation.ToRotationVector2();
		Color val = ArsenalEffects.ArsenalGaussColor;
		((Color)(ref val)).A = 0;
		float drawRotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = proj.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.direction == -1);
		Main.EntitySpriteDraw(proj, drawPosition, null, lightColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(value, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		int draws = 16;
		for (int i = 0; i < draws; i++)
		{
			float fadeIn = (float)Math.Pow(Utils.GetLerpValue(300f, 1f, base.Projectile.timeLeft, clamped: true), 3.0);
			Vector2 offset = ((float)Math.PI * 2f / (float)draws * (float)i).ToRotationVector2() * 3f * fadeIn;
			Vector2 position = base.Projectile.Center - Main.screenPosition + offset + Main.rand.NextVector2Circular(3f, 3f) * fadeIn;
			val = ArsenalEffects.ArsenalGaussColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(proj, position, null, val * fadeIn * 0.5f, drawRotation, proj.Size() * 0.5f, base.Projectile.scale, flipSprite);
			Vector2 position2 = base.Projectile.Center - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(proj, position2, null, val * fadeIn, drawRotation, proj.Size() * 0.5f, base.Projectile.scale, flipSprite);
		}
		return false;
	}
}
