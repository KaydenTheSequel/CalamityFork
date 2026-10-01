using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class SlabCrab : ModNPC
{
	private enum AIState
	{
		Hiding,
		IdleAnim,
		Enraged,
		Active,
		Walking
	}

	public bool playerCrossed;

	public const int BaseDefense = 10;

	public const int BaseAttack = 20;

	public const float BaseKB = 1f;

	public static Asset<Texture2D> GlowTexture;

	public Player Target => Main.player[base.NPC.target];

	public ref float CurrentPhase => ref base.NPC.ai[0];

	public ref float AITimer => ref base.NPC.ai[1];

	public ref float HopTimer => ref base.NPC.ai[2];

	public ref float CalmDownTimer => ref base.NPC.ai[3];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 23;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 44;
		base.NPC.height = 30;
		base.NPC.damage = 20;
		base.NPC.lifeMax = 300;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.chaseable = false;
		base.NPC.knockBackResist = 0f;
		base.NPC.defense = 999998;
		base.NPC.HitSound = SoundID.NPCHit33;
		base.NPC.DeathSound = SoundID.NPCDeath36;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SlabCrabBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.NPC.GravityIgnoresLiquid = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SlabCrab")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(playerCrossed);
		writer.Write(base.NPC.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		playerCrossed = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.direction = ((base.NPC.velocity.X != 0f) ? Math.Sign(base.NPC.velocity.X) : base.NPC.direction);
		base.NPC.spriteDirection = -base.NPC.direction;
		base.NPC.localAI[0]++;
		if (base.NPC.localAI[0] < 90f)
		{
			return;
		}
		base.NPC.damage = 0;
		float currentPhase = CurrentPhase;
		int num;
		if (currentPhase != 0f)
		{
			if (currentPhase != 1f)
			{
				if (currentPhase != 2f)
				{
					if (currentPhase != 3f)
					{
						if (currentPhase == 4f)
						{
							AITimer++;
							base.NPC.knockBackResist = 1f;
							base.NPC.damage = 20;
							base.NPC.defense = 10;
							if (!(Target.Center.Distance(base.NPC.Center) > 300f))
							{
								if (Target.Center.Distance(base.NPC.Center) > 120f)
								{
									num = ((!Collision.CanHitLine(base.NPC.Center, 1, 1, Target.Center, 1, 1)) ? 1 : 0);
									if (num != 0)
									{
										goto IL_05d5;
									}
								}
								else
								{
									num = 0;
								}
								if (CalmDownTimer > 0f)
								{
									CalmDownTimer--;
								}
								goto IL_0603;
							}
							num = 1;
							goto IL_05d5;
						}
					}
					else
					{
						base.NPC.ShowNameOnHover = true;
						base.NPC.defense = 10;
						base.NPC.knockBackResist = 1f;
						base.NPC.damage = 20;
						base.NPC.chaseable = true;
						bool outofRange = Target.Center.Distance(base.NPC.Center) > 600f || (Target.Center.Distance(base.NPC.Center) > 320f && !Collision.CanHitLine(base.NPC.Center, 1, 1, Target.Center, 1, 1));
						if (outofRange)
						{
							CalmDownTimer++;
						}
						else if (CalmDownTimer > 0f)
						{
							CalmDownTimer--;
						}
						if (base.NPC.velocity.Y == 0f)
						{
							AITimer++;
							base.NPC.knockBackResist = 0.6f;
							base.NPC.TargetClosest();
							base.NPC.velocity.X *= 0.85f;
							float hopRate = MathHelper.Lerp(25f, 10f, 1f - (float)base.NPC.life / (float)base.NPC.lifeMax);
							float lungeForwardSpeed = 6f;
							float jumpSpeed = 7f;
							if (Collision.CanHit(base.NPC.Center, 1, 1, Target.Center, 1, 1))
							{
								lungeForwardSpeed *= 1.2f;
							}
							if (outofRange && CalmDownTimer > 180f)
							{
								playerCrossed = false;
								ChangeAIHook(0f);
								base.NPC.velocity.X = 0f;
							}
							if (HopTimer >= 3f)
							{
								ChangeAIHook(4f);
							}
							if (Main.netMode != 1 && AITimer > hopRate)
							{
								HopTimer++;
								if (HopTimer % 3f == 2f)
								{
									lungeForwardSpeed *= 1.5f;
								}
								AITimer = 0f;
								base.NPC.velocity.Y -= jumpSpeed;
								base.NPC.velocity.X = lungeForwardSpeed * (float)base.NPC.direction;
								base.NPC.netUpdate = true;
							}
						}
						else
						{
							base.NPC.knockBackResist = 0.2f;
							base.NPC.velocity.X *= 0.995f;
						}
					}
				}
				else
				{
					base.NPC.ShowNameOnHover = true;
					AITimer++;
					base.NPC.defense = 10;
					base.NPC.knockBackResist = 0f;
					base.NPC.chaseable = false;
					base.NPC.TargetClosest(faceTarget: false);
					if (AITimer > 24f)
					{
						ChangeAIHook(3f);
					}
				}
			}
			else
			{
				base.NPC.ShowNameOnHover = true;
				base.NPC.chaseable = false;
				base.NPC.defense = 999998;
				AITimer++;
				base.NPC.TargetClosest(faceTarget: false);
				if (base.NPC.velocity.Y > 0f)
				{
					ChangeAIHook(3f);
				}
				if (AITimer > 90f)
				{
					ChangeAIHook(0f);
				}
				HandlePlayerDetection();
				HandlePickaxeInteraction();
			}
		}
		else
		{
			base.NPC.ShowNameOnHover = false;
			base.NPC.chaseable = false;
			base.NPC.defense = 999998;
			AITimer++;
			base.NPC.TargetClosest(faceTarget: false);
			if (base.NPC.velocity.Y > 0f)
			{
				ChangeAIHook(3f);
			}
			if (AITimer > 300f && Main.rand.NextBool(420))
			{
				ChangeAIHook(1f);
			}
			HandlePlayerDetection();
			HandlePickaxeInteraction();
		}
		goto IL_06f5;
		IL_05d5:
		CalmDownTimer++;
		goto IL_0603;
		IL_06f5:
		if (base.NPC.velocity.Y > 0f)
		{
			base.NPC.velocity.Y *= 1.1f;
		}
		return;
		IL_0603:
		if (base.NPC.oldPosition == base.NPC.position)
		{
			base.NPC.direction *= -1;
			base.NPC.netUpdate = true;
		}
		base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, (float)(5 * base.NPC.direction), 0.0125f);
		if (num != 0 && Main.rand.NextBool(3) && CalmDownTimer > 180f)
		{
			playerCrossed = false;
			ChangeAIHook(0f);
			base.NPC.velocity.X = 0f;
		}
		if (AITimer > 240f && Collision.CanHitLine(base.NPC.Center, 1, 1, Target.Center, 1, 1))
		{
			ChangeAIHook(3f);
		}
		goto IL_06f5;
	}

	public void ChangePhase(float ai0, float ai1 = -1f, float ai2 = -1f, float ai3 = -1f)
	{
		CurrentPhase = ai0;
		AITimer = ((ai1 == -1f) ? 0f : ai1);
		HopTimer = ((ai2 == -1f) ? 0f : ai2);
		CalmDownTimer = ((ai3 == -1f) ? 0f : ai3);
		base.NPC.netUpdate = true;
		if (Main.dedServ)
		{
			NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
		}
	}

	public void ChangeAIHook(float phase)
	{
		if (Main.netMode == 0)
		{
			ChangePhase((int)phase);
		}
		else
		{
			SyncSlabCrabAIPacket.Send(this, (int)phase);
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		if (CurrentPhase < 2f)
		{
			return false;
		}
		return null;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer1 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphurousShale>(), 5, 10, 30);
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy || CurrentPhase == 3f)
		{
			if (base.NPC.velocity.Y == 0f && base.NPC.frameCounter++ % 6.0 == 0.0)
			{
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.velocity.X != 0f || base.NPC.IsABestiaryIconDummy)
			{
				if (base.NPC.frame.Y > frameHeight * 22 || base.NPC.frame.Y < frameHeight * 19)
				{
					base.NPC.frame.Y = frameHeight * 19;
				}
			}
			else
			{
				base.NPC.frame.Y = frameHeight * 19;
			}
			return;
		}
		float currentPhase = CurrentPhase;
		if (currentPhase != 0f)
		{
			if (currentPhase != 1f)
			{
				if (currentPhase == 2f)
				{
					if (base.NPC.frame.Y > frameHeight * 18 || base.NPC.frame.Y < frameHeight * 15)
					{
						base.NPC.frame.Y = frameHeight * 15;
					}
					if (base.NPC.frame.Y < frameHeight * 18)
					{
						base.NPC.frameCounter++;
					}
					if (base.NPC.frameCounter >= 6.0)
					{
						base.NPC.frame.Y += frameHeight;
						base.NPC.frameCounter = 0.0;
					}
				}
				else
				{
					if (base.NPC.frameCounter++ % 6.0 == 0.0)
					{
						base.NPC.frame.Y += frameHeight;
					}
					if (base.NPC.frame.Y > frameHeight * 22 || base.NPC.frame.Y < frameHeight * 19)
					{
						base.NPC.frame.Y = frameHeight * 19;
					}
				}
			}
			else
			{
				if (base.NPC.frameCounter++ % 6.0 == 0.0)
				{
					base.NPC.frame.Y += frameHeight;
				}
				if (base.NPC.frame.Y > frameHeight * 14)
				{
					base.NPC.frame.Y = 0;
				}
			}
		}
		else
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return CurrentPhase > 1f;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return CurrentPhase > 1f;
	}

	public void HandlePickaxeInteraction()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		Rectangle tileMaus = default(Rectangle);
		((Rectangle)(ref tileMaus))._002Ector(Player.tileTargetX * 16, Player.tileTargetY * 16, 16, 16);
		if (player.HeldItem.pick <= 0 || !((Rectangle)(ref tileMaus)).Intersects(base.NPC.getRect()))
		{
			return;
		}
		float num = player.Distance(base.NPC.Center);
		Vector2 val = new Vector2((float)Player.tileRangeX, (float)Player.tileRangeY);
		if (num < (((Vector2)(ref val)).Length() + (float)player.HeldItem.tileBoost) * 16f && player.ItemAnimationActive)
		{
			Item pick = player.HeldItem;
			int pickDamage = (int)player.GetDamage(pick.DamageType).ApplyTo(pick.damage);
			Projectile.NewProjectileDirect(pick.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), pickDamage, 0f, player.whoAmI, base.NPC.whoAmI).DamageType = pick.DamageType;
			SoundEngine.PlaySound(in SoundID.Dig, base.NPC.Center);
			if (CurrentPhase < 2f)
			{
				ChangeAIHook(2f);
			}
		}
	}

	public void HandlePlayerDetection()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (!playerCrossed)
		{
			if (Math.Abs(Target.position.X - base.NPC.position.X) < 4f && Collision.CanHitLine(base.NPC.Center, 1, 1, Target.Center, 1, 1))
			{
				playerCrossed = true;
			}
		}
		else if (Target.Distance(base.NPC.Center) > 128f)
		{
			ChangeAIHook(2f);
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
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 33, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 15; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 33, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			for (int j = 1; j < 5; j++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SlabCrab" + j).Type);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects effects = (SpriteEffects)(base.NPC.spriteDirection == 1);
			Main.EntitySpriteDraw(GlowTexture.Value, base.NPC.Center - Main.screenPosition + new Vector2(0f, base.NPC.gfxOffY + 4f), base.NPC.frame, Color.White * 0.5f, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, effects);
		}
	}
}
