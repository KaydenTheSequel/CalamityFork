using Terraria.Utilities;

namespace CalamityMod;

public struct WeightedItemStack
{
	public const float DefaultWeight = 1f;

	public const float MinisiculeWeight = 1E-06f;

	internal int itemID;

	internal float weight;

	internal int minQuantity;

	internal int maxQuantity;

	internal WeightedItemStack(int id, float w)
	{
		itemID = id;
		weight = w;
		minQuantity = 1;
		maxQuantity = 1;
	}

	internal WeightedItemStack(int id, float w, int quantity)
	{
		itemID = id;
		weight = w;
		minQuantity = quantity;
		maxQuantity = quantity;
	}

	internal WeightedItemStack(int id, float w, int min, int max)
	{
		itemID = id;
		weight = w;
		minQuantity = min;
		maxQuantity = max;
	}

	internal int ChooseQuantity(UnifiedRandom rng)
	{
		return rng.Next(minQuantity, maxQuantity + 1);
	}

	public static implicit operator WeightedItemStack(int id)
	{
		return new WeightedItemStack(id, 1f, 1);
	}
}
