using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DevourerofGods;

[LongDistanceNetSync(SyncWith = typeof(DevourerofGodsHead))]
public class DevourerofGodsTail : ModNPC
{
	public static int phase1IconIndex;

	public static int phase2IconIndex;

	public static Asset<Texture2D> Texture_Glow_Purple;

	public static Asset<Texture2D> Texture_Glow_Cyan;

	public static Asset<Texture2D> TextureP2;

	public static Asset<Texture2D> TextureP2_Glow_Purple;

	public static Asset<Texture2D> TextureP2_Glow_Cyan;

	private int invinceTime;

	private bool setOpacity;

	private bool phase2Started;

	private Vector2 noiseOffset;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.DevourerofGodsHead.DisplayName");

	public override void Load()
	{
		string phase1IconPath = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsTail_Head_Boss";
		string phase2IconPath = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsTail_P2_Head_Boss";
		phase1IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase1IconPath);
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			Texture_Glow_Purple = ModContent.Request<Texture2D>(Texture + "_Glow_Purple", (AssetRequestMode)2);
			Texture_Glow_Cyan = ModContent.Request<Texture2D>(Texture + "_Glow_Cyan", (AssetRequestMode)2);
			TextureP2 = ModContent.Request<Texture2D>(Texture + "_P2", (AssetRequestMode)2);
			TextureP2_Glow_Purple = ModContent.Request<Texture2D>(Texture + "_P2_Glow_Purple", (AssetRequestMode)2);
			TextureP2_Glow_Cyan = ModContent.Request<Texture2D>(Texture + "_P2_Glow_Cyan", (AssetRequestMode)2);
		}
	}

	internal void setInvulTime(int time)
	{
		invinceTime = time;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 100;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 66;
		base.NPC.height = 66;
		base.NPC.defense = 50;
		base.NPC.LifeMaxNERB(760000, 910000, 1500000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.dontCountMe = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 1.5f;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.takenDamageMultiplier = 2f;
		}
	}

	public override void BossHeadSlot(ref int index)
	{
		NPC obj = ((CalamityGlobalNPC.DoGHead >= 0) ? Main.npc[CalamityGlobalNPC.DoGHead] : null);
		DevourerofGodsHead modNPC = obj?.ModNPC<DevourerofGodsHead>() ?? null;
		index = -1;
		if (obj != null && !(base.NPC.Opacity < 0.1f))
		{
			if (!modNPC.Phase2Started)
			{
				index = phase1IconIndex;
			}
			else if (!modNPC.AwaitingPhase2Teleport)
			{
				index = phase2IconIndex;
			}
		}
	}

	public override void BossHeadRotation(ref float rotation)
	{
		NPC obj = ((CalamityGlobalNPC.DoGHead >= 0) ? Main.npc[CalamityGlobalNPC.DoGHead] : null);
		DevourerofGodsHead modNPC = obj?.ModNPC<DevourerofGodsHead>() ?? null;
		if (obj != null && !modNPC.AwaitingPhase2Teleport && modNPC.Phase2Started)
		{
			rotation = base.NPC.rotation;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(phase2Started);
		writer.Write(invinceTime);
		writer.Write(setOpacity);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.Opacity);
		writer.Write(base.NPC.frame.X);
		writer.Write(base.NPC.frame.Y);
		writer.Write(base.NPC.frame.Width);
		writer.Write(base.NPC.frame.Height);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		phase2Started = reader.ReadBoolean();
		invinceTime = reader.ReadInt32();
		setOpacity = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.Opacity = reader.ReadSingle();
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
		if (frame.Width > 0 && frame.Height > 0)
		{
			base.NPC.frame = frame;
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		base.NPC.life = Main.npc[(int)base.NPC.ai[2]].life;
		base.NPC.lifeMax = Main.npc[(int)base.NPC.ai[2]].lifeMax;
		float lifeRatio = (float)Main.npc[(int)base.NPC.ai[2]].life / (float)Main.npc[(int)base.NPC.ai[2]].lifeMax;
		bool num = lifeRatio < 0.65f;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (num && !phase2Started && Main.npc[(int)base.NPC.ai[2]].localAI[2] <= 60f)
		{
			phase2Started = true;
			base.NPC.position = base.NPC.Center;
			base.NPC.width = (int)(80f * base.NPC.scale);
			base.NPC.height = (int)(80f * base.NPC.scale);
			base.NPC.frame = new Rectangle(0, 0, 86, 148);
			NPC nPC = base.NPC;
			nPC.position -= base.NPC.Size * 0.5f;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if (invinceTime > 0)
		{
			invinceTime--;
			base.NPC.dontTakeDamage = true;
		}
		else
		{
			base.NPC.dontTakeDamage = Main.npc[(int)base.NPC.ai[2]].dontTakeDamage;
		}
		if (Main.npc[(int)base.NPC.ai[2]].dontTakeDamage)
		{
			invinceTime = 240;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>());
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
		if (Main.npc[(int)base.NPC.ai[1]].Opacity >= 0.5f && (!setOpacity || (Main.npc[(int)base.NPC.ai[2]].localAI[2] <= 60f && Main.npc[(int)base.NPC.ai[2]].localAI[2] > 0f)))
		{
			base.NPC.Opacity += 0.165f;
			if (base.NPC.Opacity >= 1f && invinceTime <= 0)
			{
				setOpacity = true;
				base.NPC.Opacity = 1f;
			}
		}
		else
		{
			DevourerofGodsHead devourerofGodsHead = Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>();
			if (devourerofGodsHead != null && devourerofGodsHead.AttemptingToEnterPortal)
			{
				if (Main.netMode != 1)
				{
					Projectile portal = Main.projectile[Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>().PortalIndex];
					float newOpacity = 1f - Utils.GetLerpValue(270f, 100f, base.NPC.Distance(portal.Center), clamped: true);
					if (newOpacity > 0f && base.NPC.Opacity > newOpacity)
					{
						base.NPC.Opacity = newOpacity;
						if (Vector2.Dot((base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2(), Main.npc[(int)base.NPC.ai[2]].velocity) > 0f)
						{
							for (int i = 0; i < 2; i++)
							{
								Dust dust = Dust.NewDustPerfect(portal.Center, Main.rand.NextBool() ? 180 : 173);
								dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 8f);
								dust.scale *= Main.rand.NextFloat(1f, 1.8f);
								dust.noGravity = true;
							}
						}
						if (base.NPC.Opacity < 0.2f)
						{
							base.NPC.Opacity = 0f;
						}
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
			}
			else
			{
				base.NPC.Opacity = Main.npc[(int)base.NPC.ai[2]].Opacity;
			}
		}
		base.NPC.damage = ((Main.npc[(int)base.NPC.ai[2]].damage != 0) ? base.NPC.defDamage : 0);
		Vector2 segmentDirection = base.NPC.Center;
		float playerXDist = player.position.X + (float)(player.width / 2);
		float playerYDist = player.position.Y + (float)(player.height / 2);
		playerXDist = (int)(playerXDist / 16f) * 16;
		playerYDist = (int)(playerYDist / 16f) * 16;
		segmentDirection.X = (int)(segmentDirection.X / 16f) * 16;
		segmentDirection.Y = (int)(segmentDirection.Y / 16f) * 16;
		playerXDist -= segmentDirection.X;
		playerYDist -= segmentDirection.Y;
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentDirection = base.NPC.Center;
				playerXDist = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - segmentDirection.X;
				playerYDist = Main.npc[(int)base.NPC.ai[1]].position.Y + (float)(Main.npc[(int)base.NPC.ai[1]].height / 2) - segmentDirection.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(playerYDist, playerXDist) + (float)Math.PI / 2f;
			float playerDistance = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
			int segmentWidth = base.NPC.width;
			playerDistance = (playerDistance - (float)segmentWidth) / playerDistance;
			playerXDist *= playerDistance;
			playerYDist *= playerDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + playerXDist;
			base.NPC.position.Y = base.NPC.position.Y + playerYDist;
			if (playerXDist < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (playerXDist > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
		float segmentVelocity = (death ? 17.5f : 16f);
		if (expertMode)
		{
			segmentVelocity += 4f * (1f - lifeRatio);
		}
		if (Main.getGoodWorld)
		{
			segmentVelocity *= 1.1f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.realLife < 0 || base.NPC.realLife >= Main.maxNPCs || Main.npc[base.NPC.realLife] == null)
		{
			return true;
		}
		if (Main.npc[base.NPC.realLife].type != ModContent.NPCType<DevourerofGodsHead>())
		{
			return true;
		}
		bool num = CalamityDrawParameterNPC.DoGDeathAnimationTimer != 0;
		SpriteBatchSnapshot snap = new SpriteBatchSnapshot(spriteBatch);
		if (num)
		{
			if (noiseOffset == Vector2.zeroVector)
			{
				noiseOffset = base.NPC.Center;
			}
			Main.spriteBatch.End(out snap);
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:Dissolve"];
			Texture2D dissolveTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2).Value;
			miscShaderData.Shader.Parameters["noiseScale"].SetValue(1f);
			miscShaderData.Shader.Parameters["dissolveIntensity"].SetValue((float)CalamityDrawParameterNPC.DoGDeathAnimationTimer / 600f);
			miscShaderData.Shader.Parameters["sampleOffset"].SetValue(noiseOffset * 0.5f);
			EffectParameter obj = miscShaderData.Shader.Parameters["transitionColor"];
			Color specialMoveColor = DevourerofGodsHead.SpecialMoveColor;
			obj.SetValue(((Color)(ref specialMoveColor)).ToVector4());
			miscShaderData.Shader.Parameters["transitionOffset"].SetValue(0.05f);
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)dissolveTexture;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
			miscShaderData.Apply();
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool useOtherTextures = phase2Started && Main.npc[(int)base.NPC.ai[2]].localAI[2] <= 60f;
		Texture2D texture2D15 = (useOtherTextures ? TextureP2.Value : TextureAssets.Npc[base.Type].Value);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture2D15.Width / 2), (float)(texture2D15.Height / 2));
		Vector2 drawPosition = base.NPC.Center - screenPos;
		drawPosition -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawPosition += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if ((!Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>().isInPassiveState || !useOtherTextures) && base.NPC.Opacity > 0.25f)
		{
			texture2D15 = (useOtherTextures ? TextureP2_Glow_Purple.Value : Texture_Glow_Purple.Value);
			Color glowmaskColor = Color.Lerp(Color.White, Color.Fuchsia, 0.5f);
			spriteBatch.Draw(texture2D15, drawPosition, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		if (!Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>().isInAgressiveState && base.NPC.Opacity > 0.25f)
		{
			texture2D15 = (useOtherTextures ? TextureP2_Glow_Cyan.Value : Texture_Glow_Cyan.Value);
			Color glowmaskColor2 = Color.Lerp(Color.White, Color.Cyan, 0.5f);
			spriteBatch.Draw(texture2D15, drawPosition, (Rectangle?)base.NPC.frame, glowmaskColor2, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		if (num)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(in snap);
		}
		return false;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
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
		if (minDist <= (phase2Started ? 70f : 35f) * base.NPC.scale && base.NPC.Opacity >= 1f)
		{
			return invinceTime <= 0;
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 8;
			float extrapitch = (Main.zenithWorld ? 0.3f : 0f);
			SoundEngine.PlaySound(DevourerofGodsHead.HitSound with
			{
				Pitch = DevourerofGodsHead.HitSound.Pitch + extrapitch
			}, base.NPC.Center);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DoGS3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DoGS4").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(100f * base.NPC.scale);
		base.NPC.height = (int)(100f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 10; i++)
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
		for (int j = 0; j < 20; j++)
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

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(base.NPC.life - 1);
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (Main.zenithWorld && projectile.type == ModContent.ProjectileType<LaceratorYoyo>())
		{
			modifiers.SourceDamage *= 40f;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool CheckDead()
	{
		base.NPC.life = 1;
		base.NPC.dontTakeDamage = true;
		base.NPC.active = true;
		base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		if (base.NPC.realLife >= 0)
		{
			NPC Head = Main.npc[base.NPC.realLife];
			if (Head.type != ModContent.NPCType<DevourerofGodsHead>())
			{
				return false;
			}
			Head.ModNPC<DevourerofGodsHead>().Dying = true;
			Head.dontTakeDamage = true;
			Head.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public DevourerofGodsTail()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		invinceTime = 720;
		noiseOffset = Vector2.Zero;
		base._002Ector();
	}
}
