using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic.LiquidSystem;

public class ModLavaStyleLoader : ModSystem
{
	private static readonly List<ModLavaStyle> _AllStyles = new List<ModLavaStyle>();

	public static IEnumerable<ModLavaStyle> AllStyles => _AllStyles;

	public static int VanillaCount => 1;

	public static int ModCount => _AllStyles.Count;

	public static int TotalCount => ModCount + VanillaCount;

	internal static int Register(ModLavaStyle instance)
	{
		int totalCount = TotalCount;
		ModTypeLookup<ModLavaStyle>.Register(instance);
		_AllStyles.Add(instance);
		return totalCount;
	}

	public override void Unload()
	{
		_AllStyles.Clear();
	}
}
