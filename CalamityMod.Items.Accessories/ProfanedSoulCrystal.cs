using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ProfanedSoulCrystal : TransformationAccessory, ILocalizedModType, IModType, IDyeableShaderRenderer
{
	public enum ProfanedSoulCrystalState
	{
		Vanity,
		Buffs,
		Enraged,
		Empowered
	}

	public static string[] contributorNames = new string[6] { "IbanPlay", "Chen", "Nincity", "Amber", "Mishiro", "LordMetarex" };

	public static int ShieldDurabilityMax = 100;

	public static int ShieldRechargeDelay = CalamityUtils.SecondsToFrames(5);

	public static int TotalShieldRechargeTime = CalamityUtils.SecondsToFrames(4);

	public const int maxMinionRequirement = 10;

	public const int maxPscAnimTime = 120;

	public static SummonTag SummonTag = new SummonTag
	{
		MultiplicativeTagDamage = 0.2f,
		TagModifyHitEffects = ApplyTagModifyHit,
		AutoDrawTooltip = false
	};

	public KeyValuePair<int, int> profanedCrystalWingCounter = new KeyValuePair<int, int>(1, 10);

	public KeyValuePair<int, int> profanedCrystalAnimCounter = new KeyValuePair<int, int>(0, 10);

	public new string LocalizationCategory => "Items.Accessories";

	public int OwnerPlayer { get; set; }

	public float RenderDepth => 3f;

	public bool ShaderIsDyeable => false;

	public bool ShouldDrawDyeableShader
	{
		get
		{
			if (CalamityClientConfig.Instance.EnergyShieldOpacity <= 0f)
			{
				return false;
			}
			if (OwnerPlayer < 0 || OwnerPlayer >= 255)
			{
				return false;
			}
			Player player = Main.player[OwnerPlayer];
			if (player == null)
			{
				return false;
			}
			if (player.outOfRange || player.dead)
			{
				return false;
			}
			if (player.Calamity().drawingParameters.ProfanedShieldCharge <= 0f)
			{
				return false;
			}
			return true;
		}
	}

	public override string AssetPath => "CalamityMod/Items/Accessories/";

	public override (EquipType Type, string AssetName, string EquipName)[] EquipSlots => new(EquipType, string, string)[8]
	{
		(EquipType.Head, "ProfanedSoulTrans", null),
		(EquipType.Body, "ProfanedSoulTrans", null),
		(EquipType.Legs, "ProfanedSoulTrans", null),
		(EquipType.Wings, "ProfanedSoulTrans", null),
		(EquipType.Head, "ProfanedSoulTransNight", "PscNightHead"),
		(EquipType.Legs, "ProfanedSoulTransNight", "PscNightLegs"),
		(EquipType.Wings, "ProfanedSoulTransNight", "PscNightWings"),
		(EquipType.Face, null, null)
	};

	public static void ApplyTagModifyHit(Projectile proj, NPC npc, ref NPC.HitModifiers modifiers, ref float tagDamageMult, ref float critChance)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.player[proj.owner].Calamity().pscState >= 1)
		{
			bool empowered = Main.player[proj.owner].Calamity().pscState == 3;
			modifiers.ScalingBonusDamage += (empowered ? 0.4f : SummonTag.MultiplicativeTagDamage) * tagDamageMult;
			if (!Main.dedServ)
			{
				Color color = ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, 0);
				float power = Math.Min((float)npc.height / 100f, 3f);
				GeneralParticleHandler.SpawnParticle(new FlameParticle(new Vector2(Main.rand.NextFloat(npc.Left.X, npc.Right.X), Main.rand.NextFloat(npc.Top.Y, npc.Bottom.Y)), 50, 0.25f, power, color * (Main.dayTime ? 1f : 1.25f), color * (Main.dayTime ? 1.25f : 1f)));
			}
		}
	}

	public void DrawDyeableShader(SpriteBatch spriteBatch)
	{
		ProfanedSoulArtifact.DrawProfanedSoulShields(OwnerPlayer);
	}

	internal static ProfanedSoulCrystalState GetPscStateFor(Player player, bool ignoreNoBuffs = false)
	{
		if (!player.Calamity().profanedCrystalBuffs && !ignoreNoBuffs)
		{
			return ProfanedSoulCrystalState.Vanity;
		}
		if ((ignoreNoBuffs && (!DownedBossSystem.downedCalamitas || !DownedBossSystem.downedExoMechs || (float)player.maxMinions - player.slotsMinions < 10f)) || player.Transformation().Type == ModContent.ItemType<ProfanedSoulCrystal>() || !player.HasBuff<ProfanedCrystalBuff>())
		{
			return ProfanedSoulCrystalState.Vanity;
		}
		bool num = player.slotsMinions == 0f;
		bool noSentries = !Main.projectile.Any((Projectile proj) => proj.active && proj.owner == player.whoAmI && proj.sentry);
		if (num & noSentries)
		{
			return ProfanedSoulCrystalState.Empowered;
		}
		if (Main.dayTime)
		{
			return ProfanedSoulCrystalState.Buffs;
		}
		return ProfanedSoulCrystalState.Enraged;
	}

	internal static Color GetColorForPsc(int pscState, bool day, int alpha = 0)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		return (Color)((ProfanedSoulCrystalState)pscState switch
		{
			ProfanedSoulCrystalState.Vanity => new Color(231, 160, 56, alpha), 
			ProfanedSoulCrystalState.Buffs => new Color(255, 110, 56, alpha), 
			ProfanedSoulCrystalState.Enraged => new Color(145, 208, 188, alpha), 
			ProfanedSoulCrystalState.Empowered => day ? new Color(255, 75, 13, alpha) : new Color(84, 186, 163, alpha), 
			_ => Color.White, 
		});
	}

	internal static Color GetLerpedColorForPsc(CalamityPlayer calPlayer)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		if (calPlayer.pscLerpColor != Color.White)
		{
			return calPlayer.pscLerpColor;
		}
		bool day = Main.dayTime;
		double num = (Main.dayTime ? 54000.0 : 86400.0);
		double currentTime = Main.time;
		double midday = 27000.0;
		double midnight = 16200.0;
		Color dayColor = GetColorForPsc(calPlayer.pscState, day);
		Color nightColor = GetColorForPsc((calPlayer.pscState > 2) ? 3 : 2, day: false);
		Color targetColor = (Main.dayTime ? dayColor : nightColor);
		Color nonTargetColor = (Main.dayTime ? nightColor : dayColor);
		double targetTime = (Main.dayTime ? midday : midnight);
		double interpolant = Utils.GetLerpValue(num, targetTime, currentTime);
		Color result = Color.White;
		if (!Main.dayTime && Main.time > midnight)
		{
			result = Color.Lerp(nightColor, dayColor, 2f - (float)interpolant);
		}
		else if (Main.dayTime && Main.time > midday)
		{
			result = Color.Lerp(nightColor, dayColor, (float)interpolant);
		}
		if (result == Color.White)
		{
			result = Color.Lerp(nonTargetColor, targetColor, ((Main.time < midday) ? 2f : 0f) - (float)interpolant);
		}
		calPlayer.pscLerpColor = result;
		return result;
	}

	public override void ArmorIDSets()
	{
		int equipSlotBody = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Body);
		ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
		ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
		int equipSlotLegs = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
		ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
		ArmorIDs.Legs.Sets.OverridesLegs[equipSlotLegs] = true;
		int equipSlotNightLegs = EquipLoader.GetEquipSlot(base.Mod, "PscNightLegs", EquipType.Legs);
		ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotNightLegs] = true;
		ArmorIDs.Legs.Sets.OverridesLegs[equipSlotNightLegs] = true;
	}

	public override (SoundStyle sound, int delay)? HurtSound(Player p)
	{
		return ((p.Calamity().pSoulShieldDurability > 0) ? ProfanedGuardianDefender.ShieldDeathSound : Providence.HurtSound, 20);
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(8, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		SummonTag.TagItem = base.Item.type;
		SummonTag.TagTexture = TextureAssets.Item[base.Type];
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
	{
		return incomingItem.type != ModContent.ItemType<ProfanedSoulArtifact>();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		bool scal = DownedBossSystem.downedCalamitas;
		bool draedon = DownedBossSystem.downedExoMechs;
		if (!scal && !draedon)
		{
			string reject = this.GetLocalization("LockedBoth").Format(this.GetLocalizedValue("ExoMechsLock"), this.GetLocalizedValue("CalamitasLock")) + "\n" + this.GetLocalizedValue("Reject");
			tooltips.FindAndReplace("[STATUS]", reject);
			TooltipLine linePrice = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Price");
			if (linePrice != null)
			{
				linePrice.Text = "";
			}
		}
		else if (!scal || !draedon)
		{
			string reject2 = this.GetLocalization("Locked").Format((!draedon) ? this.GetLocalizedValue("ExoMechsLock") : this.GetLocalizedValue("CalamitasLock")) + "\n" + this.GetLocalizedValue("Reject");
			tooltips.FindAndReplace("[STATUS]", reject2);
			TooltipLine linePrice2 = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Price");
			if (linePrice2 != null)
			{
				linePrice2.Text = "";
			}
		}
		else
		{
			string manaCost = (100f * Main.LocalPlayer.manaCost).ToString("N0");
			string full = this.GetLocalization("FullTooltip").Format(10, manaCost);
			tooltips.FindAndReplace("[STATUS]", full);
		}
	}

	public override bool CustomSetEquipType(Player player, EquipType type, Mod mod, string name)
	{
		switch (type)
		{
		case EquipType.Legs:
			player.legs = EquipLoader.GetEquipSlot(base.Mod, Main.dayTime ? "ProfanedSoulCrystal" : "PscNightLegs", type);
			return true;
		case EquipType.Head:
			player.head = EquipLoader.GetEquipSlot(base.Mod, Main.dayTime ? "ProfanedSoulCrystal" : "PscNightHead", type);
			return true;
		case EquipType.Wings:
			player.wings = EquipLoader.GetEquipSlot(base.Mod, Main.dayTime ? "ProfanedSoulCrystal" : "PscNightWings", type);
			return true;
		default:
			return false;
		}
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.pSoulArtifact = true;
		modPlayer.profanedCrystal = true;
		if (!modPlayer.profanedCrystalPrevious && player.ownedProjectileCounts[ModContent.ProjectileType<PscTransformAnimation>()] == 0)
		{
			modPlayer.pSoulShieldDurability = 1;
			modPlayer.profanedCrystalAnim = 120;
			Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<PscTransformAnimation>(), 0, 0f, player.whoAmI);
		}
		if (DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs)
		{
			player.Calamity().profanedSoulRelicBuff = true;
		}
		modPlayer.pSoulShieldVisible = !hideVisual;
		DetermineTransformationEligibility(player);
	}

	internal static void DetermineTransformationEligibility(Player player)
	{
		if (!player.Calamity().profanedCrystalBuffs && player.Calamity().profanedCrystalAnim == -1 && DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs && (float)player.maxMinions - player.slotsMinions >= 10f && player.HasBuff<ProfanedCrystalBuff>())
		{
			player.Calamity().profanedCrystalBuffs = true;
			player.Calamity().pscState = (int)GetPscStateFor(player);
		}
	}

	internal static bool TransformItemUsage(Item item, Player player)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI != Main.myPlayer)
		{
			return false;
		}
		IEntitySource source = player.GetSource_ItemUse(item);
		int weaponType = (item.CountsAsClass<MeleeDamageClass>() ? 1 : (item.CountsAsClass<RangedDamageClass>() ? 2 : (item.CountsAsClass<MagicDamageClass>() ? 3 : (item.CountsAsClass<ThrowingDamageClass>() ? 4 : (item.CountsAsClass<SummonMeleeSpeedDamageClass>() ? 5 : (-1))))));
		if (weaponType > 0)
		{
			if (player.Calamity().profanedSoulWeaponType != weaponType || player.Calamity().profanedSoulWeaponUsage >= 370)
			{
				player.Calamity().profanedSoulWeaponType = weaponType;
				player.Calamity().profanedSoulWeaponUsage = 0;
			}
			Vector2 correctedVelocity = Main.MouseWorld - player.Center;
			((Vector2)(ref correctedVelocity)).Normalize();
			bool empowered = player.Calamity().pscState == 3;
			bool enraged = player.Calamity().pscState >= 2;
			if (item.CountsAsClass<MeleeDamageClass>())
			{
				if (player.Calamity().profanedSoulWeaponUsage % (enraged ? 4 : 6) == 0)
				{
					if (player.Calamity().profanedSoulWeaponUsage > 0 && player.Calamity().profanedSoulWeaponUsage % (enraged ? 20 : 30) == 0)
					{
						int numProj = 5;
						correctedVelocity *= 20f;
						int spread = -6;
						for (int i = 0; i < numProj; i++)
						{
							Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(correctedVelocity.X, correctedVelocity.Y), (double)MathHelper.ToRadians((float)spread), default(Vector2));
							int separation = i * 4 - 8;
							int spearBaseDamage = 350;
							int spearDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(spearBaseDamage);
							int proj = Projectile.NewProjectile(source, player.Center.X, player.Center.Y - (float)separation, perturbedspeed.X, perturbedspeed.Y, ModContent.ProjectileType<ProfanedCrystalMeleeSpear>(), spearDamage, 1f, player.whoAmI, Main.rand.NextBool((player.Calamity().profanedSoulWeaponUsage == 4) ? 5 : 7) ? 1f : 0f);
							if (proj.WithinBounds(Main.maxProjectiles))
							{
								Main.projectile[proj].DamageType = DamageClass.Summon;
								Main.projectile[proj].originalDamage = spearBaseDamage;
							}
							spread += 3;
							SoundEngine.PlaySound(in SoundID.Item20, player.Center);
						}
						player.Calamity().profanedSoulWeaponUsage = 0;
					}
					else
					{
						int spearBaseDamage2 = 250;
						int spearDamage2 = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(spearBaseDamage2);
						int proj2 = Projectile.NewProjectile(source, player.Center, correctedVelocity * 14f, ModContent.ProjectileType<ProfanedCrystalMeleeSpear>(), spearDamage2, 1f, player.whoAmI, Main.rand.NextBool((player.Calamity().profanedSoulWeaponUsage == 4) ? 5 : 7) ? 1f : 0f, 1f);
						if (proj2.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[proj2].DamageType = DamageClass.Summon;
							Main.projectile[proj2].originalDamage = spearBaseDamage2;
						}
						SoundEngine.PlaySound(in SoundID.Item20, player.Center);
					}
				}
				player.Calamity().profanedSoulWeaponUsage++;
			}
			else if (item.CountsAsClass<RangedDamageClass>())
			{
				if (enraged || Main.rand.NextBool())
				{
					correctedVelocity *= 20f;
					Vector2 perturbedspeed2 = Utils.RotatedBy(new Vector2(correctedVelocity.X + (float)Main.rand.Next(-3, 4), correctedVelocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians(3f), default(Vector2));
					bool num = Main.rand.NextDouble() <= ((enraged && !empowered) ? 0.2 : 0.3);
					bool isThiccBoomer = num && Main.rand.NextDouble() <= 0.05;
					int projType = ((!num) ? 3 : (isThiccBoomer ? 1 : 2));
					int boomBaseDamage = 200;
					int boomDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(boomBaseDamage);
					switch (projType)
					{
					case 1:
					case 2:
					{
						int proj4 = Projectile.NewProjectile(source, player.Center, perturbedspeed2, ModContent.ProjectileType<ProfanedCrystalRangedHuges>(), boomDamage, 0f, player.whoAmI, (projType == 1) ? 1f : 0f);
						if (proj4.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[proj4].DamageType = DamageClass.Summon;
							Main.projectile[proj4].originalDamage = boomBaseDamage;
						}
						break;
					}
					case 3:
					{
						int proj3 = Projectile.NewProjectile(source, player.Center, perturbedspeed2, ModContent.ProjectileType<ProfanedCrystalRangedSmalls>(), boomDamage, 0f, player.whoAmI);
						if (proj3.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[proj3].DamageType = DamageClass.Summon;
							Main.projectile[proj3].originalDamage = boomBaseDamage;
						}
						break;
					}
					}
					if (projType > 1)
					{
						SoundEngine.PlaySound(in SoundID.Item20, player.Center);
					}
				}
			}
			else if (item.CountsAsClass<MagicDamageClass>())
			{
				if (player.ownedProjectileCounts[ModContent.ProjectileType<ProfanedCrystalMageFireball>()] == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<ProfanedCrystalMageFireballSplit>()] == 0)
				{
					player.Calamity().profanedSoulWeaponUsage = 0;
				}
				int manaCost = (int)(100f * player.manaCost);
				if (player.Calamity().profanedSoulWeaponUsage == 0 && !player.silence && player.CheckMana(manaCost, pay: true))
				{
					player.manaRegenDelay = (int)player.maxRegenDelay;
					correctedVelocity *= 25f;
					SoundEngine.PlaySound(in SoundID.Item20, player.Center);
					int magefireBaseDamage = 900;
					int mageFireDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(magefireBaseDamage);
					if (player.HasBuff(94))
					{
						int sickPenalty = (int)((float)mageFireDamage * (0.05f * (float)((player.buffTime[player.FindBuffIndex(94)] + 60) / 60)));
						mageFireDamage -= sickPenalty;
					}
					int proj5 = Projectile.NewProjectile(source, player.position, correctedVelocity, ModContent.ProjectileType<ProfanedCrystalMageFireball>(), mageFireDamage, 1f, player.whoAmI, empowered ? 1f : 0f);
					if (proj5.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj5].DamageType = DamageClass.Summon;
						Main.projectile[proj5].originalDamage = magefireBaseDamage;
					}
					player.Calamity().profanedSoulWeaponUsage = (enraged ? 20 : 25);
				}
				if (player.Calamity().profanedSoulWeaponUsage > 0)
				{
					player.Calamity().profanedSoulWeaponUsage--;
				}
			}
			else if (item.CountsAsClass<ThrowingDamageClass>())
			{
				if (player.ownedProjectileCounts[ModContent.ProjectileType<ProfanedCrystalRogueShard>()] == 0)
				{
					player.Calamity().profanedSoulWeaponUsage = 0;
				}
				if (player.Calamity().profanedSoulWeaponUsage >= (empowered ? 120 : 360))
				{
					float crystalCount = 36f;
					for (float i2 = 0f; i2 < crystalCount; i2++)
					{
						float angle = (float)Math.PI * 2f / crystalCount * i2;
						int shardBaseDamage = 176;
						int shardDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(shardBaseDamage);
						int proj6 = Projectile.NewProjectile(source, player.Center, angle.ToRotationVector2() * 12f, ModContent.ProjectileType<ProfanedCrystalRogueShard>(), shardDamage, 1f, player.whoAmI);
						if (proj6.WithinBounds(Main.maxProjectiles))
						{
							Main.projectile[proj6].DamageType = DamageClass.Summon;
							Main.projectile[proj6].originalDamage = shardBaseDamage;
						}
						SoundEngine.PlaySound(in SoundID.Item20, player.Center);
					}
					player.Calamity().profanedSoulWeaponUsage = 0;
				}
				else if (player.Calamity().profanedSoulWeaponUsage % (empowered ? 5 : 10) == 0)
				{
					int chains = ((!empowered) ? 1 : 3);
					int num2 = (empowered ? 72 : 36);
					int shardBaseDamage2 = (empowered ? 125 : 220);
					int shardDamage2 = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(shardBaseDamage2);
					float interval = (float)(num2 / chains) * (empowered ? 5f : 10f);
					if (Math.Floor((float)player.Calamity().profanedSoulWeaponUsage / interval) % 2.0 == 0.0)
					{
						double radians = (float)Math.PI * 2f / (float)chains;
						double angleA = radians * 0.5;
						double angleB = (double)MathHelper.ToRadians(90f) - angleA;
						float velocityX = (float)(2.0 * Math.Sin(angleA) / Math.Sin(angleB));
						Vector2 spinningPoint = default(Vector2);
						((Vector2)(ref spinningPoint))._002Ector(velocityX, -2f);
						for (int j = 0; j < chains; j++)
						{
							Vector2 vector2 = spinningPoint.RotatedBy(radians * (double)j + (double)MathHelper.ToRadians((float)player.Calamity().profanedSoulWeaponUsage));
							((Vector2)(ref vector2)).Normalize();
							int proj7 = Projectile.NewProjectile(source, player.Center, vector2 * 12f, ModContent.ProjectileType<ProfanedCrystalRogueShard>(), shardDamage2, 1f, player.whoAmI, 1f);
							if (proj7.WithinBounds(Main.maxProjectiles))
							{
								Main.projectile[proj7].DamageType = DamageClass.Summon;
								Main.projectile[proj7].originalDamage = shardBaseDamage2;
							}
						}
						SoundEngine.PlaySound(in SoundID.Item20, player.Center);
					}
				}
				player.Calamity().profanedSoulWeaponUsage += (empowered ? 1 : 2);
			}
			else if (item.CountsAsClass<SummonMeleeSpeedDamageClass>())
			{
				if (player.ownedProjectileCounts[ModContent.ProjectileType<ProfanedCrystalWhip>()] == 0)
				{
					player.Calamity().profanedSoulWeaponUsage = 0;
				}
				if (player.Calamity().profanedSoulWeaponUsage == 0)
				{
					int whipBaseDamage = 250;
					int whipDamage = (int)player.GetTotalDamage<SummonMeleeSpeedDamageClass>().ApplyTo(whipBaseDamage);
					bool buffed = player.HasBuff<ProfanedCrystalWhipBuff>();
					correctedVelocity *= (buffed ? 10f : 8f);
					int permittedDistance = (player.HasBuff<ProfanedCrystalWhipBuff>() ? 10 : 8);
					correctedVelocity.X = Math.Clamp(correctedVelocity.X, -permittedDistance, permittedDistance);
					correctedVelocity.Y = Math.Clamp(correctedVelocity.Y, -permittedDistance, permittedDistance);
					player.ChangeDir(MathF.Sign(correctedVelocity.X));
					Projectile.NewProjectile(source, player.Center, correctedVelocity, ModContent.ProjectileType<ProfanedCrystalWhip>(), whipDamage, 1f, player.whoAmI);
					player.Calamity().profanedSoulWeaponUsage = 10;
				}
				player.Calamity().profanedSoulWeaponUsage--;
			}
		}
		return false;
	}

	public override void TransformFrameEffects(Player player)
	{
		bool enrage = player.Calamity().pscState >= 2;
		if (profanedCrystalWingCounter.Value == 0)
		{
			int key = profanedCrystalWingCounter.Key;
			profanedCrystalWingCounter = new KeyValuePair<int, int>((key != 3) ? (key + 1) : 0, enrage ? 5 : 8);
		}
		player.wingFrame = profanedCrystalWingCounter.Key;
		profanedCrystalWingCounter = new KeyValuePair<int, int>(profanedCrystalWingCounter.Key, profanedCrystalWingCounter.Value - 1);
		player.armorEffectDrawOutlines = true;
		if (player.Calamity().profanedCrystalBuffs)
		{
			player.armorEffectDrawShadow = true;
			if (enrage)
			{
				player.armorEffectDrawOutlinesForbidden = true;
			}
		}
	}

	private bool IsValidTransitionFrame(AnimationType currentAnim, AnimationType newAnim, int frame, int counter)
	{
		bool result = newAnim != AnimationType.Jump && currentAnim != AnimationType.Jump;
		if (currentAnim == AnimationType.Walk && newAnim == AnimationType.Idle)
		{
			result = counter <= 0 && (frame == 11 || frame == 15 || frame == 19);
		}
		else if (currentAnim == AnimationType.Idle && newAnim == AnimationType.Walk)
		{
			result = counter <= 0 && (frame == 2 || frame == 6);
		}
		return (currentAnim != newAnim) & result;
	}

	private int HandlePSCAnimationFrames(Player player, AnimationType newType)
	{
		int key = profanedCrystalAnimCounter.Key;
		int value = profanedCrystalAnimCounter.Value - 1;
		AnimationType currentType = ((key >= 8) ? ((key == 8) ? AnimationType.Jump : AnimationType.Walk) : AnimationType.Idle);
		bool isInvalidTransFrame = !IsValidTransitionFrame(currentType, newType, key, value);
		AnimationType type = (isInvalidTransFrame ? newType : currentType);
		int frameCount = ((type == AnimationType.Walk || (player.Calamity().profanedCrystal && player.statLife <= (int)((double)player.statLifeMax2 * 0.5))) ? 7 : 10);
		int lowerRange = type switch
		{
			AnimationType.Jump => 8, 
			AnimationType.Idle => 0, 
			_ => 9, 
		};
		int upperRange = type switch
		{
			AnimationType.Jump => 8, 
			AnimationType.Idle => 7, 
			_ => 22, 
		};
		if (value <= 0 || !isInvalidTransFrame)
		{
			value = frameCount;
			key = ((key < lowerRange || key >= upperRange) ? lowerRange : (key + 1));
		}
		profanedCrystalAnimCounter = new KeyValuePair<int, int>(key, value);
		return profanedCrystalAnimCounter.Key;
	}

	public override void TransformPostUpdate(Player player)
	{
		bool validEquipSlot = player.legs == EquipLoader.GetEquipSlot(base.Mod, "ProfanedSoulCrystal", EquipType.Legs) || player.legs == EquipLoader.GetEquipSlot(base.Mod, "PscNightLegs", EquipType.Legs);
		if ((player.Transformation().Type == ModContent.ItemType<ProfanedSoulCrystal>()) & validEquipSlot)
		{
			bool usingCarpet = player.carpetTime > 0 && player.controlJump;
			AnimationType animType = AnimationType.Walk;
			if ((player.sliding || player.velocity.Y != 0f || player.mount.Active || player.grappling[0] != -1 || !player.CheckSolidGround() || player.GoingDownWithGrapple) && !usingCarpet)
			{
				animType = AnimationType.Jump;
			}
			else if ((player.velocity.X == 0f) | usingCarpet)
			{
				animType = AnimationType.Idle;
			}
			int frame = HandlePSCAnimationFrames(player, animType);
			player.legFrame.Y = player.legFrame.Height * frame;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ProfanedSoulArtifact>().AddIngredient<ShadowspecBar>(5).AddIngredient<DivineGeode>(50)
			.AddIngredient<UnholyEssence>(100)
			.AddTile<ProfanedCrucible>()
			.AddDecraftCondition(CalamityConditions.DownedSupremeCalamitas, CalamityConditions.DownedExoMechs)
			.Register();
	}
}
