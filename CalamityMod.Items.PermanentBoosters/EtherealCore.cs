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

public class EtherealCore : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/EtherealCoreUse");

	public const int ManaBoost = 50;

	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(50);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 21;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 44;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 18);
		base.Item.rare = 10;
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().eCore;
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
				Main.NewText(color: Color.MediumVioletRed, o: Language.GetTextValue("Mods.CalamityMod.Misc.EtherealCoreText"));
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
			if (modPlayer.eCore)
			{
				return null;
			}
			player.UseManaMaxIncreasingItem(50);
			modPlayer.eCore = true;
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
		CreateRecipe().AddIngredient<AstralBar>(10).AddIngredient(3457, 20).AddIngredient(75, 20)
			.AddTile(412)
			.Register();
	}
}
