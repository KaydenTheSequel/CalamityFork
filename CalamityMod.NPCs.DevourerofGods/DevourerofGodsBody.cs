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
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DevourerofGods;

[LongDistanceNetSync(SyncWith = typeof(DevourerofGodsHead))]
public class DevourerofGodsBody : ModNPC
{
	public static int phase2IconIndex;

	private const float LaserVelocityMultiplierMin = 0.5f;

	private const float LaserVelocityDistanceMultiplier = 0.025f;

	private int invinceTime;

	private bool setOpacity;

	private bool phase2Started;

	public int SegmentIndex;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> TextureP2;

	public static Asset<Texture2D> TextureP2_Glow_Purple;

	public static Asset<Texture2D> TextureP2_Glow_Cyan;

	private Vector2 noiseOffset;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.DevourerofGodsHead.DisplayName");

	public override void Load()
	{
		string phase2IconPath = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsBody_P2_Head_Boss";
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
			TextureP2 = ModContent.Request<Texture2D>(Texture + "_P2", (AssetRequestMode)2);
			TextureP2_Glow_Purple = ModContent.Request<Texture2D>(Texture + "_P2_Glow_Purple", (AssetRequestMode)2);
			TextureP2_Glow_Cyan = ModContent.Request<Texture2D>(Texture + "_P2_Glow_Cyan", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 120;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 56;
		base.NPC.height = 56;
		base.NPC.defense = 70;
		CalamityGlobalNPC global = base.NPC.Calamity();
		if (!Main.zenithWorld)
		{
			global.DR = 0.925f;
			global.unbreakableDR = true;
			base.NPC.chaseable = false;
		}
		base.NPC.LifeMaxNERB(760000, 910000, 1500000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.dontCountMe = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 1.5f;
		}
	}

	public override void BossHeadSlot(ref int index)
	{
		NPC obj = ((CalamityGlobalNPC.DoGHead >= 0) ? Main.npc[CalamityGlobalNPC.DoGHead] : null);
		DevourerofGodsHead modNPC = obj?.ModNPC<DevourerofGodsHead>() ?? null;
		if (obj == null || modNPC.AwaitingPhase2Teleport || !modNPC.Phase2Started || base.NPC.Opacity < 0.1f)
		{
			index = -1;
		}
		else
		{
			index = phase2IconIndex;
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
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(setOpacity);
		writer.Write(base.NPC.Opacity);
		writer.Write(SegmentIndex);
		writer.Write(base.NPC.frame.X);
		writer.Write(base.NPC.frame.Y);
		writer.Write(base.NPC.frame.Width);
		writer.Write(base.NPC.frame.Height);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		phase2Started = reader.ReadBoolean();
		invinceTime = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		setOpacity = reader.ReadBoolean();
		base.NPC.Opacity = reader.ReadSingle();
		SegmentIndex = reader.ReadInt32();
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
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		base.NPC.life = Main.npc[(int)base.NPC.ai[2]].life;
		base.NPC.lifeMax = Main.npc[(int)base.NPC.ai[2]].lifeMax;
		base.NPC.life = base.NPC.lifeMax;
		float lifeRatio = (float)Main.npc[(int)base.NPC.ai[2]].life / (float)Main.npc[(int)base.NPC.ai[2]].lifeMax;
		bool num = lifeRatio < 0.65f;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (CalamityWorld.revenge)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (num && !phase2Started && Main.npc[(int)base.NPC.ai[2]].localAI[2] <= 60f)
		{
			phase2Started = true;
			if (Main.npc[(int)base.NPC.ai[2]].localAI[2] == 60f)
			{
				base.NPC.position = base.NPC.Center;
				base.NPC.width = (int)(70f * base.NPC.scale);
				base.NPC.height = (int)(70f * base.NPC.scale);
				base.NPC.frame = new Rectangle(0, 0, 114, 88);
				NPC nPC = base.NPC;
				nPC.position -= base.NPC.Size * 0.5f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
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
		_ = Main.player[base.NPC.target];
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
		NPC aheadSegment = Main.npc[(int)base.NPC.ai[1]];
		Vector2 directionToNextSegment = aheadSegment.Center - base.NPC.Center;
		if (aheadSegment.rotation != base.NPC.rotation)
		{
			directionToNextSegment = directionToNextSegment.RotatedBy(MathHelper.WrapAngle(aheadSegment.rotation - base.NPC.rotation) * 0.08f);
			directionToNextSegment = directionToNextSegment.MoveTowards((aheadSegment.rotation - base.NPC.rotation).ToRotationVector2(), 1f);
		}
		base.NPC.rotation = directionToNextSegment.ToRotation() + (float)Math.PI / 2f;
		base.NPC.Center = aheadSegment.Center - directionToNextSegment.SafeNormalize(Vector2.Zero) * base.NPC.scale * (float)(phase2Started ? 80 : base.NPC.width);
		base.NPC.spriteDirection = (directionToNextSegment.X > 0f).ToDirectionInt();
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
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
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
			miscShaderData.Shader.Parameters["noiseScale"].SetValue(0.25f);
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
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if ((!Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>().isInPassiveState && base.NPC.Opacity > 0.25f) & useOtherTextures)
		{
			texture2D15 = TextureP2_Glow_Purple.Value;
			Color glowmaskColor = Color.Lerp(Color.White, Color.Fuchsia, 0.5f);
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		if ((!Main.npc[(int)base.NPC.ai[2]].ModNPC<DevourerofGodsHead>().isInAgressiveState || !useOtherTextures) && base.NPC.Opacity > 0.25f)
		{
			texture2D15 = (useOtherTextures ? TextureP2_Glow_Cyan.Value : Texture_Glow.Value);
			Color glowmaskColor2 = Color.Lerp(Color.White, Color.Cyan, 0.5f);
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor2, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (Main.npc[(int)base.NPC.ai[2]].dontTakeDamage)
		{
			return false;
		}
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
		if (minDist <= (phase2Started ? 55f : 40f) * base.NPC.scale && base.NPC.Opacity >= 1f)
		{
			return invinceTime <= 0;
		}
		return false;
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

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread * Main.rand.NextFloat(), base.Mod.Find<ModGore>("DoGS6").Type, base.NPC.scale);
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

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public DevourerofGodsBody()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		invinceTime = 360;
		noiseOffset = Vector2.Zero;
		base._002Ector();
	}
}
