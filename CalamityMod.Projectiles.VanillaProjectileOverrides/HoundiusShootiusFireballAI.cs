using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Projectiles.VanillaProjectileOverrides;

public static class HoundiusShootiusFireballAI
{
	public static bool DoHoundiusShootiusFireballAI(Projectile projectile)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		float enemyDistanceDetection = 1200f;
		float projVelocity = 12.5f;
		float rateOfChange = 0.2f;
		Player owner = Main.player[projectile.owner];
		NPC target = projectile.Center.MinionHoming(enemyDistanceDetection, owner);
		if (target != null)
		{
			projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.SafeDirectionTo(target.Center) * projVelocity, rateOfChange);
		}
		return true;
	}
}
