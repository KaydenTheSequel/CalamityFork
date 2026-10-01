using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class UnholyEssence : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 7));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 103;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 36;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 40);
		base.Item.rare = 11;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		float brightness = (float)Main.rand.Next(90, 111) * 0.01f;
		brightness *= Main.essScale;
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0.45f * brightness, 0.3f * brightness, 0f * brightness);
	}
}
