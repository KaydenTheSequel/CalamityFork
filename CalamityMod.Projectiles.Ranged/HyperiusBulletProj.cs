using System;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HyperiusBulletProj : ModProjectile, ILocalizedModType, IModType
{
	private Color currentColor;

	public float dustAngle;

	public bool growing;

	public bool dustWave;

	public float variance;

	public Vector2 lastPos;

	public int slowdownTime;

	public bool tileTouched;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 15;
		base.Projectile.height = 15;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.extraUpdates = 25;
		base.Projectile.tileCollide = false;
		base.AIType = 14;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		float timeleftFade = (float)Math.Pow(Utils.GetLerpValue(0f, slowdownTime * base.Projectile.extraUpdates, base.Projectile.timeLeft, clamped: true), 1.0);
		if (currentColor == Color.Black)
		{
			slowdownTime = Main.rand.Next(6, 10);
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 20f;
			variance = Main.rand.NextFloat(0.7f, 1f);
			dustAngle = Main.rand.NextFloat(-0.43f, 0.43f);
			dustWave = Math.Sign(dustAngle) == 1;
			base.Projectile.scale = 1.5f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
			lastPos = base.Projectile.velocity;
			switch (Main.rand.Next(0, 5))
			{
			case 4:
				currentColor = Color.Yellow * 0.65f;
				break;
			case 3:
				currentColor = Color.Magenta * 0.65f;
				break;
			case 2:
				currentColor = Color.Red * 0.65f;
				break;
			case 1:
				currentColor = Color.Cyan * 0.65f;
				break;
			default:
				currentColor = Color.Lime * 0.65f;
				break;
			}
		}
		if (dustAngle <= -0.5f)
		{
			growing = true;
		}
		if (dustAngle >= 0.5f)
		{
			growing = false;
		}
		dustAngle += (growing ? (0.07f * variance) : (-0.07f * variance));
		if (Collision.SolidCollision(base.Projectile.Center, 4, 4) && !tileTouched && base.Projectile.numHits == 0)
		{
			tileTouched = true;
			OnHitEffects(null);
		}
		if (base.Projectile.numHits > 0 || tileTouched)
		{
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f * Math.Max(timeleftFade, 0.01f);
			if (base.Projectile.timeLeft > slowdownTime * base.Projectile.extraUpdates)
			{
				base.Projectile.timeLeft = slowdownTime * base.Projectile.extraUpdates;
			}
		}
		base.Projectile.ai[2]++;
		Vector2 orbPos = base.Projectile.Center + (base.Projectile.velocity.RotatedBy((float)(dustWave ? 1 : (-1)) * dustAngle) * 4.5f - base.Projectile.velocity * 5f);
		if (base.Projectile.ai[2] > 15f && targetDist < 1200f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(orbPos - lastPos.DirectionTo(orbPos) * timeleftFade, lastPos.DirectionTo(orbPos) * 0.1f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 5, (0.55f + MathF.Abs(dustAngle * 0.65f)) * 0.15f * timeleftFade, currentColor, new Vector2(1f - MathF.Abs(dustAngle * 0.2f), 2f - (1f - MathF.Abs(dustAngle))), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.8f - MathF.Abs(dustAngle * 0.6f), 0.7f, 0.8f));
			if (Main.rand.NextBool(8))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), -base.Projectile.velocity.RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.9f, 1.8f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.9f) * timeleftFade;
				dust.color = currentColor;
				dust.noLightEmittence = true;
				dust.fadeIn = 1.4f;
			}
		}
		lastPos = orbPos;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.numHits <= 0)
		{
			return null;
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		CalamityGlobalNPC modNPC = target.Calamity();
		if (!modNPC.hyperiusMarked)
		{
			modNPC.hyperiusMarked = true;
		}
		Player Owner = Main.player[base.Projectile.owner];
		bool crit = (float)Main.rand.Next(0, 101) < Owner.GetTotalCritChance(base.Projectile.DamageType);
		modNPC.hyperiusDamage += Math.Max(base.Projectile.damage * ((!crit) ? 1 : 2) - 1, 1);
		modifiers.DisableCrit();
		modifiers.SourceDamage *= 0f;
		modifiers.FinalDamage.Flat = 0.1f;
		modifiers.HideCombatText();
		OnHitEffects(target);
	}

	private void OnHitEffects(NPC target)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ShadowboltWallHit");
			style.Volume = 0.25f;
			style.Pitch = Main.rand.NextFloat(0.6f, 1f);
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int b = 0; b < 2; b++)
			{
				Vector2 velocity = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f * (float)((!tileTouched) ? 1 : (-1))).RotatedByRandom(0.5);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity * 0.7f, ModContent.ProjectileType<HyperiusSplit>(), (int)Math.Max((float)base.Projectile.originalDamage * 0.05f, 1f), 0f, base.Projectile.owner, 0f, 0f, Main.rand.Next(0, 5));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2);
		int startTime = 9;
		int endTime = 35;
		float timeleftFade = (float)Math.Pow(Utils.GetLerpValue(0f, slowdownTime * base.Projectile.extraUpdates, base.Projectile.timeLeft, clamped: true), 1.0);
		Vector2 squash = default(Vector2);
		for (int i = 0; i < 4; i++)
		{
			((Vector2)(ref squash))._002Ector(Utils.Remap(base.Projectile.ai[2], startTime, endTime, 0.2f, 0.6f + (float)i * 0.2f), Utils.Remap(base.Projectile.ai[2], startTime, endTime, 1f, 3f - (float)i * 0.4f));
			Color val = Color.Lerp(currentColor, Color.White, (float)i * 0.2f);
			((Color)(ref val)).A = 0;
			Color orbColor = val * 0.9f;
			Vector2 scale = base.Projectile.scale * timeleftFade * squash * (0.05f - (float)i * 0.008f) * 0.3f;
			Main.EntitySpriteDraw(orb.Value, base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * (float)i * 1.3f, null, orbColor, base.Projectile.rotation, orb.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		return false;
	}

	public HyperiusBulletProj()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		currentColor = Color.Black;
		variance = 0.8f;
		slowdownTime = 7;
		base._002Ector();
	}
}
