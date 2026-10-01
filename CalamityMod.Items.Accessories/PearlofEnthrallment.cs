using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "LureofEnthrallment" })]
public class PearlofEnthrallment : ModItem, ILocalizedModType, IModType
{
	public static int ElementalDamage = 65;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 56;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		if (player.Calamity().elementalHeart)
		{
			return false;
		}
		return true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().waterElemental = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_Accessory(base.Item);
		if (player.FindBuffIndex(ModContent.BuffType<WaterElemental>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<WaterElemental>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<WaterElementalMinion>()] < 1)
		{
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
			int anahita = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<WaterElementalMinion>(), damage, 2f, Main.myPlayer);
			if (Main.projectile.IndexInRange(anahita))
			{
				Main.projectile[anahita].originalDamage = ElementalDamage;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().waterElementalVanity = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_Accessory(base.Item);
		if (player.FindBuffIndex(ModContent.BuffType<WaterElemental>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<WaterElemental>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<WaterElementalMinion>()] < 1)
		{
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
			int anahita = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<WaterElementalMinion>(), damage, 2f, Main.myPlayer);
			if (Main.projectile.IndexInRange(anahita))
			{
				Main.projectile[anahita].originalDamage = ElementalDamage;
			}
		}
	}
}
