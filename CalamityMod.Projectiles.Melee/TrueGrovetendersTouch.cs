using System;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
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

public class TrueGrovetendersTouch : ModProjectile, ILocalizedModType, IModType
{
	private NPC[] excludedTargets = new NPC[4];

	private bool initialized;

	public float flipped;

	private const float MaxTime = 90f;

	private const int coyoteTimeFrames = 15;

	private const int MaxReach = 600;

	private const int MinReach = 250;

	private const float SnappingPoint = 0.55f;

	private const float MaxTangleReach = 400f;

	public BezierCurve curve;

	private Vector2 controlPoint1;

	private Vector2 controlPoint2;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_GrovetendersTouchBlade";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => 90f - (float)base.Projectile.timeLeft;

	public ref float HasSnapped => ref base.Projectile.ai[0];

	public ref float SnapCoyoteTime => ref base.Projectile.ai[1];

	public ref float Reach => ref base.Projectile.localAI[0];

	internal bool ReelingBack => Timer / 90f > 0.55f;

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = TrueBiomeBlade.TropicalAttunement_LocalIFrames;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		BezierCurve bezierCurve = new BezierCurve(Owner.MountedCenter, controlPoint1, controlPoint2, base.Projectile.Center);
		int numPoints = 32;
		Vector2[] chainPositions = bezierCurve.GetPoints(numPoints).ToArray();
		float collisionPoint = 0f;
		for (int i = 1; i < numPoints; i++)
		{
			Vector2 position = chainPositions[i];
			Vector2 previousPosition = chainPositions[i - 1];
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), position, previousPosition, 6f, ref collisionPoint))
			{
				return true;
			}
			if (i == numPoints - 1)
			{
				Vector2 projectileHalfLength = 85f * base.Projectile.rotation.ToRotationVector2();
				return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - projectileHalfLength, base.Projectile.Center + projectileHalfLength, 32f, ref collisionPoint);
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Vector2 projectileHalfLength = 85f * base.Projectile.rotation.ToRotationVector2();
		float collisionPoint = 0f;
		if (Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), base.Projectile.Center - projectileHalfLength, base.Projectile.Center + projectileHalfLength, 32f, ref collisionPoint))
		{
			if (SnapCoyoteTime > 0f)
			{
				modifiers.SourceDamage *= TrueBiomeBlade.TropicalAttunement_SweetSpotDamageMultiplier;
				modifiers.SetCrit();
				for (int i = 0; i < 4; i++)
				{
					Vector2 sparkSpeed = Owner.DirectionTo(target.Center).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * 9f;
					GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, sparkSpeed, Color.White, Color.LimeGreen, 1f + Main.rand.NextFloat(0f, 1f), 30, 0.4f, 0.6f));
				}
			}
		}
		else
		{
			modifiers.SourceDamage *= TrueBiomeBlade.TropicalAttunement_ChainDamageReduction;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!hit.Crit)
		{
			return;
		}
		bool boing = false;
		excludedTargets[0] = target;
		for (int i = 0; i < 3; i++)
		{
			NPC potentialTarget = TargetNext(target.Center, i);
			if (potentialTarget == null)
			{
				break;
			}
			if (!boing)
			{
				boing = true;
				SoundEngine.PlaySound(in SoundID.Item56);
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<GrovetendersEntanglingVines>(), (int)((float)damageDone * TrueBiomeBlade.TropicalAttunement_VineDamageReduction), 0f, Owner.whoAmI, target.whoAmI, potentialTarget.whoAmI);
		}
		Array.Clear(excludedTargets, 0, 3);
	}

	public NPC TargetNext(Vector2 hitFrom, int index)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		float longestReach = 400f;
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

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			ref float reach = ref Reach;
			Vector2 val = Owner.Center - Owner.Calamity().mouseWorld;
			reach = MathHelper.Clamp(((Vector2)(ref val)).Length(), 250f, 600f);
			SoundEngine.PlaySound(in SoundID.DD2_OgreSpit, base.Projectile.Center);
			controlPoint1 = base.Projectile.Center;
			controlPoint2 = base.Projectile.Center;
			base.Projectile.timeLeft = 90;
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		if (ReelingBack && HasSnapped == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item41, base.Projectile.Center);
			HasSnapped = 1f;
			SnapCoyoteTime = 15f;
		}
		if (SnapCoyoteTime > 0f)
		{
			Lighting.AddLight(base.Projectile.Center, 0.8f, 1f, 0.35f);
			SnapCoyoteTime--;
		}
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center);
		float ratio = GetSwingRatio();
		base.Projectile.Center = Owner.MountedCenter + SwingPosition(ratio);
		base.Projectile.direction = (base.Projectile.spriteDirection = -Owner.direction * (int)flipped);
		Owner.itemRotation = MathHelper.WrapAngle(Owner.AngleTo(Owner.Calamity().mouseWorld) - ((Owner.direction < 0) ? ((float)Math.PI) : 0f));
	}

	internal static float EaseInFunction(float progress)
	{
		if (progress != 0f)
		{
			return (float)Math.Pow(2.0, 10f * progress - 10f);
		}
		return 0f;
	}

	private float GetSwingRatio()
	{
		float ratio = (Timer - 49.5f) / 40.5f;
		if (!ReelingBack)
		{
			ratio = EaseInFunction(Timer / 49.5f);
		}
		if (SnapCoyoteTime > 0f)
		{
			ratio = 0f;
		}
		return ratio;
	}

	private Vector2 SwingPosition(float progress)
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (!ReelingBack)
		{
			float distance = Reach * MathHelper.Lerp((float)Math.Sin(progress * ((float)Math.PI / 2f)), 1f, 0.04f);
			distance = Math.Max(distance, 65f);
			float angleDeviation = 2.6179938f;
			float angleOffset = (float)Owner.direction * flipped * MathHelper.Lerp(0f - angleDeviation, 0f, progress);
			if (flipped == -1f)
			{
				distance *= MathHelper.Lerp(0.1f, 0.3f, (float)Math.Sin(progress * (float)Math.PI));
			}
			return base.Projectile.velocity.RotatedBy(angleOffset) * distance;
		}
		float distance2 = MathHelper.Lerp(Reach, 0f, progress);
		return base.Projectile.velocity * distance2;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		if (Timer == 0f)
		{
			return false;
		}
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_GrovetendersTouchBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_GrovetendersTouchGlow", (AssetRequestMode)2).Value;
		Vector2 projBottom = base.Projectile.Center + Utils.RotatedBy(new Vector2((float)(-handle.Width / 2), (float)(handle.Height / 2)), (double)(base.Projectile.rotation + (float)Math.PI / 4f), default(Vector2)) * 0.75f;
		DrawChain(projBottom, out var chainPositions);
		float drawRotation = (projBottom - chainPositions[^2]).ToRotation() + (float)Math.PI / 4f;
		drawRotation += ((SnapCoyoteTime > 0f) ? ((float)Math.PI) : 0f);
		drawRotation += ((base.Projectile.spriteDirection < 0) ? 0f : 0f);
		if (ReelingBack)
		{
			drawRotation = drawRotation.AngleLerp((base.Projectile.Center - Owner.Center).ToRotation(), GetSwingRatio());
		}
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		SpriteEffects flip = (SpriteEffects)((base.Projectile.spriteDirection < 0) ? 0 : 0);
		lightColor = Lighting.GetColor((int)(base.Projectile.Center.X / 16f), (int)(base.Projectile.Center.Y / 16f));
		Vector2 nitpickCorrection = ((flipped == -1f && Timer / 90f < 0.35f) ? (drawRotation.ToRotationVector2() * 16f + (((float)Owner.direction == -1f) ? (Vector2.UnitX * -12f) : Vector2.Zero)) : Vector2.Zero);
		Main.EntitySpriteDraw(handle, projBottom - nitpickCorrection - Main.screenPosition, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		if ((!ReelingBack || SnapCoyoteTime != 0f) && Timer / 90f > 0.35f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
			Main.EntitySpriteDraw(blade, projBottom - nitpickCorrection - Main.screenPosition, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, flip);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}

	private void DrawChain(Vector2 projBottom, out Vector2[] chainPositions)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_GrovetendersTouchChain", (AssetRequestMode)2).Value;
		float ratio = GetSwingRatio();
		if (!ReelingBack)
		{
			controlPoint1 = Owner.MountedCenter + SwingPosition(MathHelper.Clamp(ratio + 0.5f, 0f, 1f)) * 0.2f;
			controlPoint2 = Owner.MountedCenter + SwingPosition(MathHelper.Clamp(ratio + 0.2f, 0f, 1f)) * 0.5f;
		}
		else
		{
			Vector2 perpendicular = SwingPosition(ratio).SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866);
			controlPoint1 = Owner.MountedCenter + SwingPosition(MathHelper.Lerp(ratio, 1f, ratio)) + perpendicular * MathHelper.SmoothStep(0f, 1f, ratio) * 155f * (float)Owner.direction;
			controlPoint2 = Owner.MountedCenter + SwingPosition(MathHelper.Lerp(ratio, 1f, ratio / 2f)) + perpendicular * MathHelper.SmoothStep(0f, 1f, ratio) * -100f * (float)Owner.direction;
		}
		BezierCurve curve = new BezierCurve(Owner.MountedCenter, controlPoint1, controlPoint2, projBottom);
		int numPoints = 30;
		chainPositions = curve.GetPoints(numPoints).ToArray();
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		for (int i = 1; i < numPoints; i++)
		{
			Vector2 position = chainPositions[i];
			float rotation = (chainPositions[i] - chainPositions[i - 1]).ToRotation() - (float)Math.PI / 2f;
			float yScale = Vector2.Distance(chainPositions[i], chainPositions[i - 1]) / (float)chainTex.Height;
			((Vector2)(ref scale))._002Ector(1f, yScale);
			Color chainLightColor = Lighting.GetColor((int)position.X / 16, (int)position.Y / 16);
			if (ReelingBack)
			{
				chainLightColor *= 1f - EaseInFunction(ratio);
			}
			((Vector2)(ref origin))._002Ector((float)(chainTex.Width / 2), (float)chainTex.Height);
			Main.EntitySpriteDraw(chainTex, position - Main.screenPosition, null, chainLightColor, rotation, origin, scale, (SpriteEffects)0);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(initialized);
		writer.Write(Reach);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		initialized = reader.ReadBoolean();
		Reach = reader.ReadSingle();
	}
}
