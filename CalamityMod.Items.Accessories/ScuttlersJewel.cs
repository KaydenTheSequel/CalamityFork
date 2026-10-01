using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ScuttlersJewel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ExtractinatorMode[base.Type] = base.Item.type;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
		base.Item.MakeUsableWithChlorophyteExtractinator();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().scuttlersJewel = true;
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		float dropRand = Main.rand.Next(1, 8);
		resultStack = Main.rand.Next(1, 3);
		if (dropRand == 1f)
		{
			resultType = 178;
		}
		else if (dropRand == 2f)
		{
			resultType = 182;
		}
		else if (dropRand == 3f)
		{
			resultType = 179;
		}
		else if (dropRand == 4f)
		{
			resultType = 180;
		}
		else if (dropRand == 5f)
		{
			resultType = 177;
		}
		else if (dropRand == 6f)
		{
			resultType = 181;
		}
		else if (dropRand >= 7f)
		{
			resultType = 999;
		}
	}
}
