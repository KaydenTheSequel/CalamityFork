using System;
using System.IO;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

public class SepulcherArm : ModNPC
{
	public class SepulcherArmLimb
	{
		public Vector2 Center;

		public float Rotation;

		public SepulcherArmLimb(Vector2 center, float rotation)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Center = center;
			Rotation = rotation;
		}

		public void SendData(BinaryWriter writer)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			writer.WritePackedVector2(Center);
			writer.Write(Rotation);
		}

		public void ReceiveData(BinaryReader reader)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Center = reader.ReadPackedVector2();
			Rotation = reader.ReadSingle();
		}
	}

	public SepulcherArmLimb[] Limbs = new SepulcherArmLimb[4];

	public static Asset<Texture2D> HandTexture;

	public static Asset<Texture2D> ForearmTexture;

	public NPC SegmentToAttachTo => Main.npc[(int)base.NPC.ai[0]];

	public bool CurrentlyMoving
	{
		get
		{
			return base.NPC.ai[1] == 1f;
		}
		set
		{
			base.NPC.ai[1] = value.ToInt();
		}
	}

	public Vector2 MoveDestination
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(base.NPC.ai[2], base.NPC.ai[3]);
		}
		set
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.ai[2] = value.X;
			base.NPC.ai[3] = value.Y;
		}
	}

	public ref float Time => ref base.NPC.localAI[0];

	public bool ReelingBack
	{
		get
		{
			return base.NPC.localAI[1] == 0f;
		}
		set
		{
			base.NPC.localAI[1] = 1f - (float)value.ToInt();
		}
	}

	public Player Target => Main.player[base.NPC.target];

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.SepulcherHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			HandTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SepulcherHand", (AssetRequestMode)2);
			ForearmTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SepulcherForearm", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 34;
		base.NPC.height = 48;
		base.NPC.defense = 0;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
		base.NPC.lifeMax = (CalamityWorld.revenge ? 345000 : 300000);
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.knockBackResist = 0f;
		base.NPC.scale *= (Main.expertMode ? 1.35f : 1.2f);
		base.NPC.dontTakeDamage = true;
		base.NPC.alpha = 255;
		base.NPC.chaseable = false;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.canGhostHeal = false;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		for (int i = 0; i < Limbs.Length; i++)
		{
			Limbs[i] = new SepulcherArmLimb(base.NPC.Center, 0f);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		for (int i = 0; i < Limbs.Length; i++)
		{
			Limbs[i].SendData(writer);
		}
		writer.Write(base.NPC.rotation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		for (int i = 0; i < Limbs.Length; i++)
		{
			Limbs[i].ReceiveData(reader);
		}
		base.NPC.rotation = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc.IndexInRange((int)base.NPC.ai[0]) || !Main.npc[(int)base.NPC.ai[0]].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			return;
		}
		base.NPC.Opacity = SegmentToAttachTo.Opacity;
		base.NPC.TargetClosest(faceTarget: false);
		Vector2 idealMovePosition = SegmentToAttachTo.Center;
		float sideFactor = MathHelper.Lerp(200f, 18f, Utils.GetLerpValue(-0.51f, -0.06f, base.NPC.rotation, clamped: true));
		float aheadFactor = MathHelper.Lerp(284f, 680f, Utils.GetLerpValue(-0.51f, -0.06f, base.NPC.rotation, clamped: true));
		idealMovePosition += (SegmentToAttachTo.rotation + base.NPC.rotation * (float)base.NPC.direction - (float)Math.PI / 2f).ToRotationVector2() * base.NPC.scale * aheadFactor;
		idealMovePosition += (SegmentToAttachTo.rotation + base.NPC.rotation * (float)base.NPC.direction - (float)Math.PI / 2f + (float)Math.PI / 2f * (float)base.NPC.direction).ToRotationVector2() * base.NPC.scale * sideFactor;
		base.NPC.Center = idealMovePosition;
		UpdateLimbs();
		if (ReelingBack)
		{
			base.NPC.rotation -= 0.066f;
			if (base.NPC.rotation < -0.55f)
			{
				ReelingBack = false;
			}
		}
		else
		{
			base.NPC.rotation += 0.029f;
			if (base.NPC.rotation > 0.77f)
			{
				ReelingBack = true;
			}
		}
		Time++;
	}

	public void MoveToDestination()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Center = Vector2.Lerp(base.NPC.Center, MoveDestination, 0.05f);
		base.NPC.Center = base.NPC.Center.MoveTowards(MoveDestination, 10f);
		if (base.NPC.WithinRange(MoveDestination, 15f))
		{
			base.NPC.Center = MoveDestination;
			MoveDestination = Vector2.Zero;
			CurrentlyMoving = false;
			base.NPC.netUpdate = true;
		}
	}

	public void UpdateLimbs()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offsetFromSegment = Vector2.Zero;
		offsetFromSegment += Utils.RotatedBy(new Vector2((float)base.NPC.direction * 60f, 55f), (double)(SegmentToAttachTo.rotation - ((float)Math.PI / 2f - base.NPC.rotation * 1.7f - 0.77f) * (float)base.NPC.direction), default(Vector2)).SafeNormalize(Vector2.UnitY) * 92f;
		Limbs[0].Center = SegmentToAttachTo.Center + offsetFromSegment;
		Limbs[0].Rotation = offsetFromSegment.ToRotation();
		Limbs[1].Rotation = base.NPC.AngleFrom(Limbs[0].Center);
		Limbs[1].Center = Limbs[0].Center + offsetFromSegment * 0.5f + (base.NPC.Center - Limbs[0].Center).SafeNormalize(Vector2.UnitY) * 84f;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor_Unused)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		Texture2D armTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D foreArmTexture = ForearmTexture.Value;
		Texture2D handTexture = HandTexture.Value;
		Vector2 forearmDrawPosition = Limbs[0].Center - screenPos;
		Color drawColor = Lighting.GetColor((int)(Limbs[0].Center.X / 16f), (int)(Limbs[0].Center.Y / 16f));
		spriteBatch.Draw(foreArmTexture, forearmDrawPosition, (Rectangle?)null, drawColor, Limbs[0].Rotation + (float)Math.PI / 2f, foreArmTexture.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
		Vector2 armDrawPosition = Limbs[1].Center - screenPos;
		drawColor = Lighting.GetColor((int)(Limbs[1].Center.X / 16f), (int)(Limbs[1].Center.Y / 16f));
		spriteBatch.Draw(armTexture, armDrawPosition, (Rectangle?)null, drawColor, Limbs[1].Rotation + (float)Math.PI / 2f, armTexture.Size() * new Vector2(0.5f, 0f), base.NPC.scale, (SpriteEffects)2, 0f);
		Vector2 handDrawPosition = armDrawPosition;
		SpriteEffects handDirection = (SpriteEffects)(base.NPC.direction == -1);
		spriteBatch.Draw(handTexture, handDrawPosition, (Rectangle?)null, drawColor, Limbs[1].Rotation - (float)Math.PI / 2f, handTexture.Size() * new Vector2(0.5f, 0f), base.NPC.scale, handDirection, 0f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			Vector2 forearmGoreSpawnPosition = Limbs[0].Center + Main.rand.NextVector2Circular(6f, 6f);
			Vector2 armGoreSpawnPosition = Limbs[1].Center + Main.rand.NextVector2Circular(6f, 6f);
			Vector2 handGoreSpawnPosition = armGoreSpawnPosition + Main.rand.NextVector2Circular(6f, 6f);
			Gore.NewGorePerfect(base.NPC.GetSource_Death(), armGoreSpawnPosition, Main.rand.NextVector2Circular(3f, 3f), base.Mod.Find<ModGore>("SepulcherArm_Gore").Type, base.NPC.scale);
			for (int i = 1; i <= 2; i++)
			{
				Gore.NewGorePerfect(base.NPC.GetSource_Death(), forearmGoreSpawnPosition, Main.rand.NextVector2Circular(3f, 3f), base.Mod.Find<ModGore>($"SepulcherForearm_Gore{i}").Type, base.NPC.scale);
			}
			for (int j = 1; j <= 2; j++)
			{
				Gore.NewGorePerfect(base.NPC.GetSource_Death(), handGoreSpawnPosition, Main.rand.NextVector2Circular(3f, 3f), base.Mod.Find<ModGore>($"SepulcherHand_Gore{j}").Type, base.NPC.scale);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}
}
