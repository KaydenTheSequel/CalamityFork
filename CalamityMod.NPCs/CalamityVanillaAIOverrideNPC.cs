using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Events;
using CalamityMod.ExtraTextures;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.VanillaNPCAIOverrides;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.NPCs.VanillaNPCAIOverrides.MiniBosses;
using CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;
using CalamityMod.Systems.Collections;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.NPCs;

public sealed class CalamityVanillaAIOverrideNPC : GlobalNPC
{
	public VanillaAIOverride AIOverride;

	public static Dictionary<Type, int> NetIDLookup = new Dictionary<Type, int>();

	public const int InvalidNetID = 0;

	public static bool Enabled { get; set; } = true;

	public static HashSet<int> GlobalChangeBlacklist { get; private set; } = new HashSet<int>();

	public override bool InstancePerEntity => true;

	public static event Action<VanillaAIOverrideContext> ModifyAIOverride;

	public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
	{
		if (entity.townNPC)
		{
			return false;
		}
		if (entity.friendly)
		{
			return false;
		}
		if (entity.CountsAsACritter)
		{
			return false;
		}
		if (CalamityNPCSets.DontCountAsEnemy[entity.type])
		{
			return false;
		}
		return true;
	}

	public override GlobalNPC Clone(NPC npc, NPC npcClone)
	{
		CalamityVanillaAIOverrideNPC clone = (CalamityVanillaAIOverrideNPC)base.Clone(npc, npcClone);
		if (AIOverride != null)
		{
			clone.AIOverride = AIOverride.Clone();
			clone.AIOverride.NPC = npcClone;
		}
		else
		{
			clone.AIOverride = null;
		}
		return clone;
	}

