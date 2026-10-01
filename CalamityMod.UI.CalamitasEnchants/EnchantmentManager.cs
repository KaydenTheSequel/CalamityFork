using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Other;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.UI.CalamitasEnchants;

public sealed class EnchantmentManager : ModSystem
{
	internal const int ClearEnchantmentID = -18591774;

	internal const string ExhumedNamePath = "UI.Exhumed.DisplayName";

	public static List<Enchantment> EnchantmentList { get; internal set; } = new List<Enchantment>();

	public static Dictionary<int, int> ItemUpgradeRelationship { get; internal set; } = new Dictionary<int, int>();

	public static Enchantment ClearEnchantment { get; internal set; }

	public static IEnumerable<Enchantment> GetValidEnchantmentsForItem(Item item)
	{
		if (item == null || item.IsAir || !item.CanBeEnchantedBySomething() || ItemUpgradeRelationship.ContainsValue(item.type))
		{
			yield break;
		}
		if (item.Calamity().AppliedEnchantment.HasValue)
		{
			yield return ClearEnchantment;
			yield break;
		}
		foreach (Enchantment enchantment in EnchantmentList)
		{
			if ((!item.Calamity().AppliedEnchantment.HasValue || !item.Calamity().AppliedEnchantment.Value.Equals(enchantment)) && enchantment.ApplyRequirement(item))
			{
				yield return enchantment;
			}
		}
	}

	public static Enchantment? FindByID(int id)
	{
		Enchantment? enchantment = EnchantmentList.FirstOrDefault((Enchantment enchant) => enchant.ID == id);
		if (enchantment.HasValue && !enchantment.Value.Equals(default(Enchantment)))
		{
			return enchantment;
		}
		return null;
	}

	public static void ConstructFromModcall(IEnumerable<object> parameters)
	{
		int secondaryArgumentCount = parameters.Count();
		if (secondaryArgumentCount < 5)
		{
			throw new ArgumentNullException("ERROR: A minimum of 4 arguments must be supplied to this command; a name, a description, an id, and a requirement predicate.");
		}
		if (secondaryArgumentCount > 7)
		{
			throw new ArgumentNullException("ERROR: A maximum of 6 arguments can be supplied to this command.");
		}
		LocalizedText name = LocalizedText.Empty;
		LocalizedText description = LocalizedText.Empty;
		int id = -1;
		Predicate<Item> requirement = null;
		Action<Item> creationEffect = null;
		Action<Player> holdEffect = null;
		if (parameters.ElementAt(0) is LocalizedText nameElement)
		{
			name = nameElement;
			if (parameters.ElementAt(1) is LocalizedText descriptionElement)
			{
				description = descriptionElement;
				if (parameters.ElementAt(2) is int idElement)
				{
					id = idElement;
					if (parameters.ElementAt(3) is Predicate<Item> requirementElement)
					{
						requirement = requirementElement;
						if (!(parameters.ElementAt(4) is string iconTexturePathElement))
						{
							throw new ArgumentException("The fifth argument to this command must be a string.");
						}
						switch (secondaryArgumentCount)
						{
						case 6:
						{
							object sixthElement = parameters.ElementAt(5);
							if (sixthElement is Action<Item> creationElement3)
							{
								creationEffect = creationElement3;
								break;
							}
							if (sixthElement is Action<Player> holdElement3)
							{
								holdEffect = holdElement3;
								break;
							}
							throw new ArgumentException("The sixth argument to this command must be an Item or Player Action.");
						}
						case 7:
						{
							object sixthElement = parameters.ElementAt(5);
							object seventhElement = parameters.ElementAt(6);
							if (sixthElement is Action<Item> creationElement2)
							{
								creationEffect = creationElement2;
								holdEffect = seventhElement as Action<Player>;
								break;
							}
							if (sixthElement is Action<Player> holdElement2)
							{
								creationEffect = seventhElement as Action<Item>;
								holdEffect = holdElement2;
								break;
							}
							throw new ArgumentException("The sixth argument to this command must be an Item or Player Action and the sixth must be the other action type.");
						}
						}
						if (EnchantmentList.Any((Enchantment enchant) => enchant.ID == id) || id == -18591774)
						{
							throw new ArgumentException("An enchantment with this ID already exists. Another one must be specified.");
						}
						EnchantmentList.Add(new Enchantment(name, description, id, iconTexturePathElement, creationEffect, holdEffect, requirement));
						return;
					}
					throw new ArgumentException("The fourth argument to this command must be an Item Predicate.");
				}
				throw new ArgumentException("The third argument to this command must be an int.");
			}
			throw new ArgumentException("The second argument to this command must be a LocalizedText.");
		}
		throw new ArgumentException("The first argument to this command must be a LocalizedText.");
	}

