using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class EyeoftheStorm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
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
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().cloudElemental = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<CloudElemental>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<CloudElemental>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CloudElementalMinion>()] < 1)
			{
				Projectile.NewProjectileDirect(player.GetSource_Accessory(base.Item), damage: (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(45f), position: player.Center, velocity: -Vector2.UnitY, type: ModContent.ProjectileType<CloudElementalMinion>(), knockback: 2f, owner: Main.myPlayer).originalDamage = 45;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().cloudElementalVanity = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<CloudElemental>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<CloudElemental>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CloudElementalMinion>()] < 1)
			{
				IEntitySource source_Accessory = player.GetSource_Accessory(base.Item);
				int baseDamage = 45;
				Projectile.NewProjectileDirect(damage: (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage), spawnSource: source_Accessory, position: player.Center, velocity: -Vector2.UnitY, type: ModContent.ProjectileType<CloudElementalMinion>(), knockback: 2f, owner: Main.myPlayer).originalDamage = baseDamage;
			}
		}
	}
}
