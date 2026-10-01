using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GammaHead : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 CurrentPositionOffset;

	public Vector2 IdealPositionOffset;

	public Vector2[] OldVelocities = (Vector2[])(object)new Vector2[20];

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

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

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 36;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		Vector2 returnPosition = DrawStartPosition + CurrentPositionOffset;
		if (base.Projectile.localAI[0] == 0f)
		{
			PerformInitializationEffects();
			base.Projectile.localAI[0] = 1f;
		}
		ApplyMinionBuffs();
		base.Projectile.direction = (base.Projectile.spriteDirection = Owner.direction);
		float idealRotation = 0f;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(3000f, Owner);
		if (potentialTarget != null)
		{
			NoticeTarget(potentialTarget, ref returnPosition, ref idealRotation);
		}
		else
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealRotation, 0.15f);
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(idealRotation, 0.15f);
		base.Projectile.Center = new Vector2(base.Projectile.Center.X, MathHelper.Clamp(base.Projectile.Center.Y, 1f, returnPosition.Y - 8f));
		MoveTowardsDestination(returnPosition);
		AdjustOldVelocityArray();
		Time++;
	}

	public void PerformInitializationEffects()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		int totalHeads = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Projectile.type];
		CurrentPositionOffset = (IdealPositionOffset = new Vector2(Main.rand.NextFloat(-72f - 8f * (float)totalHeads, 72f + 8f * (float)totalHeads), 0f - Main.rand.NextFloat(8f, 84f + 4f * (float)totalHeads)));
		base.Projectile.netUpdate = true;
		if (!Main.dedServ)
		{
			for (int i = 0; i < 18; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 75);
				dust.velocity = Utils.RotatedBy(new Vector2(0f, -5f), (double)((float)i / 18f * ((float)Math.PI * 2f)), default(Vector2));
				dust.noGravity = true;
				dust.scale = 1.35f;
			}
		}
	}

	public void ApplyMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<GammaHydraBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<GammaHead>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().gammaHead = false;
			}
			if (Owner.Calamity().gammaHead)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void NoticeTarget(NPC target, ref Vector2 returnPosition, ref float idealRotation)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		returnPosition -= (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 16f;
		int canisterShootRate = 100;
		if (Time % (float)canisterShootRate > (float)(canisterShootRate - 20))
		{
			base.Projectile.frame = (int)((float)Main.projFrames[base.Type] * Utils.GetLerpValue(canisterShootRate - 20, canisterShootRate - 4, Time % (float)canisterShootRate, clamped: true));
		}
		base.Projectile.frame %= Main.projFrames[base.Type];
		Vector2 spawnPosition = base.Projectile.Center;
		float shootSpeed = MathHelper.Lerp(8f, 29f, Utils.GetLerpValue(90f, 850f, target.Distance(spawnPosition), clamped: true));
		Vector2 shootVelocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(spawnPosition, target.Center, 0.2f, shootSpeed, base.Projectile.SafeDirectionTo(target.Center));
		float fireAngle = shootVelocity.ToRotation();
		if (target.Center.X - spawnPosition.X < 0f)
		{
			fireAngle += (float)Math.PI;
		}
		if (base.Projectile.direction == (target.Center.X < spawnPosition.X).ToDirectionInt())
		{
			fireAngle += (float)Math.PI;
		}
		idealRotation = fireAngle;
		if (Main.myPlayer == base.Projectile.owner && Time % (float)canisterShootRate == (float)(canisterShootRate - 1))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, ModContent.ProjectileType<GammaCanister>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
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
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromTarget = base.Projectile.Distance(returnPosition);
		if (distanceFromTarget > 7f)
		{
			float flySpeed = MathHelper.Lerp(2f, 17f, Utils.GetLerpValue(10f, 70f, distanceFromTarget, clamped: true));
			base.Projectile.velocity = (base.Projectile.velocity * 9f + base.Projectile.SafeDirectionTo(returnPosition) * flySpeed) / 10f;
		}
		int totalHeads = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Projectile.type];
		int moveRate = 40 + totalHeads * 4;
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

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDustPerfect(base.Projectile.Center, 75, Main.rand.NextVector2CircularEdge(4f, 4f)).noGravity = true;
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
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
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
		Texture2D chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/GammaHeadChain", (AssetRequestMode)2).Value;
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
		Texture2D headTexture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(headTexture, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, headTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), lightColor, base.Projectile.rotation, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		return false;
	}
}
