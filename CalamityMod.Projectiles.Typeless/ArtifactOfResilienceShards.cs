using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class ArtifactOfResilienceShards : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 goalPosition;

	public bool behind;

	public int relicType = 1;

	public float orbitSine;

	public int burstTimer;

	public float speedMult = 1f;

	public float placementMult = 1f;

	public float orbitRot;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public bool orbiting => base.Projectile.ai[1] == 0f;

	public Player Owner => Main.player[base.Projectile.owner];

	public bool isAttacking
	{
		get
		{
			if (burstTimer == 0 && !orbiting)
			{
				return base.Projectile.ai[1] != -1f;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 230;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30 * base.Projectile.MaxUpdates;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void AI()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0923: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		float sine = (float)Math.Sin(time * 0.03f * speedMult / (float)Math.PI);
		float sine2 = (float)Math.Sin(time * (0.015f * speedMult) / (float)Math.PI);
		float sineNumberThreeSurelyWeNeedAThirdSineYouWillNotRegretAThirdSine = (float)Math.Sin((Main.GlobalTimeWrappedHourly + (float)Owner.Calamity().rOfResilienceOrbitOffset) * 4.5f / (float)Math.PI);
		orbitSine = MathHelper.Lerp(Math.Abs(sine2), 0.1f, 1f - Math.Abs(sine2));
		float shardNumMult = Utils.GetLerpValue(-10f, 30f, Owner.ownedProjectileCounts[ModContent.ProjectileType<ArtifactOfResilienceShards>()], clamped: true) * (float)((!Owner.Calamity().profanedSoulRelicBuff) ? 1 : 2);
		float displace = Utils.RotatedBy(new Vector2(25f, 0f), (double)(sineNumberThreeSurelyWeNeedAThirdSineYouWillNotRegretAThirdSine * 0.5f), default(Vector2)).ToRotation();
		orbitRot = orbitRot.AngleLerp(displace, 0.01f);
		goalPosition = Owner.Center + Utils.RotatedBy(new Vector2(250f * sine * shardNumMult * placementMult, (125f * orbitSine - 45f) * shardNumMult * placementMult), (double)orbitRot, default(Vector2));
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Lerp(Color.White, Color.Sienna, 0.5f);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.8f);
		if (Owner.Calamity().rOfResilienceCooldown > 0 && base.Projectile.ai[1] == 0f)
		{
			burstTimer = 120;
			base.Projectile.ai[1] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (Owner.Calamity().rOfResilienceEffect == 0 && base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = -1f;
			base.Projectile.timeLeft = 95;
			base.Projectile.velocity = base.Projectile.Center.DirectionTo(goalPosition) * Main.rand.NextFloat(1f, 3f);
		}
		if (time == 0f)
		{
			relicType = Main.rand.Next(1, 7);
			speedMult = (Owner.Calamity().profanedSoulRelicBuff ? Main.rand.NextFloat(0.6f, 1.5f) : Main.rand.NextFloat(0.8f, 1.2f));
			placementMult = Main.rand.NextFloat(0.75f, 1.15f);
		}
		if (orbiting)
		{
			base.Projectile.scale = 0.8f + orbitSine * 0.5f;
			base.Projectile.rotation += 0.02f * sine;
			base.Projectile.timeLeft++;
			base.Projectile.Center = goalPosition;
			base.Projectile.velocity = Vector2.Zero;
			if (base.Projectile.scale < 0.9f)
			{
				behind = true;
				base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 0.15f, 0.057f);
			}
			else
			{
				behind = false;
				base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 0.7f, 0.027f);
			}
		}
		else
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
			if (base.Projectile.ai[1] == -1f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.98f;
			}
			else if (burstTimer > 0)
			{
				base.Projectile.extraUpdates = 1;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
				if (base.Projectile.Center.Distance(Owner.Center) < 15f)
				{
					behind = true;
					base.Projectile.Center = Owner.Center;
				}
				else
				{
					Projectile projectile3 = base.Projectile;
					projectile3.Center += base.Projectile.Center.DirectionTo(Owner.Center) * 40f * Utils.GetLerpValue(90f, 0f, burstTimer);
					if (time % 8f == 0f)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(3f, 8f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard" + Main.rand.Next(1, 7), affectedByGravity: true, Main.rand.Next(20, 33), Main.rand.NextFloat(0.65f, 1.1f), Color.White * Main.rand.NextFloat(0.4f, 0.9f), Vector2.One, useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-5f, 5f)));
					}
				}
				burstTimer--;
				if (burstTimer == 0)
				{
					base.Projectile.Opacity = 1f;
					int projNum = Owner.ownedProjectileCounts[ModContent.ProjectileType<ArtifactOfResilienceShards>()] - 4;
					base.Projectile.velocity = ((float)Math.PI * 2f * base.Projectile.ai[2] / (float)projNum).ToRotationVector2() * 15f * speedMult;
					Owner.SetScreenshake(5f);
					if (base.Projectile.ai[2] == 1f)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianShieldDeactivate");
						style.Volume = 0.7f;
						style.Pitch = 0.1f;
						SoundEngine.PlaySound(in style, Owner.Center);
						style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");
						style.Volume = 0.7f;
						style.Pitch = 0f;
						SoundEngine.PlaySound(in style, Owner.Center);
					}
					for (int i = 0; i < 3; i++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(6f, 28f), "CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard" + Main.rand.Next(1, 7), Main.rand.NextBool(3), Main.rand.Next(25, 56), Main.rand.NextFloat(1.2f, 1.7f), Color.White, new Vector2(0.9f, 1.1f), useAddativeBlend: false));
						Vector2 center2 = base.Projectile.Center;
						int type = ModContent.DustType<LightDust>();
						Vector2? velocity = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(5f, 38f);
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
						dust.noGravity = Main.rand.NextBool();
						dust.scale = Main.rand.NextFloat(1.35f, 2.8f);
						dust.color = (Main.rand.NextBool() ? Color.OrangeRed : Color.Sienna);
					}
				}
			}
			else
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 0.97f;
			}
		}
		if (isAttacking && Main.rand.NextBool(4))
		{
			Vector2 center3 = base.Projectile.Center;
			int type2 = ModContent.DustType<LightDust>();
			Vector2? velocity2 = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(4f, 8f);
			newColor = default(Color);
			Dust dust2 = Dust.NewDustPerfect(center3, type2, velocity2, 0, newColor);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.55f, 1.1f) * base.Projectile.Opacity;
			dust2.color = (Main.rand.NextBool() ? Color.Orange : Color.Goldenrod);
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if ((float)Main.rand.Next(0, 101) < Owner.GetTotalCritChance(Owner.GetBestClass()))
		{
			modifiers.SetCrit();
		}
		modifiers.SourceDamage *= (isAttacking ? 1f : 0.45f);
		if (isAttacking)
		{
			target.AddBuff(ModContent.BuffType<ProfanedWeakness>(), 520);
		}
	}

	public override void OnKill(int timeLeft)
	{
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = (Texture2D)(relicType switch
		{
			1 => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard1", (AssetRequestMode)2).Value, 
			2 => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard2", (AssetRequestMode)2).Value, 
			3 => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard3", (AssetRequestMode)2).Value, 
			4 => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard4", (AssetRequestMode)2).Value, 
			5 => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard5", (AssetRequestMode)2).Value, 
			_ => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/ArtifactOfResilienceShard6", (AssetRequestMode)2).Value, 
		});
		if (isAttacking)
		{
			Projectile projectile = base.Projectile;
			Color goldenrod = Color.Goldenrod;
			((Color)(ref goldenrod)).A = 0;
			projectile.DrawProjectileWithBackglow(goldenrod * base.Projectile.Opacity, Color.White * base.Projectile.Opacity, 3f * base.Projectile.scale, tex, null, (SpriteEffects)0);
		}
		else
		{
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor * base.Projectile.Opacity, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		if (behind)
		{
			behindProjectiles.Add(index);
		}
		else
		{
			overPlayers.Add(index);
		}
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[1] != -1f && burstTimer <= 0)
		{
			return null;
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(burstTimer);
		writer.Write7BitEncodedInt(Owner.Calamity().rOfResilienceCooldown);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		burstTimer = reader.Read7BitEncodedInt();
		Owner.Calamity().rOfResilienceCooldown = reader.Read7BitEncodedInt();
	}
}
