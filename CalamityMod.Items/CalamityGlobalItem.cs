using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CalamityMod.Balancing;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.ChatTags;
using CalamityMod.CustomRecipes;
using CalamityMod.DataStructures;
using CalamityMod.Events;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Hydrothermic;
using CalamityMod.Items.Armor.Prismatic;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Tools;
using CalamityMod.Items.VanillaArmorChanges;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Other;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using CalamityMod.Systems.Collections;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.UI;
using CalamityMod.UI.CalamitasEnchants;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;

namespace CalamityMod.Items;

public class CalamityGlobalItem : GlobalItem
{
	internal interface IItemTweak
	{
		bool AppliesTo(Item it);

		void ApplyTweak(Item it);
	}

	internal class AttackSpeedExactRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public AttackSpeedExactRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			ItemID.Sets.BonusAttackSpeedMultiplier[it.type] = CapAttackSpeed(ratio);
		}
	}

	internal class AttackSpeedRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public AttackSpeedRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			float currentAttackSpeedRatio = ItemID.Sets.BonusAttackSpeedMultiplier[it.type];
			ItemID.Sets.BonusAttackSpeedMultiplier[it.type] = CapAttackSpeed(ratio * currentAttackSpeedRatio);
		}
	}

	internal class AxePowerRule : IItemTweak
	{
		internal readonly int newAxePower;

		public AxePowerRule(int newDisplayedAxePower)
		{
			newAxePower = newDisplayedAxePower / 5;
		}

		public bool AppliesTo(Item it)
		{
			return IsAxe(it);
		}

		public void ApplyTweak(Item it)
		{
			it.axe = newAxePower;
			if (it.axe < 0)
			{
				it.axe = 0;
			}
		}
	}

	internal class ConsumableRule : IItemTweak
	{
		internal readonly bool flag;

		public ConsumableRule(bool c)
		{
			flag = c;
		}

		public bool AppliesTo(Item it)
		{
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.consumable = flag;
		}
	}

	internal class CritChanceDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public CritChanceDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return DealsDamage(it);
		}

		public void ApplyTweak(Item it)
		{
			it.crit += delta;
			if (it.crit < 0)
			{
				it.crit = 0;
			}
		}
	}

	internal class CritChanceExactRule : IItemTweak
	{
		internal readonly int newCrit;

		public CritChanceExactRule(int crit)
		{
			newCrit = crit;
		}

		public bool AppliesTo(Item it)
		{
			return DealsDamage(it);
		}

		public void ApplyTweak(Item it)
		{
			it.crit = newCrit;
			if (it.crit < 0)
			{
				it.crit = 0;
			}
		}
	}

	internal class DamageDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public DamageDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return DealsDamage(it);
		}

		public void ApplyTweak(Item it)
		{
			it.damage += delta;
			if (it.damage < 0)
			{
				it.damage = 0;
			}
		}
	}

	internal class DamageExactRule : IItemTweak
	{
		internal readonly int newDamage;

		public DamageExactRule(int dmg)
		{
			newDamage = dmg;
		}

		public bool AppliesTo(Item it)
		{
			return DealsDamage(it);
		}

		public void ApplyTweak(Item it)
		{
			it.damage = newDamage;
			if (it.damage < 0)
			{
				it.damage = 0;
			}
		}
	}

	internal class DamageRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public DamageRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return DealsDamage(it);
		}

		public void ApplyTweak(Item it)
		{
			it.damage = (int)((float)it.damage * ratio);
			if (it.damage < 0)
			{
				it.damage = 0;
			}
		}
	}

	internal class DefenseDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public DefenseDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return HasDefense(it);
		}

		public void ApplyTweak(Item it)
		{
			it.defense += delta;
			if (it.defense < 0)
			{
				it.defense = 0;
			}
		}
	}

	internal class DefenseExactRule : IItemTweak
	{
		internal readonly int newDefense;

		public DefenseExactRule(int def)
		{
			newDefense = def;
		}

		public bool AppliesTo(Item it)
		{
			if (!HasDefense(it))
			{
				return it.accessory;
			}
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.defense = newDefense;
			if (it.defense < 0)
			{
				it.defense = 0;
			}
		}
	}

	internal class HammerPowerRule : IItemTweak
	{
		internal readonly int newHammerPower;

		public HammerPowerRule(int h)
		{
			newHammerPower = h;
		}

		public bool AppliesTo(Item it)
		{
			return IsHammer(it);
		}

		public void ApplyTweak(Item it)
		{
			it.hammer = newHammerPower;
			if (it.hammer < 0)
			{
				it.hammer = 0;
			}
		}
	}

	internal class KnockbackDeltaRule : IItemTweak
	{
		internal readonly float delta;

		public KnockbackDeltaRule(float d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return HasKnockback(it);
		}

		public void ApplyTweak(Item it)
		{
			it.knockBack += delta;
			if (it.knockBack < 0f)
			{
				it.knockBack = 0f;
			}
		}
	}

	internal class KnockbackExactRule : IItemTweak
	{
		internal readonly float newKnockback;

		public KnockbackExactRule(float kb)
		{
			newKnockback = kb;
		}

		public bool AppliesTo(Item it)
		{
			return HasKnockback(it);
		}

		public void ApplyTweak(Item it)
		{
			it.knockBack = newKnockback;
			if (it.knockBack < 0f)
			{
				it.knockBack = 0f;
			}
		}
	}

	internal class KnockbackRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public KnockbackRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return HasKnockback(it);
		}

		public void ApplyTweak(Item it)
		{
			it.knockBack *= ratio;
			if (it.knockBack < 0f)
			{
				it.knockBack = 0f;
			}
		}
	}

	internal class ManaDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public ManaDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return UsesMana(it);
		}

		public void ApplyTweak(Item it)
		{
			it.mana += delta;
			if (it.mana < 0)
			{
				it.mana = 0;
			}
		}
	}

	internal class ManaExactRule : IItemTweak
	{
		internal readonly int newMana;

		public ManaExactRule(int m)
		{
			newMana = m;
		}

		public bool AppliesTo(Item it)
		{
			return UsesMana(it);
		}

		public void ApplyTweak(Item it)
		{
			it.mana = newMana;
			if (it.mana < 0)
			{
				it.mana = 0;
			}
		}
	}

	internal class ManaRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public ManaRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return UsesMana(it);
		}

		public void ApplyTweak(Item it)
		{
			it.mana = (int)((float)it.mana * ratio);
			if (it.mana < 0)
			{
				it.mana = 0;
			}
		}
	}

	internal class MaxStackRule : IItemTweak
	{
		internal readonly int newMaxStack = 9999;

		public MaxStackRule(int stk)
		{
			newMaxStack = stk;
		}

		public bool AppliesTo(Item it)
		{
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.maxStack = newMaxStack;
			if (it.maxStack < 1)
			{
				it.maxStack = 1;
			}
		}
	}

	internal class MeleeSettingsRule : IItemTweak
	{
		internal readonly bool speed = true;

		internal readonly bool trueMelee;

		public MeleeSettingsRule(bool s, bool t = false)
		{
			speed = s;
			trueMelee = t;
		}

		public bool AppliesTo(Item it)
		{
			return IsMelee(it);
		}

		public void ApplyTweak(Item it)
		{
			if (speed)
			{
				it.attackSpeedOnlyAffectsWeaponAnimation = false;
			}
			if (speed)
			{
				it.DamageType = (trueMelee ? TrueMeleeDamageClass.Instance : DamageClass.Melee);
			}
			else
			{
				it.DamageType = (trueMelee ? TrueMeleeNoSpeedDamageClass.Instance : DamageClass.MeleeNoSpeed);
			}
		}
	}

	internal class PickPowerRule : IItemTweak
	{
		internal readonly int newPickPower;

		public PickPowerRule(int p)
		{
			newPickPower = p;
		}

		public bool AppliesTo(Item it)
		{
			return IsPickaxe(it);
		}

		public void ApplyTweak(Item it)
		{
			it.pick = newPickPower;
			if (it.pick < 0)
			{
				it.pick = 0;
			}
		}
	}

	internal class ScaleDeltaRule : IItemTweak
	{
		internal readonly float delta;

		public ScaleDeltaRule(float d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return IsScalable(it);
		}

		public void ApplyTweak(Item it)
		{
			if (!DisableScalingForOverhaul)
			{
				it.scale += delta;
				if (it.scale < 0f)
				{
					it.scale = 0f;
				}
			}
		}
	}

	internal class ScaleExactRule : IItemTweak
	{
		internal readonly float newScale;

		public ScaleExactRule(float s)
		{
			newScale = s;
		}

		public bool AppliesTo(Item it)
		{
			return IsScalable(it);
		}

		public void ApplyTweak(Item it)
		{
			if (!DisableScalingForOverhaul)
			{
				it.scale = newScale;
				if (it.scale < 0f)
				{
					it.scale = 0f;
				}
			}
		}
	}

	internal class ScaleRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public ScaleRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsScalable(it);
		}

		public void ApplyTweak(Item it)
		{
			if (!DisableScalingForOverhaul)
			{
				it.scale *= ratio;
				if (it.scale < 0f)
				{
					it.scale = 0f;
				}
			}
		}
	}

	internal class ShootSpeedDeltaRule : IItemTweak
	{
		internal readonly float delta;

		public ShootSpeedDeltaRule(float d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return UtilizesVelocity(it);
		}

		public void ApplyTweak(Item it)
		{
			it.shootSpeed += delta;
			if (it.shootSpeed < 0f)
			{
				it.shootSpeed = 0f;
			}
		}
	}

	internal class ShootSpeedExactRule : IItemTweak
	{
		internal readonly float newShootSpeed;

		public ShootSpeedExactRule(float ss)
		{
			newShootSpeed = ss;
		}

		public bool AppliesTo(Item it)
		{
			return UtilizesVelocity(it);
		}

		public void ApplyTweak(Item it)
		{
			it.shootSpeed = newShootSpeed;
			if (it.shootSpeed < 0f)
			{
				it.shootSpeed = 0f;
			}
		}
	}

	internal class ShootSpeedRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public ShootSpeedRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return UtilizesVelocity(it);
		}

		public void ApplyTweak(Item it)
		{
			it.shootSpeed *= ratio;
			if (it.shootSpeed < 0f)
			{
				it.shootSpeed = 0f;
			}
		}
	}

	internal class TileBoostDeltaRule : IItemTweak
	{
		private readonly int delta;

		public TileBoostDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.tileBoost += delta;
		}
	}

	internal class TileBoostExactRule : IItemTweak
	{
		private readonly int newTileBoost;

		public TileBoostExactRule(int tb)
		{
			newTileBoost = tb;
		}

		public bool AppliesTo(Item it)
		{
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.tileBoost = newTileBoost;
		}
	}

	internal class UseDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public UseDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation += delta;
			it.useTime += delta;
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class UseExactRule : IItemTweak
	{
		internal readonly int newUseTime;

		public UseExactRule(int ut)
		{
			newUseTime = ut;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation = newUseTime;
			it.useTime = newUseTime;
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class UseRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public UseRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation = (int)((float)it.useAnimation * ratio);
			it.useTime = (int)((float)it.useTime * ratio);
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class UseAnimationDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public UseAnimationDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation += delta;
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
		}
	}

	internal class UseAnimationExactRule : IItemTweak
	{
		internal readonly int newUseAnimation;

		public UseAnimationExactRule(int ua)
		{
			newUseAnimation = ua;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation = newUseAnimation;
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
		}
	}

	internal class UseAnimationRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public UseAnimationRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useAnimation = (int)((float)it.useAnimation * ratio);
			if (it.useAnimation < 1)
			{
				it.useAnimation = 1;
			}
		}
	}

	internal class UseTimeDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public UseTimeDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useTime += delta;
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class UseTimeExactRule : IItemTweak
	{
		internal readonly int newUseTime;

		public UseTimeExactRule(int ut)
		{
			newUseTime = ut;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useTime = newUseTime;
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class UseTimeRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public UseTimeRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useTime = (int)((float)it.useTime * ratio);
			if (it.useTime < 1)
			{
				it.useTime = 1;
			}
		}
	}

	internal class ReuseDelayDeltaRule : IItemTweak
	{
		internal readonly int delta;

		public ReuseDelayDeltaRule(int d)
		{
			delta = d;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.reuseDelay += delta;
			if (it.reuseDelay < 0)
			{
				it.reuseDelay = 0;
			}
		}
	}

	internal class ReuseDelayExactRule : IItemTweak
	{
		internal readonly int newReuseDelay;

		public ReuseDelayExactRule(int rd)
		{
			newReuseDelay = rd;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.reuseDelay = newReuseDelay;
			if (it.reuseDelay < 0)
			{
				it.reuseDelay = 0;
			}
		}
	}

	internal class ReuseDelayRatioRule : IItemTweak
	{
		internal readonly float ratio = 1f;

		public ReuseDelayRatioRule(float f)
		{
			ratio = f;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.reuseDelay = (int)((float)it.reuseDelay * ratio);
			if (it.reuseDelay < 0)
			{
				it.reuseDelay = 0;
			}
		}
	}

	internal class UseTurnRule : IItemTweak
	{
		internal readonly bool flag = true;

		public UseTurnRule(bool ut)
		{
			flag = ut;
		}

		public bool AppliesTo(Item it)
		{
			return IsUsable(it);
		}

		public void ApplyTweak(Item it)
		{
			it.useTurn = flag;
		}
	}

	internal class ValueRule : IItemTweak
	{
		internal readonly int newValue;

		public ValueRule(int v)
		{
			newValue = v;
		}

		public bool AppliesTo(Item it)
		{
			return true;
		}

		public void ApplyTweak(Item it)
		{
			it.value = newValue;
			if (it.value < 0)
			{
				it.value = 0;
			}
		}
	}

	private BitsByte flag0 = (byte)0;

	public float Charge;

	public float MaxCharge = 1f;

	public float ChargePerUse;

	public float ChargePerAltUse = -1f;

	public Enchantment? AppliedEnchantment;

	public float DischargeEnchantExhaustion;

	public const float DischargeEnchantExhaustionCap = 1600f;

	public const float DischargeEnchantMinDamageFactor = 0.77f;

	public const float DischargeEnchantMaxDamageFactor = 1.26f;

	public static readonly Color ExhumedTooltipColor;

	internal static ChargingEnergyParticleSet EnchantmentEnergyParticles;

	private static int cachedForgeID;

	private static readonly int Rarity0BuyPrice;

	private static readonly int Rarity1BuyPrice;

	private static readonly int Rarity2BuyPrice;

	private static readonly int Rarity3BuyPrice;

	private static readonly int Rarity4BuyPrice;

	private static readonly int Rarity5BuyPrice;

	private static readonly int Rarity6BuyPrice;

	private static readonly int Rarity7BuyPrice;

	private static readonly int Rarity8BuyPrice;

	private static readonly int Rarity9BuyPrice;

	private static readonly int Rarity10BuyPrice;

	private static readonly int Rarity11BuyPrice;

	private static readonly int Rarity12BuyPrice;

	private static readonly int Rarity13BuyPrice;

	private static readonly int Rarity14BuyPrice;

	private static readonly int Rarity15BuyPrice;

	private static readonly int Rarity16BuyPrice;

	private static readonly int Rarity17BuyPrice;

	private static readonly int[] RarityBuyPriceArray;

	private static string[] MainTooltipBackupInsertionPositions;

	private static string[] RevTooltipInsertionPositions;

	private static readonly Dictionary<int, LocalizedText> SpeedTooltips;

	internal static SortedDictionary<int, IItemTweak[]> currentTweaks;

	public override bool InstancePerEntity => true;

	public bool UsesCharge
	{
		get
		{
			return flag0[0];
		}
		set
		{
			flag0[0] = value;
		}
	}

	public float ChargeRatio
	{
		get
		{
			float ratio = Charge / MaxCharge;
			if (!float.IsNaN(ratio) && !float.IsInfinity(ratio))
			{
				return MathHelper.Clamp(ratio, 0f, 1f);
			}
			return 0f;
		}
	}

	public bool CannotBeEnchanted
	{
		get
		{
			return flag0[1];
		}
		set
		{
			flag0[1] = value;
		}
	}

	public float DischargeExhaustionRatio
	{
		get
		{
			float ratio = DischargeEnchantExhaustion / 1600f;
			if (!float.IsNaN(ratio) && !float.IsInfinity(ratio))
			{
				return MathHelper.Clamp(ratio, 0f, 1f);
			}
			return 0f;
		}
	}

	public bool revengeanceItem
	{
		get
		{
			return flag0[2];
		}
		set
		{
			flag0[2] = value;
		}
	}

	public bool donorItem
	{
		get
		{
			return flag0[3];
		}
		set
		{
			flag0[3] = value;
		}
	}

	public bool devItem
	{
		get
		{
			return flag0[4];
		}
		set
		{
			flag0[4] = value;
		}
	}

	public static int RarityWhiteBuyPrice => Rarity0BuyPrice;

	public static int RarityBlueBuyPrice => Rarity1BuyPrice;

	public static int RarityGreenBuyPrice => Rarity2BuyPrice;

	public static int RarityOrangeBuyPrice => Rarity3BuyPrice;

	public static int RarityLightRedBuyPrice => Rarity4BuyPrice;

	public static int RarityPinkBuyPrice => Rarity5BuyPrice;

	public static int RarityLightPurpleBuyPrice => Rarity6BuyPrice;

	public static int RarityLimeBuyPrice => Rarity7BuyPrice;

	public static int RarityYellowBuyPrice => Rarity8BuyPrice;

	public static int RarityCyanBuyPrice => Rarity9BuyPrice;

	public static int RarityRedBuyPrice => Rarity10BuyPrice;

	public static int RarityPurpleBuyPrice => Rarity11BuyPrice;

	public static int RarityTurquoiseBuyPrice => Rarity12BuyPrice;

	public static int RarityPureGreenBuyPrice => Rarity13BuyPrice;

	public static int RarityDarkBlueBuyPrice => Rarity14BuyPrice;

	public static int RarityVioletBuyPrice => Rarity15BuyPrice;

	public static int RarityHotPinkBuyPrice => Rarity16BuyPrice;

	public static int RarityCalamityRedBuyPrice => Rarity17BuyPrice;

	private static bool DisableScalingForOverhaul => ExternalMods.overhaul != null;

	internal static IItemTweak Consumable => new ConsumableRule(c: true);

	internal static IItemTweak NotConsumable => new ConsumableRule(c: false);

	internal static IItemTweak UseMeleeSpeed => new MeleeSettingsRule(s: true);

	internal static IItemTweak DontUseMeleeSpeed => new MeleeSettingsRule(s: false);

	internal static IItemTweak TrueMelee => new MeleeSettingsRule(s: true, t: true);

	internal static IItemTweak TrueMeleeNoSpeed => new MeleeSettingsRule(s: false, t: true);

	internal static IItemTweak UseTurn => new UseTurnRule(ut: true);

	internal static IItemTweak NoUseTurn => new UseTurnRule(ut: false);

	internal static IItemTweak Worthless => new ValueRule(0);

	public override GlobalItem Clone(Item item, Item itemClone)
	{
		CalamityGlobalItem obj = (CalamityGlobalItem)base.Clone(item, itemClone);
		obj.flag0 = flag0;
		obj.UsesCharge = UsesCharge;
		obj.Charge = Charge;
		obj.MaxCharge = MaxCharge;
		obj.ChargePerUse = ChargePerUse;
		obj.ChargePerAltUse = ChargePerAltUse;
		obj.AppliedEnchantment = (AppliedEnchantment.HasValue ? new Enchantment?(AppliedEnchantment.Value) : ((Enchantment?)null));
		obj.DischargeEnchantExhaustion = DischargeEnchantExhaustion;
		return obj;
	}

	public override void SetStaticDefaults()
	{
		SetStaticDefaults_ShimmerRecipes();
		WingStats[] stats = ArmorIDs.Wing.Sets.Stats;
		stats[7].FlyTime = 160;
		stats[7].AccRunSpeedOverride = 7.5f;
		stats[10].AccRunSpeedOverride = 9f;
		stats[10].AccRunAccelerationMult = 1.5f;
		stats[9].FlyTime = 130;
		stats[14].FlyTime = 180;
		stats[14].AccRunSpeedOverride = 6.25f;
		stats[5].AccRunAccelerationMult = 1.5f;
		stats[8].FlyTime = 240;
		stats[13].FlyTime = 170;
		stats[13].AccRunSpeedOverride = 9f;
		stats[13].AccRunAccelerationMult = 1.5f;
		stats[11].AccRunAccelerationMult = 2f;
		stats[24].FlyTime = 210;
		stats[20].FlyTime = 210;
		stats[44].FlyTime = 120;
		stats[28].DownHoverSpeedOverride = 10.8f;
		stats[28].DownHoverAccelerationMult = 10.8f;
		stats[33].DownHoverSpeedOverride = 10.8f;
		stats[33].DownHoverAccelerationMult = 10.8f;
		stats[35].DownHoverSpeedOverride = 10.8f;
		stats[35].DownHoverAccelerationMult = 10.8f;
		stats[37].DownHoverSpeedOverride = 10.8f;
		stats[37].DownHoverAccelerationMult = 10.8f;
		stats[45].AccRunAccelerationMult = 2.75f;
		stats[45].DownHoverSpeedOverride = 12f;
		stats[45].DownHoverAccelerationMult = 12f;
	}

	public override void SetDefaults(Item item)
	{
		if (item.accessory)
		{
			CannotBeEnchanted = true;
		}
		if (item.type == 576 || item.createTile == 139 || ItemID.Sets.ShimmerTransformToItem[item.type] == 576)
		{
			item.rare = 1;
		}
		if (item.type == 661 || item.type == 660 || item.type == 659)
		{
			item.rare = 4;
		}
		if (item.type == 4822)
		{
			item.vanity = false;
		}
		if (item.type == 4987)
		{
			item.rare = 5;
		}
		if (item.type == 4989)
		{
			item.rare = 8;
		}
		if (item.type == 4956)
		{
			item.rare = ModContent.RarityType<BurnishedAuric>();
		}
		if (item.type == 520 || item.type == 521 || item.type == 575 || item.type == 548 || item.type == 549 || item.type == 547)
		{
			item.ammo = 520;
			item.notAmmo = true;
		}
		if (item.type == 5 && item.healLife < 25)
		{
			item.healLife = 25;
		}
		if (item.type == 723)
		{
			item.ChangePlayerDirectionOnShoot = true;
		}
		SetDefaults_ApplyTweaks(item);
		if (item.shoot == 0)
		{
			if (item.DamageType == DamageClass.Melee)
			{
				item.DamageType = TrueMeleeDamageClass.Instance;
			}
			else if (item.DamageType == DamageClass.MeleeNoSpeed)
			{
				item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
			}
		}
	}

	public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		if (item.CountsAsClass<RogueDamageClass>())
		{
			velocity *= modPlayer.rogueVelocity;
			if (modPlayer.gloveOfRecklessness)
			{
				velocity = velocity.RotatedByRandom(MathHelper.ToRadians(12f));
			}
		}
		if (modPlayer.eArtifact && item.CountsAsClass<RangedDamageClass>())
		{
			velocity *= 1.25f;
		}
	}

	public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		IEntitySource playerSource = player.GetSource_FromThis();
		Vector2 mouse = player.ClampedMouseWorld();
		if (Main.myPlayer == player.whoAmI && player.Calamity().cursedSummonsEnchant && NPC.CountNPCS(ModContent.NPCType<CalamitasEnchantDemon>()) < 2)
		{
			CalamityNetcode.NewNPC_ClientSide(mouse, ModContent.NPCType<CalamitasEnchantDemon>(), player);
			SoundEngine.PlaySound(in SoundID.DD2_DarkMageSummonSkeleton, mouse);
		}
		bool belowManaThreshold = (float)player.statMana < (float)player.statManaMax2 * 0.25f;
		bool traitorousAlreadyInPlay = player.ownedProjectileCounts[ModContent.ProjectileType<ManaMonster>()] > 0;
		if ((Main.myPlayer == player.whoAmI && player.Calamity().manaMonsterEnchant && !traitorousAlreadyInPlay) & belowManaThreshold)
		{
			int remainingMana = player.statMana;
			int damagePerManaConsumed = 80;
			int monsterDamage = (int)player.GetTotalDamage<MagicDamageClass>().ApplyTo(remainingMana * damagePerManaConsumed);
			Vector2 shootVelocity = player.SafeDirectionTo(mouse, -Vector2.UnitY).RotatedByRandom(0.07000000029802322) * Main.rand.NextFloat(4f, 5f);
			Projectile.NewProjectile(source, player.Center + shootVelocity, shootVelocity, ModContent.ProjectileType<ManaMonster>(), monsterDamage, 0f, player.whoAmI);
			player.statMana = 0;
		}
		if (modPlayer.bloodflareMage && modPlayer.canFireBloodflareMageProjectile && item.CountsAsClass<MagicDamageClass>() && !item.channel)
		{
			modPlayer.canFireBloodflareMageProjectile = false;
			if (player.whoAmI == Main.myPlayer)
			{
				int bloodflareBoltDamage = CalamityUtils.DamageSoftCap((double)damage * BloodflareHeadMagic.GhostBoltDamageRatio, BloodflareHeadMagic.GhostBoltonDamageSoftcap);
				Projectile.NewProjectile(playerSource, position, velocity, ModContent.ProjectileType<GhostlyBolt>(), bloodflareBoltDamage, 1f, player.whoAmI);
			}
		}
		if (modPlayer.bloodflareRanged && modPlayer.canFireBloodflareRangedProjectile && item.CountsAsClass<RangedDamageClass>() && !item.channel)
		{
			modPlayer.canFireBloodflareRangedProjectile = false;
			if (player.whoAmI == Main.myPlayer)
			{
				int bloodsplosionDamage = CalamityUtils.DamageSoftCap((double)damage * BloodflareHeadRanged.BloodBombDamageRatio, BloodflareHeadRanged.BloodBombDamageSoftcap);
				Projectile.NewProjectile(playerSource, position, velocity, ModContent.ProjectileType<BloodBomb>(), bloodsplosionDamage, 2f, player.whoAmI);
			}
		}
		if (modPlayer.tarraMage && !item.channel && modPlayer.tarraCrits >= TarragonHeadMagic.CritsToSpawnLeaves && player.whoAmI == Main.myPlayer)
		{
			modPlayer.tarraCrits = 0;
			int leafAmt = 8 + Main.rand.Next(3);
			int leafDamage = (int)((float)damage * TarragonHeadMagic.LeafDamageRatio);
			for (int l = 0; l < leafAmt; l++)
			{
				float spreadMult = 0.025f * (float)l;
				float xDiff = velocity.X + (float)Main.rand.Next(-25, 26) * spreadMult;
				float yDiff = velocity.Y + (float)Main.rand.Next(-25, 26) * spreadMult;
				float speed = ((Vector2)(ref velocity)).Length();
				speed = item.shootSpeed / speed;
				xDiff *= speed;
				yDiff *= speed;
				int projectile = Projectile.NewProjectile(playerSource, position, new Vector2(xDiff, yDiff), 206, leafDamage, knockBack, player.whoAmI);
				if (projectile.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[projectile].DamageType = DamageClass.Generic;
				}
			}
		}
		if (modPlayer.ataxiaBolt && modPlayer.canFireAtaxiaRangedProjectile && item.CountsAsClass<RangedDamageClass>() && !item.channel)
		{
			modPlayer.canFireAtaxiaRangedProjectile = false;
			if (player.whoAmI == Main.myPlayer)
			{
				int ataxiaFlareDamage = (int)((double)damage * HydrothermicHeadRanged.FlareDamageRatio);
				Projectile.NewProjectile(playerSource, position, velocity * 1.25f, ModContent.ProjectileType<HydrothermicFlare>(), ataxiaFlareDamage, 2f, player.whoAmI);
			}
		}
		if (modPlayer.godSlayerRanged && modPlayer.canFireGodSlayerRangedProjectile && item.CountsAsClass<RangedDamageClass>() && !item.channel)
		{
			modPlayer.canFireGodSlayerRangedProjectile = false;
			if (player.whoAmI == Main.myPlayer)
			{
				int shrapnelRoundDamage = CalamityUtils.DamageSoftCap((double)damage * GodSlayerHeadRanged.ShrapnelRoundDamageRatio, GodSlayerHeadRanged.ShrapnelRoundDamageSoftcap);
				Projectile.NewProjectile(playerSource, position, velocity * 1.25f, ModContent.ProjectileType<GodSlayerShrapnelRound>(), shrapnelRoundDamage, 2f, player.whoAmI);
			}
		}
		if (modPlayer.ataxiaVolley && modPlayer.canFireAtaxiaRogueProjectile && item.CountsAsClass<ThrowingDamageClass>() && !item.channel)
		{
			modPlayer.canFireAtaxiaRogueProjectile = false;
			int flareID = ModContent.ProjectileType<HydrothermicFlareRogue>();
			int flareDamage = CalamityUtils.DamageSoftCap((double)HydrothermicHeadRogue.VolleyDamage + (double)damage * HydrothermicHeadRogue.VolleyDamageRatio, HydrothermicHeadRogue.VolleyDamageSoftcap);
			if (player.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(in SoundID.Item20, player.Center);
				for (int i = 0; i < 6; i++)
				{
					Vector2 circleVel = ((float)Math.PI * 2f * (float)i / 6f + velocity.ToRotation()).ToRotationVector2() * 5f;
					Projectile.NewProjectile(playerSource, player.Center, circleVel, flareID, flareDamage, 1f, player.whoAmI);
				}
			}
		}
		if (modPlayer.prismaticRegalia && item.CountsAsClass<MagicDamageClass>() && Main.rand.NextBool(PrismaticRegalia.RocketChanceDenominator) && !item.channel && player.whoAmI == Main.myPlayer)
		{
			for (int j = -5; j <= 5; j += 5)
			{
				if (j != 0)
				{
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians((float)j));
					int rocket = Projectile.NewProjectile(playerSource, position, perturbedSpeed, ModContent.ProjectileType<ScorpioRocket>(), (int)((float)damage * PrismaticRegalia.RocketDamageRatio), 2f, player.whoAmI, 0f, 12f);
					if (rocket.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[rocket].DamageType = DamageClass.Generic;
					}
				}
			}
		}
		if (modPlayer.victideSet && (item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<SummonDamageClass>()) && Main.rand.NextBool(10) && !item.channel && player.whoAmI == Main.myPlayer)
		{
			int seashellDamage = CalamityUtils.DamageSoftCap(damage * 2, 46);
			Projectile.NewProjectile(source, position, velocity * 1.25f, ModContent.ProjectileType<Seashell>(), seashellDamage, 1f, player.whoAmI);
		}
		return true;
	}

	public override void SaveData(Item item, TagCompound tag)
	{
		tag.Add("charge", Charge);
		tag.Add("enchantmentID", AppliedEnchantment.HasValue ? AppliedEnchantment.Value.ID : 0);
		tag.Add("DischargeEnchantExhaustion", DischargeEnchantExhaustion);
	}

	public override void LoadData(Item item, TagCompound tag)
	{
		if (tag.ContainsKey("Charge"))
		{
			Charge = tag.GetInt("Charge");
		}
		else
		{
			Charge = tag.GetFloat("charge");
		}
		DischargeEnchantExhaustion = tag.GetFloat("DischargeEnchantExhaustion");
		Enchantment? savedEnchantment = EnchantmentManager.FindByID(tag.GetInt("enchantmentID"));
		if (savedEnchantment.HasValue)
		{
			AppliedEnchantment = savedEnchantment.Value;
			_ = AppliedEnchantment.Value;
			item.Calamity().AppliedEnchantment.Value.CreationEffect?.Invoke(item);
		}
	}

	public override void NetSend(Item item, BinaryWriter writer)
	{
		writer.Write(Charge);
		writer.Write(AppliedEnchantment.HasValue ? AppliedEnchantment.Value.ID : 0);
		writer.Write(DischargeEnchantExhaustion);
	}

	public override void NetReceive(Item item, BinaryReader reader)
	{
		Charge = reader.ReadSingle();
		Enchantment? savedEnchantment = EnchantmentManager.FindByID(reader.ReadInt32());
		if (savedEnchantment.HasValue)
		{
			AppliedEnchantment = savedEnchantment.Value;
			if (AppliedEnchantment.Value.CreationEffect != null)
			{
				item.Calamity().AppliedEnchantment.Value.CreationEffect(item);
			}
		}
		DischargeEnchantExhaustion = reader.ReadSingle();
	}

	public override bool CanPickup(Item item, Player player)
	{
		if ((item.type == 184 || item.type == 1735 || item.type == 1868) && (player.HeldItem.type == ModContent.ItemType<IonBlaster>() || player.HeldItem.type == ModContent.ItemType<ApoctosisArray>()))
		{
			return false;
		}
		return base.CanPickup(item, player);
	}

	public override bool OnPickup(Item item, Player player)
	{
		if ((item.type == 58 || item.type == 1734 || item.type == 1867) && player.Calamity().photosynthesis)
		{
			player.HealPlayer(PhotosynthesisPotion.IncreasedHeartHeal);
		}
		return true;
	}

	public override void HoldItem(Item item, Player player)
	{
		if (player.Calamity().evilSmasherBoost > 0 && item.type != ModContent.ItemType<EvilSmasher>())
		{
			player.Calamity().evilSmasherBoost = 0;
		}
		if (player.Calamity().ChaosStone && item.mana == 0 && !player.ItemTimeIsZero)
		{
			player.manaRegenDelay = player.maxRegenDelay;
		}
	}

	public override bool? UseItem(Item item, Player player)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		if (Main.zenithWorld && item.type == 5335 && NPC.AnyNPCs(ModContent.NPCType<THELORDE>()))
		{
			player.AddBuff(ModContent.BuffType<NOU>(), 216000);
		}
		if (item.type == 5 && player.Calamity().fungalSymbiote)
		{
			player.AddBuff(ModContent.BuffType<Mushy>(), 3600);
		}
		if (item.healLife > 0)
		{
			if (player.whoAmI == Main.myPlayer && !modPlayer.spawnedJellyAura)
			{
				if (modPlayer.absorber)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<AbsorberAura>(), 0, 0f, player.whoAmI);
				}
				else if (modPlayer.GrandGelatin)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<GreenJellyAura>(), 0, 0f, player.whoAmI);
				}
				else
				{
					if (modPlayer.cleansingjelly)
					{
						Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<BlueJellyAura>(), 0, 0f, player.whoAmI);
					}
					if (modPlayer.lifejelly)
					{
						Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<PinkJellyAura>(), 0, 0f, player.whoAmI);
					}
				}
				modPlayer.spawnedJellyAura = true;
			}
			if (modPlayer.bloomStone)
			{
				modPlayer.bloomStone = false;
				modPlayer.bloomStoneTotalHeal = (modPlayer.bloomStoneHealPool = player.GetHealLife(item));
				modPlayer.bloomStone = true;
			}
		}
		if (item.type == 213 || item.type == 5295)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			Tile tileAbove = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY - 1);
			if (tile.HasTile && !tileAbove.HasTile && tileAbove.LiquidAmount == 0 && tile.TileType == ModContent.TileType<ScorchedRemains>() && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
			{
				Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = (ushort)ModContent.TileType<ScorchedRemainsGrass>();
				SoundEngine.PlaySound(in SoundID.Dig, player.Center);
				return true;
			}
			if (tile.HasTile && tile.TileType == ModContent.TileType<AstralDirt>() && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
			{
				Main.tile[Player.tileTargetX, Player.tileTargetY].TileType = (ushort)ModContent.TileType<AstralGrass>();
				SoundEngine.PlaySound(in SoundID.Dig, player.Center);
				return true;
			}
		}
		return base.UseItem(item, player);
	}

	public override bool AltFunctionUse(Item item, Player player)
	{
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().profanedCrystalBuffs && item.pick == 0 && item.axe == 0 && item.hammer == 0 && item.autoReuse && (item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>()))
		{
			return false;
		}
		if (player.HeldItem.type == ModContent.ItemType<VoidConcentrationStaff>() && player.ownedProjectileCounts[ModContent.ProjectileType<VoidConcentrationBlackhole>()] == 0)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.ModProjectile is VoidConcentrationAura && p.owner == player.whoAmI)
				{
					p.ModProjectile<VoidConcentrationAura>().HandleRightClick();
					break;
				}
			}
			return false;
		}
		if (player.HeldItem.type == ModContent.ItemType<GlacialEmbrace>())
		{
			bool canContinue = true;
			int count = 0;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p2 = enumerator2.Current;
				if (p2.type == ModContent.ProjectileType<GlacialEmbracePointyThing>() && p2.owner == player.whoAmI)
				{
					if (p2.ai[1] > 1f)
					{
						canContinue = false;
						break;
					}
					if (p2.ai[1] == 0f && ((GlacialEmbracePointyThing)p2.ModProjectile).circlingPlayer)
					{
						count++;
					}
				}
			}
			if (canContinue && count > 0)
			{
				Vector2 mouse = player.ClampedMouseWorld();
				if (mouse.MinionHoming(1000f, player) != null)
				{
					int pointyThingyAmount = count;
					float angleVariance = (float)Math.PI * 2f / (float)pointyThingyAmount;
					float angle = 0f;
					IEntitySource source = player.GetSource_ItemUse(player.HeldItem);
					for (int i = 0; i < pointyThingyAmount; i++)
					{
						if (Main.projectile.Length == Main.maxProjectiles)
						{
							break;
						}
						int GlacialEmbraceDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(80f);
						int projj = Projectile.NewProjectile(source, mouse, Vector2.Zero, ModContent.ProjectileType<GlacialEmbracePointyThing>(), GlacialEmbraceDamage, 1f, player.whoAmI, angle, 2f);
						Main.projectile[projj].originalDamage = 80;
						angle += angleVariance;
						for (int j = 0; j < 22; j++)
						{
							Dust dust = Dust.NewDustDirect(Main.projectile[projj].position, Main.projectile[projj].width, Main.projectile[projj].height, 80);
							dust.velocity = Vector2.UnitY * Main.rand.NextFloat(3f, 5.5f) * (float)Main.rand.NextBool().ToDirectionInt();
							dust.noGravity = true;
						}
					}
				}
			}
			return false;
		}
		return base.AltFunctionUse(item, player);
	}

	public override bool CanUseItem(Item item, Player player)
	{
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		CalamityGlobalItem modItem = item.Calamity();
		if (PopupGUIManager.AnyGUIsActive)
		{
			return false;
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfDeliveranceSpear>()] > 0 && (item.damage > 0 || item.ammo != AmmoID.None))
		{
			return false;
		}
		if (player.mount.Type == 8)
		{
			return base.CanUseItem(item, player);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<GiantIbanRobotOfDoom>()] > 0)
		{
			if (item.type == 3611)
			{
				return false;
			}
			if (item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.fishingPole > 0)
			{
				return false;
			}
			if (item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MeleeDamageClass>())
			{
				if (player.altFunctionUse == 0)
				{
					return FlamsteedRing.TransformItemUsage(item, player);
				}
				return false;
			}
		}
		bool autoreuse = item.autoReuse || item.CountsAsClass<SummonMeleeSpeedDamageClass>();
		if (((modPlayer.profanedCrystalBuffs && item.pick == 0 && item.axe == 0 && item.hammer == 0) & autoreuse) && (item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<SummonMeleeSpeedDamageClass>()))
		{
			if (player.altFunctionUse != 0)
			{
				return AltFunctionUse(item, player);
			}
			return ProfanedSoulCrystal.TransformItemUsage(item, player);
		}
		if (!item.IsAir)
		{
			if (modPlayer.dischargingItemEnchant)
			{
				float exhaustionCost = (float)item.useTime * 2.25f;
				if (exhaustionCost < 10f)
				{
					exhaustionCost = 10f;
				}
				DischargeEnchantExhaustion = MathHelper.Clamp(DischargeEnchantExhaustion - exhaustionCost, 0.001f, 1600f);
			}
			else
			{
				DischargeEnchantExhaustion = 0f;
			}
		}
		if (item.type >= ItemID.Count && modItem.UsesCharge)
		{
			float chargeNeeded = ((player.altFunctionUse == 2 && modItem.ChargePerAltUse != -1f) ? modItem.ChargePerAltUse : modItem.ChargePerUse);
			if (chargeNeeded > 0f)
			{
				if (modItem.Charge < chargeNeeded)
				{
					return false;
				}
				if (player.CheckMana(item) && item.ModItem.CanUseItem(player))
				{
					Charge -= chargeNeeded;
				}
			}
		}
		player.Calamity().GemTechState.OnItemUseEffects(item);
		if (item.type == 1326)
		{
			if (player.chaosState)
			{
				return false;
			}
			Vector2 teleportLocation = default(Vector2);
			teleportLocation.X = (float)Main.mouseX + Main.screenPosition.X;
			if (player.gravDir == 1f)
			{
				teleportLocation.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
			}
			else
			{
				teleportLocation.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			teleportLocation.X -= player.width / 2;
			if (teleportLocation.X > 50f && teleportLocation.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation.Y > 50f && teleportLocation.Y < (float)(Main.maxTilesY * 16 - 50))
			{
				int x = (int)teleportLocation.X / 16;
				int y = (int)teleportLocation.Y / 16;
				if ((Main.tile[x, y].WallType != 87 || (double)y <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(teleportLocation, player.width, player.height))
				{
					int duration = (CalamityPlayer.areThereAnyDamnBosses ? CalamityPlayer.chaosStateDuration : 360);
					player.AddBuff(88, duration);
				}
			}
		}
		if (item.type == 43 || item.type == 70 || item.type == 1331 || item.type == 560 || item.type == 4271 || item.type == 1133 || item.type == 5120 || item.type == 4988 || item.type == 544 || item.type == 556 || item.type == 557 || item.type == 3601)
		{
			return !BossRushEvent.BossRushActive;
		}
		return true;
	}

	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
	{
		if (item.type >= ItemID.Count)
		{
			CalamityGlobalItem modItem = item.Calamity();
			if (!item.CountsAsClass<SummonDamageClass>() && modItem.DischargeEnchantExhaustion > 0f)
			{
				damage *= DischargeEnchantmentDamageFormula();
			}
			if (!item.CountsAsClass<SummonDamageClass>() && modItem != null && modItem.UsesCharge && Charge != 0f)
			{
				damage *= ChargeDamageFormula();
			}
		}
	}

	internal float DischargeEnchantmentDamageFormula()
	{
		float interpolant = (float)Math.Pow(2.0, DischargeExhaustionRatio) - 1f;
		return MathHelper.Lerp(0.77f, 1.26f, interpolant);
	}

	internal float ChargeDamageFormula()
	{
		float x = MathHelper.Clamp(ChargeRatio, 0f, 1f);
		return MathHelper.Clamp(1.08f - 0.04f / (x + 0.06f), 0f, 1f);
	}

	public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
	{
		if (player.Calamity().oldFashioned)
		{
			modifiers.SourceDamage *= OldFashioned.DamageReductionMultiplier;
		}
		if (player.Calamity().ivDrip)
		{
			modifiers.SourceDamage *= IVDripOnTheRocks.DamageReductionMultiplier;
		}
	}

	public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (target.Calamity().hyperiusMarked)
		{
			int damage = 0;
			damage = ((target.Calamity().hyperiusDamage >= damageDone) ? damageDone : (damageDone - target.Calamity().hyperiusDamage));
			target.Calamity().hyperiusDamage -= damage;
			Projectile projectile = Projectile.NewProjectileDirect(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<HyperiusDamage>(), (int)((float)damage * HyperiusBullet.overflowEfficency), 0f, player.whoAmI, target.whoAmI);
			projectile.DamageType = item.DamageType;
			projectile.ArmorPenetration = item.ArmorPenetration;
			if (target.Calamity().hyperiusDamage <= 0)
			{
				target.Calamity().hyperiusDamage = 0;
				target.Calamity().hyperiusMarked = false;
			}
		}
	}

	public override string IsArmorSet(Item head, Item body, Item legs)
	{
		string managedArmorSetName = VanillaArmorChangeManager.GetSetBonusName(Main.player[head.playerIndexTheItemIsReservedFor]);
		if (!string.IsNullOrEmpty(managedArmorSetName))
		{
			return managedArmorSetName;
		}
		if (head.type == 238 && (body.type == 1282 || body.type == 1283 || body.type == 1284 || body.type == 1285 || body.type == 1286 || body.type == 1287 || body.type == 4256))
		{
			return "WizardHat";
		}
		if (head.type == 2275 && (body.type == 1282 || body.type == 1283 || body.type == 1284 || body.type == 1285 || body.type == 1286 || body.type == 1287 || body.type == 4256))
		{
			return "MagicHat";
		}
		if (head.type == 4982 && body.type == 4983 && legs.type == 4984)
		{
			return "CrystalAssassin";
		}
		if (head.type == 1503 && body.type == 1504 && legs.type == 1505)
		{
			return "SpectreHealing";
		}
		if (head.type == 2763 && body.type == 2764 && legs.type == 2765)
		{
			return "SolarFlare";
		}
		return "";
	}

	public override void ModifyItemScale(Item item, Player player, ref float scale)
	{
		if (item.CountsAsClass<MeleeDamageClass>() && player.HasBuff(25))
		{
			scale += 0.15f;
		}
	}

	public override void UpdateArmorSet(Player player, string set)
	{
		CalamityPlayer modPlayer = player.Calamity();
		VanillaArmorChangeManager.CreateTooltipManuallyAsNecessary(player);
		if (set == "WizardHat")
		{
			player.GetCritChance<MagicDamageClass>() -= 6f;
			player.setBonus = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus.Wizard");
		}
		if (set == "MagicHat")
		{
			player.statManaMax2 -= 20;
			player.setBonus = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus.MagicHat");
		}
		switch (set)
		{
		case "CrystalAssassin":
			player.setBonus = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus.CrystalAssassin");
			modPlayer.DashID = string.Empty;
			modPlayer.rogueStealthMax += 0.9f;
			modPlayer.wearingRogueArmor = true;
			break;
		case "SpectreHealing":
			player.GetDamage<MagicDamageClass>() += 0.4f;
			player.setBonus = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus.SpectreHealing");
			break;
		case "SolarFlare":
			player.endurance -= 0.12f;
			if (player.solarShields > 0)
			{
				modPlayer.DashID = string.Empty;
			}
			break;
		}
	}

	public override void UpdateEquip(Item item, Player player)
	{
		switch (item.type)
		{
		case 2275:
			player.GetDamage<MagicDamageClass>() -= 0.06f;
			break;
		case 1282:
			player.manaCost += 0.01f;
			break;
		case 1283:
			player.statManaMax2 -= 20;
			player.manaCost += 0.02f;
			break;
		case 1284:
			player.manaCost += 0.03f;
			break;
		case 1285:
			player.statManaMax2 -= 20;
			player.manaCost += 0.04f;
			break;
		case 1286:
		case 4256:
			player.manaCost += 0.05f;
			break;
		case 1287:
			player.statManaMax2 -= 20;
			player.manaCost += 0.06f;
			break;
		case 2277:
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.1f;
			player.jumpSpeedBoost += 0.5f;
			break;
		case 1215:
			player.GetAttackSpeed<MeleeDamageClass>() += 0.05f;
			break;
		case 1549:
			player.GetDamage<RangedDamageClass>() -= 0.05f;
			player.GetCritChance<RangedDamageClass>() -= 5f;
			break;
		case 3871:
			player.GetDamage<MeleeDamageClass>() += 0.05f;
			player.GetDamage<SummonDamageClass>() += 0.05f;
			break;
		case 3872:
			player.GetDamage<SummonDamageClass>() -= 0.1f;
			break;
		case 3873:
			player.GetCritChance<MeleeDamageClass>() -= 5f;
			player.GetDamage<SummonDamageClass>() -= 0.05f;
			break;
		case 2763:
			player.GetCritChance<MeleeDamageClass>() -= 6f;
			break;
		case 2757:
			player.GetDamage<RangedDamageClass>() -= 0.06f;
			player.GetCritChance<RangedDamageClass>() -= 2f;
			break;
		}
	}

	public override void UpdateAccessory(Item item, Player player, bool hideVisual)
	{
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		if (item.type == 1613)
		{
			player.buffImmune[194] = true;
		}
		if (item.type == 4822)
		{
			modPlayer.flameWakerBoots = true;
			if (modPlayer.bootLevel < 1)
			{
				modPlayer.bootLevel = 1;
			}
		}
		if (item.type == 4874)
		{
			modPlayer.hellfireTreads = true;
			if (modPlayer.bootLevel < 2)
			{
				modPlayer.bootLevel = 2;
			}
			player.buffImmune[24] = true;
		}
		if (item.type == 3993)
		{
			modPlayer.fairyBoots = true;
		}
		if (item.type == 4000)
		{
			player.manaCost -= 0.02f;
		}
		if (item.type == 3991 || item.type == 4001)
		{
			player.manaCost -= 0.04f;
		}
		if (item.type == 3991)
		{
			player.GetDamage<MagicDamageClass>() += 0.05f;
		}
		if (item.type == 1248)
		{
			player.Calamity().critDamage += 0.15f;
		}
		if (item.type == 1858)
		{
			player.GetDamage<RangedDamageClass>() -= 0.1f;
			player.GetCritChance<RangedDamageClass>() += 2f;
			player.Calamity().critDamage += 0.15f;
		}
		if (item.type == 1343)
		{
			player.GetDamage<MeleeDamageClass>() += 0.02f;
		}
		if (item.type == 1343 || item.type == 1322)
		{
			modPlayer.magmaStoneVisuals = !hideVisual;
		}
		if (item.type == 3990)
		{
			player.jumpSpeedBoost += BalancingConstants.AmphibianBootsJumpSpeedBoost - 1.6f;
		}
		if (item.type == 211)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.12f;
			if (modPlayer.gloveLevel < 1)
			{
				modPlayer.gloveLevel = 1;
			}
		}
		if (item.type == 897)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.12f;
			if (modPlayer.gloveLevel < 2)
			{
				modPlayer.gloveLevel = 2;
			}
		}
		if (item.type == 3992)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.12f;
		}
		if (item.type == 936)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.12f;
			if (modPlayer.gloveLevel < 3)
			{
				modPlayer.gloveLevel = 3;
			}
		}
		if (item.type == 1343)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.12f;
			if (modPlayer.gloveLevel < 4)
			{
				modPlayer.gloveLevel = 4;
			}
		}
		if (modPlayer.eGauntlet && modPlayer.gloveLevel < 5)
		{
			modPlayer.gloveLevel = 5;
		}
		if (item.type == 899 && Main.dayTime)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.1f;
		}
		if (item.type == 900 && (!Main.dayTime || Main.eclipse))
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.1f;
		}
		if (item.type == 1865 || item.type == 3110)
		{
			player.GetAttackSpeed<MeleeDamageClass>() -= 0.1f;
		}
		if (item.type == 1131)
		{
			player.GetJumpState<GravityJump>().Enable();
			if (player.Calamity().justChangedGravity)
			{
				player.GetJumpState<GravityJump>().Available = true;
			}
			if (player.wingsLogic <= 0 && player.velocity.Y != 0f && player.maxRunSpeed < 8f)
			{
				player.maxRunSpeed = 5f;
			}
			player.jumpSpeedBoost += 1.6f;
			if (player.controlDown)
			{
				player.maxFallSpeed *= 1.5f;
			}
			else
			{
				player.maxFallSpeed *= 1.2f;
			}
		}
		if (item.type == 492 && !player.mount.Active)
		{
			player.maxFallSpeed *= 1.3f;
		}
		if (item.type == 1515 && !player.mount.Active && !player.controlDown)
		{
			player.gravity *= 0.6f;
			player.maxFallSpeed *= 0.6f;
		}
		if (item.type == 2494)
		{
			player.ignoreWater = true;
		}
		else if (item.type == 1871)
		{
			if (modPlayer.wingProjectileCooldown <= 0)
			{
				IEntitySource source = player.GetSource_Accessory(item);
				if (player.controlJump && player.jump == 0 && player.velocity.Y != 0f && player.wingTime > 0f && !player.mount.Active && !player.mount.Cart)
				{
					Vector2 ornamentPos = player.Center + Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(105f)) * Main.rand.NextFloat(-512f, -320f);
					if (Projectile.NewProjectile(source, ornamentPos, Vector2.Zero, ModContent.ProjectileType<FestiveWingsOrnament>(), 0, 0f, player.whoAmI).WithinBounds(Main.maxProjectiles))
					{
						modPlayer.wingProjectileCooldown = 90;
					}
				}
			}
		}
		else if (item.type == 1797 && modPlayer.wingProjectileCooldown <= 0)
		{
			IEntitySource source2 = player.GetSource_Accessory(item);
			if (player.controlJump && player.jump == 0 && player.velocity.Y != 0f && player.wingTime > 0f && !player.mount.Active && !player.mount.Cart)
			{
				Vector2 fairyDustVel = Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(0.08f, 0.2f);
				if (Projectile.NewProjectile(source2, player.Center, fairyDustVel, ModContent.ProjectileType<TatteredFairyDust>(), 0, 0f, player.whoAmI).WithinBounds(Main.maxProjectiles))
				{
					modPlayer.wingProjectileCooldown = 8;
				}
			}
		}
		if (item.type == 1303 || item.type == 1860 || item.type == 1861)
		{
			modPlayer.jellyfishNecklace = true;
		}
		if (item.type == 3016 || item.type == 3992 || item.type == 3998)
		{
			modPlayer.fleshKnuckles = true;
		}
		if (item.type == 3090)
		{
			modPlayer.royalGel = true;
		}
		if (item.type == 1921)
		{
			modPlayer.handWarmer = true;
		}
		if (item.type == 3097 || item.type == 977 || item.type == 984)
		{
			modPlayer.DashID = string.Empty;
		}
	}

	public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
	{
		CalamityPlayer modPlayer = player.Calamity();
		float moveSpeedBoost = modPlayer.moveSpeedBonus * 0.06f;
		float flightSpeedMult = 1f + (modPlayer.soaring ? SoaringPotion.FlightBoost : 0f) + (modPlayer.reaverSpeed ? ReaverHeadMobility.SetBonusFlightBoost : 0f) + moveSpeedBoost;
		float flightAccMult = 1f + moveSpeedBoost;
		flightSpeedMult = MathHelper.Clamp(flightSpeedMult, 0.5f, 1.5f);
		speed *= flightSpeedMult;
		flightAccMult = MathHelper.Clamp(flightAccMult, 0.5f, 1.5f);
		acceleration *= flightAccMult;
	}

	public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		switch (item.type)
		{
		case 493:
			maxAscentMultiplier *= 1.3f;
			constantAscend *= 1.5f;
			break;
		case 492:
			ascentWhenFalling *= 2f;
			ascentWhenRising *= 2f;
			maxCanAscendMultiplier *= 2f;
			break;
		case 821:
			maxAscentMultiplier *= 1.2f;
			constantAscend *= 1.35f;
			break;
		case 749:
			maxAscentMultiplier *= 0.9f;
			constantAscend *= 5f;
			break;
		case 823:
			maxAscentMultiplier *= 0.904f;
			constantAscend *= 5f;
			break;
		}
	}

	public override void GrabRange(Item item, Player player, ref int grabRange)
	{
		if (item.TryGetGlobalItem<GrabRangeGlobalItem>(out var grabRangeItem) && grabRangeItem.grabRangeMultiplier > 1f)
		{
			grabRange = (int)(grabRangeItem.grabRangeMultiplier * (float)grabRange);
		}
		if (player.Calamity().reaverExplore)
		{
			grabRange += ReaverHeadExplore.SetBonusGrabRangeBoost;
		}
		if (player.wingsLogic == 31 && player.wingTime > 0f && player.controlJump && player.TryingToHoverDown && ItemID.Sets.NebulaPickup[item.type])
		{
			grabRange *= 3;
		}
	}

	public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
	{
		return Main.rand.NextFloat() <= player.Calamity().ammoCost;
	}

	public static bool HasEnoughAmmo(Player player, Item item, int ammoConsumed)
	{
		bool hasEnoughAmmo = false;
		bool canShoot = false;
		for (int i = 54; i < 58; i++)
		{
			if (player.inventory[i].ammo == item.useAmmo && (player.inventory[i].stack >= ammoConsumed || !player.inventory[i].consumable))
			{
				canShoot = true;
				hasEnoughAmmo = true;
				break;
			}
		}
		if (!hasEnoughAmmo)
		{
			for (int j = 0; j < 54; j++)
			{
				if (player.inventory[j].ammo == item.useAmmo && (player.inventory[j].stack >= ammoConsumed || !player.inventory[j].consumable))
				{
					canShoot = true;
					break;
				}
			}
		}
		return canShoot;
	}

	public static void ConsumeAdditionalAmmo(Player player, Item item, int ammoConsumed)
	{
		Item itemAmmo = new Item();
		bool hasEnoughAmmo = false;
		bool dontConsumeAmmo = false;
		for (int i = 54; i < 58; i++)
		{
			if (player.inventory[i].ammo == item.useAmmo && (player.inventory[i].stack >= ammoConsumed || !player.inventory[i].consumable))
			{
				itemAmmo = player.inventory[i];
				hasEnoughAmmo = true;
				break;
			}
		}
		if (!hasEnoughAmmo)
		{
			for (int j = 0; j < 54; j++)
			{
				if (player.inventory[j].ammo == item.useAmmo && (player.inventory[j].stack >= ammoConsumed || !player.inventory[j].consumable))
				{
					itemAmmo = player.inventory[j];
					break;
				}
			}
		}
		if (player.magicQuiver && (item.useAmmo == AmmoID.Arrow || item.useAmmo == AmmoID.Stake) && Main.rand.NextBool(5))
		{
			dontConsumeAmmo = true;
		}
		if (player.huntressAmmoCost90 && Main.rand.NextBool(10))
		{
			dontConsumeAmmo = true;
		}
		if (player.ammoBox && Main.rand.NextBool(5))
		{
			dontConsumeAmmo = true;
		}
		if (player.ammoPotion && Main.rand.NextBool(5))
		{
			dontConsumeAmmo = true;
		}
		if (player.ammoCost80 && Main.rand.NextBool(5))
		{
			dontConsumeAmmo = true;
		}
		if (player.chloroAmmoCost80 && Main.rand.NextBool(5))
		{
			dontConsumeAmmo = true;
		}
		if (player.ammoCost75 && Main.rand.NextBool(4))
		{
			dontConsumeAmmo = true;
		}
		if (Main.rand.NextFloat() > player.Calamity().ammoCost)
		{
			dontConsumeAmmo = true;
		}
		if (!dontConsumeAmmo && itemAmmo.consumable)
		{
			itemAmmo.stack -= ammoConsumed;
			if (itemAmmo.stack <= 0)
			{
				itemAmmo.active = false;
				itemAmmo.TurnToAir();
			}
		}
	}

	public override void PostUpdate(Item item)
	{
		if (CalamityItemSets.ItemForcedInsideWorld[item.type])
		{
			CalamityUtils.ForceItemIntoWorld(item);
		}
	}

	internal static void UpdateAllParticleSets()
	{
		EnchantmentEnergyParticles.Update();
	}

	public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D itemTexture = TextureAssets.Item[item.type].Value;
		Rectangle itemFrame = ((Main.itemAnimations[item.type] == null) ? itemTexture.Frame() : Main.itemAnimations[item.type].GetFrame(itemTexture));
		if (!EnchantmentManager.ItemUpgradeRelationship.ContainsKey(item.type) || !Main.LocalPlayer.InventoryHas(ModContent.ItemType<BrimstoneLocus>()))
		{
			return true;
		}
		float currentPower = 0f;
		int calamitasNPCIndex = NPC.FindFirstNPC(ModContent.NPCType<BrimstoneWitch>());
		if (calamitasNPCIndex != -1)
		{
			currentPower = Utils.GetLerpValue(11750f, 1000f, Main.LocalPlayer.Distance(Main.npc[calamitasNPCIndex].Center), clamped: true);
		}
		Vector2 particleDrawCenter = position + new Vector2(12f, 16f) * Main.inventoryScale - itemFrame.Size() * 0.25f;
		EnchantmentEnergyParticles.InterpolationSpeed = MathHelper.Lerp(0.035f, 0.1f, currentPower);
		EnchantmentEnergyParticles.DrawSet(particleDrawCenter + Main.screenPosition);
		spriteBatch.Draw(itemTexture, position, (Rectangle?)itemFrame, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnCreated(Item item, ItemCreationContext context)
	{
		Player p = Main.LocalPlayer;
		if (cachedForgeID < 0)
		{
			cachedForgeID = ModContent.TileType<DraedonsForge>();
		}
		if (context is RecipeItemCreationContext && p.adjTile[cachedForgeID])
		{
			p.Calamity().HasCraftedDraedonsForge = true;
		}
	}

	public static int GetBuyPrice(int rarity)
	{
		if (rarity >= 0 && rarity <= 11)
		{
			return RarityBuyPriceArray[rarity];
		}
		if (rarity == ModContent.RarityType<Turquoise>())
		{
			return RarityTurquoiseBuyPrice;
		}
		if (rarity == ModContent.RarityType<PureGreen>())
		{
			return RarityPureGreenBuyPrice;
		}
		if (rarity == ModContent.RarityType<CosmicPurple>())
		{
			return RarityDarkBlueBuyPrice;
		}
		if (rarity == ModContent.RarityType<BurnishedAuric>())
		{
			return RarityVioletBuyPrice;
		}
		if (rarity == ModContent.RarityType<HotPink>())
		{
			return RarityHotPinkBuyPrice;
		}
		if (rarity == ModContent.RarityType<CalamityRed>())
		{
			return RarityCalamityRedBuyPrice;
		}
		return 0;
	}

	public static int GetBuyPrice(Item item)
	{
		return GetBuyPrice(item.rare);
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		int firstTooltipIndex = -1;
		int lastTooltipIndex = -1;
		int standardTooltipCount = 0;
		for (int i = 0; i < tooltips.Count; i++)
		{
			if (tooltips[i].Name.StartsWith("Tooltip"))
			{
				if (firstTooltipIndex == -1)
				{
					firstTooltipIndex = i;
				}
				lastTooltipIndex = i;
				standardTooltipCount++;
			}
		}
		bool noStandardTooltips = false;
		string[] mainTooltipBackupInsertionPositions;
		if (firstTooltipIndex == -1)
		{
			noStandardTooltips = true;
			mainTooltipBackupInsertionPositions = MainTooltipBackupInsertionPositions;
			foreach (string lineName in mainTooltipBackupInsertionPositions)
			{
				int idx = tooltips.FindIndex((TooltipLine tooltipLine) => tooltipLine.Name == lineName);
				if (idx != -1)
				{
					firstTooltipIndex = (lastTooltipIndex = idx);
					break;
				}
			}
		}
		TooltipLine nameLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
		if (nameLine != null)
		{
			ApplyRarityColor(item, nameLine);
		}
		ModifyVanillaTooltips(item, tooltips);
		EnchantmentTooltips(item, tooltips);
		WhipAutomaticTooltips(item, tooltips, ref lastTooltipIndex);
		string[] rogueKey = new string[2]
		{
			CalamityUtils.GetTextValue("Misc.GFBRogueUppercase"),
			CalamityUtils.GetTextValue("Misc.GFBRogueLowercase")
		};
		string[] rougeKey = new string[2]
		{
			CalamityUtils.GetTextValue("Misc.GFBRougeUppercase"),
			CalamityUtils.GetTextValue("Misc.GFBRougeLowercase")
		};
		for (int n = 0; n < rogueKey.Length; n++)
		{
			if (Main.zenithWorld && rogueKey[n] != "")
			{
				tooltips.FindAndReplace(rogueKey[n], rougeKey[n]);
			}
		}
		if (item.type < ItemID.Count)
		{
			return;
		}
		CalamityGlobalItem calamityGlobalItem = item.Calamity();
		if (calamityGlobalItem != null && calamityGlobalItem.UsesCharge)
		{
			float displayedPercent = ChargeRatio * 100f;
			TooltipLine line = new TooltipLine(base.Mod, "CalamityMod:Charge", CalamityUtils.GetText("Misc.Charge").Format(displayedPercent.ToString("N1")));
			tooltips.Insert(++lastTooltipIndex, line);
		}
		if (item.ModItem is IHoldShiftTooltipItem holdShiftItem)
		{
			bool num = Main.keyState.PressingShift();
			if (num && firstTooltipIndex != -1)
			{
				TooltipLine holdShiftLine = new TooltipLine(text: item.ModItem.GetLocalizedValue(holdShiftItem.TooltipExtensionKey), mod: base.Mod, name: "CalamityMod:HoldShiftTooltip");
				if (holdShiftItem.TooltipExtensionColor.HasValue)
				{
					holdShiftLine.OverrideColor = holdShiftItem.TooltipExtensionColor;
				}
				if (holdShiftItem.HidesNormalTooltip && !noStandardTooltips)
				{
					tooltips.RemoveRange(firstTooltipIndex, standardTooltipCount);
					lastTooltipIndex -= standardTooltipCount;
				}
				tooltips.Insert(++lastTooltipIndex, holdShiftLine);
			}
			if (!num && holdShiftItem.ShowExtensionIndicator)
			{
				LocalizedText indicatorText = CalamityUtils.GetText(holdShiftItem.ExtensionIndicatorKey);
				TooltipLine indicator = new TooltipLine(base.Mod, "CalamityMod:HoldShiftExtensionIndicator", indicatorText.Value);
				if (holdShiftItem.ExtensionIndicatorColor.HasValue)
				{
					indicator.OverrideColor = holdShiftItem.ExtensionIndicatorColor;
				}
				tooltips.Insert(++lastTooltipIndex, indicator);
			}
			if (holdShiftItem.HasFlavorTooltip && holdShiftItem.FlavorTooltipKey != null)
			{
				string flavorText = item.ModItem.GetLocalizedValue(holdShiftItem.FlavorTooltipKey);
				TooltipLine flavorLine = new TooltipLine(base.Mod, "CalamityMod:FlavorTooltip", flavorText);
				if (holdShiftItem.FlavorTooltipColor.HasValue)
				{
					flavorLine.OverrideColor = holdShiftItem.FlavorTooltipColor;
				}
				tooltips.Insert(++lastTooltipIndex, flavorLine);
			}
		}
		int difficultyTooltipIndex = -1;
		mainTooltipBackupInsertionPositions = RevTooltipInsertionPositions;
		foreach (string lineName2 in mainTooltipBackupInsertionPositions)
		{
			int idx2 = tooltips.FindIndex((TooltipLine tooltipLine) => tooltipLine.Name == lineName2);
			if (idx2 != -1)
			{
				difficultyTooltipIndex = idx2;
				break;
			}
		}
		if (difficultyTooltipIndex == -1)
		{
			difficultyTooltipIndex = lastTooltipIndex;
		}
		if (revengeanceItem)
		{
			LocalizedText revText = CalamityUtils.GetText("UI.Revengeance");
			TooltipLine revLine = new TooltipLine(base.Mod, "CalamityMod:RevengeanceItem", revText.Value);
			tooltips.Insert(++difficultyTooltipIndex, revLine);
		}
		if (devItem)
		{
			string coloredText = CalamityUtils.ColorMessage(CalamityUtils.GetText("UI.DevItemTooltip").Value, CalamityUtils.DevItemColor);
			TooltipLine devLine = new TooltipLine(base.Mod, "CalamityMod:DevItem", coloredText);
			tooltips.Insert(++difficultyTooltipIndex, devLine);
		}
		else if (donorItem)
		{
			string coloredText2 = CalamityUtils.ColorMessage(CalamityUtils.GetText("UI.DonorItemTooltip").Value, CalamityUtils.DonatorItemColor);
			TooltipLine donorLine = new TooltipLine(base.Mod, "CalamityMod:DonorItem", coloredText2);
			tooltips.Insert(++difficultyTooltipIndex, donorLine);
		}
		HashSet<int> buffIdsInTooltip = new HashSet<int>();
		foreach (TooltipLine tooltip in tooltips)
		{
			foreach (TextSnippet item3 in ChatManager.ParseMessage(tooltip.Text, Color.White))
			{
				if (item3 is CalamityBuffTagHandler.Snippet buffSnippet)
				{
					buffIdsInTooltip.Add(buffSnippet.BuffId);
				}
			}
		}
		if (buffIdsInTooltip.Count <= 0)
		{
			return;
		}
		bool showTheTip = false;
		bool foundDebuff = false;
		foreach (int buffId in buffIdsInTooltip)
		{
			string tooltipKey = "";
			if (buffId < BuffID.Count)
			{
				tooltipKey = "Mods.Terraria.Buffs." + BuffID.Search.GetName(buffId) + ".ItemTooltip";
			}
			else
			{
				ModBuff modBuff = BuffLoader.GetBuff(buffId);
				tooltipKey = $"Mods.{modBuff.Mod.Name}.Buffs.{modBuff.Name}.ItemTooltip";
			}
			if (!Language.Exists(tooltipKey))
			{
				continue;
			}
			string text = Language.GetTextValue(tooltipKey);
			if (!string.IsNullOrWhiteSpace(text))
			{
				foundDebuff = true;
				if (!PlayerInput.Triggers.Current.SmartCursor)
				{
					showTheTip = true;
					break;
				}
				tooltips.Insert(++lastTooltipIndex, new TooltipLine(base.Mod, "CalamityMod:AltExpandTooltip" + buffId, $"[cbuff:{buffId}]\n{text}"));
			}
		}
		if (showTheTip)
		{
			string str = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus["SmartCursor"].First().ToString();
			tooltips.Insert(++lastTooltipIndex, new TooltipLine(base.Mod, "CalamityMod:AltExpandTooltip", CalamityUtils.GetTextValue("Misc.AltExpand").Replace("{0}", str)));
			tooltips[lastTooltipIndex].OverrideColor = new Color(170, 170, 170);
		}
		else
		{
			if (!foundDebuff)
			{
				return;
			}
			foreach (TooltipLine item2 in tooltips)
			{
				if (item2.Name.Contains("Tooltip") && !item2.Name.Contains("AltExpandTooltip"))
				{
					item2.Hide();
				}
			}
		}
	}

	private static void ApplyRarityColor(Item item, TooltipLine nameLine)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		if (item.type == ModContent.ItemType<TheCommunity>())
		{
			nameLine.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
		if (item.type == ModContent.ItemType<Sylvestaff>())
		{
			nameLine.OverrideColor = new Color(249, 197, 255);
		}
		if (item.type == ModContent.ItemType<StaffofBlushie>())
		{
			nameLine.OverrideColor = new Color(0, 0, 255);
		}
		if (item.type == ModContent.ItemType<TheDanceofLight>())
		{
			nameLine.OverrideColor = TheDanceofLight.GetSyncedLightColor();
		}
		if (item.type == ModContent.ItemType<NanoblackReaper>())
		{
			nameLine.OverrideColor = new Color(0.34f, 0.34f + 0.66f * (float)Main.DiscoG / 255f, 0.34f + 0.5f * (float)Main.DiscoG / 255f);
		}
		if (item.type == ModContent.ItemType<ShatteredCommunity>())
		{
			nameLine.OverrideColor = ShatteredCommunity.GetRarityColor();
		}
		if (item.type == ModContent.ItemType<Ozzathoth>())
		{
			nameLine.OverrideColor = ShatteredCommunity.GetRarityColor();
		}
		if (item.type == ModContent.ItemType<ProfanedSoulCrystal>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(255, 166, 0), new Color(25, 250, 25), 6f);
		}
		if (item.type == ModContent.ItemType<TemporalUmbrella>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(210, 0, 255), new Color(255, 248, 24), 4f);
		}
		if (item.type == ModContent.ItemType<Endogenesis>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(131, 239, 255), new Color(36, 55, 230), 4f);
		}
		if (item.type == ModContent.ItemType<DraconicDestruction>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(255, 69, 0), new Color(139, 0, 0), 4f);
		}
		if (item.type == ModContent.ItemType<ScarletDevil>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(191, 45, 71), new Color(185, 187, 253), 4f);
		}
		if (item.type == ModContent.ItemType<RedSun>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(204, 86, 80), new Color(237, 69, 141), 4f);
		}
		if (item.type == ModContent.ItemType<CrystylCrusher>())
		{
			nameLine.OverrideColor = new Color(129, 29, 149);
		}
		if (item.type == ModContent.ItemType<SomaPrime>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(255, 255, 255), new Color(209, 204, 111), 4f);
		}
		if (item.type == ModContent.ItemType<Svantechnical>())
		{
			nameLine.OverrideColor = new Color(220, 20, 60);
		}
		if (item.type == ModContent.ItemType<Contagion>())
		{
			nameLine.OverrideColor = new Color(207, 17, 117);
		}
		if (item.type == ModContent.ItemType<TriactisTruePaladinianMageHammerofMight>())
		{
			nameLine.OverrideColor = new Color(227, 226, 180);
		}
		if (item.type == ModContent.ItemType<IllustriousKnives>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(154, 255, 151), new Color(228, 151, 255), 4f);
		}
		if (item.type == ModContent.ItemType<DemonshadeHelm>() || item.type == ModContent.ItemType<DemonshadeBreastplate>() || item.type == ModContent.ItemType<DemonshadeGreaves>())
		{
			nameLine.OverrideColor = CalamityUtils.ColorSwap(new Color(255, 132, 22), new Color(221, 85, 7), 4f);
		}
		if (item.type == ModContent.ItemType<AngelicAlliance>())
		{
			nameLine.OverrideColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly / 2f % 1f, new Color(255, 196, 55), new Color(255, 231, 107), new Color(255, 254, 243));
		}
		if (item.type == ModContent.ItemType<Eternity>())
		{
			List<Color> colorSet = new List<Color>
			{
				new Color(188, 192, 193),
				new Color(157, 100, 183),
				new Color(249, 166, 77),
				new Color(255, 105, 234),
				new Color(67, 204, 219),
				new Color(249, 245, 99),
				new Color(236, 168, 247)
			};
			if (nameLine != null)
			{
				int colorIndex = (int)(Main.GlobalTimeWrappedHourly / 2f % (float)colorSet.Count);
				Color currentColor = colorSet[colorIndex];
				Color nextColor = colorSet[(colorIndex + 1) % colorSet.Count];
				nameLine.OverrideColor = Color.Lerp(currentColor, nextColor, (Main.GlobalTimeWrappedHourly % 2f > 1f) ? 1f : (Main.GlobalTimeWrappedHourly % 1f));
			}
		}
		if (item.type == ModContent.ItemType<FlamsteedRing>())
		{
			if (Main.GlobalTimeWrappedHourly % 1f < 0.6f)
			{
				nameLine.OverrideColor = new Color(89, 229, 255);
			}
			else if (Main.GlobalTimeWrappedHourly % 1f < 0.8f)
			{
				nameLine.OverrideColor = Color.Lerp(new Color(89, 229, 255), Color.White, (Main.GlobalTimeWrappedHourly % 1f - 0.6f) / 0.2f);
			}
			else
			{
				nameLine.OverrideColor = Color.Lerp(Color.White, new Color(89, 229, 255), (Main.GlobalTimeWrappedHourly % 1f - 0.8f) / 0.2f);
			}
		}
		if (item.type == ModContent.ItemType<Earth>())
		{
			List<Color> earthColors = new List<Color>
			{
				Color.OrangeRed,
				Color.MediumTurquoise,
				Color.LimeGreen
			};
			if (nameLine != null)
			{
				int colorIndex2 = (int)(Main.GlobalTimeWrappedHourly / 2f % (float)earthColors.Count);
				Color currentColor2 = earthColors[colorIndex2];
				Color nextColor2 = earthColors[(colorIndex2 + 1) % earthColors.Count];
				nameLine.OverrideColor = Color.Lerp(currentColor2, nextColor2, (Main.GlobalTimeWrappedHourly % 2f > 1f) ? 1f : (Main.GlobalTimeWrappedHourly % 1f));
			}
		}
	}

	private void EnchantmentTooltips(Item item, IList<TooltipLine> tooltips)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!item.IsAir && AppliedEnchantment.HasValue)
		{
			string[] array = AppliedEnchantment.Value.Description.ToString().Split('\n');
			foreach (string line in array)
			{
				TooltipLine descriptionLine = new TooltipLine(base.Mod, "Enchantment", CalamityUtils.ColorMessage(line, CalamityUtils.DonatorItemColor));
				tooltips.Add(descriptionLine);
			}
		}
	}

	private void WhipAutomaticTooltips(Item item, IList<TooltipLine> tooltips, ref int lastTooltipIndex)
	{
		Dictionary<int, SummonTag> TagByItem = new Dictionary<int, SummonTag>();
		foreach (SummonTag tag1 in CalamityBuffSets.SummonTagDebuff.Values)
		{
			if (tag1.TagItem > -1)
			{
				TagByItem.Add(tag1.TagItem, tag1);
			}
		}
		if (TagByItem.TryGetValue(item.type, out var tag2) && tag2.AutoDrawTooltip)
		{
			CalamityPlayer modPlayer = Main.LocalPlayer.Calamity();
			if (tag2.FlatTagDamage != 0)
			{
				TooltipLine line = new TooltipLine(base.Mod, "CalamityMod:FlatSummonTag", FlatTagTooltip(tag2.FlatTagDamage));
				tooltips.Insert(++lastTooltipIndex, line);
			}
			if (!modPlayer.forceSummonTagCrit && (tag2.MultiplicativeTagDamage != 0f || (modPlayer.forceSummonTagMultiplicative && tag2.TagCritChance != 0f)))
			{
				TooltipLine line2 = new TooltipLine(base.Mod, "CalamityMod:MultiplicativeSummonTag", MultTagTooltip(tag2.MultiplicativeTagDamage + (modPlayer.forceSummonTagMultiplicative ? tag2.TagCritChance : 0f)));
				tooltips.Insert(++lastTooltipIndex, line2);
			}
			if (!modPlayer.forceSummonTagMultiplicative && (tag2.TagCritChance != 0f || (modPlayer.forceSummonTagCrit && tag2.MultiplicativeTagDamage != 0f)))
			{
				TooltipLine line3 = new TooltipLine(base.Mod, "CalamityMod:CritSummonTag", CritTagTooltip(tag2.TagCritChance + (modPlayer.forceSummonTagCrit ? tag2.MultiplicativeTagDamage : 0f)));
				tooltips.Insert(++lastTooltipIndex, line3);
			}
			if (modPlayer.forceSummonTagMultiplicative && modPlayer.forceSummonTagCrit)
			{
				TooltipLine line4 = new TooltipLine(base.Mod, "CalamityMod:CritSummonTag", ((int)(Main.GlobalTimeWrappedHourly / 5f) % 2 == 0) ? CritTagTooltip(tag2.MultiplicativeTagDamage + tag2.TagCritChance) : MultTagTooltip(tag2.MultiplicativeTagDamage + tag2.TagCritChance));
				tooltips.Insert(++lastTooltipIndex, line4);
			}
		}
		static string CritTagTooltip(float crit)
		{
			return CalamityUtils.GetText("Common.SummonTagCrit").Format((crit * 100f).ToString("0.#"));
		}
		static string FlatTagTooltip(int dmg)
		{
			return CalamityUtils.GetText("Common.SummonTagDamageFlat").Format(dmg.ToString());
		}
		static string MultTagTooltip(float mult)
		{
			return CalamityUtils.GetText("Common.SummonTagDamageMult").Format((mult + 1f).ToString("0.##"));
		}
	}

	private static void ModifyVanillaTooltips(Item item, IList<TooltipLine> tooltips)
	{
		if (item.type == 966 || item.type == 3046 || item.type == 3047 || item.type == 3048 || item.type == 3049 || item.type == 3050 || item.type == 3723 || item.type == 3724 || item.type == 4689 || item.type == 4690 || item.type == 4691 || item.type == 4692 || item.type == 4693 || item.type == 4694 || item.type == 5299 || item.type == 5357)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("Campfires");
			});
		}
		if (item.type == 1859)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("HeartLantern");
			});
		}
		if (item.type == 1134)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("BottledHoney");
			});
		}
		if (item.type == 3337)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("ShinyStone");
			});
		}
		if (item.type == 49)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("BandofRegeneration");
			});
		}
		if (item.type == 860)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("CharmofMyths");
			});
		}
		if (item.type == 289)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("RegenerationPotion");
			});
		}
		if (item.type == 3006)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("SoulDrain");
			});
		}
		if (item.type == 5096)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("HamBat");
			});
		}
		if (item.type == 5337)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("AegisCrystal");
			});
		}
		if (item.type == 3800)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SquireGreatHelm");
			});
		}
		if (item.type == 3872)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SquireAltShirt");
			});
		}
		if (item.type == 2763 || item.type == 2764 || item.type == 2765)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SolarFlarePieces");
			});
		}
		if (item.type == 1303 || item.type == 88 || item.type == 4008)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssGlow");
			});
		}
		if (item.type == 1860)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssGlow");
			});
		}
		if (item.type == 298)
		{
			EditTooltipByName("BuffTime", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssGlow");
			});
		}
		if (item.type == 268)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssBreathLevel2");
			});
		}
		if (item.type == 1861)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssLightLevel") + "\n" + CalamityUtils.GetTextValue("Common.AbyssBreathLevel2");
			});
		}
		if (item.type == 291)
		{
			EditTooltipByName("BuffTime", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssBreathLevel3");
			});
		}
		if (item.type == 497 || item.type == 861)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssBreathLevel3");
			});
		}
		if (item.type == 3110)
		{
			EditTooltipByNum(4, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.AbyssBreathLevel3");
			});
		}
		if (item.type == 1133)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("Abeemination");
			});
		}
		if (item.type == 1331)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("BloodySpine");
			});
		}
		if (item.type == 1307)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("ClothierVoodooDoll");
			});
		}
		if (item.type == 5120)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("DeerThing");
			});
		}
		if (item.type == 267)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("GuideVoodooDoll");
			});
		}
		if (item.type == 1293)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("LihzahrdPowerCell");
			});
		}
		if (item.type == 544)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MechanicalEye");
			});
		}
		if (item.type == 557)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MechanicalSkull");
			});
		}
		if (item.type == 556)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MechanicalWorm");
			});
		}
		if (item.type == 4988)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("QueenSlimeCrystal");
			});
		}
		if (item.type == 43)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SuspiciousLookingEye");
			});
		}
		if (item.type == 2673)
		{
			EditTooltipByName("Consumable", delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("TruffleWorm");
			});
		}
		if (item.type == 70)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("WormFood");
			});
		}
		if (item.type == 560 || item.type == 43 || item.type == 70 || item.type == 1331 || item.type == 1133 || item.type == 5120 || item.type == 4988 || item.type == 544 || item.type == 556 || item.type == 557 || item.type == 3601)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.NotConsumable");
			});
		}
		if (item.type == 963)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetTextValue("Common.DodgeProvided") + "\n" + CalamityUtils.GetTextValue("Common.DodgeInformation");
			});
		}
		if (item.type == 984)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetTextValue("Common.DodgeProvided") + "\n" + CalamityUtils.GetTextValue("Common.DodgeInformation");
			});
		}
		if (item.type == 3223)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetTextValue("Common.DodgeProvided");
			});
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.DodgeInformation");
			});
		}
		if (item.type == 4672)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4913)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 5074)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4911)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4678)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4679)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4914)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = string.Empty;
			});
		}
		if (item.type == 4056)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("25%", "15%");
			});
		}
		if (item.type == 5126)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("25%", "15%");
			});
		}
		if (item.type == 900)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SunMoonStones");
			});
		}
		if (item.type == 899)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SunMoonStones");
			});
		}
		if (item.type == 1865)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("CelestialStoneShell");
			});
		}
		if (item.type == 3110)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("CelestialStoneShell");
			});
		}
		if (item.type == 211)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("12%", "10%");
			});
		}
		if (item.type == 536)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("TitanGloveLine");
			});
		}
		if (item.type == 897)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("PowerGlove");
			});
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("TitanGloveLine");
			});
		}
		if (item.type == 3992)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("BerserkerGlove");
			});
		}
		if (item.type == 936)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MechanicalGlove") + AddedTooltip("TitanGloveLine");
			});
		}
		if (item.type == 1343)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FireGauntlet1");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FireGauntlet2") + AddedTooltip("TitanGloveLine");
			});
		}
		if (item.type == 3366 || item.type == 3334)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("YoyoGlove");
			});
		}
		if (item.type == 4002)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MoltenQuiver");
			});
		}
		if (item.type == 1248)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("EyeoftheGolem");
			});
		}
		if (item.type == 1300)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("RifleScope1");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("RifleScope2");
			});
		}
		if (item.type == 4005)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("RifleScope");
			});
		}
		if (item.type == 1858)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("SniperScope");
			});
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("RifleScope");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("EyeoftheGolem");
			});
		}
		if (item.type == 4000)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("8%", "10%");
			});
		}
		if (item.type == 3991 || item.type == 4001)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("8%", "12%");
			});
		}
		if (item.type == 3991)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("ArcaneFlower");
			});
		}
		if (item.type == 5107)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("Magiluminescence");
			});
		}
		if (item.type == 2423)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = GetEditedTooltip("FrogLeg").Format(1.6f.ToJumpSpeedPercent());
			});
		}
		if (item.type == 3994 || item.type == 3996)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = GetEditedTooltip("FrogLeg").Format(1.6f.ToJumpSpeedPercent());
			});
		}
		if (item.type == 3995)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text = GetEditedTooltip("FrogLeg").Format(1.6f.ToJumpSpeedPercent());
			});
		}
		if (item.type == 3990)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = GetEditedTooltip("FrogLeg").Format(BalancingConstants.AmphibianBootsJumpSpeedBoost.ToJumpSpeedPercent());
			});
		}
		if (item.type == 4989)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("EmpressFlightBooster1");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("EmpressFlightBooster2");
			});
		}
		if (item.type == 1131)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("GravityGlobe");
			});
		}
		if (item.type == 4822)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlameWakerBoots");
			});
		}
		if (item.type == 4874)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("HellfireTreads");
			});
		}
		if (item.type == 3993)
		{
			EditTooltipByNum(2, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("FairyBoots");
			});
		}
		if (item.type == 1613)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("AnkhShield");
			});
		}
		if (item.type == 3016 || item.type == 3998 || item.type == 3992)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("FleshKnucklesLine");
			});
		}
		if (item.type == 1921)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("HandWarmer");
			});
		}
		if (item.type == 3187)
		{
			EditTooltipByName("Defense", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetText("Common.RogueDamage").Format(3);
			});
		}
		if (item.type == 3188)
		{
			EditTooltipByName("Defense", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetText("Common.RogueCrit").Format(3);
			});
		}
		if (item.type == 3189)
		{
			EditTooltipByName("Defense", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetText("Common.RogueVelocity").Format(3);
			});
		}
		if (item.type == 228 || item.type == 960)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("40", "20");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("6%", "3%");
			});
		}
		if (item.type == 230 || item.type == 962)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("6%", "3%");
			});
		}
		if (item.type == 792 || item.type == 793 || item.type == 794)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("3%", "6%");
			});
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("CrimsonArmorPieces");
			});
		}
		if (item.type == 2275)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MagicHat");
			});
		}
		if (item.type == 1282)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("5%", "4%");
			});
		}
		if (item.type == 1283)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("40", "20");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("7%", "5%");
			});
		}
		if (item.type == 1284)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("9%", "6%");
			});
		}
		if (item.type == 1285)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("60", "40");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("11%", "7%");
			});
		}
		if (item.type == 1286 || item.type == 4256)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("13%", "8%");
			});
		}
		if (item.type == 1287)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("80", "60");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("15%", "9%");
			});
		}
		if (item.type == 2277)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("Gi");
			});
		}
		if (item.type == 371)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("40", $"{60}");
			});
		}
		if (item.type == 1208)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("3%", $"{5}%");
			});
		}
		if (item.type == 1209)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("2%", $"{5}%");
			});
		}
		if (item.type == 376)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("60", $"{80}");
			});
		}
		if (item.type == 1213)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("6%", $"{10}%");
			});
		}
		if (item.type == 400)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("80", $"{100}");
			});
		}
		if (item.type == 1215)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("9%", "14%");
			});
		}
		if (item.type == 1549)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("13%", "8%");
			});
		}
		if (item.type == 2763)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("26%", "20%");
			});
		}
		if (item.type == 2757)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("16%", "10%");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("7%", "5%");
			});
		}
		if (item.type == 3871)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("10%", "15%");
			});
		}
		if (item.type == 3872)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("30%", "20%");
			});
		}
		if (item.type == 3873)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("20%", "15%");
			});
		}
		if (item.type == 3806)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkBrows");
			});
		}
		if (item.type == 3807)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkShirt");
			});
		}
		if (item.type == 3808)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkPants");
			});
		}
		if (item.type == 3880)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkAltHead");
			});
		}
		if (item.type == 3881)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkAltShirt0");
			});
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkAltShirt1");
			});
		}
		if (item.type == 3882)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MonkAltPants");
			});
		}
		EditTooltipByName("SetBonus", delegate(TooltipLine line)
		{
			VanillaArmorChangeManager.ApplySetBonusTooltipChanges(item, ref line.Text);
		});
		if ((item.type == 3776 || item.type == 3777 || item.type == 3778) && !Main.LocalPlayer.Calamity().forbiddenCirclet)
		{
			EditTooltipByName("SetBonus", delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetText("Vanilla.Armor.SetBonus.Forbidden").Format(CalamityUtils.GetArmorSetBonusKey());
			});
		}
		if (item.type == 2757 || item.type == 2758 || item.type == 2759)
		{
			EditTooltipByName("SetBonus", delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetText("Vanilla.Armor.SetBonus.Vortex").Format(CalamityUtils.GetArmorSetBonusKey());
			});
		}
		if (item.type == 303)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("ArcheryPotion");
			});
		}
		if (item.type == 290)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("25%", "15%");
			});
		}
		if (item.type == 294)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("20%", "10%");
			});
		}
		if (item.type == 2322)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("25%", "15%");
			});
		}
		if (item.type == 353 || item.type == 2266)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("AleSake");
			});
		}
		if (item.type == 295)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("FeatherfallPotion");
			});
		}
		if (item.type == 1353)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofCursedFlames");
			});
		}
		if (item.type == 1354)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofFire");
			});
		}
		if (item.type == 1355)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofGold");
			});
		}
		if (item.type == 1356)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofIchor");
			});
		}
		if (item.type == 1357)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofNanites");
			});
		}
		if (item.type == 1358)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofParty");
			});
		}
		if (item.type == 1359)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofPoison");
			});
		}
		if (item.type == 1340)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("FlaskofVenom");
			});
		}
		if (item.type == 3289)
		{
			AddYoyoStats(-1f, 432f, 28f);
		}
		if (item.type == 3282)
		{
			AddYoyoStats(30f, 384f, 28f);
		}
		if (item.type == 3283)
		{
			AddYoyoStats(-1f, 400f, 32f);
		}
		if (item.type == 3262)
		{
			AddYoyoStats(21f, 320f, 25f);
		}
		if (item.type == 3284)
		{
			AddYoyoStats(-1f, 432f, 42f);
		}
		if (item.type == 3279)
		{
			AddYoyoStats(18f, 288f, 22f);
		}
		if (item.type == 3280)
		{
			AddYoyoStats(18f, 288f, 22f);
		}
		if (item.type == 3315)
		{
			AddYoyoStats(-1f, 384f, 36f);
		}
		if (item.type == 3316)
		{
			AddYoyoStats(-1f, 384f, 36f);
		}
		if (item.type == 3290)
		{
			AddYoyoStats(-1f, 368f, 42f);
		}
		if (item.type == 5294)
		{
			AddYoyoStats(24f, 320f, 20f);
		}
		if (item.type == 3281)
		{
			AddYoyoStats(20f, 288f, 17f);
		}
		if (item.type == 3291)
		{
			AddYoyoStats(-1f, 480f, 54f);
		}
		if (item.type == 3285)
		{
			AddYoyoStats(16f, 272f, 20f);
		}
		if (item.type == 3287)
		{
			AddYoyoStats(-1f, 480f, 42f);
		}
		if (item.type == 3389)
		{
			AddYoyoStats(-1f, 512f, 54f);
		}
		if (item.type == 3292)
		{
			AddYoyoStats(-1f, 480f, 36f);
		}
		if (item.type == 3288)
		{
			AddYoyoStats(-1f, 480f, 42f);
		}
		if (item.type == 3317)
		{
			AddYoyoStats(30f, 400f, 36f);
		}
		if (item.type == 3278)
		{
			AddYoyoStats(15f, 240f, 14f);
		}
		if (item.type == 3286)
		{
			AddYoyoStats(-1f, 400f, 36f);
		}
		if (item.type == 4978)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 493)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.95f, 0.15f);
		}
		if (item.type == 492)
		{
			AddWingStats(item.wingSlot, 1f, 0.2f, 1f, 1.5f, 0.1f, "DemonWings");
		}
		if (item.type == 748)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 749)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.35f, 0.5f);
		}
		if (item.type == 761)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 1515)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f, "BeeWings");
		}
		if (item.type == 785)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 786)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.66f, 0.1f, "BoneWings");
		}
		if (item.type == 821)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.8f, 0.135f);
		}
		if (item.type == 822)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 823)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.5f);
		}
		if (item.type == 2280)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.66f, 0.1f);
		}
		if (item.type == 2494)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f, "FinWings");
		}
		if (item.type == 2609)
		{
			AddWingStats(item.wingSlot, 0.75f, 0.15f, 1f, 2.5f, 0.125f);
		}
		if (item.type == 948)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.805f, 0.1f);
		}
		if (item.type == 1162)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 1165)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 3580 || item.type == 3582 || item.type == 3592 || item.type == 3924 || item.type == 3928 || item.type == 665 || item.type == 1583 || item.type == 1584 || item.type == 1585 || item.type == 1586 || item.type == 4750 || item.type == 4754 || item.type == 4730 || item.type == 4746)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 3588 || item.type == 3228)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.5f, 0.1f);
		}
		if (item.type == 1797)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.805f, 0.1f, "TatteredFairyWings");
		}
		if (item.type == 1830)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.805f, 0.1f);
		}
		if (item.type == 1866)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.66f, 0.1f);
		}
		if (item.type == 1871)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.805f, 0.1f, "FestiveWings");
		}
		if (item.type == 2770)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 0.5f, 1.66f, 0.1f);
		}
		if (item.type == 3468)
		{
			AddWingStats(item.wingSlot, 0.85f, 0.15f, 1f, 3f, 0.135f, "WingsSolar");
		}
		if (item.type == 3471)
		{
			AddWingStats(item.wingSlot, 0.85f, 0.15f, 1f, 3f, 0.135f, "WingsStardust");
		}
		if (item.type == 3469)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 1f, 2.45f, 0.15f, "WingsVortex");
		}
		if (item.type == 3470)
		{
			AddWingStats(item.wingSlot, 0.5f, 0.1f, 1f, 2.45f, 0.15f, "WingsNebula");
		}
		if (item.type == 3883)
		{
			AddWingStats(item.wingSlot, 0.75f, 0.15f, 1f, 2.5f, 0.125f);
		}
		if (item.type == 4823)
		{
			AddWingStats(item.wingSlot, 0.85f, 0.15f, 1f, 2.5f, 0.125f);
		}
		if (item.type == 4954)
		{
			AddWingStats(item.wingSlot, 0.95f, 0.15f, 1f, 4.5f, 0.1f);
		}
		if (item.type == 84)
		{
			AddGrappleStats(18.75f, 11.5f, 11f, 11f);
		}
		if (item.type == 1236)
		{
			AddGrappleStats(18.75f, 10f, 11f, 11f);
		}
		if (item.type == 4759)
		{
			AddGrappleStats(19f, 11.5f, 11f, 11f);
		}
		if (item.type == 1237)
		{
			AddGrappleStats(20.625f, 10.5f, 11.75f, 11f);
		}
		if (item.type == 1238)
		{
			AddGrappleStats(22.5f, 11f, 12.5f, 11f);
		}
		if (item.type == 1239)
		{
			AddGrappleStats(24.375f, 11.5f, 13.25f, 11f);
		}
		if (item.type == 1240)
		{
			AddGrappleStats(26.25f, 12f, 14f, 11f);
		}
		if (item.type == 4257)
		{
			AddGrappleStats(27.5f, 12.5f, 15f, 11f);
		}
		if (item.type == 1241)
		{
			AddGrappleStats(29.125f, 12.5f, 14.75f, 11f);
		}
		if (item.type == 939)
		{
			AddGrappleStats(22.625f, 10f, 11f, 11f);
		}
		if (item.type == 1273)
		{
			AddGrappleStats(21.875f, 15f, 11f, 11f);
		}
		if (item.type == 2585)
		{
			AddGrappleStats(18.75f, 13f, 11f, 11f);
		}
		if (item.type == 2360)
		{
			AddGrappleStats(25f, 13f, 11f, 11f);
		}
		if (item.type == 185)
		{
			AddGrappleStats(25f, 13f, 15f, 11f);
		}
		if (item.type == 1800)
		{
			AddGrappleStats(31.25f, 13.5f, 20f, 13f);
		}
		if (item.type == 1915)
		{
			AddGrappleStats(25f, 11.5f, 11f, 11f);
		}
		if (item.type == 437)
		{
			AddGrappleStats(27.5f, 14f, 17f, 11f);
		}
		if (item.type == 4980)
		{
			AddGrappleStats(30f, 16f, 18f, 11f);
		}
		if (item.type == 3023 || item.type == 3020 || item.type == 3022)
		{
			AddGrappleStats(30f, 15f, 18f, 11f);
		}
		if (item.type == 3021)
		{
			AddGrappleStats(30f, 16f, 18f, 12f);
		}
		if (item.type == 2800)
		{
			AddGrappleStats(31.25f, 14f, 20f, 11f);
		}
		if (item.type == 1829)
		{
			AddGrappleStats(34.375f, 15.5f, 22f, 11f);
		}
		if (item.type == 1916)
		{
			AddGrappleStats(34.375f, 15.5f, 17f, 11f);
		}
		if (item.type == 3572)
		{
			AddGrappleStats(34.375f, 18f, 24f, 16f);
		}
		if (item.type == 3623)
		{
			AddGrappleStats(37.5f, 16f, 24f, 0f);
		}
		if (item.type == 313)
		{
			AddHerbTooltips("Daybloom");
		}
		if (item.type == 314)
		{
			AddHerbTooltips("Moonglow");
		}
		if (item.type == 317)
		{
			AddHerbTooltips("Waterleaf");
		}
		if (item.type == 315)
		{
			AddHerbTooltips("Blinkroot");
		}
		if (item.type == 2358)
		{
			AddHerbTooltips("Shiverthorn");
		}
		if (item.type == 316)
		{
			AddHerbTooltips("Deathweed");
		}
		if (item.type == 318)
		{
			AddHerbTooltips("Fireblossom");
		}
		if (item.type == 307)
		{
			AddSeedTooltips("Daybloom");
		}
		if (item.type == 308)
		{
			AddSeedTooltips("Moonglow");
		}
		if (item.type == 311)
		{
			AddSeedTooltips("Waterleaf");
		}
		if (item.type == 309)
		{
			AddSeedTooltips("Blinkroot");
		}
		if (item.type == 2357)
		{
			AddSeedTooltips("Shiverthorn");
		}
		if (item.type == 310)
		{
			AddSeedTooltips("Deathweed");
		}
		if (item.type == 312)
		{
			AddSeedTooltips("Fireblossom");
		}
		if (item.type == 3521 || item.type == 3485)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("GoldPickaxe");
			});
		}
		if (item.type == 1294)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("Picksaw");
			});
		}
		if (item.type == 2786 || item.type == 2776 || item.type == 2781 || item.type == 3466)
		{
			EditTooltipByName("Material", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.CanMineUelibloom");
			});
		}
		if (item.type == 2784 || item.type == 2774 || item.type == 2779 || item.type == 3464)
		{
			EditTooltipByName("TileBoost", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetTextValue("Common.CanMineUelibloom");
			});
		}
		if (item.type == ModContent.ItemType<Respiteblock>())
		{
			EditTooltipByName("AxePower", delegate(TooltipLine line)
			{
				line.Text = line.Text.Replace("610%", "612%");
			});
		}
		if (item.master && (item.type < ItemID.Count || item.ModItem?.Mod is CalamityMod))
		{
			EditTooltipByName("Master", delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("MasterExclusive");
			});
		}
		if (item.type == 2610)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("SlimeGun");
			});
		}
		if (item.type == 4986)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("GelBalloon");
			});
		}
		if (item.type == 3859)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("DD2BetsyBow");
			});
		}
		if (item.type == 1326)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("RodofDiscord");
			});
		}
		if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && (item.type == 367 || item.type == 787))
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("Pwnhammer");
			});
		}
		if (item.type == 2294)
		{
			EditTooltipByName("NeedsBait", delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("GoldenFishingRod");
			});
		}
		if (item.type == 321 || item.type == 1173 || item.type == 1174 || item.type == 1175 || item.type == 1176 || item.type == 1177 || item.type == 3229 || item.type == 3230 || item.type == 3231 || item.type == 3232 || item.type == 3233)
		{
			EditTooltipByName("Material", delegate(TooltipLine line)
			{
				line.Text += AddedTooltip("Tombstones");
			});
		}
		EditTooltipByName("Speed", delegate(TooltipLine line)
		{
			RedistributeSpeedTooltips(item, line);
		});
		if (item.healLife > 0 && Main.LocalPlayer.Calamity().healingPotionMultiplier != 1f)
		{
			EditTooltipByName("HealLife", delegate(TooltipLine line)
			{
				line.Text = Language.GetOrRegister("CommonItemTooltip.RestoresLife").Format((int)((float)item.healLife * Main.LocalPlayer.Calamity().healingPotionMultiplier));
			});
		}
		if (item.type == 3549)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text = EditedTooltip("LunarCraftingStation");
			});
		}
		if ((item.type == 3097 || item.type == 977) && CalamityKeybinds.DashHotkey.GetAssignedKeysOrEmpty().Count != 0)
		{
			EditTooltipByNum(1, delegate(TooltipLine line)
			{
				line.Text = CalamityUtils.GetText("Vanilla.DashKey").Format(CalamityKeybinds.DashHotkey.TooltipHotkeyString());
			});
		}
		static string AddedTooltip(string key)
		{
			return "\n" + CalamityUtils.GetTextValue("Vanilla.AddedTooltip." + key);
		}
		void AddGrappleStats(float r, float l, float e, float p)
		{
			EditTooltipByName("Equipable", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + CalamityUtils.GetText("Common.GrappleStats").Format(r.ToString(), l.ToString(), e.ToString(), p.ToString());
			});
		}
		void AddHerbTooltips(string key)
		{
			int materialIndex = 0;
			for (int i = 0; i < tooltips.Count; i++)
			{
				if (tooltips[i].Name == "Material")
				{
					materialIndex = i;
					break;
				}
			}
			tooltips.Insert(materialIndex + 1, new TooltipLine(CalamityMod.Instance, "Tooltip0", CalamityUtils.GetTextValue("Vanilla.HerbTooltips." + key)));
		}
		void AddSeedTooltips(string key)
		{
			int materialIndex = 0;
			for (int i = 0; i < tooltips.Count; i++)
			{
				if (tooltips[i].Name == "Placeable")
				{
					materialIndex = i;
					break;
				}
			}
			tooltips.Insert(materialIndex + 1, new TooltipLine(CalamityMod.Instance, "Tooltip0", CalamityUtils.GetTextValue("Vanilla.SeedTooltips." + key)));
		}
		void AddWingStats(int slot, float fall, float rise, float rMax, float tMax, float asc, string extraKey = null)
		{
			EditTooltipByNum(0, delegate(TooltipLine line)
			{
				line.Text += WingStatsTooltip(ArmorIDs.Wing.Sets.Stats[slot], fall, rise, rMax, tMax, asc, extraKey);
			});
		}
		void AddYoyoStats(float d, float r, float s)
		{
			EditTooltipByName("Knockback", delegate(TooltipLine line)
			{
				line.Text = line.Text + "\n" + ((d == -1f) ? CalamityUtils.GetText("Common.YoyoStatsInfinite").Format(r.ToTiles(), s.ToString()) : CalamityUtils.GetText("Common.YoyoStats").Format(r.ToTiles(), s.ToString(), d.ToString()));
			});
		}
		void ApplyTooltipEdits(IList<TooltipLine> lines, Func<Item, TooltipLine, bool> predicate, Action<TooltipLine> action)
		{
			foreach (TooltipLine line in lines)
			{
				if (predicate(item, line))
				{
					action(line);
				}
			}
		}
		static string EditedTooltip(string key)
		{
			return CalamityUtils.GetTextValue("Vanilla.EditedTooltip." + key);
		}
		void EditTooltipByName(string lineName, Action<TooltipLine> action)
		{
			ApplyTooltipEdits(tooltips, LineName(lineName), action);
		}
		void EditTooltipByNum(int lineNum, Action<TooltipLine> action)
		{
			ApplyTooltipEdits(tooltips, LineNum(lineNum), action);
		}
		static LocalizedText GetEditedTooltip(string key)
		{
			return CalamityUtils.GetText("Vanilla.EditedTooltip." + key);
		}
		static Func<Item, TooltipLine, bool> LineName(string s)
		{
			return (Item i, TooltipLine l) => l.Mod == "Terraria" && l.Name == s;
		}
		static Func<Item, TooltipLine, bool> LineNum(int n)
		{
			return (Item i, TooltipLine l) => l.Mod == "Terraria" && l.Name == $"Tooltip{n}";
		}
		static string WingStatsTooltip(WingStats stats, float fall, float rise, float rMax, float tMax, float asc, string extraKey = null)
		{
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			int time = stats.FlyTime;
			float run = stats.AccRunSpeedOverride;
			float rAcc = stats.AccRunAccelerationMult * 0.08f;
			bool hover = stats.HasDownHoverStats;
			float hSpeed = stats.DownHoverSpeedOverride;
			float hAcc = stats.DownHoverAccelerationMult * 0.08f;
			float baseJumpSpeed = (CalamityServerConfig.Instance.FasterJumpSpeed ? BalancingConstants.ConfigBoostedBaseJumpSpeed : 5.01f) + 1f;
			StringBuilder sb = new StringBuilder(512);
			sb.Append('\n');
			sb.Append(CalamityUtils.GetText("Common.WingStats").Format(time.FramesToSeconds(), run.ToMph(), (tMax * baseJumpSpeed).ToMph()));
			sb.Append('\n');
			if (Main.keyState.PressingShift())
			{
				sb.Append(CalamityUtils.GetText("Common.WingStatsAcceleration").Format(rAcc.ToMphps(), asc.ToMphps(), (asc + rise).ToMphps(), (rMax * baseJumpSpeed).ToMph(), (asc + fall).ToMphps()));
				if (hover)
				{
					sb.Append('\n');
					sb.Append(CalamityUtils.GetText("Common.WingStatsHover").Format(hSpeed.ToMph(), hAcc.ToMphps()));
				}
			}
			else
			{
				StringBuilder stringBuilder = sb;
				StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(11, 1, stringBuilder);
				handler.AppendLiteral("[c/B8B8B8:");
				handler.AppendFormatted(CalamityUtils.GetTextValue("UI.HoldShiftTooltipExtensionIndicator"));
				handler.AppendLiteral("]");
				stringBuilder.Append(ref handler);
			}
			if (extraKey != null)
			{
				sb.Append('\n');
				sb.Append(CalamityUtils.GetTextValue("Vanilla.Wings." + extraKey));
			}
			return sb.ToString();
		}
	}

	private static void RedistributeSpeedTooltips(Item item, TooltipLine line)
	{
		foreach (var (threshold, tooltip) in SpeedTooltips)
		{
			if (item.useAnimation <= threshold)
			{
				line.Text = tooltip.Value;
				break;
			}
		}
	}

	public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_100a: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1019: Unknown result type (might be due to invalid IL or missing references)
		//IL_1026: Unknown result type (might be due to invalid IL or missing references)
		//IL_102b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_1042: Unknown result type (might be due to invalid IL or missing references)
		//IL_1047: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_1088: Unknown result type (might be due to invalid IL or missing references)
		//IL_108d: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1105: Unknown result type (might be due to invalid IL or missing references)
		//IL_110a: Unknown result type (might be due to invalid IL or missing references)
		//IL_110c: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1110: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_1128: Unknown result type (might be due to invalid IL or missing references)
		//IL_112a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1133: Unknown result type (might be due to invalid IL or missing references)
		//IL_1138: Unknown result type (might be due to invalid IL or missing references)
		//IL_1146: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0973: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_119f: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1198: Unknown result type (might be due to invalid IL or missing references)
		//IL_1249: Unknown result type (might be due to invalid IL or missing references)
		//IL_124e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1255: Unknown result type (might be due to invalid IL or missing references)
		//IL_1242: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1401: Unknown result type (might be due to invalid IL or missing references)
		//IL_1406: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11de: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1208: Unknown result type (might be due to invalid IL or missing references)
		//IL_120d: Unknown result type (might be due to invalid IL or missing references)
		//IL_121b: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_126d: Unknown result type (might be due to invalid IL or missing references)
		//IL_126f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1278: Unknown result type (might be due to invalid IL or missing references)
		//IL_127d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1285: Unknown result type (might be due to invalid IL or missing references)
		//IL_16be: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1687: Unknown result type (might be due to invalid IL or missing references)
		//IL_1689: Unknown result type (might be due to invalid IL or missing references)
		//IL_1692: Unknown result type (might be due to invalid IL or missing references)
		//IL_1697: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1302: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_141f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_1429: Unknown result type (might be due to invalid IL or missing references)
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_1438: Unknown result type (might be due to invalid IL or missing references)
		//IL_143d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_144c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1451: Unknown result type (might be due to invalid IL or missing references)
		//IL_1327: Unknown result type (might be due to invalid IL or missing references)
		//IL_1336: Unknown result type (might be due to invalid IL or missing references)
		//IL_1349: Unknown result type (might be due to invalid IL or missing references)
		//IL_134e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1353: Unknown result type (might be due to invalid IL or missing references)
		//IL_1366: Unknown result type (might be due to invalid IL or missing references)
		//IL_1368: Unknown result type (might be due to invalid IL or missing references)
		//IL_1374: Unknown result type (might be due to invalid IL or missing references)
		//IL_1379: Unknown result type (might be due to invalid IL or missing references)
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1468: Unknown result type (might be due to invalid IL or missing references)
		//IL_146d: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1488: Unknown result type (might be due to invalid IL or missing references)
		//IL_148f: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1503: Unknown result type (might be due to invalid IL or missing references)
		//IL_1508: Unknown result type (might be due to invalid IL or missing references)
		//IL_1512: Unknown result type (might be due to invalid IL or missing references)
		//IL_1517: Unknown result type (might be due to invalid IL or missing references)
		//IL_151c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1605: Unknown result type (might be due to invalid IL or missing references)
		//IL_1611: Unknown result type (might be due to invalid IL or missing references)
		//IL_1616: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1641: Unknown result type (might be due to invalid IL or missing references)
		//IL_1643: Unknown result type (might be due to invalid IL or missing references)
		//IL_164f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1654: Unknown result type (might be due to invalid IL or missing references)
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1533: Unknown result type (might be due to invalid IL or missing references)
		//IL_1538: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Unknown result type (might be due to invalid IL or missing references)
		//IL_154e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1574: Unknown result type (might be due to invalid IL or missing references)
		//IL_157e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1597: Unknown result type (might be due to invalid IL or missing references)
		if (line.Name == "ItemName" && line.Mod == "Terraria" && item.type == ModContent.ItemType<XyksBlessingBlue>() && CalamityClientConfig.Instance.TextEffects)
		{
			Color rarityColor = Color.White;
			Vector2 basePosition = default(Vector2);
			((Vector2)(ref basePosition))._002Ector((float)line.X, (float)line.Y);
			float rate = Main.GlobalTimeWrappedHourly * 6f;
			List<Color> eColors = new List<Color>
			{
				Color.DodgerBlue,
				Color.Cyan,
				Color.RoyalBlue
			};
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color val2 = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
			Vector2 backScale = line.BaseScale;
			Color backColor = val2;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			int draws = 20;
			for (int i = 0; i < draws; i++)
			{
				Vector2 backPosition = basePosition + ((float)Math.PI * 2f * (float)i / 20f).ToRotationVector2() * 3.5f;
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, backPosition, backColor, line.Rotation, line.Origin, backScale, line.MaxWidth, line.Spread);
			}
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Texture2D square = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2).Value;
			Color drawColor = backColor;
			Vector2 rotationPoint = texture.Size() * 0.5f;
			int length = line.Text.Length;
			for (int j = 0; j < 8; j++)
			{
				Main.EntitySpriteDraw(texture, basePosition + Vector2.UnitX * (float)length * 4f + Vector2.UnitY * 23f + Vector2.UnitX * (float)((j % 2 == 0) ? (-10 * j) : (10 * j)), null, drawColor, (float)Math.PI / 2f, rotationPoint, new Vector2(0.3f - 0.02f * (float)j, 1f + 0.35f * (float)j) * 0.3f * Main.rand.NextFloat(0.9f, 1f), (SpriteEffects)0);
			}
			for (int k = 0; k < 10; k++)
			{
				Math.Sin(Main.GlobalTimeWrappedHourly * (0f + (float)k * 0.8f) / (float)Math.PI);
				float sine2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f / (float)Math.PI);
				float squareSine = (float)Math.Sin((float)k * 2.5f / (float)Math.PI);
				Math.Sin((float)Math.Pow(Utils.GetLerpValue(0f, 110f, (int)(Main.GlobalTimeWrappedHourly * (float)(60 + k * 2)) % 120), 5.0));
				Vector2 weirdPos = Vector2.UnitX * 53f * (float)k * Utils.GetLerpValue(20f, 0f, k, clamped: true) + Vector2.UnitY * -23f * squareSine * sine2 + new Vector2(5f, 10f);
				for (int t = 0; t < 3; t++)
				{
					Main.EntitySpriteDraw(square, basePosition + weirdPos, null, drawColor * (1f - 0.03f * (float)k), 0f, square.Size() * 0.5f, (1.2f - 0.07f * (float)t) * new Vector2(1f, 1f) * (0.25f * ((float)Math.Pow(Utils.GetLerpValue(11f, 0f, k, clamped: true), 3.0) + 0.2f)), (SpriteEffects)0);
				}
				for (int l = 0; l < 3; l++)
				{
					Main.EntitySpriteDraw(texture, basePosition + weirdPos, null, drawColor * (1f - 0.03f * (float)k), 0f, texture.Size() * 0.5f, (0.65f - 0.07f * (float)l) * new Vector2(1f, 1f) * (0.25f * ((float)Math.Pow(Utils.GetLerpValue(11f, 0f, k, clamped: true), 3.0) + 0.2f)), (SpriteEffects)0);
				}
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, basePosition, rarityColor, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		if (line.Name == "ItemName" && line.Mod == "Terraria" && item.type == ModContent.ItemType<XyksBlessingOrange>() && CalamityClientConfig.Instance.TextEffects)
		{
			Color rarityColor2 = Color.White;
			Vector2 basePosition2 = default(Vector2);
			((Vector2)(ref basePosition2))._002Ector((float)line.X, (float)line.Y);
			float rate2 = Main.GlobalTimeWrappedHourly * 6f;
			List<Color> eColors2 = new List<Color>
			{
				new Color(248, 117, 52),
				Color.Gold,
				Color.Orange
			};
			int colorIndex2 = (int)(rate2 / 2f % (float)eColors2.Count);
			Color val3 = eColors2[colorIndex2];
			Color nextColor2 = eColors2[(colorIndex2 + 1) % eColors2.Count];
			Color val4 = Color.Lerp(val3, nextColor2, (rate2 % 2f > 1f) ? 1f : (rate2 % 1f));
			Vector2 backScale2 = line.BaseScale;
			Color backColor2 = val4;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			int draws2 = 20;
			for (int m = 0; m < draws2; m++)
			{
				Vector2 backPosition2 = basePosition2 + ((float)Math.PI * 2f * (float)m / 20f).ToRotationVector2() * 3.5f;
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, backPosition2, backColor2, line.Rotation, line.Origin, backScale2, line.MaxWidth, line.Spread);
			}
			Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Texture2D texture3 = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2).Value;
			Texture2D square2 = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSquareParticleThick", (AssetRequestMode)2).Value;
			Color drawColor2 = backColor2;
			Vector2 rotationPoint2 = texture2.Size() * 0.5f;
			int length2 = line.Text.Length;
			for (int n = 0; n < 8; n++)
			{
				Main.EntitySpriteDraw(texture2, basePosition2 + Vector2.UnitX * (float)length2 * 4f + Vector2.UnitY * 23f + Vector2.UnitX * (float)((n % 2 == 0) ? (-10 * n) : (10 * n)), null, drawColor2, (float)Math.PI / 2f, rotationPoint2, new Vector2(0.3f - 0.02f * (float)n, 1f + 0.35f * (float)n) * 0.3f * Main.rand.NextFloat(0.9f, 1f), (SpriteEffects)0);
			}
			for (int num = 0; num < 10; num++)
			{
				float sine3 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (0f + (float)num * 0.8f) / (float)Math.PI);
				float squareSine2 = (float)Math.Sin((float)num * 2.5f / (float)Math.PI);
				float clockSine = (float)Math.Sin((float)Math.Pow(Utils.GetLerpValue(0f, 110f, (int)(Main.GlobalTimeWrappedHourly * (float)(60 + num * 2)) % 120), 5.0));
				Vector2 weirdPos2 = Vector2.UnitX * 53f * (float)num * Utils.GetLerpValue(20f, 0f, num, clamped: true) + Vector2.UnitY * 23f * squareSine2 + Vector2.UnitY * 5.5f * sine3 + new Vector2(5f, 10f);
				for (int num2 = 0; num2 < 3; num2++)
				{
					Main.EntitySpriteDraw(square2, basePosition2 + weirdPos2, null, drawColor2 * (1f - 0.03f * (float)num), clockSine * ((float)Math.PI / 2f) + (float)Math.PI / 4f, square2.Size() * 0.5f, (1f - 0.07f * (float)num2) * new Vector2(1f, 1f) * (0.25f * ((float)Math.Pow(Utils.GetLerpValue(11f, 0f, num, clamped: true), 3.0) + 0.2f)), (SpriteEffects)0);
				}
				for (int num3 = 0; num3 < 3; num3++)
				{
					Main.EntitySpriteDraw(texture3, basePosition2 + weirdPos2, null, drawColor2 * (1f - 0.03f * (float)num), clockSine * ((float)Math.PI / 2f), texture3.Size() * 0.5f, (0.6f - 0.07f * (float)num3) * new Vector2(1f, 1f) * (0.25f * ((float)Math.Pow(Utils.GetLerpValue(11f, 0f, num, clamped: true), 3.0) + 0.2f)), (SpriteEffects)0);
				}
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, basePosition2, rarityColor2, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		if (line.Name == "ItemName" && line.Mod == "Terraria" && item.IsEnchanted())
		{
			Color rarityColor3 = (Color)(((_003F?)line.OverrideColor) ?? line.Color);
			Vector2 basePosition3 = default(Vector2);
			((Vector2)(ref basePosition3))._002Ector((float)line.X, (float)line.Y);
			float backInterpolant = (float)Math.Pow(Main.GlobalTimeWrappedHourly * 0.81f % 1f, 1.5);
			Vector2 backScale3 = line.BaseScale * MathHelper.Lerp(1f, 1.2f, backInterpolant);
			Color backColor3 = Color.Lerp(rarityColor3, Color.DarkRed, backInterpolant) * (float)Math.Pow(1f - backInterpolant, 0.46000000834465027);
			Vector2 backPosition3 = basePosition3 - new Vector2(1f, 0.1f) * backInterpolant * 10f;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			for (int num4 = 0; num4 < 2; num4++)
			{
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, backPosition3, backColor3, line.Rotation, line.Origin, backScale3, line.MaxWidth, line.Spread);
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, basePosition3, rarityColor3, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		if (line.Mod == "Terraria" && item.type == ModContent.ItemType<IVDripOnTheRocks>() && line.Name == "Tooltip4")
		{
			Vector2 basePosition4 = default(Vector2);
			((Vector2)(ref basePosition4))._002Ector((float)line.X, (float)line.Y);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			for (int num5 = 0; num5 < 3; num5++)
			{
				float timer = (float)Math.Sin(Main.GlobalTimeWrappedHourly + 1f) / 2f;
				float angle = Main.GlobalTimeWrappedHourly * 2f + (float)num5 * ((float)Math.PI * 2f) / 3f;
				float radius = timer * (3f + (float)num5 * 6f);
				Vector2 offset = new Vector2((float)Math.Cos(angle) * 1.7f, (float)Math.Sin(angle) * 1f) * radius;
				Vector2 pos = basePosition4 + offset;
				Color color = CalamityUtils.MulticolorLerp(Math.Abs(timer + (float)num5 * 0.2f) % 1f, Color.LightBlue, Color.LightGreen, (Color)(num5 switch
				{
					2 => Color.Red, 
					3 => Color.Blue, 
					_ => Color.White, 
				}));
				float opacity = 1f - (float)num5 * 0.2f;
				float rotation = line.Rotation + timer * (Main.GlobalTimeWrappedHourly * 1E-05f);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, pos, color * opacity, rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			return false;
		}
		if (line.Mod == "Terraria" && item.type == ModContent.ItemType<OntologicalDespoiler>() && (line.Name == "Tooltip1" || line.Name == "Tooltip2" || line.Name == "Tooltip4" || line.Name == "Tooltip5" || line.Name == "Tooltip7"))
		{
			Color rarityColor4 = Color.Black;
			Vector2 basePosition5 = default(Vector2);
			((Vector2)(ref basePosition5))._002Ector((float)line.X, (float)line.Y);
			Vector2 backScale4 = line.BaseScale;
			Player Owner = Main.LocalPlayer;
			if (Owner == null)
			{
				return false;
			}
			float sine4 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f / (float)Math.PI);
			int draws3 = 20;
			Color usedColor = Color.White;
			if (line.Name == "Tooltip1" || line.Name == "Tooltip4" || line.Name == "Tooltip7")
			{
				float rate3 = Main.GlobalTimeWrappedHourly * 3f;
				List<Color> eColors3 = new List<Color>
				{
					Owner.shirtColor,
					Color.Lerp(Owner.shirtColor, Color.Black, 0.15f),
					Color.Lerp(Owner.shirtColor, Color.White, 0.05f),
					Color.Lerp(Owner.shirtColor, Color.White, 0.25f)
				};
				int colorIndex3 = (int)(rate3 / 2f % (float)eColors3.Count);
				Color val5 = eColors3[colorIndex3];
				Color nextColor3 = eColors3[(colorIndex3 + 1) % eColors3.Count];
				usedColor = Color.Lerp(val5, nextColor3, (rate3 % 2f >= 1f) ? 1f : (rate3 % 1f));
				if (Owner.shirtColor == Color.White)
				{
					((Color)(ref usedColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				}
			}
			if (line.Name == "Tooltip5")
			{
				for (int num6 = 0; num6 < 4; num6++)
				{
					Vector2 shake = Main.rand.NextVector2Circular(5f, 5f);
					Vector2 backPosition4 = basePosition5 + shake;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, backPosition4, rarityColor4, line.Rotation, line.Origin, backScale4, line.MaxWidth, line.Spread);
				}
			}
			Color val6;
			if (line.Name == "Tooltip2" || line.Name == "Tooltip5")
			{
				for (int num7 = 0; num7 < draws3; num7++)
				{
					Color clr = ((line.Name == "Tooltip5") ? Color.Lerp(Color.White, Color.Black, sine4) : Color.White);
					Vector2 backPosition5 = basePosition5 + ((float)Math.PI * 2f * (float)num7 / (float)draws3).ToRotationVector2() * (1.5f + 0.2f * sine4);
					SpriteBatch spriteBatch = Main.spriteBatch;
					DynamicSpriteFont font = line.Font;
					string text = line.Text;
					val6 = clr;
					((Color)(ref val6)).A = 0;
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, backPosition5, val6, line.Rotation, line.Origin, backScale4, line.MaxWidth, line.Spread);
				}
				Color clr2 = ((line.Name == "Tooltip5") ? Color.Lerp(Color.Black, Color.White, sine4) : Color.Black);
				ChatManager.DrawColorCodedString(Main.spriteBatch, line.Font, line.Text, basePosition5, clr2, line.Rotation, line.Origin, backScale4);
				return false;
			}
			if (line.Name == "Tooltip4")
			{
				for (int num8 = 0; num8 < draws3; num8++)
				{
					Vector2 backPosition6 = basePosition5 + ((float)Math.PI * 2f * (float)num8 / (float)draws3).ToRotationVector2() * (4.5f + 0.2f * sine4);
					SpriteBatch spriteBatch2 = Main.spriteBatch;
					DynamicSpriteFont font2 = line.Font;
					string text2 = line.Text;
					val6 = usedColor;
					((Color)(ref val6)).A = 0;
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch2, font2, text2, backPosition6, val6, line.Rotation, line.Origin, backScale4, line.MaxWidth, line.Spread);
				}
				for (int num9 = 0; num9 < draws3; num9++)
				{
					Vector2 backPosition7 = basePosition5 + ((float)Math.PI * 2f * (float)num9 / (float)draws3).ToRotationVector2() * (2.5f + 0.2f * sine4);
					ChatManager.DrawColorCodedString(Main.spriteBatch, line.Font, line.Text, backPosition7, Color.Black, line.Rotation, line.Origin, backScale4);
				}
				ChatManager.DrawColorCodedString(Main.spriteBatch, line.Font, line.Text, basePosition5, Color.White, line.Rotation, line.Origin, backScale4);
				return false;
			}
			if (line.Name == "Tooltip7")
			{
				Texture2D texture4 = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
				Color drawColor3 = Color.Black;
				Vector2 rotationPoint3 = texture4.Size() * 0.5f;
				for (int num10 = 0; num10 < 6; num10++)
				{
					int length3 = line.Text.Length;
					Vector2 position = basePosition5 + Vector2.UnitX * (float)length3 * 4f + Vector2.UnitY * 10f + Vector2.UnitX * (float)((num10 % 2 == 0) ? (-7 * num10) : (7 * num10));
					val6 = usedColor;
					((Color)(ref val6)).A = 0;
					Main.EntitySpriteDraw(texture4, position, null, val6, (float)Math.PI / 2f, rotationPoint3, new Vector2(0.9f - 0.085f * (float)num10, 1f + 2.7f * (float)num10 * 1f) * 0.7f * Main.rand.NextFloat(0.95f, 1f), (SpriteEffects)0);
					Main.EntitySpriteDraw(texture4, basePosition5 + Vector2.UnitX * (float)length3 * 4f + Vector2.UnitY * 10f + Vector2.UnitX * (float)((num10 % 2 == 0) ? (-7 * num10) : (7 * num10)), null, drawColor3, (float)Math.PI / 2f, rotationPoint3, new Vector2(0.9f - 0.05f * (float)num10, 1f + 4.5f * (float)num10 * 1f) * 0.55f * Main.rand.NextFloat(0.95f, 1f), (SpriteEffects)0);
				}
				for (int num11 = 0; num11 < draws3; num11++)
				{
					Vector2 backPosition8 = basePosition5 + ((float)Math.PI * 2f * (float)num11 / (float)draws3).ToRotationVector2() * 1.5f;
					SpriteBatch spriteBatch3 = Main.spriteBatch;
					DynamicSpriteFont font3 = line.Font;
					string text3 = line.Text;
					val6 = usedColor;
					((Color)(ref val6)).A = 0;
					ChatManager.DrawColorCodedString(spriteBatch3, font3, text3, backPosition8, val6 * 0.6f, line.Rotation, line.Origin, backScale4);
				}
				ChatManager.DrawColorCodedString(Main.spriteBatch, line.Font, line.Text, basePosition5, Color.Black, line.Rotation, line.Origin, backScale4);
				return false;
			}
			if (line.Name == "Tooltip1")
			{
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, basePosition5, usedColor, line.Rotation, line.Origin, backScale4, line.MaxWidth, line.Spread);
				return false;
			}
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, line.Text, basePosition5, Color.White, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		return true;
	}

	public static void InsertKnowledgeTooltip(List<TooltipLine> tooltips, int tier, bool allowOldWorlds = false)
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge1", CalamityUtils.GetTextValue("Misc.SchematicKnowledgeTooltip"));
		TooltipLine line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.SchematicKnowledgeTooltip2"));
		switch (tier)
		{
		case 1:
			line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.Tier1KnowledgeTooltip"));
			break;
		case 2:
			line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.Tier2KnowledgeTooltip"));
			break;
		case 3:
			line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.Tier3KnowledgeTooltip"));
			break;
		case 4:
			line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.Tier4KnowledgeTooltip"));
			break;
		case 5:
			line2 = new TooltipLine(CalamityMod.Instance, "SchematicKnowledge2", CalamityUtils.GetTextValue("Misc.Tier5KnowledgeTooltip"));
			break;
		}
		line.OverrideColor = (line2.OverrideColor = Color.Cyan);
		bool allowedDueToOldWorld = allowOldWorlds && CalamityWorld.IsWorldAfterDraedonUpdate;
		tooltips.AddWithCondition(line, !ArsenalTierGatedRecipe.HasTierBeenLearned(tier) && !allowedDueToOldWorld);
		tooltips.AddWithCondition(line2, !ArsenalTierGatedRecipe.HasTierBeenLearned(tier) && !allowedDueToOldWorld);
	}

	internal static void LoadTweaks()
	{
		IItemTweak[] trueMelee = Do(TrueMelee);
		IItemTweak[] trueMeleeNoSpeed = Do(TrueMeleeNoSpeed);
		IItemTweak[] nonConsumableBossSummon = Do(MaxStack(1), NotConsumable);
		IItemTweak[] phaseblade = Do(UseTurn, DamageExact(36));
		IItemTweak[] phasesaber = Do(DamageExact(132));
		SortedDictionary<int, IItemTweak[]> sortedDictionary = new SortedDictionary<int, IItemTweak[]>();
		sortedDictionary.Add(406, Do(TrueMelee, DamageExact(69), UseExact(7), ShootSpeedExact(18f)));
		sortedDictionary.Add(481, Do(UseExact(14)));
		sortedDictionary.Add(482, Do(UseTurn, DamageExact(75), UseExact(7)));
		sortedDictionary.Add(3377, Do(UseTimeExact(15), UseAnimationExact(45), ReuseDelayExact(15)));
		sortedDictionary.Add(739, Do(ManaExact(2)));
		sortedDictionary.Add(2424, Do(DamageExact(107), UseExact(30)));
		sortedDictionary.Add(3772, Do(UseExact(10)));
		sortedDictionary.Add(157, Do(DamageRatio(0.9f)));
		sortedDictionary.Add(1324, Do(DamageExact(76), UseExact(14)));
		sortedDictionary.Add(1801, Do(DamageExact(50)));
		sortedDictionary.Add(723, Do(UseMeleeSpeed, DamageExact(131), UseAnimationExact(40), KnockbackExact(8f)));
		sortedDictionary.Add(1121, Do(DamageExact(11), ManaExact(4)));
		sortedDictionary.Add(1123, Do(UseTurn, DamageExact(32)));
		sortedDictionary.Add(1130, Do(DamageExact(11), UseTimeExact(22), ShootSpeedExact(10f)));
		sortedDictionary.Add(2888, Do(DamageExact(21), UseExact(38)));
		sortedDictionary.Add(3211, Do(UseTurn, UseRatio(0.8f), DamageExact(120)));
		sortedDictionary.Add(1931, Do(DamageExact(41), ManaExact(7)));
		sortedDictionary.Add(1825, Do(DamageExact(24)));
		sortedDictionary.Add(986, Do(DamageExact(40), ShootSpeedExact(15f)));
		sortedDictionary.Add(198, phaseblade);
		sortedDictionary.Add(3764, phasesaber);
		sortedDictionary.Add(1166, Do(UseTurn, DamageExact(25)));
		sortedDictionary.Add(1313, Do(DamageExact(27), ManaExact(13), ShootSpeedExact(5.5f)));
		sortedDictionary.Add(3852, Do(ManaExact(14)));
		sortedDictionary.Add(964, Do(DamageExact(11)));
		sortedDictionary.Add(426, Do(UseTurn, DamageExact(140)));
		sortedDictionary.Add(1782, Do(DamageExact(66)));
		sortedDictionary.Add(3282, Do(DamageExact(31)));
		sortedDictionary.Add(3012, Do(DamageExact(100)));
		sortedDictionary.Add(1929, Do(DamageExact(35)));
		sortedDictionary.Add(1325, Do(DamageRatio(1.34f)));
		sortedDictionary.Add(1226, Do(UseMeleeSpeed, DamageExact(180), UseExact(45), ShootSpeedExact(22f)));
		sortedDictionary.Add(1228, Do(UseMeleeSpeed, DamageExact(95)));
		sortedDictionary.Add(1227, Do(UseMeleeSpeed, DamageExact(92), UseExact(10)));
		sortedDictionary.Add(1928, Do(UseTurn, UseMeleeSpeed, DamageExact(80), UseExact(30)));
		sortedDictionary.Add(3014, Do(DamageExact(63)));
		sortedDictionary.Add(434, Do(DamageExact(21)));
		sortedDictionary.Add(537, Do(TrueMelee, DamageExact(55), UseExact(9), ShootSpeedExact(12f)));
		sortedDictionary.Add(435, Do(UseExact(18)));
		sortedDictionary.Add(483, Do(DamageExact(60), UseExact(9)));
		sortedDictionary.Add(3284, Do(DamageExact(43)));
		sortedDictionary.Add(905, Do(UseExact(12)));
		sortedDictionary.Add(3279, Do(DamageExact(20)));
		sortedDictionary.Add(3280, Do(DamageExact(20)));
		sortedDictionary.Add(515, Do(DamageExact(6)));
		sortedDictionary.Add(3009, Do(DamageExact(20)));
		sortedDictionary.Add(518, Do(DamageExact(40)));
		sortedDictionary.Add(3051, Do(DamageExact(35)));
		sortedDictionary.Add(545, Do(DamageExact(14)));
		sortedDictionary.Add(3010, Do(DamageExact(25)));
		sortedDictionary.Add(672, Do(UseRatio(0.9f), DamageExact(90)));
		sortedDictionary.Add(3029, Do(DamageExact(30)));
		sortedDictionary.Add(389, Do(DamageExact(85)));
		sortedDictionary.Add(274, Do(TrueMelee, DamageExact(40)));
		sortedDictionary.Add(3008, Do(DamageExact(58)));
		sortedDictionary.Add(3543, Do(DamageExact(125), UseExact(22)));
		sortedDictionary.Add(3859, Do(DamageExact(42)));
		sortedDictionary.Add(3827, Do(UseMeleeSpeed, DamageExact(150)));
		sortedDictionary.Add(3249, Do(DamageExact(50)));
		sortedDictionary.Add(1327, Do(UseMeleeSpeed, DamageExact(65), ShootSpeedExact(15f)));
		sortedDictionary.Add(44, Do(DamageExact(12)));
		sortedDictionary.Add(272, Do(DamageExact(28)));
		sortedDictionary.Add(3349, Do(UseTurn, DamageExact(24)));
		sortedDictionary.Add(1910, Do(ShootSpeedDelta(5f)));
		sortedDictionary.Add(742, Do(DamageExact(27)));
		sortedDictionary.Add(5005, Do(DamageExact(68)));
		sortedDictionary.Add(55, Do(DamageExact(24)));
		sortedDictionary.Add(989, Do(UseMeleeSpeed, DamageExact(30), ShootSpeedExact(15f)));
		sortedDictionary.Add(3104, Do(DamageExact(8)));
		sortedDictionary.Add(368, Do(TrueMelee, DamageExact(170)));
		sortedDictionary.Add(4952, Do(DamageExact(54)));
		sortedDictionary.Add(2608, Do(UseTurn, UseExact(13)));
		sortedDictionary.Add(3546, Do(DamageExact(50), UseExact(25)));
		sortedDictionary.Add(119, Do(DamageExact(37)));
		sortedDictionary.Add(218, Do(DamageExact(36), ManaExact(18)));
		sortedDictionary.Add(506, Do(DamageExact(21), ShootSpeedDelta(3f)));
		sortedDictionary.Add(112, Do(ManaExact(7), DamageRatio(0.78f)));
		sortedDictionary.Add(1264, Do(ManaExact(7), UseExact(22), DamageExact(70), ShootSpeedExact(14f)));
		sortedDictionary.Add(3030, Do(DamageExact(53)));
		sortedDictionary.Add(676, Do(UseMeleeSpeed, DamageExact(88)));
		sortedDictionary.Add(2270, Do(UseExact(6)));
		sortedDictionary.Add(3519, Do(TrueMelee, DamageExact(17)));
		sortedDictionary.Add(1297, Do(DamageExact(150)));
		sortedDictionary.Add(3316, Do(DamageExact(39)));
		sortedDictionary.Add(200, phaseblade);
		sortedDictionary.Add(3766, phasesaber);
		sortedDictionary.Add(758, Do(DamageExact(105)));
		sortedDictionary.Add(550, Do(TrueMelee, DamageExact(130), ShootSpeedExact(7f)));
		sortedDictionary.Add(578, Do(UseExact(12)));
		sortedDictionary.Add(164, Do(UseExact(20)));
		sortedDictionary.Add(1302, Do(DamageExact(13)));
		sortedDictionary.Add(5294, Do(DamageExact(27)));
		sortedDictionary.Add(2364, Do(DamageExact(18)));
		sortedDictionary.Add(724, Do(UseMeleeSpeed));
		sortedDictionary.Add(670, Do(UseExact(25), ShootSpeedExact(9f)));
		sortedDictionary.Add(496, Do(UseExact(6), ShootSpeedExact(20f)));
		sortedDictionary.Add(1306, Do(UseMeleeSpeed, DamageExact(75), ShootSpeedExact(20f)));
		sortedDictionary.Add(1334, Do(DamageExact(13)));
		sortedDictionary.Add(1335, Do(DamageExact(11)));
		sortedDictionary.Add(2365, Do(DamageExact(25)));
		sortedDictionary.Add(1445, Do(DamageExact(83), ShootSpeedExact(11f)));
		sortedDictionary.Add(2880, Do(UseMeleeSpeed, DamageExact(80), UseExact(25)));
		sortedDictionary.Add(6, Do(TrueMelee, DamageExact(10)));
		sortedDictionary.Add(671, Do(UseTurn));
		sortedDictionary.Add(1314, Do(DamageRatio(2.65f)));
		sortedDictionary.Add(3291, Do(DamageExact(65)));
		sortedDictionary.Add(2795, Do(DamageExact(49)));
		sortedDictionary.Add(514, Do(DamageExact(46), UseExact(10), ManaExact(4)));
		sortedDictionary.Add(3541, Do(DamageExact(57), ManaExact(10)));
		sortedDictionary.Add(3495, Do(TrueMelee, DamageExact(11)));
		sortedDictionary.Add(1178, Do(DamageExact(61)));
		sortedDictionary.Add(561, Do(DamageExact(80), ShootSpeedExact(18f)));
		sortedDictionary.Add(3570, Do(DamageExact(110)));
		sortedDictionary.Add(494, Do(DamageExact(50), ShootSpeedExact(12f)));
		sortedDictionary.Add(113, Do(DamageExact(23), ManaExact(10), UseAnimationExact(20), UseTimeExact(10)));
		sortedDictionary.Add(682, Do(DamageExact(60)));
		sortedDictionary.Add(3269, Do(ManaExact(6), DamageExact(75)));
		sortedDictionary.Add(3063, Do(UseMeleeSpeed, DamageExact(240)));
		sortedDictionary.Add(2750, Do(DamageExact(58), ManaExact(7), ShootSpeedExact(13f)));
		sortedDictionary.Add(4457, Do(DamageExact(90)));
		sortedDictionary.Add(4458, Do(DamageExact(90)));
		sortedDictionary.Add(98, Do(DamageExact(4)));
		sortedDictionary.Add(120, Do(UseExact(28)));
		sortedDictionary.Add(3835, Do(TrueMeleeNoSpeed, DamageExact(83)));
		sortedDictionary.Add(3836, Do(TrueMelee, DamageExact(90)));
		sortedDictionary.Add(3858, Do(DamageExact(225)));
		sortedDictionary.Add(3567, Do(DamageExact(17)));
		sortedDictionary.Add(3569, Do(DamageExact(50)));
		sortedDictionary.Add(155, Do(CritDelta(10)));
		sortedDictionary.Add(756, Do(TrueMelee, UseRatio(0.8f), DamageExact(100)));
		sortedDictionary.Add(96, Do(DamageExact(22)));
		sortedDictionary.Add(390, Do(TrueMelee, DamageExact(65), UseExact(8), ShootSpeedExact(15f)));
		sortedDictionary.Add(436, Do(UseExact(16)));
		sortedDictionary.Add(484, Do(UseTurn, DamageExact(70), UseExact(8)));
		sortedDictionary.Add(3107, Do(DamageExact(77)));
		sortedDictionary.Add(788, Do(ManaExact(10), DamageExact(65)));
		sortedDictionary.Add(273, Do(TrueMelee, DamageExact(45)));
		sortedDictionary.Add(1947, Do(UseMeleeSpeed));
		sortedDictionary.Add(4258, phaseblade);
		sortedDictionary.Add(4259, phasesaber);
		sortedDictionary.Add(1193, Do(TrueMelee, DamageExact(128), ShootSpeedExact(6f)));
		sortedDictionary.Add(1194, Do(DamageExact(48)));
		sortedDictionary.Add(1192, Do(UseTurn, DamageExact(175)));
		sortedDictionary.Add(1513, Do(DamageExact(95), ShootSpeedExact(28f)));
		sortedDictionary.Add(1186, Do(TrueMelee, DamageExact(120), ShootSpeedExact(5.4f)));
		sortedDictionary.Add(1187, Do(DamageExact(45)));
		sortedDictionary.Add(1185, Do(DamageExact(150)));
		sortedDictionary.Add(661, Do(DamageExact(32)));
		sortedDictionary.Add(659, Do(UseTurn, DamageExact(45)));
		sortedDictionary.Add(5117, Do(DamageExact(25), ShootSpeedExact(15f)));
		sortedDictionary.Add(219, Do(UseExact(20)));
		sortedDictionary.Add(3483, Do(TrueMelee, DamageExact(18)));
		sortedDictionary.Add(1308, Do(DamageExact(57)));
		sortedDictionary.Add(1122, Do(DamageExact(135)));
		sortedDictionary.Add(5065, Do(DamageExact(80)));
		sortedDictionary.Add(3106, Do(UseTurn, UseExact(11), DamageExact(200)));
		sortedDictionary.Add(2330, Do(UseTurn, KnockbackExact(10f)));
		sortedDictionary.Add(201, phaseblade);
		sortedDictionary.Add(3767, phasesaber);
		sortedDictionary.Add(1157, Do(DamageExact(63)));
		sortedDictionary.Add(1260, Do(DamageExact(60), ManaExact(40)));
		sortedDictionary.Add(495, Do(DamageExact(40), ManaExact(13), KnockbackExact(8f)));
		sortedDictionary.Add(3285, Do(DamageExact(18)));
		sortedDictionary.Add(1802, Do(DamageExact(36)));
		sortedDictionary.Add(2622, Do(DamageExact(103)));
		sortedDictionary.Add(1930, Do(DamageExact(40)));
		sortedDictionary.Add(199, phaseblade);
		sortedDictionary.Add(3765, phasesaber);
		sortedDictionary.Add(1870, Do(DamageExact(24)));
		sortedDictionary.Add(3287, Do(DamageExact(48)));
		sortedDictionary.Add(759, Do(DamageExact(60), ShootSpeedExact(9f)));
		sortedDictionary.Add(266, Do(DamageExact(22), UseExact(20)));
		sortedDictionary.Add(741, Do(DamageExact(25)));
		sortedDictionary.Add(1571, Do(DamageExact(63)));
		sortedDictionary.Add(3018, Do(UseMeleeSpeed, DamageExact(45), ShootSpeedExact(16f)));
		sortedDictionary.Add(1444, Do(DamageExact(100)));
		sortedDictionary.Add(3052, Do(DamageExact(55)));
		sortedDictionary.Add(3053, Do(DamageExact(40), ShootSpeedExact(30f)));
		sortedDictionary.Add(3054, Do(DamageExact(50)));
		sortedDictionary.Add(4270, Do(DamageExact(49)));
		sortedDictionary.Add(534, Do(DamageExact(36)));
		sortedDictionary.Add(4764, Do(ShootSpeedExact(11f)));
		sortedDictionary.Add(278, Do(DamageExact(8)));
		sortedDictionary.Add(3513, Do(TrueMelee, DamageExact(14)));
		sortedDictionary.Add(3787, Do(DamageExact(46)));
		sortedDictionary.Add(3258, Do(UseTurn, DamageExact(120)));
		sortedDictionary.Add(4758, Do(DamageExact(9)));
		sortedDictionary.Add(1254, Do(DamageExact(200), UseExact(40)));
		sortedDictionary.Add(3473, Do(DamageExact(122)));
		sortedDictionary.Add(3006, Do(DamageExact(38)));
		sortedDictionary.Add(127, Do(DamageExact(23)));
		sortedDictionary.Add(280, Do(TrueMelee, DamageExact(14)));
		sortedDictionary.Add(1446, Do(DamageExact(72)));
		sortedDictionary.Add(3779, Do(UseExact(20), ManaExact(11), ShootSpeedExact(2f)));
		sortedDictionary.Add(1296, Do(DamageExact(150)));
		sortedDictionary.Add(197, Do(UseExact(18)));
		sortedDictionary.Add(3531, Do(DamageExact(20)));
		sortedDictionary.Add(4607, Do(DamageExact(49)));
		sortedDictionary.Add(3352, Do(UseTurn, DamageExact(21)));
		sortedDictionary.Add(1258, Do(DamageExact(75)));
		sortedDictionary.Add(4060, Do(DamageExact(55)));
		sortedDictionary.Add(2332, Do(TrueMelee, DamageExact(24)));
		sortedDictionary.Add(679, Do(DamageExact(34)));
		sortedDictionary.Add(3351, Do(UseTurn, UseRatio(0.8f), DamageExact(70)));
		sortedDictionary.Add(796, Do(DamageExact(17)));
		sortedDictionary.Add(757, Do(DamageExact(95)));
		sortedDictionary.Add(4144, Do(TrueMeleeNoSpeed, DamageExact(13)));
		sortedDictionary.Add(3389, Do(DamageExact(90)));
		sortedDictionary.Add(3292, Do(DamageExact(80)));
		sortedDictionary.Add(801, Do(DamageExact(24)));
		sortedDictionary.Add(802, Do(TrueMelee, DamageExact(20)));
		sortedDictionary.Add(800, Do(DamageExact(15)));
		sortedDictionary.Add(4061, Do(UseMeleeSpeed));
		sortedDictionary.Add(4062, Do(DamageExact(18)));
		sortedDictionary.Add(1201, Do(DamageExact(52)));
		sortedDictionary.Add(1199, Do(UseTurn, DamageExact(192)));
		sortedDictionary.Add(1200, Do(TrueMelee, DamageExact(144), ShootSpeedExact(6.5f)));
		sortedDictionary.Add(740, Do(ManaExact(2)));
		sortedDictionary.Add(3105, Do(DamageExact(62), UseExact(33), ManaExact(20)));
		sortedDictionary.Add(3210, Do(UseExact(9)));
		sortedDictionary.Add(277, Do(TrueMelee, DamageExact(20)));
		sortedDictionary.Add(5298, Do(DamageExact(24)));
		sortedDictionary.Add(674, Do(TrueMelee, DamageExact(112)));
		sortedDictionary.Add(675, Do(DamageExact(105)));
		sortedDictionary.Add(2624, Do(DamageExact(45)));
		sortedDictionary.Add(4915, Do(DamageExact(8)));
		sortedDictionary.Add(3489, Do(TrueMelee, DamageExact(15)));
		sortedDictionary.Add(47, Do(DamageExact(11)));
		sortedDictionary.Add(683, Do(ManaRatio(0.78f), DamageRatio(0.91f)));
		sortedDictionary.Add(1265, Do(UseExact(8)));
		sortedDictionary.Add(1569, Do(DamageExact(38)));
		sortedDictionary.Add(3288, Do(DamageExact(48)));
		sortedDictionary.Add(2188, Do(UseExact(27)));
		sortedDictionary.Add(1155, Do(DamageExact(44)));
		sortedDictionary.Add(165, Do(DamageExact(23)));
		sortedDictionary.Add(202, phaseblade);
		sortedDictionary.Add(3768, phasesaber);
		sortedDictionary.Add(284, Do(DamageExact(16), Value(Item.sellPrice(0, 0, 0, 20))));
		sortedDictionary.Add(3286, Do(DamageExact(53)));
		sortedDictionary.Add(203, phaseblade);
		sortedDictionary.Add(3769, phasesaber);
		sortedDictionary.Add(4956, Do(DamageExact(210)));
		sortedDictionary.Add(1304, Do(UseTurn, KnockbackExact(12f)));
		sortedDictionary.Add(3999, Do(DefenseExact(3)));
		sortedDictionary.Add(4038, Do(DefenseExact(3)));
		sortedDictionary.Add(4003, Do(DefenseExact(4)));
		sortedDictionary.Add(193, Do(DefenseExact(2)));
		sortedDictionary.Add(4004, Do(DefenseExact(3)));
		sortedDictionary.Add(216, Do(DefenseExact(3)));
		sortedDictionary.Add(3871, Do(DefenseDelta(-2)));
		sortedDictionary.Add(3873, Do(DefenseDelta(-3)));
		sortedDictionary.Add(3872, Do(DefenseDelta(-3)));
		sortedDictionary.Add(3800, Do(DefenseDelta(-1)));
		sortedDictionary.Add(3802, Do(DefenseDelta(-2)));
		sortedDictionary.Add(3801, Do(DefenseDelta(-4)));
		sortedDictionary.Add(5295, Do(AxePower(100)));
		sortedDictionary.Add(387, Do(TrueMeleeNoSpeed, AxePower(90), TileBoostExact(0), DamageExact(75)));
		sortedDictionary.Add(388, Do(TrueMeleeNoSpeed, TileBoostExact(1), DamageExact(32)));
		sortedDictionary.Add(778, Do(TileBoostExact(1)));
		sortedDictionary.Add(993, Do(AxePower(160), UseTimeExact(10), TileBoostExact(1)));
		sortedDictionary.Add(799, Do(AxePower(100), UseTimeExact(13)));
		sortedDictionary.Add(1320, Do(UseTimeExact(6)));
		sortedDictionary.Add(2746, Do(HammerPower(25), UseTimeExact(11)));
		sortedDictionary.Add(3098, Do(TrueMeleeNoSpeed, UseTimeExact(3), TileBoostExact(0)));
		sortedDictionary.Add(882, Do(UseTimeExact(9)));
		sortedDictionary.Add(1917, Do(UseTimeExact(9), TileBoostExact(1)));
		sortedDictionary.Add(1232, Do(TrueMeleeNoSpeed, AxePower(120), UseTimeExact(3), DamageExact(112)));
		sortedDictionary.Add(1231, Do(TrueMeleeNoSpeed, TileBoostExact(2), DamageExact(43)));
		sortedDictionary.Add(1233, Do(AxePower(165), TileBoostExact(2)));
		sortedDictionary.Add(1262, Do(TrueMeleeNoSpeed, UseTimeExact(5)));
		sortedDictionary.Add(1230, Do(TileBoostExact(2)));
		sortedDictionary.Add(1234, Do(UseTimeExact(8), TileBoostExact(2)));
		sortedDictionary.Add(383, Do(TrueMeleeNoSpeed, UseTimeExact(4), DamageExact(51)));
		sortedDictionary.Add(385, Do(TrueMeleeNoSpeed, PickPower(130), UseTimeExact(5), DamageExact(21)));
		sortedDictionary.Add(776, Do(PickPower(130), UseTimeExact(9)));
		sortedDictionary.Add(991, Do(AxePower(125), UseTimeExact(12)));
		sortedDictionary.Add(3506, Do(AxePower(50), UseTimeExact(16), TileBoostExact(0)));
		sortedDictionary.Add(3505, Do(UseTimeExact(12), TileBoostExact(0)));
		sortedDictionary.Add(3509, Do(UseTimeExact(10), TileBoostExact(0)));
		sortedDictionary.Add(798, Do(UseTimeExact(10)));
		sortedDictionary.Add(579, Do(TrueMeleeNoSpeed, TileBoostExact(1), DamageExact(80)));
		sortedDictionary.Add(654, Do(HammerPower(25), UseTimeExact(9)));
		sortedDictionary.Add(797, Do(HammerPower(70), UseTimeExact(13)));
		sortedDictionary.Add(3518, Do(AxePower(80), UseTimeExact(14)));
		sortedDictionary.Add(3517, Do(HammerPower(60), UseTimeExact(9)));
		sortedDictionary.Add(3521, Do(UseTimeExact(9)));
		sortedDictionary.Add(787, Do(UseTimeExact(10), TileBoostExact(1)));
		sortedDictionary.Add(10, Do(AxePower(60), UseTimeExact(15)));
		sortedDictionary.Add(7, Do(HammerPower(45), UseTimeExact(11)));
		sortedDictionary.Add(1, Do(UseTimeExact(8)));
		sortedDictionary.Add(2798, Do(PickPower(220), AxePower(120), UseTimeExact(4), DamageExact(54)));
		sortedDictionary.Add(3494, Do(AxePower(60), UseTimeExact(15)));
		sortedDictionary.Add(3493, Do(HammerPower(45), UseTimeExact(11)));
		sortedDictionary.Add(3497, Do(PickPower(40), UseTimeExact(8)));
		sortedDictionary.Add(5095, Do(UseExact(13), TileBoostExact(1)));
		sortedDictionary.Add(3524, Do(AxePower(175), UseTimeExact(5)));
		sortedDictionary.Add(3522, Do(AxePower(175), UseTimeExact(5)));
		sortedDictionary.Add(3525, Do(AxePower(175), UseTimeExact(5)));
		sortedDictionary.Add(3523, Do(AxePower(175), UseTimeExact(5)));
		sortedDictionary.Add(204, Do(HammerPower(70)));
		sortedDictionary.Add(217, Do(HammerPower(75), AxePower(125)));
		sortedDictionary.Add(122, Do(UseTimeExact(10)));
		sortedDictionary.Add(384, Do(TrueMeleeNoSpeed, AxePower(80), UseTimeExact(4), DamageExact(63)));
		sortedDictionary.Add(386, Do(TrueMeleeNoSpeed, PickPower(160), UseTimeExact(4), DamageExact(26)));
		sortedDictionary.Add(777, Do(PickPower(160), UseTimeExact(8)));
		sortedDictionary.Add(992, Do(AxePower(140), UseTimeExact(11)));
		sortedDictionary.Add(2779, Do(TrueMeleeNoSpeed, UseTimeExact(3), TileBoostExact(4), DamageExact(95)));
		sortedDictionary.Add(103, Do(PickPower(66), UseTimeExact(9)));
		sortedDictionary.Add(1197, Do(TrueMeleeNoSpeed, AxePower(80), UseTimeExact(4), DamageExact(63)));
		sortedDictionary.Add(1196, Do(TrueMeleeNoSpeed, PickPower(160), UseTimeExact(4), DamageExact(26)));
		sortedDictionary.Add(1195, Do(PickPower(160), UseTimeExact(8)));
		sortedDictionary.Add(1223, Do(AxePower(140), UseTimeExact(11)));
		sortedDictionary.Add(1190, Do(TrueMeleeNoSpeed, AxePower(70), UseTimeExact(4), DamageExact(51)));
		sortedDictionary.Add(1189, Do(TrueMeleeNoSpeed, UseTimeExact(5), DamageExact(21)));
		sortedDictionary.Add(1188, Do(UseTimeExact(9)));
		sortedDictionary.Add(1222, Do(AxePower(125)));
		sortedDictionary.Add(2516, Do(HammerPower(25), UseTimeExact(11)));
		sortedDictionary.Add(660, Do(HammerPower(25), UseTimeExact(4), UseAnimationExact(20), DamageExact(36)));
		sortedDictionary.Add(990, Do(TileBoostExact(1)));
		sortedDictionary.Add(3482, Do(AxePower(80), UseTimeExact(14)));
		sortedDictionary.Add(3481, Do(HammerPower(60), UseTimeExact(9)));
		sortedDictionary.Add(3485, Do(PickPower(55), UseTimeExact(9)));
		sortedDictionary.Add(367, Do(UseTimeExact(11), TileBoostExact(1)));
		sortedDictionary.Add(657, Do(HammerPower(25), UseTimeExact(10)));
		sortedDictionary.Add(2320, Do(HammerPower(50), UseTimeExact(10)));
		sortedDictionary.Add(2342, Do(TrueMeleeNoSpeed, AxePower(45)));
		sortedDictionary.Add(922, Do(HammerPower(25), UseTimeExact(9)));
		sortedDictionary.Add(3512, Do(AxePower(70), UseTimeExact(14)));
		sortedDictionary.Add(3511, Do(HammerPower(55), UseTimeExact(10)));
		sortedDictionary.Add(3515, Do(PickPower(50)));
		sortedDictionary.Add(2784, Do(TrueMeleeNoSpeed, UseTimeExact(3), TileBoostExact(4), DamageExact(95)));
		sortedDictionary.Add(1507, Do(AxePower(170), TileBoostExact(4)));
		sortedDictionary.Add(1506, Do(TileBoostExact(4)));
		sortedDictionary.Add(3464, Do(TrueMeleeNoSpeed, UseTimeExact(3), TileBoostExact(4), DamageExact(95)));
		sortedDictionary.Add(104, Do(HammerPower(70), UseTimeExact(13)));
		sortedDictionary.Add(3500, Do(AxePower(50), UseTimeExact(16)));
		sortedDictionary.Add(3499, Do(HammerPower(35), UseTimeExact(12)));
		sortedDictionary.Add(3503, Do(UseTimeExact(10)));
		sortedDictionary.Add(1204, Do(TrueMeleeNoSpeed, AxePower(90), TileBoostExact(0), DamageExact(75)));
		sortedDictionary.Add(1203, Do(TrueMeleeNoSpeed, PickPower(180), TileBoostExact(1), DamageExact(32)));
		sortedDictionary.Add(1202, Do(PickPower(180), UseTimeExact(8), TileBoostExact(1)));
		sortedDictionary.Add(1224, Do(AxePower(160), UseTimeExact(10), TileBoostExact(1)));
		sortedDictionary.Add(3488, Do(AxePower(70), UseTimeExact(14)));
		sortedDictionary.Add(3487, Do(HammerPower(55), UseTimeExact(10)));
		sortedDictionary.Add(3491, Do(UseTimeExact(11)));
		sortedDictionary.Add(2774, Do(TrueMeleeNoSpeed, UseTimeExact(3), TileBoostExact(4), DamageExact(95)));
		sortedDictionary.Add(45, Do(AxePower(100), UseTimeExact(13)));
		sortedDictionary.Add(196, Do(UseTimeExact(11), TileBoostExact(0)));
		sortedDictionary.Add(3368, trueMeleeNoSpeed);
		sortedDictionary.Add(3507, trueMelee);
		sortedDictionary.Add(4463, trueMelee);
		sortedDictionary.Add(4790, trueMelee);
		sortedDictionary.Add(4788, trueMelee);
		sortedDictionary.Add(2778, trueMeleeNoSpeed);
		sortedDictionary.Add(2331, trueMelee);
		sortedDictionary.Add(4923, trueMelee);
		sortedDictionary.Add(486, trueMelee);
		sortedDictionary.Add(4789, trueMelee);
		sortedDictionary.Add(2783, trueMeleeNoSpeed);
		sortedDictionary.Add(3463, trueMeleeNoSpeed);
		sortedDictionary.Add(1826, trueMelee);
		sortedDictionary.Add(3501, trueMelee);
		sortedDictionary.Add(2773, trueMeleeNoSpeed);
		sortedDictionary.Add(190, Do(UseTurn));
		sortedDictionary.Add(795, Do(UseTurn));
		sortedDictionary.Add(2745, Do(UseTurn));
		sortedDictionary.Add(881, Do(UseTurn));
		sortedDictionary.Add(1909, Do(UseTurn));
		sortedDictionary.Add(3508, Do(UseTurn));
		sortedDictionary.Add(653, Do(UseTurn));
		sortedDictionary.Add(121, Do(UseTurn));
		sortedDictionary.Add(3520, Do(UseTurn));
		sortedDictionary.Add(4, Do(UseTurn));
		sortedDictionary.Add(3496, Do(UseTurn));
		sortedDictionary.Add(46, Do(UseTurn));
		sortedDictionary.Add(2517, Do(UseTurn));
		sortedDictionary.Add(3484, Do(UseTurn));
		sortedDictionary.Add(656, Do(UseTurn));
		sortedDictionary.Add(921, Do(UseTurn));
		sortedDictionary.Add(3514, Do(UseTurn));
		sortedDictionary.Add(3502, Do(UseTurn));
		sortedDictionary.Add(3490, Do(UseTurn));
		sortedDictionary.Add(24, Do(UseTurn));
		sortedDictionary.Add(1133, nonConsumableBossSummon);
		sortedDictionary.Add(1331, nonConsumableBossSummon);
		sortedDictionary.Add(3601, nonConsumableBossSummon);
		sortedDictionary.Add(5120, nonConsumableBossSummon);
		sortedDictionary.Add(544, nonConsumableBossSummon);
		sortedDictionary.Add(557, nonConsumableBossSummon);
		sortedDictionary.Add(556, nonConsumableBossSummon);
		sortedDictionary.Add(5334, nonConsumableBossSummon);
		sortedDictionary.Add(4988, nonConsumableBossSummon);
		sortedDictionary.Add(560, nonConsumableBossSummon);
		sortedDictionary.Add(43, nonConsumableBossSummon);
		sortedDictionary.Add(70, nonConsumableBossSummon);
		sortedDictionary.Add(4346, Do(Worthless));
		sortedDictionary.Add(930, Do(Value(Item.sellPrice(0, 0, 10))));
		sortedDictionary.Add(183, Do(Worthless));
		sortedDictionary.Add(5, Do(Worthless));
		sortedDictionary.Add(4341, Do(Value(Item.sellPrice(0, 0, 0, 20))));
		sortedDictionary.Add(5391, Do(Worthless));
		sortedDictionary.Add(2887, Do(Worthless));
		sortedDictionary.Add(60, Do(Worthless));
		currentTweaks = sortedDictionary;
	}

	internal static void UnloadTweaks()
	{
		currentTweaks?.Clear();
		currentTweaks = null;
	}

	internal static void SetDefaults_ApplyTweaks(Item item)
	{
		if (currentTweaks == null || !currentTweaks.TryGetValue(item.type, out var tweaks))
		{
			return;
		}
		IItemTweak[] array = tweaks;
		foreach (IItemTweak tweak in array)
		{
			if (tweak.AppliesTo(item))
			{
				tweak.ApplyTweak(item);
			}
		}
	}

	internal static IItemTweak[] Do(params IItemTweak[] r)
	{
		return r;
	}

	internal static bool DealsDamage(Item it)
	{
		return it.damage > 0;
	}

	internal static bool HasDefense(Item it)
	{
		return it.defense > 0;
	}

	internal static bool HasKnockback(Item it)
	{
		return !it.accessory & !it.vanity;
	}

	internal static bool IsAxe(Item it)
	{
		return it.axe > 0;
	}

	internal static bool IsHammer(Item it)
	{
		return it.hammer > 0;
	}

	internal static bool IsMelee(Item it)
	{
		if (!it.CountsAsClass<MeleeDamageClass>())
		{
			return it.CountsAsClass<MeleeNoSpeedDamageClass>();
		}
		return true;
	}

	internal static bool IsPickaxe(Item it)
	{
		return it.pick > 0;
	}

	internal static bool IsScalable(Item it)
	{
		if (it.damage > 0)
		{
			return IsMelee(it);
		}
		return false;
	}

	internal static bool IsUsable(Item it)
	{
		if (it.useStyle != 0 && it.useTime > 0)
		{
			return it.useAnimation > 0;
		}
		return false;
	}

	internal static bool UsesMana(Item it)
	{
		return IsUsable(it);
	}

	internal static bool UtilizesVelocity(Item it)
	{
		if (!IsUsable(it))
		{
			return it.ammo > AmmoID.None;
		}
		return true;
	}

	private static float CapAttackSpeed(float f)
	{
		return MathHelper.Clamp(f, BalancingConstants.MinimumAllowedAttackSpeed, BalancingConstants.MaximumAllowedAttackSpeed);
	}

	internal static IItemTweak AttackSpeedExact(float f)
	{
		return new AttackSpeedExactRule(f);
	}

	internal static IItemTweak AttackSpeedRatio(float f)
	{
		return new AttackSpeedRatioRule(f);
	}

	internal static IItemTweak AxePower(int a)
	{
		return new AxePowerRule(a);
	}

	internal static IItemTweak CritDelta(int d)
	{
		return new CritChanceDeltaRule(d);
	}

	internal static IItemTweak CritExact(int crit)
	{
		return new CritChanceExactRule(crit);
	}

	internal static IItemTweak DamageDelta(int d)
	{
		return new DamageDeltaRule(d);
	}

	internal static IItemTweak DamageExact(int d)
	{
		return new DamageExactRule(d);
	}

	internal static IItemTweak DamageRatio(float f)
	{
		return new DamageRatioRule(f);
	}

	internal static IItemTweak DefenseDelta(int d)
	{
		return new DefenseDeltaRule(d);
	}

	internal static IItemTweak DefenseExact(int d)
	{
		return new DefenseExactRule(d);
	}

	internal static IItemTweak HammerPower(int h)
	{
		return new HammerPowerRule(h);
	}

	internal static IItemTweak KnockbackDelta(float d)
	{
		return new KnockbackDeltaRule(d);
	}

	internal static IItemTweak KnockbackExact(float kb)
	{
		return new KnockbackExactRule(kb);
	}

	internal static IItemTweak KnockbackRatio(float r)
	{
		return new KnockbackRatioRule(r);
	}

	internal static IItemTweak ManaDelta(int d)
	{
		return new ManaDeltaRule(d);
	}

	internal static IItemTweak ManaExact(int m)
	{
		return new ManaExactRule(m);
	}

	internal static IItemTweak ManaRatio(float f)
	{
		return new ManaRatioRule(f);
	}

	internal static IItemTweak MaxStack(int stk)
	{
		return new MaxStackRule(stk);
	}

	internal static IItemTweak PickPower(int p)
	{
		return new PickPowerRule(p);
	}

	internal static IItemTweak ScaleDelta(float d)
	{
		return new ScaleDeltaRule(d);
	}

	internal static IItemTweak ScaleExact(float s)
	{
		return new ScaleExactRule(s);
	}

	internal static IItemTweak ScaleRatio(float f)
	{
		return new ScaleRatioRule(f);
	}

	internal static IItemTweak ShootSpeedDelta(float d)
	{
		return new ShootSpeedDeltaRule(d);
	}

	internal static IItemTweak ShootSpeedExact(float s)
	{
		return new ShootSpeedExactRule(s);
	}

	internal static IItemTweak ShootSpeedRatio(float f)
	{
		return new ShootSpeedRatioRule(f);
	}

	internal static IItemTweak TileBoostDelta(int d)
	{
		return new TileBoostDeltaRule(d);
	}

	internal static IItemTweak TileBoostExact(int tb)
	{
		return new TileBoostExactRule(tb);
	}

	internal static IItemTweak UseDelta(int d)
	{
		return new UseDeltaRule(d);
	}

	internal static IItemTweak UseExact(int ut)
	{
		return new UseExactRule(ut);
	}

	internal static IItemTweak UseRatio(float f)
	{
		return new UseRatioRule(f);
	}

	internal static IItemTweak UseAnimationDelta(int d)
	{
		return new UseAnimationDeltaRule(d);
	}

	internal static IItemTweak UseAnimationExact(int ua)
	{
		return new UseAnimationExactRule(ua);
	}

	internal static IItemTweak UseAnimationRatio(float f)
	{
		return new UseAnimationRatioRule(f);
	}

	internal static IItemTweak UseTimeDelta(int d)
	{
		return new UseTimeDeltaRule(d);
	}

	internal static IItemTweak UseTimeExact(int ut)
	{
		return new UseTimeExactRule(ut);
	}

	internal static IItemTweak UseTimeRatio(float f)
	{
		return new UseTimeRatioRule(f);
	}

	internal static IItemTweak ReuseDelayDelta(int d)
	{
		return new ReuseDelayDeltaRule(d);
	}

	internal static IItemTweak ReuseDelayExact(int rd)
	{
		return new ReuseDelayExactRule(rd);
	}

	internal static IItemTweak ReuseDelayRatio(float f)
	{
		return new ReuseDelayRatioRule(f);
	}

	internal static IItemTweak Value(int v)
	{
		return new ValueRule(v);
	}

	private void SetStaticDefaults_ShimmerRecipes()
	{
		int[] shimmerTransmute = ItemID.Sets.ShimmerTransformToItem;
		shimmerTransmute[ModContent.ItemType<AuricOre>()] = ModContent.ItemType<UelibloomOre>();
		shimmerTransmute[ModContent.ItemType<UelibloomOre>()] = ModContent.ItemType<ExodiumCluster>();
		shimmerTransmute[ModContent.ItemType<ExodiumCluster>()] = 3460;
		shimmerTransmute[ModContent.ItemType<AstralOre>()] = ModContent.ItemType<ScoriaOre>();
		shimmerTransmute[ModContent.ItemType<ScoriaOre>()] = ModContent.ItemType<PerennialOre>();
		shimmerTransmute[ModContent.ItemType<PerennialOre>()] = shimmerTransmute[3460];
		shimmerTransmute[ModContent.ItemType<HallowedOre>()] = shimmerTransmute[947];
		shimmerTransmute[ModContent.ItemType<AerialiteOre>()] = shimmerTransmute[364];
		shimmerTransmute[3460] = ModContent.ItemType<ScoriaOre>();
		shimmerTransmute[947] = ModContent.ItemType<HallowedOre>();
		shimmerTransmute[364] = ModContent.ItemType<AerialiteOre>();
		shimmerTransmute[ModContent.ItemType<InfernalSuevite>()] = 174;
		shimmerTransmute[ModContent.ItemType<CrawCarapace>()] = ModContent.ItemType<GiantShell>();
		shimmerTransmute[ModContent.ItemType<GiantShell>()] = ModContent.ItemType<CrawCarapace>();
		shimmerTransmute[ModContent.ItemType<LifeJelly>()] = ModContent.ItemType<CleansingJelly>();
		shimmerTransmute[ModContent.ItemType<CleansingJelly>()] = ModContent.ItemType<VitalJelly>();
		shimmerTransmute[ModContent.ItemType<VitalJelly>()] = ModContent.ItemType<LifeJelly>();
		shimmerTransmute[ModContent.ItemType<PolarisParrotfish>()] = ModContent.ItemType<GacruxianMollusk>();
		shimmerTransmute[ModContent.ItemType<GacruxianMollusk>()] = ModContent.ItemType<UrsaSergeant>();
		shimmerTransmute[ModContent.ItemType<UrsaSergeant>()] = ModContent.ItemType<PolarisParrotfish>();
	}

	static CalamityGlobalItem()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		ExhumedTooltipColor = new Color(198, 27, 64);
		EnchantmentEnergyParticles = new ChargingEnergyParticleSet(-1, 2, Color.DarkViolet, Color.White, 0.04f, 24f);
		cachedForgeID = -1;
		Rarity0BuyPrice = Item.buyPrice(0, 0, 50);
		Rarity1BuyPrice = Item.buyPrice(0, 1);
		Rarity2BuyPrice = Item.buyPrice(0, 2);
		Rarity3BuyPrice = Item.buyPrice(0, 5);
		Rarity4BuyPrice = Item.buyPrice(0, 10);
		Rarity5BuyPrice = Item.buyPrice(0, 20);
		Rarity6BuyPrice = Item.buyPrice(0, 35);
		Rarity7BuyPrice = Item.buyPrice(0, 45);
		Rarity8BuyPrice = Item.buyPrice(0, 60);
		Rarity9BuyPrice = Item.buyPrice(0, 80);
		Rarity10BuyPrice = Item.buyPrice(1);
		Rarity11BuyPrice = Item.buyPrice(1, 20);
		Rarity12BuyPrice = Item.buyPrice(1, 50);
		Rarity13BuyPrice = Item.buyPrice(1, 75);
		Rarity14BuyPrice = Item.buyPrice(2);
		Rarity15BuyPrice = Item.buyPrice(2, 40);
		Rarity16BuyPrice = Item.buyPrice(2, 80);
		Rarity17BuyPrice = Item.buyPrice(3, 20);
		RarityBuyPriceArray = new int[18]
		{
			Rarity0BuyPrice, Rarity1BuyPrice, Rarity2BuyPrice, Rarity3BuyPrice, Rarity4BuyPrice, Rarity5BuyPrice, Rarity6BuyPrice, Rarity7BuyPrice, Rarity8BuyPrice, Rarity9BuyPrice,
			Rarity10BuyPrice, Rarity11BuyPrice, Rarity12BuyPrice, Rarity13BuyPrice, Rarity14BuyPrice, Rarity15BuyPrice, Rarity16BuyPrice, Rarity17BuyPrice
		};
		MainTooltipBackupInsertionPositions = new string[23]
		{
			"Material", "Consumable", "Ammo", "Placeable", "UseMana", "HealMana", "HealLife", "TileBoost", "HammerPower", "AxePower",
			"PickPower", "Defense", "Vanity", "Quest", "WandConsumes", "Equipable", "BaitPower", "NeedsBait", "FishingPower", "Knockback",
			"NoTransfer", "FavoriteDesc", "ItemName"
		};
		RevTooltipInsertionPositions = new string[21]
		{
			"Expert", "SetBonus", "CalamityMod:PrefixAccStealthGen", "PrefixAccMeleeSpeed", "PrefixAccMoveSpeed", "PrefixAccDamage", "PrefixAccCritChance", "PrefixAccMaxMana", "PrefixAccDefense", "CalamityMod:PrefixStealthDamage",
			"PrefixKnockback", "PrefixShootSpeed", "PrefixSize", "PrefixUseMana", "PrefixCritChance", "PrefixSpeed", "PrefixDamage", "OneDropLogo", "BuffTime", "WellFedExpert",
			"EtherianManaWarning"
		};
		SpeedTooltips = new Dictionary<int, LocalizedText>
		{
			{
				5,
				Language.GetText("LegacyTooltip.6")
			},
			{
				9,
				Language.GetText("LegacyTooltip.7")
			},
			{
				14,
				Language.GetText("LegacyTooltip.8")
			},
			{
				22,
				Language.GetText("LegacyTooltip.9")
			},
			{
				29,
				Language.GetText("LegacyTooltip.10")
			},
			{
				37,
				Language.GetText("LegacyTooltip.11")
			},
			{
				45,
				Language.GetText("LegacyTooltip.12")
			},
			{
				int.MaxValue,
				Language.GetText("LegacyTooltip.13")
			}
		};
		currentTweaks = null;
	}
}
