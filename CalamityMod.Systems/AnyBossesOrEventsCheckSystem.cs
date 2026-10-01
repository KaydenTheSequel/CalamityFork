using CalamityMod.CalPlayer;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AnyBossesOrEventsCheckSystem : ModSystem
{
	public override void PreUpdateEntities()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		SCalSky.RitualDramaProjectileIsPresent = CalamityUtils.CountProjectiles(ModContent.ProjectileType<SCalRitualDrama>()) > 0;
		CalamityPlayer.areThereAnyDamnBosses = CalamityUtils.AnyBossNPCS();
		int closestPlayer = Player.FindClosest(new Vector2((float)(Main.maxTilesX / 2), (float)Main.worldSurface / 2f) * 16f, 0, 0);
		CalamityPlayer.areThereAnyDamnEvents = CalamityGlobalNPC.AnyEvents(Main.player[closestPlayer]);
	}
}
