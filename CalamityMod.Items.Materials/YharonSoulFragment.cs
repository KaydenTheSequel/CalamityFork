using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "HellcasterFragment" })]
public class YharonSoulFragment : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		float brightness = (float)Main.rand.Next(90, 111) * 0.01f;
		brightness *= Main.essScale;
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0.5f * brightness, 0.3f * brightness, 0.05f * brightness);
	}

	public override void SetDefaults()
	{
		base.Item.width = 10;
		base.Item.height = 14;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}
}
