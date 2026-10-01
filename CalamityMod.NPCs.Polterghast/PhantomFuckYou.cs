using System;
using System.IO;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Polterghast;

public class PhantomFuckYou : ModNPC
{
	private bool start = true;

	public static int MineDamage = 70;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.defense = 45;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.chaseable = false;
		base.NPC.lifeMax = 20000;
		base.NPC.dontTakeDamage = true;
		base.NPC.HitSound = SoundID.NPCHit36;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		base.NPC.Calamity().VulnerableToSickness = false;
		if (Main.zenithWorld)
		{
			base.NPC.dontTakeDamage = true;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(start);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		start = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		if (start)
		{
			start = false;
			for (int i = 0; i < 10; i++)
			{
				int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dust].noGravity = true;
			}
			base.NPC.ai[1] = base.NPC.ai[0];
		}
		if (CalamityGlobalNPC.ghostBoss < 0 || !Main.npc[CalamityGlobalNPC.ghostBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		float chargePhaseGateValue = 480f;
		if (Main.getGoodWorld)
		{
			chargePhaseGateValue *= 0.5f;
		}
		bool num = Main.npc[CalamityGlobalNPC.ghostBoss].Calamity().newAI[0] >= chargePhaseGateValue - 60f;
		float lifeRatio = Main.npc[CalamityGlobalNPC.ghostBoss].life / Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax;
		float tileEnrageMult = Main.npc[CalamityGlobalNPC.ghostBoss].ai[3];
		base.NPC.TargetClosest();
		Vector2 direction = Main.player[base.NPC.target].Center - base.NPC.Center;
		((Vector2)(ref direction)).Normalize();
		direction *= 0.5f;
		base.NPC.rotation = direction.ToRotation();
		if (!num || Main.getGoodWorld)
		{
			base.NPC.ai[2]++;
			float shootMineGateValue = 150f;
			if (Main.getGoodWorld)
			{
				shootMineGateValue *= 0.5f;
			}
			if (base.NPC.ai[2] >= shootMineGateValue)
			{
				if (Main.netMode != 1)
				{
					int type = ModContent.ProjectileType<PhantomMine>();
					float maxVelocity = 8f * tileEnrageMult;
					float acceleration = 1.15f + (tileEnrageMult - 1f) * 0.15f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, direction, type, MineDamage, 1f, base.NPC.target, maxVelocity, acceleration);
				}
				base.NPC.ai[2] = 0f;
			}
		}
		NPC parent = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Polterghast>())];
		double rad = (double)base.NPC.ai[1] * (Math.PI / 180.0);
		double dist = 500.0;
		base.NPC.position.X = parent.Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.NPC.width / 2);
		base.NPC.position.Y = parent.Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.NPC.height / 2);
		float SPEEN = 1f - lifeRatio * 2f;
		if (SPEEN < 0f)
		{
			SPEEN = 0f;
		}
		base.NPC.ai[1] += (Main.getGoodWorld ? 1.5f : 0.5f) + SPEEN;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 0);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
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

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, hit.HitDirection, -1f);
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 45;
		base.NPC.height = 45;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 2; i++)
		{
			int ghostDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[ghostDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[ghostDust].scale = 0.5f;
				Main.dust[ghostDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
			Main.dust[ghostDust2].noGravity = true;
			Dust obj2 = Main.dust[ghostDust2];
			obj2.velocity *= 5f;
			ghostDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[ghostDust2];
			obj3.velocity *= 2f;
		}
	}
}
