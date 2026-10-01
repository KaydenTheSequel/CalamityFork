using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "Phantoplasm", "Polterplasm" })]
public class Necroplasm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 110;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 1508;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 52;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 60);
		base.Item.rare = 11;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 0);
	}
}
