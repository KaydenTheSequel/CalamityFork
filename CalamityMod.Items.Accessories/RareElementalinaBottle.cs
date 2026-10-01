using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "WifeinaBottlewithBoobs" })]
public class RareElementalinaBottle : ModItem, ILocalizedModType, IModType
{
	public static int ElementalDamage = 45;

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
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().rareSandElemental = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<RareSandElemental>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<RareSandElemental>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SandElementalHealer>()] < 1)
			{
				Projectile.NewProjectileDirect(player.GetSource_Accessory(base.Item), damage: (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage), position: player.Center, velocity: -Vector2.UnitY, type: ModContent.ProjectileType<SandElementalHealer>(), knockback: 2f, owner: Main.myPlayer).originalDamage = ElementalDamage;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().rareSandElementalVanity = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<RareSandElemental>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<RareSandElemental>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SandElementalHealer>()] < 1)
			{
				Projectile.NewProjectileDirect(player.GetSource_Accessory(base.Item), damage: (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage), position: player.Center, velocity: -Vector2.UnitY, type: ModContent.ProjectileType<SandElementalHealer>(), knockback: 2f, owner: Main.myPlayer).originalDamage = ElementalDamage;
			}
		}
	}
}
