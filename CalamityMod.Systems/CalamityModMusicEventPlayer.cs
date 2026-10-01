using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamityModMusicEventPlayer : ModPlayer
{
	public override void OnEnterWorld()
	{
		if (Main.netMode == 1 && base.Player.whoAmI != Main.myPlayer)
		{
			MusicEventSystem.SendSyncRequest();
		}
	}
}