	public static VanillaAIOverride GetVanillaAIOverrideToApply(NPC npc)
	{
		if (npc == null)
		{
			return null;
		}
		if (npc.whoAmI < 0 || npc.whoAmI >= Main.maxNPCs)
		{
			return null;
		}
		if (!npc.active)
		{
			return null;
		}
		if (npc.type == 618)
		{
			return new DreadnautilusAI();
		}
		if (npc.type == 523 && Main.npc[(int)npc.ai[0]].type == ModContent.NPCType<PrimordialWyrmHead>())
		{
			return new CultistAI.AncientDoomAI();
		}
		if (Main.zenithWorld && npc.type == 222)
		{
			return new QueenBeeAI();
		}
		if (CalamityWorld.death && npc.type == 371)
		{
			return new DukeFishronAI.DetonatingBubbleAI();
		}
		if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
		{
			switch (npc.type)
			{
			case 50:
				return new KingSlimeAI();
			case 4:
				return new EyeOfCthulhuAI();
			case 13:
			case 14:
			case 15:
				return new EaterOfWorldsAI();
			case 266:
				return new BrainOfCthulhuAI();
			case 267:
				return new CreeperAI();
			case 222:
				return new QueenBeeAI();
			case 35:
				if (Main.netMode == 0)
				{
					return new SkeletronAI();
				}
				return null;
			case 36:
				if (Main.netMode == 0)
				{
					return new SkeletronAI.SkeletronHandAI();
				}
				return null;
			case 68:
				return new SkeletronAI.DungeonGuardianAI();
			case 668:
				return new DeerclopsAI();
			case 113:
				return new WallOfFleshAI();
			case 114:
				return new WallOfFleshAI.EyeAI();
			case 657:
				return new QueenSlimeAI();
			case 658:
				return new QueenSlimeAI.CrystalSlimeAI();
			case 659:
				return new QueenSlimeAI.BouncySlimeAI();
			case 134:
			case 135:
			case 136:
				return new DestroyerAI();
			case 139:
				return new DestroyerAI.ProbeAI();
			case 125:
				return new TwinsAI.RetinazerAI();
			case 126:
				return new TwinsAI.SpazmatismAI();
			case 127:
				return new SkeletronPrimeAI();
			case 131:
				return new SkeletronPrimeAI.PrimeLaserAI();
			case 128:
				return new SkeletronPrimeAI.PrimeCannonAI();
			case 130:
				return new SkeletronPrimeAI.PrimeViceAI();
			case 129:
				return new SkeletronPrimeAI.PrimeSawAI();
			case 262:
				return new PlanteraAI();
			case 263:
				return new PlanteraAI.HookAI();
			case 264:
				return new PlanteraAI.TentacleAI();
			case 636:
				return new EmpressofLightAI();
			case 245:
				return new GolemAI();
			case 247:
			case 248:
				return new GolemAI.FistAI();
			case 246:
				return new GolemAI.HeadAI();
			case 249:
				return new GolemAI.HeadFreeAI();
			case 370:
				return new DukeFishronAI();
			case 327:
				if (DownedBossSystem.downedDoG)
				{
					return new PumpkingAI();
				}
				break;
			case 328:
				if (DownedBossSystem.downedDoG)
				{
					return new PumpkingAI.BladeAI();
				}
				break;
			case 345:
				if (DownedBossSystem.downedDoG)
				{
					return new IceQueenAI();
				}
				break;
			case 477:
				if (DownedBossSystem.downedDoG)
				{
					return new MothronAI();
				}
				break;
			case 439:
			case 440:
				return new CultistAI();
			case 522:
				return new CultistAI.AncientLightAI();
			case 523:
				return new CultistAI.AncientDoomAI();
			case 396:
			case 397:
			case 398:
			case 400:
			case 401:
				return new MoonLordAI();
			}
		}
		if (CalamityWorld.revenge)
		{
			switch (npc.aiStyle)
			{
			case 1:
				if (npc.type == ModContent.NPCType<BloomSlime>() || npc.type == ModContent.NPCType<InfernalCongealment>() || npc.type == ModContent.NPCType<CrimulanBlightSlime>() || npc.type == ModContent.NPCType<CryoSlime>() || npc.type == ModContent.NPCType<EbonianBlightSlime>() || npc.type == ModContent.NPCType<PerennialSlime>() || npc.type == ModContent.NPCType<IrradiatedSlime>() || npc.type == ModContent.NPCType<AstralSlime>())
				{
					return new SlimeAI();
				}
				switch (npc.type)
				{
				case 1:
				case 16:
				case 59:
				case 71:
				case 81:
				case 138:
				case 141:
				case 147:
				case 183:
				case 184:
				case 204:
				case 225:
				case 244:
				case 302:
				case 304:
				case 333:
				case 334:
				case 335:
				case 336:
				case 535:
				case 537:
				case 667:
				case 676:
					return new SlimeAI();
				}
				break;
			case 2:
				if (npc.type == ModContent.NPCType<CalamityEye>())
				{
					return new DemonEyeAI();
				}
				switch (npc.type)
				{
				case 2:
				case 116:
				case 133:
				case 170:
				case 171:
				case 180:
				case 190:
				case 191:
				case 192:
				case 193:
				case 194:
				case 317:
				case 318:
					return new DemonEyeAI();
				}
				break;
			case 3:
			{
				if (npc.type == ModContent.NPCType<Stormlion>() || npc.type == ModContent.NPCType<BucketZombie>() || npc.type == ModContent.NPCType<AstralachneaGround>() || npc.type == ModContent.NPCType<RenegadeWarlock>())
				{
					return new RevengeanceAndDeathAI.FighterAI();
				}
				int type = npc.type;
				if (type <= 132)
				{
					if (type <= 53)
					{
						if (type <= 28)
						{
							if (type != 3 && type != 21 && (uint)(type - 26) > 2u)
							{
								break;
							}
						}
						else if (type <= 44)
						{
							if (type != 31 && type != 44)
							{
								break;
							}
						}
						else if (type != 47 && (uint)(type - 52) > 1u)
						{
							break;
						}
					}
					else if (type <= 80)
					{
						if (type != 67 && type != 73 && (uint)(type - 77) > 3u)
						{
							break;
						}
					}
					else if (type <= 111)
					{
						if (type != 104 && (uint)(type - 109) > 2u)
						{
							break;
						}
					}
					else if (type != 120 && type != 132)
					{
						break;
					}
				}
				else if (type <= 411)
				{
					switch (type)
					{
					case 140:
					case 159:
					case 161:
					case 162:
					case 166:
					case 167:
					case 168:
					case 181:
					case 185:
					case 186:
					case 187:
					case 188:
					case 189:
					case 196:
					case 197:
					case 198:
					case 199:
					case 200:
					case 201:
					case 202:
					case 203:
					case 206:
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 223:
					case 254:
					case 255:
					case 257:
					case 258:
					case 269:
					case 270:
					case 271:
					case 272:
					case 273:
					case 274:
					case 275:
					case 276:
					case 277:
					case 278:
					case 279:
					case 280:
					case 290:
					case 291:
					case 293:
					case 294:
					case 295:
					case 296:
					case 305:
					case 306:
					case 307:
					case 308:
					case 309:
					case 310:
					case 311:
					case 312:
					case 313:
					case 314:
					case 319:
					case 320:
					case 321:
					case 322:
					case 323:
					case 324:
					case 326:
					case 331:
					case 332:
					case 338:
					case 339:
					case 340:
					case 342:
					case 343:
					case 348:
					case 349:
					case 350:
					case 351:
					case 379:
					case 380:
					case 381:
					case 382:
					case 383:
					case 385:
					case 386:
					case 389:
					case 391:
					case 409:
					case 411:
						break;
					default:
						goto end_IL_03ca;
					}
				}
				else
				{
					switch (type)
					{
					case 415:
					case 419:
					case 424:
					case 429:
					case 430:
					case 431:
					case 432:
					case 433:
					case 434:
					case 435:
					case 436:
					case 449:
					case 450:
					case 451:
					case 452:
					case 460:
					case 461:
					case 462:
					case 464:
					case 466:
					case 468:
					case 469:
					case 470:
					case 471:
					case 480:
					case 481:
					case 482:
					case 489:
					case 494:
					case 495:
					case 498:
					case 499:
					case 500:
					case 501:
					case 502:
					case 503:
					case 504:
					case 505:
					case 506:
					case 508:
					case 518:
					case 520:
					case 524:
					case 525:
					case 526:
					case 527:
					case 528:
					case 529:
					case 530:
					case 532:
					case 534:
					case 536:
					case 580:
					case 582:
					case 635:
						break;
					default:
						goto end_IL_03ca;
					}
				}
				return new RevengeanceAndDeathAI.FighterAI();
			}
			case 5:
				switch (npc.type)
				{
				case 5:
				case 6:
				case 23:
				case 42:
				case 173:
				case 176:
				case 205:
				case 210:
				case 211:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 252:
					return new RevengeanceAndDeathAI.FlyingAI();
				}
				break;
			case 6:
				switch (npc.type)
				{
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 39:
				case 40:
				case 41:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 117:
				case 118:
				case 119:
				case 402:
				case 412:
				case 413:
				case 414:
				case 513:
				case 514:
				case 515:
				case 621:
				case 622:
				case 623:
					return new RevengeanceAndDeathAI.WormAI();
				}
				break;
			case 13:
				switch (npc.type)
				{
				case 43:
				case 56:
				case 101:
				case 175:
				case 259:
				case 260:
					return new RevengeanceAndDeathAI.PlantAI();
				}
				break;
			case 14:
				if (npc.type == ModContent.NPCType<StellarCulex>() || npc.type == ModContent.NPCType<Melter>() || npc.type == ModContent.NPCType<AeroSlime>())
				{
					return new RevengeanceAndDeathAI.BatAI();
				}
				switch (npc.type)
				{
				case 49:
				case 51:
				case 60:
				case 93:
				case 121:
				case 137:
				case 150:
				case 151:
				case 152:
				case 158:
				case 226:
				case 634:
					return new RevengeanceAndDeathAI.BatAI();
				}
				break;
			case 16:
				switch (npc.type)
				{
				case 57:
				case 58:
				case 65:
				case 102:
				case 157:
				case 241:
				case 465:
					return new RevengeanceAndDeathAI.SwimmingAI();
				}
				break;
			case 18:
				switch (npc.type)
				{
				case 63:
				case 64:
				case 103:
				case 221:
				case 242:
				case 256:
					return new RevengeanceAndDeathAI.JellyfishAI();
				}
				break;
			case 20:
				if (npc.type == 70)
				{
					return new RevengeanceAndDeathAI.SpikeBallAI();
				}
				break;
			case 21:
				if (npc.type == 72)
				{
					return new RevengeanceAndDeathAI.BlazingWheelAI();
				}
				break;
			case 22:
				switch (npc.type)
				{
				case 75:
				case 82:
				case 122:
				case 182:
				case 253:
				case 316:
				case 330:
				case 490:
					return new RevengeanceAndDeathAI.HoveringAI();
				}
				break;
			case 23:
			{
				int type = npc.type;
				if ((uint)(type - 83) <= 1u || type == 179)
				{
					return new RevengeanceAndDeathAI.FlyingWeaponAI();
				}
				break;
			}
			case 25:
			{
				int type = npc.type;
				if (type == 85 || type == 341 || type == 629)
				{
					return new RevengeanceAndDeathAI.MimicAI();
				}
				break;
			}
			case 26:
				if (npc.type == ModContent.NPCType<Rotdog>())
				{
					return new RevengeanceAndDeathAI.UnicornAI();
				}
				switch (npc.type)
				{
				case 86:
				case 155:
				case 315:
				case 329:
				case 410:
				case 423:
				case 546:
					return new RevengeanceAndDeathAI.UnicornAI();
				}
				break;
			case 29:
				if (npc.type == 115)
				{
					return new WallOfFleshAI.HungryAI();
				}
				break;
			case 39:
			{
				if (npc.type == ModContent.NPCType<Plagueshell>())
				{
					return new RevengeanceAndDeathAI.TortoiseAI();
				}
				int type = npc.type;
				if ((uint)(type - 153) <= 1u || type == 417 || (uint)(type - 496) <= 1u)
				{
					return new RevengeanceAndDeathAI.TortoiseAI();
				}
				break;
			}
			case 40:
				if (npc.type == 531)
				{
					return new RevengeanceAndDeathAI.SpiderAI();
				}
				break;
			case 41:
			{
				if (npc.type == ModContent.NPCType<Aries>())
				{
					return new RevengeanceAndDeathAI.HerplingAI();
				}
				int type = npc.type;
				if (type == 174 || type == 177 || type == 378)
				{
					return new RevengeanceAndDeathAI.HerplingAI();
				}
				break;
			}
			case 44:
				switch (npc.type)
				{
				case 224:
				case 509:
				case 581:
				case 587:
					return new RevengeanceAndDeathAI.FlyingFishAI();
				}
				break;
			case 49:
				if (npc.type == 250)
				{
					return new RevengeanceAndDeathAI.AngryNimbusAI();
				}
				break;
			case 73:
				if (npc.type == 387)
				{
					return new RevengeanceAndDeathAI.TeslaTurretAI();
				}
				break;
			case 74:
			{
				int type = npc.type;
				if (type == 388 || type == 418)
				{
					return new RevengeanceAndDeathAI.CoriteAI();
				}
				break;
			}
			case 80:
				if (npc.type == 399)
				{
					return new RevengeanceAndDeathAI.MartianProbeAI();
				}
				break;
			case 85:
			{
				int type = npc.type;
				if (type == 405 || type == 421 || type == 467)
				{
					return new RevengeanceAndDeathAI.StarCellAI();
				}
				break;
			}
			case 86:
			{
				int type = npc.type;
				if (type == 472 || type == 521)
				{
					return new RevengeanceAndDeathAI.AncientVisionAI();
				}
				break;
			}
			case 87:
			{
				int type = npc.type;
				if ((uint)(type - 473) <= 3u)
				{
					return new RevengeanceAndDeathAI.BigMimicAI();
				}
				break;
			}
			case 89:
				if (npc.type == 478)
				{
					return new RevengeanceAndDeathAI.MothronEggAI();
				}
				break;
			case 91:
				if (npc.type == 483)
				{
					return new RevengeanceAndDeathAI.GraniteElementalAI();
				}
				break;
			case 95:
				if (npc.type == 406)
				{
					return new RevengeanceAndDeathAI.SmallStarCellAI();
				}
				break;
			case 96:
				if (npc.type == 407)
				{
					return new RevengeanceAndDeathAI.FlowInvaderAI();
				}
				break;
			case 50:
				{
					int type = npc.type;
					if (type == 261 || type == 265)
					{
						return new RevengeanceAndDeathAI.SporeAI();
					}
					break;
				}
				end_IL_03ca:
				break;
			}
		}
		return null;
	}

