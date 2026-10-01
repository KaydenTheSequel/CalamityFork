using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HydraHead : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 CurrentPositionOffset;

	public Vector2 IdealPositionOffset;

	public Vector2[] OldVelocities = (Vector2[])(object)new Vector2[20];

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Hydra";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public ref float AttackType => ref base.Projectile.ai[1];

	public Vector2 DrawStartPosition
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			if (base.Projectile.owner < 0 || base.Projectile.owner >= Main.player.Length)
			{
				return Vector2.Zero;
			}
			return Main.player[base.Projectile.owner].Top + Vector2.UnitY * 8f;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.WritePackedVector2(CurrentPositionOffset);
		writer.WritePackedVector2(IdealPositionOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CurrentPositionOffset = reader.ReadPackedVector2();
		IdealPositionOffset = reader.ReadPackedVector2();
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.dead || !Owner.active || Owner.HeldItem.type != ModContent.ItemType<Hydra>())
		{
			base.Projectile.Kill();
		}
		else
		{
			base.Projectile.timeLeft = 2;
		}
		Vector2 returnPosition = DrawStartPosition + CurrentPositionOffset;
		if (base.Projectile.localAI[0] == 0f)
		{
			PerformInitializationEffects();
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 aimDestination = Owner.Calamity().mouseWorld;
		float idealRotation = base.Projectile.AngleTo(aimDestination);
		if (Time < 0f)
		{
			base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, idealRotation, (Time + 15f) / 15f);
		}
		else
		{
			base.Projectile.rotation = idealRotation;
		}
		base.Projectile.Center = new Vector2(base.Projectile.Center.X, MathHelper.Clamp(base.Projectile.Center.Y, 1f, returnPosition.Y - 8f));
		MoveTowardsDestination(returnPosition);
		AdjustOldVelocityArray();
		PerformAttacks(aimDestination);
	}

	public void PerformInitializationEffects()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		int totalHeads = Owner.ownedProjectileCounts[base.Type];
		CurrentPositionOffset = (IdealPositionOffset = new Vector2(Main.rand.NextFloat(-72f - 8f * (float)totalHeads, 72f + 8f * (float)totalHeads), 0f - Main.rand.NextFloat(8f, 84f + 4f * (float)totalHeads)));
		base.Projectile.netUpdate = true;
	}

	public void MoveTowardsDestination(Vector2 returnPosition)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromTarget = base.Projectile.Distance(returnPosition);
		if (distanceFromTarget > 7f)
		{
			float flySpeed = MathHelper.Lerp(2f, 17f, Utils.GetLerpValue(10f, 70f, distanceFromTarget, clamped: true));
			base.Projectile.velocity = (base.Projectile.velocity * 9f + base.Projectile.SafeDirectionTo(returnPosition) * flySpeed) / 10f;
		}
		int totalHeads = Owner.ownedProjectileCounts[base.Type];
		int moveRate = 40 + totalHeads * 4;
		Time++;
		if (Time % (float)moveRate == (float)moveRate - 1f)
		{
			IdealPositionOffset = new Vector2(Main.rand.NextFloat(-72f - 8f * (float)totalHeads, 72f + 8f * (float)totalHeads), 0f - Main.rand.NextFloat(8f, 84f + 4f * (float)totalHeads));
		}
		if (Vector2.Distance(IdealPositionOffset, CurrentPositionOffset) > 0.2f)
		{
			CurrentPositionOffset = Vector2.Lerp(CurrentPositionOffset, IdealPositionOffset, 0.125f);
		}
		if (base.Projectile.Center.Y > Owner.Top.Y - 46f)
		{
			base.Projectile.Center = new Vector2(base.Projectile.Center.X, Owner.Top.Y - 46f);
		}
		if (base.Projectile.Distance(returnPosition) > 120f)
		{
			base.Projectile.Center = returnPosition + base.Projectile.DirectionFrom(returnPosition) * 120f;
		}
		base.Projectile.MinionAntiClump(0.15f);
	}

	public void AdjustOldVelocityArray()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		for (int i = OldVelocities.Length - 1; i > 0; i--)
		{
			OldVelocities[i] = OldVelocities[i - 1];
		}
		OldVelocities[0] = base.Projectile.velocity;
	}

	public void PerformAttacks(Vector2 aimDestination)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		Item heldItem = Owner.HeldItem;
		Vector2 shootDirection = base.Projectile.SafeDirectionTo(aimDestination);
		int usedAmmoItemId;
		int projToShoot;
		if (AttackType > 0f)
		{
			Owner.PickAmmo(heldItem, out usedAmmoItemId, out var itemVelocity, out var itemDamage, out var itemKB, out projToShoot);
			int type = ModContent.ProjectileType<HydrasBlood>();
			for (int i = 0; i < 2; i++)
			{
				Vector2 spreadDirection = shootDirection.RotatedByRandom(MathHelper.ToRadians(5f));
				float spreadVelocity = itemVelocity * Main.rand.NextFloat(1f, 1.4f);
				Vector2 shootPos = base.Projectile.Center + shootDirection * 24f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPos, spreadVelocity * spreadDirection, type, itemDamage, itemKB, base.Projectile.owner);
			}
			base.Projectile.rotation -= MathHelper.ToRadians(25f) * (float)Math.Sign(base.Projectile.rotation);
			Time = -15f;
			Projectile projectile = base.Projectile;
			projectile.velocity += shootDirection * -10f;
			AttackType = 0f;
		}
		else if (AttackType < 0f)
		{
			Owner.PickAmmo(heldItem, out projToShoot, out var itemVelocity2, out var itemDamage2, out var itemKB2, out usedAmmoItemId);
			int gunType = ModContent.ProjectileType<HydraHeadLaunch>();
			int gunDamage = itemDamage2 * 5;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, itemVelocity2 * shootDirection, gunType, gunDamage, itemKB2, base.Projectile.owner);
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDustPerfect(base.Projectile.Center, 171, Main.rand.NextVector2CircularEdge(4f, 4f)).noGravity = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/HydraHeadChain", (AssetRequestMode)2).Value;
		Vector2 end = base.Projectile.Center + (float)((base.Projectile.spriteDirection == 1).ToInt() * 10) * Vector2.UnitX;
		List<Vector2> controlPoints = new List<Vector2> { DrawStartPosition };
		for (int i = 0; i < OldVelocities.Length; i++)
		{
			float swayResponsiveness = Utils.GetLerpValue(0f, 6f, i, clamped: true) * Utils.GetLerpValue(OldVelocities.Length, (float)OldVelocities.Length - 6f, i, clamped: true);
			Vector2 swayTotalOffset = OldVelocities[i] * swayResponsiveness;
			controlPoints.Add(Vector2.Lerp(DrawStartPosition, end, (float)i / (float)OldVelocities.Length) + swayTotalOffset);
		}
		controlPoints.Add(end);
		int chainPointCount = (int)(Vector2.Distance(controlPoints.First(), controlPoints.Last()) / 5f);
		if (chainPointCount < 12)
		{
			chainPointCount = 12;
		}
		List<Vector2> chainPoints = new BezierCurve(controlPoints.ToArray()).GetPoints(chainPointCount);
		for (int j = 0; j < chainPoints.Count; j++)
		{
			Vector2 positionAtPoint = chainPoints[j];
			if (!(Vector2.Distance(positionAtPoint, base.Projectile.Center) < 10f))
			{
				float angleAtPoint = ((j == chainPoints.Count - 1) ? (end - chainPoints[j]).ToRotation() : (chainPoints[j + 1] - chainPoints[j]).ToRotation());
				angleAtPoint += (float)Math.PI / 2f;
				Main.EntitySpriteDraw(chain, positionAtPoint - Main.screenPosition, null, Color.Lerp(Color.White, Color.Transparent, 0.6f), angleAtPoint, chain.Size() / 2f, 1f, (SpriteEffects)0);
			}
		}
		bool shouldFlip = Math.Abs(base.Projectile.rotation) > (float)Math.PI / 2f;
		Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, null, lightColor, base.Projectile.rotation, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)(shouldFlip ? 2 : 0));
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
