using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class SoulSlurper : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.npcSlots = 1f;
		base.NPC.damage = 0;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 80;
		base.NPC.knockBackResist = 0.65f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		if (DownedBossSystem.downedProvidence)
		{
			base.NPC.damage = 60;
			base.NPC.defense = 45;
			base.NPC.lifeMax = 2000;
		}
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SoulSlurperBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SoulSlurper")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.Calamity().ZoneCalamity)
		{
			return 0f;
		}
		return 0.25f;
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		bool provy = DownedBossSystem.downedProvidence;
		Player target = Main.player[base.NPC.target];
		if (base.NPC.target < 0 || base.NPC.target == 255 || target.dead)
		{
			base.NPC.TargetClosest();
		}
		float npcSpeed = 5f;
		float npcAcceleration = 0.07f;
		Vector2 npcCenter = base.NPC.Center;
		float targetX = target.Center.X;
		float targetY = target.Center.Y;
		targetX = (int)(targetX / 8f) * 8;
		targetY = (int)(targetY / 8f) * 8;
		npcCenter.X = (int)(npcCenter.X / 8f) * 8;
		npcCenter.Y = (int)(npcCenter.Y / 8f) * 8;
		targetX -= npcCenter.X;
		targetY -= npcCenter.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		float num = targetDistance;
		bool tooFar = false;
		if (targetDistance > 600f)
		{
			tooFar = true;
		}
		if (targetDistance == 0f)
		{
			targetX = base.NPC.velocity.X;
			targetY = base.NPC.velocity.Y;
		}
		else
		{
			targetDistance = npcSpeed / targetDistance;
			targetX *= targetDistance;
			targetY *= targetDistance;
		}
		if (num > 100f)
		{
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] > 0f)
			{
				base.NPC.velocity.Y += 0.023f;
			}
			else
			{
				base.NPC.velocity.Y -= 0.023f;
			}
			if (base.NPC.ai[0] < -100f || base.NPC.ai[0] > 100f)
			{
				base.NPC.velocity.X += 0.023f;
			}
			else
			{
				base.NPC.velocity.X -= 0.023f;
			}
			if (base.NPC.ai[0] > 200f)
			{
				base.NPC.ai[0] = -200f;
			}
		}
		if (target.dead)
		{
			targetX = (float)base.NPC.direction * npcSpeed / 2f;
			targetY = (0f - npcSpeed) / 2f;
		}
		if (base.NPC.velocity.X < targetX)
		{
			base.NPC.velocity.X += npcAcceleration;
		}
		else if (base.NPC.velocity.X > targetX)
		{
			base.NPC.velocity.X -= npcAcceleration;
		}
		if (base.NPC.velocity.Y < targetY)
		{
			base.NPC.velocity.Y += npcAcceleration;
		}
		else if (base.NPC.velocity.Y > targetY)
		{
			base.NPC.velocity.Y -= npcAcceleration;
		}
		base.NPC.localAI[0]++;
		if (base.NPC.justHit)
		{
			base.NPC.localAI[0] = 0f;
		}
		if (Main.netMode != 1 && base.NPC.localAI[0] >= 120f)
		{
			base.NPC.localAI[0] = 0f;
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, target.position, target.width, target.height))
			{
				int dmg = (Main.masterMode ? 19 : (Main.expertMode ? 22 : 30));
				int projType = ModContent.ProjectileType<BrimstoneBarrage>();
				Vector2 projectileVelocity = default(Vector2);
				((Vector2)(ref projectileVelocity))._002Ector(targetX, targetY);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 16f, projectileVelocity, projType, dmg + (provy ? 30 : 0), 0f, Main.myPlayer, 1f, 0f, ((Vector2)(ref projectileVelocity)).Length() * 3f);
			}
		}
		int num2 = (int)base.NPC.Center.X;
		int npcTileY = (int)base.NPC.Center.Y;
		int i = num2 / 16;
		npcTileY /= 16;
		if (!WorldGen.SolidTile(i, npcTileY))
		{
			Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.75f, 0f, 0f);
		}
		if (targetX > 0f)
		{
			base.NPC.spriteDirection = 1;
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX);
		}
		if (targetX < 0f)
		{
			base.NPC.spriteDirection = -1;
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX) + (float)Math.PI;
		}
		float recoilSpeed = 0.7f;
		if (base.NPC.collideX)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - recoilSpeed);
			if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
			{
				base.NPC.velocity.X = 2f;
			}
			if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
			{
				base.NPC.velocity.X = -2f;
			}
		}
		if (base.NPC.collideY)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - recoilSpeed);
			if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1.5f)
			{
				base.NPC.velocity.Y = 2f;
			}
			if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1.5f)
			{
				base.NPC.velocity.Y = -2f;
			}
		}
		if (tooFar)
		{
			if ((base.NPC.velocity.X > 0f && targetX > 0f) || (base.NPC.velocity.X < 0f && targetX < 0f))
			{
				if (Math.Abs(base.NPC.velocity.X) < 12f)
				{
					base.NPC.velocity.X *= 1.05f;
				}
			}
			else
			{
				base.NPC.velocity.X *= 0.9f;
			}
		}
		if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
		{
			base.NPC.netUpdate = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		int afterimageAmt = 5;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color redGlow = Color.Lerp(Color.White, Color.Red, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color glowmaskAfterimageColor = redGlow;
				glowmaskAfterimageColor = Color.Lerp(glowmaskAfterimageColor, Color.White, 0.5f);
				glowmaskAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 glowmaskAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				glowmaskAfterimagePos -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
				glowmaskAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, glowmaskAfterimagePos, (Rectangle?)base.NPC.frame, glowmaskAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, (SpriteEffects)(base.NPC.spriteDirection != 1), 0f);
			}
		}
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, redGlow, base.NPC.rotation, halfSizeTexture, base.NPC.scale, (SpriteEffects)(base.NPC.spriteDirection != 1), 0f);
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => Main.hardMode);
		mainRule.Add(ModContent.ItemType<SlurperPole>(), 30, 1, 1, !Main.hardMode);
		mainRule.AddFail(ModContent.ItemType<SlurperPole>(), 10, 1, 1, Main.hardMode);
		LeadingConditionRule mainRule2 = npcLoot.DefineConditionalDropSet(DropHelper.Hardmode());
		LeadingConditionRule postProv = npcLoot.DefineConditionalDropSet(DropHelper.PostProv());
		mainRule2.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
		postProv.Add(ModContent.ItemType<Bloodstone>(), 4);
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
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSlurper").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSlurper2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSlurper3").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 10; i++)
		{
			int brimDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[brimDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[brimDust].scale = 0.5f;
				Main.dust[brimDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			int brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[brimDust2].noGravity = true;
			Dust obj2 = Main.dust[brimDust2];
			obj2.velocity *= 5f;
			brimDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[brimDust2];
			obj3.velocity *= 2f;
		}
	}
}
