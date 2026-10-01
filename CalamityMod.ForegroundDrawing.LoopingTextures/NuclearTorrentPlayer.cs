using Terraria.ModLoader;

namespace CalamityMod.ForegroundDrawing.LoopingTextures;

public class NuclearTorrentPlayer : ModPlayer
{
	public bool ShouldDisplayTorrentMonolith;

	public override void ResetEffects()
	{
		ShouldDisplayTorrentMonolith = false;
	}
}
