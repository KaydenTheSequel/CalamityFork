using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class NerfExpertDebuffsSystem : ModSystem
{
	public override void PostUpdateTime()
	{
		if (CalamityServerConfig.Instance.NerfExpertDebuffs)
		{
			GameModeData copy = Main.RegisteredGameModes[1]with
			{
				DebuffTimeMultiplier = 1f
			};
			Main.RegisteredGameModes[1] = copy;
			copy = Main.RegisteredGameModes[2]with
			{
				DebuffTimeMultiplier = 1f
			};
			Main.RegisteredGameModes[2] = copy;
			Main.GameMode = Main.GameMode;
		}
		else
		{
			GameModeData copy2 = Main.RegisteredGameModes[1]with
			{
				DebuffTimeMultiplier = 2f
			};
			Main.RegisteredGameModes[1] = copy2;
			copy2 = Main.RegisteredGameModes[2]with
			{
				DebuffTimeMultiplier = 2.5f
			};
			Main.RegisteredGameModes[2] = copy2;
			Main.GameMode = Main.GameMode;
		}
	}
}
