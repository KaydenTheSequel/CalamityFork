using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Perforator;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(PerforatorHeadMedium))]
public class PerforatorBodyMedium : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfMediumHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfMediumDeath");

	public static Asset<Texture2D> GlowTexture;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.PerforatorHeadMedium.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 14;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 6;
		base.NPC.LifeMaxNERB(130, 170, 7000);
		if (Main.zenithWorld)
		{
			base.NPC.lifeMax *= 4;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
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
		base.NPC.Calamity().SplittingWorm = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void AI()
	{
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.NPC.realLife = -1;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
		}
		if (Main.netMode != 1)
		{
			if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.ai[2] > 0f)
				{
					base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.position.Y + (float)base.NPC.height), base.NPC.type, base.NPC.whoAmI);
				}
				else
				{
					base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.position.Y + (float)base.NPC.height), ModContent.NPCType<PerforatorTailMedium>(), base.NPC.whoAmI);
				}
				Main.npc[(int)base.NPC.ai[0]].ai[1] = base.NPC.whoAmI;
				Main.npc[(int)base.NPC.ai[0]].ai[2] = base.NPC.ai[2] - 1f;
				base.NPC.netUpdate = true;
			}
			bool spawnedBlob = false;
			if (!Main.npc[(int)base.NPC.ai[1]].active && !Main.npc[(int)base.NPC.ai[0]].active)
			{
				if (death)
				{
					spawnedBlob = true;
					int type = ModContent.ProjectileType<IchorBlob>();
					Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Main.rand.NextVector2CircularEdge(3f, 3f), type, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer, 0f, base.NPC.Center.Y);
				}
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				base.NPC.active = false;
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != base.NPC.aiStyle)
			{
				if (death && !spawnedBlob)
				{
					spawnedBlob = true;
					int type2 = ModContent.ProjectileType<IchorBlob>();
					Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Main.rand.NextVector2CircularEdge(3f, 3f), type2, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer, 0f, base.NPC.Center.Y);
				}
				base.NPC.type = ModContent.NPCType<PerforatorHeadMedium>();
				int whoAmI = base.NPC.whoAmI;
				float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
				float ai0 = base.NPC.ai[0];
				base.NPC.SetDefaultsKeepPlayerInteraction(base.NPC.type);
				base.NPC.life = (int)((float)base.NPC.lifeMax * lifeRatio);
				base.NPC.ai[0] = ai0;
				base.NPC.TargetClosest();
				base.NPC.ForceNetUpdate();
				base.NPC.whoAmI = whoAmI;
				base.NPC.alpha = 0;
			}
			if (!Main.npc[(int)base.NPC.ai[0]].active || Main.npc[(int)base.NPC.ai[0]].aiStyle != base.NPC.aiStyle)
			{
				if (death && !spawnedBlob)
				{
					int type3 = ModContent.ProjectileType<IchorBlob>();
					Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Main.rand.NextVector2CircularEdge(3f, 3f), type3, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer, 0f, base.NPC.Center.Y);
				}
				int whoAmI2 = base.NPC.whoAmI;
				float otherLifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
				float ai1 = base.NPC.ai[1];
				base.NPC.SetDefaultsKeepPlayerInteraction(base.NPC.type);
				base.NPC.life = (int)((float)base.NPC.lifeMax * otherLifeRatio);
				base.NPC.ai[1] = ai1;
				base.NPC.TargetClosest();
				base.NPC.ForceNetUpdate();
				base.NPC.whoAmI = whoAmI2;
				base.NPC.alpha = 0;
			}
			if (!base.NPC.active && Main.dedServ)
			{
				NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
			}
		}
		Vector2 segmentDirection = base.NPC.Center;
		float targetX = Main.player[base.NPC.target].Center.X;
		float targetY = Main.player[base.NPC.target].Center.Y;
		targetX = (int)(targetX / 16f) * 16;
		targetY = (int)(targetY / 16f) * 16;
		segmentDirection.X = (int)(segmentDirection.X / 16f) * 16;
		segmentDirection.Y = (int)(segmentDirection.Y / 16f) * 16;
		targetX -= segmentDirection.X;
		targetY -= segmentDirection.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentDirection = base.NPC.Center;
				targetX = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentDirection.X;
				targetY = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentDirection.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX) + (float)Math.PI / 2f;
			targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			int npcWidth = base.NPC.width;
			npcWidth = (int)((float)npcWidth * base.NPC.scale);
			targetDistance = (targetDistance - (float)npcWidth) / targetDistance;
			targetX *= targetDistance;
			targetY *= targetDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X += targetX;
			base.NPC.position.Y += targetY;
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha >= 85)
		{
			if (base.NPC.alpha > 0 && base.NPC.life > 0)
			{
				for (int dustIndex = 0; dustIndex < 2; dustIndex++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, Main.rand.NextBool() ? 170 : 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}
			Vector2 val = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val)).Length() > 2f)
			{
				base.NPC.alpha -= 42;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
		}
		else if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
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
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color glowmaskColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(4) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		if (Main.netMode != 1 && Main.zenithWorld)
		{
			int type = ModContent.ProjectileType<IchorBlob>();
			Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, Vector2.UnitY, type, PerforatorHive.IchorBlobDamage, 0f, Main.myPlayer);
			for (int i = -1; i < 2; i++)
			{
				int type2 = ModContent.ProjectileType<IchorShot>();
				Vector2 baseVelocity = Vector2.UnitY * Main.rand.NextFloat(-12.5f, -5f);
				int spread = Main.rand.Next(16, 36);
				Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center, baseVelocity.RotatedBy(MathHelper.ToRadians((float)(spread * i))), type2, PerforatorHive.IchorShotDamage, 0f, Main.myPlayer);
			}
		}
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
		for (int k = 0; k < 5; k++)
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MediumPerf2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("MediumPerf3").Type, base.NPC.scale);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(69, 240);
		}
	}
}
