using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.EntitySources;
using CalamityMod.Events;
using CalamityMod.ExtraTextures;
using CalamityMod.Graphics;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;
using CalamityMod.Packets.Entities;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Healing;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Projectiles.VanillaProjectileOverrides;
using CalamityMod.Systems.Collections;
using CalamityMod.Systems.Mechanic;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Crags.Tree;
using CalamityMod.Tiles.FurnitureAuric;
using CalamityMod.Tiles.Ores;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles;

public class CalamityGlobalProjectile : GlobalProjectile
{
	internal interface IProjectileTweak
	{
		bool AppliesTo(Projectile proj);

		void ApplyTweak(Projectile proj);
	}

	internal class ArmorPenetrationDeltaRule : IProjectileTweak
	{
		internal readonly int delta;

		public ArmorPenetrationDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.ArmorPenetration += delta;
		}
	}

	internal class ArmorPenetrationExactRule : IProjectileTweak
	{
		internal readonly int armorPen;

		public ArmorPenetrationExactRule(int a)
		{
			armorPen = a;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.ArmorPenetration = armorPen;
		}
	}

	internal class DefenseDamageRule : IProjectileTweak
	{
		internal readonly bool flag = true;

		public DefenseDamageRule(bool dd)
		{
			flag = dd;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.Calamity().DealsDefenseDamage = flag;
		}
	}

	internal class ExtraUpdatesDeltaRule : IProjectileTweak
	{
		internal readonly int delta;

		public ExtraUpdatesDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.extraUpdates += delta;
			if (proj.extraUpdates < 0)
			{
				proj.extraUpdates = 0;
			}
		}
	}

	internal class ExtraUpdatesExactRule : IProjectileTweak
	{
		internal readonly int newExtraUpdates;

		public ExtraUpdatesExactRule(int eu)
		{
			newExtraUpdates = eu;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.extraUpdates = newExtraUpdates;
			if (proj.extraUpdates < 0)
			{
				proj.extraUpdates = 0;
			}
		}
	}

	internal class MaxUpdatesExactRule : IProjectileTweak
	{
		internal readonly int newMaxUpdates;

		public MaxUpdatesExactRule(int mu)
		{
			newMaxUpdates = mu;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.MaxUpdates = newMaxUpdates;
			if (proj.extraUpdates < 0)
			{
				proj.extraUpdates = 0;
			}
		}
	}

	internal class IDStaticIFrameRule : IProjectileTweak
	{
		internal readonly int idStaticIFrameValue = -2;

		public IDStaticIFrameRule(int f)
		{
			idStaticIFrameValue = f;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.usesLocalNPCImmunity = false;
			proj.localNPCHitCooldown = -2;
			proj.usesIDStaticNPCImmunity = true;
			proj.idStaticNPCHitCooldown = idStaticIFrameValue;
		}
	}

	internal class IgnoreWaterRule : IProjectileTweak
	{
		internal readonly bool flag = true;

		public IgnoreWaterRule(bool iw)
		{
			flag = iw;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.ignoreWater = flag;
		}
	}

	internal class LocalIFrameRule : IProjectileTweak
	{
		internal readonly int localIFrameValue = -2;

		public LocalIFrameRule(int f)
		{
			localIFrameValue = f;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.usesLocalNPCImmunity = true;
			proj.localNPCHitCooldown = localIFrameValue;
			proj.usesIDStaticNPCImmunity = false;
			proj.idStaticNPCHitCooldown = 0;
		}
	}

	internal class PiercingDeltaRule : IProjectileTweak
	{
		internal readonly int delta;

		public PiercingDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.penetrate += delta;
			if (proj.penetrate < 1)
			{
				proj.penetrate = 1;
			}
			proj.maxPenetrate = proj.penetrate;
		}
	}

	internal class PiercingExactRule : IProjectileTweak
	{
		internal readonly int newPenetrate = -1;

		public PiercingExactRule(int p)
		{
			newPenetrate = p;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.penetrate = newPenetrate;
			if (proj.penetrate == 0)
			{
				proj.penetrate = 1;
			}
			proj.maxPenetrate = proj.penetrate;
		}
	}

	internal class ScaleDeltaRule : IProjectileTweak
	{
		internal readonly float delta;

		public ScaleDeltaRule(float d)
		{
			delta = d;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.scale += delta;
			if (proj.scale < 0f)
			{
				proj.scale = 0f;
			}
		}
	}

	internal class ScaleExactRule : IProjectileTweak
	{
		internal readonly float newScale;

		public ScaleExactRule(float s)
		{
			newScale = s;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.scale = newScale;
			if (proj.scale < 0f)
			{
				proj.scale = 0f;
			}
		}
	}

	internal class ScaleRatioRule : IProjectileTweak
	{
		internal readonly float ratio = 1f;

		public ScaleRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.scale *= ratio;
			if (proj.scale < 0f)
			{
				proj.scale = 0f;
			}
		}
	}

	internal class SingleHitImmunityRule : IProjectileTweak
	{
		internal readonly bool flag;

		public SingleHitImmunityRule(bool imm)
		{
			flag = imm;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.appliesImmunityTimeOnSingleHits = flag;
		}
	}

	internal class TileCollideRule : IProjectileTweak
	{
		internal readonly bool flag = true;

		public TileCollideRule(bool tc)
		{
			flag = tc;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.tileCollide = flag;
		}
	}

	internal class TimeLeftDeltaRule : IProjectileTweak
	{
		internal readonly int delta;

		public TimeLeftDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.timeLeft += delta;
			if (proj.timeLeft < 1)
			{
				proj.timeLeft = 1;
			}
		}
	}

	internal class TimeLeftExactRule : IProjectileTweak
	{
		internal readonly int newTimeLeft;

		public TimeLeftExactRule(int t)
		{
			newTimeLeft = t;
		}

		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.timeLeft = newTimeLeft;
			if (proj.timeLeft < 1)
			{
				proj.timeLeft = 1;
			}
		}
	}

	internal class TrueMeleeRule : IProjectileTweak
	{
		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.DamageType = TrueMeleeDamageClass.Instance;
		}
	}

	internal class TrueMeleeNoSpeedRule : IProjectileTweak
	{
		public bool AppliesTo(Projectile proj)
		{
			return true;
		}

		public void ApplyTweak(Projectile proj)
		{
			proj.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		}
	}

	internal class YoyoLifetimeRule : IProjectileTweak
	{
		internal readonly float newLifetime = -1f;

		public YoyoLifetimeRule(float l)
		{
			newLifetime = l;
		}

		public bool AppliesTo(Projectile proj)
		{
			return IsAYoyo(proj);
		}

		public void ApplyTweak(Projectile proj)
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[proj.type] = newLifetime;
		}
	}

	internal class YoyoRangeRule : IProjectileTweak
	{
		internal readonly float newMaxRange;

		public YoyoRangeRule(float r)
		{
			newMaxRange = r;
		}

		public bool AppliesTo(Projectile proj)
		{
			return IsAYoyo(proj);
		}

		public void ApplyTweak(Projectile proj)
		{
			ProjectileID.Sets.YoyosMaximumRange[proj.type] = newMaxRange;
		}
	}

	internal class YoyoTopSpeedRule : IProjectileTweak
	{
		internal readonly float newTopSpeed;

		public YoyoTopSpeedRule(float s)
		{
			newTopSpeed = s;
		}

		public bool AppliesTo(Projectile proj)
		{
			return IsAYoyo(proj);
		}

		public void ApplyTweak(Projectile proj)
		{
			ProjectileID.Sets.YoyosTopSpeed[proj.type] = newTopSpeed;
		}
	}

	public bool CreatedByPlayerDash;

	public int ParentNPCIndex;

	public const float AcceleratingBossLaserVelocityCap = 8f;

	private bool frameOneHacksExecuted;

	public int supercritHits;

	public float bonusCritDamage;

	public bool forcedCrit;

	public float totalRicoshotDamageBonus;

	public bool appliesSomaShred;

	public bool showArcFlash;

	public int arcFlashCooldown;

	public Vector2 arenaBoxPosition;

	public ArenaWallSystem.Box arenaBox;

	public bool brimstoneBullets;

	public bool fireBullet;

	public bool iceBullet;

	public bool shockBullet;

	public bool lifeBullet;

	public bool deepcoreBullet;

	public bool pearlBullet1;

	public bool pearlBullet2;

	public bool pearlBullet3;

	public bool betterLifeBullet1;

	public bool betterLifeBullet2;

	public float conditionalHomingRange;

	public bool grapeBeer;

	public int defExtraUpdates;

	public int timesPierced;

	private const float EmpressRainbowStreakSpreadOutCutoff = 140f;

	private const int EmpressLastingRainbowTotalDuration = 660;

	private const int EmpressLastingRainbowTimeBeforeDealingDamage = 60;

	private const int FishronSharknadoTotalDuration = 540;

	private const int FishronCthulhunadoTotalDuration = 840;

	private const int FishronTornadoTimeBeforeDealingDamage = 60;

	public int flatDRTimer;

	public int flatDR;

	public int multiplicativeDRTimer;

	public float multiplicativeDR;

	public bool hookCanSpawnFlower;

	public bool DealsDefenseDamage;

	public bool? buffedByOldFashioned;

	public bool nihilicArrow;

	public bool stealthStrike;

	public int stealthStrikeHitCount;

	public bool extorterBoost;

	public bool LocketClone;

	public bool CannotProc;

	public bool JewelSpikeSpawned;

	public int TransformerTimer;

	public bool overridesMinionDamagePrevention;

	public int ExplosiveEnchantCountdown;

	public const int ExplosiveEnchantTime = 2400;

	public float UpdatePriority;

	public int BloodstoneOrbValue;

	public int HomingTarget;

	public bool IgnoreBoCIllusions;

	private float isReelingIn;

	private float CatchTime;

	private float TimerToCatch;

	public int CaughtItemID;

	public float PersistentFishingData;

	public Vector2 PersistentFishingDataVector2;

	internal static SortedDictionary<int, IProjectileTweak[]> currentTweaks = null;

	internal static IProjectileTweak LocalIFramesOneHit = new LocalIFrameRule(-1);

	internal static IProjectileTweak NoPiercing = new PiercingExactRule(1);

	internal static IProjectileTweak InfinitePiercing = new PiercingExactRule(-1);

	public override bool InstancePerEntity => true;

	internal static IProjectileTweak DefenseDamage => new DefenseDamageRule(dd: true);

	internal static IProjectileTweak NoDefenseDamage => new DefenseDamageRule(dd: false);

	internal static IProjectileTweak DefaultIDStaticIFrames => new IDStaticIFrameRule(10);

	internal static IProjectileTweak IgnoreWater => new IgnoreWaterRule(iw: true);

	internal static IProjectileTweak DontIgnoreWater => new IgnoreWaterRule(iw: false);

	internal static IProjectileTweak SingleHitImmunity => new SingleHitImmunityRule(imm: true);

	internal static IProjectileTweak TileCollide => new TileCollideRule(tc: true);

	internal static IProjectileTweak NoTileCollide => new TileCollideRule(tc: false);

	internal static IProjectileTweak TrueMelee => new TrueMeleeRule();

	internal static IProjectileTweak TrueMeleeNoSpeed => new TrueMeleeNoSpeedRule();

	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		CreatedByPlayerDash = source is ProjectileSource_PlayerDashHit;
		if (source is EntitySource_ItemUse_WithAmmo)
		{
			extorterBoost = true;
		}
		if (Main.player[projectile.owner].Calamity().spiritOrigin && projectile.CountsAsClass<RangedDamageClass>())
		{
			projectile.CritChance += Main.player[projectile.owner].Calamity().spiritOriginCritBoost;
			projectile.Calamity().supercritHits = -1;
		}
		if (source is EntitySource_ItemUse_WithAmmo entitySource_ItemUse_WithAmmo)
		{
			Item item = entitySource_ItemUse_WithAmmo.Item;
			if (item != null && source is EntitySource_Parent { Entity: Player player } && player.Calamity().grapeBeer && (item.useAmmo == AmmoID.Bullet || item.useAmmo == AmmoID.Arrow || item.useAmmo == AmmoID.Dart || item.useAmmo == AmmoID.Rocket))
			{
				if (player.heldProj != projectile.whoAmI && projectile.aiStyle != 75 && projectile.damage > 0 && !CalamityProjectileSets.DoesNotGetHomingWithGrapeBeer[projectile.type])
				{
					ApplyGrapeBeer();
				}
				else
				{
					grapeBeer = true;
				}
			}
		}
		if (source is EntitySource_Parent { Entity: NPC npc })
		{
			if (!npc.friendly)
			{
				ParentNPCIndex = npc.whoAmI;
			}
		}
		else if (source is EntitySource_Parent { Entity: Projectile parent })
		{
			if (parent.Calamity().grapeBeer)
			{
				if (Main.player[projectile.owner].heldProj != projectile.whoAmI && projectile.aiStyle != 75 && projectile.damage > 0 && !CalamityProjectileSets.DoesNotGetHomingWithGrapeBeer[projectile.type])
				{
					ApplyGrapeBeer();
				}
				else
				{
					grapeBeer = true;
				}
			}
			if (parent.Calamity().IgnoreBoCIllusions)
			{
				IgnoreBoCIllusions = true;
			}
			if (parent.type == 89 && projectile.type == 90)
			{
				projectile.damage = (int)((float)projectile.damage * 0.55f);
			}
			if (parent.type == 130 && projectile.type == 131)
			{
				projectile.damage /= 2;
			}
			if (parent.type == 639 && projectile.type == 640)
			{
				projectile.damage /= 2;
			}
			if (parent.type == 478 && projectile.type == 480)
			{
				projectile.damage /= 2;
			}
			if (parent.type == 226 && projectile.type == 227)
			{
				projectile.damage = (int)((float)projectile.damage * 0.7f);
			}
		}
		if (source is EntitySource_OnHit { Context: "SetBonus_GhostHurt" })
		{
			projectile.damage /= 2;
		}
		void ApplyGrapeBeer()
		{
			grapeBeer = true;
			conditionalHomingRange = 600f;
			if (projectile.timeLeft > 300 * projectile.MaxUpdates)
			{
				projectile.timeLeft = 300 * projectile.MaxUpdates;
			}
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}
	}

	public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		binaryWriter.Write(ParentNPCIndex);
	}

	public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
	{
		ParentNPCIndex = binaryReader.ReadInt32();
	}

	public override void SetStaticDefaults()
	{
		for (int type = 0; type < ProjectileID.Count; type++)
		{
			if ((type >= 133 && type <= 144) || (type >= 776 && type <= 801))
			{
				ProjectileID.Sets.RocketsSkipDamageForPlayers[type] = true;
			}
		}
	}

	public override void SetDefaults(Projectile projectile)
	{
		if (projectile.type == 876)
		{
			projectile.originalDamage = projectile.damage;
		}
		SetDefaults_ApplyTweaks(projectile);
	}

	public override bool PreAI(Projectile projectile)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1155: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_1187: Unknown result type (might be due to invalid IL or missing references)
		//IL_1196: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11db: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1359: Unknown result type (might be due to invalid IL or missing references)
		//IL_135f: Unknown result type (might be due to invalid IL or missing references)
		//IL_136d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1377: Unknown result type (might be due to invalid IL or missing references)
		//IL_137d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1387: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_124d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1258: Unknown result type (might be due to invalid IL or missing references)
		//IL_1096: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_110a: Unknown result type (might be due to invalid IL or missing references)
		//IL_110f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_1130: Unknown result type (might be due to invalid IL or missing references)
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_130d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1601: Unknown result type (might be due to invalid IL or missing references)
		//IL_160c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1611: Unknown result type (might be due to invalid IL or missing references)
		//IL_1616: Unknown result type (might be due to invalid IL or missing references)
		//IL_1619: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1623: Unknown result type (might be due to invalid IL or missing references)
		//IL_1625: Unknown result type (might be due to invalid IL or missing references)
		//IL_162e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		//IL_158b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1660: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_142e: Unknown result type (might be due to invalid IL or missing references)
		//IL_143c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1446: Unknown result type (might be due to invalid IL or missing references)
		//IL_144c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1456: Unknown result type (might be due to invalid IL or missing references)
		//IL_145b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1460: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1488: Unknown result type (might be due to invalid IL or missing references)
		//IL_148d: Unknown result type (might be due to invalid IL or missing references)
		//IL_149d: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_166e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_251f: Unknown result type (might be due to invalid IL or missing references)
		//IL_167d: Unknown result type (might be due to invalid IL or missing references)
		//IL_168e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2808: Unknown result type (might be due to invalid IL or missing references)
		//IL_2813: Unknown result type (might be due to invalid IL or missing references)
		//IL_363c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3660: Unknown result type (might be due to invalid IL or missing references)
		//IL_3666: Unknown result type (might be due to invalid IL or missing references)
		//IL_368b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3695: Unknown result type (might be due to invalid IL or missing references)
		//IL_369a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_4230: Unknown result type (might be due to invalid IL or missing references)
		//IL_423b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3229: Unknown result type (might be due to invalid IL or missing references)
		//IL_323f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3245: Unknown result type (might be due to invalid IL or missing references)
		//IL_3297: Unknown result type (might be due to invalid IL or missing references)
		//IL_32a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_32a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_4573: Unknown result type (might be due to invalid IL or missing references)
		//IL_4578: Unknown result type (might be due to invalid IL or missing references)
		//IL_43e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_43ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_43f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_43f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_39ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_38cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_38f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_38f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_391e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3928: Unknown result type (might be due to invalid IL or missing references)
		//IL_392d: Unknown result type (might be due to invalid IL or missing references)
		//IL_383e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3844: Unknown result type (might be due to invalid IL or missing references)
		//IL_3849: Unknown result type (might be due to invalid IL or missing references)
		//IL_384e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3853: Unknown result type (might be due to invalid IL or missing references)
		//IL_3858: Unknown result type (might be due to invalid IL or missing references)
		//IL_385c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3864: Unknown result type (might be due to invalid IL or missing references)
		//IL_3869: Unknown result type (might be due to invalid IL or missing references)
		//IL_386d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3872: Unknown result type (might be due to invalid IL or missing references)
		//IL_3880: Unknown result type (might be due to invalid IL or missing references)
		//IL_3885: Unknown result type (might be due to invalid IL or missing references)
		//IL_4623: Unknown result type (might be due to invalid IL or missing references)
		//IL_4628: Unknown result type (might be due to invalid IL or missing references)
		//IL_462d: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4659: Unknown result type (might be due to invalid IL or missing references)
		//IL_465e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4660: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e49: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fe8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3fee: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_4006: Unknown result type (might be due to invalid IL or missing references)
		//IL_400b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4012: Unknown result type (might be due to invalid IL or missing references)
		//IL_4017: Unknown result type (might be due to invalid IL or missing references)
		//IL_4020: Unknown result type (might be due to invalid IL or missing references)
		//IL_402a: Unknown result type (might be due to invalid IL or missing references)
		//IL_402f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4034: Unknown result type (might be due to invalid IL or missing references)
		//IL_4afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2212: Unknown result type (might be due to invalid IL or missing references)
		//IL_4681: Unknown result type (might be due to invalid IL or missing references)
		//IL_45ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_45ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e71: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4054: Unknown result type (might be due to invalid IL or missing references)
		//IL_4064: Unknown result type (might be due to invalid IL or missing references)
		//IL_406a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4077: Unknown result type (might be due to invalid IL or missing references)
		//IL_407d: Unknown result type (might be due to invalid IL or missing references)
		//IL_407f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4084: Unknown result type (might be due to invalid IL or missing references)
		//IL_4089: Unknown result type (might be due to invalid IL or missing references)
		//IL_408c: Unknown result type (might be due to invalid IL or missing references)
		//IL_40b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_40b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_40c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_40d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_40d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_40de: Unknown result type (might be due to invalid IL or missing references)
		//IL_40e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_40ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_40f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_40fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1801: Unknown result type (might be due to invalid IL or missing references)
		//IL_1806: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3798: Unknown result type (might be due to invalid IL or missing references)
		//IL_37a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_37bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_37c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_358e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3594: Unknown result type (might be due to invalid IL or missing references)
		//IL_3599: Unknown result type (might be due to invalid IL or missing references)
		//IL_359e: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_35ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_35b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_35b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_35bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_35c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_4692: Unknown result type (might be due to invalid IL or missing references)
		//IL_4478: Unknown result type (might be due to invalid IL or missing references)
		//IL_447e: Unknown result type (might be due to invalid IL or missing references)
		//IL_448b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4491: Unknown result type (might be due to invalid IL or missing references)
		//IL_4493: Unknown result type (might be due to invalid IL or missing references)
		//IL_44b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_44bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_42de: Unknown result type (might be due to invalid IL or missing references)
		//IL_42f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_42fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_42ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_4304: Unknown result type (might be due to invalid IL or missing references)
		//IL_431a: Unknown result type (might be due to invalid IL or missing references)
		//IL_431e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4323: Unknown result type (might be due to invalid IL or missing references)
		//IL_4327: Unknown result type (might be due to invalid IL or missing references)
		//IL_4331: Unknown result type (might be due to invalid IL or missing references)
		//IL_4336: Unknown result type (might be due to invalid IL or missing references)
		//IL_4338: Unknown result type (might be due to invalid IL or missing references)
		//IL_4342: Unknown result type (might be due to invalid IL or missing references)
		//IL_4347: Unknown result type (might be due to invalid IL or missing references)
		//IL_4359: Unknown result type (might be due to invalid IL or missing references)
		//IL_4360: Unknown result type (might be due to invalid IL or missing references)
		//IL_4365: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ebf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d46: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_412e: Unknown result type (might be due to invalid IL or missing references)
		//IL_413e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4144: Unknown result type (might be due to invalid IL or missing references)
		//IL_4151: Unknown result type (might be due to invalid IL or missing references)
		//IL_4157: Unknown result type (might be due to invalid IL or missing references)
		//IL_4159: Unknown result type (might be due to invalid IL or missing references)
		//IL_415e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4163: Unknown result type (might be due to invalid IL or missing references)
		//IL_4166: Unknown result type (might be due to invalid IL or missing references)
		//IL_4186: Unknown result type (might be due to invalid IL or missing references)
		//IL_418c: Unknown result type (might be due to invalid IL or missing references)
		//IL_419d: Unknown result type (might be due to invalid IL or missing references)
		//IL_41a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_41ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_41bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_41c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_41ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_41d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_41d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_41de: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b16: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b37: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_34fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3510: Unknown result type (might be due to invalid IL or missing references)
		//IL_3523: Unknown result type (might be due to invalid IL or missing references)
		//IL_33c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_33db: Unknown result type (might be due to invalid IL or missing references)
		//IL_33e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_33fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2def: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df8: Unknown result type (might be due to invalid IL or missing references)
		//IL_291a: Unknown result type (might be due to invalid IL or missing references)
		//IL_291f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2921: Unknown result type (might be due to invalid IL or missing references)
		//IL_2924: Unknown result type (might be due to invalid IL or missing references)
		//IL_297f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2985: Unknown result type (might be due to invalid IL or missing references)
		//IL_298a: Unknown result type (might be due to invalid IL or missing references)
		//IL_298f: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_29bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_29c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_46bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_486e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_181d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1833: Unknown result type (might be due to invalid IL or missing references)
		//IL_18da: Unknown result type (might be due to invalid IL or missing references)
		//IL_18df: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_3545: Unknown result type (might be due to invalid IL or missing references)
		//IL_3559: Unknown result type (might be due to invalid IL or missing references)
		//IL_3417: Unknown result type (might be due to invalid IL or missing references)
		//IL_3426: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f89: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ff2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ff7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3036: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ba6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2933: Unknown result type (might be due to invalid IL or missing references)
		//IL_293a: Unknown result type (might be due to invalid IL or missing references)
		//IL_44c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_44c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_44dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_44e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_44e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_44e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_44fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4507: Unknown result type (might be due to invalid IL or missing references)
		//IL_450c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4524: Unknown result type (might be due to invalid IL or missing references)
		//IL_438d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4392: Unknown result type (might be due to invalid IL or missing references)
		//IL_439c: Unknown result type (might be due to invalid IL or missing references)
		//IL_43a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f96: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_4feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ff2: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5002: Unknown result type (might be due to invalid IL or missing references)
		//IL_5007: Unknown result type (might be due to invalid IL or missing references)
		//IL_500c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c62: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1904: Unknown result type (might be due to invalid IL or missing references)
		//IL_1909: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2950: Unknown result type (might be due to invalid IL or missing references)
		//IL_2952: Unknown result type (might be due to invalid IL or missing references)
		//IL_4718: Unknown result type (might be due to invalid IL or missing references)
		//IL_471a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d42: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d47: Unknown result type (might be due to invalid IL or missing references)
		//IL_4898: Unknown result type (might be due to invalid IL or missing references)
		//IL_48ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_48b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_48b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_48c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_48de: Unknown result type (might be due to invalid IL or missing references)
		//IL_48eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_48f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_48ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_4913: Unknown result type (might be due to invalid IL or missing references)
		//IL_4918: Unknown result type (might be due to invalid IL or missing references)
		//IL_491d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3083: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cda: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2682: Unknown result type (might be due to invalid IL or missing references)
		//IL_2689: Unknown result type (might be due to invalid IL or missing references)
		//IL_268e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2695: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_471d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4722: Unknown result type (might be due to invalid IL or missing references)
		//IL_46ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_4704: Unknown result type (might be due to invalid IL or missing references)
		//IL_5735: Unknown result type (might be due to invalid IL or missing references)
		//IL_573f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5744: Unknown result type (might be due to invalid IL or missing references)
		//IL_534a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5354: Unknown result type (might be due to invalid IL or missing references)
		//IL_5359: Unknown result type (might be due to invalid IL or missing references)
		//IL_5148: Unknown result type (might be due to invalid IL or missing references)
		//IL_514e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5153: Unknown result type (might be due to invalid IL or missing references)
		//IL_5171: Unknown result type (might be due to invalid IL or missing references)
		//IL_517f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5189: Unknown result type (might be due to invalid IL or missing references)
		//IL_518e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5195: Unknown result type (might be due to invalid IL or missing references)
		//IL_519b: Unknown result type (might be due to invalid IL or missing references)
		//IL_51a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_51aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_51af: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e44: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_30aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_30af: Unknown result type (might be due to invalid IL or missing references)
		//IL_30b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26de: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2346: Unknown result type (might be due to invalid IL or missing references)
		//IL_2349: Unknown result type (might be due to invalid IL or missing references)
		//IL_473c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4759: Unknown result type (might be due to invalid IL or missing references)
		//IL_476c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4772: Unknown result type (might be due to invalid IL or missing references)
		//IL_4774: Unknown result type (might be due to invalid IL or missing references)
		//IL_477b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4780: Unknown result type (might be due to invalid IL or missing references)
		//IL_4787: Unknown result type (might be due to invalid IL or missing references)
		//IL_57a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_57b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_5375: Unknown result type (might be due to invalid IL or missing references)
		//IL_537a: Unknown result type (might be due to invalid IL or missing references)
		//IL_531f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5329: Unknown result type (might be due to invalid IL or missing references)
		//IL_532e: Unknown result type (might be due to invalid IL or missing references)
		//IL_50bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_50c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_50cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_4955: Unknown result type (might be due to invalid IL or missing references)
		//IL_4969: Unknown result type (might be due to invalid IL or missing references)
		//IL_496e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4984: Unknown result type (might be due to invalid IL or missing references)
		//IL_498a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4991: Unknown result type (might be due to invalid IL or missing references)
		//IL_49af: Unknown result type (might be due to invalid IL or missing references)
		//IL_49b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_49c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_49c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_49d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_49e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_49f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_0937: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_30d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_30e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_30e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2755: Unknown result type (might be due to invalid IL or missing references)
		//IL_275c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2761: Unknown result type (might be due to invalid IL or missing references)
		//IL_272f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2734: Unknown result type (might be due to invalid IL or missing references)
		//IL_2738: Unknown result type (might be due to invalid IL or missing references)
		//IL_273d: Unknown result type (might be due to invalid IL or missing references)
		//IL_26fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2700: Unknown result type (might be due to invalid IL or missing references)
		//IL_2707: Unknown result type (might be due to invalid IL or missing references)
		//IL_270c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_58da: Unknown result type (might be due to invalid IL or missing references)
		//IL_58df: Unknown result type (might be due to invalid IL or missing references)
		//IL_5458: Unknown result type (might be due to invalid IL or missing references)
		//IL_545d: Unknown result type (might be due to invalid IL or missing references)
		//IL_53f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_53f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5404: Unknown result type (might be due to invalid IL or missing references)
		//IL_5409: Unknown result type (might be due to invalid IL or missing references)
		//IL_540f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5420: Unknown result type (might be due to invalid IL or missing references)
		//IL_53ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_53b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_53bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_53c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e98: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ebc: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ed5: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_30f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_30f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_30fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3109: Unknown result type (might be due to invalid IL or missing references)
		//IL_3114: Unknown result type (might be due to invalid IL or missing references)
		//IL_3121: Unknown result type (might be due to invalid IL or missing references)
		//IL_2782: Unknown result type (might be due to invalid IL or missing references)
		//IL_2789: Unknown result type (might be due to invalid IL or missing references)
		//IL_278e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e14: Unknown result type (might be due to invalid IL or missing references)
		//IL_312e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3133: Unknown result type (might be due to invalid IL or missing references)
		//IL_3141: Unknown result type (might be due to invalid IL or missing references)
		//IL_27bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_604e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6054: Unknown result type (might be due to invalid IL or missing references)
		//IL_6059: Unknown result type (might be due to invalid IL or missing references)
		//IL_605e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ee5: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_23bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_5818: Unknown result type (might be due to invalid IL or missing references)
		//IL_5822: Unknown result type (might be due to invalid IL or missing references)
		//IL_5827: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c88: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ca8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5917: Unknown result type (might be due to invalid IL or missing references)
		//IL_591c: Unknown result type (might be due to invalid IL or missing references)
		//IL_56b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_56ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_62d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_62e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_62e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_62e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_555e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5564: Unknown result type (might be due to invalid IL or missing references)
		//IL_556e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5573: Unknown result type (might be due to invalid IL or missing references)
		//IL_5597: Unknown result type (might be due to invalid IL or missing references)
		//IL_559d: Unknown result type (might be due to invalid IL or missing references)
		//IL_55d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_55db: Unknown result type (might be due to invalid IL or missing references)
		//IL_55e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_561b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5621: Unknown result type (might be due to invalid IL or missing references)
		//IL_562b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5630: Unknown result type (might be due to invalid IL or missing references)
		//IL_5655: Unknown result type (might be due to invalid IL or missing references)
		//IL_565b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5686: Unknown result type (might be due to invalid IL or missing references)
		//IL_5690: Unknown result type (might be due to invalid IL or missing references)
		//IL_5695: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_63ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_640f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6415: Unknown result type (might be due to invalid IL or missing references)
		//IL_6417: Unknown result type (might be due to invalid IL or missing references)
		//IL_63d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a65: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_68b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_68c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_68c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_674a: Unknown result type (might be due to invalid IL or missing references)
		//IL_6750: Unknown result type (might be due to invalid IL or missing references)
		//IL_6755: Unknown result type (might be due to invalid IL or missing references)
		//IL_675a: Unknown result type (might be due to invalid IL or missing references)
		//IL_6515: Unknown result type (might be due to invalid IL or missing references)
		//IL_651b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6520: Unknown result type (might be due to invalid IL or missing references)
		//IL_6525: Unknown result type (might be due to invalid IL or missing references)
		//IL_6536: Unknown result type (might be due to invalid IL or missing references)
		//IL_6541: Unknown result type (might be due to invalid IL or missing references)
		//IL_6546: Unknown result type (might be due to invalid IL or missing references)
		//IL_654d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6552: Unknown result type (might be due to invalid IL or missing references)
		//IL_655d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6562: Unknown result type (might be due to invalid IL or missing references)
		//IL_657e: Unknown result type (might be due to invalid IL or missing references)
		//IL_684e: Unknown result type (might be due to invalid IL or missing references)
		//IL_67e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_67e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_67f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_67f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6837: Unknown result type (might be due to invalid IL or missing references)
		//IL_65e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_60f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_60fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_6107: Unknown result type (might be due to invalid IL or missing references)
		//IL_610d: Unknown result type (might be due to invalid IL or missing references)
		//IL_610f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6136: Unknown result type (might be due to invalid IL or missing references)
		//IL_613b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_5eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ef1: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d53: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d28: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_664f: Unknown result type (might be due to invalid IL or missing references)
		//IL_65b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f37: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f44: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f63: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d72: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_66c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_661d: Unknown result type (might be due to invalid IL or missing references)
		//IL_614e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6153: Unknown result type (might be due to invalid IL or missing references)
		//IL_616f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6175: Unknown result type (might be due to invalid IL or missing references)
		//IL_6177: Unknown result type (might be due to invalid IL or missing references)
		//IL_617c: Unknown result type (might be due to invalid IL or missing references)
		//IL_6181: Unknown result type (might be due to invalid IL or missing references)
		//IL_6187: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ac2: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_5aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_5af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_59bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_59d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_59dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_59de: Unknown result type (might be due to invalid IL or missing references)
		//IL_59e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_59f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_59f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_59fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_6be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_6af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_67c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_67cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_67d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6686: Unknown result type (might be due to invalid IL or missing references)
		//IL_6d96: Unknown result type (might be due to invalid IL or missing references)
		//IL_6da1: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_66fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_61aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_61bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_61c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_61ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_61e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6031: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ba1: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_5bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c10: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_5feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_6e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_6e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_6e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_6e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_6e8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_6eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f66: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fef: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_7008: Unknown result type (might be due to invalid IL or missing references)
		//IL_700d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7012: Unknown result type (might be due to invalid IL or missing references)
		//IL_7025: Unknown result type (might be due to invalid IL or missing references)
		//IL_7209: Unknown result type (might be due to invalid IL or missing references)
		//IL_720e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7214: Unknown result type (might be due to invalid IL or missing references)
		//IL_721a: Unknown result type (might be due to invalid IL or missing references)
		//IL_7220: Unknown result type (might be due to invalid IL or missing references)
		//IL_722d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7232: Unknown result type (might be due to invalid IL or missing references)
		//IL_7125: Unknown result type (might be due to invalid IL or missing references)
		//IL_7135: Unknown result type (might be due to invalid IL or missing references)
		//IL_713b: Unknown result type (might be due to invalid IL or missing references)
		//IL_713d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7153: Unknown result type (might be due to invalid IL or missing references)
		//IL_715f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7164: Unknown result type (might be due to invalid IL or missing references)
		//IL_716a: Unknown result type (might be due to invalid IL or missing references)
		//IL_7170: Unknown result type (might be due to invalid IL or missing references)
		//IL_7176: Unknown result type (might be due to invalid IL or missing references)
		//IL_717b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7185: Unknown result type (might be due to invalid IL or missing references)
		//IL_718a: Unknown result type (might be due to invalid IL or missing references)
		//IL_71a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_71a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_71c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_71ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_71cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_70a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_70b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_70bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_70cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_70d1: Unknown result type (might be due to invalid IL or missing references)
		if (ProjectileID.Sets.LightPet[projectile.type] && Main.LocalPlayer.Calamity().ZoneAbyss)
		{
			EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource
			{
				center = projectile.Center,
				scale = 1f
			});
		}
		if (projectile.bobber && RunFishingMinigames(projectile))
		{
			return false;
		}
		HomingTarget = -1;
		if (projectile.type == 373)
		{
			return HornetMinionAI.DoHornetMinionAI(projectile);
		}
		if (projectile.type == 375)
		{
			return ImpMinionAI.DoImpMinionAI(projectile);
		}
		if (projectile.type == 317)
		{
			return RavenMinionAI.DoRavenMinionAI(projectile);
		}
		if (projectile.type == 967)
		{
			return HoundiusShootiusFireballAI.DoHoundiusShootiusFireballAI(projectile);
		}
		if (Main.player[projectile.owner].yoyoGlove && projectile.aiStyle == 99)
		{
			if (projectile.ai[2] == 0f)
			{
				projectile.ai[2] = projectile.damage;
			}
			int MainYoyo = -1;
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile proj = Main.projectile[x];
				if (proj.active && proj.type == projectile.type && proj.owner == projectile.owner)
				{
					MainYoyo = x;
					break;
				}
			}
			if (projectile.whoAmI != MainYoyo)
			{
				projectile.damage = (int)(projectile.ai[2] * 0.5f);
			}
			else
			{
				projectile.damage = (int)projectile.ai[2];
			}
		}
		if (projectile.aiStyle == 99 && projectile.ai[0] == -1f)
		{
			projectile.Kill();
		}
		if (projectile.minion && ExplosiveEnchantCountdown > 0)
		{
			ExplosiveEnchantCountdown--;
			if (ExplosiveEnchantCountdown <= 300)
			{
				if (Main.rand.NextBool(24))
				{
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, projectile.Center);
				}
				Dust dust = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(projectile.width, projectile.height) * 0.42f, 267);
				dust.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0.45f, 1f));
				dust.scale = Main.rand.NextFloat(1.4f, 1.65f);
				dust.fadeIn = 0.5f;
				dust.noGravity = true;
			}
			if (ExplosiveEnchantCountdown % 40 == 39 && Main.rand.NextBool(12))
			{
				int damage = (int)Main.player[projectile.owner].GetTotalDamage<SummonDamageClass>().ApplyTo(2000f);
				Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<SummonBrimstoneExplosionSmall>(), damage, 0f, projectile.owner);
			}
			if (ExplosiveEnchantCountdown <= 0)
			{
				SoundEngine.PlaySound(in SoundID.DD2_KoboldExplosion, projectile.Center);
				if (Main.myPlayer == projectile.owner)
				{
					if (projectile.minionSlots > 0f)
					{
						int damage2 = (int)Main.player[projectile.owner].GetTotalDamage<SummonDamageClass>().ApplyTo(6000f);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<SummonBrimstoneExplosion>(), damage2, 0f, projectile.owner);
					}
					projectile.Kill();
				}
			}
		}
		if (projectile.type == 270)
		{
			_ = projectile.ai[0];
			bool num = projectile.ai[0] == -1f;
			bool revSkeletronAcceleratingSkull = projectile.ai[0] == -2f;
			bool revSkeletronPrimeHomingSkull = projectile.ai[0] == -3f;
			if (num | revSkeletronPrimeHomingSkull)
			{
				projectile.alpha = 0;
			}
			if (projectile.alpha > 0)
			{
				projectile.alpha -= 75;
			}
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			projectile.frame++;
			if (projectile.frame > 2)
			{
				projectile.frame = 0;
			}
			if (revSkeletronAcceleratingSkull)
			{
				float maxVelocity = (CalamityWorld.death ? 18f : 15f);
				if (((Vector2)(ref projectile.velocity)).Length() < maxVelocity)
				{
					float acceleration = 1.015f;
					projectile.velocity *= acceleration;
					if (((Vector2)(ref projectile.velocity)).Length() > maxVelocity)
					{
						((Vector2)(ref projectile.velocity)).Normalize();
						projectile.velocity *= maxVelocity;
					}
				}
			}
			if (!num && !revSkeletronPrimeHomingSkull)
			{
				int numDust = (revSkeletronAcceleratingSkull ? 1 : 2);
				int dustType = (revSkeletronAcceleratingSkull ? 91 : 6);
				float dustScale = (revSkeletronAcceleratingSkull ? 1f : 2f);
				float dustVelocityOffset = (revSkeletronAcceleratingSkull ? 1f : 2f);
				for (int i = 0; i < numDust; i++)
				{
					Dust dust2 = Dust.NewDustDirect(new Vector2(projectile.position.X + 4f, projectile.position.Y + 4f), projectile.width - 8, projectile.height - 8, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), dustScale);
					dust2.position -= projectile.velocity * dustVelocityOffset;
					dust2.noGravity = true;
					dust2.velocity *= 0.3f;
				}
			}
			else
			{
				for (int j = 0; j < 2; j++)
				{
					int num174 = Dust.NewDust(new Vector2(projectile.position.X + 4f, projectile.position.Y + 4f), projectile.width - 8, projectile.height - 8, 5, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.5f);
					Dust obj = Main.dust[num174];
					obj.position -= projectile.velocity;
					Main.dust[num174].noGravity = true;
					Main.dust[num174].velocity.X *= 0.3f;
					Main.dust[num174].velocity.Y *= 0.3f;
				}
				int num175 = 0;
				num175 = Player.FindClosest(projectile.Center, 1, 1);
				projectile.ai[1]++;
				float homingStartTime = (revSkeletronPrimeHomingSkull ? 10f : 30f);
				float homingEndTime = (CalamityWorld.death ? 105f : 90f);
				if (revSkeletronPrimeHomingSkull)
				{
					homingEndTime += 60f;
				}
				if (Vector2.Distance(projectile.Center, Main.player[num175].Center) < ((!revSkeletronPrimeHomingSkull) ? 96f : ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 192f : 120f)) && projectile.ai[1] < homingEndTime)
				{
					projectile.ai[1] = homingEndTime;
				}
				if (projectile.ai[1] < homingEndTime && projectile.ai[1] > homingStartTime)
				{
					float num176 = ((Vector2)(ref projectile.velocity)).Length();
					Vector2 vector24 = Main.player[num175].Center - projectile.Center;
					((Vector2)(ref vector24)).Normalize();
					vector24 *= num176;
					float inertia = ((!CalamityWorld.death && !BossRushEvent.BossRushActive) ? (revSkeletronPrimeHomingSkull ? 25f : 30f) : (revSkeletronPrimeHomingSkull ? 20f : 25f));
					projectile.velocity = (projectile.velocity * (inertia - 1f) + vector24) / inertia;
					((Vector2)(ref projectile.velocity)).Normalize();
					projectile.velocity *= num176;
				}
				float maxVelocity2 = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 18f : 15f);
				float acceleration2 = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 1.02f : 1.015f);
				if (((Vector2)(ref projectile.velocity)).Length() < maxVelocity2)
				{
					projectile.velocity *= acceleration2;
				}
				if (projectile.localAI[0] == 0f)
				{
					projectile.localAI[0] = 1f;
					SoundEngine.PlaySound(in SoundID.Item8, projectile.Center);
					for (int k = 0; k < 10; k++)
					{
						int num177 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 2f);
						Main.dust[num177].noGravity = true;
						Main.dust[num177].velocity = projectile.Center - Main.dust[num177].position;
						((Vector2)(ref Main.dust[num177].velocity)).Normalize();
						Dust obj2 = Main.dust[num177];
						obj2.velocity *= -5f;
						Dust obj3 = Main.dust[num177];
						obj3.velocity += projectile.velocity / 2f;
					}
				}
			}
			if (projectile.ai[0] == 0f)
			{
				float num180 = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
				float num181 = projectile.localAI[0];
				if (num181 == 0f)
				{
					projectile.localAI[0] = num180;
					num181 = num180;
				}
				float num182 = projectile.position.X;
				float num183 = projectile.position.Y;
				float num184 = 300f;
				bool flag4 = false;
				int num185 = 0;
				if (projectile.ai[1] == 0f)
				{
					for (int l = 0; l < Main.maxNPCs; l++)
					{
						if (Main.npc[l].CanBeChasedBy(this) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(l + 1)))
						{
							float num187 = Main.npc[l].position.X + (float)(Main.npc[l].width / 2);
							float num188 = Main.npc[l].position.Y + (float)(Main.npc[l].height / 2);
							float num189 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num187) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num188);
							if (num189 < num184 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[l].position, Main.npc[l].width, Main.npc[l].height))
							{
								num184 = num189;
								num182 = num187;
								num183 = num188;
								flag4 = true;
								num185 = l;
							}
						}
					}
					if (flag4)
					{
						projectile.ai[1] = num185 + 1;
					}
					flag4 = false;
				}
				if (projectile.ai[1] > 0f)
				{
					int num190 = (int)(projectile.ai[1] - 1f);
					if (Main.npc[num190].active && Main.npc[num190].CanBeChasedBy(this, ignoreDontTakeDamage: true) && !Main.npc[num190].dontTakeDamage)
					{
						float num191 = Main.npc[num190].position.X + (float)(Main.npc[num190].width / 2);
						float num192 = Main.npc[num190].position.Y + (float)(Main.npc[num190].height / 2);
						if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num191) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num192) < 1000f)
						{
							flag4 = true;
							num182 = Main.npc[num190].position.X + (float)(Main.npc[num190].width / 2);
							num183 = Main.npc[num190].position.Y + (float)(Main.npc[num190].height / 2);
						}
					}
					else
					{
						projectile.ai[1] = 0f;
					}
				}
				if (!projectile.friendly)
				{
					flag4 = false;
				}
				if (flag4)
				{
					float num193 = num181;
					Vector2 vector25 = default(Vector2);
					((Vector2)(ref vector25))._002Ector(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num194 = num182 - vector25.X;
					float num195 = num183 - vector25.Y;
					float num196 = (float)Math.Sqrt(num194 * num194 + num195 * num195);
					num196 = num193 / num196;
					num194 *= num196;
					num195 *= num196;
					int num197 = 32;
					projectile.velocity.X = (projectile.velocity.X * (float)(num197 - 1) + num194) / (float)num197;
					projectile.velocity.Y = (projectile.velocity.Y * (float)(num197 - 1) + num195) / (float)num197;
				}
			}
			projectile.spriteDirection = projectile.direction;
			if (revSkeletronAcceleratingSkull)
			{
				projectile.rotation += (float)Math.PI / 90f * ((Vector2)(ref projectile.velocity)).Length() * (float)projectile.direction;
			}
			else if (projectile.direction < 0)
			{
				projectile.rotation = (float)Math.Atan2(0f - projectile.velocity.Y, 0f - projectile.velocity.X);
			}
			else
			{
				projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
			}
			return false;
		}
		if (projectile.type == 299 && projectile.ai[1] == 1f)
		{
			float spawnDustGateValue = 2f * (float)projectile.MaxUpdates;
			if (projectile.localAI[0] == spawnDustGateValue)
			{
				SoundEngine.PlaySound(in SoundID.Item8, projectile.Center);
				for (int m = 0; m < 20; m++)
				{
					int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 91, 0f, 0f, 100);
					Dust obj4 = Main.dust[dust3];
					obj4.velocity *= 3f;
					Dust obj5 = Main.dust[dust3];
					obj5.velocity += projectile.velocity * 0.75f;
					Main.dust[dust3].scale *= 1.2f;
					Main.dust[dust3].noGravity = true;
				}
			}
			projectile.localAI[0]++;
			if (projectile.localAI[0] > spawnDustGateValue)
			{
				for (int n = 0; n < 2; n++)
				{
					int dust4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 91, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
					Dust obj6 = Main.dust[dust4];
					obj6.velocity *= 0.6f;
					Main.dust[dust4].scale *= 1.4f;
					Main.dust[dust4].noGravity = true;
				}
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
			return false;
		}
		if (projectile.type == 811)
		{
			if (projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item17, projectile.Center);
				projectile.localAI[0] = 1f;
				for (int num198 = 0; num198 < 8; num198++)
				{
					Dust obj7 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 100)];
					obj7.velocity = (Main.rand.NextFloatDirection() * (float)Math.PI).ToRotationVector2() * 2f + projectile.velocity.SafeNormalize(Vector2.Zero) * 3f;
					obj7.scale = 1.5f;
					obj7.fadeIn = 1.7f;
					obj7.position = projectile.Center;
				}
			}
			projectile.alpha = 0;
			Dust obj8 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 100)];
			obj8.velocity = obj8.velocity / 4f + projectile.velocity / 2f;
			obj8.scale = 1.2f;
			obj8.position = projectile.Center + Main.rand.NextFloat() * projectile.velocity * 2f;
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
			return false;
		}
		if (projectile.type == 814)
		{
			if (projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item171, projectile.Center);
				projectile.localAI[0] = 1f;
				for (int num199 = 0; num199 < 8; num199++)
				{
					Dust dust5 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 100);
					dust5.velocity = (Main.rand.NextFloatDirection() * (float)Math.PI).ToRotationVector2() * 2f + projectile.velocity.SafeNormalize(Vector2.Zero) * 2f;
					dust5.scale = 0.9f;
					dust5.fadeIn = 1.1f;
					dust5.position = projectile.Center;
				}
			}
			projectile.alpha = 0;
			Dust dust6 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 100);
			dust6.velocity = dust6.velocity / 4f + projectile.velocity / 2f;
			dust6.scale = 1.2f;
			dust6.position = projectile.Center + Main.rand.NextFloat() * projectile.velocity * 2f;
			int trailLength = projectile.oldPos.Length / 2;
			for (int num200 = 1; num200 < trailLength && !(projectile.oldPos[num200] == Vector2.Zero); num200++)
			{
				if (Main.rand.NextBool(3))
				{
					Dust dust7 = Dust.NewDustDirect(projectile.oldPos[num200], projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y, 100);
					dust7.velocity = dust7.velocity / 4f + projectile.velocity / 2f;
					dust7.scale = 1.2f;
					dust7.position = projectile.oldPos[num200] + projectile.Size / 2f + Main.rand.NextFloat() * projectile.velocity * 2f;
				}
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
			return false;
		}
		if (projectile.type == 683 || projectile.type == 922)
		{
			float maxHitboxSize = 30f;
			if (projectile.type == 922)
			{
				maxHitboxSize = 20f;
			}
			projectile.ai[0]++;
			if (projectile.ai[0] <= 0f)
			{
				if (projectile.ai[0] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item167, projectile.Center);
				}
				return false;
			}
			if (projectile.ai[0] > 9f)
			{
				projectile.Kill();
				return false;
			}
			projectile.velocity = Vector2.Zero;
			projectile.position = projectile.Center;
			projectile.Size = new Vector2(16f, 8f) * MathHelper.Lerp(5f, maxHitboxSize, Utils.GetLerpValue(0f, 9f, projectile.ai[0]));
			projectile.Center = projectile.position;
			Point point = projectile.TopLeft.ToTileCoordinates();
			Point point2 = projectile.BottomRight.ToTileCoordinates();
			int num201 = point.X / 2 + point2.X / 2;
			int num202 = projectile.width / 2;
			if ((int)projectile.ai[0] % 3 != 0)
			{
				return false;
			}
			int num203 = (int)projectile.ai[0] / 3;
			for (int num204 = point.X; num204 <= point2.X; num204++)
			{
				for (int num205 = point.Y; num205 <= point2.Y; num205++)
				{
					if (Vector2.Distance(projectile.Center, new Vector2((float)(num204 * 16), (float)(num205 * 16))) > (float)num202)
					{
						continue;
					}
					Tile tileSafely = Framing.GetTileSafely(num204, num205);
					if (!tileSafely.HasTile || (!TileID.Sets.Platforms[tileSafely.TileType] && tileSafely.TileType != 380))
					{
						if (!tileSafely.HasTile || !Main.tileSolid[tileSafely.TileType] || Main.tileSolidTop[tileSafely.TileType] || Main.tileFrameImportant[tileSafely.TileType])
						{
							continue;
						}
						Tile tileSafely2 = Framing.GetTileSafely(num204, num205 - 1);
						if (tileSafely2.HasTile && Main.tileSolid[tileSafely2.TileType] && !Main.tileSolidTop[tileSafely2.TileType])
						{
							continue;
						}
					}
					int num206 = WorldGen.KillTile_GetTileDustAmount(fail: true, tileSafely, num204, num205);
					for (int num207 = 0; num207 < num206; num207++)
					{
						Dust obj9 = Main.dust[WorldGen.KillTile_MakeTileDust(num204, num205, tileSafely)];
						obj9.velocity.Y -= 3f + (float)num203 * 1.5f;
						obj9.velocity.Y *= Main.rand.NextFloat();
						obj9.velocity.Y *= 0.75f;
						obj9.scale += (float)num203 * 0.03f;
					}
					if (num203 >= 2)
					{
						if (projectile.type == 922)
						{
							Color newColor = NPC.AI_121_QueenSlime_GetDustColor();
							((Color)(ref newColor)).A = 150;
							for (int num208 = 0; num208 < num206 - 1; num208++)
							{
								int num209 = Dust.NewDust(projectile.position, 12, 12, 4, 0f, 0f, 50, newColor, 1.5f);
								Main.dust[num209].velocity.Y -= 0.1f + (float)num203 * 0.5f;
								Main.dust[num209].velocity.Y *= Main.rand.NextFloat();
								Main.dust[num209].velocity.X *= Main.rand.NextFloatDirection() * 3f;
								Main.dust[num209].position = new Vector2((float)(num204 * 16 + Main.rand.Next(16)), (float)(num205 * 16 + Main.rand.Next(16)));
								if (!Main.rand.NextBool(3))
								{
									Dust obj10 = Main.dust[num209];
									obj10.velocity *= 0.5f;
									Main.dust[num209].noGravity = true;
								}
							}
						}
						else
						{
							for (int num210 = 0; num210 < num206 - 1; num210++)
							{
								Dust obj11 = Main.dust[WorldGen.KillTile_MakeTileDust(num204, num205, tileSafely)];
								obj11.velocity.Y -= 1f + (float)num203;
								obj11.velocity.Y *= Main.rand.NextFloat();
								obj11.velocity.Y *= 0.75f;
							}
						}
					}
					if (num206 <= 0 || Main.rand.NextBool(3))
					{
						continue;
					}
					float num211 = (float)Math.Abs(num201 - num204) / (maxHitboxSize / 2f);
					if (projectile.type == 922)
					{
						Color newColor2 = NPC.AI_121_QueenSlime_GetDustColor();
						((Color)(ref newColor2)).A = 150;
						for (int num212 = 0; num212 < 3; num212++)
						{
							int num213 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 50, newColor2, 2f - (float)num203 * 0.15f + num211 * 0.5f);
							Main.dust[num213].velocity.Y -= 0.1f + (float)num203 * 0.5f + num211 * (float)num203 * 1f;
							Main.dust[num213].velocity.Y *= Main.rand.NextFloat();
							Main.dust[num213].velocity.X *= Main.rand.NextFloatDirection() * 3f;
							Main.dust[num213].position = new Vector2((float)(num204 * 16 + 20), (float)(num205 * 16 + 20));
							if (!Main.rand.NextBool(3))
							{
								Dust obj12 = Main.dust[num213];
								obj12.velocity *= 0.5f;
								Main.dust[num213].noGravity = true;
							}
						}
					}
					else
					{
						Gore gore = Gore.NewGoreDirect(projectile.GetSource_FromAI(), projectile.position, Vector2.Zero, 61 + Main.rand.Next(3), 1f - (float)num203 * 0.15f + num211 * 0.5f);
						gore.velocity.Y -= 0.1f + (float)num203 * 0.5f + num211 * (float)num203 * 1f;
						gore.velocity.Y *= Main.rand.NextFloat();
						gore.position = new Vector2((float)(num204 * 16 + 20), (float)(num205 * 16 + 20));
					}
				}
			}
			return false;
		}
		if (projectile.type == 348 && projectile.ai[1] > 0f)
		{
			if (projectile.ai[0] < 0f)
			{
				projectile.ai[0]++;
			}
			else if (((Vector2)(ref projectile.velocity)).Length() < projectile.ai[1])
			{
				projectile.velocity *= 1.04f;
				if (((Vector2)(ref projectile.velocity)).Length() > projectile.ai[1])
				{
					((Vector2)(ref projectile.velocity)).Normalize();
					projectile.velocity *= projectile.ai[1];
				}
			}
			else if (projectile.ai[0] == 0f || projectile.ai[0] == 2f)
			{
				projectile.scale += 0.005f;
				projectile.alpha -= 25;
				if (projectile.alpha <= 0)
				{
					projectile.ai[0] = 1f;
					projectile.alpha = 0;
				}
			}
			else if (projectile.ai[0] == 1f)
			{
				projectile.scale -= 0.005f;
				projectile.alpha += 25;
				if (projectile.alpha >= 255)
				{
					projectile.ai[0] = 2f;
					projectile.alpha = 255;
				}
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
			return false;
		}
		if (projectile.type == 385)
		{
			if (projectile.ai[1] == 0f)
			{
				float num553 = 4f;
				float num554 = (float)(Math.Cos((float)Math.PI / 15f * projectile.ai[0]) - 0.5) * num553;
				projectile.velocity.Y -= num554;
				projectile.ai[0]++;
				num554 = (float)(Math.Cos((float)Math.PI / 15f * projectile.ai[0]) - 0.5) * num553;
				projectile.velocity.Y += num554;
				projectile.localAI[0]++;
				if (projectile.localAI[0] > 10f)
				{
					projectile.alpha -= 5;
					if (projectile.alpha < 100)
					{
						projectile.alpha = 100;
					}
					projectile.rotation += projectile.velocity.X * 0.1f;
					projectile.frame = (int)(projectile.localAI[0] / 3f) % 3;
				}
				if (projectile.wet)
				{
					projectile.position.Y -= 16f;
					projectile.Kill();
				}
				return false;
			}
			if (projectile.ai[1] < 0f)
			{
				projectile.timeLeft -= 2;
				float num624 = -2f;
				float num625 = (float)(Math.Cos((float)Math.PI / 15f * projectile.ai[0]) - 0.5) * num624;
				projectile.velocity.Y -= num625;
				projectile.ai[0]++;
				num625 = (float)(Math.Cos((float)Math.PI / 15f * projectile.ai[0]) - 0.5) * num624;
				projectile.velocity.Y += num625;
				projectile.localAI[0]++;
				if (projectile.localAI[0] > 10f)
				{
					projectile.alpha -= 5;
					if (projectile.alpha < 100)
					{
						projectile.alpha = 100;
					}
					projectile.rotation += projectile.velocity.X * 0.1f;
					projectile.frame = (int)(projectile.localAI[0] / 3f) % 3;
				}
				if (projectile.wet)
				{
					projectile.position.Y -= 16f;
					projectile.Kill();
				}
				return false;
			}
		}
		else if (projectile.type == 386)
		{
			if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
			{
				bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
				int num626 = 16;
				int num627 = 16;
				float segmentScale = (death ? 2.5f : 2f);
				int segmentWidth = 150;
				int segmentHeight = 42;
				if (projectile.velocity.X != 0f)
				{
					projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));
				}
				projectile.frameCounter++;
				if (projectile.frameCounter > 2)
				{
					projectile.frame++;
					projectile.frameCounter = 0;
				}
				if (projectile.frame >= 6)
				{
					projectile.frame = 0;
				}
				if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
				{
					projectile.localAI[0] = 1f;
					projectile.position.X += projectile.width / 2;
					projectile.position.Y += projectile.height / 2;
					projectile.scale = ((float)(num626 + num627) - projectile.ai[1]) * segmentScale / (float)(num627 + num626);
					projectile.width = (int)((float)segmentWidth * projectile.scale);
					projectile.height = (int)((float)segmentHeight * projectile.scale);
					projectile.position.X -= projectile.width / 2;
					projectile.position.Y -= projectile.height / 2;
					projectile.netUpdate = true;
				}
				if (projectile.ai[1] != -1f)
				{
					projectile.scale = ((float)(num626 + num627) - projectile.ai[1]) * segmentScale / (float)(num627 + num626);
					projectile.width = (int)((float)segmentWidth * projectile.scale);
					projectile.height = (int)((float)segmentHeight * projectile.scale);
				}
				int maxAlpha = 150;
				int minAlpha = 100;
				if (projectile.timeLeft > 780)
				{
					maxAlpha = 220;
					minAlpha = 200;
				}
				if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.alpha -= 30;
					if (projectile.alpha < minAlpha)
					{
						projectile.alpha = minAlpha;
					}
				}
				else
				{
					projectile.alpha += 30;
					if (projectile.alpha > maxAlpha)
					{
						projectile.alpha = maxAlpha;
					}
				}
				if (projectile.ai[0] > 0f)
				{
					projectile.ai[0]--;
				}
				if (projectile.ai[0] == 1f && projectile.ai[1] > 0f && projectile.owner == Main.myPlayer)
				{
					projectile.netUpdate = true;
					Vector2 center = projectile.Center;
					center.Y -= (float)segmentHeight * projectile.scale / 2f;
					float num628 = ((float)(num626 + num627) - projectile.ai[1] + 1f) * segmentScale / (float)(num627 + num626);
					center.Y -= (float)segmentHeight * num628 / 2f;
					center.Y += 2f;
					float segmentSpawnDelay = 10f;
					Projectile.NewProjectile(projectile.GetSource_FromThis(), center, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, segmentSpawnDelay, projectile.ai[1] - 1f);
					int sharkronSpawnGateValue = (death ? 2 : 3);
					if ((int)projectile.ai[1] % sharkronSpawnGateValue == 0 && projectile.ai[1] != 0f)
					{
						int sharkron = NPC.NewNPC(projectile.GetSource_FromAI(), (int)center.X, (int)center.Y, 373);
						Main.npc[sharkron].velocity = projectile.velocity;
						Main.npc[sharkron].scale = 1.5f;
						Main.npc[sharkron].netUpdate = true;
						Main.npc[sharkron].ai[2] = projectile.width;
						Main.npc[sharkron].ai[3] = -1.5f;
					}
				}
				if (projectile.ai[0] <= 0f)
				{
					float widthSwayScale = (float)projectile.width / 5f * 2.5f;
					float sway = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)projectile.ai[0])) - 0.5) * widthSwayScale;
					projectile.position.X -= sway * (float)(-projectile.direction);
					projectile.ai[0]--;
					sway = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)projectile.ai[0])) - 0.5) * widthSwayScale;
					projectile.position.X += sway * (float)(-projectile.direction);
				}
				return false;
			}
			int minAlpha2 = 100;
			if (projectile.timeLeft > 780)
			{
				minAlpha2 = 200;
			}
			int alphaChange = 30;
			if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
			{
				projectile.alpha -= alphaChange;
				if (projectile.alpha < minAlpha2 + alphaChange)
				{
					projectile.alpha = minAlpha2 + alphaChange;
				}
			}
		}
		else
		{
			if (projectile.type == 873 && projectile.hostile)
			{
				bool death2 = CalamityWorld.death || BossRushEvent.BossRushActive;
				bool spreadOut = false;
				bool homeIn = false;
				float spreadOutCutoffTime = 140f;
				float homeInCutoffTime = ((!NPC.ShouldEmpressBeEnraged()) ? (death2 ? 70f : 80f) : (death2 ? 55f : 65f));
				float spreadDeceleration = 0.97f;
				float minAcceleration = (death2 ? 0.075f : 0.05f);
				float maxAcceleration = (death2 ? 0.15f : 0.1f);
				float homingVelocity = (death2 ? 36f : 30f);
				float maxVelocity3 = homingVelocity * 1.5f;
				float accelerationToMaxVelocity = 1.01f;
				if ((float)projectile.timeLeft > spreadOutCutoffTime)
				{
					spreadOut = true;
				}
				else if ((float)projectile.timeLeft > homeInCutoffTime)
				{
					homeIn = true;
				}
				if (spreadOut)
				{
					float spreadVelocity = (float)Math.Cos((float)projectile.whoAmI % 6f / 6f + projectile.position.X / 320f + projectile.position.Y / 160f);
					projectile.velocity *= spreadDeceleration;
					projectile.velocity = projectile.velocity.RotatedBy(spreadVelocity * ((float)Math.PI * 2f) * 0.125f * 1f / 30f);
				}
				if (homeIn)
				{
					int playerIndex = (int)projectile.ai[0];
					Vector2 velocity = projectile.velocity;
					if (Main.player.IndexInRange(playerIndex))
					{
						Player player = Main.player[playerIndex];
						velocity = projectile.DirectionTo(player.Center) * homingVelocity;
					}
					float amount = MathHelper.Lerp(minAcceleration, maxAcceleration, Utils.GetLerpValue(spreadOutCutoffTime, 30f, projectile.timeLeft, clamped: true));
					projectile.velocity = Vector2.SmoothStep(projectile.velocity, velocity, amount);
				}
				else if (((Vector2)(ref projectile.velocity)).Length() < maxVelocity3)
				{
					projectile.velocity *= accelerationToMaxVelocity;
					if (((Vector2)(ref projectile.velocity)).Length() > maxVelocity3)
					{
						((Vector2)(ref projectile.velocity)).Normalize();
						projectile.velocity *= maxVelocity3;
					}
				}
				projectile.Opacity = (spreadOut ? 0.4f : Utils.GetLerpValue(240f, 220f, projectile.timeLeft, clamped: true));
				projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				return false;
			}
			if (projectile.type == 465)
			{
				if (NPC.AnyNPCs(439))
				{
					if (projectile.localAI[1] == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item121, projectile.Center);
						projectile.localAI[1] = 1f;
					}
					if (projectile.ai[0] < 180f)
					{
						projectile.alpha -= 5;
						if (projectile.alpha < 0)
						{
							projectile.alpha = 0;
						}
					}
					else
					{
						projectile.alpha += 5;
						if (projectile.alpha > 255)
						{
							projectile.alpha = 255;
							projectile.Kill();
							return false;
						}
					}
					ref float reference = ref projectile.ai[0];
					float num629 = reference;
					reference = num629 + 1f;
					if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 180f && Main.netMode != 1)
					{
						int[] array6 = new int[2];
						Vector2[] array7 = (Vector2[])(object)new Vector2[2];
						int num731 = 0;
						float num732 = 2000f;
						for (int num733 = 0; num733 < 255; num733++)
						{
							if (!Main.player[num733].active || Main.player[num733].dead)
							{
								continue;
							}
							Vector2 center9 = Main.player[num733].Center;
							if (Vector2.Distance(center9, projectile.Center) < num732 && Collision.CanHit(projectile.Center, 1, 1, center9, 1, 1))
							{
								array6[num731] = num733;
								array7[num731] = center9;
								if (++num731 >= array7.Length)
								{
									break;
								}
							}
						}
						for (int num734 = 0; num734 < num731; num734++)
						{
							Vector2 vector52 = array7[num734] - projectile.Center;
							float ai = Main.rand.Next(100);
							Vector2 vector53 = Vector2.Normalize(vector52.RotatedByRandom(0.7853981852531433)) * 7f;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vector53, 466, projectile.damage, 0f, Main.myPlayer, vector52.ToRotation(), ai);
						}
					}
					Lighting.AddLight(projectile.Center, 0.4f, 0.85f, 0.9f);
					if (++projectile.frameCounter >= 4)
					{
						projectile.frameCounter = 0;
						if (++projectile.frame >= Main.projFrames[projectile.type])
						{
							projectile.frame = 0;
						}
					}
					if (projectile.alpha >= 150 || !(projectile.ai[0] < 180f))
					{
						return false;
					}
					for (int num735 = 0; num735 < 1; num735++)
					{
						float num737 = (float)Main.rand.NextDouble() * 1f - 0.5f;
						if (num737 < -0.5f)
						{
							num737 = -0.5f;
						}
						if (num737 > 0.5f)
						{
							num737 = 0.5f;
						}
						Vector2 value40 = Utils.RotatedBy(new Vector2((float)(-projectile.width) * 0.2f * projectile.scale, 0f), (double)(num737 * ((float)Math.PI * 2f)), default(Vector2)).RotatedBy(projectile.velocity.ToRotation());
						Dust dust8 = Dust.NewDustDirect(projectile.Center - Vector2.One * 5f, 10, 10, 226, (0f - projectile.velocity.X) / 3f, (0f - projectile.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
						dust8.position = projectile.Center + value40;
						dust8.velocity = Vector2.Normalize(dust8.position - projectile.Center) * 2f;
						dust8.noGravity = true;
					}
					for (int num738 = 0; num738 < 1; num738++)
					{
						float num740 = (float)Main.rand.NextDouble() * 1f - 0.5f;
						if (num740 < -0.5f)
						{
							num740 = -0.5f;
						}
						if (num740 > 0.5f)
						{
							num740 = 0.5f;
						}
						Vector2 value41 = Utils.RotatedBy(new Vector2((float)(-projectile.width) * 0.6f * projectile.scale, 0f), (double)(num740 * ((float)Math.PI * 2f)), default(Vector2)).RotatedBy(projectile.velocity.ToRotation());
						Dust dust9 = Dust.NewDustDirect(projectile.Center - Vector2.One * 5f, 10, 10, 226, (0f - projectile.velocity.X) / 3f, (0f - projectile.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
						dust9.velocity = Vector2.Zero;
						dust9.position = projectile.Center + value41;
						dust9.noGravity = true;
					}
					return false;
				}
			}
			else
			{
				if (projectile.type == 659)
				{
					float maxSpeed = 12f;
					int accelerationTime = 30;
					if (projectile.localAI[0] > 0f)
					{
						projectile.localAI[0]--;
					}
					if (projectile.localAI[0] == 0f && projectile.ai[0] < 0f && projectile.owner == Main.myPlayer)
					{
						projectile.localAI[0] = 5f;
						for (int num741 = 0; num741 < Main.maxNPCs; num741++)
						{
							NPC nPC13 = Main.npc[num741];
							if (nPC13.CanBeChasedBy(this) && ((projectile.ai[0] < 0f || Main.npc[(int)projectile.ai[0]].Distance(projectile.Center) > nPC13.Distance(projectile.Center)) & (nPC13.Distance(projectile.Center) < 500f)) && (Collision.CanHitLine(projectile.Center, 0, 0, nPC13.Center, 0, 0) || Collision.CanHitLine(projectile.Center, 0, 0, nPC13.Top, 0, 0)))
							{
								projectile.ai[0] = num741;
							}
						}
						if (projectile.ai[0] >= 0f)
						{
							projectile.timeLeft = 300;
							projectile.netUpdate = true;
						}
					}
					if (projectile.timeLeft > 30 && projectile.alpha > 0)
					{
						projectile.alpha -= 12;
					}
					if (projectile.timeLeft > 30 && projectile.alpha < 128 && Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
					{
						projectile.alpha = 128;
					}
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					if (++projectile.frameCounter > 4)
					{
						projectile.frameCounter = 0;
						if (++projectile.frame >= 4)
						{
							projectile.frame = 0;
						}
					}
					float num1035 = 0.5f;
					if (projectile.timeLeft < 120)
					{
						num1035 = 1.1f;
					}
					if (projectile.timeLeft < 60)
					{
						num1035 = 1.6f;
					}
					projectile.ai[1]++;
					_ = projectile.ai[1] / 180f;
					for (float num1037 = 0f; num1037 < 3f; num1037++)
					{
						if (Main.rand.NextBool(3))
						{
							Dust shflame = Dust.NewDustDirect(projectile.Center, 0, 0, 27, 0f, -2f, 200);
							shflame.position = projectile.Center + Vector2.UnitY.RotatedBy(num1037 * ((float)Math.PI * 2f) / 3f + projectile.ai[1]) * 10f;
							shflame.noGravity = true;
							shflame.velocity = projectile.DirectionFrom(shflame.position);
							shflame.scale = num1035;
							shflame.fadeIn = 0.5f;
						}
					}
					if (projectile.timeLeft > 2 && Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
					{
						projectile.timeLeft = 2;
					}
					int num1038 = (int)projectile.ai[0];
					if (num1038 >= 0 && Main.npc[num1038].active)
					{
						if (projectile.Distance(Main.npc[num1038].Center) > 1f)
						{
							Vector2 vector106 = projectile.DirectionTo(Main.npc[num1038].Center).SafeNormalize(Vector2.UnitX);
							float length = ((Vector2)(ref projectile.velocity)).Length();
							float step = maxSpeed / (float)accelerationTime;
							if (length >= maxSpeed)
							{
								step = 0f;
							}
							projectile.velocity = vector106 * (length + step);
							if (length >= maxSpeed && (projectile.Center + projectile.velocity).Distance(Main.npc[num1038].Center) > projectile.Center.Distance(Main.npc[num1038].Center))
							{
								projectile.velocity = Vector2.Zero;
								projectile.Center = Main.npc[num1038].Center;
							}
						}
						return false;
					}
					if (projectile.ai[0] == -1f && projectile.timeLeft > 5)
					{
						projectile.timeLeft = 5;
					}
					if (projectile.ai[0] == -2f && projectile.timeLeft > 180)
					{
						projectile.timeLeft = 180;
					}
					if (projectile.ai[0] >= 0f)
					{
						projectile.ai[0] = -1f;
						projectile.netUpdate = true;
					}
					return false;
				}
				if (projectile.type == 207)
				{
					if (projectile.alpha < 170)
					{
						int totalDust = 5;
						for (int num1039 = 0; num1039 < totalDust; num1039++)
						{
							float x2 = projectile.position.X - projectile.velocity.X / (float)totalDust * (float)num1039;
							float y2 = projectile.position.Y - projectile.velocity.Y / (float)totalDust * (float)num1039;
							int dust10 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 75);
							Main.dust[dust10].alpha = projectile.alpha;
							Main.dust[dust10].position.X = x2;
							Main.dust[dust10].position.Y = y2;
							Dust obj13 = Main.dust[dust10];
							obj13.velocity *= 0f;
							Main.dust[dust10].noGravity = true;
						}
					}
					float velocityLength = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
					float cachedVelocityLength = projectile.localAI[0];
					if (cachedVelocityLength == 0f)
					{
						projectile.localAI[0] = velocityLength;
						cachedVelocityLength = velocityLength;
					}
					if (projectile.alpha > 0)
					{
						projectile.alpha -= 25;
					}
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					float posX = projectile.position.X;
					float posY = projectile.position.Y;
					float homingDistance = 300f;
					bool homeIn2 = false;
					int target = 0;
					if (projectile.ai[1] == 0f)
					{
						for (int num1040 = 0; num1040 < Main.maxNPCs; num1040++)
						{
							if (Main.npc[num1040].CanBeChasedBy(this) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num1040 + 1)))
							{
								float targetCenterX = Main.npc[num1040].Center.X;
								float targetCenterY = Main.npc[num1040].Center.Y;
								float targetDistance = Math.Abs(projectile.Center.X - targetCenterX) + Math.Abs(projectile.Center.Y - targetCenterY);
								if (targetDistance < homingDistance && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num1040].position, Main.npc[num1040].width, Main.npc[num1040].height))
								{
									homingDistance = targetDistance;
									posX = targetCenterX;
									posY = targetCenterY;
									homeIn2 = true;
									target = num1040;
								}
							}
						}
						if (homeIn2)
						{
							projectile.ai[1] = target + 1;
						}
						homeIn2 = false;
					}
					if (projectile.ai[1] > 0f)
					{
						int targetIndex = (int)(projectile.ai[1] - 1f);
						if (Main.npc[targetIndex].active && Main.npc[targetIndex].CanBeChasedBy(this, ignoreDontTakeDamage: true) && !Main.npc[targetIndex].dontTakeDamage)
						{
							float targetCenterX2 = Main.npc[targetIndex].Center.X;
							float targetCenterY2 = Main.npc[targetIndex].Center.Y;
							float homingCutOffDistance = 1000f;
							if (Math.Abs(projectile.Center.X - targetCenterX2) + Math.Abs(projectile.Center.Y - targetCenterY2) < homingCutOffDistance)
							{
								homeIn2 = true;
								posX = Main.npc[targetIndex].Center.X;
								posY = Main.npc[targetIndex].Center.Y;
							}
						}
						else
						{
							projectile.ai[1] = 0f;
						}
					}
					if (!projectile.friendly)
					{
						homeIn2 = false;
					}
					if (homeIn2)
					{
						int inertia2 = 8;
						float homingSpeed = cachedVelocityLength;
						Vector2 homeDirection = (new Vector2(posX, posY) - projectile.Center).SafeNormalize(Vector2.UnitY);
						projectile.velocity = (projectile.velocity * (float)inertia2 + homeDirection * homingSpeed) / ((float)inertia2 + 1f);
					}
					projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
					return false;
				}
				if (projectile.type == 305)
				{
					projectile.HealingProjectile((int)projectile.ai[1], (int)projectile.ai[0], 4f, 15f);
					int dust11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 183, 0f, 0f, 100);
					Main.dust[dust11].noGravity = true;
					Dust obj14 = Main.dust[dust11];
					obj14.velocity *= 0f;
					Main.dust[dust11].position.X -= projectile.velocity.X * 0.2f;
					Main.dust[dust11].position.Y += projectile.velocity.Y * 0.2f;
					return false;
				}
				if (projectile.type == 356)
				{
					projectile.ai[1]++;
					if (projectile.ai[1] >= 60f)
					{
						projectile.friendly = true;
						int target2 = (int)projectile.ai[0];
						if (Main.myPlayer == projectile.owner && (target2 == -1 || !Main.npc[target2].CanBeChasedBy(projectile)))
						{
							target2 = -1;
							int[] array8 = new int[Main.maxNPCs];
							int randomTargets = 0;
							float homingDistance2 = 800f;
							for (int num1041 = 0; num1041 < Main.maxNPCs; num1041++)
							{
								if (Main.npc[num1041].CanBeChasedBy(projectile) && Math.Abs(Main.npc[num1041].Center.X - projectile.Center.X) + Math.Abs(Main.npc[num1041].Center.Y - projectile.Center.Y) < homingDistance2)
								{
									array8[randomTargets] = num1041;
									randomTargets++;
								}
							}
							if (randomTargets == 0)
							{
								projectile.Kill();
								return false;
							}
							target2 = array8[Main.rand.Next(randomTargets)];
							projectile.ai[0] = target2;
							projectile.netUpdate = true;
						}
						if (target2 != -1)
						{
							int inertia3 = 30;
							float homingSpeed2 = 4f;
							Vector2 homeDirection2 = (Main.npc[target2].Center - projectile.Center).SafeNormalize(Vector2.UnitY);
							projectile.velocity = (projectile.velocity * (float)inertia3 + homeDirection2 * homingSpeed2) / ((float)inertia3 + 1f);
						}
					}
					int maxDust = 3;
					float dustOffsetMultiplier = 1f / (float)maxDust;
					for (int num1042 = 0; num1042 < maxDust; num1042++)
					{
						float dustOffsetX = projectile.velocity.X * dustOffsetMultiplier * (float)num1042;
						float dustOffsetY = (0f - projectile.velocity.Y * dustOffsetMultiplier) * (float)num1042;
						int dust12 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 175, 0f, 0f, 100, default(Color), 1.3f);
						Main.dust[dust12].noGravity = true;
						Dust obj15 = Main.dust[dust12];
						obj15.velocity *= 0f;
						Main.dust[dust12].position.X -= dustOffsetX;
						Main.dust[dust12].position.Y -= dustOffsetY;
					}
					return false;
				}
				if (projectile.type == 298)
				{
					projectile.HealingProjectile((int)projectile.ai[1], (int)projectile.ai[0], 4f, 15f);
					int maxDust2 = 3;
					float dustOffsetMultiplier2 = 1f / (float)maxDust2;
					for (int num1043 = 0; num1043 < maxDust2; num1043++)
					{
						float dustOffsetX2 = projectile.velocity.X * dustOffsetMultiplier2 * (float)num1043;
						float dustOffsetY2 = (0f - projectile.velocity.Y * dustOffsetMultiplier2) * (float)num1043;
						int dust13 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 175, 0f, 0f, 100, default(Color), 1.3f);
						Main.dust[dust13].noGravity = true;
						Dust obj16 = Main.dust[dust13];
						obj16.velocity *= 0f;
						Main.dust[dust13].position.X -= dustOffsetX2;
						Main.dust[dust13].position.Y -= dustOffsetY2;
					}
					return false;
				}
				if (projectile.type == 584)
				{
					ref float initialSpeed = ref projectile.localAI[1];
					if (initialSpeed == 0f)
					{
						initialSpeed = ((Vector2)(ref projectile.velocity)).Length();
					}
					if (!Main.npc.IndexInRange((int)projectile.ai[0]) || !Main.npc[(int)projectile.ai[0]].active || !Main.npc[(int)projectile.ai[0]].townNPC)
					{
						projectile.Kill();
						return false;
					}
					NPC npcToHeal = Main.npc[(int)projectile.ai[0]];
					if (!projectile.WithinRange(npcToHeal.Center, initialSpeed))
					{
						Rectangle hitbox = projectile.Hitbox;
						if (!((Rectangle)(ref hitbox)).Intersects(npcToHeal.Hitbox))
						{
							Vector2 flySpeed = projectile.SafeDirectionTo(npcToHeal.Center) * initialSpeed;
							if (flySpeed.Y < projectile.velocity.Y)
							{
								flySpeed.Y = projectile.velocity.Y;
							}
							flySpeed.Y++;
							projectile.velocity = Vector2.Lerp(projectile.velocity, flySpeed, 0.04f);
							projectile.rotation += projectile.velocity.X * 0.05f;
							return false;
						}
					}
					projectile.Kill();
					int healAmount = npcToHeal.lifeMax - npcToHeal.life;
					int maxHealAmount = 20;
					if (npcToHeal.lifeMax > 250)
					{
						maxHealAmount = (int)Math.Max(maxHealAmount, (float)npcToHeal.lifeMax * 0.05f);
					}
					if (healAmount > maxHealAmount)
					{
						healAmount = maxHealAmount;
					}
					if (healAmount > 0)
					{
						npcToHeal.life += healAmount;
						npcToHeal.HealEffect(healAmount);
						return false;
					}
					return false;
				}
			}
		}
		bool adultWyrmAlive = false;
		if (CalamityGlobalNPC.adultEidolonWyrmHead != -1 && Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active)
		{
			adultWyrmAlive = true;
		}
		if (adultWyrmAlive || (CalamityWorld.death && !CalamityPlayer.areThereAnyDamnBosses))
		{
			if (projectile.type == 468)
			{
				if (projectile.ai[1] == 0f)
				{
					projectile.ai[1] = 1f;
					SoundEngine.PlaySound(in SoundID.Item34, projectile.Center);
				}
				else if (projectile.ai[1] == 1f && Main.netMode != 1)
				{
					int num1044 = -1;
					float num1045 = 2000f;
					for (int num1046 = 0; num1046 < 255; num1046++)
					{
						if (Main.player[num1046].active && !Main.player[num1046].dead)
						{
							Vector2 center10 = Main.player[num1046].Center;
							float num1047 = Vector2.Distance(center10, projectile.Center);
							if ((num1047 < num1045 || num1044 == -1) && Collision.CanHit(projectile.Center, 1, 1, center10, 1, 1))
							{
								num1045 = num1047;
								num1044 = num1046;
							}
						}
					}
					if (num1045 < 20f)
					{
						projectile.Kill();
						return false;
					}
					if (num1044 != -1)
					{
						projectile.ai[1] = 21f;
						projectile.ai[0] = num1044;
						projectile.netUpdate = true;
					}
				}
				else if (projectile.ai[1] > 20f && projectile.ai[1] < 200f)
				{
					projectile.ai[1]++;
					int num1048 = (int)projectile.ai[0];
					if (!Main.player[num1048].active || Main.player[num1048].dead)
					{
						projectile.ai[1] = 1f;
						projectile.ai[0] = 0f;
						projectile.netUpdate = true;
					}
					else
					{
						float num1049 = projectile.velocity.ToRotation();
						Vector2 vector107 = Main.player[num1048].Center - projectile.Center;
						if (((Vector2)(ref vector107)).Length() < 20f)
						{
							projectile.Kill();
							return false;
						}
						float targetAngle2 = vector107.ToRotation();
						if (vector107 == Vector2.Zero)
						{
							targetAngle2 = num1049;
						}
						float num1050 = num1049.AngleLerp(targetAngle2, 0.01f);
						projectile.velocity = Utils.RotatedBy(new Vector2(((Vector2)(ref projectile.velocity)).Length(), 0f), (double)num1050, default(Vector2));
					}
				}
				if (projectile.ai[1] >= 1f && projectile.ai[1] < 20f)
				{
					projectile.ai[1]++;
					if (projectile.ai[1] == 20f)
					{
						projectile.ai[1] = 1f;
					}
				}
				projectile.alpha -= 40;
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
				projectile.spriteDirection = projectile.direction;
				projectile.frameCounter++;
				if (projectile.frameCounter >= 3)
				{
					projectile.frame++;
					projectile.frameCounter = 0;
					if (projectile.frame >= 4)
					{
						projectile.frame = 0;
					}
				}
				if (Main.rand.NextBool(4))
				{
					Vector2 value42 = -Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(11.25f)).RotatedBy(projectile.velocity.ToRotation());
					Dust dust14 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100);
					dust14.velocity *= 0.1f;
					dust14.position = projectile.Center + value42 * (float)projectile.width / 2f;
					dust14.fadeIn = 0.9f;
				}
				if (Main.rand.NextBool(32))
				{
					Vector2 value43 = -Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(22.5f)).RotatedBy(projectile.velocity.ToRotation());
					Dust smoke = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155, default(Color), 0.8f);
					smoke.velocity *= 0.3f;
					smoke.position = projectile.Center + value43 * (float)projectile.width / 2f;
					if (Main.rand.NextBool(2))
					{
						smoke.fadeIn = 1.4f;
					}
				}
				if (Main.rand.NextBool(2))
				{
					Vector2 value44 = -Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(45f)).RotatedBy(projectile.velocity.ToRotation());
					Dust shflame2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default(Color), 1.2f);
					shflame2.velocity *= 0.3f;
					shflame2.noGravity = true;
					shflame2.position = projectile.Center + value44 * (float)projectile.width / 2f;
					if (Main.rand.NextBool(2))
					{
						shflame2.fadeIn = 1.4f;
					}
				}
				return false;
			}
			if (projectile.type == 464)
			{
				if (projectile.localAI[1] == 0f)
				{
					projectile.localAI[1] = 1f;
					SoundEngine.PlaySound(in SoundID.Item120, projectile.Center);
				}
				projectile.ai[0]++;
				float duration = 300f;
				if (projectile.ai[1] == 1f)
				{
					if (projectile.ai[0] >= duration - 20f)
					{
						projectile.alpha += 10;
					}
					else
					{
						projectile.alpha -= 10;
					}
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					if (projectile.alpha > 255)
					{
						projectile.alpha = 255;
					}
					if (projectile.ai[0] >= duration)
					{
						projectile.Kill();
						return false;
					}
					int num1051 = Player.FindClosest(projectile.Center, 1, 1);
					Vector2 vector108 = Main.player[num1051].Center - projectile.Center;
					float scaleFactor2 = ((Vector2)(ref projectile.velocity)).Length();
					((Vector2)(ref vector108)).Normalize();
					vector108 *= scaleFactor2;
					projectile.velocity = (projectile.velocity * 15f + vector108) / 16f;
					((Vector2)(ref projectile.velocity)).Normalize();
					projectile.velocity *= scaleFactor2;
					if (projectile.ai[0] % 60f == 0f && Main.netMode != 1)
					{
						Vector2 vector109 = projectile.rotation.ToRotationVector2();
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vector109, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
					}
					projectile.rotation += (float)Math.PI / 30f;
					return false;
				}
				projectile.position -= projectile.velocity;
				if (projectile.ai[0] >= duration - 260f)
				{
					projectile.alpha += 3;
				}
				else
				{
					projectile.alpha -= 40;
				}
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
				if (projectile.ai[0] >= duration - 255f)
				{
					projectile.Kill();
					return false;
				}
				Vector2 val = Utils.RotatedBy(new Vector2(0f, -720f), (double)projectile.velocity.ToRotation(), default(Vector2));
				float scaleFactor3 = projectile.ai[0] % (duration - 255f) / (duration - 255f);
				Vector2 spinningpoint13 = val * scaleFactor3;
				for (int num1052 = 0; num1052 < 6; num1052++)
				{
					Dust.NewDustDirect(projectile.Center + spinningpoint13.RotatedBy((float)num1052 * ((float)Math.PI * 2f) / 6f) + Utils.RandomVector2(Main.rand, -8f, 8f) / 2f, 8, 8, 197, 0f, 0f, 100, Color.Transparent).noGravity = true;
				}
				return false;
			}
			if (projectile.type == 466 && !projectile.friendly)
			{
				projectile.frameCounter++;
				if (projectile.velocity == Vector2.Zero)
				{
					if (projectile.frameCounter >= projectile.extraUpdates * 2)
					{
						projectile.frameCounter = 0;
						bool flag30 = true;
						for (int num1053 = 1; num1053 < projectile.oldPos.Length; num1053++)
						{
							if (projectile.oldPos[num1053] != projectile.oldPos[0])
							{
								flag30 = false;
							}
						}
						if (flag30)
						{
							projectile.Kill();
							return false;
						}
					}
				}
				else
				{
					if (projectile.frameCounter < projectile.extraUpdates * 2)
					{
						return false;
					}
					projectile.frameCounter = 0;
					float num1054 = ((Vector2)(ref projectile.velocity)).Length();
					UnifiedRandom unifiedRandom = new UnifiedRandom((int)projectile.ai[1]);
					int num1055 = 0;
					Vector2 spinningpoint14 = -Vector2.UnitY;
					while (true)
					{
						int num1056 = unifiedRandom.Next();
						projectile.ai[1] = num1056;
						num1056 %= 100;
						Vector2 vector110 = ((float)num1056 / 100f * ((float)Math.PI * 2f)).ToRotationVector2();
						if (vector110.Y > 0f)
						{
							vector110.Y *= -1f;
						}
						bool flag31 = false;
						if (vector110.Y > -0.02f)
						{
							flag31 = true;
						}
						if (vector110.X * (float)(projectile.extraUpdates + 1) * 2f * num1054 + projectile.localAI[0] > 40f)
						{
							flag31 = true;
						}
						if (vector110.X * (float)(projectile.extraUpdates + 1) * 2f * num1054 + projectile.localAI[0] < -40f)
						{
							flag31 = true;
						}
						if (flag31)
						{
							if (num1055++ >= 100)
							{
								projectile.velocity = Vector2.Zero;
								projectile.localAI[1] = 1f;
								break;
							}
							continue;
						}
						spinningpoint14 = vector110;
						break;
					}
					if (projectile.velocity != Vector2.Zero)
					{
						projectile.localAI[0] += spinningpoint14.X * (float)(projectile.extraUpdates + 1) * 2f * num1054;
						projectile.velocity = spinningpoint14.RotatedBy(projectile.ai[0] + (float)Math.PI / 2f) * num1054;
						projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
					}
				}
				return false;
			}
		}
		if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
		{
			bool death3 = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (projectile.type == 961)
			{
				int dustType2 = 16;
				float dustVelocityMultiplier = 0.75f;
				int numDust2 = 5;
				int numDust3 = 5;
				int fadeInTime = 25;
				int fadeOutGateValue = (death3 ? 90 : 65);
				float killGateValue = (death3 ? 100f : 75f);
				int maxFrames = 5;
				bool fadeIn = projectile.ai[0] < (float)fadeInTime;
				bool fadeOut = projectile.ai[0] >= (float)fadeOutGateValue;
				bool killProjectile = projectile.ai[0] >= killGateValue;
				projectile.ai[0]++;
				if (projectile.localAI[0] == 0f)
				{
					projectile.localAI[0] = 1f;
					projectile.rotation = projectile.velocity.ToRotation();
					projectile.frame = Main.rand.Next(maxFrames);
					for (int num1057 = 0; num1057 < numDust2; num1057++)
					{
						Dust dust15 = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(24f, 24f), dustType2, projectile.velocity * dustVelocityMultiplier * MathHelper.Lerp(0.2f, 0.7f, Main.rand.NextFloat()));
						dust15.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
						dust15.scale = 0.8f + Main.rand.NextFloat() * 0.5f;
					}
					for (int num1058 = 0; num1058 < numDust3; num1058++)
					{
						Dust dust16 = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(24f, 24f), dustType2, Main.rand.NextVector2Circular(2f, 2f) + projectile.velocity * dustVelocityMultiplier * MathHelper.Lerp(0.2f, 0.5f, Main.rand.NextFloat()));
						dust16.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
						dust16.scale = 0.8f + Main.rand.NextFloat() * 0.5f;
						dust16.fadeIn = 1f;
					}
					SoundEngine.PlaySound(in SoundID.DeerclopsIceAttack, projectile.Center);
				}
				if (fadeIn)
				{
					projectile.Opacity += 0.04f;
					if (projectile.Opacity > 1f)
					{
						projectile.Opacity = 1f;
					}
					projectile.scale = projectile.Opacity * projectile.ai[1];
				}
				if (fadeOut)
				{
					projectile.Opacity -= 0.2f;
				}
				if (killProjectile)
				{
					projectile.Kill();
				}
				return false;
			}
			if (projectile.type == 962)
			{
				projectile.ai[0]++;
				projectile.frame = (int)projectile.ai[1];
				if (projectile.localAI[0] == 0f)
				{
					projectile.localAI[0] = 1f;
					projectile.rotation = projectile.velocity.ToRotation();
					for (int dustIndex = 0; dustIndex < 5; dustIndex++)
					{
						Dust dust17 = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(24f, 24f), 16, projectile.velocity * MathHelper.Lerp(0.2f, 0.7f, Main.rand.NextFloat()));
						dust17.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
						dust17.scale = 0.8f + Main.rand.NextFloat() * 0.5f;
					}
					for (int num1059 = 0; num1059 < 5; num1059++)
					{
						Dust dust18 = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(24f, 24f), 16, Main.rand.NextVector2Circular(2f, 2f) + projectile.velocity * MathHelper.Lerp(0.2f, 0.5f, Main.rand.NextFloat()));
						dust18.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
						dust18.scale = 0.8f + Main.rand.NextFloat() * 0.5f;
						dust18.fadeIn = 1f;
					}
				}
				if (projectile.ai[0] >= 5f + projectile.ai[2])
				{
					projectile.velocity.Y += 0.3f;
				}
				if (projectile.ai[0] <= projectile.ai[2])
				{
					projectile.Opacity = 0.4f;
					projectile.timeLeft++;
					if (projectile.ai[0] == projectile.ai[2])
					{
						projectile.velocity *= 100f;
						projectile.velocity *= (death3 ? 16f : 12f) + Main.rand.NextFloat() * 2f;
					}
				}
				else
				{
					projectile.Opacity = 1f;
				}
				return false;
			}
			if (projectile.type == 44)
			{
				if (Main.wofNPCIndex < 0 || !Main.npc[Main.wofNPCIndex].active || Main.npc[Main.wofNPCIndex].life <= 0 || projectile.tileCollide)
				{
					return true;
				}
				if (projectile.ai[0] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item8, projectile.Center);
				}
				projectile.rotation += (float)projectile.direction * 0.8f;
				projectile.ai[0]++;
				if (((Vector2)(ref projectile.velocity)).Length() < projectile.ai[1] && projectile.ai[0] >= 30f)
				{
					projectile.velocity *= 1.06f;
				}
				Vector2 vector111 = Main.player[Main.npc[Main.wofNPCIndex].target].Center - projectile.Center;
				if (((Vector2)(ref vector111)).Length() < 10f)
				{
					projectile.Kill();
					return false;
				}
				if (projectile.ai[0] < 210f)
				{
					float scaleFactor4 = ((Vector2)(ref projectile.velocity)).Length();
					((Vector2)(ref vector111)).Normalize();
					vector111 *= scaleFactor4;
					projectile.velocity = (projectile.velocity * 30f + vector111) / 31f;
					((Vector2)(ref projectile.velocity)).Normalize();
					projectile.velocity *= scaleFactor4;
				}
				for (int num1060 = 0; num1060 < 2; num1060++)
				{
					Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 100).noGravity = true;
				}
				return false;
			}
			if (projectile.type == 926)
			{
				if (projectile.ai[1] == -2f)
				{
					if (projectile.alpha == 0 && Main.rand.NextBool(3))
					{
						Color newColor3 = NPC.AI_121_QueenSlime_GetDustColor();
						((Color)(ref newColor3)).A = 150;
						int num1061 = 8;
						bool noGravity = Main.rand.NextBool();
						Dust dust19 = Dust.NewDustDirect(projectile.position - new Vector2((float)num1061, (float)num1061) + projectile.velocity, projectile.width + num1061 * 2, projectile.height + num1061 * 2, 4, 0f, 0f, 50, newColor3, 1.2f);
						dust19.velocity *= 0.3f;
						dust19.velocity += projectile.velocity * 0.3f;
						dust19.noGravity = noGravity;
					}
					projectile.alpha -= 50;
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.05f;
					projectile.velocity.Y += 0.1f;
					if (projectile.velocity.Y > 16f)
					{
						projectile.velocity.Y = 16f;
					}
					if (Main.getGoodWorld && ((Vector2)(ref projectile.velocity)).Length() > 4f)
					{
						projectile.velocity *= 0.985f;
					}
					return false;
				}
			}
			else if (projectile.type == 920)
			{
				if (projectile.ai[1] < 0f)
				{
					if (projectile.frameCounter == 0)
					{
						projectile.frameCounter = 1;
						projectile.frame = Main.rand.Next(3);
					}
					if (projectile.alpha == 0 && Main.rand.NextBool(3))
					{
						Color newColor4 = default(Color);
						((Color)(ref newColor4))._002Ector(78, 136, 255, 150);
						Dust dust20 = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 4, 0f, 0f, 50, newColor4, 1.2f);
						dust20.velocity *= 0.3f;
						dust20.velocity += projectile.velocity * 0.3f;
						dust20.noGravity = true;
					}
					projectile.alpha -= 50;
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					if (projectile.ai[1] == -1f)
					{
						if (projectile.ai[0] >= 5f)
						{
							projectile.velocity.Y += 0.05f;
						}
						else
						{
							projectile.ai[0]++;
						}
						if (projectile.velocity.Y > 16f)
						{
							projectile.velocity.Y = 16f;
						}
					}
					projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
					return false;
				}
			}
			else
			{
				if (projectile.type == 303 && projectile.ai[1] >= 1f)
				{
					bool num1062 = projectile.ai[1] == 2f;
					bool homeIn3 = false;
					float homingTime = 140f;
					float spreadOutCutoffTime2 = 510f;
					float homeInCutoffTime2 = spreadOutCutoffTime2 - homingTime;
					float minAcceleration2 = 0.08f;
					float maxAcceleration2 = 0.12f;
					float homingVelocity2 = 25f;
					float maxVelocity4 = 15f;
					if (!num1062)
					{
						if ((float)projectile.timeLeft > homeInCutoffTime2 && (float)projectile.timeLeft <= spreadOutCutoffTime2)
						{
							homeIn3 = true;
						}
						else if (((Vector2)(ref projectile.velocity)).Length() < maxVelocity4)
						{
							projectile.velocity *= 1.01f;
						}
					}
					else if (((Vector2)(ref projectile.velocity)).Length() < maxVelocity4)
					{
						projectile.velocity *= 1.01f;
					}
					if (homeIn3)
					{
						int playerIndex2 = (int)projectile.ai[0];
						Vector2 velocity2 = projectile.velocity;
						if (Main.player.IndexInRange(playerIndex2))
						{
							Player player2 = Main.player[playerIndex2];
							velocity2 = projectile.DirectionTo(player2.Center) * homingVelocity2;
						}
						float amount2 = MathHelper.Lerp(minAcceleration2, maxAcceleration2, Utils.GetLerpValue(spreadOutCutoffTime2, 30f, projectile.timeLeft, clamped: true));
						projectile.velocity = Vector2.SmoothStep(projectile.velocity, velocity2, amount2);
						if (Vector2.Distance(projectile.Center, Main.player[playerIndex2].Center) < 96f && (float)projectile.timeLeft > homeInCutoffTime2)
						{
							projectile.timeLeft = (int)homeInCutoffTime2;
						}
					}
					if (projectile.timeLeft <= 3)
					{
						projectile.position = projectile.Center;
						projectile.width = (projectile.height = 128);
						projectile.position.X -= projectile.width / 2;
						projectile.position.Y -= projectile.height / 2;
					}
					if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
					{
						projectile.tileCollide = false;
						projectile.alpha = 255;
					}
					else
					{
						for (int num1063 = 0; num1063 < 2; num1063++)
						{
							float num1064 = 0f;
							float num1065 = 0f;
							if (num1063 == 1)
							{
								num1064 = projectile.velocity.X * 0.5f;
								num1065 = projectile.velocity.Y * 0.5f;
							}
							Dust dust21 = Dust.NewDustDirect(new Vector2(projectile.position.X + 3f + num1064, projectile.position.Y + 3f + num1065) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100);
							dust21.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
							dust21.velocity *= 0.2f;
							dust21.noGravity = true;
							Dust dust22 = Dust.NewDustDirect(new Vector2(projectile.position.X + 3f + num1064, projectile.position.Y + 3f + num1065) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
							dust22.fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
							dust22.velocity *= 0.05f;
						}
					}
					if (projectile.velocity != Vector2.Zero)
					{
						projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
					}
					return false;
				}
				if (projectile.type == 275 || projectile.type == 276)
				{
					if (projectile.ai[2] > 0f && ((Vector2)(ref projectile.velocity)).Length() < projectile.ai[2])
					{
						projectile.velocity *= 1.012f;
					}
					projectile.frameCounter++;
					if (projectile.frameCounter > 1)
					{
						projectile.frameCounter = 0;
						projectile.frame++;
						if (projectile.frame > 1)
						{
							projectile.frame = 0;
						}
					}
					if (projectile.ai[1] == 0f)
					{
						projectile.ai[1] = 1f;
						SoundEngine.PlaySound(in SoundID.Item17, projectile.Center);
					}
					if (projectile.alpha > 0)
					{
						projectile.alpha -= 30;
					}
					if (projectile.alpha < 0)
					{
						projectile.alpha = 0;
					}
					projectile.ai[0]++;
					if (projectile.ai[0] >= 120f && ((Vector2)(ref projectile.velocity)).Length() < 18f)
					{
						projectile.velocity *= 1.01f;
					}
					projectile.tileCollide = projectile.ai[0] >= 300f;
					if (projectile.timeLeft > 600)
					{
						projectile.timeLeft = 600;
					}
					projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (float)Math.PI / 2f;
					return false;
				}
				if (projectile.type == 277 && projectile.ai[2] != 0f)
				{
					projectile.tileCollide = false;
					if (projectile.alpha > 0)
					{
						projectile.alpha -= 30;
						if (projectile.alpha < 0)
						{
							projectile.alpha = 0;
						}
					}
					Tile tileSafely3 = Framing.GetTileSafely(projectile.Center.ToTileCoordinates());
					if (tileSafely3.HasUnactuatedTile && Main.tileSolid[tileSafely3.TileType])
					{
						projectile.velocity = Vector2.Zero;
						projectile.ai[1]++;
						float explodeGateValue = 600f;
						if (projectile.ai[1] >= explodeGateValue)
						{
							if (projectile.owner == Main.myPlayer)
							{
								int totalProjectiles = (death3 ? 12 : 8);
								float radians = (float)Math.PI * 2f / (float)totalProjectiles;
								int type = ModContent.ProjectileType<ThornBallSpike>();
								float velocity3 = 1f;
								Vector2 spinningPoint = default(Vector2);
								((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity3);
								for (int num1066 = 0; num1066 < totalProjectiles; num1066++)
								{
									Vector2 velocity4 = spinningPoint.RotatedBy(radians * (float)num1066);
									Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center + Vector2.Normalize(velocity4) * 16f, velocity4, type, PlanteraAI.ThornBallSpikeDamage, 0f, Main.myPlayer);
								}
							}
							SoundEngine.PlaySound(in SoundID.Item17, projectile.Center);
							for (int num1067 = 0; num1067 < 8; num1067++)
							{
								int randomDustType = (Main.rand.NextBool() ? 125 : 148);
								Dust dust23 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, randomDustType, 0f, 0f, 0, default(Color), 2f);
								dust23.velocity *= 3f;
								if (Main.rand.NextBool())
								{
									dust23.scale = 0.5f;
									dust23.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
								}
							}
							for (int num1068 = 0; num1068 < 10; num1068++)
							{
								int randomDustType2 = (Main.rand.NextBool() ? 125 : 148);
								Dust dust24 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, randomDustType2, 0f, 0f, 0, default(Color), 3f);
								dust24.noGravity = true;
								dust24.velocity *= 5f;
								Dust dust25 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, randomDustType2, 0f, 0f, 0, default(Color), 2f);
								dust25.velocity *= 2f;
							}
							projectile.Kill();
						}
					}
					else
					{
						int closestPlayer = Player.FindClosest(projectile.Center, 1, 1);
						float homingSpeed3 = 7.5f + Vector2.Distance(Main.player[closestPlayer].Center, projectile.Center) * 0.01f;
						Vector2 homingVelocity3 = Vector2.Normalize(Main.player[closestPlayer].Center - projectile.Center) * homingSpeed3;
						int inertia4 = 200;
						projectile.velocity.X = (projectile.velocity.X * (float)(inertia4 - 1) + homingVelocity3.X) / (float)inertia4;
						if (((Vector2)(ref projectile.velocity)).Length() > 16f)
						{
							((Vector2)(ref projectile.velocity)).Normalize();
							projectile.velocity *= 16f;
						}
						projectile.ai[0]++;
						if (projectile.ai[0] > 15f)
						{
							projectile.velocity.Y += 0.1f;
						}
						projectile.rotation += projectile.velocity.X * 0.05f;
						if (projectile.velocity.Y > 16f)
						{
							projectile.velocity.Y = 16f;
						}
					}
					return false;
				}
				if (projectile.type == 593)
				{
					if (((Vector2)(ref projectile.velocity)).Length() < 6f)
					{
						projectile.velocity *= 1.005f;
					}
				}
				else
				{
					if (projectile.type == 464)
					{
						if (projectile.localAI[1] == 0f)
						{
							projectile.localAI[1] = 1f;
							SoundEngine.PlaySound(in SoundID.Item120, projectile.Center);
						}
						projectile.ai[0]++;
						float duration2 = 300f;
						if (projectile.ai[1] == 1f)
						{
							if (projectile.ai[0] >= duration2 - 20f)
							{
								projectile.alpha += 10;
							}
							else
							{
								projectile.alpha -= 10;
							}
							if (projectile.alpha < 0)
							{
								projectile.alpha = 0;
							}
							if (projectile.alpha > 255)
							{
								projectile.alpha = 255;
							}
							if (projectile.ai[0] >= duration2)
							{
								projectile.Kill();
								return false;
							}
							int num1069 = Player.FindClosest(projectile.Center, 1, 1);
							Vector2 vector112 = Main.player[num1069].Center - projectile.Center;
							float scaleFactor5 = ((Vector2)(ref projectile.velocity)).Length();
							((Vector2)(ref vector112)).Normalize();
							if (vector112.HasNaNs())
							{
								vector112 = Vector2.Zero;
							}
							vector112 *= scaleFactor5;
							projectile.velocity = (projectile.velocity * 20f + vector112) / 21f;
							((Vector2)(ref projectile.velocity)).Normalize();
							projectile.velocity *= scaleFactor5;
							projectile.velocity *= 0.99f;
							if (projectile.ai[0] % (death3 ? 20f : 30f) == 0f && Main.netMode != 1)
							{
								Vector2 vector113 = projectile.rotation.ToRotationVector2();
								Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, vector113, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
							}
							projectile.rotation += (float)Math.PI / 30f;
							Lighting.AddLight(projectile.Center, 0.3f, 0.75f, 0.9f);
							return false;
						}
						projectile.position -= projectile.velocity;
						float splitProjectileDuration = duration2 - 255f;
						if (projectile.ai[0] >= splitProjectileDuration - 5f)
						{
							projectile.alpha += 3;
						}
						else
						{
							projectile.alpha -= 40;
						}
						if (projectile.alpha < 0)
						{
							projectile.alpha = 0;
						}
						if (projectile.alpha > 255)
						{
							projectile.alpha = 255;
						}
						if (projectile.ai[0] >= splitProjectileDuration)
						{
							projectile.Kill();
							return false;
						}
						Vector2 val2 = Utils.RotatedBy(new Vector2(0f, -720f), (double)projectile.velocity.ToRotation(), default(Vector2));
						float scaleFactor6 = projectile.ai[0] % splitProjectileDuration / splitProjectileDuration;
						Vector2 spinningpoint15 = val2 * scaleFactor6;
						for (int num1070 = 0; num1070 < 6; num1070++)
						{
							Vector2 vector114 = projectile.Center + spinningpoint15.RotatedBy((float)num1070 * ((float)Math.PI * 2f) / 6f);
							Lighting.AddLight(vector114, 0.3f, 0.75f, 0.9f);
							for (int num1071 = 0; num1071 < 2; num1071++)
							{
								Dust.NewDustDirect(vector114 + Utils.RandomVector2(Main.rand, -8f, 8f) / 2f, 8, 8, 197, 0f, 0f, 100, Color.Transparent).noGravity = true;
							}
						}
						return false;
					}
					if (projectile.type == 452)
					{
						projectile.alpha -= 40;
						if (projectile.alpha < 0)
						{
							projectile.alpha = 0;
						}
						if (projectile.ai[0] == 0f)
						{
							projectile.localAI[0]++;
							if (projectile.localAI[0] >= 45f)
							{
								projectile.localAI[0] = 0f;
								projectile.ai[0] = 1f;
								projectile.ai[1] = 0f - projectile.ai[1];
								projectile.netUpdate = true;
							}
							projectile.velocity.X = projectile.velocity.RotatedBy(projectile.ai[1]).X;
							projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
							projectile.velocity.Y -= 0.08f;
							if (projectile.velocity.Y > 0f)
							{
								projectile.velocity.Y -= 0.2f;
							}
							if (projectile.velocity.Y < -7f)
							{
								projectile.velocity.Y = -7f;
							}
						}
						else if (projectile.ai[0] == 1f)
						{
							projectile.localAI[0]++;
							if (projectile.localAI[0] >= 90f)
							{
								projectile.localAI[0] = 0f;
								projectile.ai[0] = 2f;
								projectile.ai[1] = (int)Player.FindClosest(projectile.position, projectile.width, projectile.height);
								projectile.netUpdate = true;
							}
							projectile.velocity.X = projectile.velocity.RotatedBy(projectile.ai[1]).X;
							projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
							projectile.velocity.Y -= 0.08f;
							if (projectile.velocity.Y > 0f)
							{
								projectile.velocity.Y -= 0.2f;
							}
							if (projectile.velocity.Y < -7f)
							{
								projectile.velocity.Y = -7f;
							}
						}
						else if (projectile.ai[0] == 2f)
						{
							projectile.localAI[0]++;
							if (projectile.localAI[0] >= 45f)
							{
								projectile.localAI[0] = 0f;
								projectile.ai[0] = 3f;
								projectile.netUpdate = true;
							}
							Vector2 value45 = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
							((Vector2)(ref value45)).Normalize();
							value45 *= 12f;
							value45 = Vector2.Lerp(projectile.velocity, value45, 0.6f);
							float num1072 = 0.4f;
							if (projectile.velocity.X < value45.X)
							{
								projectile.velocity.X += num1072;
								if (projectile.velocity.X < 0f && value45.X > 0f)
								{
									projectile.velocity.X += num1072;
								}
							}
							else if (projectile.velocity.X > value45.X)
							{
								projectile.velocity.X -= num1072;
								if (projectile.velocity.X > 0f && value45.X < 0f)
								{
									projectile.velocity.X -= num1072;
								}
							}
							if (projectile.velocity.Y < value45.Y)
							{
								projectile.velocity.Y += num1072;
								if (projectile.velocity.Y < 0f && value45.Y > 0f)
								{
									projectile.velocity.Y += num1072;
								}
							}
							else if (projectile.velocity.Y > value45.Y)
							{
								projectile.velocity.Y -= num1072;
								if (projectile.velocity.Y > 0f && value45.Y < 0f)
								{
									projectile.velocity.Y -= num1072;
								}
							}
						}
						else if (projectile.ai[0] == 3f)
						{
							Vector2 value46 = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
							if (((Vector2)(ref value46)).Length() < 30f)
							{
								projectile.Kill();
								return false;
							}
							float velocityLimit = (death3 ? 28f : 24f) / MathHelper.Clamp(projectile.ai[2] * 0.75f, 1f, 3f);
							if (((Vector2)(ref projectile.velocity)).Length() < velocityLimit)
							{
								projectile.velocity *= 1.01f;
							}
						}
						if (projectile.alpha < 40)
						{
							Dust.NewDustDirect(projectile.Center - Vector2.One * 5f, 10, 10, 229, (0f - projectile.velocity.X) / 3f, (0f - projectile.velocity.Y) / 3f, 150, Color.Transparent, 1.2f).noGravity = true;
						}
						projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
						return false;
					}
					if (projectile.type == 454 && Main.npc[(int)projectile.ai[1]].type == 397)
					{
						float velocityLimit2 = (death3 ? 14f : 12f);
						if (((Vector2)(ref projectile.velocity)).Length() < velocityLimit2)
						{
							projectile.velocity *= 1.0075f;
						}
						return true;
					}
					if (projectile.type == 456)
					{
						Vector2 value47 = default(Vector2);
						((Vector2)(ref value47))._002Ector(0f, 216f);
						projectile.alpha -= 15;
						if (projectile.alpha < 0)
						{
							projectile.alpha = 0;
						}
						int num1073 = (int)Math.Abs(projectile.ai[0]) - 1;
						int num1074 = (int)projectile.ai[1];
						if (!Main.npc[num1073].active || Main.npc[num1073].type != 396)
						{
							projectile.Kill();
							return false;
						}
						projectile.localAI[0]++;
						if (projectile.localAI[0] >= 330f && projectile.ai[0] > 0f && Main.netMode != 1)
						{
							projectile.ai[0] *= -1f;
							projectile.netUpdate = true;
						}
						if (Main.netMode != 1 && projectile.ai[0] > 0f && (!Main.player[(int)projectile.ai[1]].active || Main.player[(int)projectile.ai[1]].dead))
						{
							projectile.ai[0] *= -1f;
							projectile.netUpdate = true;
						}
						projectile.rotation = (Main.npc[(int)Math.Abs(projectile.ai[0]) - 1].Center - Main.player[(int)projectile.ai[1]].Center + value47).ToRotation() + (float)Math.PI / 2f;
						if (projectile.ai[0] > 0f)
						{
							Vector2 value48 = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
							if (value48.X != 0f || value48.Y != 0f)
							{
								projectile.velocity = Vector2.Normalize(value48) * Math.Min(32f, ((Vector2)(ref value48)).Length());
							}
							else
							{
								projectile.velocity = Vector2.Zero;
							}
							if (((Vector2)(ref value48)).Length() < 40f && projectile.localAI[1] == 0f)
							{
								projectile.localAI[1] = 1f;
								int timeToAdd = 840;
								if (Main.expertMode)
								{
									timeToAdd = 960;
								}
								if (!Main.player[num1074].creativeGodMode)
								{
									Main.player[num1074].AddBuff(145, timeToAdd);
								}
							}
						}
						else
						{
							Vector2 value49 = Main.npc[(int)Math.Abs(projectile.ai[0]) - 1].Center - projectile.Center + value47;
							if (value49.X != 0f || value49.Y != 0f)
							{
								projectile.velocity = Vector2.Normalize(value49) * Math.Min(32f, ((Vector2)(ref value49)).Length());
							}
							else
							{
								projectile.velocity = Vector2.Zero;
							}
							if (((Vector2)(ref value49)).Length() < 40f)
							{
								projectile.Kill();
							}
						}
						return false;
					}
					if (projectile.type == 455 && Main.npc[(int)projectile.ai[1]].type == 396)
					{
						Vector2? vector115 = null;
						if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
						{
							projectile.velocity = -Vector2.UnitY;
						}
						if (Main.npc[(int)projectile.ai[1]].active)
						{
							Vector2 value50 = default(Vector2);
							((Vector2)(ref value50))._002Ector(27f, 59f);
							Vector2 value51 = Utils.Vector2FromElipse(Main.npc[(int)projectile.ai[1]].localAI[0].ToRotationVector2(), value50 * Main.npc[(int)projectile.ai[1]].localAI[1]);
							projectile.position = Main.npc[(int)projectile.ai[1]].Center + value51 - projectile.Size / 2f;
						}
						if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
						{
							projectile.velocity = -Vector2.UnitY;
						}
						if (projectile.localAI[0] == 0f)
						{
							SoundEngine.PlaySound(in SoundID.Zombie104, projectile.Center);
						}
						float num1075 = 1f;
						projectile.localAI[0]++;
						if (projectile.localAI[0] >= 180f)
						{
							projectile.Kill();
							return false;
						}
						projectile.scale = (float)Math.Sin(projectile.localAI[0] * (float)Math.PI / 180f) * 10f * num1075;
						if (projectile.scale > num1075)
						{
							projectile.scale = num1075;
						}
						float num1076 = projectile.velocity.ToRotation();
						num1076 += projectile.ai[0];
						projectile.rotation = num1076 - (float)Math.PI / 2f;
						projectile.velocity = num1076.ToRotationVector2();
						float num1077 = 3f;
						float num1078 = projectile.width;
						Vector2 samplingPoint = projectile.Center;
						if (vector115.HasValue)
						{
							samplingPoint = vector115.Value;
						}
						float[] array9 = new float[(int)num1077];
						Collision.LaserScan(samplingPoint, projectile.velocity, num1078 * projectile.scale, 2400f, array9);
						float num1079 = 0f;
						for (int num1080 = 0; num1080 < array9.Length; num1080++)
						{
							num1079 += array9[num1080];
						}
						num1079 /= num1077;
						if (!Collision.CanHitLine(Main.npc[(int)projectile.ai[1]].Center, 1, 1, Main.player[Main.npc[(int)projectile.ai[1]].target].Center, 1, 1) && Main.npc[(int)projectile.ai[1]].Calamity().newAI[0] == 1f)
						{
							num1079 = 2400f;
						}
						float amount3 = 0.5f;
						projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num1079, amount3);
						Vector2 vector116 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
						Vector2 vector117 = default(Vector2);
						for (int num1081 = 0; num1081 < 2; num1081++)
						{
							float num1082 = projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
							float num1083 = (float)Main.rand.NextDouble() * 2f + 2f;
							((Vector2)(ref vector117))._002Ector((float)Math.Cos(num1082) * num1083, (float)Math.Sin(num1082) * num1083);
							Dust dust26 = Dust.NewDustDirect(vector116, 0, 0, 229, vector117.X, vector117.Y);
							dust26.noGravity = true;
							dust26.scale = 1.7f;
						}
						if (Main.rand.NextBool(5))
						{
							Vector2 value52 = projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
							Dust smoke2 = Dust.NewDustDirect(vector116 + value52 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
							smoke2.velocity *= 0.5f;
							smoke2.velocity.Y = 0f - Math.Abs(smoke2.velocity.Y);
						}
						DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
						Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, DelegateMethods.CastLight);
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool RunFishingMinigames(Projectile projectile)
	{
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1786: Unknown result type (might be due to invalid IL or missing references)
		//IL_1791: Unknown result type (might be due to invalid IL or missing references)
		//IL_1157: Unknown result type (might be due to invalid IL or missing references)
		//IL_115c: Unknown result type (might be due to invalid IL or missing references)
		//IL_115e: Unknown result type (might be due to invalid IL or missing references)
		//IL_116b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_118e: Unknown result type (might be due to invalid IL or missing references)
		//IL_119b: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1964: Unknown result type (might be due to invalid IL or missing references)
		//IL_196e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1973: Unknown result type (might be due to invalid IL or missing references)
		//IL_198c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1996: Unknown result type (might be due to invalid IL or missing references)
		//IL_199b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1071: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_19db: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1084: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1090: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1405: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_1411: Unknown result type (might be due to invalid IL or missing references)
		//IL_1426: Unknown result type (might be due to invalid IL or missing references)
		//IL_1435: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_157a: Unknown result type (might be due to invalid IL or missing references)
		//IL_157f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1591: Unknown result type (might be due to invalid IL or missing references)
		//IL_1596: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1506: Unknown result type (might be due to invalid IL or missing references)
		//IL_150c: Unknown result type (might be due to invalid IL or missing references)
		//IL_150e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1518: Unknown result type (might be due to invalid IL or missing references)
		//IL_152a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1539: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbb: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[projectile.owner];
		CalamityPlayer cplayer = owner.Calamity();
		Rectangle val;
		switch (owner.Calamity().SelectedFishingMinigame)
		{
		case CalamityPlayer.FishingMinigames.ScrapBobber:
		{
			if (CatchTime < 0f || (isReelingIn == 1f && CaughtItemID > 0))
			{
				owner.Calamity().ShouldHideControls = true;
			}
			if (isReelingIn != projectile.ai[0])
			{
				return false;
			}
			if (CatchTime < 0f)
			{
				CatchTime++;
			}
			if (projectile.wet || (projectile.lavaWet && owner.accLavaFishing) || projectile.honeyWet)
			{
				TimerToCatch++;
			}
			int speedup2 = Math.Min(owner.Calamity().consecutiveCaughtFish * 5, 25);
			int totalTime2 = 60 - speedup2;
			if (TimerToCatch + 90f >= (float)(160 - owner.HeldItem.fishingPole) && CatchTime >= 0f)
			{
				if (PersistentFishingData == 0f)
				{
					PersistentFishingData = (Main.rand.NextBool() ? 1 : (-1));
				}
				float timer2 = TimerToCatch - 160f - (float)owner.HeldItem.fishingPole;
				SmallSplashAtOffset(new Vector2(200f * PersistentFishingData * (timer2 / 90f), 0f));
			}
			if (TimerToCatch >= (float)(160 - owner.HeldItem.fishingPole) && CatchTime >= 0f)
			{
				PersistentFishingData = 0f;
				projectile.FishingCheck();
				if (projectile.ai[1] < 0f)
				{
					CatchTime = -totalTime2 * 4;
					CaughtItemID = (int)projectile.localAI[1];
				}
				TimerToCatch = 0f;
			}
			if (CatchTime < 0f && CatchTime % (float)totalTime2 == 0f)
			{
				Splash();
				PersistentFishingData = Main.rand.Next(0, 4);
				if (owner.whoAmI == Main.myPlayer)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.UnitX.RotatedBy(PersistentFishingData * ((float)Math.PI / 2f)) * 10f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 45 - speedup2, 0.04f, Color.Blue, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
				}
			}
			if (CatchTime < 0f && CatchTime % (float)totalTime2 == -15f)
			{
				int playerDir2 = -1;
				if (owner.Calamity().pressedUp)
				{
					playerDir2 = 3;
				}
				if (owner.Calamity().pressedLeft)
				{
					playerDir2 = 2;
				}
				if (owner.Calamity().pressedDown)
				{
					playerDir2 = 1;
				}
				if (owner.Calamity().pressedRight)
				{
					playerDir2 = 0;
				}
				if ((float)playerDir2 != PersistentFishingData)
				{
					if (owner.whoAmI == Main.myPlayer)
					{
						if (playerDir2 >= 0)
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.UnitX.RotatedBy((float)playerDir2 * ((float)Math.PI / 2f)) * 10f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 15, 0.04f, Color.Red, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.Zero, "CalamityMod/Particles/HighResHollowCircleHardEdge", affectedByGravity: false, 15, 0.06f, Color.Red, Vector2.One));
						}
					}
					TimerToCatch = 0f;
					CatchTime = 0f;
					CaughtItemID = -1;
					PersistentFishingData = 0f;
					owner.Calamity().consecutiveCaughtFish = 0;
				}
			}
			if (CatchTime == -1f)
			{
				projectile.localAI[1] = CaughtItemID;
				ReelTheBobberChecks();
				owner.Calamity().consecutiveCaughtFish++;
				if (projectile.ai[0] == 2f)
				{
					return false;
				}
				isReelingIn = 1f;
				CatchTime = CaughtItemID;
				TimerToCatch = 0f;
			}
			if (projectile.ai[0] == 0f)
			{
				projectile.ai[1] = CatchTime;
				projectile.localAI[1] = TimerToCatch;
				if (CaughtItemID != -1 && isReelingIn == 0f)
				{
					projectile.localAI[1] = 0f;
					projectile.ai[1] = 0f;
				}
				projectile.ai[0] = isReelingIn;
			}
			return false;
		}
		case CalamityPlayer.FishingMinigames.ScoriaBobber:
		{
			if (PersistentFishingData == -1f)
			{
				PersistentFishingData = 1f;
				TimerToCatch = 300f;
			}
			Vector2 val2 = GetWaterLine();
			if (val2.Y >= projectile.Center.Y)
			{
				TimerToCatch = 300f;
				projectile.velocity.Y += 0.4f;
			}
			else if (projectile.ai[0] == 0f)
			{
				owner.Calamity().ShouldHideControls = true;
				if (owner.Calamity().pressedUp)
				{
					projectile.velocity.Y -= 0.125f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedDown)
				{
					projectile.velocity.Y += 0.125f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedLeft)
				{
					projectile.velocity.X -= 0.125f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedRight)
				{
					projectile.velocity.X += 0.125f;
					projectile.netUpdate = true;
				}
			}
			Projectile projectile5 = projectile;
			projectile5.velocity *= 0.975f;
			if (val2.Y <= projectile.Center.Y && projectile.ai[0] == 0f && (!projectile.lavaWet || owner.accLavaFishing))
			{
				TimerToCatch -= Main.rand.Next(1, 5);
				if (TimerToCatch <= 0f)
				{
					projectile.FishingCheck();
					if (projectile.ai[1] < 0f)
					{
						CaughtItemID = (int)projectile.localAI[1];
						projectile.ai[1] = 0f;
						projectile.localAI[1] = 0f;
						if (owner.whoAmI == Main.myPlayer)
						{
							for (int j = 0; j < 1000; j++)
							{
								Vector2 vectorToCheck2 = projectile.Center + new Vector2((float)Main.rand.Next(-200, 201), (float)Main.rand.Next(-200, 201));
								Point tileCoordsToCheck2 = vectorToCheck2.ToSafeTileCoordinates();
								val = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
								if (!((Rectangle)(ref val)).Contains((int)vectorToCheck2.X, (int)vectorToCheck2.Y) || Main.tile[tileCoordsToCheck2.X, tileCoordsToCheck2.Y].IsTileSolid() || Main.tile[tileCoordsToCheck2.X, tileCoordsToCheck2.Y].LiquidAmount <= 0)
								{
									continue;
								}
								if (projectile.localAI[2] >= 1f)
								{
									int customSonarText2 = (int)(projectile.localAI[2] - 1f);
									if (Main.popupText[customSonarText2].sonar)
									{
										Main.popupText[customSonarText2].position = vectorToCheck2 - FontAssets.MouseText.Value.MeasureString(Main.popupText[customSonarText2].name) / 2f;
									}
								}
								PersistentFishingDataVector2 = vectorToCheck2;
								TimerToCatch = 600f;
								break;
							}
						}
					}
				}
				if (PersistentFishingDataVector2 != Vector2.Zero && owner.whoAmI == Main.myPlayer)
				{
					Dust.NewDustPerfect(PersistentFishingDataVector2, 31);
					if (Main.rand.NextBool(3))
					{
						Dust.NewDustPerfect(PersistentFishingDataVector2, 6);
					}
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(PersistentFishingDataVector2, Vector2.One.RotatedByRandom(6.28) * Main.rand.NextFloat(1f, 4f), Color.Lerp(Color.DarkOrange, Color.DarkGray, Main.rand.NextFloat()), 15, 0.2f, 1f));
				}
				if (projectile.lavaWet && owner.whoAmI == Main.myPlayer)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.Zero, "CalamityMod/Particles/HighResHollowCircleHardEdge", affectedByGravity: false, 2, 0.01f, Color.DarkGray, Vector2.One));
				}
				List<(Vector2, int)> validRifts2 = new List<(Vector2, int)>();
				ActiveEntityIterator<Projectile>.Enumerator enumerator4 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					Projectile item4 = enumerator4.Current;
					if (item4.bobber && item4.owner == projectile.owner && item4.ai[0] == 0f && item4.Calamity().PersistentFishingDataVector2 != Vector2.Zero)
					{
						validRifts2.Add((item4.Calamity().PersistentFishingDataVector2, item4.whoAmI));
					}
				}
				foreach (var item5 in validRifts2)
				{
					if (projectile.Distance(item5.Item1) < 16f)
					{
						projectile.localAI[1] = Main.projectile[item5.Item2].Calamity().CaughtItemID;
						ReelTheBobberChecks();
						if (projectile.ai[0] < 2f)
						{
							projectile.ai[0] = 1f;
						}
						Main.projectile[item5.Item2].Calamity().PersistentFishingDataVector2 = PersistentFishingDataVector2;
						Main.projectile[item5.Item2].Calamity().CaughtItemID = CaughtItemID;
						break;
					}
				}
			}
			if (projectile.ai[0] == 0f)
			{
				return true;
			}
			break;
		}
		case CalamityPlayer.FishingMinigames.PerennialBobber:
			if (projectile.ai[0] == 0f)
			{
				if (projectile.wet)
				{
					projectile.extraUpdates = 0;
					if (cplayer.mouseRight && projectile.ai[1] == 0f)
					{
						projectile.localAI[1] += 2f;
						owner.lifeRegenCount -= 60;
					}
					break;
				}
				if (PersistentFishingData >= 0f && Main.npc[(int)PersistentFishingData].active && projectile.Distance(Main.npc[(int)PersistentFishingData].Center + PersistentFishingDataVector2) < 128f)
				{
					projectile.Center = Main.npc[(int)PersistentFishingData].Center + PersistentFishingDataVector2;
					projectile.velocity = Vector2.Zero;
					if (cplayer.mouseRight && projectile.ai[1] == 0f && owner.miscCounter % 10 == 0)
					{
						owner.lifeRegenCount -= 600;
						owner.Fishing_GetBait(out Item baitItem);
						int baitPower = baitItem?.bait ?? 0;
						if (baitItem != null && baitItem.type == 2673)
						{
							projectile.ai[0] = 1f;
							projectile.localAI[1] = 1f;
							ReelTheBobberChecks();
						}
						else if (baitItem?.type == ModContent.ItemType<BloodwormItem>())
						{
							projectile.ai[0] = 1f;
							projectile.localAI[1] = ModContent.NPCType<OldDuke>() * -1;
							baitPower = 4444;
							ReelTheBobberChecks();
						}
						else if (baitPower > 75)
						{
							baitPower = (int)Math.Pow(baitPower - 75, 0.5) + 75;
						}
						int projID = Projectile.NewProjectile(projectile.GetSource_FromThis(), Main.npc[(int)PersistentFishingData].Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), (int)owner.GetBestClassDamage().ApplyTo(owner.HeldItem.fishingPole + owner.fishingSkill + baitPower), 0f, owner.whoAmI, Main.npc[(int)PersistentFishingData].whoAmI);
						if (Main.projectile.IndexInRange(projID))
						{
							Main.projectile[projID].ArmorPenetration = 100;
						}
					}
					projectile.timeLeft++;
					projectile.extraUpdates = 0;
					return true;
				}
				if (PersistentFishingData >= 0f)
				{
					PersistentFishingData = -1f;
					projectile.velocity.Y -= 3f;
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					NPC item3 = enumerator3.Current;
					if (!item3.friendly && !item3.dontTakeDamage && projectile.Colliding(projectile.Hitbox, item3.Hitbox))
					{
						PersistentFishingDataVector2 = projectile.Center - item3.Center;
						PersistentFishingData = item3.whoAmI;
						break;
					}
				}
				if (PersistentFishingData == -1f)
				{
					projectile.extraUpdates = 1;
				}
				else
				{
					projectile.extraUpdates = 0;
				}
			}
			else if (projectile.ai[0] == 1f)
			{
				projectile.extraUpdates = 3;
			}
			break;
		case CalamityPlayer.FishingMinigames.NavystoneBobber:
		{
			if (CatchTime < 0f || (isReelingIn == 1f && CaughtItemID > 0))
			{
				owner.Calamity().ShouldHideControls = true;
			}
			if (isReelingIn != projectile.ai[0])
			{
				return false;
			}
			Vector2 AdjustedOwnerWaterline = GetWaterLine();
			if (AdjustedOwnerWaterline.Y < owner.Center.Y)
			{
				AdjustedOwnerWaterline.Y = owner.Center.Y;
			}
			if (AdjustedOwnerWaterline.Y >= projectile.Center.Y)
			{
				projectile.velocity.Y += 0.4f;
			}
			else
			{
				projectile.velocity.Y -= 0.1f;
			}
			Projectile projectile4 = projectile;
			projectile4.velocity *= 0.975f;
			if (CatchTime < 0f)
			{
				CatchTime++;
			}
			if (projectile.wet || (projectile.lavaWet && owner.accLavaFishing) || projectile.honeyWet)
			{
				TimerToCatch++;
			}
			int speedup = 15;
			int totalTime = 60 - speedup;
			if (TimerToCatch + 90f >= 300f && CatchTime >= 0f)
			{
				if (PersistentFishingData == 0f)
				{
					PersistentFishingData = (Main.rand.NextBool() ? 1 : (-1));
				}
				if (owner.whoAmI == Main.myPlayer)
				{
					float timer = TimerToCatch - 300f;
					SmallSplashAtOffset(new Vector2(200f * PersistentFishingData * (timer / 90f), 0f));
					SmallDustAtOffset(new Vector2(200f * PersistentFishingData * (timer / 90f), 0f), 68);
				}
			}
			if (TimerToCatch >= 300f && CatchTime >= 0f)
			{
				PersistentFishingData = 0f;
				projectile.FishingCheck();
				if (projectile.ai[1] < 0f)
				{
					CatchTime = -totalTime * 4;
					CaughtItemID = (int)projectile.localAI[1];
				}
				TimerToCatch = 0f;
			}
			if (CatchTime < 0f && CatchTime % (float)totalTime == 0f && owner.whoAmI == Main.myPlayer)
			{
				Splash();
				PersistentFishingData = Main.rand.Next(0, 2);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.UnitX.RotatedBy(PersistentFishingData * (float)Math.PI) * 10f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 45 - speedup, 0.04f, Color.Blue, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
			}
			if (CatchTime < 0f && CatchTime % (float)totalTime == -15f)
			{
				int playerDir = -1;
				if (owner.Calamity().pressedLeft)
				{
					playerDir = 1;
				}
				if (owner.Calamity().pressedRight)
				{
					playerDir = 0;
				}
				if ((float)playerDir != PersistentFishingData)
				{
					if (owner.whoAmI == Main.myPlayer)
					{
						if (playerDir >= 0)
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.UnitX.RotatedBy((float)playerDir * (float)Math.PI) * 10f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 15, 0.04f, Color.Red, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.2f));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.Zero, "CalamityMod/Particles/HighResHollowCircleHardEdge", affectedByGravity: false, 15, 0.06f, Color.Red, Vector2.One));
						}
					}
					TimerToCatch = 0f;
					CatchTime = 0f;
					CaughtItemID = -1;
					PersistentFishingData = 0f;
					owner.Calamity().consecutiveCaughtFish = 0;
				}
			}
			if (CatchTime == -1f)
			{
				projectile.localAI[1] = CaughtItemID;
				ReelTheBobberChecks();
				owner.Calamity().consecutiveCaughtFish++;
				if (projectile.ai[0] == 2f)
				{
					return false;
				}
				isReelingIn = 1f;
				CatchTime = CaughtItemID;
				TimerToCatch = 0f;
			}
			if (projectile.ai[0] == 0f)
			{
				projectile.ai[1] = CatchTime;
				projectile.localAI[1] = TimerToCatch;
				if (CaughtItemID != -1 && isReelingIn == 0f)
				{
					projectile.localAI[1] = 0f;
					projectile.ai[1] = 0f;
				}
				projectile.ai[0] = isReelingIn;
			}
			if (projectile.ai[0] == 0f)
			{
				return true;
			}
			break;
		}
		case CalamityPlayer.FishingMinigames.DevourerofCods:
		{
			List<int> fishToEat = TheDevourerofCods.FishToEat;
			if (projectile.ai[1] < -1f)
			{
				if (fishToEat.Contains((int)projectile.localAI[1]))
				{
					projectile.ai[1] = 0f;
					projectile.localAI[1] = 0f;
					SoundEngine.PlaySound(in SoundID.Item2, projectile.Center);
					if (!owner.HasBuff(207))
					{
						if (owner.HasBuff(206))
						{
							int bIndex = owner.FindBuffIndex(206);
							if (owner.buffTime[bIndex] < CalamityUtils.MinutesToFrames(24))
							{
								owner.buffTime[bIndex] += 300;
							}
						}
						else
						{
							owner.AddBuff(206, 300);
						}
					}
					else
					{
						int bIndex2 = owner.FindBuffIndex(207);
						if (owner.buffTime[bIndex2] < CalamityUtils.MinutesToFrames(24))
						{
							owner.buffTime[bIndex2] += 300;
						}
					}
				}
				else
				{
					projectile.ai[1] = -30f;
				}
			}
			return false;
		}
		case CalamityPlayer.FishingMinigames.SkylineBobber:
		{
			if (CatchTime < 0f || (isReelingIn == 1f && CaughtItemID > 0))
			{
				owner.Calamity().ShouldHideControls = true;
			}
			if (PersistentFishingData == -1f)
			{
				PersistentFishingData = 1f;
				TimerToCatch = 300f;
			}
			Vector2 Waterline = GetWaterLine();
			if (Waterline.Y >= projectile.Center.Y)
			{
				projectile.velocity.Y += 0.4f;
			}
			else if (projectile.ai[0] <= 0f)
			{
				projectile.velocity.Y -= 0.1f;
			}
			if (projectile.ai[0] <= 1f)
			{
				owner.Calamity().ShouldHideControls = true;
			}
			Projectile projectile2 = projectile;
			projectile2.velocity *= 0.975f;
			if (projectile.wet)
			{
				Projectile projectile3 = projectile;
				projectile3.velocity *= 0.99f;
			}
			if (projectile.lavaWet && owner.whoAmI == Main.myPlayer)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(projectile.Center, Vector2.Zero, "CalamityMod/Particles/HighResHollowCircleHardEdge", affectedByGravity: false, 2, 0.01f, Color.DarkGray, Vector2.One));
			}
			if (projectile.ai[0] == 0f)
			{
				if (owner.Calamity().pressedUp && projectile.velocity.Y < 0f)
				{
					projectile.velocity.Y -= 0.3f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedUp && Waterline.Y < projectile.Center.Y)
				{
					projectile.velocity.Y -= 0.3f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedDown)
				{
					projectile.velocity.Y += 0.25f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedLeft)
				{
					projectile.velocity.X -= 0.25f;
					projectile.netUpdate = true;
				}
				if (owner.Calamity().pressedRight)
				{
					projectile.velocity.X += 0.25f;
					projectile.netUpdate = true;
				}
				if (Waterline.Y <= projectile.Center.Y - 8f)
				{
					TimerToCatch -= Main.rand.Next(1, 5);
				}
				if (TimerToCatch <= 0f)
				{
					projectile.FishingCheck();
					PlayerFishingConditions fishingCond = Main.player[projectile.owner].GetFishingConditions();
					bool canLavaFish = ItemID.Sets.CanFishInLava[fishingCond.PoleItemType] || ItemID.Sets.IsLavaBait[fishingCond.BaitItemType] || Main.player[projectile.owner].accLavaFishing;
					if (projectile.ai[1] < 0f && (!projectile.lavaWet || canLavaFish))
					{
						CaughtItemID = (int)projectile.localAI[1];
						projectile.ai[1] = 0f;
						projectile.localAI[1] = 0f;
						for (int i = 0; i < 1000; i++)
						{
							Vector2 vectorToCheck = Waterline + new Vector2((float)Main.rand.Next(-200, 201), (float)Main.rand.Next(-200, 32));
							Point tileCoordsToCheck = vectorToCheck.ToSafeTileCoordinates();
							val = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
							if (!((Rectangle)(ref val)).Contains((int)vectorToCheck.X, (int)vectorToCheck.Y) || Main.tile[tileCoordsToCheck.X, tileCoordsToCheck.Y].IsTileSolid() || Main.tile[tileCoordsToCheck.X, tileCoordsToCheck.Y].LiquidAmount != 0)
							{
								continue;
							}
							if (projectile.localAI[2] >= 1f)
							{
								int customSonarText = (int)(projectile.localAI[2] - 1f);
								if (Main.popupText[customSonarText].sonar)
								{
									Main.popupText[customSonarText].position = vectorToCheck - FontAssets.MouseText.Value.MeasureString(Main.popupText[customSonarText].name) / 2f;
								}
							}
							PersistentFishingDataVector2 = vectorToCheck;
							TimerToCatch = 600f;
							break;
						}
					}
				}
				if (PersistentFishingDataVector2 != Vector2.Zero && owner.miscCounter % 15 == 0 && owner.whoAmI == Main.myPlayer)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(PersistentFishingDataVector2, Vector2.Zero, "CalamityMod/Particles/HighResHollowCircleHardEdge", affectedByGravity: false, 25, 0.02f, Color.Green, Vector2.One));
				}
				List<(Vector2, int)> validRifts = new List<(Vector2, int)>();
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile item = enumerator.Current;
					if (item.bobber && item.owner == projectile.owner && item.ai[0] == 0f && item.Calamity().PersistentFishingDataVector2 != Vector2.Zero)
					{
						validRifts.Add((item.Calamity().PersistentFishingDataVector2, item.whoAmI));
					}
				}
				foreach (var item2 in validRifts)
				{
					if (projectile.Distance(item2.Item1) < 16f)
					{
						projectile.localAI[1] = Main.projectile[item2.Item2].Calamity().CaughtItemID;
						projectile.ai[0] = -1f;
						Main.projectile[item2.Item2].Calamity().PersistentFishingDataVector2 = PersistentFishingDataVector2;
						Main.projectile[item2.Item2].Calamity().CaughtItemID = CaughtItemID;
						break;
					}
				}
			}
			if (projectile.ai[0] == -1f && (Waterline.Y <= projectile.Center.Y || ((Vector2)(ref projectile.velocity)).Length() < 0.5f))
			{
				ReelTheBobberChecks();
				if (projectile.ai[0] < 2f)
				{
					projectile.ai[0] = 1f;
				}
			}
			if (projectile.ai[0] == 0f)
			{
				return true;
			}
			break;
		}
		}
		return false;
		Vector2 GetWaterLine()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			Vector2 FoundWaterline = projectile.Center;
			Point tilePos = projectile.Center.ToTileCoordinates();
			if (Main.tile[tilePos.X, tilePos.Y].LiquidAmount > 0)
			{
				for (int k = 0; k < 1000; k++)
				{
					if (k > 0 && k < Main.maxTilesY && Main.tile[tilePos.X, tilePos.Y - k].LiquidAmount <= 0)
					{
						FoundWaterline.Y = (float)(tilePos.Y - k + 1) * 16f;
						FoundWaterline.Y += (1f - (float)(int)Main.tile[tilePos.X, tilePos.Y - k + 1].LiquidAmount / 255f) * 16f;
						break;
					}
				}
			}
			else
			{
				for (int l = 0; l < 1000; l++)
				{
					if (l > 0 && l < Main.maxTilesY && Main.tile[tilePos.X, tilePos.Y + l].LiquidAmount != 0)
					{
						FoundWaterline.Y = (float)(tilePos.Y + l) * 16f;
						FoundWaterline.Y += (1f - (float)(int)Main.tile[tilePos.X, tilePos.Y + l].LiquidAmount / 255f) * 16f;
						break;
					}
				}
			}
			return FoundWaterline;
		}
		void ReelTheBobberChecks()
		{
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			if (projectile.localAI[1] == 1f)
			{
				if (Main.netMode != 1)
				{
					NPC.SpawnOnPlayer(owner.whoAmI, 370);
				}
				else
				{
					NetMessage.SendData(61, -1, -1, null, owner.whoAmI, 370f);
				}
				projectile.ai[0] = 2f;
			}
			else if (projectile.localAI[1] < 1f)
			{
				Point point = default(Point);
				((Point)(ref point))._002Ector((int)projectile.position.X, (int)projectile.position.Y);
				int num = (int)(0f - projectile.localAI[1]);
				if (num == 618)
				{
					point.Y += 64;
				}
				if (Main.netMode == 1)
				{
					NetMessage.SendData(130, -1, -1, null, point.X / 16, point.Y / 16, num);
				}
				else
				{
					if (num == 682)
					{
						NPC.unlockedSlimeRedSpawn = true;
					}
					NPC.NewNPC(new EntitySource_FishedOut(owner), point.X, point.Y, num);
					projectile.ai[0] = 2f;
					WorldGen.CheckAchievement_RealEstateAndTownSlimes();
				}
			}
			else if (Main.rand.NextBool(7) && !owner.accFishingLine)
			{
				projectile.ai[0] = 2f;
				projectile.ai[1] = 0f;
				projectile.localAI[1] = 0f;
			}
			else
			{
				projectile.ai[1] = projectile.localAI[1];
			}
			projectile.netUpdate = true;
		}
		void SmallDustAtOffset(Vector2 offset, int dustID)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			int num = Dust.NewDust(new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f) + offset, projectile.width + 12, 24, dustID);
			Main.dust[num].velocity.Y -= 4f;
			Main.dust[num].velocity.X *= 2.5f;
			Main.dust[num].scale = 0.8f;
			Main.dust[num].alpha = 100;
			Main.dust[num].noGravity = true;
		}
		void SmallSplashAtOffset(Vector2 offset)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			int num = Dust.NewDust(new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f) + offset, projectile.width + 12, 24, Dust.dustWater());
			Main.dust[num].velocity.Y -= 4f;
			Main.dust[num].velocity.X *= 2.5f;
			Main.dust[num].scale = 0.8f;
			Main.dust[num].alpha = 100;
			Main.dust[num].noGravity = true;
		}
		void Splash()
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			for (int k = 0; k < 100; k++)
			{
				int num = Dust.NewDust(new Vector2(projectile.position.X - 6f, projectile.position.Y - 10f), projectile.width + 12, 24, Dust.dustWater());
				Main.dust[num].velocity.Y -= 4f;
				Main.dust[num].velocity.X *= 2.5f;
				Main.dust[num].scale = 0.8f;
				Main.dust[num].alpha = 100;
				Main.dust[num].noGravity = true;
			}
			SoundEngine.PlaySound(in SoundID.SplashWeak, projectile.Center);
		}
	}

	public override void AI(Projectile projectile)
	{
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1059: Unknown result type (might be due to invalid IL or missing references)
		//IL_107d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_1129: Unknown result type (might be due to invalid IL or missing references)
		//IL_1133: Unknown result type (might be due to invalid IL or missing references)
		//IL_1138: Unknown result type (might be due to invalid IL or missing references)
		//IL_1165: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1241: Unknown result type (might be due to invalid IL or missing references)
		//IL_1247: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1265: Unknown result type (might be due to invalid IL or missing references)
		//IL_126a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1284: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1323: Unknown result type (might be due to invalid IL or missing references)
		//IL_1329: Unknown result type (might be due to invalid IL or missing references)
		//IL_137b: Unknown result type (might be due to invalid IL or missing references)
		//IL_139c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1822: Unknown result type (might be due to invalid IL or missing references)
		//IL_1828: Unknown result type (might be due to invalid IL or missing references)
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18de: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_191c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1923: Unknown result type (might be due to invalid IL or missing references)
		//IL_192d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1933: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17de: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1626: Unknown result type (might be due to invalid IL or missing references)
		//IL_1647: Unknown result type (might be due to invalid IL or missing references)
		//IL_164d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1446: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_146d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_172d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_151e: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_154d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1552: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2205: Unknown result type (might be due to invalid IL or missing references)
		//IL_220b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1def: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e15: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2115: Unknown result type (might be due to invalid IL or missing references)
		//IL_211b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2120: Unknown result type (might be due to invalid IL or missing references)
		//IL_2126: Unknown result type (might be due to invalid IL or missing references)
		//IL_212b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2135: Unknown result type (might be due to invalid IL or missing references)
		//IL_2141: Unknown result type (might be due to invalid IL or missing references)
		//IL_214b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2006: Unknown result type (might be due to invalid IL or missing references)
		//IL_2012: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_2044: Unknown result type (might be due to invalid IL or missing references)
		//IL_2058: Unknown result type (might be due to invalid IL or missing references)
		//IL_205d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2063: Unknown result type (might be due to invalid IL or missing references)
		//IL_2068: Unknown result type (might be due to invalid IL or missing references)
		//IL_2081: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2259: Unknown result type (might be due to invalid IL or missing references)
		//IL_2170: Unknown result type (might be due to invalid IL or missing references)
		//IL_2176: Unknown result type (might be due to invalid IL or missing references)
		//IL_217b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2186: Unknown result type (might be due to invalid IL or missing references)
		//IL_218b: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2441: Unknown result type (might be due to invalid IL or missing references)
		//IL_225e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2285: Unknown result type (might be due to invalid IL or missing references)
		//IL_2293: Unknown result type (might be due to invalid IL or missing references)
		//IL_2299: Unknown result type (might be due to invalid IL or missing references)
		//IL_229b: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2252: Unknown result type (might be due to invalid IL or missing references)
		//IL_224b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2446: Unknown result type (might be due to invalid IL or missing references)
		//IL_246d: Unknown result type (might be due to invalid IL or missing references)
		//IL_247b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2481: Unknown result type (might be due to invalid IL or missing references)
		//IL_2483: Unknown result type (might be due to invalid IL or missing references)
		//IL_2488: Unknown result type (might be due to invalid IL or missing references)
		//IL_243a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2433: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_22af: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2313: Unknown result type (might be due to invalid IL or missing references)
		//IL_2315: Unknown result type (might be due to invalid IL or missing references)
		//IL_2326: Unknown result type (might be due to invalid IL or missing references)
		//IL_232c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2331: Unknown result type (might be due to invalid IL or missing references)
		//IL_2337: Unknown result type (might be due to invalid IL or missing references)
		//IL_2341: Unknown result type (might be due to invalid IL or missing references)
		//IL_234d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2490: Unknown result type (might be due to invalid IL or missing references)
		//IL_2495: Unknown result type (might be due to invalid IL or missing references)
		//IL_2497: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_250e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2514: Unknown result type (might be due to invalid IL or missing references)
		//IL_2519: Unknown result type (might be due to invalid IL or missing references)
		//IL_251f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2529: Unknown result type (might be due to invalid IL or missing references)
		//IL_2535: Unknown result type (might be due to invalid IL or missing references)
		//IL_2374: Unknown result type (might be due to invalid IL or missing references)
		//IL_2388: Unknown result type (might be due to invalid IL or missing references)
		//IL_238d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2393: Unknown result type (might be due to invalid IL or missing references)
		//IL_2398: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23be: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_255c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2570: Unknown result type (might be due to invalid IL or missing references)
		//IL_2575: Unknown result type (might be due to invalid IL or missing references)
		//IL_257b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2580: Unknown result type (might be due to invalid IL or missing references)
		//IL_2599: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ad: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!frameOneHacksExecuted)
		{
			if (projectile.hostile)
			{
				if (projectile.type == 498 && Main.expertMode)
				{
					projectile.damage /= 2;
				}
				if (!CalamityPlayer.areThereAnyDamnBosses)
				{
					switch (projectile.type)
					{
					case 55:
					case 82:
					case 84:
					case 96:
					case 115:
					case 128:
					case 177:
					case 180:
					case 240:
					case 257:
					case 264:
					case 288:
					case 302:
					case 303:
					case 501:
					case 508:
					case 811:
					case 813:
					case 814:
					case 909:
						projectile.damage = (int)Math.Round((double)projectile.damage * 0.65);
						break;
					}
				}
			}
			else
			{
				if ((projectile.minion || ProjectileID.Sets.MinionShot[projectile.type] || projectile.sentry || ProjectileID.Sets.SentryShot[projectile.type]) && (player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfDeliveranceSpear>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfConvergenceCrystal>()] > 0))
				{
					projectile.damage = (int)((double)projectile.damage * 0.1);
				}
				if (projectile.CountsAsClass<RogueDamageClass>() && stealthStrike)
				{
					int gloveArmorPenAmt = (modPlayer.nanotech ? 15 : 8);
					if (modPlayer.filthyGlove || modPlayer.bloodyGlove)
					{
						projectile.ArmorPenetration += gloveArmorPenAmt;
					}
				}
			}
			if (NPC.downedMoonlord && CalamityProjectileSets.IsBuffedDungeonProjectile[projectile.type] && ((projectile.type != 303 && projectile.type != 299) || !(projectile.ai[1] > 0f)))
			{
				projectile.damage += 30;
			}
			if (DownedBossSystem.downedDoG && (Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && CalamityProjectileSets.IsBuffedEventProjectile[projectile.type])
			{
				projectile.damage += 15;
			}
			if (projectile.type == 566 || projectile.type == 181)
			{
				if (projectile.timeLeft > 570 && player.HeldItem.type == 2888)
				{
					projectile.DamageType = DamageClass.Ranged;
				}
			}
			else if (projectile.type == 476)
			{
				projectile.DamageType = DamageClass.Magic;
			}
			frameOneHacksExecuted = true;
		}
		switch (projectile.type)
		{
		case 28:
		case 29:
		case 37:
		case 99:
		case 184:
		case 185:
		case 186:
		case 187:
		case 470:
		case 516:
		case 519:
		case 637:
		case 773:
		case 903:
		case 904:
		case 905:
		case 906:
		case 910:
		case 911:
		case 980:
		case 1005:
		case 1013:
		case 1014:
		case 1021:
			projectile.extraUpdates = (Main.zenithWorld ? 1 : 0);
			break;
		case 811:
		case 836:
		case 909:
			projectile.extraUpdates = (CalamityWorld.revenge ? 1 : 0);
			break;
		case 181:
		case 189:
		case 307:
		case 316:
		case 566:
			projectile.extraUpdates = 1;
			break;
		}
		if (projectile.aiStyle == 33 && projectile.ai[2] == 1f && projectile.localAI[0] == 0f)
		{
			projectile.localAI[1]--;
		}
		if (projectile.type >= 833 && projectile.type <= 835 && projectile.ai[0] == 5f)
		{
			projectile.tileCollide = false;
		}
		if (projectile.type == 312 && projectile.ai[0] >= 20f)
		{
			projectile.ai[2]++;
			if (projectile.ai[2] < 30f)
			{
				projectile.velocity.Y -= 0.5f;
			}
		}
		if (projectile.type == 63 && projectile.ai[0] == 1f)
		{
			if (projectile.ai[1] > 0f)
			{
				projectile.ai[2]++;
				if (projectile.ai[2] <= 11f)
				{
					projectile.ai[1]--;
				}
			}
			else
			{
				projectile.velocity *= 1.33f;
			}
		}
		if (projectile.type == 154 && projectile.ai[0] == 1f && projectile.ai[1] > 0f)
		{
			projectile.ai[2]++;
			if (projectile.ai[2] <= 7f)
			{
				projectile.ai[1]--;
			}
		}
		if (projectile.type == 247 && projectile.ai[0] == 1f)
		{
			if (projectile.ai[1] > 0f)
			{
				projectile.ai[2]++;
				if (projectile.ai[2] <= 5f)
				{
					projectile.ai[1]--;
				}
			}
			else
			{
				projectile.velocity *= 1.33f;
			}
		}
		if (projectile.type == 9)
		{
			projectile.tileCollide = false;
		}
		if (projectile.type == 973 && projectile.localAI[1] < 32f)
		{
			projectile.localAI[1] = 32f;
		}
		if (projectile.type == 1013 && Main.zenithWorld && Main.rand.Next(100) >= 95)
		{
			projectile.velocity *= Main.rand.NextFloat(0.9f, 1.25f);
		}
		if (projectile.type == 719)
		{
			projectile.ai[0]--;
		}
		if ((projectile.type == 83 || projectile.type == 100) && projectile.ai[0] == 1f && ((Vector2)(ref projectile.velocity)).Length() < 8f)
		{
			projectile.velocity *= 1.0025f;
		}
		if (projectile.type == 384 && projectile.timeLeft > 780)
		{
			if (projectile.alpha < 200)
			{
				projectile.alpha = 200;
			}
			if (projectile.alpha > 220)
			{
				projectile.alpha = 220;
			}
		}
		if (projectile.type == 872 && (CalamityWorld.revenge || BossRushEvent.BossRushActive))
		{
			int spreadOutTime = 90;
			if (projectile.timeLeft > 660 - spreadOutTime)
			{
				projectile.velocity *= 1.015525f;
			}
		}
		if (projectile.type == 876 && projectile.damage > projectile.originalDamage)
		{
			if (projectile.ai[0] == 0f)
			{
				projectile.originalDamage = projectile.damage;
				projectile.ai[0] = 1f;
			}
			else
			{
				projectile.damage = projectile.originalDamage;
			}
		}
		if (ProjectileID.Sets.IsAGolfBall[projectile.type])
		{
			int auricOreID = ModContent.TileType<AuricOre>();
			int auricRepulserID = ModContent.TileType<AuricRepulserPanelTile>();
			List<Point> EdgeTiles = new List<Point>();
			int extraDist = 8;
			int left = (int)projectile.position.X - extraDist;
			int up = (int)projectile.position.Y - extraDist;
			int right = (int)projectile.Right.X + extraDist;
			int down = (int)projectile.Bottom.Y + extraDist;
			if (left % 16 == 0)
			{
				left--;
			}
			if (up % 16 == 0)
			{
				up--;
			}
			if (right % 16 == 0)
			{
				right++;
			}
			if (down % 16 == 0)
			{
				down++;
			}
			int width = right / 16 - left / 16;
			int height = down / 16 - up / 16;
			left /= 16;
			up /= 16;
			for (int i = left; i <= left + width; i++)
			{
				EdgeTiles.Add(new Point(i, up));
				EdgeTiles.Add(new Point(i, up + height));
			}
			for (int j = up; j < up + height; j++)
			{
				EdgeTiles.Add(new Point(left, j));
				EdgeTiles.Add(new Point(left + width, j));
			}
			foreach (Point touchedTile in EdgeTiles)
			{
				Tile tile = Framing.GetTileSafely(touchedTile);
				if (tile.HasTile && tile.HasUnactuatedTile && (tile.TileType == auricOreID || tile.TileType == auricRepulserID))
				{
					if (tile.TileType == auricOreID)
					{
						AuricOre.Animate = true;
					}
					Vector2 yeetVec = Vector2.Normalize(projectile.Center - touchedTile.ToWorldCoordinates());
					projectile.velocity += yeetVec * 40f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1");
					style.Pitch = 0.4f;
					SoundEngine.PlaySound(in style);
				}
			}
		}
		if (projectile.npcProj || projectile.trap || !projectile.friendly || projectile.damage <= 0)
		{
			return;
		}
		if (modPlayer.fungalSymbiote && player.HasBuff(ModContent.BuffType<Mushy>()) && Main.player[projectile.owner].miscCounter % 6 == 0 && projectile.FinalExtraUpdate() && projectile.owner == Main.myPlayer && player.ownedProjectileCounts[131] < 15)
		{
			if (projectile.type == ModContent.ProjectileType<NebulashFlail>() || projectile.type == ModContent.ProjectileType<CosmicDischargeFlail>() || projectile.type == ModContent.ProjectileType<MourningstarFlail>() || projectile.type == 611)
			{
				Vector2 vector24 = Main.OffsetsPlayerOnhand[Main.player[projectile.owner].bodyFrame.Y / 56] * 2f;
				if (player.direction != 1)
				{
					vector24.X = (float)player.bodyFrame.Width - vector24.X;
				}
				if (player.gravDir != 1f)
				{
					vector24.Y = (float)player.bodyFrame.Height - vector24.Y;
				}
				vector24 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
				Vector2 newCenter = player.RotatedRelativePoint(player.position + vector24, reverseRotation: true) + projectile.velocity;
				Projectile.NewProjectile(projectile.GetSource_FromThis(), newCenter, Vector2.Zero, 131, 0, 0f, projectile.owner);
			}
			else
			{
				Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, 131, 0, 0f, projectile.owner);
			}
		}
		if (projectile.CountsAsClass<RogueDamageClass>())
		{
			if (!LocketClone && !CannotProc)
			{
				if (modPlayer.nanotech)
				{
					if (Main.player[projectile.owner].miscCounter % 30 == 0 && projectile.FinalExtraUpdate() && projectile.owner == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<global::CalamityMod.Projectiles.Typeless.Nanotech>()] < 5)
					{
						int damage = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(60f);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<global::CalamityMod.Projectiles.Typeless.Nanotech>(), damage, 0f, projectile.owner);
					}
				}
				else if (modPlayer.moonCrown && Main.player[projectile.owner].miscCounter % 120 == 0 && projectile.FinalExtraUpdate() && projectile.owner == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<MoonSigil>()] < 5)
				{
					int damage2 = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(42f);
					int proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<MoonSigil>(), damage2, 0f, projectile.owner);
					if (proj.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj].DamageType = DamageClass.Generic;
					}
				}
				if (modPlayer.dragonScales && Main.player[projectile.owner].miscCounter % 50 == 0 && projectile.FinalExtraUpdate() && projectile.owner == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<DragonShit>()] < 5)
				{
					int damage3 = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(DragonScales.ShitBaseDamage);
					int proj2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.One.RotatedByRandom(6.2831854820251465) * 1.2f, ModContent.ProjectileType<DragonShit>(), damage3, 0f, projectile.owner);
					if (proj2.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj2].DamageType = DamageClass.Generic;
						Main.projectile[proj2].ArmorPenetration = 10;
					}
				}
				if (modPlayer.daedalusSplit && Main.player[projectile.owner].miscCounter % 30 == 0 && projectile.FinalExtraUpdate() && projectile.owner == Main.myPlayer && player.ownedProjectileCounts[90] < DaedalusHeadRogue.ShardCountLimit)
				{
					int crystalDamage = CalamityUtils.DamageSoftCap((double)projectile.damage * DaedalusHeadRogue.ShardDamageRatio, DaedalusHeadRogue.ShardDamageSoftcap);
					Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
					int shard = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, velocity, 90, crystalDamage, 0f, projectile.owner);
					if (shard.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[shard].DamageType = DamageClass.Generic;
					}
				}
			}
			if (player.meleeEnchant > 0 && !projectile.noEnchantments && !projectile.noEnchantmentVisuals)
			{
				switch (player.meleeEnchant)
				{
				case 1:
					if (!Main.rand.NextBool(3))
					{
						Dust dust3 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100);
						dust3.noGravity = true;
						dust3.fadeIn = 1.5f;
						dust3.velocity *= 0.25f;
					}
					break;
				case 2:
					if (Main.rand.NextBool())
					{
						Dust dust5 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 75, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
						dust5.noGravity = true;
						dust5.velocity *= 0.7f;
						dust5.velocity.Y -= 0.5f;
					}
					break;
				case 3:
					if (Main.rand.NextBool())
					{
						Dust dust7 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
						dust7.noGravity = true;
						dust7.velocity *= 0.7f;
						dust7.velocity.Y -= 0.5f;
					}
					break;
				case 4:
					if (Main.rand.NextBool())
					{
						Dust dust6 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default(Color), 1.1f);
						dust6.noGravity = true;
						dust6.velocity *= 0.5f;
					}
					break;
				case 5:
					if (Main.rand.NextBool())
					{
						Dust dust8 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 169, 0f, 0f, 100);
						dust8.velocity.X += projectile.direction;
						dust8.velocity.Y += 0.2f;
						dust8.noGravity = true;
					}
					break;
				case 6:
					if (Main.rand.NextBool())
					{
						Dust dust2 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100);
						dust2.velocity.X += projectile.direction;
						dust2.velocity.Y += 0.2f;
						dust2.noGravity = true;
					}
					break;
				case 8:
					if (Main.rand.NextBool(4))
					{
						Dust dust4 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 46, 0f, 0f, 100);
						dust4.noGravity = true;
						dust4.fadeIn = 1.5f;
						dust4.velocity *= 0.25f;
					}
					break;
				case 99:
				{
					int dustType = ((!player.Calamity().flaskHoly) ? ((!player.Calamity().flaskBrimstone) ? ((!Main.rand.NextBool()) ? 1 : 121) : (Main.rand.NextBool() ? 114 : ModContent.DustType<BrimstoneFlame>())) : (Main.rand.NextBool() ? 87 : 244));
					if (Main.rand.NextBool(4))
					{
						Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, default(Color), Main.rand.NextFloat(0.6f, 0.9f));
						dust.noGravity = dust.type != 121;
						if (!player.Calamity().flaskHoly)
						{
							dust.fadeIn = 1f;
						}
						dust.velocity = (Vector2)((player.Calamity().flaskHoly && Main.rand.NextBool(3)) ? new Vector2(Main.rand.NextFloat(-0.9f, 0.9f), Main.rand.NextFloat(-6.6f, -9.8f)) : ((dust.type == 121) ? new Vector2(Main.rand.NextFloat(-0.7f, 0.7f), Main.rand.NextFloat(0.6f, 1.8f)) : (-projectile.velocity * 0.2f)));
					}
					break;
				}
				}
			}
		}
		if ((projectile.CountsAsClass<MeleeDamageClass>() || projectile.CountsAsClass<SummonMeleeSpeedDamageClass>()) && (player.Calamity().flaskBrimstone || player.Calamity().flaskCrumbling || player.Calamity().flaskHoly) && !projectile.noEnchantments && !projectile.noEnchantmentVisuals)
		{
			int dustType2 = ((!player.Calamity().flaskHoly) ? ((!player.Calamity().flaskBrimstone) ? ((!Main.rand.NextBool()) ? 1 : 121) : (Main.rand.NextBool() ? 114 : ModContent.DustType<BrimstoneFlame>())) : (Main.rand.NextBool() ? 87 : 244));
			if (Main.rand.NextBool(player.Calamity().flaskCrumbling ? 5 : 4))
			{
				Dust dust9 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dustType2, 0f, 0f, 100, default(Color), Main.rand.NextFloat(0.6f, 0.9f));
				dust9.noGravity = dust9.type != 121;
				if (player.Calamity().flaskBrimstone)
				{
					dust9.fadeIn = 0.8f;
				}
				dust9.velocity = (Vector2)((player.Calamity().flaskHoly && Main.rand.NextBool(3)) ? new Vector2(Main.rand.NextFloat(-0.9f, 0.9f), Main.rand.NextFloat(-6.6f, -9.8f)) : ((dust9.type == 121) ? new Vector2(Main.rand.NextFloat(-0.7f, 0.7f), Main.rand.NextFloat(0.6f, 1.8f)) : (-projectile.velocity * 0.2f)));
			}
		}
		if (modPlayer.theBee && projectile.owner == Main.myPlayer && projectile.damage > 0 && player.statLife >= player.statLifeMax2 && (!modPlayer.HasAnyEnergyShield || modPlayer.TotalEnergyShielding >= modPlayer.TotalMaxShieldDurability) && Main.rand.NextBool(5))
		{
			Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, 91, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.5f).noGravity = true;
		}
		if (modPlayer.eGauntlet && modPlayer.eGauntletVisuals && projectile.CountsAsClass<MeleeDamageClass>() && Main.rand.NextBool(3))
		{
			int element = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 66, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f);
			Main.dust[element].noGravity = true;
		}
		if (!projectile.CountsAsClass<MeleeDamageClass>() && player.meleeEnchant == 7 && !projectile.noEnchantmentVisuals)
		{
			Vector2 velocity2 = projectile.velocity;
			if ((double)((Vector2)(ref velocity2)).Length() > 4.0)
			{
				velocity2 *= 4f / ((Vector2)(ref velocity2)).Length();
			}
			if (Main.rand.NextBool(20))
			{
				Dust dust10 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, Main.rand.Next(139, 143), velocity2.X, velocity2.Y, 0, default(Color), 1.2f);
				dust10.scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
				dust10.velocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
				dust10.velocity.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
				dust10.velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
				dust10.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
			}
			if (Main.rand.NextBool(40) && !Main.dedServ)
			{
				int Type = Main.rand.Next(276, 283);
				Gore gore = Gore.NewGoreDirect(projectile.GetSource_FromAI(), projectile.position, velocity2, Type);
				gore.scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
				gore.velocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
				gore.velocity.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
				gore.velocity.X += (float)Main.rand.Next(-50, 51) * 0.05f;
				gore.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.05f;
			}
		}
		if (!showArcFlash && projectile.numHits == 0)
		{
			showArcFlash = true;
		}
		if (arcFlashCooldown >= 0)
		{
			arcFlashCooldown--;
		}
		if (arcFlashCooldown == 0)
		{
			showArcFlash = true;
		}
		if (conditionalHomingRange > 0f && Main.player[projectile.owner].heldProj != projectile.whoAmI && projectile.aiStyle != 75)
		{
			CalamityUtils.HomeInOnNPC(projectile, !projectile.tileCollide, conditionalHomingRange, 12f, 20f, respectIFrames: true);
		}
		if (brimstoneBullets)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(projectile.Center + projectile.velocity * 3f, projectile.velocity, affectedByGravity: false, 2, 0.9f, Color.Crimson * 0.7f));
			Dust dust11 = Dust.NewDustPerfect(projectile.Center - projectile.velocity, Main.rand.NextBool(3) ? 90 : ModContent.DustType<BrimstoneFlame>(), projectile.velocity * Main.rand.NextFloat(0.05f, 0.9f));
			dust11.noGravity = true;
			dust11.scale = Main.rand.NextFloat(0.5f, 1f);
		}
		if (fireBullet && projectile.timeLeft > 200)
		{
			Vector2 spawnOffset = Utils.RotatedBy(new Vector2((float)Math.Sin((float)projectile.timeLeft / 25f * ((float)Math.PI * 2f)) * 8f, 10f), (double)projectile.rotation, default(Vector2));
			for (int k = 0; k < 2; k++)
			{
				Dust dust12 = Dust.NewDustPerfect(projectile.Center + spawnOffset, Main.rand.NextBool() ? 174 : 6, projectile.velocity * Main.rand.NextFloat(0.1f, 0.9f));
				dust12.noGravity = true;
				dust12.scale = Main.rand.NextFloat(0.4f, 0.8f);
			}
		}
		if (iceBullet && projectile.timeLeft > 200)
		{
			Vector2 spawnOffset2 = Utils.RotatedBy(new Vector2((float)Math.Sin((float)projectile.timeLeft / 25f * ((float)Math.PI * 2f)) * -8f, 10f), (double)projectile.rotation, default(Vector2));
			for (int l = 0; l < 2; l++)
			{
				Dust dust13 = Dust.NewDustPerfect(projectile.Center + spawnOffset2, Main.rand.NextBool() ? 135 : 137, projectile.velocity * Main.rand.NextFloat(0.1f, 0.9f));
				dust13.noGravity = true;
				dust13.scale = Main.rand.NextFloat(0.4f, 0.8f);
			}
		}
		if (shockBullet)
		{
			float targetDist = Vector2.Distance(player.Center, projectile.Center);
			if (projectile.timeLeft > 200 && targetDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + projectile.velocity, -projectile.velocity * 0.05f, affectedByGravity: false, 2, 1.1f, Color.Turquoise * 0.75f));
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + Main.rand.NextVector2Circular(6f, 6f), -projectile.velocity * Main.rand.NextFloat(0.05f, 0.4f), affectedByGravity: false, 20, 0.4f, Color.Turquoise * 0.75f));
				}
			}
		}
		if (pearlBullet1 || pearlBullet2 || pearlBullet3)
		{
			float targetDist2 = Vector2.Distance(player.Center, projectile.Center);
			if (projectile.timeLeft > 200 && targetDist2 < 1400f)
			{
				Color color = (pearlBullet1 ? Color.LightBlue : (pearlBullet2 ? Color.LightPink : Color.Khaki));
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(projectile.Center + projectile.velocity * 1.5f, -projectile.velocity * 0.05f, affectedByGravity: false, 3, 0.0093f, color, new Vector2(0.6f, 1.8f), quickShrink: false, glow: false));
				if (Main.rand.NextBool(5))
				{
					GeneralParticleHandler.SpawnParticle(new PearlParticle(projectile.Center + Main.rand.NextVector2Circular(6f, 6f), -projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f), affectedByGravity: false, Main.rand.Next(15, 21), Main.rand.NextFloat(0.4f, 0.55f), color, 0.9f, Main.rand.NextFloat(1f, -1f), hitTiles: true));
				}
			}
		}
		if (lifeBullet)
		{
			float targetDist3 = Vector2.Distance(player.Center, projectile.Center);
			if (projectile.timeLeft > 200 && targetDist3 < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + projectile.velocity, -projectile.velocity * 0.05f, affectedByGravity: false, 2, 0.85f, Color.White * 0.75f));
				for (int m = 0; m < 2; m++)
				{
					Dust dust14 = Dust.NewDustPerfect(projectile.Center + projectile.velocity, 261, -projectile.velocity * Main.rand.NextFloat(0.1f, 0.9f));
					dust14.noGravity = true;
					dust14.scale = Main.rand.NextFloat(0.65f, 0.9f);
					dust14.alpha = 100;
				}
			}
		}
		if (betterLifeBullet1)
		{
			float targetDist4 = Vector2.Distance(player.Center, projectile.Center);
			if (projectile.timeLeft > 200 && targetDist4 < 1400f)
			{
				Color color2 = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				Vector2 spawnOffset3 = Utils.RotatedBy(new Vector2((float)Math.Sin((float)projectile.timeLeft / 25f * ((float)Math.PI * 2f)) * -8f, 10f), (double)projectile.rotation, default(Vector2));
				for (int n = 0; n < 3; n++)
				{
					Dust dust15 = Dust.NewDustPerfect(projectile.Center + spawnOffset3, 278, projectile.velocity * Main.rand.NextFloat(0.05f, 0.2f));
					dust15.noGravity = true;
					dust15.scale = Main.rand.NextFloat(0.35f, 0.45f);
					dust15.color = color2;
				}
				GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + projectile.velocity, projectile.velocity * 0.05f, affectedByGravity: false, 2, 0.85f, color2));
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + Main.rand.NextVector2Circular(6f, 6f), -projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f), affectedByGravity: false, 20, 0.55f, color2 * 0.5f));
				}
			}
		}
		if (!betterLifeBullet2)
		{
			return;
		}
		float targetDist5 = Vector2.Distance(player.Center, projectile.Center);
		if (projectile.timeLeft > 200 && targetDist5 < 1400f)
		{
			Color color3 = (Color)(Main.rand.Next(1, 4) switch
			{
				2 => Color.LightPink, 
				1 => Color.LightBlue, 
				_ => Color.Khaki, 
			});
			Vector2 spawnOffset4 = Utils.RotatedBy(new Vector2((float)Math.Sin((float)projectile.timeLeft / 25f * ((float)Math.PI * 2f)) * 8f, 10f), (double)projectile.rotation, default(Vector2));
			for (int num = 0; num < 3; num++)
			{
				Dust dust16 = Dust.NewDustPerfect(projectile.Center + spawnOffset4, 278, projectile.velocity * Main.rand.NextFloat(0.05f, 0.2f));
				dust16.noGravity = true;
				dust16.scale = Main.rand.NextFloat(0.35f, 0.45f);
				dust16.color = color3;
			}
			GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + projectile.velocity, projectile.velocity * 0.05f, affectedByGravity: false, 2, 0.85f, color3));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(projectile.Center + Main.rand.NextVector2Circular(6f, 6f), -projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f), affectedByGravity: false, 20, 0.55f, color3 * 0.5f));
			}
		}
	}

	public override void PostAI(Projectile projectile)
	{
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.FinalExtraUpdate() && flatDRTimer > 0)
		{
			flatDRTimer--;
			if (flatDRTimer <= 0)
			{
				flatDR = 0;
			}
		}
		if (projectile.FinalExtraUpdate() && multiplicativeDRTimer > 0)
		{
			multiplicativeDRTimer--;
			if (multiplicativeDRTimer <= 0)
			{
				multiplicativeDR = 0f;
			}
		}
		if (projectile.FinalExtraUpdate() && TransformerTimer > 0)
		{
			TransformerTimer--;
		}
		if (projectile.aiStyle == 7 && projectile.ai[0] == 2f && Main.player[projectile.owner].Calamity().bloomStone && !hookCanSpawnFlower)
		{
			hookCanSpawnFlower = true;
			if (Main.myPlayer == projectile.owner)
			{
				Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<BloomStoneFlower>(), 0, 0f, projectile.owner, projectile.whoAmI);
			}
		}
		if (projectile.type != 1019 || projectile.owner != Main.myPlayer)
		{
			return;
		}
		int x = (int)(projectile.Center.X / 16f);
		int y = (int)(projectile.Center.Y / 16f);
		if (!WorldGen.InWorld(x, y, 3))
		{
			return;
		}
		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = y - 1; j <= y + 1; j++)
			{
				Tile tile = Main.tile[i, j];
				if (tile.TileType == ModContent.TileType<AstralTreeSapling>() || tile.TileType == ModContent.TileType<AstralSnowTreeSapling>())
				{
					bool isPlayerNear = WorldGen.PlayerLOS(i, j);
					if (WorldGen.GrowTree(i, j) & isPlayerNear)
					{
						WorldGen.TreeGrowFXCheck(i, j);
					}
				}
				else if (tile.TileType == ModContent.TileType<AstralPalmSapling>() || tile.TileType == ModContent.TileType<AcidWoodTreeSapling>())
				{
					bool isPlayerNear2 = WorldGen.PlayerLOS(i, j);
					if (WorldGen.GrowPalmTree(i, j) & isPlayerNear2)
					{
						WorldGen.TreeGrowFXCheck(i, j);
					}
				}
				else if (tile.TileType == ModContent.TileType<SpineSapling>() && WorldGen.PlayerLOS(i, j) && Main.tile[i, j + 1].TileType != ModContent.TileType<SpineSapling>())
				{
					SpineTree.Spawn(i, j, 22, 28, saplingExists: true);
				}
				NetMessage.SendTileSquare(-1, i, j, 1, 1);
			}
		}
	}

	public override void UseGrapple(Player player, ref int type)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().bloomStoneHookVisuals)
		{
			for (int i = 0; i < 8; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center, player.Center.DirectionTo(player.Calamity().mouseWorld).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(4f, 11f), "CalamityMod/Particles/MiniFlower", affectedByGravity: false, Main.rand.Next(65, 79), Main.rand.NextFloat(1.8f, 2.8f), Color.Lerp(Color.HotPink, Color.Plum, Main.rand.NextFloat(0f, 0.65f)), new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(0f, (float)Math.PI * 2f)));
				Dust dust = Dust.NewDustPerfect(player.Center, ModContent.DustType<SquashDust>());
				dust.noLightEmittence = true;
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.9f, 1.4f);
				dust.color = (Main.rand.NextBool() ? Color.Gold : Color.HotPink);
				dust.velocity = player.Center.DirectionTo(player.Calamity().mouseWorld).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(4f, 11f);
				dust.fadeIn = -0.5f;
			}
		}
	}

	public override void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
	{
		float mult = 1f;
		if (player.Calamity().reaverSpeed)
		{
			mult += ReaverHeadMobility.SetBonusHookBoost;
		}
		if (player.Calamity().tungstenArmorHookBoost)
		{
			mult += 0.5f;
		}
		if (player.Calamity().bloomStone)
		{
			mult += 0.5f;
		}
		speed *= mult;
		if (((Vector2)(ref player.velocity)).Length() > 2f)
		{
			player.Calamity().hookPullVisuals = 60;
		}
	}

	public override void GrappleRetreatSpeed(Projectile projectile, Player player, ref float speed)
	{
		float mult = 1f;
		if (player.Calamity().reaverSpeed)
		{
			mult += ReaverHeadMobility.SetBonusHookBoost;
		}
		if (player.Calamity().tungstenArmorHookBoost)
		{
			mult += 0.5f;
		}
		if (player.Calamity().bloomStone)
		{
			mult += 0.5f;
		}
		speed *= mult;
	}

	public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.oldFashioned && buffedByOldFashioned.HasValue)
		{
			modifiers.SourceDamage *= (buffedByOldFashioned.Value ? OldFashioned.DamageBoostMultiplier : OldFashioned.DamageReductionMultiplier);
		}
		if (modPlayer.ivDrip && buffedByOldFashioned.HasValue)
		{
			modifiers.SourceDamage *= (buffedByOldFashioned.Value ? IVDripOnTheRocks.DamageBoostMultiplier : IVDripOnTheRocks.DamageReductionMultiplier);
		}
		if (modPlayer.rum && projectile.DamageType.CountsAsClass(DamageClass.Summon))
		{
			if (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type])
			{
				modifiers.SourceDamage *= Rum.MinionBoost;
			}
			else
			{
				modifiers.SourceDamage *= Rum.NonMinionBoost;
			}
		}
		if ((modPlayer.moscowMule || modPlayer.bloodyMary) && (projectile.DamageType == DamageClass.Summon || PierceResistNPC.exemptProjectiles.Contains(projectile.type) || (PierceResistNPC.singleHitboxExemptProjectiles.ContainsKey(projectile.type) && PierceResistNPC.singleHitboxExemptProjectiles[projectile.type])))
		{
			modifiers.SourceDamage *= (modPlayer.moscowMule ? 0.7f : 1f) * (modPlayer.bloodyMary ? 0.33f : 1f);
		}
		if (projectile.type == 877 || projectile.type == 879 || projectile.type == 878)
		{
			float vanillaVelocityDamageMultiplier = 0.1f + ((Vector2)(ref player.velocity)).Length() / 7f * 0.9f;
			float baseVelocityDamageMultiplier = 0.01f + ((Vector2)(ref player.velocity)).Length() * 0.002f;
			float calamityVelocityDamageMultiplier = 100f * (1f - 1f / (1f + baseVelocityDamageMultiplier));
			modifiers.SourceDamage *= calamityVelocityDamageMultiplier / vanillaVelocityDamageMultiplier;
		}
		if (projectile.type == 466 && projectile.ai[2] == 1f)
		{
			if (projectile.numHits > 0)
			{
				projectile.damage = (int)((float)projectile.damage * 0.8f);
			}
			if (projectile.damage < 1)
			{
				projectile.damage = 1;
			}
		}
		if (projectile.type == 728)
		{
			if (projectile.numHits > 0)
			{
				projectile.damage = (int)((float)projectile.damage * 0.95f);
			}
			if (projectile.damage < 1)
			{
				projectile.damage = 1;
			}
		}
		if (player.wingsLogic == 32 && player.wingTime > 0f && projectile.type == 623)
		{
			modifiers.SourceDamage *= MathF.Max(1f, MathHelper.Lerp(4f, 1f, player.wingTime / (float)player.wingTimeMax));
		}
		if (totalRicoshotDamageBonus > 0f)
		{
			modifiers.ScalingBonusDamage += totalRicoshotDamageBonus;
		}
		if (forcedCrit)
		{
			modifiers.SetCrit();
		}
		if (modPlayer.rottenDogTooth && projectile.Calamity().stealthStrike && !modPlayer.vampiricTalisman)
		{
			target.AddBuff(ModContent.BuffType<Crumbling>(), 150);
		}
		else if (modPlayer.vampiricTalisman && projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 150);
			if (!modPlayer.nanotech)
			{
				target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
			}
		}
		if (modPlayer.flamingItemEnchant && !projectile.minion && !projectile.npcProj && !projectile.Calamity().CreatedByPlayerDash)
		{
			target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 120);
		}
		if (ExplosiveEnchantCountdown > 0)
		{
			modifiers.SourceDamage *= MathHelper.SmoothStep(1f, 1.6f, 1f - (float)ExplosiveEnchantCountdown / 2400f);
		}
		if (modPlayer.farProximityRewardEnchant)
		{
			float proximityDamageInterpolant = Utils.GetLerpValue(250f, 2400f, target.Distance(player.Center), clamped: true);
			float proximityDamageFactor = MathHelper.SmoothStep(0.7f, 1.45f, proximityDamageInterpolant);
			modifiers.SourceDamage *= proximityDamageFactor;
		}
		if (modPlayer.closeProximityRewardEnchant)
		{
			float proximityDamageInterpolant2 = Utils.GetLerpValue(400f, 175f, target.Distance(player.Center), clamped: true);
			float proximityDamageFactor2 = MathHelper.SmoothStep(0.75f, 1.75f, proximityDamageInterpolant2);
			modifiers.SourceDamage *= proximityDamageFactor2;
		}
		if (projectile.type == 710 && !WorldUtils.Find(projectile.Center.ToTileCoordinates(), Searches.Chain(new Searches.Down(12), new Conditions.IsSolid()), out var _))
		{
			modifiers.SourceDamage /= 1.5f;
		}
		if (deepcoreBullet && !Main.dedServ)
		{
			Vector2 cen = Vector2.Lerp(projectile.Center, target.Center, 0.65f);
			int numSparks = Main.rand.Next(2, 5);
			for (int i = 0; i < numSparks; i++)
			{
				Vector2 sparkVelocity = projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(2f, 8f);
				Color sparkColor = Color.Lerp(Color.AliceBlue, Color.DarkSlateBlue, Main.rand.NextFloat(0.7f));
				sparkColor = Color.Lerp(sparkColor, Color.CornflowerBlue, Main.rand.NextFloat());
				GeneralParticleHandler.SpawnParticle(new SparkParticle(cen, -sparkVelocity, affectedByGravity: false, 10, 0.5f, sparkColor));
			}
		}
		if (projectile.owner == Main.myPlayer && !projectile.npcProj && !projectile.trap && projectile.CountsAsClass<RogueDamageClass>() && modPlayer.scuttlersJewel && stealthStrike && modPlayer.scuttlerCooldown <= 0 && !JewelSpikeSpawned)
		{
			int damage = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(16f);
			int spike = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<JewelSpike>(), damage, projectile.knockBack, projectile.owner);
			Main.projectile[spike].frame = 4;
			if (spike.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[spike].DamageType = DamageClass.Generic;
			}
			modPlayer.scuttlerCooldown = 30;
			JewelSpikeSpawned = true;
		}
	}

	public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.FinalDamage.Flat -= flatDR;
		modifiers.FinalDamage *= 1f - multiplicativeDR;
	}

	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.usesLocalNPCImmunity && projectile.usesIDStaticNPCImmunity && (projectile.penetrate != 1 || projectile.appliesImmunityTimeOnSingleHits))
		{
			projectile.localNPCImmunity[target.whoAmI] = projectile.localNPCHitCooldown;
			Projectile.perIDStaticNPCImmunity[projectile.type][target.whoAmI] = Main.GameUpdateCount + (uint)projectile.idStaticNPCHitCooldown;
		}
		if (BloodstoneOrbValue > 0)
		{
			Projectile.NewProjectile(projectile.GetSource_OnHit(target), projectile.Center, projectile.velocity.SafeNormalize(Vector2.Zero) * Math.Min(((Vector2)(ref projectile.velocity)).Length() * (float)projectile.MaxUpdates / 4f, 4f) * Main.rand.NextFloat(0.75f, 1.25f), ModContent.ProjectileType<BloodstoneHealOrb>(), BloodstoneOrbValue, 0f, Main.player[projectile.owner].whoAmI);
		}
		if (Main.player[projectile.owner].statMana < 0 && Main.player[projectile.owner].Calamity().ChaosStone)
		{
			Player player = Main.player[projectile.owner];
			float burnRatio = (float)(-player.statMana / 100) * ChaosStone.DamageMultPer100Mana;
			target.Calamity().manaBurn += (float)damageDone * burnRatio * (player.Calamity().oldFashioned ? OldFashioned.DamageBoostMultiplier : 1f) * (player.Calamity().ivDrip ? IVDripOnTheRocks.DamageBoostMultiplier : 1f) * (player.Calamity().vodka ? (1f + Vodka.DebuffBoost) : 1f);
			target.Calamity().playerManaBurnIntensity = (float)(-player.statMana) / (float)player.statManaMax2;
			if (Main.netMode != 0)
			{
				ManaBurnSyncPacket.Send(target);
			}
		}
		if (projectile.type != ModContent.ProjectileType<HyperiusBulletProj>() && projectile.type != ModContent.ProjectileType<HyperiusSplit>() && projectile.type != ModContent.ProjectileType<HyperiusDamage>() && projectile.type != ModContent.ProjectileType<HyperiusBleed>() && target.Calamity().hyperiusMarked)
		{
			int damage = 0;
			damage = ((target.Calamity().hyperiusDamage >= damageDone) ? damageDone : (damageDone - target.Calamity().hyperiusDamage));
			target.Calamity().hyperiusDamage -= damage;
			Projectile projectile2 = Projectile.NewProjectileDirect(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<HyperiusDamage>(), (int)((float)damage * HyperiusBullet.overflowEfficency), 0f, projectile.owner, target.whoAmI);
			projectile2.DamageType = projectile.DamageType;
			projectile2.ArmorPenetration = projectile.ArmorPenetration;
			if (target.Calamity().hyperiusDamage <= 0)
			{
				target.Calamity().hyperiusDamage = 0;
				target.Calamity().hyperiusMarked = false;
			}
		}
		if (projectile.type == 477)
		{
			projectile.ResetLocalNPCHitImmunity();
			projectile.localNPCImmunity[target.whoAmI] = -1;
		}
		if (!projectile.usesIDStaticNPCImmunity || CalamityProjectileSets.SharedIDStaticIFrames[projectile.type] == -1)
		{
			return;
		}
		for (int proj = 0; proj < CalamityProjectileSets.SharedIDStaticIFrames.Length; proj++)
		{
			if (CalamityProjectileSets.SharedIDStaticIFrames[proj] == CalamityProjectileSets.SharedIDStaticIFrames[projectile.type])
			{
				Projectile.perIDStaticNPCImmunity[proj][target.whoAmI] = Main.GameUpdateCount + (uint)projectile.idStaticNPCHitCooldown;
			}
		}
	}

	public override bool? CanDamage(Projectile projectile)
	{
		if (projectile.hostile && projectile.damage - flatDR <= 0)
		{
			return false;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		switch (projectile.type)
		{
		case 961:
			if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
			{
				float fadeInTime = 25f;
				float fadeOutGateValue = (death ? 90f : 65f);
				return projectile.ai[0] >= fadeInTime && projectile.ai[0] < fadeOutGateValue;
			}
			break;
		case 962:
			if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
			{
				return projectile.ai[0] > projectile.ai[2];
			}
			break;
		case 922:
			if (death)
			{
				return projectile.ai[0] > 0f;
			}
			break;
		case 348:
			if (projectile.ai[1] > 0f)
			{
				return ((Vector2)(ref projectile.velocity)).Length() >= projectile.ai[1];
			}
			break;
		case 384:
			if (projectile.timeLeft > 480)
			{
				return false;
			}
			break;
		case 386:
			if (projectile.timeLeft > 780)
			{
				return false;
			}
			break;
		case 873:
			if (projectile.hostile)
			{
				return (float)projectile.timeLeft <= 140f;
			}
			break;
		case 872:
			if (projectile.timeLeft > 600)
			{
				return false;
			}
			break;
		}
		return null;
	}

	public override bool CanHitPlayer(Projectile projectile, Player target)
	{
		if (projectile.type == 465)
		{
			return false;
		}
		return true;
	}

	public override bool? CanHitNPC(Projectile projectile, NPC target)
	{
		if (projectile.usesLocalNPCImmunity && projectile.usesIDStaticNPCImmunity && (projectile.localNPCImmunity[target.whoAmI] != 0 || Projectile.perIDStaticNPCImmunity[projectile.type][target.whoAmI] > Main.GameUpdateCount))
		{
			return false;
		}
		if (target.Calamity().IsArmored() && HomingTarget > -1 && HomingTarget != target.whoAmI)
		{
			return false;
		}
		return null;
	}

	public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.type == 477)
		{
			projectile.ResetLocalNPCHitImmunity();
		}
		return base.OnTileCollide(projectile, oldVelocity);
	}

	public override Color? GetAlpha(Projectile projectile, Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.Calamity().trippy)
		{
			return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, Main.DiscoR);
		}
		if (Main.LocalPlayer.Calamity().omniscience && projectile.hostile && projectile.damage > 0 && projectile.alpha < 255 && (projectile.ModProjectile == null || (projectile.ModProjectile != null && projectile.ModProjectile.CanHitPlayer(Main.LocalPlayer) && (projectile.ModProjectile.CanDamage() ?? true))))
		{
			Color val = Color.Crimson;
			((Color)(ref val)).A = 0;
			Color val2 = val;
			val = Color.OrangeRed;
			((Color)(ref val)).A = 0;
			Color mainColor = Color.Lerp(val2, val, Main.GlobalTimeWrappedHourly * 2f % 1f);
			return new Color((int)Math.Max(((Color)(ref mainColor)).R, ((Color)(ref lightColor)).R), (int)Math.Max(((Color)(ref mainColor)).G, ((Color)(ref lightColor)).G), (int)Math.Max(((Color)(ref mainColor)).B, ((Color)(ref lightColor)).B));
		}
		if (projectile.type == 270 && (projectile.ai[0] == -1f || projectile.ai[0] == -3f))
		{
			float homingTime = (CalamityWorld.death ? 105f : 90f);
			if (projectile.ai[0] == -3f)
			{
				homingTime += 60f;
			}
			return (projectile.ai[1] >= homingTime) ? new Color(184, 140, 255, projectile.alpha) : new Color(255, 255, 255, (int)Utils.WrappedLerp(0f, 255f, (float)(projectile.timeLeft % 40) / 40f));
		}
		if (projectile.type == 814)
		{
			return new Color(200, 0, 0, projectile.alpha);
		}
		if (projectile.type == 55)
		{
			return new Color(200, 200, 0, projectile.alpha);
		}
		if (projectile.type == 719)
		{
			return new Color(250, 250, 0, projectile.alpha);
		}
		if (projectile.type == 926 || projectile.type == 920 || projectile.type == 921)
		{
			return new Color(255, 255, 255, projectile.alpha);
		}
		if (projectile.type == 84)
		{
			if (projectile.alpha < 200)
			{
				return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 0);
			}
			return Color.Transparent;
		}
		if (projectile.ai[1] > 0f && projectile.type == 348)
		{
			if (((Vector2)(ref projectile.velocity)).Length() < projectile.ai[1])
			{
				float minVelocity = projectile.ai[1] * 0.5f;
				byte b2 = (byte)((((Vector2)(ref projectile.velocity)).Length() - minVelocity) / minVelocity * 200f);
				byte a2 = (byte)((float)(int)b2 / 200f * 255f);
				return new Color((int)b2, (int)b2, (int)b2, (int)a2);
			}
			return new Color(200, 200, 200, projectile.alpha);
		}
		if (projectile.type == 277)
		{
			float startWarningColorGateValue = 420f;
			float timeToReachFullIntensity = 180f;
			Color initialColor = lightColor;
			Color finalColor = Color.Lerp(new Color(125, 75, 75), Color.Red, (float)Math.Abs(Math.Sin((projectile.ai[1] - startWarningColorGateValue) * ((float)Math.PI / 45f))));
			((Color)(ref finalColor)).A = (byte)(255 - projectile.alpha);
			if (projectile.ai[1] > startWarningColorGateValue)
			{
				float colorTransitionRatio = (projectile.ai[1] - startWarningColorGateValue) / timeToReachFullIntensity;
				return Color.Lerp(initialColor, finalColor, colorTransitionRatio);
			}
			return initialColor;
		}
		if (projectile.type == 275 || projectile.type == 276 || projectile.type == 468 || projectile.type == 593)
		{
			if (projectile.timeLeft < 85)
			{
				byte b3 = (byte)(projectile.timeLeft * 3);
				byte a3 = (byte)((float)projectile.alpha * ((float)(int)b3 / 255f));
				return new Color((int)b3, (int)b3, (int)b3, (int)a3);
			}
			return new Color(255, 255, 255, projectile.alpha);
		}
		return null;
	}

	public override bool PreDraw(Projectile projectile, ref Color lightColor)
	{
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		bool shouldDrawBool = true;
		if (projectile.type == 317)
		{
			RavenMinionAI.DoRavenMinionDrawing(projectile, ref lightColor);
			shouldDrawBool = false;
		}
		if (projectile.type == 961 && (CalamityWorld.revenge || BossRushEvent.BossRushActive))
		{
			bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
			Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
			Rectangle value26 = texture.Frame(1, 5, 0, projectile.frame);
			Vector2 origin12 = default(Vector2);
			((Vector2)(ref origin12))._002Ector(16f, (float)(value26.Height / 2));
			Color alpha5 = projectile.GetAlpha(lightColor);
			Vector2 vector39 = default(Vector2);
			((Vector2)(ref vector39))._002Ector(projectile.scale);
			float killGateValue = (num ? 100f : 75f);
			float lerpValue5 = Utils.GetLerpValue(killGateValue, killGateValue - 50f, projectile.ai[0], clamped: true);
			vector39.Y *= lerpValue5;
			Vector4 vector40 = ((Color)(ref lightColor)).ToVector4();
			Color val = new Color(67, 17, 17);
			Vector4 vector41 = ((Color)(ref val)).ToVector4();
			vector41 *= vector40;
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Main.EntitySpriteDraw(TextureAssets.Extra[98].Value, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) - projectile.velocity * projectile.scale * 0.5f, null, projectile.GetAlpha(new Color(vector41.X, vector41.Y, vector41.Z, vector41.W)), projectile.rotation + (float)Math.PI / 2f, TextureAssets.Extra[98].Value.Size() / 2f, projectile.scale * 0.9f, spriteEffects);
			Color color49 = projectile.GetAlpha(Color.White) * Utils.Remap(projectile.ai[0], 0f, killGateValue, 0.5f, 0f);
			((Color)(ref color49)).A = 0;
			for (int i = 0; i < 4; i++)
			{
				Main.EntitySpriteDraw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) + projectile.rotation.ToRotationVector2().RotatedBy((float)Math.PI / 2f * (float)i) * 2f * vector39, value26, color49, projectile.rotation, origin12, vector39, spriteEffects);
			}
			Main.EntitySpriteDraw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), value26, alpha5, projectile.rotation, origin12, vector39, spriteEffects);
			shouldDrawBool = false;
		}
		if (projectile.type == 270 && projectile.ai[0] == -2f)
		{
			Main.instance.LoadProjectile(532);
			Texture2D crossbone = TextureAssets.Projectile[532].Value;
			Main.spriteBatch.Draw(crossbone, projectile.Center - Main.screenPosition, (Rectangle?)null, projectile.GetAlpha(lightColor), projectile.rotation, crossbone.Size() / 2f, projectile.scale, (SpriteEffects)(projectile.spriteDirection == -1), 0f);
			return false;
		}
		if (projectile.type == 44 && Main.wofNPCIndex >= 0 && Main.npc[Main.wofNPCIndex].active && Main.npc[Main.wofNPCIndex].life > 0 && !projectile.tileCollide)
		{
			Texture2D texture2 = ExtraTextureRefs.WallOfFleshDemonSickleTexture.Value;
			int frameHeight = texture2.Height / Main.projFrames[projectile.type];
			int frameY = frameHeight * projectile.frame;
			Rectangle rectangle = default(Rectangle);
			((Rectangle)(ref rectangle))._002Ector(0, frameY, texture2.Width, frameHeight);
			Vector2 origin13 = rectangle.Size() / 2f;
			SpriteEffects spriteEffects2 = (SpriteEffects)0;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects2 = (SpriteEffects)1;
			}
			Main.spriteBatch.Draw(texture2, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), (Rectangle?)rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin13, projectile.scale, spriteEffects2, 0f);
			return false;
		}
		if (projectile.type == 735 || projectile.type == 595)
		{
			Texture2D tex = TextureAssets.Projectile[projectile.type].Value;
			Rectangle frame = tex.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
			Vector2 origin14 = frame.Size() / 2f;
			SpriteEffects spriteEffects3 = (SpriteEffects)(projectile.spriteDirection == -1);
			Main.spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), (Rectangle?)frame, projectile.GetAlpha(lightColor), projectile.rotation, origin14, projectile.scale, spriteEffects3, 0f);
			shouldDrawBool = false;
		}
		if (Main.zenithWorld && NPC.AnyNPCs(ModContent.NPCType<CeaselessVoid>()))
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			GameShaders.Armor.GetShaderFromItemId(3556).Apply();
		}
		return shouldDrawBool;
	}

	public override bool PreKill(Projectile projectile, int timeLeft)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		bool revQueenBeeBeeHive = projectile.type == 655 && (CalamityWorld.revenge || BossRushEvent.BossRushActive) && (projectile.ai[2] == 1f || CalamityWorld.death);
		if (revQueenBeeBeeHive)
		{
			SoundEngine.PlaySound(in SoundID.NPCDeath1, projectile.Center);
			for (int num573 = 0; num573 < 30; num573++)
			{
				int num574 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 147);
				if (Main.rand.NextBool())
				{
					Main.dust[num574].scale *= 1.4f;
				}
				projectile.velocity *= 1.9f;
			}
		}
		if (projectile.owner == Main.myPlayer && revQueenBeeBeeHive)
		{
			if (Main.netMode != 1)
			{
				int beeAmt = Main.rand.Next(2, 6);
				int totalBeesAlive = NPC.CountNPCS(210) + NPC.CountNPCS(211);
				int finalBeeAmtToSpawn = (NPC.AnyNPCs(222) ? Math.Min(beeAmt, (CalamityWorld.death ? 9 : 15) - totalBeesAlive) : NPC.GetAvailableAmountOfNPCsToSpawnUpToSlot(beeAmt));
				for (int i = 0; i < finalBeeAmtToSpawn; i++)
				{
					int beeType = (Main.rand.NextBool() ? 210 : 211);
					if (Main.zenithWorld)
					{
						beeType = (Main.rand.NextBool(3) ? ModContent.NPCType<PlagueChargerLarge>() : ModContent.NPCType<PlagueCharger>());
					}
					else if (CalamityWorld.death || BossRushEvent.BossRushActive)
					{
						switch (Main.rand.Next(12))
						{
						case 6:
						case 7:
						case 8:
							beeType = -58;
							break;
						case 9:
						case 10:
							beeType = 232;
							break;
						case 11:
							beeType = -59;
							break;
						}
					}
					int beeSpawn = NPC.NewNPC(projectile.GetSource_FromThis(), (int)projectile.Center.X, (int)projectile.Center.Y, beeType, 1);
					Main.npc[beeSpawn].velocity.X = (float)Main.rand.Next(-200, 201) * 0.002f;
					Main.npc[beeSpawn].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.002f;
					Main.npc[beeSpawn].ai[3] = 1f;
					Main.npc[beeSpawn].timeLeft = 600;
					Main.npc[beeSpawn].netUpdate = true;
				}
			}
			if (Main.netMode != 0)
			{
				NetMessage.SendData(29, -1, -1, null, projectile.identity, projectile.owner);
			}
		}
		if (revQueenBeeBeeHive)
		{
			projectile.active = false;
			return false;
		}
		return true;
	}

	public override void OnKill(Projectile projectile, int timeLeft)
	{
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (projectile.owner != Main.myPlayer || projectile.npcProj || projectile.trap || !projectile.CountsAsClass<RogueDamageClass>())
		{
			return;
		}
		if (modPlayer.etherealExtorter && extorterBoost && Main.player[projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<LostSoulFriendly>()] < 5)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				int damage = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(20f);
				int soul = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, velocity, ModContent.ProjectileType<LostSoulFriendly>(), damage, 0f, projectile.owner);
				Main.projectile[soul].tileCollide = false;
				if (soul.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[soul].DamageType = DamageClass.Generic;
				}
			}
		}
		if (modPlayer.scuttlersJewel && stealthStrike && modPlayer.scuttlerCooldown <= 0 && !JewelSpikeSpawned)
		{
			int damage2 = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(16f);
			int spike = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<JewelSpike>(), damage2, projectile.knockBack, projectile.owner);
			Main.projectile[spike].frame = 4;
			if (spike.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[spike].DamageType = DamageClass.Generic;
			}
			modPlayer.scuttlerCooldown = 30;
		}
	}

	internal static void LoadTweaks()
	{
		IProjectileTweak[] defenseDamage = Do(DefenseDamage);
		IProjectileTweak[] trueMelee = Do(TrueMelee, DefaultIDStaticIFrames);
		IProjectileTweak[] trueMeleeNoSpeed = Do(TrueMeleeNoSpeed, DefaultIDStaticIFrames);
		IProjectileTweak[] defaultIFrames = Do(DefaultIDStaticIFrames);
		IProjectileTweak[] standardBulletTweaks = Do(ExtraUpdatesDelta(2));
		IProjectileTweak[] standardChainsawTweaks = Do(TrueMeleeNoSpeed, ArmorPenetrationDelta(15), LocalIFrames(5));
		IProjectileTweak[] standardDrillTweaks = Do(TrueMeleeNoSpeed, ArmorPenetrationDelta(25), LocalIFrames(5));
		IProjectileTweak[] counterweightTweaks = Do(MaxUpdatesExact(2), DefaultIDStaticIFrames);
		SortedDictionary<int, IProjectileTweak[]> sortedDictionary = new SortedDictionary<int, IProjectileTweak[]>();
		sortedDictionary.Add(552, RebalanceYoyo(-1f, 432f, 28f, 1, 12));
		sortedDictionary.Add(545, RebalanceYoyo(30f, 384f, 28f, 1, 15));
		sortedDictionary.Add(546, RebalanceYoyo(-1f, 400f, 32f, 1, 12));
		sortedDictionary.Add(534, RebalanceYoyo(21f, 320f, 25f, 1, 15));
		sortedDictionary.Add(547, RebalanceYoyo(-1f, 432f, 42f, 1, 12));
		sortedDictionary.Add(542, RebalanceYoyo(18f, 288f, 22f, 0, 20));
		sortedDictionary.Add(543, RebalanceYoyo(18f, 288f, 22f, 0, 20));
		sortedDictionary.Add(562, RebalanceYoyo(-1f, 384f, 36f, 1, 12));
		sortedDictionary.Add(563, RebalanceYoyo(-1f, 384f, 36f, 1, 12));
		sortedDictionary.Add(553, RebalanceYoyo(-1f, 352f, 42f, 2, 12));
		sortedDictionary.Add(999, RebalanceYoyo(24f, 320f, 20f, 0, 15));
		sortedDictionary.Add(544, RebalanceYoyo(20f, 288f, 17f, 0, 20));
		sortedDictionary.Add(554, RebalanceYoyo(-1f, 480f, 54f, 2));
		sortedDictionary.Add(548, RebalanceYoyo(16f, 272f, 20f, 0, 20));
		sortedDictionary.Add(550, RebalanceYoyo(-1f, 480f, 42f, 2, 12));
		sortedDictionary.Add(603, RebalanceYoyo(-1f, 512f, 54f, 2));
		sortedDictionary.Add(604, Do(LocalIFrames(-1)));
		sortedDictionary.Add(555, RebalanceYoyo(-1f, 480f, 36f, 1, 12));
		sortedDictionary.Add(551, RebalanceYoyo(-1f, 480f, 42f, 2, 12));
		sortedDictionary.Add(564, RebalanceYoyo(30f, 400f, 36f, 1, 15));
		sortedDictionary.Add(541, RebalanceYoyo(15f, 240f, 14f, 0, 20));
		sortedDictionary.Add(549, RebalanceYoyo(-1f, 400f, 36f, 1, 12));
		sortedDictionary.Add(61, standardChainsawTweaks);
		sortedDictionary.Add(62, standardDrillTweaks);
		sortedDictionary.Add(66, Do(TrueMelee, LocalIFrames(7)));
		sortedDictionary.Add(383, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(595, Do(TrueMeleeNoSpeed, ScaleExact(1.25f), IDStaticIFrames(5)));
		sortedDictionary.Add(181, Do(PiercingExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(469, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(556, counterweightTweaks);
		sortedDictionary.Add(557, counterweightTweaks);
		sortedDictionary.Add(26, Do(ExtraUpdatesExact(1)));
		sortedDictionary.Add(14, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(242, Do(ExtraUpdatesDelta(2), LocalIFrames(-1)));
		sortedDictionary.Add(509, Do(TrueMeleeNoSpeed, ArmorPenetrationDelta(15), LocalIFrames(7), ScaleExact(1.5f)));
		sortedDictionary.Add(224, standardChainsawTweaks);
		sortedDictionary.Add(223, standardDrillTweaks);
		sortedDictionary.Add(229, Do(NoPiercing));
		sortedDictionary.Add(57, standardChainsawTweaks);
		sortedDictionary.Add(59, standardDrillTweaks);
		sortedDictionary.Add(97, Do(TrueMelee, LocalIFrames(9)));
		sortedDictionary.Add(89, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(104, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(776, Do(LocalIFrames(15)));
		sortedDictionary.Add(779, Do(IDStaticIFrames(15)));
		sortedDictionary.Add(780, Do(LocalIFrames(15)));
		sortedDictionary.Add(783, Do(IDStaticIFrames(15)));
		sortedDictionary.Add(803, Do(LocalIFrames(15)));
		sortedDictionary.Add(804, Do(LocalIFrames(15)));
		sortedDictionary.Add(392, Do(ExtraUpdatesExact(2), LocalIFrames(90)));
		sortedDictionary.Add(684, Do(PiercingExact(3), DefaultIDStaticIFrames));
		sortedDictionary.Add(533, Do(LocalIFrames(30)));
		sortedDictionary.Add(124, Do(NoPiercing));
		sortedDictionary.Add(6, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(286, Do(ExtraUpdatesDelta(2), IDStaticIFrames(5)));
		sortedDictionary.Add(932, Do(PiercingExact(7), ExtraUpdatesExact(1)));
		sortedDictionary.Add(405, Do(ExtraUpdatesExact(1), TimeLeftExact(150), DefaultIDStaticIFrames));
		sortedDictionary.Add(19, Do(ExtraUpdatesExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(321, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(221, Do(MaxUpdatesExact(4), LocalIFrames(10)));
		sortedDictionary.Add(491, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(359, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(333, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(566, Do(PiercingExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(802, Do(TrueMelee, LocalIFrames(-1)));
		sortedDictionary.Add(287, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(280, Do(PiercingExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(558, counterweightTweaks);
		sortedDictionary.Add(107, standardDrillTweaks);
		sortedDictionary.Add(41, Do(ExtraUpdatesDelta(2)));
		sortedDictionary.Add(113, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(263, Do(ExtraUpdatesExact(1)));
		sortedDictionary.Add(279, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(451, Do(ExtraUpdatesExact(1)));
		sortedDictionary.Add(295, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(296, Do(ExtraUpdatesExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(391, Do(ExtraUpdatesExact(2), LocalIFrames(90)));
		sortedDictionary.Add(445, Do(ArmorPenetrationDelta(25), LocalIFrames(5)));
		sortedDictionary.Add(106, Do(MaxUpdatesExact(3), DefaultIDStaticIFrames));
		sortedDictionary.Add(293, Do(TileCollide));
		sortedDictionary.Add(36, standardBulletTweaks);
		sortedDictionary.Add(502, Do(PiercingExact(3), LocalIFrames(-1)));
		sortedDictionary.Add(697, Do(TrueMeleeNoSpeed, ScaleExact(3f)));
		sortedDictionary.Add(699, Do(TrueMelee, IDStaticIFrames(18)));
		sortedDictionary.Add(707, Do(ScaleRatio(2f)));
		sortedDictionary.Add(638, standardBulletTweaks);
		sortedDictionary.Add(58, standardChainsawTweaks);
		sortedDictionary.Add(60, standardDrillTweaks);
		sortedDictionary.Add(64, Do(TrueMelee, LocalIFrames(8)));
		sortedDictionary.Add(285, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(430, standardDrillTweaks);
		sortedDictionary.Add(576, Do(ExtraUpdatesDelta(-1)));
		sortedDictionary.Add(217, standardChainsawTweaks);
		sortedDictionary.Add(216, standardDrillTweaks);
		sortedDictionary.Add(214, standardChainsawTweaks);
		sortedDictionary.Add(213, standardDrillTweaks);
		sortedDictionary.Add(284, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(559, counterweightTweaks);
		sortedDictionary.Add(926, Do(NoPiercing));
		sortedDictionary.Add(921, Do(NoPiercing));
		sortedDictionary.Add(560, counterweightTweaks);
		sortedDictionary.Add(169, Do(TimeLeftDelta(45)));
		sortedDictionary.Add(168, Do(TimeLeftDelta(45)));
		sortedDictionary.Add(167, Do(TimeLeftDelta(45)));
		sortedDictionary.Add(170, Do(TimeLeftDelta(45)));
		sortedDictionary.Add(369, Do(TrueMeleeNoSpeed, ArmorPenetrationDelta(15), LocalIFrames(6)));
		sortedDictionary.Add(867, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(432, standardDrillTweaks);
		sortedDictionary.Add(609, standardDrillTweaks);
		sortedDictionary.Add(9, Do(TimeLeftExact(75), DefaultIDStaticIFrames));
		sortedDictionary.Add(503, Do(NoPiercing));
		sortedDictionary.Add(35, Do(ExtraUpdatesExact(1)));
		sortedDictionary.Add(116, Do(ExtraUpdatesExact(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(735, Do(TrueMeleeNoSpeed, ScaleExact(1.25f), IDStaticIFrames(5)));
		sortedDictionary.Add(731, Do(PiercingExact(3), DefaultIDStaticIFrames));
		sortedDictionary.Add(220, standardChainsawTweaks);
		sortedDictionary.Add(219, standardDrillTweaks);
		sortedDictionary.Add(1000, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(973, Do(PiercingExact(4)));
		sortedDictionary.Add(283, Do(ExtraUpdatesDelta(2), DefaultIDStaticIFrames));
		sortedDictionary.Add(390, Do(ExtraUpdatesExact(2), LocalIFrames(90)));
		sortedDictionary.Add(428, standardDrillTweaks);
		sortedDictionary.Add(189, Do(PiercingExact(2)));
		sortedDictionary.Add(969, Do(ExtraUpdatesExact(3), TimeLeftExact(1920)));
		sortedDictionary.Add(561, counterweightTweaks);
		sortedDictionary.Add(252, trueMeleeNoSpeed);
		sortedDictionary.Add(938, trueMelee);
		sortedDictionary.Add(46, trueMelee);
		sortedDictionary.Add(944, trueMelee);
		sortedDictionary.Add(105, trueMelee);
		sortedDictionary.Add(879, trueMelee);
		sortedDictionary.Add(940, trueMelee);
		sortedDictionary.Add(877, trueMelee);
		sortedDictionary.Add(941, trueMelee);
		sortedDictionary.Add(130, trueMelee);
		sortedDictionary.Add(429, trueMeleeNoSpeed);
		sortedDictionary.Add(367, trueMelee);
		sortedDictionary.Add(215, trueMelee);
		sortedDictionary.Add(212, trueMelee);
		sortedDictionary.Add(927, Do(TrueMelee, IDStaticIFrames(4)));
		sortedDictionary.Add(945, trueMelee);
		sortedDictionary.Add(842, trueMelee);
		sortedDictionary.Add(878, trueMelee);
		sortedDictionary.Add(942, trueMelee);
		sortedDictionary.Add(431, trueMeleeNoSpeed);
		sortedDictionary.Add(49, trueMelee);
		sortedDictionary.Add(610, trueMeleeNoSpeed);
		sortedDictionary.Add(368, trueMelee);
		sortedDictionary.Add(153, trueMelee);
		sortedDictionary.Add(939, trueMelee);
		sortedDictionary.Add(218, trueMelee);
		sortedDictionary.Add(47, trueMelee);
		sortedDictionary.Add(943, trueMelee);
		sortedDictionary.Add(427, trueMeleeNoSpeed);
		sortedDictionary.Add(386, defenseDamage);
		sortedDictionary.Add(687, defenseDamage);
		sortedDictionary.Add(961, defenseDamage);
		sortedDictionary.Add(923, defenseDamage);
		sortedDictionary.Add(329, defenseDamage);
		sortedDictionary.Add(292, defenseDamage);
		sortedDictionary.Add(300, defenseDamage);
		sortedDictionary.Add(455, defenseDamage);
		sortedDictionary.Add(454, defenseDamage);
		sortedDictionary.Add(447, defenseDamage);
		sortedDictionary.Add(384, defenseDamage);
		sortedDictionary.Add(277, Do(Main.zenithWorld ? IgnoreWater : DontIgnoreWater, DefenseDamage));
		sortedDictionary.Add(970, defaultIFrames);
		sortedDictionary.Add(669, defaultIFrames);
		sortedDictionary.Add(597, defaultIFrames);
		sortedDictionary.Add(121, defaultIFrames);
		sortedDictionary.Add(40, defaultIFrames);
		sortedDictionary.Add(15, defaultIFrames);
		sortedDictionary.Add(253, defaultIFrames);
		sortedDictionary.Add(272, defaultIFrames);
		sortedDictionary.Add(316, defaultIFrames);
		sortedDictionary.Add(655, defaultIFrames);
		sortedDictionary.Add(183, defaultIFrames);
		sortedDictionary.Add(319, defaultIFrames);
		sortedDictionary.Add(337, defaultIFrames);
		sortedDictionary.Add(819, defaultIFrames);
		sortedDictionary.Add(975, defaultIFrames);
		sortedDictionary.Add(813, defaultIFrames);
		sortedDictionary.Add(621, defaultIFrames);
		sortedDictionary.Add(320, defaultIFrames);
		sortedDictionary.Add(310, defaultIFrames);
		sortedDictionary.Add(28, defaultIFrames);
		sortedDictionary.Add(519, defaultIFrames);
		sortedDictionary.Add(21, defaultIFrames);
		sortedDictionary.Add(117, defaultIFrames);
		sortedDictionary.Add(474, defaultIFrames);
		sortedDictionary.Add(599, defaultIFrames);
		sortedDictionary.Add(837, defaultIFrames);
		sortedDictionary.Add(712, defaultIFrames);
		sortedDictionary.Add(99, defaultIFrames);
		sortedDictionary.Add(261, defaultIFrames);
		sortedDictionary.Add(516, defaultIFrames);
		sortedDictionary.Add(1013, defaultIFrames);
		sortedDictionary.Add(637, defaultIFrames);
		sortedDictionary.Add(517, defaultIFrames);
		sortedDictionary.Add(271, defaultIFrames);
		sortedDictionary.Add(410, defaultIFrames);
		sortedDictionary.Add(111, defaultIFrames);
		sortedDictionary.Add(311, Do(IDStaticIFrames(7)));
		sortedDictionary.Add(162, defaultIFrames);
		sortedDictionary.Add(1004, defaultIFrames);
		sortedDictionary.Add(715, defaultIFrames);
		sortedDictionary.Add(716, defaultIFrames);
		sortedDictionary.Add(718, defaultIFrames);
		sortedDictionary.Add(717, defaultIFrames);
		sortedDictionary.Add(714, defaultIFrames);
		sortedDictionary.Add(481, defaultIFrames);
		sortedDictionary.Add(273, defaultIFrames);
		sortedDictionary.Add(460, defaultIFrames);
		sortedDictionary.Add(461, defaultIFrames);
		sortedDictionary.Add(459, defaultIFrames);
		sortedDictionary.Add(225, defaultIFrames);
		sortedDictionary.Add(207, defaultIFrames);
		sortedDictionary.Add(222, defaultIFrames);
		sortedDictionary.Add(585, defaultIFrames);
		sortedDictionary.Add(778, defaultIFrames);
		sortedDictionary.Add(782, defaultIFrames);
		sortedDictionary.Add(862, defaultIFrames);
		sortedDictionary.Add(863, defaultIFrames);
		sortedDictionary.Add(518, defaultIFrames);
		sortedDictionary.Add(158, defaultIFrames);
		sortedDictionary.Add(147, defaultIFrames);
		sortedDictionary.Add(241, defaultIFrames);
		sortedDictionary.Add(354, defaultIFrames);
		sortedDictionary.Add(500, defaultIFrames);
		sortedDictionary.Add(149, defaultIFrames);
		sortedDictionary.Add(477, Do(ExtraUpdatesExact(2), LocalIFrames(-1)));
		sortedDictionary.Add(226, defaultIFrames);
		sortedDictionary.Add(227, defaultIFrames);
		sortedDictionary.Add(521, defaultIFrames);
		sortedDictionary.Add(522, defaultIFrames);
		sortedDictionary.Add(90, defaultIFrames);
		sortedDictionary.Add(94, defaultIFrames);
		sortedDictionary.Add(493, defaultIFrames);
		sortedDictionary.Add(494, defaultIFrames);
		sortedDictionary.Add(103, defaultIFrames);
		sortedDictionary.Add(478, Do(ExtraUpdatesExact(1), LocalIFrames(20)));
		sortedDictionary.Add(480, defaultIFrames);
		sortedDictionary.Add(95, defaultIFrames);
		sortedDictionary.Add(1009, defaultIFrames);
		sortedDictionary.Add(705, defaultIFrames);
		sortedDictionary.Add(45, defaultIFrames);
		sortedDictionary.Add(126, defaultIFrames);
		sortedDictionary.Add(17, defaultIFrames);
		sortedDictionary.Add(910, defaultIFrames);
		sortedDictionary.Add(1017, defaultIFrames);
		sortedDictionary.Add(911, defaultIFrames);
		sortedDictionary.Add(928, defaultIFrames);
		sortedDictionary.Add(586, defaultIFrames);
		sortedDictionary.Add(906, defaultIFrames);
		sortedDictionary.Add(801, defaultIFrames);
		sortedDictionary.Add(799, defaultIFrames);
		sortedDictionary.Add(810, defaultIFrames);
		sortedDictionary.Add(29, defaultIFrames);
		sortedDictionary.Add(306, defaultIFrames);
		sortedDictionary.Add(56, defaultIFrames);
		sortedDictionary.Add(65, defaultIFrames);
		sortedDictionary.Add(77, defaultIFrames);
		sortedDictionary.Add(443, Do(IDStaticIFrames(8)));
		sortedDictionary.Add(442, defaultIFrames);
		sortedDictionary.Add(173, defaultIFrames);
		sortedDictionary.Add(108, defaultIFrames);
		sortedDictionary.Add(12, defaultIFrames);
		sortedDictionary.Add(2, defaultIFrames);
		sortedDictionary.Add(404, defaultIFrames);
		sortedDictionary.Add(188, defaultIFrames);
		sortedDictionary.Add(163, defaultIFrames);
		sortedDictionary.Add(248, defaultIFrames);
		sortedDictionary.Add(120, defaultIFrames);
		sortedDictionary.Add(172, defaultIFrames);
		sortedDictionary.Add(520, defaultIFrames);
		sortedDictionary.Add(1007, defaultIFrames);
		sortedDictionary.Add(936, defaultIFrames);
		sortedDictionary.Add(772, defaultIFrames);
		sortedDictionary.Add(654, defaultIFrames);
		sortedDictionary.Add(160, defaultIFrames);
		sortedDictionary.Add(262, defaultIFrames);
		sortedDictionary.Add(20, defaultIFrames);
		sortedDictionary.Add(30, defaultIFrames);
		sortedDictionary.Add(146, defaultIFrames);
		sortedDictionary.Add(92, defaultIFrames);
		sortedDictionary.Add(485, defaultIFrames);
		sortedDictionary.Add(91, defaultIFrames);
		sortedDictionary.Add(260, Do(DefaultIDStaticIFrames));
		sortedDictionary.Add(69, defaultIFrames);
		sortedDictionary.Add(905, defaultIFrames);
		sortedDictionary.Add(791, defaultIFrames);
		sortedDictionary.Add(792, defaultIFrames);
		sortedDictionary.Add(790, defaultIFrames);
		sortedDictionary.Add(807, defaultIFrames);
		sortedDictionary.Add(374, defaultIFrames);
		sortedDictionary.Add(967, defaultIFrames);
		sortedDictionary.Add(80, defaultIFrames);
		sortedDictionary.Add(118, defaultIFrames);
		sortedDictionary.Add(278, defaultIFrames);
		sortedDictionary.Add(479, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(312, defaultIFrames);
		sortedDictionary.Add(507, defaultIFrames);
		sortedDictionary.Add(5, defaultIFrames);
		sortedDictionary.Add(164, defaultIFrames);
		sortedDictionary.Add(439, defaultIFrames);
		sortedDictionary.Add(440, defaultIFrames);
		sortedDictionary.Add(633, defaultIFrames);
		sortedDictionary.Add(632, Do(IDStaticIFrames(5)));
		sortedDictionary.Add(904, defaultIFrames);
		sortedDictionary.Add(789, defaultIFrames);
		sortedDictionary.Add(787, defaultIFrames);
		sortedDictionary.Add(806, defaultIFrames);
		sortedDictionary.Add(206, defaultIFrames);
		sortedDictionary.Add(1014, defaultIFrames);
		sortedDictionary.Add(16, Do(IDStaticIFrames(8), SingleHitImmunity));
		sortedDictionary.Add(255, defaultIFrames);
		sortedDictionary.Add(535, defaultIFrames);
		sortedDictionary.Add(536, defaultIFrames);
		sortedDictionary.Add(424, Do(IDStaticIFrames(5), SingleHitImmunity));
		sortedDictionary.Add(425, Do(IDStaticIFrames(5), SingleHitImmunity));
		sortedDictionary.Add(426, Do(IDStaticIFrames(5), SingleHitImmunity));
		sortedDictionary.Add(591, defaultIFrames);
		sortedDictionary.Add(1005, defaultIFrames);
		sortedDictionary.Add(398, defaultIFrames);
		sortedDictionary.Add(795, defaultIFrames);
		sortedDictionary.Add(798, defaultIFrames);
		sortedDictionary.Add(793, defaultIFrames);
		sortedDictionary.Add(796, defaultIFrames);
		sortedDictionary.Add(808, defaultIFrames);
		sortedDictionary.Add(809, defaultIFrames);
		sortedDictionary.Add(389, defaultIFrames);
		sortedDictionary.Add(408, defaultIFrames);
		sortedDictionary.Add(399, defaultIFrames);
		sortedDictionary.Add(400, defaultIFrames);
		sortedDictionary.Add(401, defaultIFrames);
		sortedDictionary.Add(402, defaultIFrames);
		sortedDictionary.Add(709, defaultIFrames);
		sortedDictionary.Add(39, defaultIFrames);
		sortedDictionary.Add(131, defaultIFrames);
		sortedDictionary.Add(148, defaultIFrames);
		sortedDictionary.Add(514, Do(IDStaticIFrames(1)));
		sortedDictionary.Add(619, defaultIFrames);
		sortedDictionary.Add(620, defaultIFrames);
		sortedDictionary.Add(634, Do(IDStaticIFrames(5), SingleHitImmunity));
		sortedDictionary.Add(635, Do(IDStaticIFrames(5), SingleHitImmunity));
		sortedDictionary.Add(152, defaultIFrames);
		sortedDictionary.Add(151, defaultIFrames);
		sortedDictionary.Add(150, defaultIFrames);
		sortedDictionary.Add(157, defaultIFrames);
		sortedDictionary.Add(344, defaultIFrames);
		sortedDictionary.Add(343, defaultIFrames);
		sortedDictionary.Add(342, defaultIFrames);
		sortedDictionary.Add(584, defaultIFrames);
		sortedDictionary.Add(583, defaultIFrames);
		sortedDictionary.Add(335, defaultIFrames);
		sortedDictionary.Add(907, defaultIFrames);
		sortedDictionary.Add(587, defaultIFrames);
		sortedDictionary.Add(301, defaultIFrames);
		sortedDictionary.Add(761, defaultIFrames);
		sortedDictionary.Add(762, defaultIFrames);
		sortedDictionary.Add(588, defaultIFrames);
		sortedDictionary.Add(67, defaultIFrames);
		sortedDictionary.Add(68, defaultIFrames);
		sortedDictionary.Add(968, defaultIFrames);
		sortedDictionary.Add(630, defaultIFrames);
		sortedDictionary.Add(631, defaultIFrames);
		sortedDictionary.Add(336, defaultIFrames);
		sortedDictionary.Add(161, defaultIFrames);
		sortedDictionary.Add(98, Do(ExtraUpdatesExact(1), DefaultIDStaticIFrames));
		sortedDictionary.Add(267, defaultIFrames);
		sortedDictionary.Add(184, defaultIFrames);
		sortedDictionary.Add(54, defaultIFrames);
		sortedDictionary.Add(182, defaultIFrames);
		sortedDictionary.Add(950, defaultIFrames);
		sortedDictionary.Add(135, defaultIFrames);
		sortedDictionary.Add(138, defaultIFrames);
		sortedDictionary.Add(141, defaultIFrames);
		sortedDictionary.Add(144, defaultIFrames);
		sortedDictionary.Add(357, defaultIFrames);
		sortedDictionary.Add(145, defaultIFrames);
		sortedDictionary.Add(10, defaultIFrames);
		sortedDictionary.Add(88, defaultIFrames);
		sortedDictionary.Add(195, defaultIFrames);
		sortedDictionary.Add(76, defaultIFrames);
		sortedDictionary.Add(1010, defaultIFrames);
		sortedDictionary.Add(417, defaultIFrames);
		sortedDictionary.Add(416, defaultIFrames);
		sortedDictionary.Add(415, defaultIFrames);
		sortedDictionary.Add(418, defaultIFrames);
		sortedDictionary.Add(134, defaultIFrames);
		sortedDictionary.Add(137, defaultIFrames);
		sortedDictionary.Add(140, defaultIFrames);
		sortedDictionary.Add(143, defaultIFrames);
		sortedDictionary.Add(338, defaultIFrames);
		sortedDictionary.Add(339, defaultIFrames);
		sortedDictionary.Add(340, defaultIFrames);
		sortedDictionary.Add(341, defaultIFrames);
		sortedDictionary.Add(727, defaultIFrames);
		sortedDictionary.Add(763, defaultIFrames);
		sortedDictionary.Add(318, defaultIFrames);
		sortedDictionary.Add(125, defaultIFrames);
		sortedDictionary.Add(31, defaultIFrames);
		sortedDictionary.Add(42, defaultIFrames);
		sortedDictionary.Add(1015, defaultIFrames);
		sortedDictionary.Add(589, defaultIFrames);
		sortedDictionary.Add(930, defaultIFrames);
		sortedDictionary.Add(123, defaultIFrames);
		sortedDictionary.Add(773, defaultIFrames);
		sortedDictionary.Add(606, defaultIFrames);
		sortedDictionary.Add(434, defaultIFrames);
		sortedDictionary.Add(51, defaultIFrames);
		sortedDictionary.Add(483, defaultIFrames);
		sortedDictionary.Add(484, defaultIFrames);
		sortedDictionary.Add(294, defaultIFrames);
		sortedDictionary.Add(495, defaultIFrames);
		sortedDictionary.Add(497, defaultIFrames);
		sortedDictionary.Add(812, defaultIFrames);
		sortedDictionary.Add(1006, defaultIFrames);
		sortedDictionary.Add(1011, defaultIFrames);
		sortedDictionary.Add(3, defaultIFrames);
		sortedDictionary.Add(71, defaultIFrames);
		sortedDictionary.Add(981, defaultIFrames);
		sortedDictionary.Add(159, defaultIFrames);
		sortedDictionary.Add(660, defaultIFrames);
		sortedDictionary.Add(179, defaultIFrames);
		sortedDictionary.Add(166, defaultIFrames);
		sortedDictionary.Add(1016, defaultIFrames);
		sortedDictionary.Add(608, defaultIFrames);
		sortedDictionary.Add(607, defaultIFrames);
		sortedDictionary.Add(476, defaultIFrames);
		sortedDictionary.Add(186, defaultIFrames);
		sortedDictionary.Add(1008, defaultIFrames);
		sortedDictionary.Add(313, defaultIFrames);
		sortedDictionary.Add(378, defaultIFrames);
		sortedDictionary.Add(24, defaultIFrames);
		sortedDictionary.Add(185, defaultIFrames);
		sortedDictionary.Add(659, Do(IDStaticIFrames(5)));
		sortedDictionary.Add(569, defaultIFrames);
		sortedDictionary.Add(570, defaultIFrames);
		sortedDictionary.Add(571, defaultIFrames);
		sortedDictionary.Add(567, defaultIFrames);
		sortedDictionary.Add(568, defaultIFrames);
		sortedDictionary.Add(323, defaultIFrames);
		sortedDictionary.Add(330, defaultIFrames);
		sortedDictionary.Add(955, defaultIFrames);
		sortedDictionary.Add(613, defaultIFrames);
		sortedDictionary.Add(624, defaultIFrames);
		sortedDictionary.Add(37, defaultIFrames);
		sortedDictionary.Add(470, defaultIFrames);
		sortedDictionary.Add(397, defaultIFrames);
		sortedDictionary.Add(831, defaultIFrames);
		sortedDictionary.Add(246, Do(IDStaticIFrames(7), SingleHitImmunity));
		sortedDictionary.Add(249, defaultIFrames);
		sortedDictionary.Add(971, defaultIFrames);
		sortedDictionary.Add(33, defaultIFrames);
		sortedDictionary.Add(48, defaultIFrames);
		sortedDictionary.Add(730, defaultIFrames);
		sortedDictionary.Add(732, defaultIFrames);
		sortedDictionary.Add(78, defaultIFrames);
		sortedDictionary.Add(908, defaultIFrames);
		sortedDictionary.Add(122, defaultIFrames);
		sortedDictionary.Add(523, defaultIFrames);
		sortedDictionary.Add(511, defaultIFrames);
		sortedDictionary.Add(512, defaultIFrames);
		sortedDictionary.Add(513, defaultIFrames);
		sortedDictionary.Add(510, defaultIFrames);
		sortedDictionary.Add(209, defaultIFrames);
		sortedDictionary.Add(590, defaultIFrames);
		sortedDictionary.Add(433, defaultIFrames);
		sortedDictionary.Add(423, defaultIFrames);
		sortedDictionary.Add(4, defaultIFrames);
		sortedDictionary.Add(114, defaultIFrames);
		sortedDictionary.Add(70, defaultIFrames);
		sortedDictionary.Add(304, defaultIFrames);
		sortedDictionary.Add(282, defaultIFrames);
		sortedDictionary.Add(980, defaultIFrames);
		sortedDictionary.Add(463, defaultIFrames);
		sortedDictionary.Add(11, defaultIFrames);
		sortedDictionary.Add(7, defaultIFrames);
		sortedDictionary.Add(8, defaultIFrames);
		sortedDictionary.Add(615, defaultIFrames);
		sortedDictionary.Add(616, defaultIFrames);
		sortedDictionary.Add(578, defaultIFrames);
		sortedDictionary.Add(579, defaultIFrames);
		sortedDictionary.Add(1012, defaultIFrames);
		sortedDictionary.Add(979, defaultIFrames);
		sortedDictionary.Add(954, defaultIFrames);
		sortedDictionary.Add(27, defaultIFrames);
		sortedDictionary.Add(22, defaultIFrames);
		sortedDictionary.Add(165, defaultIFrames);
		sortedDictionary.Add(903, defaultIFrames);
		sortedDictionary.Add(786, defaultIFrames);
		sortedDictionary.Add(784, defaultIFrames);
		sortedDictionary.Add(805, defaultIFrames);
		sortedDictionary.Add(211, defaultIFrames);
		sortedDictionary.Add(1, defaultIFrames);
		sortedDictionary.Add(52, defaultIFrames);
		sortedDictionary.Add(444, defaultIFrames);
		sortedDictionary.Add(876, defaultIFrames);
		sortedDictionary.Add(880, defaultIFrames);
		sortedDictionary.Add(929, defaultIFrames);
		currentTweaks = sortedDictionary;
		static IProjectileTweak[] RebalanceYoyo(float lifetime, float range, float topSpeed, int extraUpdates, int iframes = 10)
		{
			return new IProjectileTweak[5]
			{
				ExtraUpdatesExact(extraUpdates),
				LocalIFrames(iframes * (extraUpdates + 1)),
				YoyoLifetime((lifetime <= 0f) ? (-1f) : (lifetime * (float)(extraUpdates + 1))),
				YoyoRange(range),
				YoyoTopSpeed(topSpeed / (float)(extraUpdates + 1))
			};
		}
	}

	internal static void UnloadTweaks()
	{
		currentTweaks?.Clear();
		currentTweaks = null;
	}

	internal static void SetDefaults_ApplyTweaks(Projectile proj)
	{
		if (currentTweaks == null || !currentTweaks.TryGetValue(proj.type, out var tweaks))
		{
			return;
		}
		IProjectileTweak[] array = tweaks;
		foreach (IProjectileTweak tweak in array)
		{
			if (tweak.AppliesTo(proj))
			{
				tweak.ApplyTweak(proj);
			}
		}
	}

	internal static IProjectileTweak[] Do(params IProjectileTweak[] r)
	{
		return r;
	}

	internal static bool IsAYoyo(Projectile proj)
	{
		return proj.aiStyle == 99;
	}

	internal static IProjectileTweak ArmorPenetrationDelta(int d)
	{
		return new ArmorPenetrationDeltaRule(d);
	}

	internal static IProjectileTweak ArmorPenetrationExact(int a)
	{
		return new ArmorPenetrationExactRule(a);
	}

	internal static IProjectileTweak ExtraUpdatesDelta(int d)
	{
		return new ExtraUpdatesDeltaRule(d);
	}

	internal static IProjectileTweak ExtraUpdatesExact(int eu)
	{
		return new ExtraUpdatesExactRule(eu);
	}

	internal static IProjectileTweak MaxUpdatesExact(int mu)
	{
		return new MaxUpdatesExactRule(mu);
	}

	internal static IProjectileTweak IDStaticIFrames(int f)
	{
		return new IDStaticIFrameRule(f);
	}

	internal static IProjectileTweak LocalIFrames(int f)
	{
		return new LocalIFrameRule(f);
	}

	internal static IProjectileTweak PiercingDelta(int p)
	{
		return new PiercingDeltaRule(p);
	}

	internal static IProjectileTweak PiercingExact(int p)
	{
		return new PiercingExactRule(p);
	}

	internal static IProjectileTweak ScaleDelta(float d)
	{
		return new ScaleDeltaRule(d);
	}

	internal static IProjectileTweak ScaleExact(float s)
	{
		return new ScaleExactRule(s);
	}

	internal static IProjectileTweak ScaleRatio(float f)
	{
		return new ScaleRatioRule(f);
	}

	internal static IProjectileTweak TimeLeftDelta(int d)
	{
		return new TimeLeftDeltaRule(d);
	}

	internal static IProjectileTweak TimeLeftExact(int t)
	{
		return new TimeLeftExactRule(t);
	}

	internal static IProjectileTweak YoyoLifetime(float l)
	{
		return new YoyoLifetimeRule(l);
	}

	internal static IProjectileTweak YoyoRange(float r)
	{
		return new YoyoRangeRule(r);
	}

	internal static IProjectileTweak YoyoTopSpeed(float r)
	{
		return new YoyoTopSpeedRule(r);
	}

	public CalamityGlobalProjectile()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		ParentNPCIndex = -1;
		showArcFlash = true;
		arenaBoxPosition = Vector2.Zero;
		defExtraUpdates = -1;
		HomingTarget = -1;
		CaughtItemID = -1;
		PersistentFishingData = -1f;
		PersistentFishingDataVector2 = Vector2.Zero;
		base._002Ector();
	}
}
