using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.StormWeaver;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(StormWeaverHead))]
public class StormWeaverTail : ModNPC
{
	private int invinceTime = 180;

	public static Asset<Texture2D> Phase2Texture;

	public static Asset<Texture2D> GlowTexture;

	public static int LightningOrbDamage = 64;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.StormWeaverHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			Phase2Texture = ModContent.Request<Texture2D>(Texture + "Naked", (AssetRequestMode)2);
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 80;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 48;
		base.NPC.height = 80;
		base.NPC.lifeMax = 825000;
		base.NPC.LifeMaxNERB(base.NPC.lifeMax, base.NPC.lifeMax, 500000);
		base.NPC.takenDamageMultiplier = 2f;
		base.NPC.HitSound = SoundID.NPCHit53;
		base.NPC.DeathSound = StormWeaverHead.DeathSound;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.boss = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.15f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.1f;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.7f;
		}
		base.NPC.Calamity().VulnerableToElectricity = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(invinceTime);
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		invinceTime = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		if (invinceTime > 0)
		{
			invinceTime--;
			base.NPC.damage = 0;
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.dontTakeDamage = false;
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (CalamityWorld.revenge)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if ((float)base.NPC.life / (float)base.NPC.lifeMax < 0.8f && (!Main.zenithWorld || !CalamityWorld.revenge))
		{
			if (base.NPC.takenDamageMultiplier == 2f)
			{
				base.NPC.Calamity().VulnerableToHeat = true;
				base.NPC.Calamity().VulnerableToCold = true;
				base.NPC.Calamity().VulnerableToSickness = true;
				if (!Main.dedServ)
				{
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWArmorTail1").Type, base.NPC.scale);
					Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWArmorTail2").Type, base.NPC.scale);
				}
				CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
				base.NPC.defense = 40;
				calamityGlobalNPC.DR = 0.4f;
				base.NPC.takenDamageMultiplier = 1f;
				base.NPC.HitSound = SoundID.NPCHit13;
				base.NPC.frame = new Rectangle(0, 0, 42, 68);
			}
		}
		else if (Main.netMode != 1)
		{
			float spawnOrbGateValue = 300f;
			if (Main.npc[(int)base.NPC.ai[2]].localAI[0] % spawnOrbGateValue == 0f)
			{
				int type = 465;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type, LightningOrbDamage, 0f, Main.myPlayer);
			}
		}
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<StormWeaverHead>());
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
			if (base.NPC.alpha != 0)
			{
				for (int i = 0; i < 2; i++)
				{
					int redDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
					Main.dust[redDust].noGravity = true;
					Main.dust[redDust].noLight = true;
				}
			}
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		Vector2 segmentLocation = base.NPC.Center;
		float targetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
		float targetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
		targetX = (int)(targetX / 16f) * 16;
		targetY = (int)(targetY / 16f) * 16;
		segmentLocation.X = (int)(segmentLocation.X / 16f) * 16;
		segmentLocation.Y = (int)(segmentLocation.Y / 16f) * 16;
		targetX -= segmentLocation.X;
		targetY -= segmentLocation.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentLocation = base.NPC.Center;
				targetX = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - segmentLocation.X;
				targetY = Main.npc[(int)base.NPC.ai[1]].position.Y + (float)(Main.npc[(int)base.NPC.ai[1]].height / 2) - segmentLocation.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX) + (float)Math.PI / 2f;
			targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			int npcWidth = base.NPC.width;
			targetDistance = (targetDistance - (float)npcWidth) / targetDistance;
			targetX *= targetDistance;
			targetY *= targetDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + targetX;
			base.NPC.position.Y = base.NPC.position.Y + targetY;
			if (targetX < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (targetX > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = num < 0.8f && (!Main.zenithWorld || !revenge);
		bool num2 = num < 0.55f;
		float chargePhaseGateValue = (death ? 320f : (revenge ? 340f : (expertMode ? 360f : 400f)));
		if (!num2)
		{
			chargePhaseGateValue *= 0.5f;
		}
		Texture2D texture = (phase2 ? Phase2Texture.Value : TextureAssets.Npc[base.Type].Value);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		float chargeTelegraphTime = 120f;
		float chargeTelegraphGateValue = chargePhaseGateValue - chargeTelegraphTime;
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color drawColorAlpha = base.NPC.GetAlpha(drawColor);
		if (Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] > chargeTelegraphGateValue)
		{
			drawColorAlpha = Color.Lerp(drawColorAlpha, Color.Cyan, MathHelper.Clamp((Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] - chargeTelegraphGateValue) / chargeTelegraphTime, 0f, 1f));
		}
		else if (Main.npc[(int)base.NPC.ai[2]].localAI[3] > 0f)
		{
			drawColorAlpha = Color.Lerp(drawColorAlpha, Color.Cyan, MathHelper.Clamp(Main.npc[(int)base.NPC.ai[2]].localAI[3] / 60f, 0f, 1f));
		}
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, drawColorAlpha, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if (!phase2)
		{
			texture = GlowTexture.Value;
			Color rainbowBecauseWhyTheFuckNot = default(Color);
			((Color)(ref rainbowBecauseWhyTheFuckNot))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
			Color drawColorAlpha37 = Color.Lerp(Color.White, rainbowBecauseWhyTheFuckNot, 0.5f);
			spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, drawColorAlpha37, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool num = (float)base.NPC.life / (float)base.NPC.lifeMax < 0.55f;
		float chargePhaseGateValue = (death ? 320f : (revenge ? 340f : (expertMode ? 360f : 400f)));
		if (!num)
		{
			chargePhaseGateValue *= 0.5f;
		}
		int buffDuration = ((Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] >= chargePhaseGateValue) ? 240 : 120);
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(144, buffDuration);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWNudeTail1").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWNudeTail2").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(50f * base.NPC.scale);
		base.NPC.height = (int)(50f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int cosmiliteDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[cosmiliteDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[cosmiliteDust].scale = 0.5f;
				Main.dust[cosmiliteDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int cosmiliteDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[cosmiliteDust2].noGravity = true;
			Dust obj2 = Main.dust[cosmiliteDust2];
			obj2.velocity *= 5f;
			cosmiliteDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[cosmiliteDust2];
			obj3.velocity *= 2f;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
