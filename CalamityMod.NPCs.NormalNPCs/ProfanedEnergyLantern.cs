using System;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class ProfanedEnergyLantern : ModNPC
{
	public static Asset<Texture2D> ChainTexture;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.ProfanedEnergyBody.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 6;
		if (!Main.dedServ)
		{
			ChainTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/NormalNPCs/ProfanedEnergySegment", (AssetRequestMode)2);
		}
		NPCID.Sets.PositiveNPCTypesExcludedFromDeathTally[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 1f;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.lifeMax = 1;
		base.AIType = -1;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.Banner = ModContent.NPCType<ProfanedEnergyBody>();
		base.BannerItem = ModContent.ItemType<ProfanedEnergyBanner>();
	}

	public override void AI()
	{
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.energyFlame < 0 || !Main.npc[CalamityGlobalNPC.energyFlame].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		int energyBase = CalamityGlobalNPC.energyFlame;
		if (base.NPC.ai[3] > 0f)
		{
			energyBase = (int)base.NPC.ai[3] - 1;
		}
		if (Main.netMode != 1)
		{
			base.NPC.localAI[0]--;
			if (base.NPC.localAI[0] <= 0f)
			{
				base.NPC.localAI[0] = Main.rand.Next(120, 480);
				base.NPC.ai[0] = Main.rand.Next(-100, 101);
				base.NPC.ai[1] = Main.rand.Next(-100, 101);
				base.NPC.netUpdate = true;
			}
		}
		base.NPC.TargetClosest();
		float lanternAcceleration = 0.1f;
		float lanternSpeed = 500f;
		if ((double)Main.npc[CalamityGlobalNPC.energyFlame].life < (double)Main.npc[CalamityGlobalNPC.energyFlame].lifeMax * 0.25)
		{
			lanternSpeed += 50f;
		}
		if ((double)Main.npc[CalamityGlobalNPC.energyFlame].life < (double)Main.npc[CalamityGlobalNPC.energyFlame].lifeMax * 0.1)
		{
			lanternSpeed += 50f;
		}
		if (Main.expertMode)
		{
			float expertSpeedMult = 1f - (float)base.NPC.life / (float)base.NPC.lifeMax;
			lanternSpeed += expertSpeedMult * 200f;
			lanternAcceleration += 0.15f;
		}
		if (!Main.npc[energyBase].active || CalamityGlobalNPC.energyFlame < 0)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		Vector2 lanternRelocation = default(Vector2);
		((Vector2)(ref lanternRelocation))._002Ector(base.NPC.ai[0] * 16f + 8f, base.NPC.ai[1] * 16f + 8f);
		float targetXDist = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - (float)(base.NPC.width / 2) - lanternRelocation.X;
		float targetYDist = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - (float)(base.NPC.height / 2) - lanternRelocation.Y;
		float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
		float baseXPos = Main.npc[energyBase].position.X + (float)(Main.npc[energyBase].width / 2);
		float baseYPos = Main.npc[energyBase].position.Y + (float)(Main.npc[energyBase].height / 2);
		Vector2 baseCurrentPos = default(Vector2);
		((Vector2)(ref baseCurrentPos))._002Ector(baseXPos, baseYPos);
		float basePosition = baseXPos + base.NPC.ai[0];
		float num = baseYPos + base.NPC.ai[1];
		float baseXDist = basePosition - baseCurrentPos.X;
		float baseYDist = num - baseCurrentPos.Y;
		float baseDistance = (float)Math.Sqrt(baseXDist * baseXDist + baseYDist * baseYDist);
		baseDistance = lanternSpeed / baseDistance;
		baseXDist *= baseDistance;
		baseYDist *= baseDistance;
		if (base.NPC.position.X < baseXPos + baseXDist)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + lanternAcceleration;
			if (base.NPC.velocity.X < 0f && baseXDist > 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
		}
		else if (base.NPC.position.X > baseXPos + baseXDist)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - lanternAcceleration;
			if (base.NPC.velocity.X > 0f && baseXDist < 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
		}
		if (base.NPC.position.Y < baseYPos + baseYDist)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + lanternAcceleration;
			if (base.NPC.velocity.Y < 0f && baseYDist > 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.9f;
			}
		}
		else if (base.NPC.position.Y > baseYPos + baseYDist)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - lanternAcceleration;
			if (base.NPC.velocity.Y > 0f && baseYDist < 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y * 0.9f;
			}
		}
		if (base.NPC.velocity.X > 8f)
		{
			base.NPC.velocity.X = 8f;
		}
		if (base.NPC.velocity.X < -8f)
		{
			base.NPC.velocity.X = -8f;
		}
		if (base.NPC.velocity.Y > 8f)
		{
			base.NPC.velocity.Y = 8f;
		}
		if (base.NPC.velocity.Y < -8f)
		{
			base.NPC.velocity.Y = -8f;
		}
		if (Main.netMode == 1 || Main.player[base.NPC.target].dead)
		{
			return;
		}
		base.NPC.localAI[1] += Main.rand.Next(1, 6);
		if (!(base.NPC.localAI[1] >= 600f))
		{
			return;
		}
		if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
		{
			((Vector2)(ref lanternRelocation))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			targetXDist = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - lanternRelocation.X + (float)Main.rand.Next(-10, 11);
			float absoluteTargetX = Math.Abs(targetXDist * 0.1f);
			if (targetYDist > 0f)
			{
				absoluteTargetX = 0f;
			}
			targetYDist = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - lanternRelocation.Y + (float)Main.rand.Next(-10, 11) - absoluteTargetX;
			targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
			targetDistance = 11f / targetDistance;
			targetXDist *= targetDistance;
			targetYDist *= targetDistance;
			int damage = (Main.masterMode ? 25 : (Main.expertMode ? 30 : 40));
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, targetXDist, targetYDist, ModContent.ProjectileType<HolyBomb>(), damage, 0f, Main.myPlayer);
			base.NPC.localAI[1] = 0f;
		}
		else
		{
			base.NPC.localAI[1] = 250f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.energyFlame != -1)
		{
			Vector2 center = default(Vector2);
			((Vector2)(ref center))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float drawPositionX = Main.npc[CalamityGlobalNPC.energyFlame].Center.X - center.X;
			float drawPositionY = Main.npc[CalamityGlobalNPC.energyFlame].Center.Y - center.Y;
			drawPositionY += 10f;
			float rotation = (float)Math.Atan2(drawPositionY, drawPositionX) - 1.57f;
			bool draw = true;
			while (draw)
			{
				float totalDrawDistance = (float)Math.Sqrt(drawPositionX * drawPositionX + drawPositionY * drawPositionY);
				if (totalDrawDistance < 16f)
				{
					draw = false;
					continue;
				}
				totalDrawDistance = 16f / totalDrawDistance;
				drawPositionX *= totalDrawDistance;
				drawPositionY *= totalDrawDistance;
				center.X += drawPositionX;
				center.Y += drawPositionY;
				drawPositionX = Main.npc[CalamityGlobalNPC.energyFlame].Center.X - center.X;
				drawPositionY = Main.npc[CalamityGlobalNPC.energyFlame].Center.Y - center.Y;
				drawPositionY -= 10f;
				Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
				Texture2D chain = ChainTexture.Value;
				Main.spriteBatch.Draw(chain, new Vector2(center.X - screenPos.X, center.Y - screenPos.Y), (Rectangle?)new Rectangle(0, 0, chain.Width, chain.Height), color, rotation, new Vector2((float)chain.Width * 0.5f, (float)chain.Height * 0.5f), 1f, (SpriteEffects)0, 0f);
			}
		}
		return true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 50; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