	internal static bool IsGlobalChangeBlacklisted(NPC npc)
	{
		return GlobalChangeBlacklist.Contains(npc.type);
	}

	internal static void RegisterNetID(VanillaAIOverride aiOverride)
	{
		int id = NetIDLookup.Count + 1;
		NetIDLookup[aiOverride.GetType()] = id;
	}

	public override void Unload()
	{
		NetIDLookup.Clear();
		GlobalChangeBlacklist.Clear();
		ModifyAIOverride = null;
	}

	public override void SetDefaults(NPC npc)
	{
		if (Enabled && Main.netMode != 1)
		{
			AIOverride = GetVanillaAIOverrideToApply(npc);
			if (ModifyAIOverride != null)
			{
				VanillaAIOverrideContext context = new VanillaAIOverrideContext
				{
					NPC = npc,
					NPCType = npc.type,
					InRevengeanceWorld = CalamityWorld.revenge,
					InDeathWorld = CalamityWorld.death,
					InBossRush = BossRushEvent.BossRushActive,
					OverrideToApply = AIOverride
				};
				ModifyAIOverride(context);
				AIOverride = context.OverrideToApply;
			}
			if (AIOverride != null)
			{
				AIOverride.NPC = npc;
				AIOverride.SetDefaults(base.Mod);
			}
		}
	}

