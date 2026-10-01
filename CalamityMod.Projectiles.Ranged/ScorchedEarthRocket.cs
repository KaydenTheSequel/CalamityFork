using System.IO;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ScorchedEarthRocket : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public bool BonusEffectMode;

	public bool HasHit;

	public bool SetLifetime;

	public static readonly SoundStyle RocketExplosion = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunMPFBExplosion");

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 34);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HasHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HasHit = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 6;
		}
		if (time <= 20)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			base.Projectile.frame = 0;
			base.Projectile.frameCounter = 0;
		}
		if (time == 24)
		{
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/ScorchedEarthShot", 3);
			soundStyle.Volume = 0.25f;
			soundStyle.MaxInstances = 8;
			soundStyle = soundStyle with
			{
				Pitch = -0.1f
			};
			SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
		}
		if (time > 24 && time < 34)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.5f;
		}
		if (time >= 24)
		{
			Lighting.AddLight(base.Projectile.Center, 1f, 0.79f, 0.3f);
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= 5)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame++;
				if (base.Projectile.frame >= Main.projFrames[base.Type])
				{
					base.Projectile.frame = 6;
				}
			}
		}
		if (time >= 37 && time < 67)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 350f, 20f, 6f);
		}
		BonusEffectMode = base.Projectile.ai[2] == 2f;
		if (BonusEffectMode)
		{
			if (!SetLifetime)
			{
				base.Projectile.timeLeft = 60;
				base.Projectile.extraUpdates = 0;
				base.Projectile.damage = 0;
				base.Projectile.alpha = 255;
				SetLifetime = true;
			}
			if (RocketID == 772f || RocketID == 774f || RocketID == 4458f)
			{
				CalamityUtils.RocketBehaviorInfo info = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
				int blastRadius = (int)((float)base.Projectile.RocketBehavior(info) * 3f);
				if (time % 5 == 0)
				{
					base.Projectile.ExplodeTiles((int)((float)blastRadius * Utils.Remap(time, 60f, 1f, 1f, 0f)), info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
				}
			}
			else
			{
				Point center = base.Projectile.Center.ToTileCoordinates();
				int blastRadius2 = CalamityUtils.RocketBehavior(info: new CalamityUtils.RocketBehaviorInfo((int)RocketID), proj: base.Projectile);
				if (RocketID == 4459f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(time, 60f, 1f, 1f, 0f);
					if (time == 0)
					{
						Utils.PlotTileArea(center.X * blastRadius2, center.Y * blastRadius2, DelegateMethods.SpreadDry);
					}
				}
				if (RocketID == 4447f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(time, 60f, 1f, 1f, 0f);
					if (time == 0)
					{
						Utils.PlotTileArea(center.X * blastRadius2, center.Y * blastRadius2, DelegateMethods.SpreadWater);
					}
				}
				if (RocketID == 4448f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(time, 60f, 1f, 1f, 0f);
					if (time == 0)
					{
						Utils.PlotTileArea(center.X * blastRadius2, center.Y * blastRadius2, DelegateMethods.SpreadLava);
					}
				}
				if (RocketID == 4449f)
				{
					DelegateMethods.f_1 = 10.5f * Utils.Remap(time, 60f, 1f, 1f, 0f);
					if (time == 0)
					{
						Utils.PlotTileArea(center.X * blastRadius2, center.Y * blastRadius2, DelegateMethods.SpreadHoney);
					}
				}
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HasHit = true;
		base.Projectile.netUpdate = true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		HasHit = true;
		base.Projectile.netUpdate = true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		HasHit = true;
		base.Projectile.netUpdate = true;
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		if (!HasHit || BonusEffectMode || base.Projectile.ai[2] != 0f)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		SoundStyle style = RocketExplosion with
		{
			MaxInstances = 2
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 15; i++)
		{
			Vector2 randVel = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Black, Main.rand.Next(20, 26), Main.rand.NextFloat(0.9f, 2.3f), 0.7f));
		}
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Main.rand.NextBool() ? Color.OrangeRed : (Color.DarkGoldenrod * 0.8f), "CalamityMod/Particles/ShineExplosion1", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.2f, 20, UseAdditiveBlend: true, 1.4f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.18f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Red, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f, 1.8f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.5f, 0.8f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.13f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			bool isClusterRocket = RocketID == 4445f || RocketID == 4446f;
			if (RocketID == 772f || RocketID == 774f || RocketID == 4458f || RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ScorchedEarthRocket>(), 0, 0f, base.Projectile.owner, RocketID, 0f, 2f);
			}
			float blastSize = 300f;
			float minMultiplier = 0.25f;
			int hitsToMinMult = 4;
			int debuff1 = 189;
			int debuff2 = 204;
			int debuffTime = 360;
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * (isClusterRocket ? 0.75f : 1f)), base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.localAI[0] = debuff1;
			projectile.localAI[2] = debuff2;
			projectile.localAI[1] = debuffTime;
			projectile.timeLeft = 15;
			projectile.DamageType = DamageClass.Ranged;
			for (int k = 0; k < (isClusterRocket ? 9 : 5); k++)
			{
				Vector2 velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(8f, 10f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<ScorchedEarthClusterBomb>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack * 0.25f, base.Projectile.owner);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (time < 1)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Projectile.type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
