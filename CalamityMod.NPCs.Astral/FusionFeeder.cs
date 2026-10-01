using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class FusionFeeder : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public const int TimeBetweenCharges = 60;

	public Player Target => Main.player[base.NPC.target];

	public ref float VerticalMovementDirection => ref base.NPC.ai[0];

	public ref float ChargeDelay => ref base.NPC.ai[2];

	public ref float SearchSoundCreationDelay => ref base.NPC.localAI[0];

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/FusionFeederGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 40f;
		value.Position.Y -= 6f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.noTileCollide = true;
		base.NPC.noGravity = true;
		base.NPC.width = 120;
		base.NPC.height = 48;
		base.NPC.damage = 45;
		base.NPC.aiStyle = -1;
		base.NPC.lifeMax = 500;
		base.NPC.defense = 28;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.knockBackResist = 0.8f;
		base.NPC.behindTiles = true;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.AnimationType = 542;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<FusionFeederBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 65;
			base.NPC.defense = 38;
			base.NPC.knockBackResist = 0.7f;
			base.NPC.lifeMax = 750;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralDesert>().Type };
	}

	public static bool ValidMovementPosition(Tile tile)
	{
		return tile.HasUnactuatedTile;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		Point tileCheckPoint = base.NPC.Bottom.ToTileCoordinates();
		Tile checkTile = Framing.GetTileSafely(tileCheckPoint);
		bool canMoveFreely = ValidMovementPosition(checkTile);
		canMoveFreely |= base.NPC.wet;
		base.NPC.TargetClosest();
		Vector2 targetCenter = ((Rectangle)(ref base.NPC.targetRect)).Center.ToVector2();
		float distanceFromTarget = base.NPC.Distance(targetCenter);
		bool attemptingToAttackTarget = Target.velocity.Y > -0.1f && !Target.dead && distanceFromTarget > 150f;
		if (SearchSoundCreationDelay == -1f && !canMoveFreely)
		{
			SearchSoundCreationDelay = 20f;
		}
		if (SearchSoundCreationDelay > 0f)
		{
			SearchSoundCreationDelay--;
		}
		if (canMoveFreely)
		{
			if (base.NPC.soundDelay == 0)
			{
				base.NPC.soundDelay = (int)MathHelper.Clamp(distanceFromTarget / 40f, 10f, 20f);
				SoundEngine.PlaySound(in SoundID.WormDig, base.NPC.Center);
			}
			tileCheckPoint = (base.NPC.Center + Vector2.UnitY * 24f).ToTileCoordinates();
			checkTile = Framing.GetTileSafely(tileCheckPoint.X, tileCheckPoint.Y - 2);
			bool belowSand = ValidMovementPosition(checkTile);
			if (ChargeDelay < 30f)
			{
				ChargeDelay++;
			}
			if (attemptingToAttackTarget)
			{
				base.NPC.TargetClosest();
				NPC nPC = base.NPC;
				nPC.velocity += new Vector2((float)base.NPC.direction, (float)base.NPC.directionY) * 0.15f;
				base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -6f, 6f);
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, -3f, 3f);
				Vector2 center = base.NPC.Center;
				Vector2 val = base.NPC.velocity.SafeNormalize(Vector2.Zero);
				Vector2 size = base.NPC.Size;
				tileCheckPoint = (center + val * ((Vector2)(ref size)).Length() * 2f + base.NPC.velocity).ToTileCoordinates();
				checkTile = Framing.GetTileSafely(tileCheckPoint);
				bool shouldntCharge = ValidMovementPosition(checkTile);
				if (!shouldntCharge && base.NPC.wet)
				{
					shouldntCharge = checkTile.LiquidAmount > 0;
				}
				if (!shouldntCharge && Math.Sign(base.NPC.velocity.X) == base.NPC.direction && distanceFromTarget < 540f && (ChargeDelay >= 30f || ChargeDelay < 0f))
				{
					if (SearchSoundCreationDelay == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Zombie7, base.NPC.Center);
						SearchSoundCreationDelay = -1f;
					}
					if (ChargeDelay > 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item73, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int damage = ((!DownedBossSystem.downedAstrumAureus) ? (Main.masterMode ? 30 : (Main.expertMode ? 35 : 45)) : (Main.masterMode ? 34 : (Main.expertMode ? 40 : 55)));
							Vector2 meteorShootVelocity = base.NPC.SafeDirectionTo(Target.Center) * 9f;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + meteorShootVelocity * 6f, meteorShootVelocity, ModContent.ProjectileType<AstralMeteorProj>(), damage, 0f);
						}
					}
					ChargeDelay = -30f;
					base.NPC.velocity = base.NPC.SafeDirectionTo(targetCenter - Vector2.UnitY * 80f) * 16f;
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X *= -1f;
					base.NPC.direction *= -1;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
					base.NPC.velocity.Y *= -1f;
					base.NPC.directionY = Math.Sign(base.NPC.velocity.Y);
					VerticalMovementDirection = base.NPC.directionY;
				}
				float movementDirectionSwitchThreshold = 0.06f;
				float horizontalSearchAcceleration = 0.1f;
				float horizontalSearchMaxSpeed = 6f;
				float verticalSearchAcceleration = 0.01f;
				float verticalSearchMaxSpeed = 0.5f;
				VerticalMovementDirection = (!belowSand).ToDirectionInt();
				base.NPC.velocity.X += (float)base.NPC.direction * horizontalSearchAcceleration;
				if (Math.Abs(base.NPC.velocity.X) > horizontalSearchMaxSpeed)
				{
					base.NPC.velocity.X *= 0.95f;
				}
				if (VerticalMovementDirection == -1f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - verticalSearchAcceleration;
					if (base.NPC.velocity.Y < 0f - movementDirectionSwitchThreshold)
					{
						VerticalMovementDirection = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y += verticalSearchAcceleration;
					if (base.NPC.velocity.Y > movementDirectionSwitchThreshold)
					{
						VerticalMovementDirection = -1f;
					}
				}
				if (Math.Abs(base.NPC.velocity.Y) > verticalSearchMaxSpeed)
				{
					base.NPC.velocity.Y *= 0.95f;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y == 0f)
			{
				if (attemptingToAttackTarget)
				{
					base.NPC.TargetClosest();
				}
				base.NPC.velocity.X += 0.1f;
				if (Math.Abs(base.NPC.velocity.X) > 1f)
				{
					base.NPC.velocity.X *= 0.95f;
				}
			}
			base.NPC.velocity.Y += 0.3f;
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
			VerticalMovementDirection = 1f;
		}
		base.NPC.rotation = MathHelper.Clamp(base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f, -0.2f, 0.2f);
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.FusionFeeder")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 134, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(46, 4, 60, 6), Vector2.Zero, 0.55f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 4, 25);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 6; i++)
			{
				float rand = Main.rand.NextFloat(-0.18f, 0.18f);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + new Vector2(Main.rand.NextFloat(0f, base.NPC.width), Main.rand.NextFloat(0f, base.NPC.height)), base.NPC.velocity * rand, base.Mod.Find<ModGore>("FusionFeederGore" + i).Type);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector(0f, 10f);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(67f, 23f);
		if (base.NPC.IsABestiaryIconDummy)
		{
			drawColor = Color.White;
		}
		spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, base.NPC.Center - screenPos + offset, (Rectangle?)base.NPC.frame, drawColor, base.NPC.rotation, origin, 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos + offset, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, origin, 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
		return false;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(3))
		{
			return 0.14f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(319, 8);
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 2, 3, 3, 4));
		npcLoot.Add(4028, 30);
	}
}
