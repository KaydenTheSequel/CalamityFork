using System;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class TransformerBlob : ModProjectile, ILocalizedModType, IModType
{
	private float radius;

	public bool canDamage;

	public float speed;

	public float rotationAngle;

	public int time;

	public int currentLayer;

	public float sine;

	public float rotSpeed;

	public int savedFrame;

	public int poweredTimerMax;

	public int poweredTimer;

	public Color cl1;

	public Color cl2;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public bool visuals => moddedOwner.transformerVisual;

	public float visualMult
	{
		get
		{
			if (visuals || powered)
			{
				if (!visuals)
				{
					return 0.4f;
				}
				return 1f;
			}
			return 0.2f;
		}
	}

	public ref float layer => ref base.Projectile.ai[0];

	public bool powered => base.Projectile.localAI[0] == 5f;

	public float poweredLerp => (float)Math.Pow(Utils.GetLerpValue(poweredTimerMax, 90f, poweredTimer, clamped: true), 4.0);

	public override void SendExtraAI(BinaryWriter writer)
	{
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 16;
		ProjectileID.Sets.NoLiquidDistortion[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 56;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60 * base.Projectile.MaxUpdates;
		base.Projectile.ArmorPenetration = 25;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref cl1)).ToVector3() * 0.5f);
		this.sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)((base.Projectile.ai[1] % 2f == 0f) ? 10 : 6) / (float)Math.PI) * 0.4f;
		if (!powered)
		{
			poweredTimerMax = (int)(140f + MathHelper.Clamp(Utils.Remap(Owner.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()], 1f, 30f, 7f, 6f, clamped: false), 1f, 120f) * base.Projectile.ai[1]);
		}
		if (time == 0)
		{
			base.Projectile.netUpdate = true;
			rotationAngle = base.Projectile.ai[2];
			base.Projectile.frame = 8;
		}
		if (poweredTimer == -1 && powered)
		{
			poweredTimer = poweredTimerMax;
			savedFrame = base.Projectile.frame;
		}
		if (time >= 40)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6 * base.Projectile.MaxUpdates)
			{
				if (powered && base.Projectile.frame == 8)
				{
					base.Projectile.frame = 8;
				}
				else
				{
					base.Projectile.frame++;
				}
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= 16)
			{
				base.Projectile.frame = 0;
			}
		}
		else
		{
			base.Projectile.frame = (int)MathHelper.Lerp((float)savedFrame, 8f, poweredLerp);
		}
		layer = (int)(Utils.GetLerpValue(0f, 10f, base.Projectile.ai[1]) + 0.9f);
		if (time >= 90)
		{
			canDamage = true;
		}
		if (!moddedOwner.transformer || Owner.dead)
		{
			base.Projectile.Kill();
		}
		if (layer != (float)currentLayer)
		{
			currentLayer = (int)layer;
			if (time > 60)
			{
				time = 60;
			}
		}
		rotationAngle = MathHelper.Lerp(rotationAngle, base.Projectile.ai[2], 0.025f);
		if (time >= 40)
		{
			if (poweredTimer == 1)
			{
				base.Projectile.netUpdate = true;
				if (visuals)
				{
					Owner.SetScreenshake(3.5f);
				}
				base.Projectile.numHits = 0;
				base.Projectile.velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * 12f;
				base.Projectile.extraUpdates = 8;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				for (int j = 0; j <= 9; j++)
				{
					float variance = Main.rand.NextFloat(-0.6f, 0.6f);
					int dustStyle = 278;
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle, base.Projectile.velocity);
					dust.scale = Main.rand.NextFloat(0.9f, 1.2f) - Math.Abs(variance);
					dust.velocity = (base.Projectile.velocity * 2f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance));
					dust.noGravity = true;
					dust.color = cl1;
				}
				if (visuals)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/OmicronBeam");
					style.Volume = 0.3f;
					style.Pitch = Math.Clamp(Main.rand.NextFloat(0.1f, 0.2f) + base.Projectile.ai[1] * 0.02f, 0f, 1f);
					style.MaxInstances = 1;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			else if (poweredTimer == 0)
			{
				float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
				Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 20f;
				if (Vector2.Distance(Owner.Center, base.Projectile.Center) < 1400f)
				{
					if (visuals)
					{
						GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity, -base.Projectile.velocity * 0.3f, affectedByGravity: false, 21, 0.04f, cl1 * 0.65f, new Vector2(0.6f, 0.5f), quickShrink: true, glow: false, 0.7f));
					}
					if (time % 2 == 0)
					{
						Vector2 dustVel = (-base.Projectile.velocity).RotatedByRandom(0.30000001192092896);
						Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<VoidDustInverted>(), dustVel * Main.rand.NextFloat(0.1f, 0.8f));
						dust2.noGravity = true;
						dust2.scale = Main.rand.NextFloat(0.4f, 0.6f);
						dust2.color = new Color(30, 30, 30);
						dust2.noLightEmittence = true;
					}
				}
				base.Projectile.rotation = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(-90f)).ToRotation();
			}
			else
			{
				Vector2 centerPoint = (powered ? Vector2.Lerp(Owner.Center, Vector2.Lerp(Owner.Calamity().mouseWorld, Owner.Center, 0.6f), poweredLerp) : Owner.Center);
				float layerDist = Utils.Remap(Owner.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()], 1f, 20f, 60f, 55f, clamped: false) * layer;
				float fadeValue = MathHelper.Clamp(Utils.Remap(base.Projectile.ai[1], 1f, 10f, 0.3f, 0.1f, clamped: false), 0.3f, 0f);
				float positioning = (-60f - layerDist + 30f * this.sine) * MathHelper.Clamp(powered ? (1f - poweredLerp) : 1f, fadeValue, 1f);
				base.Projectile.velocity = (centerPoint + Utils.RotatedBy(new Vector2(0f, positioning), (double)(rotationAngle * (0f - rotSpeed) + Main.GlobalTimeWrappedHourly * 2.5f * (1f - layer * (powered ? 0.33f : 0.15f)) * (float)((layer % 2f != 0f) ? 1 : (-1))), default(Vector2)) - base.Projectile.Center) / speed;
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(this.sine, 0.02f);
			}
			if (powered && poweredTimer != 0)
			{
				rotSpeed *= Utils.Remap(base.Projectile.ai[1], 1f, 10f, 1.0067f, 1.0058f, clamped: false);
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).RotatedBy(MathHelper.ToRadians(-90f)).ToRotation(), poweredLerp);
			}
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.965f;
			base.Projectile.rotation = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(-90f)).ToRotation();
		}
		speed = MathHelper.Lerp(speed, 1f, (float)Math.Pow(Utils.GetLerpValue(0f, 180f, time), 2.0));
		if (poweredTimer != 0)
		{
			base.Projectile.timeLeft++;
		}
		if (poweredTimer > 0)
		{
			poweredTimer--;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.life <= 0 && target.realLife == -1)
		{
			base.Projectile.numHits--;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		if (poweredTimer == 0)
		{
			float minMult = 0.05f;
			int hitsToMinMult = 4;
			float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult) * ((base.Projectile.numHits == 0) ? 1.5f : 1f);
			modifiers.SourceDamage *= damageMult + (Main.zenithWorld ? (0.2f * base.Projectile.ai[1]) : 0f);
		}
		else
		{
			modifiers.SourceDamage *= 0.2f;
		}
		target.MoveNPC(Owner.Center.DirectionTo(target.Center), (poweredTimer == 0) ? 7 : 3);
		if (base.Projectile.numHits == 0 && poweredTimer == 0)
		{
			for (int i = 0; i <= 6; i++)
			{
				float variance = Main.rand.NextFloat(-0.4f, 0.4f);
				Vector2 fxVel = (base.Projectile.velocity * 3f).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + fxVel, fxVel, affectedByGravity: false, 45, Main.rand.NextFloat(0.8f, 1f) - Math.Abs(variance), Main.rand.NextBool(4) ? cl2 : cl1));
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 14; i++)
		{
			Vector2 dustVel = (Vector2.One * 5f).RotatedByRandom(100.0);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<VoidDustInverted>(), dustVel * Main.rand.NextFloat(0.1f, 0.8f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.1f);
			dust.color = new Color(30, 30, 30);
			dust.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		Texture2D orbTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/TheTransformer", (AssetRequestMode)2).Value;
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Rectangle frame = orbTexture.Frame(1, 16, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Color val;
		if (powered)
		{
			val = cl1;
			((Color)(ref val)).A = 0;
			_ = val * poweredLerp * visualMult;
			for (int i = 0; i < 2; i++)
			{
				float bScale2 = 0.25f;
				Vector2 position = base.Projectile.Center - Main.screenPosition;
				val = cl2;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(bTexture, position, null, val * Utils.GetLerpValue(poweredTimerMax, 180f, poweredTimer, clamped: true) * visualMult, base.Projectile.rotation, bTexture.Size() * 0.5f, Vector2.Lerp(new Vector2(0.6f, 1.4f), Vector2.One, MathHelper.Min(Utils.GetLerpValue(8f, 15f, base.Projectile.frame, clamped: true), Utils.GetLerpValue(8f, 0f, base.Projectile.frame, clamped: true))) * bScale2 * base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(orbTexture, base.Projectile.Center - Main.screenPosition, frame, Color.White * visualMult, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		for (int j = 0; j < 10; j++)
		{
			val = cl1;
			((Color)(ref val)).A = 0;
			Color auraColor = val * (powered ? ((float)Math.Pow(Utils.GetLerpValue(poweredTimerMax, 20f, poweredTimer, clamped: true), 3.0)) : sine) * 0.6f * visualMult;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)j / 10f).ToRotationVector2() * 4f;
			Main.EntitySpriteDraw(orbTexture, base.Projectile.Center - Main.screenPosition + drawOffset, frame, auraColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public TransformerBlob()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		radius = 25f;
		speed = 250f;
		sine = 1f;
		rotSpeed = 1f;
		poweredTimerMax = 140;
		poweredTimer = -1;
		cl1 = Color.LightSkyBlue;
		cl2 = Color.DodgerBlue;
		base._002Ector();
	}
}
