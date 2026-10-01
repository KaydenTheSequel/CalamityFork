using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class DarkPlasma : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(7, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 108;
	}

	public override void SetDefaults()
	{
		base.Item.width = 15;
		base.Item.height = 12;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1, 40);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		float brightness = (float)Main.rand.Next(90, 111) * 0.01f;
		brightness *= Main.essScale;
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0f * brightness, 0.45f * brightness, 0.7f * brightness);
	}
}
