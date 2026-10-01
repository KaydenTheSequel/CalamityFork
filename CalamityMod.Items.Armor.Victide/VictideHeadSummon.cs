using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Victide;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "VictideHelmet" })]
public class VictideHeadSummon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 1;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<VictideBreastplate>())
		{
			return legs.type == ModContent.ItemType<VictideGreaves>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<VictideBreastplate>("CommonSetBonus");
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.victideSet = true;
		calamityPlayer.victideSummoner = true;
		player.maxMinions++;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<SeaSnailBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<SeaSnailBuff>(), 3600);
			}
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<VictideSeaSnail>()] < 1)
			{
				int baseDamage = 7;
				int minionDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(7f);
				int p = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<VictideSeaSnail>(), minionDamage, 0f, Main.myPlayer);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = baseDamage;
				}
			}
		}
		player.ignoreWater = true;
		if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
		{
			player.GetDamage<SummonDamageClass>() += 0.1f;
			player.lifeRegen += 3;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<SummonDamageClass>() += 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(3).AddTile(16).Register();
	}
}
