using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class KeelhaulBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<KeelhaulGeyserBottom>() || p.type == ModContent.ProjectileType<KeelhaulGeyserTop>())
			{
				p.Kill();
			}
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y - 80f, 0f, 0f, ModContent.ProjectileType<KeelhaulGeyserBottom>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y - 80f, 0f, 0f, ModContent.ProjectileType<KeelhaulGeyserTop>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
	}
}
