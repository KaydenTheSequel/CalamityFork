using Terraria.DataStructures;

namespace CalamityMod.Utilities.Daybreak;

internal readonly record struct PlayerShader(int LocalIndex, PlayerDrawHelper.ShaderConfiguration ShaderType)
{
	public int PackedValue => LocalIndex + (int)ShaderType * 1000;

	public static PlayerShader FromPacked(int packed)
	{
		return new PlayerShader(packed % 1000, (PlayerDrawHelper.ShaderConfiguration)(packed / 1000));
	}
}
