using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Systems.Collections;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AquaticScourge;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(AquaticScourgeHead))]
public class AquaticScourgeTail : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.AquaticScourgeHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 36;
		base.NPC.width = 32;
		base.NPC.height = 32;
		base.NPC.defense = 25;
		base.NPC.DR_NERD(0.15f);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.LifeMaxNERB(80000, 96000, 1000000);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		base.NPC.chaseable = false;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.15f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.1f;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 1.25f;
		}
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool getFuckedAI = Main.zenithWorld;
		bool nonHostile = calamityGlobalNPC.newAI[0] == 0f;
		if (base.NPC.justHit || (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.999 || BossRushEvent.BossRushActive || Main.zenithWorld)
		{
			if (nonHostile)
			{
				base.NPC.timeLeft *= 20;
				base.NPC.npcSlots = 16f;
				base.NPC.damage = base.NPC.defDamage;
				CalamityGlobalNPC.BossKillTimes.TryGetValue(base.NPC.type, out var revKillTime);
				calamityGlobalNPC.KillTime = revKillTime;
				calamityGlobalNPC.newAI[0] = 1f;
				nonHostile = false;
				base.NPC.chaseable = true;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			base.NPC.damage = 0;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[1]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[1]].life;
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (player.position.Y < 300f || (double)player.position.Y > Main.worldSurface * 16.0)
		{
			_ = 1;
		}
		else if (player.position.X > 7680f)
		{
			_ = player.position.X < (float)(Main.maxTilesX * 16 - 7680);
		}
		else
			_ = 0;
		if (Main.remixWorld)
		{
			if (player.position.Y < (float)Main.UnderworldLayer * 0.8f || player.position.Y > (float)Main.UnderworldLayer)
			{
				_ = 1;
			}
			else if (player.position.X > 7680f)
			{
				_ = player.position.X < (float)(Main.maxTilesX * 16 - 7680);
			}
			else
				_ = 0;
		}
		bool biomeEnraged = base.NPC.localAI[2] <= 0f;
		float enrageScale = 0f;
		if (biomeEnraged)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 2f;
		}
		bool immuneToSlowingDebuffs = getFuckedAI;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		bool shouldDespawn = true;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<AquaticScourgeHead>())
			{
				shouldDespawn = false;
				break;
			}
		}
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
		float maxDistance = ((calamityGlobalNPC.newAI[0] == 1f) ? 12800f : 6400f);
		if (player.dead || Vector2.Distance(base.NPC.Center, player.Center) > maxDistance || (nonHostile & biomeEnraged))
		{
			calamityGlobalNPC.newAI[1] = 1f;
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.velocity.Y += 2f;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y += 2f;
			}
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				for (int a = 0; a < Main.npc.Length; a++)
				{
					int type = Main.npc[a].type;
					if (CalamityNPCTypeSets.AquaticScourge.Contains(type))
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		else
		{
			calamityGlobalNPC.newAI[1] = 0f;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha < 128)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		Vector2 scourgePosition = base.NPC.Center;
		Vector2 predictionVector = (Main.getGoodWorld ? (Main.player[base.NPC.target].velocity * 20f) : Vector2.Zero);
		float scourgeTargetX = player.Center.X + predictionVector.X;
		float scourgeTargetY = player.Center.Y + predictionVector.Y;
		float scourgeMaxSpeed = 5f;
		if (calamityGlobalNPC.newAI[0] == 1f)
		{
			scourgeMaxSpeed = (revenge ? 14.4f : 12f);
			if (expertMode)
			{
				scourgeMaxSpeed += 2.4f * (1f - lifeRatio);
			}
			scourgeMaxSpeed += 3f * enrageScale;
			if (death | getFuckedAI)
			{
				scourgeMaxSpeed += 5f;
				scourgeMaxSpeed += Vector2.Distance(player.Center, base.NPC.Center) * 0.001f;
			}
			if (Main.getGoodWorld)
			{
				scourgeMaxSpeed *= 1.15f;
			}
		}
		scourgeTargetX = (int)(scourgeTargetX / 16f) * 16;
		scourgeTargetY = (int)(scourgeTargetY / 16f) * 16;
		scourgePosition.X = (int)(scourgePosition.X / 16f) * 16;
		scourgePosition.Y = (int)(scourgePosition.Y / 16f) * 16;
		scourgeTargetX -= scourgePosition.X;
		scourgeTargetY -= scourgePosition.Y;
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				scourgePosition = base.NPC.Center;
				scourgeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - scourgePosition.X;
				scourgeTargetY = Main.npc[(int)base.NPC.ai[1]].Center.Y - scourgePosition.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(scourgeTargetY, scourgeTargetX) + (float)Math.PI / 2f;
			float scourgeTargetDist = (float)Math.Sqrt(scourgeTargetX * scourgeTargetX + scourgeTargetY * scourgeTargetY);
			int scourgeWidth = base.NPC.width;
			scourgeTargetDist = (scourgeTargetDist - (float)scourgeWidth) / scourgeTargetDist;
			scourgeTargetX *= scourgeTargetDist;
			scourgeTargetY *= scourgeTargetDist;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X = base.NPC.position.X + scourgeTargetX;
			base.NPC.position.Y = base.NPC.position.Y + scourgeTargetY;
			if (scourgeTargetX < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (scourgeTargetX > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 scaledDraw = default(Vector2);
		((Vector2)(ref scaledDraw))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += scaledDraw * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color color = base.NPC.GetAlpha(drawColor);
		if (CalamityWorld.revenge || BossRushEvent.BossRushActive || Main.zenithWorld)
		{
			if (Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[3] > 300f)
			{
				color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp((Main.npc[(int)base.NPC.ai[2]].Calamity().newAI[3] - 300f) / 180f, 0f, 1f));
			}
			else if (Main.npc[(int)base.NPC.ai[2]].localAI[3] > 0f)
			{
				color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp(Main.npc[(int)base.NPC.ai[2]].localAI[3] / 90f, 0f, 1f));
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, scaledDraw, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion)
		{
			return base.NPC.Calamity().newAI[0] == 1f;
		}
		return null;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ASTail").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ASTail2").Type, base.NPC.scale);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 300);
		}
	}
}