	public override void OnModLoad()
	{
		EnchantmentList = new List<Enchantment>
		{
			new Enchantment(CalamityUtils.GetText("UI.Exhumed.DisplayName"), CalamityUtils.GetText("UI.Exhumed.Description"), 1, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Exhumed", null, null, (Item item) => ItemUpgradeRelationship.ContainsKey(item.type)),
			new Enchantment(CalamityUtils.GetText("UI.Indignant.DisplayName"), CalamityUtils.GetText("UI.Indignant.Description"), 100, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Indignant", null, delegate(Player player)
			{
				player.Calamity().cursedSummonsEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.CountsAsClass<SummonDamageClass>() && !item.IsWhip()),
			new Enchantment(CalamityUtils.GetText("UI.Aflame.DisplayName"), CalamityUtils.GetText("UI.Aflame.Description"), 200, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Aflame", null, delegate(Player player)
			{
				player.Calamity().flamingItemEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && !item.CountsAsClass<SummonDamageClass>() && !item.IsWhip()),
			new Enchantment(CalamityUtils.GetText("UI.Oblatory.DisplayName"), CalamityUtils.GetText("UI.Oblatory.Description"), 300, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Oblatory", delegate(Item item)
			{
				item.damage = (int)((double)item.damage * 1.25);
				item.mana = (int)Math.Ceiling((double)item.mana * 0.7);
			}, delegate(Player player)
			{
				player.Calamity().lifeManaEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.CountsAsClass<MagicDamageClass>() && item.mana > 0 && item.type != ModContent.ItemType<Eternity>()),
			new Enchantment(CalamityUtils.GetText("UI.Resentful.DisplayName"), CalamityUtils.GetText("UI.Resentful.Description"), 400, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Resentful", null, delegate(Player player)
			{
				player.Calamity().farProximityRewardEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.shoot > 0 && !item.IsTrueMelee() && item.type != ModContent.ItemType<TheFinalDawn>()),
			new Enchantment(CalamityUtils.GetText("UI.Bloodthirsty.DisplayName"), CalamityUtils.GetText("UI.Bloodthirsty.Description"), 500, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Bloodthirsty", null, delegate(Player player)
			{
				player.Calamity().closeProximityRewardEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.shoot > 0 && !item.IsTrueMelee() && item.type != ModContent.ItemType<TheFinalDawn>()),
			new Enchantment(CalamityUtils.GetText("UI.Ephemeral.DisplayName"), CalamityUtils.GetText("UI.Ephemeral.Description"), 600, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Ephemeral", null, delegate(Player player)
			{
				player.Calamity().dischargingItemEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && !item.CountsAsClass<SummonDamageClass>() && !item.CountsAsClass<RogueDamageClass>() && !item.channel && item.type != ModContent.ItemType<HeavenlyGale>()),
			new Enchantment(CalamityUtils.GetText("UI.Hellbound.DisplayName"), CalamityUtils.GetText("UI.Hellbound.Description"), 700, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Hellbound", null, delegate(Player player)
			{
				player.Calamity().explosiveMinionsEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.CountsAsClass<SummonDamageClass>() && !item.IsWhip()),
			new Enchantment(CalamityUtils.GetText("UI.Tainted.DisplayName"), CalamityUtils.GetText("UI.Tainted.Description"), 800, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Tainted", delegate(Item item)
			{
				item.useAnimation = (item.useTime = 25);
			}, delegate(Player player)
			{
				//IL_0099: Unknown result type (might be due to invalid IL or missing references)
				//IL_009e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0107: Unknown result type (might be due to invalid IL or missing references)
				//IL_010c: Unknown result type (might be due to invalid IL or missing references)
				if (!Main.gameMenu)
				{
					player.Calamity().bladeArmEnchant = true;
					bool flag = false;
					int num = ModContent.ProjectileType<TaintedBladeSlasher>();
					ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Projectile current = enumerator.Current;
						if (current.type == num && current.owner == player.whoAmI)
						{
							flag = true;
							break;
						}
					}
					if (Main.myPlayer == player.whoAmI && !flag)
					{
						IEntitySource source_ItemUse = player.GetSource_ItemUse(player.HeldItem);
						float num2 = 5f;
						int num3 = (int)((float)player.HeldItem.damage * num2);
						Projectile projectile = Projectile.NewProjectileDirect(source_ItemUse, player.Center, Vector2.Zero, ModContent.ProjectileType<TaintedBladeSlasher>(), num3, 0f, player.whoAmI, 0f, player.HeldItem.type);
						projectile.localAI[0] = 0f;
						projectile.originalDamage = num3;
						projectile.OriginalArmorPenetration = player.HeldItem.ArmorPenetration;
						projectile.OriginalCritChance = player.HeldItem.crit;
						Projectile projectile2 = Projectile.NewProjectileDirect(source_ItemUse, player.Center, Vector2.Zero, ModContent.ProjectileType<TaintedBladeSlasher>(), num3, 0f, player.whoAmI, 1f, player.HeldItem.type);
						projectile2.localAI[0] = -80f;
						projectile2.originalDamage = num3;
						projectile2.OriginalArmorPenetration = player.HeldItem.ArmorPenetration;
						projectile2.OriginalCritChance = player.HeldItem.crit;
					}
				}
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.CountsAsClass<MeleeDamageClass>() && !item.noUseGraphic && item.shoot > 0),
			new Enchantment(CalamityUtils.GetText("UI.Traitorous.DisplayName"), CalamityUtils.GetText("UI.Traitorous.Description"), 900, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Traitorous", null, delegate(Player player)
			{
				player.Calamity().manaMonsterEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.CountsAsClass<MagicDamageClass>() && item.mana > 0),
			new Enchantment(CalamityUtils.GetText("UI.Withering.DisplayName"), CalamityUtils.GetText("UI.Withering.Description"), 1000, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Withered", null, delegate(Player player)
			{
				player.Calamity().witheringWeaponEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && !item.CountsAsClass<SummonDamageClass>()),
			new Enchantment(CalamityUtils.GetText("UI.Persecuted.DisplayName"), CalamityUtils.GetText("UI.Persecuted.Description"), 1100, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Persecuted", null, delegate(Player player)
			{
				player.Calamity().persecutedEnchant = true;
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.shoot > 0),
			new Enchantment(CalamityUtils.GetText("UI.Lecherous.DisplayName"), CalamityUtils.GetText("UI.Lecherous.Description"), 1200, "CalamityMod/UI/CalamitasEnchantments/CurseIcon_Lecherous", null, delegate(Player player)
			{
				//IL_0085: Unknown result type (might be due to invalid IL or missing references)
				if (!Main.gameMenu)
				{
					player.Calamity().lecherousOrbEnchant = true;
					bool flag = false;
					int num = ModContent.NPCType<LecherousOrb>();
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC current = enumerator.Current;
						if (current.type == num && current.target == player.whoAmI)
						{
							flag = true;
							break;
						}
					}
					if (Main.myPlayer == player.whoAmI && !flag && !player.Calamity().awaitingLecherousOrbSpawn)
					{
						player.Calamity().awaitingLecherousOrbSpawn = true;
						CalamityNetcode.NewNPC_ClientSide(player.Center, num, player);
					}
				}
			}, (Item item) => item.IsEnchantable() && item.damage > 0 && item.shoot > 0 && !item.CountsAsClass<SummonDamageClass>() && !item.IsTrueMelee())
		};
		ClearEnchantment = new Enchantment(CalamityUtils.GetText("UI.Disenchant"), LocalizedText.Empty, -18591774, null, delegate(Item item)
		{
			item.Calamity().AppliedEnchantment = null;
			item.Calamity().DischargeEnchantExhaustion = 0f;
		}, (Item item) => item.IsEnchantable() && item.shoot >= 0);
		ItemUpgradeRelationship = new Dictionary<int, int>
		{
			[ModContent.ItemType<TheCommunity>()] = ModContent.ItemType<ShatteredCommunity>(),
			[ModContent.ItemType<EntropysVigil>()] = ModContent.ItemType<CindersOfLament>(),
			[ModContent.ItemType<VoidEaterMarionette>()] = ModContent.ItemType<Metastasis>(),
			[ModContent.ItemType<GhastlyVisage>()] = ModContent.ItemType<GruesomeEminence>(),
			[ModContent.ItemType<BurningSea>()] = ModContent.ItemType<Rancor>()
		};
	}

	public override void Unload()
	{
		EnchantmentList = null;
		ItemUpgradeRelationship = null;
	}
}
