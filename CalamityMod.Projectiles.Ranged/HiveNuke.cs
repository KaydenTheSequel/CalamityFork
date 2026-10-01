using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HiveNuke : ModProjectile, ILocalizedModType, IModType
{
	public bool HasHit;

	public float Time;

	public bool BonusEffectMode;

	public bool SetLifetime;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public ref float ProjectileSpeed => ref base.Projectile.ai[1];

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 1500;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		BonusEffectMode = base.Projectile.ai[2] == 2f;
		if (BonusEffectMode)
		{
			if (RocketID == 772f || RocketID == 774f || RocketID == 4458f)
			{
				if (!SetLifetime)
				{
					base.Projectile.timeLeft = 60;
					base.Projectile.extraUpdates = 0;
					base.Projectile.damage = 0;
					base.Projectile.alpha = 255;
					SetLifetime = true;
				}
				CalamityUtils.RocketBehaviorInfo info = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
				int blastRadius = (int)((float)base.Projectile.RocketBehavior(info) * 5f);
				if (Time % 5f == 0f)
				{
					base.Projectile.ExplodeTiles((int)((float)blastRadius * Utils.Remap(Time, 60f, 1f, 1f, 0f)), info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
				}
			}
			else
			{
				Point center = base.Projectile.Center.ToTileCoordinates();
				if (RocketID == 4459f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(Time, 60f, 1f, 1f, 0f);
					if (Time == 0f)
					{
						Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadDry);
					}
				}
				if (RocketID == 4447f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(Time, 60f, 1f, 1f, 0f);
					if (Time == 0f)
					{
						Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadWater);
					}
				}
				if (RocketID == 4448f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(Time, 60f, 1f, 1f, 0f);
					if (Time == 0f)
					{
						Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadLava);
					}
				}
				if (RocketID == 4449f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(Time, 60f, 1f, 1f, 0f);
					if (Time == 0f)
					{
						Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadHoney);
					}
				}
			}
			Time++;
			return;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.wet && (RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f))
		{
			base.Projectile.Kill();
		}
		Time++;
		if (base.Projectile.timeLeft <= 30)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		if (Main.dedServ)
		{
			return;
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 10f, 0f, 0f, 255f);
		Color newColor;
		if (Time % 3f == 0f)
		{
			Vector2 center2 = base.Projectile.Center;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int type = (Main.rand.NextBool() ? 303 : TheHiveHoldout.DustEffectsID);
			float scale = Main.rand.NextFloat(0.3f, 0.6f);
			newColor = default(Color);
			Dust trailDust = Dust.NewDustDirect(center2, width, height, type, 0f, 0f, 0, newColor, scale);
			trailDust.noGravity = true;
			trailDust.noLight = true;
			trailDust.noLightEmittence = true;
			trailDust.velocity = -base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 0.8f);
			if (trailDust.type != TheHiveHoldout.DustEffectsID)
			{
				trailDust.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
			}
		}
		if (Time > 5f)
		{
			Color smokeColor = Color.Lerp(Color.Black, Color.Lime, 0.25f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center - base.Projectile.velocity * 2f, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.6f), smokeColor * 0.65f, 9, Main.rand.NextFloat(0.45f, 0.6f), 0.23f, Main.rand.NextFloat(-0.2f, 0.2f)));
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f) - base.Projectile.velocity * 2.5f;
			Vector2? velocity = -base.Projectile.velocity.RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.2f, 0.9f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, 303, velocity, 0, newColor, Main.rand.NextFloat(0.9f, 1.6f));
			dust.noGravity = false;
			dust.color = Color.Black;
			dust.alpha = Main.rand.Next(90, 221);
		}
		Vector2 center3 = base.Projectile.Center;
		newColor = TheHiveHoldout.StaticEffectsColor;
		Lighting.AddLight(center3, ((Color)(ref newColor)).ToVector3() * 0.7f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.4f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (BonusEffectMode)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/StarProj", (AssetRequestMode)2).Value;
		if (Time > 6f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], TheHiveHoldout.EffectsColor * 0.4f, 1, texture);
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HasHit = true;
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		HasHit = true;
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		bool isClusterRocket = RocketID == 4445f || RocketID == 4446f;
		if (HasHit && !BonusEffectMode)
		{
			if (Main.zenithWorld)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/BEES/bees", 12);
				style.Volume = 1.5f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = new SoundStyle("CalamityMod/Sounds/Item/TheHiveNuke");
				style.Volume = 0.35f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/TheHiveNuke");
				style.Volume = 0.9f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			Owner.SetScreenshake(9.5f);
			CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
			rocketBehaviorInfo.clusterProjectileID = 0;
			rocketBehaviorInfo.destructiveClusterProjectileID = 0;
			CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
			int blastRadius = (int)((float)base.Projectile.RocketBehavior(info) * 5f);
			base.Projectile.ExpandHitboxBy((float)blastRadius);
			if (RocketID == 772f || RocketID == 774f || RocketID == 4458f || RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<HiveNuke>(), 0, 0f, base.Projectile.owner, RocketID, 0f, 2f);
			}
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
			base.Projectile.penetrate = -1;
			base.Projectile.Damage();
			for (int k = 0; k < 3; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, TheHiveHoldout.StaticEffectsColor * 0.8f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), (float)base.Projectile.width / 22815f, (float)base.Projectile.width / (2275f + (float)(520 * k)), 20 + 4 * k, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int i = 0; i < 30; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f), affectedByGravity: false, 60, Main.rand.NextFloat(0.8f, 1.4f), TheHiveHoldout.StaticEffectsColor, AddativeBlend: true, needed: true));
			}
			for (int j = 0; j < 45; j++)
			{
				if (Main.rand.NextBool(5))
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? TheHiveHoldout.DustEffectsID : 303, Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
					dust2.scale = Main.rand.NextFloat(0.85f, 1.25f);
					dust2.noGravity = true;
					if (dust2.type != TheHiveHoldout.DustEffectsID)
					{
						dust2.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
					}
				}
				else
				{
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 303, Utils.RotatedByRandom(new Vector2(35f, 35f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.7f));
					dust3.noGravity = false;
					dust3.color = Color.Black;
					dust3.alpha = Main.rand.Next(90, 221);
				}
			}
			float projAmount = (isClusterRocket ? 30f : 20f);
			if (Main.player[base.Projectile.owner].strongBees)
			{
				projAmount *= 1.15f;
			}
			for (int l = 0; (float)l < projAmount; l++)
			{
				int BEES = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.2f, 0.8f), ModContent.ProjectileType<BasicPlagueBee>(), (int)((float)base.Projectile.damage * (isClusterRocket ? 0.03f : 0.04f)), 0f, base.Projectile.owner, 0f, 0f, isClusterRocket ? 2f : 1f);
				if (BEES.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[BEES].penetrate = 1;
					Main.projectile[BEES].DamageType = DamageClass.Ranged;
				}
			}
		}
		else
		{
			if (BonusEffectMode)
			{
				return;
			}
			for (int m = 0; m <= 15; m++)
			{
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f) - base.Projectile.velocity * 3.5f, Main.rand.NextBool(3) ? TheHiveHoldout.DustEffectsID : 303, base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.4f, 0.9f), 0, default(Color), Main.rand.NextFloat(0.5f, 0.9f));
				dust4.noGravity = false;
				if (dust4.type != TheHiveHoldout.DustEffectsID)
				{
					dust4.color = (Main.rand.NextBool(3) ? TheHiveHoldout.EffectsColor : TheHiveHoldout.StaticEffectsColor);
				}
			}
		}
	}
}
