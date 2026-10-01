using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.DataStructures;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CnidarianJellyfishOnTheString : ModProjectile, ILocalizedModType, IModType
{
	public const int SegmentCount = 10;

	public const float SegmentDistance = 20f;

	public static int FadeoutTime = 20;

	public static int ElectrifyTimer = 180;

	public static float ZapDamageMultiplier = 0.5f;

	public static readonly SoundStyle ZapSound = SoundID.Item94 with
	{
		Volume = SoundID.Item94.Volume * 0.5f
	};

	public static readonly SoundStyle SlapSound = new SoundStyle("CalamityMod/Sounds/Custom/WetSlap", 4);

	public List<VerletSimulatedSegment> Segments;

	public CalamityUtils.CurveSegment anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0f, 1f, 0.35f, 3);

	public CalamityUtils.CurveSegment contraction = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.5f, 1.35f, -0.85f, 5);

	public CalamityUtils.CurveSegment retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineInOut, 0.7f, 0.5f, 0.5f);

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Initialized => ref base.Projectile.ai[0];

	public ref float Timer => ref base.Projectile.ai[1];

	public Vector2 CnidarianPos
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			return Segments[9].position;
		}
	}

	public float TotalChainLength => 90f;

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = -1;
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.scale = 1.15f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public void SetOrigin(Vector2 position)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = position;
		Vector2 val = base.Projectile.Center - Owner.Center;
		if (((Vector2)(ref val)).Length() > 380f * Owner.whipRangeMultiplier)
		{
			base.Projectile.Center = Owner.Center + (base.Projectile.Center - Owner.Center).SafeNormalize(Vector2.One) * 380f * Owner.whipRangeMultiplier;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), CnidarianPos - base.Projectile.Hitbox.Size() / 2f, base.Projectile.Hitbox.Size());
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public void Initialize()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		SetOrigin(Owner.Calamity().mouseWorld);
		Segments = new List<VerletSimulatedSegment>(10);
		for (int i = 0; i < 10; i++)
		{
			VerletSimulatedSegment segment = new VerletSimulatedSegment(base.Projectile.Center + Vector2.UnitY * 20f * (float)i);
			Segments.Add(segment);
		}
		Segments[0].locked = true;
		int j = 0;
		foreach (VerletSimulatedSegment segment2 in Segments)
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(segment2.position, Vector2.UnitY * (-1f * (float)j / 10f), Color.White, Color.Cyan, 1f, 10));
			j++;
		}
		Initialized = 1f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		if (Initialized == 0f)
		{
			Initialize();
		}
		if (Owner.channel)
		{
			base.Projectile.timeLeft = FadeoutTime;
		}
		SetOrigin(base.Projectile.Center.MoveTowards(Owner.Calamity().mouseWorld, 10f));
		SimulateSegments();
		Electrify(3, 300f);
		Vector2 val = base.Projectile.Center - Owner.Center;
		if (((Vector2)(ref val)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
		Timer++;
	}

	public void Electrify(int maxTargets, float targettingDistance)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		float timeAfterZap = MathHelper.Clamp(20f - (Timer - 20f) % (float)ElectrifyTimer, 0f, 20f);
		float postZapTime = 1f - timeAfterZap / 20f;
		Vector2 cnidarianPos = CnidarianPos;
		Color newColor = Color.DeepSkyBlue;
		Lighting.AddLight(cnidarianPos, ((Color)(ref newColor)).ToVector3() * (1f - postZapTime));
		if (Timer % (float)ElectrifyTimer != (float)(ElectrifyTimer - 1))
		{
			return;
		}
		SoundEngine.PlaySound(in ZapSound, CnidarianPos);
		int maxDust = 2 + Main.rand.Next(3);
		for (int i = 0; i < maxDust; i++)
		{
			Vector2 center = base.Projectile.Center;
			float speedX = -3f + Main.rand.NextFloat(0f, 6f);
			float scale = Main.rand.NextFloat(0.2f, 1f);
			newColor = default(Color);
			Dust.NewDustDirect(center, 0, 0, 226, speedX, -5f, 0, newColor, scale);
			Vector2 cnidarianPos2 = CnidarianPos;
			float speedX2 = -4f + Main.rand.NextFloat(0f, 8f);
			scale = Main.rand.NextFloat(0.2f, 1f);
			newColor = default(Color);
			Dust.NewDustDirect(cnidarianPos2, 0, 0, 226, speedX2, -3f, 0, newColor, scale);
		}
		int[] targetArray = new int[maxTargets];
		int targetsAquired = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (targetsAquired == maxTargets)
			{
				break;
			}
			if (n.CanBeChasedBy(base.Projectile))
			{
				Vector2 val = CnidarianPos - n.Center;
				if (((Vector2)(ref val)).Length() < targettingDistance)
				{
					targetArray[targetsAquired] = n.whoAmI;
					targetsAquired++;
				}
			}
		}
		if (targetsAquired <= 0)
		{
			return;
		}
		for (int j = 0; j < targetsAquired; j++)
		{
			Vector2 velocity = (Main.npc[targetArray[j]].Center - CnidarianPos).SafeNormalize(Vector2.Zero) * 10f;
			for (int k = 0; k < 3; k++)
			{
				Color bloomColor = (Main.rand.NextBool() ? (Main.rand.NextBool() ? Color.Gold : Color.Cyan) : Color.SpringGreen);
				GeneralParticleHandler.SpawnParticle(new ElectricSpark(CnidarianPos, velocity.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(0.2f, 1.3f), Color.Gold, bloomColor, 0.5f + Main.rand.NextFloat(0.5f), 30, (float)Math.PI / 4f, 10f, 1f, 2f));
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), CnidarianPos, velocity, ModContent.ProjectileType<CnidarianSpark>(), (int)((float)base.Projectile.damage * ZapDamageMultiplier), base.Projectile.knockBack, base.Projectile.owner, targetArray[j]);
			}
		}
	}

	public void SimulateSegments()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (Segments == null)
		{
			Segments = new List<VerletSimulatedSegment>(10);
			for (int i = 0; i < 10; i++)
			{
				Segments[i] = new VerletSimulatedSegment(base.Projectile.Center);
			}
		}
		Segments[0].oldPosition = Segments[0].position;
		Segments[0].position = base.Projectile.Center;
		Segments = VerletSimulatedSegment.SimpleSimulation(Segments, 20f);
		base.Projectile.ForceNetUpdate();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Segments[9].position - Segments[9].oldPosition;
		float centrifugalForce = Math.Clamp(((Vector2)(ref val)).Length() * 2f, 0f, 130f) / 130f;
		if (centrifugalForce > 0.2f)
		{
			SoundStyle style = SlapSound with
			{
				Volume = SlapSound.Volume * centrifugalForce + 0.8f
			};
			SoundEngine.PlaySound(in style, target.position);
			Owner.MinionAttackTargetNPC = target.whoAmI;
			target.AddBuff(ModContent.BuffType<CnidarianSummonTagBuff>(), 240);
		}
	}

	internal float StretchRatio()
	{
		return CalamityUtils.PiecewiseAnimation(MathHelper.Clamp((Timer + 45f) % (float)ElectrifyTimer, 0f, 80f) / 80f, anticipation, contraction, retract);
	}

	public float PrimWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 1.6f;
	}

	public Color PrimColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		float timeAfterZap = MathHelper.Clamp(20f - (Timer - 5f - completionRatio * 12f) % (float)ElectrifyTimer, 0f, 20f);
		float postZapTime = 1f - timeAfterZap / 20f;
		Color startingColor = Color.Lerp(Color.Cyan, Color.Maroon, (float)Math.Pow(postZapTime, 2.0)) * ((float)base.Projectile.timeLeft / (float)FadeoutTime);
		return Color.Lerp(Color.DarkCyan * 0f, startingColor, (float)Math.Pow(completionRatio, 1.5)) * 0.7f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		PrimitiveRenderer.RenderTrail(Segments.Select(delegate(VerletSimulatedSegment x)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x.position;
		}).ToArray(), new PrimitiveSettings(PrimWidthFunction, PrimColorFunction), 66);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 squish = default(Vector2);
		((Vector2)(ref squish))._002Ector(2f - StretchRatio(), StretchRatio());
		Vector2 val = Segments[9].position - Segments[9].oldPosition;
		float centrifugalForce = Math.Clamp(((Vector2)(ref val)).Length() * 2f - 10f, 0f, 130f) / 150f;
		Vector2 centrifugalSquish = default(Vector2);
		((Vector2)(ref centrifugalSquish))._002Ector(1f - centrifugalForce * 0.66f, 1f + centrifugalForce * 2.2f);
		squish *= centrifugalSquish;
		float rotation = (Segments[9].position - Segments[8].position).ToRotation() - (float)Math.PI / 2f;
		lightColor = Lighting.GetColor((int)CnidarianPos.X / 16, (int)CnidarianPos.Y / 16);
		Main.EntitySpriteDraw(tex, Segments[9].position - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * ((float)base.Projectile.timeLeft / (float)FadeoutTime), rotation, tex.Size() / 2f, base.Projectile.scale * squish, (SpriteEffects)0);
		float timeAfterZap = MathHelper.Clamp(20f - (Timer - 15f) % (float)ElectrifyTimer, 0f, 20f);
		float postZapTime = 1f - timeAfterZap / 20f;
		if (postZapTime < 1f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Main.EntitySpriteDraw(tex, Segments[9].position - Main.screenPosition, null, Color.DeepSkyBlue * (1f - postZapTime), rotation, tex.Size() / 2f, (base.Projectile.scale + postZapTime * 1.4f) * squish, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(Segments[9].position);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 sentPos = reader.ReadVector2();
		if (Segments != null)
		{
			try
			{
				Segments[9].position = sentPos;
			}
			catch (Exception)
			{
				CalamityMod.Log.Warn((object)"IbanPlay Victide Cnidarian Position Netcode failed safely");
			}
		}
	}
}
