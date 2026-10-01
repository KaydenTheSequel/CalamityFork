using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class GeyserTelegraph : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 100;
		base.Projectile.trap = true;
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized && Main.myPlayer != base.Projectile.owner)
		{
			int projectileType = ModContent.ProjectileType<SmokeTelegraph>();
			float randomVelocity = Main.rand.NextFloat() + 0.5f;
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, -8f * randomVelocity, projectileType, 0, 0f, base.Projectile.owner);
			Main.projectile[proj].netUpdate = true;
			initialized = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			int projectileType = 654;
			if (base.Projectile.ai[0] == 1f)
			{
				projectileType = ModContent.ProjectileType<BrimstoneGeyser>();
			}
			float randomVelocity = Main.rand.NextFloat() + 0.5f;
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, -8f * randomVelocity, projectileType, 20, 2f, base.Projectile.owner);
			Main.projectile[proj].friendly = false;
			Main.projectile[proj].netUpdate = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
