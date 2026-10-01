using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.InverseKinematics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public abstract class ExoskeletonCannon : ModProjectile, ILocalizedModType, IModType
{
	public int ShootTimer;

	public LimbCollection Limbs = new LimbCollection(new CyclicCoordinateDescentUpdateRule(0.27f, (float)Math.PI / 2f), 70f, 82f);

	public static readonly Vector2[] HoverOffsetTable;

	public static readonly float[] RotationalClampTable;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int HoverOffsetIndex => (int)base.Projectile.ai[0];

	public bool TargetingSomething => base.Projectile.ai[1] == 1f;

	public Player Owner => Main.player[base.Projectile.owner];

	public virtual bool UsesSuperpredictiveness => false;

	public virtual Vector2 DrawOffset
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2((float)(OwnerRestingOffset.X > 0f).ToDirectionInt() * 6f, -6f);
		}
	}

	public virtual Vector2 ConnectOffset
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			if (HoverOffsetIndex < 2)
			{
				return Vector2.Zero;
			}
			return new Vector2((float)(OwnerRestingOffset.X > 0f).ToDirectionInt() * 14f, -30f);
		}
	}

	public abstract int ShootRate { get; }

	public abstract float ShootSpeed { get; }

	public abstract Vector2 OwnerRestingOffset { get; }

	public abstract void ClampFirstLimbRotation(ref double limbRotation);

	public abstract void ShootAtTarget(NPC target, Vector2 shootDirection);

	public override void SetDefaults()
	{
		ShootTimer = Main.rand?.Next(ShootRate) ?? 0;
		base.Projectile.width = 94;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 3f;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900000;
		base.Projectile.scale = 1f;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.rotation);
		writer.Write((Limbs?.Limbs?.Length).GetValueOrDefault());
		for (int i = 0; i < Limbs.Limbs.Length; i++)
		{
			writer.Write(Limbs[i].Rotation);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = reader.ReadSingle();
		reader.ReadInt32();
		int limbCount = Limbs.Limbs.Length;
		for (int i = 0; i < limbCount; i++)
		{
			Limbs[i].Rotation = reader.ReadDouble();
			if (i >= 1)
			{
				Limbs[i].ConnectPoint = Limbs[i - 1].EndPoint;
			}
		}
	}

	public override void AI()
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 connectPosition = Main.LocalPlayer.Center + ConnectOffset;
			connectPosition.X += (float)(OwnerRestingOffset.X > 0f).ToDirectionInt() * base.Projectile.scale * 20f;
			Vector2 endPosition = Owner.Center + OwnerRestingOffset;
			endPosition += (Main.MouseWorld - endPosition) * 0.075f;
			ClampFirstLimbRotation(ref Limbs[0].Rotation);
			Limbs.Update(connectPosition, endPosition);
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.ai[1] = 0f;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.Center = Limbs.EndPoint;
		Owner.AddBuff(ModContent.BuffType<ExoskeletonCannons>(), 3600);
		if (Owner.dead)
		{
			Owner.Calamity().AresCannons = false;
		}
		if (Owner.Calamity().AresCannons)
		{
			base.Projectile.timeLeft = 2;
		}
		float idealRotation = ((Main.myPlayer != base.Projectile.owner) ? base.Projectile.rotation : base.Projectile.AngleTo(Main.MouseWorld));
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1020f);
		if (potentialTarget != null)
		{
			base.Projectile.ai[1] = 1f;
			idealRotation = base.Projectile.AngleTo(potentialTarget.Center);
			if (UsesSuperpredictiveness)
			{
				idealRotation = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, potentialTarget, ShootSpeed).ToRotation();
			}
			ShootTimer++;
			if (ShootTimer >= ShootRate)
			{
				ShootAtTarget(potentialTarget, idealRotation.ToRotationVector2());
				ShootTimer = 0;
			}
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealRotation, 0.15f);
		base.Projectile.spriteDirection = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt();
	}

	public void DefaultDrawCannon(Texture2D glowmask)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(2, Main.projFrames[base.Type], TargetingSomething.ToInt(), base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		float rotation = base.Projectile.rotation;
		if (base.Projectile.spriteDirection == -1)
		{
			rotation += (float)Math.PI;
		}
		DrawLimbs();
		Color lightColor = Lighting.GetColor(base.Projectile.Center.ToTileCoordinates());
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, base.Projectile.GetAlpha(Color.White), rotation, origin, base.Projectile.scale, direction);
	}

	public void DrawLimbs()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		int frame = (int)(Main.GlobalTimeWrappedHourly * 8.1f) % 9;
		Vector2 segmentOriginFactor = default(Vector2);
		for (int i = 0; i < Limbs.Limbs.Length; i++)
		{
			float scale = base.Projectile.scale;
			float rotation = (float)Limbs[i].Rotation;
			((Vector2)(ref segmentOriginFactor))._002Ector(0f, 0.5f);
			SpriteEffects segmentDirection = (SpriteEffects)1;
			Texture2D segmentTexture;
			Texture2D glowmaskTexture;
			if (i == 0)
			{
				segmentTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ArmPart1", (AssetRequestMode)2).Value;
				glowmaskTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ArmPart1Glowmask", (AssetRequestMode)2).Value;
			}
			else
			{
				segmentTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ArmPart2", (AssetRequestMode)2).Value;
				glowmaskTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ArmPart2Glowmask", (AssetRequestMode)2).Value;
			}
			Rectangle segmentFrame = segmentTexture.Frame(1, 9, 0, frame);
			if (i <= 1)
			{
				segmentDirection = (SpriteEffects)(OwnerRestingOffset.X < 0f);
				if (OwnerRestingOffset.X < 0f)
				{
					segmentOriginFactor.X = 1f;
					rotation += (float)Math.PI;
				}
			}
			else
			{
				scale *= 0.67f;
			}
			Color segmentColor = Lighting.GetColor(Limbs[i].ConnectPoint.ToTileCoordinates());
			Vector2 segmentDrawPosition = Limbs[i].ConnectPoint - Main.screenPosition;
			Main.spriteBatch.DrawLineBetter(Limbs[i].ConnectPoint, Limbs[i].EndPoint, Color.Cyan, 3f);
			Main.EntitySpriteDraw(segmentTexture, segmentDrawPosition, segmentFrame, base.Projectile.GetAlpha(segmentColor), rotation, segmentFrame.Size() * segmentOriginFactor, scale, segmentDirection);
			if (glowmaskTexture != null)
			{
				Main.EntitySpriteDraw(glowmaskTexture, segmentDrawPosition, segmentFrame, base.Projectile.GetAlpha(Color.White), rotation, segmentFrame.Size() * segmentOriginFactor, scale, segmentDirection);
			}
		}
		Vector2 shoulderPosition = Limbs.ConnectPoint + DrawOffset * base.Projectile.scale;
		Color shoulderColor = Lighting.GetColor(Limbs.ConnectPoint.ToTileCoordinates());
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ArmTopShoulder", (AssetRequestMode)2).Value;
		SpriteEffects shoulderDirection = (SpriteEffects)(OwnerRestingOffset.X < 0f);
		Rectangle shoulderFrame = value.Frame(1, 9, 0, frame);
		Main.EntitySpriteDraw(value, shoulderPosition - Main.screenPosition, shoulderFrame, base.Projectile.GetAlpha(shoulderColor), 0f, shoulderFrame.Size() * 0.5f, base.Projectile.scale, shoulderDirection);
	}

	static ExoskeletonCannon()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		HoverOffsetTable = (Vector2[])(object)new Vector2[4]
		{
			new Vector2(300f, 96f),
			new Vector2(-300f, 96f),
			new Vector2(190f, -102f),
			new Vector2(-190f, -102f)
		};
		RotationalClampTable = new float[4] { 0.23f, 2.9115927f, 0.2f, 2.9415927f };
	}
}
