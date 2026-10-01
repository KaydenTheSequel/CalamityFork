using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class VanquisherArrowProj : ModProjectile, ILocalizedModType, IModType
{
	public bool Phase2;

	public float HomingTime;

	public Color MainColor;

	public NPC targeted;

	public int rotDir;

	public float rotSpeed;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/VanquisherArrow";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float ProjectileSpeed => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 35;
		base.Projectile.height = 35;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 7;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.extraUpdates;
	}

	public override void AI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		if (targeted != null && base.Projectile.localNPCImmunity[targeted.whoAmI] == -1)
		{
			base.Projectile.localNPCImmunity[targeted.whoAmI] = 15 * base.Projectile.extraUpdates;
		}
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.Cyan,
			Color.Magenta
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		MainColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (Time == 0f)
		{
			rotDir = ((!Main.rand.NextBool()) ? 1 : (-1));
			rotSpeed = Main.rand.NextFloat(0.8f, 1.1f);
			base.Projectile.scale = 0.014f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f;
			ProjectileSpeed = 30f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (Time > 4f && Main.rand.NextBool(6))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 226 : 272, -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.15f, 0.35f);
			dust.noLightEmittence = true;
		}
		if (Time > 45f)
		{
			if (targeted != null)
			{
				Phase2 = true;
				if (HomingTime == 0f)
				{
					HomingTime = 1f;
				}
				if (targeted.life <= 0)
				{
					targeted = null;
				}
			}
			else
			{
				targeted = base.Projectile.Center.ClosestNPCAt(450f);
			}
		}
		if (HomingTime > 0f && HomingTime < 2f && targeted != null)
		{
			base.Projectile.timeLeft++;
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.65f, 7f, 0.98f, 0.95f, accelerate: true);
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 7f)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 7f, 0.04f);
		}
		if (HomingTime > 1f)
		{
			HomingTime--;
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.2f * (float)rotDir * rotSpeed * Utils.GetLerpValue(15f, 5f, HomingTime, clamped: true));
		}
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HomingTime = Main.rand.Next(12, 16) * base.Projectile.extraUpdates;
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= ((base.Projectile.numHits == 0) ? 0.4f : 1f);
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits <= 0)
		{
			return;
		}
		Vector2 vel = Utils.RotatedByRandom(new Vector2(0.1f, 0.1f), 100.0);
		GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(base.Projectile.Center, vel, affectedByGravity: false, 9, Main.rand.NextFloat(0.15f, 0.25f), Main.rand.NextBool() ? Color.Magenta : Color.Cyan));
		for (int j = -1; j <= 1; j += 2)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), vel.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(2f, 12.5f) * (float)j);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.7f);
				dust.color = (Main.rand.NextBool() ? Color.Magenta : Color.Cyan);
				dust.noLightEmittence = true;
				dust.fadeIn = 1f;
			}
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ScorpioHit");
		style.Volume = 0.25f;
		style.Pitch = 0.1f;
		style.PitchVariance = 0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot with
		{
			Volume = 0.4f,
			Pitch = -0.4f,
			PitchVariance = 0.3f
		}, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			return false;
		}
		Asset<Texture2D> arrow = ModContent.Request<Texture2D>("CalamityMod/Items/Ammo/VanquisherArrow", (AssetRequestMode)2);
		Asset<Texture2D> glow = ModContent.Request<Texture2D>("CalamityMod/Items/Ammo/VanquisherArrowGlow", (AssetRequestMode)2);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineSoftEdge", (AssetRequestMode)2).Value;
		if (Time > 6f)
		{
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Projectile projectile = base.Projectile;
			int mode = ProjectileID.Sets.TrailingMode[base.Type];
			Color mainColor = MainColor;
			((Color)(ref mainColor)).A = 0;
			CalamityUtils.DrawAfterimagesCentered(projectile, mode, mainColor * 0.6f, 1, texture);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
		Main.EntitySpriteDraw(arrow.Value, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, arrow.Size() / 2f, 1f, (SpriteEffects)0);
		Main.EntitySpriteDraw(glow.Value, base.Projectile.Center - Main.screenPosition, null, Color.Lerp(MainColor, Color.White, 0.6f), base.Projectile.rotation, glow.Size() / 2f, 1f, (SpriteEffects)0);
		return false;
	}
}
