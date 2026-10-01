using Terraria;

public interface IBulkyArmor
{
	string BulkTexture { get; }

	string EquipSlotName(Player drawPlayer)
	{
		return "";
	}
}
