namespace CalamityMod.Schematics;

public struct ChestItem
{
	internal int Type;

	internal int Stack;

	internal ChestItem(int type, int stack)
	{
		Type = type;
		Stack = stack;
	}
}
