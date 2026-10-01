using System;
using System.IO;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class IceClasper : ModNPC
{
	public enum IceClasperAIState
	{
		Shooting,
		Dashing
	}

	public bool expert = Main.expertMode;

	public bool revenge = CalamityWorld.revenge;

	public bool death = CalamityWorld.death;

	public bool checkedRotationDir;

	public int rotationDir;

	public float MaxVelocity = 10f;

	public float DistanceFromPlayer = 500f;

	public float AmountOfProjectiles = (CalamityWorld.death ? 2f : (CalamityWorld.revenge ? 4f : (Main.expertMode ? 3f : 3f)));

	public float TimeBetweenProjectiles = (CalamityWorld.death ? 50f : (CalamityWorld.revenge ? 35f : (Main.expertMode ? 40f : 45f)));

	public float TimeBetweenBurst = (CalamityWorld.death ? 240f : 180f);

	public float ProjectileSpeed = 8f;

	public float TimeBeforeDash = (CalamityWorld.revenge ? 100f : 120f);

	public float TimeDashing = 100f;

	public float DashSpeed = 6f;

	public Player player => Main.player[base.NPC.target];

	public IceClasperAIState CurrentState
	{
		get
		{
			return (IceClasperAIState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public ref float RotationIncrease => ref base.NPC.ai[1];

	public ref float TimerForShooting => ref base.NPC.ai[2];

	public ref float AITimer => ref base.NPC.ai[3];

	public bool isDashing
	{
		get
		{
			if (CurrentState == IceClasperAIState.Dashing && AITimer > TimeBeforeDash)
			{
				return AITimer <= TimeBeforeDash + TimeDashing;
			}
			return false;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Rotation = MathHelper.ToRadians(135f);
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.TrailingMode[base.Type] = 0;
		NPCID.Sets.TrailCacheLength[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 3f;
		base.NPC.noGravity = true;
		base.NPC.damage = 32;
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.defense = 12;
		base.NPC.lifeMax = 500;
		base.NPC.knockBackResist = 0.25f;
		base.NPC.noTileCollide = true;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath7;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<IceClasperBanner>();
		base.NPC.coldDamage = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.IceClasper")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(rotationDir);
		writer.Write(checkedRotationDir);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		rotationDir = reader.ReadInt32();
		checkedRotationDir = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || player.dead || !player.active)
		{
			base.NPC.TargetClosest();
		}
		AIMovement(player);
		float distToTarget = base.NPC.Distance(player.Center) + 0.1f;
		base.NPC.rotation = base.NPC.rotation.AngleTowards(base.NPC.AngleTo(player.Center), isDashing ? ((death ? 0.0005f : (revenge ? 0.0003f : (expert ? 0.0002f : 0.0001f))) * distToTarget) : 0.3f);
		Vector2 center = base.NPC.Center;
		Color cyan = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref cyan)).ToVector3());
		switch (CurrentState)
		{
		case IceClasperAIState.Shooting:
			State_Shooting(player);
			break;
		case IceClasperAIState.Dashing:
			State_Dashing(player);
			break;
		}
	}

	public void AIMovement(Player player)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (!checkedRotationDir)
		{
			rotationDir = Main.rand.NextBool().ToDirectionInt();
			checkedRotationDir = true;
			base.NPC.netUpdate = true;
		}
		Vector2 shootingPos = player.Center + new Vector2(MathF.Cos(RotationIncrease) * (float)rotationDir, MathF.Sin(RotationIncrease) * (float)rotationDir) * DistanceFromPlayer;
		RotationIncrease += ((CurrentState == IceClasperAIState.Shooting) ? 0.02f : 0.008f);
		base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, (shootingPos - base.NPC.Center).SafeNormalize(Vector2.Zero) * 6f, 0.1f);
		base.NPC.velocity = Vector2.Clamp(base.NPC.velocity, new Vector2(0f - MaxVelocity, 0f - MaxVelocity), new Vector2(MaxVelocity, MaxVelocity));
		base.NPC.netUpdate = true;
	}

	public void State_Shooting(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.Distance(player.Center) > 800f)
		{
			return;
		}
		AITimer++;
		if (AITimer >= TimeBetweenBurst)
		{
			if (TimerForShooting % TimeBetweenProjectiles == 0f)
			{
				Vector2 vecToPlayer = base.NPC.SafeDirectionTo(player.Center);
				Vector2 projVelocity = vecToPlayer * ProjectileSpeed;
				int type = ModContent.ProjectileType<IceClasperEnemyProjectile>();
				int damage = (Main.masterMode ? 15 : (Main.expertMode ? 18 : 24));
				if (Main.netMode != 1)
				{
					if (death)
					{
						for (int i = -16; i < 8; i += 8)
						{
							Vector2 spreadVelocity = projVelocity.RotatedBy(MathHelper.ToRadians((float)i));
							int projectile = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projVelocity.SafeNormalize(Vector2.Zero) * 10f, spreadVelocity, type, damage, 0f, Main.myPlayer);
							Main.projectile[projectile].timeLeft = 300;
						}
						base.NPC.netUpdate = true;
					}
					else
					{
						int projectile2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projVelocity.SafeNormalize(Vector2.Zero) * 10f, projVelocity, type, damage, 0f, Main.myPlayer);
						Main.projectile[projectile2].timeLeft = 300;
						base.NPC.netUpdate = true;
					}
				}
				NPC nPC = base.NPC;
				nPC.velocity -= vecToPlayer * 3f;
				SoundEngine.PlaySound(in SoundID.Item28, base.NPC.Center);
				base.NPC.netUpdate = true;
			}
			TimerForShooting++;
			if (TimerForShooting >= TimeBetweenProjectiles * AmountOfProjectiles)
			{
				TimerForShooting = 0f;
				AITimer = 0f;
				CurrentState = IceClasperAIState.Dashing;
				base.NPC.netUpdate = true;
			}
		}
		else if (AITimer >= TimeBetweenBurst / 2f && AITimer < TimeBetweenBurst)
		{
			Vector2 randPos = Main.rand.NextVector2CircularEdge(100f, 100f);
			Dust.NewDustPerfect(base.NPC.Center + randPos, 172, base.NPC.DirectionFrom(base.NPC.Center + base.NPC.velocity + randPos) * Main.rand.NextFloat(5f, 7f), 0, default(Color), 1.5f).noGravity = true;
			base.NPC.netUpdate = true;
		}
	}

	public void State_Dashing(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		float distToTarget = base.NPC.Distance(player.Center) + 0.1f;
		AITimer++;
		if (AITimer <= TimeBeforeDash)
		{
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, -base.NPC.rotation.ToRotationVector2() * 2f, 0.1f);
			base.NPC.netUpdate = true;
			return;
		}
		if (AITimer > TimeBeforeDash && AITimer <= TimeBeforeDash + TimeDashing)
		{
			base.NPC.velocity = base.NPC.rotation.ToRotationVector2() * (DashSpeed + 2f / (distToTarget * 0.1f));
			base.NPC.netUpdate = true;
			return;
		}
		AITimer = 0f;
		checkedRotationDir = false;
		CurrentState = IceClasperAIState.Shooting;
		base.NPC.netUpdate = true;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (isDashing ? 0.4f : 0.15f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.ZoneSnow || spawnInfo.Player.PillarZone() || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.InSunkenSea() || !Main.hardMode || spawnInfo.PlayerInTown || spawnInfo.Player.ZoneOldOneArmy || Main.snowMoon || Main.pumpkinMoon)
		{
			return 0f;
		}
		return 0.02f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(44, 240);
			target.AddBuff(46, 120);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 92, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 92, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("IceClasper").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("IceClasper2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("IceClasper3").Type);
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return isDashing;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EssenceofEleum>());
		npcLoot.Add(ModContent.ItemType<FrostBarrier>(), 5);
		npcLoot.Add(ModContent.ItemType<AncientIceChunk>(), 3);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 position = base.NPC.Center - screenPos;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		position -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		position += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		float interpolant = ((AITimer > TimeBeforeDash && AITimer <= TimeBeforeDash + TimeDashing) ? (1f - (AITimer - TimeBeforeDash) / TimeDashing) : (MathHelper.Clamp(AITimer, 0f, TimeBeforeDash) / TimeBeforeDash));
		float AfterimageFade = MathHelper.Lerp(0f, 1f, interpolant);
		if (CurrentState == IceClasperAIState.Dashing && CalamityClientConfig.Instance.Afterimages)
		{
			Color val = default(Color);
			for (int i = 0; i < base.NPC.oldPos.Length; i++)
			{
				((Color)(ref val))._002Ector(0.79f, 0.94f, 0.98f);
				((Color)(ref val)).A = 125;
				Color afterimageDrawColor = val * base.NPC.Opacity * (1f - (float)i / (float)base.NPC.oldPos.Length) * AfterimageFade;
				Vector2 afterimageDrawPosition = base.NPC.oldPos[i] + base.NPC.Size * 0.5f - screenPos;
				spriteBatch.Draw(texture, afterimageDrawPosition, (Rectangle?)base.NPC.frame, afterimageDrawColor, base.NPC.rotation - (float)Math.PI / 2f, origin, base.NPC.scale, (SpriteEffects)0, 0f);
			}
		}
		spriteBatch.Draw(texture, position, (Rectangle?)base.NPC.frame, drawColor, base.NPC.rotation - (float)Math.PI / 2f, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