	public override void OnSpawn(NPC npc, IEntitySource source)
	{
		if (Enabled)
		{
			AIOverride?.OnSpawn(base.Mod);
		}
	}

	public override bool PreAI(NPC npc)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (!Enabled)
		{
			return base.PreAI(npc);
		}
		bool result = true;
		if (!IsGlobalChangeBlacklisted(npc))
		{
			result &= GlobalPreAI(npc);
		}
		if (AIOverride != null)
		{
			result &= AIOverride.AI(base.Mod);
			if (AIOverride.DisableMultiplayerSmoothing)
			{
				npc.netOffset = Vector2.Zero;
				if (AIOverride.EnableMultiplayerSmoothingAheadOfAI)
				{
					AIOverride.DisableMultiplayerSmoothing = false;
				}
			}
		}
		return result;
	}

	public override void AI(NPC npc)
	{
		if (Enabled && !IsGlobalChangeBlacklisted(npc))
		{
			GlobalAI(npc);
		}
	}

	public override void PostAI(NPC npc)
	{
		if (Enabled)
		{
			AIOverride?.PostAI(base.Mod);
		}
	}

	public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
	{
		if (!Enabled)
		{
			return base.CanBeHitByProjectile(npc, projectile);
		}
		return AIOverride?.CanBeHitByProjectile(base.Mod, projectile);
	}

	public override void HitEffect(NPC npc, NPC.HitInfo hit)
	{
		if (Enabled)
		{
			AIOverride?.HitEffect(base.Mod, hit);
		}
	}

	public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (Enabled)
		{
			AIOverride?.ModifyHitByItem(base.Mod, player, item, ref modifiers);
		}
	}

	public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (Enabled)
		{
			AIOverride?.ModifyHitByProjectile(base.Mod, projectile, ref modifiers);
		}
	}

	public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		if (Enabled)
		{
			AIOverride?.OnHitByItem(base.Mod, player, item, hit, damageDone);
		}
	}

	public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		if (Enabled)
		{
			AIOverride?.OnHitByProjectile(base.Mod, projectile, hit, damageDone);
		}
	}

	public override bool PreKill(NPC npc)
	{
		if (!Enabled || AIOverride == null)
		{
			return base.PreKill(npc);
		}
		return AIOverride.PreKill(base.Mod);
	}

	public override void FindFrame(NPC npc, int frameHeight)
	{
		if (Enabled && !npc.IsABestiaryIconDummy)
		{
			AIOverride?.FindFrame(base.Mod, frameHeight);
		}
	}

	public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (!Enabled)
		{
			base.PreDraw(npc, spriteBatch, screenPos, drawColor);
		}
		if (npc.IsABestiaryIconDummy)
		{
			return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
		}
		bool result = true;
		if (!IsGlobalChangeBlacklisted(npc))
		{
			result &= GlobalPreDraw(npc, spriteBatch, screenPos, drawColor);
		}
		return result & (AIOverride?.PreDraw(base.Mod, spriteBatch, screenPos, drawColor) ?? true);
	}

	public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (Enabled)
		{
			if (!IsGlobalChangeBlacklisted(npc))
			{
				GlobalPostDraw(npc, spriteBatch, screenPos, drawColor);
			}
			AIOverride?.PostDraw(base.Mod, spriteBatch, screenPos, drawColor);
		}
	}

	public static int GetNetID(VanillaAIOverride aiOverride)
	{
		if (aiOverride == null)
		{
			return 0;
		}
		if (!NetIDLookup.TryGetValue(aiOverride.GetType(), out var netID))
		{
			return 0;
		}
		return netID;
	}

	public static bool TryGetNetID(VanillaAIOverride aIOverride, out int netID)
	{
		netID = GetNetID(aIOverride);
		return netID != 0;
	}

	public static VanillaAIOverride GetNewInstanceOrNullFromNetID(int netID, NPC ownerNPC)
	{
		Type type = NetIDLookup.FirstOrDefault((KeyValuePair<Type, int> kv) => kv.Value == netID).Key;
		if (type == null)
		{
			return null;
		}
		VanillaAIOverride instance = (VanillaAIOverride)Activator.CreateInstance(type);
		if (instance == null)
		{
			return null;
		}
		instance.NPC = ownerNPC;
		return instance;
	}

	public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		int netID;
		if (!npc.active || npc.life <= 0)
		{
			AIOverride = null;
			binaryWriter.Write7BitEncodedInt(0);
		}
		else if (!TryGetNetID(AIOverride, out netID))
		{
			binaryWriter.Write7BitEncodedInt(0);
		}
		else
		{
			binaryWriter.Write7BitEncodedInt(netID);
			AIOverride.SendExtraAI(bitWriter, binaryWriter);
		}
	}

	public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
	{
		int remoteNetID = binaryReader.Read7BitEncodedInt();
		if (GetNetID(AIOverride) != remoteNetID)
		{
			AIOverride = GetNewInstanceOrNullFromNetID(remoteNetID, npc);
			AIOverride?.SetDefaults(base.Mod);
		}
		AIOverride?.ReceiveExtraAI(bitReader, binaryReader);
	}

	private static bool GlobalPreAI(NPC npc)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calNPC = npc.GetGlobalNPC<CalamityGlobalNPC>();
		if (npc.type == 372 || npc.type == 373)
		{
			npc.damage = ((npc.alpha <= 0) ? npc.defDamage : 0);
		}
		else if (npc.type == 5)
		{
			Lighting.AddLight(npc.Center, 0.2f, 0.2f, 0.2f);
		}
		else if (npc.type == 439)
		{
			float lifeRatio = (float)npc.life / (float)npc.lifeMax;
			float colorTransitionAmt = (float)Math.Pow(1f - lifeRatio, 2.0);
			Color lightColor = Color.Lerp(Color.Cyan, Color.Blue, colorTransitionAmt);
			Lighting.AddLight(npc.Center, (float)(int)((Color)(ref lightColor)).R / 255f, (float)(int)((Color)(ref lightColor)).G / 255f, (float)(int)((Color)(ref lightColor)).B / 255f);
			if (calNPC.newAI[1] > 0f)
			{
				calNPC.newAI[1]--;
			}
			Vector2 hitboxSize = default(Vector2);
			((Vector2)(ref hitboxSize))._002Ector(152.73654f);
			if (npc.Size != hitboxSize)
			{
				npc.Size = hitboxSize;
			}
		}
		else if (npc.type == 440 && Main.npc[(int)npc.ai[3]].active)
		{
			float lifeRatio2 = (float)Main.npc[(int)npc.ai[3]].life / (float)Main.npc[(int)npc.ai[3]].lifeMax;
			float colorTransitionAmt2 = (float)Math.Pow(1f - lifeRatio2, 2.0);
			Color lightColor2 = Color.Lerp(Color.Cyan, Color.Blue, colorTransitionAmt2);
			Lighting.AddLight(npc.Center, (float)(int)((Color)(ref lightColor2)).R / 255f, (float)(int)((Color)(ref lightColor2)).G / 255f, (float)(int)((Color)(ref lightColor2)).B / 255f);
		}
		return true;
	}

	private static void GlobalAI(NPC npc)
	{
		switch (npc.type)
		{
		case 551:
			npc.damage = ((npc.ai[0] == 2f) ? npc.defDamage : 0);
			break;
		case 558:
		case 559:
		case 560:
			npc.damage = ((npc.ai[0] == 2f) ? npc.defDamage : 0);
			break;
		case 477:
			npc.damage = ((npc.ai[0] == 3.2f) ? ((int)Math.Round((double)npc.defDamage * 1.3)) : ((npc.ai[0] == 2f) ? ((int)Math.Round((double)npc.defDamage * 0.5)) : 0));
			break;
		case 479:
			npc.damage = ((npc.ai[0] == 2.1f) ? npc.defDamage : 0);
			break;
		case 85:
		case 341:
		case 629:
			npc.damage = ((npc.ai[0] != 0f && npc.velocity.Y != 0f) ? npc.defDamage : 0);
			break;
		case 473:
		case 474:
		case 475:
		case 476:
			npc.damage = ((npc.ai[0] != 3f) ? npc.defDamage : 0);
			if (npc.ai[0] == 3f)
			{
				npc.ai[1] += 0.5f;
			}
			break;
		case 388:
		case 418:
			npc.damage = ((npc.ai[0] == 2f || npc.ai[0] == 3f) ? npc.defDamage : 0);
			break;
		case 483:
			npc.damage = ((npc.ai[0] != -1f) ? npc.defDamage : 0);
			break;
		case 482:
			npc.damage = ((!(npc.ai[2] < 0f)) ? npc.defDamage : 0);
			break;
		case -10:
		case -9:
		case -8:
		case -7:
		case -6:
		case -5:
		case -4:
		case -3:
		case -2:
		case -1:
		case 1:
		case 16:
		case 59:
		case 71:
		case 81:
		case 138:
		case 141:
		case 147:
		case 183:
		case 225:
		case 244:
		case 302:
		case 304:
		case 333:
		case 334:
		case 335:
		case 336:
		case 537:
		case 667:
		case 676:
			npc.damage = ((npc.velocity.Y != 0f && !(((Vector2)(ref npc.velocity)).Length() < 3f)) ? npc.defDamage : 0);
			break;
		case 496:
		case 497:
			npc.damage = ((npc.ai[0] == 3f) ? ((int)Math.Round((double)npc.defDamage * 1.2)) : 0);
			break;
		case 153:
		case 154:
			npc.damage = ((npc.ai[0] == 3f) ? ((int)Math.Round((double)npc.defDamage * 1.4)) : 0);
			break;
		case 417:
			npc.damage = ((npc.ai[0] == 6f) ? ((int)Math.Round((double)npc.defDamage * 1.2)) : 0);
			break;
		}
	}

	private static bool GlobalPreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calNPC = npc.GetGlobalNPC<CalamityGlobalNPC>();
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (CalamityNPCTypeSets.Destroyer.Contains(npc.type) && !npc.IsABestiaryIconDummy)
		{
			Texture2D npcTexture = TextureAssets.Npc[npc.type].Value;
			Vector2 halfSize = npc.frame.Size() / 2f;
			SpriteEffects spriteEffects = (SpriteEffects)(npc.spriteDirection == 1);
			Color segmentDrawColor = npc.GetAlpha(drawColor);
			int num = (int)((npc.position.X - 8f) / 16f);
			int x2 = (int)((npc.position.X + (float)npc.width + 8f) / 16f);
			int y = (int)((npc.position.Y - 8f) / 16f);
			int y2 = (int)((npc.position.Y + (float)npc.height + 8f) / 16f);
			for (int l = num; l <= x2; l++)
			{
				for (int m = y; m <= y2; m++)
				{
					if (Lighting.Brightness(l, m) == 0f)
					{
						segmentDrawColor = Color.Black;
					}
				}
			}
			spriteBatch.Draw(npcTexture, npc.Center - screenPos + new Vector2(0f, npc.gfxOffY), (Rectangle?)npc.frame, segmentDrawColor, npc.rotation, halfSize, npc.scale, spriteEffects, 0f);
			if (npc.ai[2] == 0f && segmentDrawColor != Color.Black)
			{
				float num2 = (float)npc.life / (float)npc.lifeMax;
				bool phase4 = num2 < (death ? 0.4f : 0.25f);
				bool phase5 = num2 < (death ? 0.2f : 0.1f);
				bool num3 = calNPC.newAI[1] < 600f && calNPC.newAI[1] > 60f;
				float phaseTransitionColorAmount = ((num3 | phase5) ? 1f : 0f);
				if (!num3 && !phase5)
				{
					if (calNPC.newAI[3] >= 1620f)
					{
						phaseTransitionColorAmount = MathHelper.Clamp(1f - (calNPC.newAI[3] - 1620f) / 180f, 0f, 1f);
					}
					else if (calNPC.newAI[3] >= 720f)
					{
						phaseTransitionColorAmount = MathHelper.Clamp((calNPC.newAI[3] - 720f) / 180f, 0f, 1f);
					}
				}
				int alpha = 192;
				Color groundColor = default(Color);
				((Color)(ref groundColor))._002Ector(150, 0, 0, alpha);
				Color flightColor = (Color)(revenge ? new Color(0, 0, 150, alpha) : groundColor);
				Color segmentColor = Color.Lerp(groundColor, flightColor, phaseTransitionColorAmount);
				Color val = new Color(255, 125, 125, alpha);
				Color telegraphColor_Green = default(Color);
				((Color)(ref telegraphColor_Green))._002Ector(125, 255, 125, alpha);
				Color telegraphColor_Cyan = default(Color);
				((Color)(ref telegraphColor_Cyan))._002Ector(0, 255, 255, alpha);
				Color telegraphColor = val;
				float telegraphProgress = 0f;
				if (npc.TryGetAIOverride<DestroyerAI>(out var destroyerAI) && destroyerAI.LaserColor != -1)
				{
					float telegraphGateValue = ((!death) ? 450f : (phase5 ? 180f : (phase4 ? 270f : 360f))) - 120f;
					if (calNPC.newAI[0] > telegraphGateValue)
					{
						switch (destroyerAI.LaserColor)
						{
						case 1:
							telegraphColor = telegraphColor_Green;
							break;
						case 2:
							telegraphColor = telegraphColor_Cyan;
							break;
						}
						telegraphProgress = MathHelper.Clamp((calNPC.newAI[0] - telegraphGateValue) / 120f, 0f, 1f);
					}
				}
				Texture2D glowTexture = (CalamityClientConfig.Instance.EnableVanillaTextureEdits ? ExtraTextureRefs.DestroyerHeadGlowmask.Value : TextureAssets.Dest[0].Value);
				switch (npc.type)
				{
				case 135:
					glowTexture = (CalamityClientConfig.Instance.EnableVanillaTextureEdits ? ExtraTextureRefs.DestroyerBodyGlowmask.Value : TextureAssets.Dest[1].Value);
					break;
				case 136:
					glowTexture = (CalamityClientConfig.Instance.EnableVanillaTextureEdits ? ExtraTextureRefs.DestroyerTailGlowmask.Value : TextureAssets.Dest[2].Value);
					break;
				}
				float alphaMultiplier = 1f - (float)npc.alpha / 255f;
				spriteBatch.Draw(glowTexture, npc.Center - screenPos + new Vector2(0f, npc.gfxOffY), (Rectangle?)npc.frame, Color.Lerp(segmentColor, telegraphColor, telegraphProgress) * alphaMultiplier, npc.rotation, halfSize, npc.scale, spriteEffects, 0f);
			}
			return false;
		}
		return true;
	}

	private static void GlobalPostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calNPC = npc.GetGlobalNPC<CalamityGlobalNPC>();
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		if (CalamityWorld.death)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (npc.type == 439 || npc.type == 440)
		{
			spriteBatch.EnterShaderRegion();
			_ = calNPC.newAI[1] / 35f;
			float lifeRatio = ((npc.type == 439) ? ((float)npc.life / (float)npc.lifeMax) : ((float)Main.npc[(int)npc.ai[3]].life / (float)Main.npc[(int)npc.ai[3]].lifeMax));
			float flickerPower = 0f;
			if (lifeRatio < 0.85f)
			{
				flickerPower += 0.1f;
			}
			if (lifeRatio < 0.7f)
			{
				flickerPower += 0.1f;
			}
			if (lifeRatio < 0.55f)
			{
				flickerPower += 0.1f;
			}
			if (lifeRatio < 0.4f)
			{
				flickerPower += 0.1f;
			}
			if (lifeRatio < 0.25f)
			{
				flickerPower += 0.1f;
			}
			if (lifeRatio < 0.1f)
			{
				flickerPower += 0.1f;
			}
			float opacity = 1f;
			opacity *= MathHelper.Lerp(MathHelper.Max(1f - flickerPower, 0.56f), 1f, (float)Math.Pow(Math.Cos(Main.GlobalTimeWrappedHourly * MathHelper.Lerp(3f, 5f, flickerPower)) * 0.5 + 0.5, 24.0));
			float intensityAndOpacityMult = ((npc.type == 440) ? 0.9f : 1f);
			opacity *= intensityAndOpacityMult;
			Texture2D forcefieldTexture = global::CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas.ForcefieldTexture.Value;
			if (npc.type == 439)
			{
				GameShaders.Misc["CalamityMod:SupremeShield"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/EternityStreak", (AssetRequestMode)2));
			}
			else
			{
				GameShaders.Misc["CalamityMod:SupremeShield"].UseImage1("Images/Misc/noise");
			}
			float colorTransitionAmt = (float)Math.Pow(1f - lifeRatio, 2.0);
			Color forcefieldColor = Color.Lerp(Color.MediumSpringGreen, Color.Black, colorTransitionAmt);
			Color secondaryForcefieldColor = Color.Lerp(Color.Cyan, Color.Blue, colorTransitionAmt);
			forcefieldColor *= opacity;
			secondaryForcefieldColor *= opacity;
			GameShaders.Misc["CalamityMod:SupremeShield"].UseSecondaryColor(secondaryForcefieldColor);
			GameShaders.Misc["CalamityMod:SupremeShield"].UseColor(forcefieldColor);
			GameShaders.Misc["CalamityMod:SupremeShield"].UseSaturation(1f);
			GameShaders.Misc["CalamityMod:SupremeShield"].UseOpacity(0.65f);
			GameShaders.Misc["CalamityMod:SupremeShield"].Apply();
			float shieldScale = ((npc.type == 440) ? 1.65f : MathHelper.Lerp(1.65f, 3f, (float)Math.Pow(lifeRatio, 2.0)));
			spriteBatch.Draw(forcefieldTexture, npc.Center - Main.screenPosition, (Rectangle?)null, Color.White * opacity, 0f, forcefieldTexture.Size() * 0.5f, shieldScale, (SpriteEffects)0, 0f);
			spriteBatch.ExitShaderRegion();
		}
		else if (npc.type == 139)
		{
			float eyeTelegraphGateValue = (NPC.IsMechQueenUp ? 360f : (revenge ? 240f : 120f)) - 60f;
			Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
			Vector2 halfSize = npc.frame.Size() / 2f;
			Vector2 drawPosition = npc.Center - screenPos + Vector2.UnitX.RotatedBy(npc.rotation) * ((float)npc.width * 0.45f * (float)npc.spriteDirection) + Vector2.UnitY * npc.gfxOffY;
			float colorScale = MathHelper.Clamp((npc.localAI[0] - eyeTelegraphGateValue) / 60f, 0f, 1f);
			Color drawColor2 = new Color(255, 100, 150, 192) * colorScale;
			spriteBatch.SetBlendState(BlendState.Additive);
			spriteBatch.Draw(glowTexture, drawPosition, (Rectangle?)npc.frame, drawColor2, npc.rotation, halfSize, npc.scale * 1.1f, (SpriteEffects)0, 0f);
			spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
	}
}
