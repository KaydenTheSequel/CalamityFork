using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AstrumDeus;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(AstrumDeusHead))]
public class AstrumDeusTail : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public static Asset<Texture2D> GlowTexture2;

	public static Asset<Texture2D> TextureFlash;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.AstrumDeusHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			GlowTexture2 = ModContent.Request<Texture2D>(Texture + "Glow2", (AssetRequestMode)2);
			TextureFlash = ModContent.Request<Texture2D>(Texture + "GlowFlash", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 60;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 52;
		base.NPC.height = 68;
		base.NPC.defense = 50;
		base.NPC.DR_NERD(0.4f);
		base.NPC.LifeMaxNERB(200000, 240000, 650000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.4f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.35f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.2f;
		}
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = AstrumDeusHead.HitSound;
		base.NPC.DeathSound = AstrumDeusHead.DeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.dontCountMe = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (calamityGlobalNPC.newAI[1] < 180f || base.NPC.dontTakeDamage)
		{
			base.NPC.damage = 0;
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		Vector2.Distance(player.Center, base.NPC.Center);
		Vector2.Distance(player.Center, base.NPC.Center);
		if (revenge && !Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
		{
			Main.LocalPlayer.AddBuff(ModContent.BuffType<DoGExtremeGravity>(), 2);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool doubleWormPhase = calamityGlobalNPC.newAI[0] != 0f;
		bool startFlightPhase = (lifeRatio < 0.8f) | death | doubleWormPhase;
		bool deathModeEnragePhase_Head = calamityGlobalNPC.newAI[0] == 3f;
		bool deathModeEnragePhase_BodyAndTail = false;
		float resistanceTime = (doubleWormPhase ? 300f : 600f);
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = calamityGlobalNPC.newAI[1] < resistanceTime;
		float aiSwitchTimer = ((!doubleWormPhase) ? (Main.getGoodWorld ? 900f : 1800f) : (Main.getGoodWorld ? 600f : 1200f));
		calamityGlobalNPC.newAI[3]++;
		if (calamityGlobalNPC.newAI[3] >= aiSwitchTimer)
		{
			calamityGlobalNPC.newAI[3] = 0f;
		}
		if (doubleWormPhase && calamityGlobalNPC.newAI[3] % aiSwitchTimer == 0f && !(deathModeEnragePhase_Head | deathModeEnragePhase_BodyAndTail))
		{
			SoundStyle style = AstrumDeusHead.SplitSound with
			{
				Pitch = -0.2f,
				Volume = 0.9f
			};
			SoundEngine.PlaySound(in style, player.Center);
		}
		_ = calamityGlobalNPC.newAI[3];
		int phase1Length = (death ? 80 : (revenge ? 70 : (expertMode ? 60 : 50)));
		int phase2Length = (death ? 40 : (revenge ? 35 : (expertMode ? 30 : 25)));
		int gfbLength = (death ? 8 : (revenge ? 7 : (expertMode ? 6 : 5)));
		if (!(Main.zenithWorld & doubleWormPhase))
		{
		}
		int gfbMaxWormCount = 10;
		int gfbWormCount = 0;
		if (Main.zenithWorld)
		{
			gfbWormCount = NPC.CountNPCS(ModContent.NPCType<AstrumDeusHead>());
		}
		if (gfbWormCount > gfbMaxWormCount)
		{
			gfbWormCount = gfbMaxWormCount;
		}
		base.NPC.dontTakeDamage = Main.npc[(int)base.NPC.ai[2]].dontTakeDamage;
		base.NPC.Opacity = Main.npc[(int)base.NPC.ai[2]].Opacity;
		if (Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] == 3f)
		{
			base.NPC.defense = 25;
			calamityGlobalNPC.DR = 0.15f;
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128 && !base.NPC.dontTakeDamage)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		bool shouldDespawn = true;
		int headType = ModContent.NPCType<AstrumDeusHead>();
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].type == headType && Main.npc[i].active)
			{
				shouldDespawn = false;
				break;
			}
		}
		if (shouldDespawn && Main.npc.IndexInRange((int)base.NPC.ai[1]) && Main.npc[(int)base.NPC.ai[1]].active && Main.npc[(int)base.NPC.ai[1]].life > 0)
		{
			shouldDespawn = false;
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		float segmentVelocity = ((calamityGlobalNPC.newAI[1] < resistanceTime * 0.4f && !doubleWormPhase) ? 25f : (deathModeEnragePhase_Head ? 19f : (death ? 17.5f : 16f)));
		float segmentVelocityBoost = 5f * (1f - lifeRatio);
		segmentVelocity += segmentVelocityBoost;
		if (gfbWormCount > 0)
		{
			segmentVelocity += (float)(gfbMaxWormCount - gfbWormCount) * 0.444f;
		}
		if (revenge)
		{
			float revMultiplier = 1.1f;
			segmentVelocity *= revMultiplier;
		}
		Vector2 segmentCenter = base.NPC.Center;
		float segmentTargetX = player.Center.X;
		float segmentTargetY = player.Center.Y;
		segmentTargetX = (int)(segmentTargetX / 16f) * 16;
		segmentTargetY = (int)(segmentTargetY / 16f) * 16;
		segmentCenter.X = (int)(segmentCenter.X / 16f) * 16;
		segmentCenter.Y = (int)(segmentCenter.Y / 16f) * 16;
		segmentTargetX -= segmentCenter.X;
		segmentTargetY -= segmentCenter.Y;
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentCenter = base.NPC.Center;
				segmentTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentCenter.X;
				segmentTargetY = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentCenter.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(segmentTargetY, segmentTargetX) + (float)Math.PI / 2f;
			float segmentTargetDist = (float)Math.Sqrt(segmentTargetX * segmentTargetX + segmentTargetY * segmentTargetY);
			int segmentWidth = base.NPC.width;
			segmentTargetDist = (segmentTargetDist - (float)segmentWidth) / segmentTargetDist;
			segmentTargetX *= segmentTargetDist;
			segmentTargetY *= segmentTargetDist;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + segmentTargetX;
			base.NPC.position.Y = base.NPC.position.Y + segmentTargetY;
			if (segmentTargetX < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (segmentTargetX > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
		if (calamityGlobalNPC.newAI[1] == 0f && !doubleWormPhase)
		{
			SoundEngine.PlaySound(in AstrumDeusHead.SpawnSound, base.NPC.Center);
			calamityGlobalNPC.newAI[1] = 1f;
		}
		if (calamityGlobalNPC.newAI[1] < resistanceTime)
		{
			Vector2 val = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val)).Length() > 2f || calamityGlobalNPC.newAI[1] > 1f)
			{
				calamityGlobalNPC.newAI[1]++;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool deathModeEnragePhase = Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[0] == 3f;
		bool doubleWormPhase = base.NPC.Calamity().newAI[0] != 0f && !deathModeEnragePhase;
		float cyanThreshold = (Main.getGoodWorld ? 300f : 600f);
		float transitionStart = cyanThreshold * 0.75f;
		float transitionEnd = cyanThreshold * 0.8f;
		bool drawCyan = base.NPC.Calamity().newAI[3] >= transitionEnd && base.NPC.Calamity().newAI[3] <= cyanThreshold + transitionEnd;
		bool inColorTrans = doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold >= transitionStart && base.NPC.Calamity().newAI[3] % cyanThreshold <= transitionEnd;
		Texture2D wormTexture = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTex = default(Vector2);
		((Vector2)(ref halfSizeTex))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)wormTexture.Width, (float)wormTexture.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTex * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(wormTexture, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
		wormTexture = GlowTexture.Value;
		Color phaseColor = (drawCyan ? Color.Cyan : Color.Orange);
		Color otherPhaseColor = (drawCyan ? Color.Orange : Color.Cyan);
		Texture2D otherTexture;
		if (doubleWormPhase)
		{
			wormTexture = (drawCyan ? GlowTexture2.Value : wormTexture);
			otherTexture = (drawCyan ? wormTexture : GlowTexture2.Value);
		}
		else
		{
			otherTexture = wormTexture;
		}
		Color wormColorLerp = Color.Lerp(Color.White, doubleWormPhase ? phaseColor : Color.Orange, 0.5f) * (deathModeEnragePhase ? 1f : base.NPC.Opacity);
		int timesToDraw = (deathModeEnragePhase ? 3 : ((!drawCyan) ? 1 : 2));
		for (int i = 0; i < timesToDraw; i++)
		{
			float opacity = Utils.GetLerpValue(transitionStart, transitionEnd, base.NPC.Calamity().newAI[3] % cyanThreshold, clamped: true);
			spriteBatch.Draw(wormTexture, drawLocation, (Rectangle?)base.NPC.frame, wormColorLerp * (inColorTrans ? (1f - opacity) : 1f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			if (inColorTrans)
			{
				spriteBatch.Draw(otherTexture, drawLocation, (Rectangle?)base.NPC.frame, Color.Lerp(Color.White, otherPhaseColor, 0.5f) * opacity, base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
			if (doubleWormPhase && base.NPC.Calamity().newAI[3] % cyanThreshold < 25f)
			{
				spriteBatch.Draw(TextureFlash.Value, drawLocation, (Rectangle?)base.NPC.frame, Color.White * MathHelper.Lerp(1f, 0f, base.NPC.Calamity().newAI[3] % cyanThreshold / 25f), base.NPC.rotation, halfSizeTex, base.NPC.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return !base.NPC.dontTakeDamage;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0 || Main.zenithWorld)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 5; i++)
		{
			int purpleDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[purpleDust].scale = 0.5f;
				Main.dust[purpleDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
			Main.dust[astralDust].noGravity = true;
			Dust obj2 = Main.dust[astralDust];
			obj2.velocity *= 5f;
			astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[astralDust];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusTail1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("AstrumDeusTail2").Type);
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
