using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Aerospec;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AerospecHelmet" })]
public class AerospecHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static float SummonDamageBoost = 0.1f;

	public static float MoveSpeedBoost = 0.05f;

	public static int SetBonusMinionSlotBoost = 1;

	public static float SetBonusSummonDamageBoost = 0.11f;

	public static int ValkyrieDamage = 20;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AerospecBreastplate>())
		{
			return legs.type == ModContent.ItemType<AerospecLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), AerospecBreastplate.SetBonusHurtDamageThreshold);
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.valkyrie = true;
		calamityPlayer.aeroSet = true;
		player.noFallDmg = true;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<ValkyrieBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<ValkyrieBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Valkyrie>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ValkyrieDamage);
				int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Valkyrie>(), damage, 0f, Main.myPlayer);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = ValkyrieDamage;
				}
			}
		}
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
		player.maxMinions += SetBonusMinionSlotBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += MoveSpeedBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(5).AddIngredient(824, 3).AddIngredient(320)
			.AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AerospecHeadRogue>())
			.Register();
	}
}
