using Terraria;

public interface IDrawArmOverShoulderpad
{
	string FrontArmTexture { get; }

	string EquipSlotName(Player drawPlayer)
	{
		return "";
	}
}
