using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.HiveMind;

public class HiveBlob : ModNPC
{
	private const float NormalShootGate = 240f;

	private const float FastShootGate = 180f;

	private const float TelegraphDuration = 120f;

	public static int VileClotDamage = 8;

	public static int CursedFlameDamage = 15;

	private bool FastVariant => base.NPC.ai[2] > 0f;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0.1f;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.width = 25;
		base.NPC.height = 25;
		base.NPC.lifeMax = 50;
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 1300;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 2;
		}
		base.NPC.knockBackResist = 0.9f;
		base.AIType = -1;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.HiveBlob")
		});
	}

	public override void AI()
	{
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool getFuckedAI = Main.zenithWorld;
		int hiveMind = CalamityGlobalNPC.hiveMind;
		if (hiveMind < 0 || !Main.npc[hiveMind].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		if ((float)Main.npc[hiveMind].life / (float)Main.npc[hiveMind].lifeMax < 0.8f)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		base.NPC.alpha = Main.npc[hiveMind].alpha;
		if (base.NPC.ai[3] > 0f)
		{
			hiveMind = (int)base.NPC.ai[3] - 1;
		}
		if (Main.netMode != 1)
		{
			base.NPC.localAI[0] -= (getFuckedAI ? 10f : 1f);
			float RandomPositionMultiplier = (getFuckedAI ? 4f : 1f);
			if (base.NPC.localAI[0] <= 0f)
			{
				base.NPC.localAI[0] = Main.rand.Next(180, 361);
				base.NPC.ai[0] = (float)Main.rand.Next(-100, 101) * RandomPositionMultiplier;
				base.NPC.ai[1] = (float)Main.rand.Next(-100, 101) * RandomPositionMultiplier;
				base.NPC.netUpdate = true;
			}
		}
		float relocateSpeed = (getFuckedAI ? 1.2f : (death ? 0.8f : (revenge ? 0.7f : (expertMode ? 0.6f : 0.5f))));
		float acceleration = 0.8f;
		float distanceFromMind = (FastVariant ? 96f : 128f);
		if (Main.getGoodWorld)
		{
			distanceFromMind *= 2f;
		}
		float hiveMindX = Main.npc[hiveMind].Center.X;
		float hiveMindY = Main.npc[hiveMind].Center.Y;
		Vector2 hiveMindPos = default(Vector2);
		((Vector2)(ref hiveMindPos))._002Ector(hiveMindX, hiveMindY);
		float randomPosX = hiveMindX + base.NPC.ai[0];
		float num = hiveMindY + base.NPC.ai[1];
		float finalRandPosX = randomPosX - hiveMindPos.X;
		float finalRandPosY = num - hiveMindPos.Y;
		float finalRandDistance = (float)Math.Sqrt(finalRandPosX * finalRandPosX + finalRandPosY * finalRandPosY);
		finalRandDistance = distanceFromMind / finalRandDistance;
		finalRandPosX *= finalRandDistance;
		finalRandPosY *= finalRandDistance;
		if (base.NPC.position.X < hiveMindX + finalRandPosX)
		{
			base.NPC.velocity.X += relocateSpeed;
			if (base.NPC.velocity.X < 0f && finalRandPosX > 0f)
			{
				base.NPC.velocity.X *= acceleration;
			}
		}
		else if (base.NPC.position.X > hiveMindX + finalRandPosX)
		{
			base.NPC.velocity.X -= relocateSpeed;
			if (base.NPC.velocity.X > 0f && finalRandPosX < 0f)
			{
				base.NPC.velocity.X *= acceleration;
			}
		}
		if (base.NPC.position.Y < hiveMindY + finalRandPosY)
		{
			base.NPC.velocity.Y += relocateSpeed;
			if (base.NPC.velocity.Y < 0f && finalRandPosY > 0f)
			{
				base.NPC.velocity.Y *= acceleration;
			}
		}
		else if (base.NPC.position.Y > hiveMindY + finalRandPosY)
		{
			base.NPC.velocity.Y -= relocateSpeed;
			if (base.NPC.velocity.Y > 0f && finalRandPosY < 0f)
			{
				base.NPC.velocity.Y *= acceleration;
			}
		}
		float velocityLimit = relocateSpeed * 16f;
		if (base.NPC.velocity.X > velocityLimit)
		{
			base.NPC.velocity.X = velocityLimit;
		}
		if (base.NPC.velocity.X < 0f - velocityLimit)
		{
			base.NPC.velocity.X = 0f - velocityLimit;
		}
		if (base.NPC.velocity.Y > velocityLimit)
		{
			base.NPC.velocity.Y = velocityLimit;
		}
		if (base.NPC.velocity.Y < 0f - velocityLimit)
		{
			base.NPC.velocity.Y = 0f - velocityLimit;
		}
		if (Main.netMode == 1)
		{
			return;
		}
		float shootGateValue = (FastVariant ? 180f : 240f);
		if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[Main.npc[hiveMind].target].position, Main.player[Main.npc[hiveMind].target].width, Main.player[Main.npc[hiveMind].target].height))
		{
			base.NPC.localAI[1] = shootGateValue * 0.5f;
		}
		if (base.NPC.localAI[1] < shootGateValue)
		{
			base.NPC.localAI[1]++;
			if (base.NPC.localAI[1] < shootGateValue - 120f)
			{
				base.NPC.localAI[1] += Main.rand.Next(2);
			}
			if (death)
			{
				base.NPC.localAI[1]++;
			}
		}
		if (base.NPC.alpha > 0 || !(base.NPC.localAI[1] >= shootGateValue) || !(Vector2.Distance(Main.player[Main.npc[hiveMind].target].Center, base.NPC.Center) > 80f))
		{
			return;
		}
		base.NPC.localAI[1] = 0f;
		if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[Main.npc[hiveMind].target].position, Main.player[Main.npc[hiveMind].target].width, Main.player[Main.npc[hiveMind].target].height))
		{
			float projSpeed = (death ? 8f : (revenge ? 7f : (expertMode ? 6f : 4f)));
			if (Main.getGoodWorld)
			{
				projSpeed *= 1.5f;
			}
			Vector2 projDirection = base.NPC.Center;
			float playerX = Main.player[Main.npc[hiveMind].target].Center.X - projDirection.X;
			float playerY = Main.player[Main.npc[hiveMind].target].Center.Y - projDirection.Y;
			float playerDist = (float)Math.Sqrt(playerX * playerX + playerY * playerY);
			playerDist = projSpeed / playerDist;
			playerX *= playerDist;
			playerY *= playerDist;
			int type = ((Main.getGoodWorld && Main.rand.NextBool(5)) ? 96 : ModContent.ProjectileType<VileClot>());
			int damage = ((type == 96) ? CursedFlameDamage : VileClotDamage);
			Vector2 projectileVelocity = default(Vector2);
			((Vector2)(ref projectileVelocity))._002Ector(playerX, playerY);
			if (type == 96)
			{
				projectileVelocity = (Main.player[Main.npc[hiveMind].target].Center - base.NPC.Center - Main.player[Main.npc[hiveMind].target].velocity * 20f).SafeNormalize(Vector2.UnitY) * projSpeed;
			}
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projDirection, projectileVelocity, type, damage, 0f, Main.myPlayer);
			base.NPC.netUpdate = true;
		}
	}

	public override bool CanHitNPC(NPC target)
	{
		return base.NPC.alpha == 0;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 vector2 = base.NPC.Center - screenPos;
		vector2 -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
		vector2 += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color color = base.NPC.GetAlpha(drawColor);
		float shootGateValue = (FastVariant ? 180f : 240f);
		if (base.NPC.localAI[1] > shootGateValue - 120f)
		{
			color = Color.Lerp(color, Color.LimeGreen * base.NPC.Opacity, MathHelper.Clamp((base.NPC.localAI[1] - (shootGateValue - 120f)) / 120f, 0f, 1f));
		}
		spriteBatch.Draw(texture, vector2, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, vector, base.NPC.scale, spriteEffects, 0f);
		return false;
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

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
			}
		}
		if (Main.netMode != 1 && FastVariant && Main.zenithWorld)
		{
			for (int j = 1; j < 3; j++)
			{
				Vector2 spawnAt = base.NPC.Center + new Vector2(0f, (float)base.NPC.height / 2f);
				NPC.NewNPC(base.NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<HiveBlob>());
			}
		}
	}
}
