using CalamityMod.Rarities;
using CalamityMod.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class CosmiliteBar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 114;
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(6, 10));
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CosmiliteBarTile>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 0.5f * brightness, 0f, 0.5f * brightness);
	}
}
