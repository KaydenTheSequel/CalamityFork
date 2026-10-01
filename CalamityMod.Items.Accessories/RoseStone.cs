using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RoseStone : ModItem, ILocalizedModType, IModType
{
	public static int ElementalDamage = 60;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().brimElemental = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_Accessory(base.Item);
		if (player.FindBuffIndex(ModContent.BuffType<BrimstoneElemental>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<BrimstoneElemental>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<BrimstoneElementalMinion>()] < 1)
		{
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
			int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<BrimstoneElementalMinion>(), damage, 2f, Main.myPlayer);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = ElementalDamage;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().brimElementalVanity = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_Accessory(base.Item);
		if (player.FindBuffIndex(ModContent.BuffType<BrimstoneElemental>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<BrimstoneElemental>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<BrimstoneElementalMinion>()] < 1)
		{
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
			int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<BrimstoneElementalMinion>(), damage, 2f, Main.myPlayer);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = ElementalDamage;
			}
		}
	}
}
