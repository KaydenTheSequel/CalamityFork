using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Mollusk;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class MolluskShellmet : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.05f;

	public static int CritBoost = 4;

	public static float SetBonusDR = 0.1f;

	public static int ShellfishDamage = 140;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 15;
	}

	public override void UpdateEquip(Player player)
	{
		player.ignoreWater = true;
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.Calamity().molluskHelmet = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<MolluskShellplate>())
		{
			return legs.type == ModContent.ItemType<MolluskShelleggings>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusDR.ToPercent());
		player.endurance += SetBonusDR;
		player.maxMinions += 4;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<ShellfishBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<ShellfishBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Shellfish>()] < 2)
			{
				Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<Shellfish>(), ShellfishDamage, 0f, player.whoAmI).originalDamage = ShellfishDamage;
			}
		}
		player.Calamity().wearingRogueArmor = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MolluskHusk>(6).AddIngredient<SeaPrism>(15).AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<MolluskShelleggings>())
			.Register();
	}
}
