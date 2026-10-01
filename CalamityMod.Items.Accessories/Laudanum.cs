using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Laudanum : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(195, 223, 255);
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<HeartofDarkness>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
		base.Item.SetRevExclusive();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().laudanum = true;
	}
}
