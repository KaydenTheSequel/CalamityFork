using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public class EnchantedStarfish : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 10;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 21;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5339;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.useAnimation = 30;
		base.Item.useTime = 30;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item29;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.autoReuse = true;
		base.Item.consumable = true;
		base.Item.value = Item.sellPrice(0, 0, 25);
		base.Item.rare = 2;
	}

	public override bool? UseItem(Player player)
	{
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			if (player.ConsumedManaCrystals >= 9)
			{
				return null;
			}
			player.UseManaMaxIncreasingItem(20);
			player.ConsumedManaCrystals++;
			AchievementsHelper.HandleSpecialEvent(player, 1);
		}
		return true;
	}
}
