using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Fearmonger;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class FearmongerGreathelm : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 60;

	public static float ManaCostReduction = 0.1f;

	public static float SummonDamageBoost = 0.2f;

	public static int SetBonusMinionSlotBoost = 2;

	public static int RegenBoostDurationPerHit = CalamityUtils.SecondsToFrames(0.17f);

	public static int RegenBoostDurationLimit = CalamityUtils.SecondsToFrames(1.5f);

	public static int MinionRegenBoost = 5;

	public static int MinionRegenTimeBoost = 4;

	public static int MinionRegenTimeFloor = 900;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 35;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<FearmongerPlateMail>())
		{
			return legs.type == ModContent.ItemType<FearmongerGreaves>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, MinionRegenBoost.ToRegenPerSecond());
		player.Calamity().fearmongerSet = true;
		player.Calamity().wearingRogueArmor = true;
		player.Calamity().WearingPostMLSummonerSet = true;
		player.maxMinions += SetBonusMinionSlotBoost;
		int[] obj = new int[13]
		{
			24, 44, 39, 153, 0, 67, 0, 0, 0, 0,
			0, 46, 47
		};
		obj[4] = ModContent.BuffType<Daybroken>();
		obj[6] = ModContent.BuffType<Shadowflame>();
		obj[7] = ModContent.BuffType<BrimstoneFlames>();
		obj[8] = ModContent.BuffType<HolyFlames>();
		obj[9] = ModContent.BuffType<Voidfrost>();
		obj[10] = ModContent.BuffType<GodSlayerInferno>();
		int[] immuneDebuffs = obj;
		for (int i = 0; i < immuneDebuffs.Length; i++)
		{
			player.buffImmune[immuneDebuffs[i]] = true;
		}
		Lighting.AddLight(player.Center, 0.3f, 0.18f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1832).AddIngredient<CosmiliteBar>(8).AddIngredient<AscendantSpiritEssence>(2)
			.AddIngredient(547, 8)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
