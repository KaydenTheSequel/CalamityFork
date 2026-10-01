using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Empyrean;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "XerocMask" })]
public class EmpyreanMask : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.12f;

	public static int RogueCritBoost = 7;

	public static float RogueVelocityBoost = 0.1f;

	public static float SetBonusRogueStealth = 1.15f;

	public static int WrathDuration = CalamityUtils.SecondsToFrames(3);

	public static float PermanentWrathHealthRatio = 0.5f;

	public static float WrathRogueDamageBoost = 0.1f;

	public static int WrathRogueCritBoost = 5;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, RogueVelocityBoost.ToPercent());

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/MeldTransformation_Head", EquipType.Head, null, "MeldTransformation");
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/MeldTransformation_Body", EquipType.Body, null, "MeldTransformation");
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/MeldTransformation_Neck", EquipType.Neck, null, "MeldTransformation");
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/MeldTransformation_Legs", EquipType.Legs, null, "MeldTransformation");
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlotHead = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Head);
			int equipSlotBody = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Body);
			int equipSlotLegs = EquipLoader.GetEquipSlot(base.Mod, "MeldTransformation", EquipType.Legs);
			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.defense = 16;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<EmpyreanCloak>())
		{
			return legs.type == ModContent.ItemType<EmpyreanCuisses>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawOutlines = true;
		player.Calamity().meldTransformation = true;
		player.Calamity().meldTransformationForce = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.xerocSet = true;
		modPlayer.rogueStealthMax += SetBonusRogueStealth;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth());
		if (player.statLife <= (int)((float)player.statLifeMax2 * PermanentWrathHealthRatio))
		{
			player.AddBuff(ModContent.BuffType<EmpyreanWrath>(), 2);
		}
		modPlayer.wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.Calamity().rogueVelocity += RogueVelocityBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(10).AddIngredient(3467, 8).AddTile(412)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<EmpyreanCloak>())
			.Register();
	}
}
