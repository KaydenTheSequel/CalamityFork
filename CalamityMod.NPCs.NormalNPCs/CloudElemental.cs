using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class CloudElemental : ModNPC
{
	public enum AttackState
	{
		Hover,
		CloudTeleport,
		LightningSummon,
		TornadoSummon,
		LightningBladeSlice,
		NimbusSummon
	}

	public static Asset<Texture2D> AttackTexture;

	public Player Target => Main.player[base.NPC.target];

	public AttackState CurrentAttackState
	{
		get
		{
			return (AttackState)base.NPC.ai[0];
		}
		set
		{
			if (base.NPC.ai[0] != (float)value)
			{
				base.NPC.ai[0] = (float)value;
				base.NPC.netUpdate = true;
			}
		}
	}

	public bool Phase2 => (float)base.NPC.life < (float)base.NPC.lifeMax * 0.5f;

	public ref float AttackTimer => ref base.NPC.ai[1];

	public override void SetStaticDefaults()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Position = new Vector2(28f, 20f);
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 10f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 2f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			AttackTexture = ModContent.Request<Texture2D>(Texture + "Attack", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 3f;
		base.NPC.damage = 38;
		base.NPC.width = 80;
		base.NPC.height = 140;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 6000;
		base.NPC.knockBackResist = 0.05f;
		base.NPC.value = Item.buyPrice(0, 1, 50);
		base.NPC.HitSound = SoundID.NPCHit23;
		base.NPC.DeathSound = SoundID.NPCDeath39;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CloudElementalBanner>();
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = false;
		base.NPC.Calamity().VulnerableToWater = false;
		base.NPC.Calamity().VulnerableToHeat = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CloudElemental")
		});
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.375f, 0.5f, 0.625f);
		if (Target.dead || !Target.active || !Main.player.IndexInRange(base.NPC.target))
		{
			base.NPC.TargetClosest();
		}
		switch (CurrentAttackState)
		{
		case AttackState.Hover:
			DoBehavior_Hover();
			break;
		case AttackState.CloudTeleport:
			DoBehavior_CloudTeleport();
			break;
		case AttackState.LightningSummon:
			DoBehavior_LightningSummon();
			break;
		case AttackState.TornadoSummon:
			DoBehavior_TornadoSummon();
			break;
		case AttackState.LightningBladeSlice:
			DoBehavior_LightningBladeSlice();
			break;
		case AttackState.NimbusSummon:
			DoBehavior_NimbusSummon();
			break;
		}
		AttackTimer++;
	}

	public void DoBehavior_Hover()
	{
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		int hoverTime = (int)MathHelper.Lerp(330f, 180f, 1f - lifeRatio);
		float hoverAcceleration = MathHelper.Lerp(0.2f, 0.425f, 1f - lifeRatio);
		Vector2 hoverSpeed = default(Vector2);
		((Vector2)(ref hoverSpeed))._002Ector(8.5f, 4.5f);
		if (Main.rand.NextBool(8) && !Main.dedServ)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 16);
				dust.velocity = Main.rand.NextVector2CircularEdge(4f, 4f);
				dust.velocity.Y /= 3f;
				dust.scale = Main.rand.NextFloat(1.15f, 1.35f);
				dust.noGravity = true;
			}
		}
		if (AttackTimer < (float)(hoverTime - 30))
		{
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(Target.Center) * hoverSpeed;
			if (Math.Abs(base.NPC.Center.X - Target.Center.X) > 30f)
			{
				base.NPC.SimpleFlyMovement(idealVelocity, hoverAcceleration);
				base.NPC.spriteDirection = (base.NPC.velocity.X > 0f).ToDirectionInt();
			}
		}
		else
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.95f;
		}
		if (AttackTimer >= (float)hoverTime)
		{
			List<AttackState> potentialAttackStates = new List<AttackState>
			{
				AttackState.NimbusSummon,
				AttackState.TornadoSummon
			};
			if (Phase2)
			{
				potentialAttackStates.Add(AttackState.LightningSummon);
				potentialAttackStates.Add(AttackState.LightningBladeSlice);
			}
			if (NPC.CountNPCS(250) >= 10)
			{
				potentialAttackStates.Remove(AttackState.NimbusSummon);
			}
			if (Main.rand.NextBool(3))
			{
				CurrentAttackState = AttackState.CloudTeleport;
			}
			else
			{
				CurrentAttackState = Main.rand.Next(potentialAttackStates);
			}
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public void DoBehavior_CloudTeleport()
	{
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		int teleportFadeoutTime = 75;
		int teleportFadeinTime = 60;
		if (AttackTimer <= (float)teleportFadeoutTime)
		{
			float fadeoutCompletion = Utils.GetLerpValue(0f, teleportFadeoutTime, AttackTimer, clamped: true);
			float particleSpawnRate = MathHelper.Clamp(fadeoutCompletion + 0.6f, 0.5f, 1f);
			base.NPC.Opacity = MathHelper.Lerp(1f, 0f, fadeoutCompletion);
			if (Main.rand.NextFloat() < particleSpawnRate && !Main.dedServ)
			{
				Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 16);
				if (Main.rand.NextBool(15) && !Main.dedServ)
				{
					int smokeType = Utils.SelectRandom<int>(Main.rand, 825, 826, 827);
					Vector2 smokeVelocity = Main.rand.NextVector2CircularEdge(6f, 6f);
					Gore.NewGorePerfect(base.NPC.GetSource_FromAI(), base.NPC.Center + Main.rand.NextVector2Circular(40f, 40f), smokeVelocity, smokeType);
				}
			}
		}
		if (AttackTimer == (float)teleportFadeoutTime)
		{
			float teleportRadius = 420f;
			base.NPC.Center = Target.Center + Main.rand.NextVector2CircularEdge(teleportRadius, teleportRadius);
			base.NPC.netUpdate = true;
		}
		if (AttackTimer > (float)teleportFadeoutTime && AttackTimer <= (float)(teleportFadeoutTime + teleportFadeinTime))
		{
			float fadeinCompletion = Utils.GetLerpValue(teleportFadeoutTime, teleportFadeoutTime + teleportFadeinTime, AttackTimer, clamped: true);
			float particleSpawnRate2 = MathHelper.Clamp(fadeinCompletion + 0.6f, 0.5f, 1f);
			base.NPC.Opacity = fadeinCompletion;
			if (Main.rand.NextFloat() < particleSpawnRate2 && !Main.dedServ)
			{
				Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 16);
			}
		}
		if (AttackTimer >= (float)(teleportFadeoutTime + teleportFadeinTime))
		{
			CurrentAttackState = AttackState.Hover;
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public void DoBehavior_LightningSummon()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		int cloudSummonDelay = 60;
		int cloudSummonRate = 30;
		int totalCloudWavesToSummon = 5;
		int lightningDamage = (Main.masterMode ? 19 : (Main.expertMode ? 23 : 36));
		if (Phase2)
		{
			cloudSummonRate -= 5;
			totalCloudWavesToSummon += 2;
			lightningDamage += 2;
		}
		NPC nPC = base.NPC;
		nPC.velocity *= 0.96f;
		if (AttackTimer > (float)cloudSummonDelay && Main.netMode != 1 && (AttackTimer - (float)cloudSummonDelay) % (float)cloudSummonRate == (float)(cloudSummonRate - 1))
		{
			int projectileType = ModContent.ProjectileType<LightningCloud>();
			float cloudSpawnOutwardness = (AttackTimer - (float)cloudSummonDelay) / (float)cloudSummonRate * 50f;
			Vector2 spawnPosition = base.NPC.Top + new Vector2(cloudSpawnOutwardness, -36f);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition, Vector2.Zero, projectileType, lightningDamage, 0f, Main.myPlayer);
			spawnPosition = base.NPC.Top + new Vector2(0f - cloudSpawnOutwardness, -36f);
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition, Vector2.Zero, projectileType, lightningDamage, 0f, Main.myPlayer);
		}
		if (AttackTimer >= (float)(cloudSummonDelay + cloudSummonRate * totalCloudWavesToSummon))
		{
			CurrentAttackState = AttackState.Hover;
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public void DoBehavior_TornadoSummon()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		int tornadoSpawnDelay = 60;
		int totalTornadosToSummon = (Phase2 ? 8 : 5);
		NPC nPC = base.NPC;
		nPC.velocity *= 0.96f;
		if (Main.netMode != 1 && AttackTimer == (float)tornadoSpawnDelay)
		{
			int projectileType = ModContent.ProjectileType<StormMarkHostile>();
			for (int i = 0; i < totalTornadosToSummon; i++)
			{
				float angle = (float)Math.PI * 2f / (float)totalTornadosToSummon * (float)i;
				Vector2 spawnPosition = Target.Center + angle.ToRotationVector2() * 620f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition, Vector2.Zero, projectileType, 0, 0f, Main.myPlayer);
			}
		}
		if (AttackTimer >= (float)tornadoSpawnDelay + 180f)
		{
			CurrentAttackState = AttackState.Hover;
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public void DoBehavior_NimbusSummon()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		int nimbusSummonDelay = 45;
		int totalNimbiToSummon = 5;
		int nimbusSummonRate = 50;
		if (Phase2)
		{
			totalNimbiToSummon++;
			nimbusSummonRate -= 10;
		}
		if (AttackTimer < (float)nimbusSummonDelay)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.92f;
		}
		else if ((AttackTimer - (float)nimbusSummonDelay) % (float)nimbusSummonRate == (float)(nimbusSummonRate - 1))
		{
			Point spawnPosition = (base.NPC.Center + base.NPC.ai[2].ToRotationVector2() * 300f).ToPoint();
			if (Main.netMode != 1 && NPC.CountNPCS(250) < totalNimbiToSummon && !CalamityUtils.ParanoidTileRetrieval(spawnPosition.X, spawnPosition.Y).HasTile)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnPosition.X, spawnPosition.Y, 250);
			}
			if (!Main.dedServ)
			{
				for (int i = 0; i < 20; i++)
				{
					Dust.NewDustDirect(spawnPosition.ToVector2(), -20, 20, 16);
				}
				SoundEngine.PlaySound(in SoundID.Item122, spawnPosition.ToVector2());
			}
			base.NPC.ai[2] += (float)Math.PI * 2f / (float)totalNimbiToSummon;
		}
		if (AttackTimer >= (float)(nimbusSummonDelay + nimbusSummonRate * totalNimbiToSummon))
		{
			base.NPC.ai[2] = 0f;
			CurrentAttackState = AttackState.Hover;
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public void DoBehavior_LightningBladeSlice()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		int totalSlices = 4;
		int sliceChargeTime = 45;
		int sliceChargeDelay = 15;
		float sliceChargeSpeed = 22f;
		if (AttackTimer % (float)(sliceChargeTime + sliceChargeDelay) < (float)sliceChargeDelay)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.92f;
		}
		if (AttackTimer % (float)(sliceChargeTime + sliceChargeDelay) == (float)sliceChargeDelay)
		{
			base.NPC.damage = base.NPC.defDamage * 2;
			base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center) * sliceChargeSpeed;
			base.NPC.spriteDirection = (base.NPC.velocity.X > 0f).ToDirectionInt();
			base.NPC.netUpdate = true;
		}
		if (AttackTimer >= (float)((sliceChargeTime + sliceChargeDelay) * totalSlices))
		{
			base.NPC.damage = 0;
			CurrentAttackState = AttackState.Hover;
			AttackTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = AttackTexture.Value;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection != -1);
		if (CurrentAttackState != AttackState.Hover)
		{
			Main.EntitySpriteDraw(texture, base.NPC.Center - screenPos, base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction);
		}
		else
		{
			texture = TextureAssets.Npc[base.Type].Value;
			Main.EntitySpriteDraw(texture, base.NPC.Center - screenPos, base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction);
		}
		if (Main.zenithWorld)
		{
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Particles/WulfrumHat", (AssetRequestMode)2).Value;
			SpriteEffects hatdirection = (SpriteEffects)(base.NPC.spriteDirection != 1);
			int xoffset = ((base.NPC.direction == 1) ? 70 : 20);
			Vector2 offset = default(Vector2);
			((Vector2)(ref offset))._002Ector((float)xoffset, -10f);
			Main.EntitySpriteDraw(value, base.NPC.Center - screenPos + offset, null, base.NPC.GetAlpha(Color.LightBlue), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, hatdirection);
		}
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter = base.NPC.frameCounter + (double)MathHelper.Max(((Vector2)(ref base.NPC.velocity)).Length() * 0.1f, 0.6f) + 1.0;
		if (base.NPC.frameCounter >= ((CurrentAttackState != AttackState.Hover) ? 16.0 : 8.0))
		{
			base.NPC.frame.Y += frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameHeight * 8)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !Main.hardMode || (!Main.raining && !Main.remixWorld) || !spawnInfo.Player.ZoneSkyHeight)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		return SpawnCondition.Sky.Chance * 0.1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<StaticDischarge>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 16, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 16, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<EssenceofSunlight>(), 1, 8, 10, 10, 12));
		npcLoot.Add(ModContent.ItemType<EyeoftheStorm>(), 3);
		npcLoot.Add(ModContent.ItemType<StormSaber>(), 5);
	}
}
