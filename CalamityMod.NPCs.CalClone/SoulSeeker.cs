using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.CalClone;

public class SoulSeeker : ModNPC
{
	private int timer;

	private bool start = true;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.damage = 0;
		base.NPC.defense = 10;
		base.NPC.lifeMax = (CalamityWorld.death ? 1500 : 2500);
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 15000;
		}
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<CalamitasClone>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SoulSeeker")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override bool PreAI()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (CalamityGlobalNPC.calamitas < 0 || !Main.npc[CalamityGlobalNPC.calamitas].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return false;
		}
		NPC parent = Main.npc[CalamityGlobalNPC.calamitas];
		if (start)
		{
			for (int d = 0; d < 15; d++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			}
			base.NPC.ai[1] = base.NPC.ai[0];
			start = false;
		}
		float projectileSpeed = 9f;
		Vector2 velocity = Main.player[parent.target].Center - base.NPC.Center;
		((Vector2)(ref velocity)).Normalize();
		velocity *= projectileSpeed;
		base.NPC.rotation = velocity.ToRotation() + (float)Math.PI;
		timer++;
		if (timer >= 180)
		{
			if (Main.netMode != 1 && parent.ai[1] < 2f && parent.Calamity().newAI[2] <= 0f)
			{
				for (int i = 0; i < 3; i++)
				{
					Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
				}
				int type = ModContent.ProjectileType<BurningBolt>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, type, CalamitasClone.DartDamage, 1f, parent.target, 1f, 0f, projectileSpeed * 2f);
			}
			timer = 0;
		}
		double rad = (double)base.NPC.ai[1] * (Math.PI / 180.0);
		double dist = (death ? 180 : 150);
		base.NPC.position.X = parent.Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.NPC.width / 2);
		base.NPC.position.Y = parent.Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.NPC.height / 2);
		base.NPC.velocity = Vector2.Zero;
		base.NPC.ai[1] += (death ? 0.5f : 2f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSeeker").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSeeker2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SoulSeeker3").Type);
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 50);
		base.NPC.Center = base.NPC.position;
		for (int d = 0; d < 20; d++)
		{
			int red = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[red];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[red].scale = 0.5f;
				Main.dust[red].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 40; i++)
		{
			int red2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[red2].noGravity = true;
			Dust obj2 = Main.dust[red2];
			obj2.velocity *= 5f;
			red2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[red2];
			obj3.velocity *= 2f;
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)(texture.Height / Main.npcFrameCount[base.Type]) / 2f);
		Color white = Color.White;
		float colorLerpAmt = 0.5f;
		int afterImageAmt = 5;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int a = 1; a < afterImageAmt; a += 2)
			{
				Color afterImageColor = drawColor;
				afterImageColor = Color.Lerp(afterImageColor, white, colorLerpAmt);
				afterImageColor = base.NPC.GetAlpha(afterImageColor);
				afterImageColor *= (float)(afterImageAmt - a) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[a] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimagePos, (Rectangle?)base.NPC.frame, afterImageColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawPos = base.NPC.Center - screenPos;
		drawPos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color glow = Color.Lerp(Color.White, Color.Red, colorLerpAmt);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterImageAmt; i++)
			{
				Color glowColor = glow;
				glowColor = Color.Lerp(glowColor, white, colorLerpAmt);
				glowColor *= (float)(afterImageAmt - i) / 15f;
				Vector2 afterimagePos2 = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos2 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos2 += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimagePos2, (Rectangle?)base.NPC.frame, glowColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture, drawPos, (Rectangle?)base.NPC.frame, white, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}
}
