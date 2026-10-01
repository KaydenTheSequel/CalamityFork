using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge;

[LongDistanceNetSync(SyncWith = typeof(DesertNuisanceHead))]
public class DesertNuisanceBody : ModNPC
{
	public static Asset<Texture2D> BodyTexture2;

	public static Asset<Texture2D> BodyTexture3;

	public static Asset<Texture2D> BodyTexture4;

	private const int ClosedFinFrame = 5;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.DesertNuisanceHead.DisplayName");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			BodyTexture2 = ModContent.Request<Texture2D>(Texture + "2", (AssetRequestMode)2);
			BodyTexture3 = ModContent.Request<Texture2D>(Texture + "3", (AssetRequestMode)2);
			BodyTexture4 = ModContent.Request<Texture2D>(Texture + "4", (AssetRequestMode)2);
		}
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 12;
		base.NPC.width = 88;
		base.NPC.height = 88;
		base.NPC.defense = 5;
		if (Main.getGoodWorld)
		{
			base.NPC.defense += 27;
		}
		base.NPC.LifeMaxNERB(1500, 1800, 40000);
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (CalamityWorld.death)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (base.NPC.ai[3] > 0f)
		{
			switch ((int)base.NPC.ai[3])
			{
			case 10:
			{
				base.NPC.ai[3] = 1f;
				base.NPC.position = base.NPC.Center;
				NPC nPC3 = base.NPC;
				nPC3.position -= base.NPC.Size * 0.5f;
				base.NPC.frame = new Rectangle(0, 0, (BodyTexture2 != null) ? BodyTexture2.Width() : 0, (BodyTexture2 != null) ? BodyTexture2.Height() : 0);
				base.NPC.ForceNetUpdate();
				break;
			}
			case 20:
			{
				base.NPC.ai[3] = 2f;
				base.NPC.position = base.NPC.Center;
				NPC nPC2 = base.NPC;
				nPC2.position -= base.NPC.Size * 0.5f;
				base.NPC.frame = new Rectangle(0, 0, (BodyTexture3 != null) ? BodyTexture3.Width() : 0, (BodyTexture3 != null) ? BodyTexture3.Height() : 0);
				base.NPC.ForceNetUpdate();
				break;
			}
			case 30:
			{
				base.NPC.ai[3] = 3f;
				base.NPC.position = base.NPC.Center;
				NPC nPC = base.NPC;
				nPC.position -= base.NPC.Size * 0.5f;
				base.NPC.frame = new Rectangle(0, 0, (BodyTexture4 != null) ? BodyTexture4.Width() : 0, (BodyTexture4 != null) ? BodyTexture4.Height() : 0);
				base.NPC.ForceNetUpdate();
				break;
			}
			}
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		_ = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<DesertNuisanceHead>());
		if (!shouldDespawn)
		{
			if (base.NPC.ai[1] <= 0f)
			{
				shouldDespawn = true;
			}
			else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
			{
				shouldDespawn = true;
			}
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
		}
		NPC aheadSegment = Main.npc[(int)base.NPC.ai[1]];
		Vector2 directionToNextSegment = aheadSegment.Center - base.NPC.Center;
		if (aheadSegment.rotation != base.NPC.rotation)
		{
			directionToNextSegment = directionToNextSegment.RotatedBy(MathHelper.WrapAngle(aheadSegment.rotation - base.NPC.rotation) * 0.08f);
			directionToNextSegment = directionToNextSegment.MoveTowards((aheadSegment.rotation - base.NPC.rotation).ToRotationVector2(), 1f);
		}
		int segmentOffset = 62;
		base.NPC.rotation = directionToNextSegment.ToRotation() + (float)Math.PI / 2f;
		base.NPC.Center = aheadSegment.Center - directionToNextSegment.SafeNormalize(Vector2.Zero) * base.NPC.scale * (float)segmentOffset;
		base.NPC.spriteDirection = (directionToNextSegment.X > 0f).ToDirectionInt();
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		float hitDistance = 30f;
		switch ((int)base.NPC.ai[3])
		{
		default:
			hitDistance = 40f;
			break;
		case 2:
		case 20:
			hitDistance = 40f;
			break;
		case 3:
		case 30:
			hitDistance = 30f;
			break;
		}
		return minDist <= hitDistance * base.NPC.scale;
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[3] != 0f)
		{
			return;
		}
		Tile tileSafely = Framing.GetTileSafely(Main.npc[(int)base.NPC.ai[2]].Center.ToTileCoordinates());
		if (tileSafely.HasUnactuatedTile || tileSafely.LiquidAmount > 0)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 10.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = frameHeight * 5;
			}
		}
		else if (base.NPC.frame.Y > 0)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 10.0)
			{
				base.NPC.frame.Y += frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			switch ((int)base.NPC.ai[3])
			{
			default:
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisanceBody").Type, base.NPC.scale);
				break;
			case 1:
			case 10:
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisanceBody2").Type, base.NPC.scale);
				break;
			case 2:
			case 20:
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisanceBody3").Type, base.NPC.scale);
				break;
			case 3:
			case 30:
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeNuisanceBody4").Type, base.NPC.scale);
				break;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.7f * balance);
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			return Color.Orange * (float)(int)((Color)(ref drawColor)).A * base.NPC.Opacity;
		}
		return null;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[3] > 0f)
		{
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Texture2D texture = null;
			switch ((int)base.NPC.ai[3])
			{
			default:
				texture = BodyTexture2.Value;
				break;
			case 2:
			case 20:
				texture = BodyTexture3.Value;
				break;
			case 3:
			case 30:
				texture = BodyTexture4.Value;
				break;
			}
			Vector2 halfSizeTexture = default(Vector2);
			((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
			Vector2 drawLocation = base.NPC.Center - screenPos;
			drawLocation -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
			drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
			spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			return false;
		}
		return true;
	}
}
