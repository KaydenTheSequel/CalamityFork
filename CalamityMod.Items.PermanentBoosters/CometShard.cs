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

public class CometShard : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/CometShardUse");

	public const int ManaBoost = 50;

	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(50);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 21;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 46;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 6);
		base.Item.rare = 4;
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().cShard;
	}

	public override bool CanUseItem(Player player)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (player.ConsumedManaCrystals != 9)
		{
			return false;
		}
		if (HasConsumedBefore(player))
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.NewText(color: Color.SkyBlue, o: Language.GetTextValue("Mods.CalamityMod.Misc.CometShardText"));
			}
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in UseSound, player.Center);
		CalamityPlayer modPlayer = player.Calamity();
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			if (modPlayer.cShard)
			{
				return null;
			}
			player.UseManaMaxIncreasingItem(50);
			modPlayer.cShard = true;
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
		CreateRecipe().AddIngredient(117, 10).AddIngredient(75, 10).AddIngredient<StarblightSoot>(50)
			.AddTile(16)
			.Register();
	}
}
