using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DesertScourge;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(DesertScourgeHead))]
public class DesertScourgeTail : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.DesertScourgeHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 14;
		base.NPC.width = 104;
		base.NPC.height = 104;
		base.NPC.defense = 9;
		base.NPC.LifeMaxNERB(4200, 5000, 1150000);
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.boss = true;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = DesertScourgeHead.HitSound;
		base.NPC.DeathSound = DesertScourgeHead.DeathSound;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.4f;
		}
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.alpha);
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.alpha = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (CalamityWorld.death)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		base.NPC.dontTakeDamage = Main.npc[(int)base.NPC.ai[1]].dontTakeDamage;
		base.NPC.canDisplayBuffs = Main.npc[(int)base.NPC.ai[1]].canDisplayBuffs;
		_ = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<DesertScourgeHead>());
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
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		else
		{
			base.NPC.alpha = Main.npc[(int)base.NPC.ai[1]].alpha;
			if (base.NPC.alpha != 255 && base.NPC.dontTakeDamage)
			{
				for (int dustIndex = 0; dustIndex < 2; dustIndex++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 85, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
		}
		Vector2 segmentTilePos = base.NPC.Center;
		float playerXPos = Main.player[base.NPC.target].Center.X;
		float playerYPos = Main.player[base.NPC.target].Center.Y;
		playerXPos = (int)(playerXPos / 16f) * 16;
		playerYPos = (int)(playerYPos / 16f) * 16;
		segmentTilePos.X = (int)(segmentTilePos.X / 16f) * 16;
		segmentTilePos.Y = (int)(segmentTilePos.Y / 16f) * 16;
		playerXPos -= segmentTilePos.X;
		playerYPos -= segmentTilePos.Y;
		float playerDistance = (float)Math.Sqrt(playerXPos * playerXPos + playerYPos * playerYPos);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentTilePos = base.NPC.Center;
				playerXPos = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentTilePos.X;
				playerYPos = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentTilePos.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(playerYPos, playerXPos) + (float)Math.PI / 2f;
			playerDistance = (float)Math.Sqrt(playerXPos * playerXPos + playerYPos * playerYPos);
			int segmentOffset = 70;
			playerDistance = (playerDistance - (float)segmentOffset) / playerDistance;
			playerXPos *= playerDistance;
			playerYPos *= playerDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + playerXPos;
			base.NPC.position.Y = base.NPC.position.Y + playerYPos;
			if (playerXPos < 0f)
			{
				base.NPC.spriteDirection = 1;
			}
			else if (playerXPos > 0f)
			{
				base.NPC.spriteDirection = -1;
			}
		}
		NPC head = Main.npc[(int)base.NPC.ai[2]];
		float burrowTimeGateValue = 600f;
		if (head.Calamity().newAI[0] >= burrowTimeGateValue)
		{
			_ = head.Calamity().newAI[1] == 1f;
		}
		else
			_ = 0;
		_ = head.Calamity().newAI[1];
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
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
		if (minDist <= 20f * base.NPC.scale)
		{
			return base.NPC.alpha <= 0;
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScourgeTail").Type, base.NPC.scale);
			}
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			return Color.MediumBlue * (float)(int)((Color)(ref drawColor)).A * base.NPC.Opacity;
		}
		return null;
	}
}
