using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.PermanentBoosters;

public class MiracleFruit : ModItem, ILocalizedModType, IModType
{
	public const int LifeBoost = 25;

	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/MiracleFruitConsume");

	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 36;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = UseSound;
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 24);
		base.Item.rare = 8;
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().mFruit;
	}

	public override bool CanUseItem(Player player)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (player.ConsumedLifeFruit != 20)
		{
			return false;
		}
		if (HasConsumedBefore(player))
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.NewText(color: Color.DeepSkyBlue, o: Language.GetTextValue("Mods.CalamityMod.Misc.MiracleFruitText"));
			}
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			if (modPlayer.mFruit)
			{
				return null;
			}
			player.UseHealthMaxIncreasingItem(25);
			modPlayer.mFruit = true;
		}
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (HasConsumedBefore(Main.LocalPlayer))
		{
			list.AddConsumedTooltip();
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1291).AddIngredient<LifeAlloy>(5).AddIngredient<LivingShard>(12)
			.AddTile(134)
			.Register();
	}
}
