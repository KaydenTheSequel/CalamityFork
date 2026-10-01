using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "WifeinaBottle" })]
public class ElementalinaBottle : ModItem, ILocalizedModType, IModType
{
	public const int ElementalDamage = 45;

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
		return !player.Calamity().elementalHeart;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().sandElemental = true;
		SpawnElemental(player);
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().sandElementalVanity = true;
		SpawnElemental(player);
	}

	public void SpawnElemental(Player player)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(ModContent.BuffType<SandElemental>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<SandElemental>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SandElementalMinion>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(45f);
				Projectile.NewProjectileDirect(player.GetSource_Accessory(base.Item), player.Center, -Vector2.UnitY, ModContent.ProjectileType<SandElementalMinion>(), damage, 2f, Main.myPlayer).originalDamage = 45;
			}
		}
	}
}
