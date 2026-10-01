using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoHydraHead : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 DeltaPosition;

	public Vector2 DeltaPositionMoving;

	public Vector2[] OldVelocities = (Vector2[])(object)new Vector2[20];

	public new string LocalizationCategory => "Projectiles.Summon";

	public int BodyUUIDIndex => Projectile.GetByUUID(base.Projectile.owner, base.Projectile.ai[0]);

	public float Time
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		DeltaPosition = reader.ReadVector2();
		DeltaPositionMoving = reader.ReadVector2();
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(DeltaPosition);
		writer.WriteVector2(DeltaPositionMoving);
	}

	public override void AI()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (BodyUUIDIndex < 0 || BodyUUIDIndex >= Main.projectile.Length)
		{
			base.Projectile.Kill();
			return;
		}
		Projectile body = Main.projectile[BodyUUIDIndex];
		if (!body.active)
		{
			base.Projectile.Kill();
			return;
		}
		int totalHeads = player.ownedProjectileCounts[base.Projectile.type];
		if (base.Projectile.localAI[0] == 0f)
		{
			DeltaPosition = (DeltaPositionMoving = new Vector2(Main.rand.NextFloat(-72f - 8f * (float)totalHeads, 72f + 8f * (float)totalHeads), 0f - Main.rand.NextFloat(8f, 84f + 4f * (float)totalHeads)));
			base.Projectile.netUpdate = true;
			if (!Main.dedServ)
			{
				for (int i = 0; i < 18; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 113);
					dust.velocity = Utils.RotatedBy(new Vector2(0f, -5f), (double)((float)i / 18f * ((float)Math.PI * 2f)), default(Vector2));
					dust.noGravity = true;
					dust.scale = 1.2f;
				}
			}
			base.Projectile.localAI[0] = 1f;
		}
		Time++;
		if (Time % (60f + (float)totalHeads * 6f) == 59f + (float)totalHeads * 6f)
		{
			DeltaPosition = new Vector2(Main.rand.NextFloat(-72f - 8f * (float)totalHeads, 72f + 8f * (float)totalHeads), 0f - Main.rand.NextFloat(8f, 84f + 4f * (float)totalHeads));
		}
		if (Vector2.Distance(DeltaPosition, DeltaPositionMoving) > 0.2f)
		{
			DeltaPositionMoving = Vector2.Lerp(DeltaPositionMoving, DeltaPosition, 0.125f);
		}
		Vector2 returnPosition = body.Center + new Vector2((float)((body.spriteDirection == 1) ? 12 : (-14)), -50f) + DeltaPositionMoving;
		if (body.ai[0] >= 0f && body.ai[0] < (float)Main.maxNPCs)
		{
			NPC target = Main.npc[(int)body.ai[0]];
			if (target.active && base.Projectile.Distance(target.Center) < 2800f && target.CanBeChasedBy() && Main.myPlayer == base.Projectile.owner)
			{
				if (Time % 40f == 24f)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(target.Center) * 6f, ModContent.ProjectileType<EndoHydraRay>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				if (Time % 40f >= 33f)
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 1;
				}
				else if (Time % 40f >= 27f)
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 2;
				}
				else if (Time % 40f >= 22f)
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 3;
				}
				else if (Time % 40f >= 17f)
				{
					base.Projectile.frame = Main.projFrames[base.Type] - 4;
				}
				base.Projectile.direction = (base.Projectile.spriteDirection = (player.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt());
				if (Math.Abs(player.Center.X - base.Projectile.Center.X) < 80f)
				{
					base.Projectile.direction = (base.Projectile.spriteDirection = player.direction);
				}
				returnPosition.X -= 48f * (float)(target.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt();
			}
			else
			{
				base.Projectile.direction = (base.Projectile.spriteDirection = (player.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt());
				if (Math.Abs(player.Center.X - base.Projectile.Center.X) < 80f)
				{
					base.Projectile.direction = (base.Projectile.spriteDirection = player.direction);
				}
				base.Projectile.frame = 0;
			}
		}
		else
		{
			base.Projectile.direction = (base.Projectile.spriteDirection = player.direction);
			base.Projectile.frame = 0;
		}
		float distanceFromTarget = base.Projectile.Distance(returnPosition);
		if (distanceFromTarget > 7f)
		{
			float speed = MathHelper.Lerp(2f, 17f, Utils.GetLerpValue(10f, 70f, distanceFromTarget, clamped: true));
			base.Projectile.velocity = (base.Projectile.velocity * 9f + base.Projectile.SafeDirectionTo(returnPosition) * speed) / 10f;
		}
		if (base.Projectile.Center.Y > body.Center.Y - 50f)
		{
			base.Projectile.Center = new Vector2(base.Projectile.Center.X, body.Center.Y - 50f);
		}
		if (base.Projectile.Distance(returnPosition) > 120f)
		{
			base.Projectile.Center = returnPosition + base.Projectile.DirectionFrom(returnPosition) * 120f;
		}
		base.Projectile.MinionAntiClump(0.15f);
		AdjustOldVelocityArray();
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		if (BodyUUIDIndex < 0 || BodyUUIDIndex >= Main.projectile.Length)
		{
			return false;
		}
		Projectile body = Main.projectile[BodyUUIDIndex];
		if (!body.active)
		{
			return false;
		}
		Texture2D chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/EndoHydraChain", (AssetRequestMode)2).Value;
		Vector2 start = body.Center + new Vector2((float)((body.spriteDirection == 1) ? 12 : (-14)), -30f);
		Vector2 end = base.Projectile.Center + (float)((base.Projectile.spriteDirection == 1).ToInt() * 10) * Vector2.UnitX;
		List<Vector2> controlPoints = new List<Vector2> { start };
		for (int i = 0; i < OldVelocities.Length; i++)
		{
			float swayResponsiveness = Utils.GetLerpValue(0f, 6f, i, clamped: true) * Utils.GetLerpValue(OldVelocities.Length, (float)OldVelocities.Length - 6f, i, clamped: true);
			swayResponsiveness *= 2.5f;
			Vector2 swayTotalOffset = OldVelocities[i] * swayResponsiveness;
			controlPoints.Add(Vector2.Lerp(start, end, (float)i / (float)OldVelocities.Length) + swayTotalOffset);
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
