using System;
using System.IO;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

[LongDistanceNetSync(SyncWith = typeof(SepulcherHead))]
public class SepulcherTail : ModNPC
{
	private bool setAlpha;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.SepulcherHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 20;
		base.NPC.height = 20;
		base.NPC.defense = 0;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
		base.NPC.lifeMax = (CalamityWorld.revenge ? 345000 : 300000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.scale *= (Main.expertMode ? 1.35f : 1.2f);
		base.NPC.alpha = 255;
		base.NPC.chaseable = false;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.canGhostHeal = false;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(setAlpha);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		setAlpha = reader.ReadBoolean();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		bool shouldDie = false;
		if (base.NPC.ai[1] <= 0f)
		{
			shouldDie = true;
		}
		else if (Main.npc[(int)base.NPC.ai[1]].life <= 0 || base.NPC.life <= 0)
		{
			shouldDie = true;
		}
		if (shouldDie)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128 && !setAlpha)
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
			if (base.NPC.alpha <= 0)
			{
				setAlpha = true;
				base.NPC.alpha = 0;
			}
		}
		else
		{
			base.NPC.alpha = Main.npc[(int)base.NPC.ai[2]].alpha;
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
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX) + 1.57f;
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
		if (Main.zenithWorld && !NPC.AnyNPCs(ModContent.NPCType<BrimstoneHeart>()))
		{
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			calamityGlobalNPC.DR = 0.5f;
			calamityGlobalNPC.unbreakableDR = false;
			base.NPC.chaseable = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0 && !base.NPC.Calamity().unbreakableDR)
		{
			base.NPC.soundDelay = Main.rand.Next(5, 8);
			SoundEngine.PlaySound(in SoundID.DD2_SkeletonHurt, base.NPC.Center);
		}
		if (base.NPC.life > 0 || Main.dedServ)
		{
			return;
		}
		for (int i = 1; i <= 2; i++)
		{
			Vector2 goreSpawnPosition = base.NPC.Center;
			if (i == 2)
			{
				goreSpawnPosition -= (base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2() * 20f;
			}
			Gore.NewGorePerfect(base.NPC.GetSource_Death(), goreSpawnPosition, Main.rand.NextVector2Circular(3f, 3f), base.Mod.Find<ModGore>($"SepulcherTail_Gore{i}").Type, base.NPC.scale);
		}
	}

	public override bool CheckActive()
	{
		return false;
	}
}
