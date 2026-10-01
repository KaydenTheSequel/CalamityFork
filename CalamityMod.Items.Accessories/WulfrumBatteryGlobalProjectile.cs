using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class WulfrumBatteryGlobalProjectile : GlobalProjectile
{
	public override void AI(Projectile projectile)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		if (!projectile.npcProj && !projectile.trap && (projectile.minion || projectile.sentry) && !ProjectileID.Sets.MinionShot[projectile.type] && !ProjectileID.Sets.SentryShot[projectile.type] && Main.player[projectile.owner].GetModPlayer<WulfrumBatteryPlayer>().battery)
		{
			float lightMult = 1f;
			if (Lighting.UpdateEveryFrame)
			{
				lightMult *= 0.25f;
			}
			Vector2 center = projectile.Center;
			Color newColor = Color.LightGreen;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * lightMult);
			if (Main.rand.NextBool(15))
			{
				Vector2 val = projectile.Hitbox.Size() / 2f;
				float size = ((Vector2)(ref val)).Length();
				Vector2 position = projectile.Center + Main.rand.NextVector2Circular(1f, 1f) * size;
				Vector2? velocity = Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(0.5f, 1.3f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 66, velocity, 0, newColor);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.SkyBlue : Color.LightGreen);
				dust.velocity = projectile.velocity * 0.2f;
			}
		}
	}
}
