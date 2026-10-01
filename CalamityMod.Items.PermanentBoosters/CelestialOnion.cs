using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.PermanentBoosters;

[LegacyName(new string[] { "MLGRune2" })]
public class CelestialOnion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Misc";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.consumable = true;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = SoundID.Item4;
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 10;
	}

	public static bool HasConsumedBefore(Player player)
	{
		if (!Main.masterMode || !player.extraAccessory)
		{
			return player.Calamity().extraAccessoryML;
		}
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (HasConsumedBefore(player))
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.NewText(color: Color.LightSlateGray, o: Language.GetTextValue("Mods.CalamityMod.Misc.CelestialOnionText"));
			}
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (Main.masterMode)
		{
			if (player.itemAnimation > 0 && !player.extraAccessory && player.itemTime == 0)
			{
				player.itemTime = base.Item.useTime;
				player.extraAccessory = true;
			}
		}
		else if (player.itemAnimation > 0 && !modPlayer.extraAccessoryML && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			modPlayer.extraAccessoryML = true;
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
}
