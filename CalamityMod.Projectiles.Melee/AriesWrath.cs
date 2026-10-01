using System;
using System.Linq;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AriesWrath : ModProjectile, ILocalizedModType, IModType
{
	private NPC[] excludedTargets = new NPC[4];

	private const float MaxProjReach = 500f;

	public Particle smear;

	public Projectile lastConstellation;

	public CalamityUtils.CurveSegment slowIn = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0f, 0.2f, 1f, 3);

	public CalamityUtils.CurveSegment bounce = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.3f, 1f, 0.2f);

	public CalamityUtils.CurveSegment remain = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.6f, 1f, -0.1f);

	public CalamityUtils.CurveSegment scaleUp = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0f, 0.2f, 1f, 3);

	public CalamityUtils.CurveSegment scaleDown = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.3f, 1f, 0.2f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxiaExtra2";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float ChainSwapTimer => ref base.Projectile.ai[0];

	public ref float BlastCooldown => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = FourSeasonsGalaxia.AriesAttunement_LocalIFrames;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - Vector2.One * 50f * base.Projectile.scale, Vector2.One * 100f * base.Projectile.scale);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Vector2 sparkSpeed = Owner.DirectionTo(target.Center).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * 9f;
			GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, sparkSpeed, Color.White, Color.HotPink, 1f + Main.rand.NextFloat(0f, 1f), 30, 0.4f));
		}
		Vector2 sliceDirection = Main.rand.NextVector2CircularEdge(50f, 100f);
		GeneralParticleHandler.SpawnParticle(new LineVFX(target.Center - sliceDirection, sliceDirection * 2f, 0.2f, Color.HotPink * 0.6f)
		{
			Lifetime = 6
		});
		if (BlastCooldown > 0f)
		{
			return;
		}
		excludedTargets[0] = target;
		for (int j = 0; j < 3; j++)
		{
			NPC potentialTarget = TargetNext(target.Center, j);
			if (potentialTarget == null)
			{
				break;
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, target.SafeDirectionTo(potentialTarget.Center, Vector2.Zero) * 25f, ModContent.ProjectileType<GalaxiaBolt>(), (int)((float)hit.Damage * FourSeasonsGalaxia.AriesAttunement_OnHitBoltDamageReduction), 0f, Owner.whoAmI, 0.9f, (float)Math.PI / 10f).scale = 2f;
		}
		Array.Clear(excludedTargets, 0, 3);
		BlastCooldown = 30f;
	}

	public NPC TargetNext(Vector2 hitFrom, int index)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		float longestReach = 500f;
		NPC target = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!Enumerable.Contains(excludedTargets, npc) && npc.CanBeChasedBy() && !npc.friendly && !npc.townNPC)
			{
				float distance = Vector2.Distance(hitFrom, npc.Center);
				if (distance < longestReach)
				{
					longestReach = distance;
					target = npc;
				}
			}
		}
		if (index < 3)
		{
			excludedTargets[index + 1] = target;
		}
		return target;
	}

	internal float ThrowDisplace()
	{
		return CalamityUtils.PiecewiseAnimation(MathHelper.Clamp(ChainSwapTimer / 40f, 0f, 1f), slowIn, bounce, remain);
	}

	internal float ScaleEquation()
	{
		return CalamityUtils.PiecewiseAnimation(MathHelper.Clamp(ChainSwapTimer / 30f, 0f, 1f), scaleUp, scaleDown);
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center;
		if (Owner.CantUseHoldout() || Owner.HeldItem.type != ModContent.ItemType<FourSeasonsGalaxia>())
		{
			center = Owner.Center - base.Projectile.Center;
			if (!(((Vector2)(ref center)).Length() < 30f))
			{
				center = Owner.Center - base.Projectile.Center;
				if (!(((Vector2)(ref center)).Length() > 2000f) && !(((Vector2)(ref base.Projectile.velocity)).Length() > 100f))
				{
					if (base.Projectile.timeLeft <= 2)
					{
						Projectile projectile = base.Projectile;
						projectile.velocity *= 10f;
					}
					if (base.Projectile.velocity.AngleBetween(Owner.Center - base.Projectile.Center) > (float)Math.PI / 4f)
					{
						base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(Owner.Center, Vector2.Zero).ToRotation(), (float)Math.PI / 20f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * 0.98f;
					}
					else
					{
						base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(Owner.Center, Vector2.Zero).ToRotation(), (float)Math.PI).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * 1.05f;
					}
					base.Projectile.rotation = Main.GlobalTimeWrappedHourly * 25f;
					Projectile projectile2 = base.Projectile;
					center = Owner.Center - base.Projectile.Center;
					projectile2.scale = MathHelper.Clamp(((Vector2)(ref center)).Length() / ((float)FourSeasonsGalaxia.AriesAttunement_Reach * 0.5f), 0.3f, 2f);
					base.Projectile.timeLeft = 4;
					return;
				}
			}
			base.Projectile.Kill();
			return;
		}
		if (ChainSwapTimer == 0f)
		{
			base.Projectile.Center = Owner.Center;
			SoundStyle style = SoundID.Item120 with
			{
				Volume = SoundID.Item120.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Main.LocalPlayer.SetScreenshake(3f);
		}
		base.Projectile.scale = 1f + ScaleEquation();
		base.Projectile.timeLeft = 2;
		Vector2 mouse = Owner.ClampedMouseWorld();
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, mouse, 0.05f * ThrowDisplace());
		base.Projectile.Center = base.Projectile.Center.MoveTowards(mouse, 40f * ThrowDisplace());
		center = base.Projectile.Center - Owner.Center;
		if (((Vector2)(ref center)).Length() > (float)FourSeasonsGalaxia.AriesAttunement_Reach)
		{
			base.Projectile.Center = Owner.Center + Owner.SafeDirectionTo(base.Projectile.Center, Vector2.Zero) * (float)FourSeasonsGalaxia.AriesAttunement_Reach;
		}
		base.Projectile.rotation = Main.GlobalTimeWrappedHourly * 25f;
		Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.velocity = Owner.SafeDirectionTo(base.Projectile.Center, Vector2.Zero);
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.velocity.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		if (smear == null)
		{
			smear = new CircularSmearSmokeyVFX(base.Projectile.Center, Color.MediumOrchid, base.Projectile.rotation, base.Projectile.scale);
			GeneralParticleHandler.SpawnParticle(smear);
		}
		if (smear != null)
		{
			smear.Position = base.Projectile.Center;
			smear.Rotation = base.Projectile.rotation + (float)Math.PI / 2f + (float)Math.PI / 4f;
			smear.Time = 0;
			smear.Scale = base.Projectile.scale;
			((Color)(ref smear.Color)).A = (byte)(255f * MathHelper.Clamp(ChainSwapTimer / 50f, 0f, 1f));
		}
		if (Main.rand.NextBool())
		{
			float maxDistance = base.Projectile.scale * 82f;
			Vector2 distance = Main.rand.NextVector2Circular(maxDistance, maxDistance);
			Vector2 spinningpoint = distance;
			center = default(Vector2);
			Vector2 angularVelocity = spinningpoint.RotatedBy(1.5707963705062866, center).SafeNormalize(Vector2.Zero) * 2f * (1f + ((Vector2)(ref distance)).Length() / 15f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + distance, angularVelocity, Main.rand.NextBool(3) ? Color.HotPink : Color.Plum, Color.DarkOrchid, 1f + 1f * (((Vector2)(ref distance)).Length() / maxDistance), 10, 0.05f, 3f));
		}
		float smokeDistance = base.Projectile.scale * 62f;
		Vector2 smokePos = Main.rand.NextVector2Circular(smokeDistance, smokeDistance);
		Vector2 spinningpoint2 = smokePos;
		center = default(Vector2);
		Vector2 smokeSpeed = spinningpoint2.RotatedBy(1.5707963705062866, center).SafeNormalize(Vector2.Zero) * 0.1f * (1f + ((Vector2)(ref smokePos)).Length() / 15f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + smokePos, smokeSpeed, Color.Lerp(Color.Navy, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(0.4f, 1f) * base.Projectile.scale, 0.8f, 0f, glowing: false, 0f, required: true));
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + smokePos, smokeSpeed, Main.hslToRgb(0.85f, 1f, 0.5f), 20, Main.rand.NextFloat(0.4f, 1f) * base.Projectile.scale, 0.8f, 0f, glowing: true, 0.01f, required: true));
		}
		if ((lastConstellation == null || !lastConstellation.active) && Owner.whoAmI == Main.myPlayer && ChainSwapTimer > 20f)
		{
			lastConstellation = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, Vector2.Zero, ModContent.ProjectileType<AriesWrathConstellation>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.AriesAttunement_ChainDamageReduction), 0f, Owner.whoAmI);
		}
		ChainSwapTimer++;
		BlastCooldown--;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxiaExtra2", (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center;
		Main.EntitySpriteDraw(origin: value.Size() / 2f, rotation: base.Projectile.rotation + (float)Math.PI / 4f, texture: value, position: drawPos - Main.screenPosition, sourceRectangle: null, color: lightColor, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
