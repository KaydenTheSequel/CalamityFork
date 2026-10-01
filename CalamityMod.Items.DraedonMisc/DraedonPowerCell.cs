using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

[LegacyName(new string[] { "PowerCell" })]
public class DraedonPowerCell : ModItem, ILocalizedModType, IModType
{
	public const float ChargeValue = 1f;

	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ExtractinatorMode[base.Type] = base.Item.type;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 14;
		base.Item.rare = ModContent.RarityType<DarkOrange>();
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.MakeUsableWithChlorophyteExtractinator();
		base.Item.useTime = 2;
		base.Item.value = Item.sellPrice(0, 0, 0, 10);
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		float dropRand = Main.rand.NextFloat();
		resultStack = 1;
		if (dropRand < 0.025f)
		{
			resultType = ModContent.ItemType<MysteriousCircuitry>();
		}
		else if (dropRand < 0.05f)
		{
			resultType = ModContent.ItemType<DubiousPlating>();
		}
		else if (dropRand < 0.95f)
		{
			resultType = 71;
			resultStack = Main.rand.Next(25, 100);
		}
		else
		{
			resultType = 72;
		}
	}
}
