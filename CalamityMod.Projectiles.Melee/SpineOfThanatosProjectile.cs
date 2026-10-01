using System;
using System.Collections.Generic;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SpineOfThanatosProjectile : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> WhipPoints = new List<Vector2>();

	public const int Lifetime = 125;

	public const int FlyBackTime = 40;

	public const int FinalWhipRayShootRate = 10;

	public const int LaserRayCount = 12;

	public const float MaximumBendFactor = 42f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public float CurrentBendFactor => 42f * CalamityUtils.Convert01To010(Time / 125f);

	public Vector2 WhipEnd
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + WhipOutwardness;
		}
	}

	public ref Vector2 WhipOutwardness => ref base.Projectile.velocity;

	public ref float Time => ref base.Projectile.ai[0];

	public ref float SwingDirection => ref base.Projectile.ai[1];

	public ref float InitialDirectionRotation => ref base.Projectile.localAI[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 70;
		base.Projectile.scale = 0.75f;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 125;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 22;
	}

	public void DetermineWhipPoints()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		Vector2 startingPosition = Owner.RotatedRelativePoint(Owner.MountedCenter);
		List<Vector2> initialPoints = new List<Vector2> { startingPosition };
		for (int i = 0; i < 12; i++)
		{
			Vector2 bendOffset = Vector2.UnitX * (0f - SwingDirection);
			bendOffset *= CurrentBendFactor * CalamityUtils.Convert01To010((float)i / 12f);
			bendOffset *= Utils.GetLerpValue(0f, 300f, Owner.Distance(Vector2.Lerp(startingPosition, base.Projectile.Center, (float)i / 12f) + bendOffset), clamped: true);
			initialPoints.Add(Vector2.Lerp(startingPosition, base.Projectile.Center, (float)i / 12f) + bendOffset);
		}
		initialPoints.Add(base.Projectile.Center);
		BezierCurve bezierCurve = new BezierCurve(initialPoints.ToArray());
		int totalChains = (int)(base.Projectile.Distance(startingPosition) / 24f / base.Projectile.scale);
		totalChains = (int)MathHelper.Clamp((float)totalChains, 40f, 440f);
		WhipPoints = bezierCurve.GetPoints(totalChains);
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		DetermineWhipPoints();
		Vector2 playerRotatedPosition = Owner.RotatedRelativePoint(Owner.MountedCenter);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.CantUseHoldout())
			{
				HandleChannelMovement(playerRotatedPosition);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		ManipulatePlayerValues();
		if (SwingDirection == 0f && base.Projectile.timeLeft == 40)
		{
			CreateBadassPrismExplosion();
		}
		Time++;
	}

	public void HandleChannelMovement(Vector2 playerRotatedPosition)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		if (InitialDirectionRotation == 0f)
		{
			InitialDirectionRotation = WhipOutwardness.ToRotation() - (float)Math.PI / 2f;
		}
		float attackCompletionRatio = Utils.GetLerpValue(125f, 40f, base.Projectile.timeLeft, clamped: true);
		float baseSwingAngle = MathHelper.Lerp(-1.1f, (float)Math.PI / 2f, 1f - attackCompletionRatio);
		Vector2 swingDirection = (SwingDirection * baseSwingAngle + (float)Math.PI / 2f).ToRotationVector2();
		swingDirection = swingDirection.RotatedBy(InitialDirectionRotation);
		if (base.Projectile.timeLeft < 40)
		{
			WhipOutwardness = Vector2.Lerp(WhipOutwardness, InitialDirectionRotation.ToRotationVector2(), 0.1f);
		}
		else
		{
			Vector2 swingSpeedIncrement = swingDirection * MathHelper.SmoothStep(3.8f, 13f, (float)Math.Pow(CalamityUtils.Convert01To010(Time / 125f), 8.0));
			if (SwingDirection == 0f)
			{
				swingSpeedIncrement *= 0.84f;
			}
			ref Vector2 whipOutwardness = ref WhipOutwardness;
			whipOutwardness += swingSpeedIncrement;
		}
		base.Projectile.Center = playerRotatedPosition;
		base.Projectile.rotation = WhipOutwardness.ToRotation();
	}

	public void ManipulatePlayerValues()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		if (SwingDirection == 0f)
		{
			Owner.itemRotation = WhipOutwardness.ToRotation() * (float)base.Projectile.direction;
			Owner.ChangeDir(base.Projectile.direction);
		}
	}

	public void CreateBadassPrismExplosion()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.DD2_DarkMageHealImpact with
		{
			Pitch = SoundID.DD2_DarkMageHealImpact.Pitch + 0.15f
		};
		SoundEngine.PlaySound(in style, Owner.Center);
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), WhipEnd, Vector2.Zero, ModContent.ProjectileType<ThanatosBoom>(), base.Projectile.damage * 2, 0f, base.Projectile.owner);
		int rayDamage = (int)((double)base.Projectile.damage * 1.5);
		NPC potentialTarget = WhipEnd.ClosestNPCAt(700f);
		for (int i = 0; i < 12; i++)
		{
			float rayRotation = base.Projectile.rotation + MathHelper.Lerp(-0.57f, 0.57f, (float)i / 12f);
			float targetAimDisparity = 0f;
			if (potentialTarget != null)
			{
				targetAimDisparity = base.Projectile.rotation.ToRotationVector2().AngleBetween((potentialTarget.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero));
			}
			Vector2 prismEndPosition = WhipEnd + rayRotation.ToRotationVector2() * 420f;
			if (potentialTarget != null && targetAimDisparity < (float)Math.PI * 27f / 100f)
			{
				prismEndPosition = potentialTarget.Center + potentialTarget.velocity * 4f;
			}
			int prismRay = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), prismEndPosition, Vector2.Zero, ModContent.ProjectileType<PrismRay>(), rayDamage, base.Projectile.knockBack * 0.2f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(prismRay))
			{
				Main.projectile[prismRay].ModProjectile<PrismRay>().RayHue = (float)i / 12f;
				Main.projectile[prismRay].ModProjectile<PrismRay>().StartingPosition = WhipEnd;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < WhipPoints.Count - 1; i++)
		{
			string whipTexturePath = ((i != WhipPoints.Count - 2) ? ((i != 0) ? $"CalamityMod/Projectiles/Melee/SpineOfThanatosBody{i % 2 + 1}" : "CalamityMod/Projectiles/Melee/SpineOfThanatosTail") : Texture);
			Texture2D whipSegmentTexture = ModContent.Request<Texture2D>(whipTexturePath, (AssetRequestMode)2).Value;
			Texture2D value = ModContent.Request<Texture2D>(whipTexturePath + "Glowmask", (AssetRequestMode)2).Value;
			Vector2 origin = whipSegmentTexture.Size() * 0.5f;
			float rotation = (WhipPoints[i + 1] - WhipPoints[i]).ToRotation() + (float)Math.PI / 2f;
			Vector2 drawPosition = WhipPoints[i] - Main.screenPosition;
			Color color = base.Projectile.GetAlpha(Lighting.GetColor((int)WhipPoints[i].X / 16, (int)WhipPoints[i].Y / 16));
			Main.EntitySpriteDraw(whipSegmentTexture, drawPosition, null, color, rotation, origin, base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPosition, null, Color.White, rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (WhipPoints.Count <= 1)
		{
			return false;
		}
		float width = base.Projectile.scale * 38f;
		for (int i = 0; i < WhipPoints.Count - 1; i++)
		{
			float _ = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), WhipPoints[i], WhipPoints[i + 1], width, ref _))
			{
				return true;
			}
		}
		return false;
	}
}
