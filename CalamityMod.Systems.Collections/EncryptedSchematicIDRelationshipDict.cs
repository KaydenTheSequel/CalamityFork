using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.DraedonMisc;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

public sealed class EncryptedSchematicIDRelationshipDict : ModSystem
{
	public static IDictionary<int, int> Dict { get; private set; }

	public override void OnModLoad()
	{
		Dict = new Dictionary<int, int>
		{
			[1] = ModContent.ItemType<EncryptedSchematicPlanetoid>(),
			[2] = ModContent.ItemType<EncryptedSchematicJungle>(),
			[3] = ModContent.ItemType<EncryptedSchematicHell>(),
			[4] = ModContent.ItemType<EncryptedSchematicIce>()
		};
	}

	public override void Unload()
	{
		Dict?.Clear();
		Dict = null;
	}

	public static bool TryGet(int schematicID, out int schematicItemType)
	{
		return Dict.TryGetValue(schematicID, out schematicItemType);
	}

	public static bool TryGetKey(int schematicItemType, out int schematicID)
	{
		try
		{
			schematicID = Dict.First((KeyValuePair<int, int> pair) => pair.Value == schematicItemType).Key;
			return true;
		}
		catch
		{
			schematicID = 0;
			return false;
		}
	}
}
