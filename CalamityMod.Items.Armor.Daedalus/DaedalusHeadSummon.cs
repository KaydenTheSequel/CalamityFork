using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "DaedalusHeadgear" })]
public class DaedalusHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.1f;

	public static int SetBonusMinionSlotBoost = 1;

	public static float SetBonusSummonDamageBoost = 0.1f;

	public static int CrystalDamage = 95;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 3;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DaedalusBreastplate>())
		{
			return legs.type == ModContent.ItemType<DaedalusLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent());
		player.Calamity().daedalusCrystal = true;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<DaedalusCrystalBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<DaedalusCrystalBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DaedalusCrystal>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(CrystalDamage);
				Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<DaedalusCrystal>(), damage, 0f, Main.myPlayer, 50f).originalDamage = CrystalDamage;
			}
		}
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(7).AddIngredient<EssenceofEleum>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DaedalusHeadRogue>())
			.Register();
	}
}
