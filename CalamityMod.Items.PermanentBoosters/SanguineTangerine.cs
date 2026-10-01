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

[LegacyName(new string[] { "BloodOrange" })]
public class SanguineTangerine : ModItem, ILocalizedModType, IModType
{
	public const int LifeBoost = 25;

	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/BloodOrangeConsume");

	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 40;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = UseSound;
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 16);
		base.Item.rare = 6;
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().sTangerine;
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
				Main.NewText(color: Color.Orange, o: Language.GetTextValue("Mods.CalamityMod.Misc.SanguineTangerineText"));
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
			if (modPlayer.sTangerine)
			{
				return null;
			}
			player.UseHealthMaxIncreasingItem(25);
			modPlayer.sTangerine = true;
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
		CreateRecipe().AddIngredient(1291).AddIngredient<BloodOrb>(10).AddIngredient(547, 5)
			.AddIngredient(548, 5)
			.AddIngredient(549, 5)
			.AddTile(134)
			.Register();
	}
}
