using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

public interface IExtendedHat
{
	string ExtensionTexture { get; }

	Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo);

	bool PreDrawExtension(PlayerDrawSet drawInfo)
	{
		return true;
	}

	string EquipSlotName(Player drawPlayer)
	{
		return "";
	}
}
