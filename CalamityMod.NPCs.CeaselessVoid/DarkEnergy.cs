using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.CeaselessVoid;

[HasPierceResist(false)]
public class DarkEnergy : ModNPC
{
	private bool start = true;

	private const double minDistance = 10.0;

	private double distance = 10.0;

	private const double minMaxDistance = 800.0;

	public const int MaxHP = 12000;

	public const int MaxBossRushHP = 20000;

	public const int HitboxSize = 64;

	public const int FrameCount = 8;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 120;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.dontTakeDamage = true;
		base.NPC.width = (base.NPC.height = 64);
		base.NPC.defense = 50;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 20000 : 12000);
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit53;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<CeaselessVoid>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DarkEnergy")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(start);
		writer.Write(distance);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.Opacity);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		start = reader.ReadBoolean();
		distance = reader.ReadDouble();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.Opacity = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		if (start)
		{
			start = false;
			base.NPC.ai[3] = base.NPC.ai[0];
		}
		if (base.NPC.Opacity < 1f && base.NPC.dontTakeDamage)
		{
			base.NPC.damage = 0;
			base.NPC.Opacity += 0.005f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
			base.NPC.scale = MathHelper.Lerp(0.05f, Main.getGoodWorld ? 0.5f : 1f, base.NPC.Opacity);
		}
		else
		{
			if (base.NPC.dontTakeDamage)
			{
				for (int k = 0; k < 15; k++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173);
					Main.dust[dust].noGravity = true;
				}
			}
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.dontTakeDamage = false;
			float scalar = (float)Math.Cos(base.NPC.Calamity().newAI[1] * 0.33f) / 2f + 0.5f;
			base.NPC.scale = MathHelper.Lerp(0.8f, 1f, scalar);
			base.NPC.Opacity = MathHelper.Lerp(0.5f, 1f, scalar);
			base.NPC.Calamity().newAI[1]++;
		}
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.8f * base.NPC.Opacity, 0f, 1.2f * base.NPC.Opacity);
		if (CalamityGlobalNPC.voidBoss < 0 || !Main.npc[CalamityGlobalNPC.voidBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		int num;
		double num2;
		if (!CalamityWorld.death)
		{
			num = (BossRushEvent.BossRushActive ? 1 : 0);
			if (num == 0)
			{
				num2 = (revenge ? 960.0 : (expertMode ? 880.0 : 800.0));
				goto IL_033b;
			}
		}
		else
		{
			num = 1;
		}
		num2 = 1040.0;
		goto IL_033b;
		IL_033b:
		double maxDistance = num2;
		double rateOfChangeIncrease = maxDistance / 800.0 - 1.0;
		double rateOfChange = (double)(base.NPC.ai[1] * 0.5f) + 2.0 + rateOfChangeIncrease;
		if (base.NPC.Calamity().newAI[0] == 0f)
		{
			distance += rateOfChange;
			if (distance >= maxDistance)
			{
				distance = maxDistance;
				base.NPC.Calamity().newAI[0] = 1f;
			}
		}
		else
		{
			distance -= rateOfChange;
			if (distance <= 10.0)
			{
				distance = 10.0;
				base.NPC.Calamity().newAI[0] = 0f;
			}
		}
		float minRotationVelocity = 0.5f;
		float rotationVelocityIncrease = ((num != 0) ? 0.2f : (revenge ? 0.15f : (expertMode ? 0.1f : 0f)));
		rotationVelocityIncrease += rotationVelocityIncrease * (base.NPC.ai[1] * 0.5f);
		NPC parent = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<CeaselessVoid>())];
		double radians = (double)base.NPC.ai[3] * (Math.PI / 180.0);
		base.NPC.position.X = parent.Center.X - (float)(int)(Math.Cos(radians) * distance) - (float)(base.NPC.width / 2);
		base.NPC.position.Y = parent.Center.Y - (float)(int)(Math.Sin(radians) * distance) - (float)(base.NPC.height / 2);
		base.NPC.ai[3] += minRotationVelocity + rotationVelocityIncrease;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D mainTexture = TextureAssets.Npc[base.Type].Value;
		Vector2 drawPos = base.NPC.Center - screenPos;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)(mainTexture.Width / 2), (float)(mainTexture.Height / Main.npcFrameCount[base.Type] / 2));
		if (base.NPC.IsABestiaryIconDummy)
		{
			float scale = 1f;
			Main.EntitySpriteDraw(mainTexture, drawPos, base.NPC.frame, Color.White, base.NPC.rotation, drawOrigin, scale, spriteEffects);
			return false;
		}
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color white = Color.White * base.NPC.Opacity;
		int trailCount = 5;
		drawPos -= new Vector2((float)mainTexture.Width, (float)(mainTexture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < trailCount; i += 2)
			{
				Color trailColor = drawColor;
				trailColor = Color.Lerp(trailColor, white, 0.5f);
				trailColor = base.NPC.GetAlpha(trailColor);
				trailColor *= (float)(trailCount - i) / 15f;
				Vector2 trailPos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				trailPos -= new Vector2((float)mainTexture.Width, (float)(mainTexture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				trailPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(mainTexture, trailPos, (Rectangle?)base.NPC.frame, trailColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(mainTexture, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
		if (base.NPC.dontTakeDamage)
		{
			return false;
		}
		Texture2D glowTexture = GlowTexture.Value;
		Color glowColor = Color.Lerp(Color.White, Color.Fuchsia, 0.5f) * base.NPC.Opacity;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < trailCount; j++)
			{
				Color trailColor2 = glowColor;
				trailColor2 = Color.Lerp(trailColor2, white, 0.5f);
				trailColor2 = base.NPC.GetAlpha(trailColor2);
				trailColor2 *= (float)(trailCount - j) / 15f;
				Vector2 trailPos2 = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				trailPos2 -= new Vector2((float)glowTexture.Width, (float)(glowTexture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				trailPos2 += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(glowTexture, trailPos2, (Rectangle?)base.NPC.frame, trailColor2, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(glowTexture, drawPos, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, drawOrigin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * balance);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (Main.zenithWorld && hurtInfo.Damage > 0)
		{
			target.AddBuff(80, 30);
		}
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
		return minDist <= 35f * base.NPC.scale;
	}

	public override bool CheckDead()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 20; k++)
			{
				int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}
