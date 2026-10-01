using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AtaxiaHelmet" })]
public class HydrothermicHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.1f;

	public static int SetBonusMinionSlotBoost = 1;

	public static float SetBonusSummonDamageBoost = 0.25f;

	public static int VentDamage = 190;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 6;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<HydrothermicArmor>())
		{
			return legs.type == ModContent.ItemType<HydrothermicSubligar>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlines = true;
		player.Calamity().hydrothermalSmoke = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), HydrothermicArmor.InfernoHealthThreshold.ToPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.ataxiaBlaze = true;
		calamityPlayer.chaosSpirit = true;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<HydrothermicVentBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<HydrothermicVentBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HydrothermicVent>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(VentDamage);
				int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<HydrothermicVent>(), damage, 0f, Main.myPlayer, 38f);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = VentDamage;
				}
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
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddIngredient<EssenceofHavoc>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<HydrothermicHeadRogue>())
			.Register();
	}
}
