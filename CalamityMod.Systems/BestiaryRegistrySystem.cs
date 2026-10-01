using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Crags;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BestiaryRegistrySystem : ModSystem
{
	public override void PostSetupContent()
	{
		ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<AstralachneaGround>()] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<AstralachneaWall>()];
		ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<DevilFishAlt>()] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<DevilFish>()];
		ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<ScryllarRage>()] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<Scryllar>()];
	}
}
