using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Ravager;

[HasPierceResist(false)]
public class RavagerClawLeft : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.RavagerBody.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 75;
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = -1;
		base.NPC.width = 80;
		base.NPC.height = 40;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.15f);
		base.NPC.lifeMax = 12500;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.noGravity = true;
		base.NPC.alpha = 255;
		base.NPC.netAlways = true;
		base.NPC.HitSound = RavagerBody.HitSound;
		base.NPC.DeathSound = RavagerBody.LimbLossSound;
		if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
		{
			base.NPC.damage = (int)((double)base.NPC.damage * 1.5);
			base.NPC.defense *= 2;
			base.NPC.lifeMax *= 4;
		}
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 26000;
		}
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void AI()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			base.NPC.ai[1] = -90f;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.noTileCollide = true;
			Vector2 npcCenter = default(Vector2);
			((Vector2)(ref npcCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float ravBodyXDist = Main.npc[CalamityGlobalNPC.scavenger].Center.X - npcCenter.X;
			float ravBodyYDist = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - npcCenter.Y;
			ravBodyXDist -= 120f;
			ravBodyYDist += 50f;
			float ravBodyDistance = (float)Math.Sqrt(ravBodyXDist * ravBodyXDist + ravBodyYDist * ravBodyYDist);
			if (ravBodyDistance < 48f)
			{
				base.NPC.rotation = 0f;
				base.NPC.Center = Main.npc[CalamityGlobalNPC.scavenger].Center + new Vector2(-120f, 50f);
				base.NPC.ai[1]++;
				if (base.NPC.life < base.NPC.lifeMax / 2)
				{
					base.NPC.ai[1]++;
				}
				if (base.NPC.life < base.NPC.lifeMax / 3)
				{
					base.NPC.ai[1]++;
				}
				if (base.NPC.life < base.NPC.lifeMax / 5)
				{
					base.NPC.ai[1] += 2f;
				}
				if (base.NPC.ai[1] >= 60f)
				{
					if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
					{
						base.NPC.TargetClosest();
					}
					if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
					{
						base.NPC.TargetClosest();
					}
					if (base.NPC.Center.X + 100f > Main.player[base.NPC.target].Center.X)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[0] = 1f;
					}
					else
					{
						base.NPC.ai[1] = 0f;
					}
				}
			}
			else
			{
				ravBodyDistance = 36f / ravBodyDistance;
				base.NPC.velocity.X = ravBodyXDist * ravBodyDistance;
				base.NPC.velocity.Y = ravBodyYDist * ravBodyDistance;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = base.NPC.defDamage;
			SoundEngine.PlaySound(in RavagerBody.FistSound, base.NPC.Center);
			base.NPC.noTileCollide = true;
			base.NPC.collideX = false;
			base.NPC.collideY = false;
			float clawSpeed = 12f;
			if (base.NPC.life < base.NPC.lifeMax / 2)
			{
				clawSpeed += 2f;
			}
			if (base.NPC.life < base.NPC.lifeMax / 3)
			{
				clawSpeed += 2f;
			}
			if (base.NPC.life < base.NPC.lifeMax / 5)
			{
				clawSpeed += 4f;
			}
			Vector2 npcCenterAttack = default(Vector2);
			((Vector2)(ref npcCenterAttack))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float targetX = Main.player[base.NPC.target].Center.X - npcCenterAttack.X;
			float targetY = Main.player[base.NPC.target].Center.Y - npcCenterAttack.Y;
			float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			targetDistance = clawSpeed / targetDistance;
			base.NPC.velocity.X = targetX * targetDistance;
			base.NPC.velocity.Y = targetY * targetDistance;
			base.NPC.ai[0] = 2f;
			base.NPC.rotation = (float)Math.Atan2(0f - base.NPC.velocity.Y, 0f - base.NPC.velocity.X);
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
			{
				if (base.NPC.velocity.X > 0f && base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
				{
					base.NPC.noTileCollide = false;
				}
				if (base.NPC.velocity.X < 0f && base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
				{
					base.NPC.noTileCollide = false;
				}
			}
			else
			{
				if (base.NPC.velocity.Y > 0f && base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.noTileCollide = false;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.noTileCollide = false;
				}
			}
			Vector2 npcCenterRetract = default(Vector2);
			((Vector2)(ref npcCenterRetract))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float num = Main.npc[CalamityGlobalNPC.scavenger].Center.X - npcCenterRetract.X;
			float bodyReturnYDist = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - npcCenterRetract.Y;
			float num2 = num + Main.npc[CalamityGlobalNPC.scavenger].velocity.X;
			bodyReturnYDist += Main.npc[CalamityGlobalNPC.scavenger].velocity.Y;
			bodyReturnYDist += 40f;
			float num3 = num2 - 110f;
			if (((float)Math.Sqrt(num3 * num3 + bodyReturnYDist * bodyReturnYDist) > 700f || base.NPC.collideX || base.NPC.collideY) | base.NPC.justHit)
			{
				base.NPC.damage = 0;
				base.NPC.noTileCollide = true;
				base.NPC.ai[0] = 0f;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 3f)
			{
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.noTileCollide = true;
			float velocityMult = 0.4f;
			Vector2 clawCenter = default(Vector2);
			((Vector2)(ref clawCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float playerX = Main.player[base.NPC.target].Center.X - clawCenter.X;
			float playerY = Main.player[base.NPC.target].Center.Y - clawCenter.Y;
			float playerDist = (float)Math.Sqrt(playerX * playerX + playerY * playerY);
			playerDist = 12f / playerDist;
			playerX *= playerDist;
			playerY *= playerDist;
			if (base.NPC.velocity.X < playerX)
			{
				base.NPC.velocity.X += velocityMult;
				if (base.NPC.velocity.X < 0f && playerX > 0f)
				{
					base.NPC.velocity.X += velocityMult * 2f;
				}
			}
			else if (base.NPC.velocity.X > playerX)
			{
				base.NPC.velocity.X -= velocityMult;
				if (base.NPC.velocity.X > 0f && playerX < 0f)
				{
					base.NPC.velocity.X -= velocityMult * 2f;
				}
			}
			if (base.NPC.velocity.Y < playerY)
			{
				base.NPC.velocity.Y += velocityMult;
				if (base.NPC.velocity.Y < 0f && playerY > 0f)
				{
					base.NPC.velocity.Y += velocityMult * 2f;
				}
			}
			else if (base.NPC.velocity.Y > playerY)
			{
				base.NPC.velocity.Y -= velocityMult;
				if (base.NPC.velocity.Y > 0f && playerY < 0f)
				{
					base.NPC.velocity.Y -= velocityMult * 2f;
				}
			}
			base.NPC.rotation = (float)Math.Atan2(0f - base.NPC.velocity.Y, 0f - base.NPC.velocity.X);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		Vector2 center = default(Vector2);
		((Vector2)(ref center))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
		float drawPositionX = Main.npc[CalamityGlobalNPC.scavenger].Center.X - center.X;
		float drawPositionY = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - center.Y;
		drawPositionY += 12f;
		drawPositionX -= 28f;
		float rotation = (float)Math.Atan2(drawPositionY, drawPositionX) - (float)Math.PI / 2f;
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
			drawPositionX = Main.npc[CalamityGlobalNPC.scavenger].Center.X - center.X;
			drawPositionY = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - center.Y;
			drawPositionY += 12f;
			drawPositionX -= 28f;
			Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
			spriteBatch.Draw(RavagerBody.ChainTexture.Value, new Vector2(center.X - screenPos.X, center.Y - screenPos.Y), (Rectangle?)new Rectangle(0, 0, RavagerBody.ChainTexture.Value.Width, RavagerBody.ChainTexture.Value.Height), color, rotation, new Vector2((float)RavagerBody.ChainTexture.Value.Width * 0.5f, (float)RavagerBody.ChainTexture.Value.Height * 0.5f), 1f, (SpriteEffects)0, 0f);
		}
		return true;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (DownedBossSystem.downedProvidence)
			{
				target.AddBuff(ModContent.BuffType<Laceration>(), 240);
			}
			else
			{
				target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 240);
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			for (int dustCounter = 0; (double)dustCounter < (double)(hit.Damage / base.NPC.lifeMax) * 100.0; dustCounter++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
		else if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerClawLeft").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerClawLeft2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScavengerClawLeft3").Type);
		}
	}
}
