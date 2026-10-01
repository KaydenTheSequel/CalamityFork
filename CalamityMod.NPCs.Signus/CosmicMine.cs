using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Signus;

public class CosmicMine : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 140;
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.lifeMax = 4800;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit53;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CosmicMine")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.signus < 0 || !Main.npc[CalamityGlobalNPC.signus].active)
		{
			base.NPC.life = 0;
			base.NPC.checkDead();
			base.NPC.netUpdate = true;
			return;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.7f, 0.2f, 1.1f);
		base.NPC.rotation = base.NPC.velocity.X * 0.04f;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		Vector2 vector = player.Center - base.NPC.Center;
		base.NPC.ai[2] = ((Vector2)(ref vector)).Length();
		base.NPC.damage = 0;
		base.NPC.ai[3]++;
		if (base.NPC.ai[2] < 90f || base.NPC.ai[3] >= 300f || base.NPC.Calamity().newAI[0] > 0f)
		{
			base.NPC.Calamity().newAI[0]++;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
			base.NPC.dontTakeDamage = false;
			base.NPC.scale = MathHelper.Lerp(1f, 3f, base.NPC.Calamity().newAI[0] / 45f);
			if (base.NPC.Calamity().newAI[0] >= 45f)
			{
				CheckDead();
				base.NPC.life = 0;
			}
			return;
		}
		if (base.NPC.ai[1] == 0f)
		{
			base.NPC.scale -= 0.02f;
			base.NPC.alpha += 30;
			if (base.NPC.alpha >= 250)
			{
				base.NPC.alpha = 255;
				base.NPC.ai[1] = 1f;
			}
		}
		else if (base.NPC.ai[1] == 1f)
		{
			base.NPC.scale += 0.02f;
			base.NPC.alpha -= 30;
			if (base.NPC.alpha <= 0)
			{
				base.NPC.alpha = 0;
				base.NPC.ai[1] = 0f;
			}
		}
		float num = (death ? 16f : 14f);
		Vector2 vector167 = default(Vector2);
		((Vector2)(ref vector167))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
		float playerXDist = player.position.X + (float)player.width * 0.5f - vector167.X;
		float playerYDist = player.Center.Y - vector167.Y;
		float playerDistance = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
		float velocityMult = num / playerDistance;
		playerXDist *= velocityMult;
		playerYDist *= velocityMult;
		base.NPC.ai[0]--;
		if (playerDistance < 200f || base.NPC.ai[0] > 0f)
		{
			if (playerDistance < 200f)
			{
				base.NPC.ai[0] = 20f;
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			return;
		}
		base.NPC.velocity.X = (base.NPC.velocity.X * 50f + playerXDist) / 51f;
		base.NPC.velocity.Y = (base.NPC.velocity.Y * 50f + playerYDist) / 51f;
		if (playerDistance < 350f)
		{
			base.NPC.velocity.X = (base.NPC.velocity.X * 10f + playerXDist) / 11f;
			base.NPC.velocity.Y = (base.NPC.velocity.Y * 10f + playerYDist) / 11f;
		}
		if (playerDistance < 300f)
		{
			base.NPC.velocity.X = (base.NPC.velocity.X * 7f + playerXDist) / 8f;
			base.NPC.velocity.Y = (base.NPC.velocity.Y * 7f + playerYDist) / 8f;
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, Main.DiscoG, 255, 0);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.Calamity().newAI[0] >= 45f)
		{
			return false;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
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
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckDead()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.damage = base.NPC.defDamage;
		base.NPC.width = (base.NPC.height = 256);
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
			Main.dust[cosmiliteDust].noGravity = true;
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
			Main.dust[cosmiliteDust2].noGravity = true;
		}
		return true;
	}
}
