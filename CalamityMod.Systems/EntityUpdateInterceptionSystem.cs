using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.TileEntities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class EntityUpdateInterceptionSystem : ModSystem
{
	public override void PostUpdateDusts()
	{
		DeathAshParticle.UpdateAll();
	}

	public override void PostUpdateNPCs()
	{
		CalamityGlobalTownNPC.ResetTownNPCNameBools();
	}

	public override void PostUpdateTime()
	{
		TileEntityTimeHandler.Update();
	}

	public override void PostUpdateEverything()
	{
		if (!Main.dedServ)
		{
			GeneralParticleHandler.Update();
		}
	}
}
