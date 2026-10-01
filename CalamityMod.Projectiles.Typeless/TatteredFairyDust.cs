using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class TatteredFairyDust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0.9f, 0.9f);
		if (base.Projectile.timeLeft % 2 == 0)
		{
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 195, Main.rand.NextFloat(0.1f), Main.rand.NextFloat(0.1f)).noGravity = true;
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 43, Main.rand.NextFloat(0.1f), Main.rand.NextFloat(0.1f), 0, new Color(190, 3, 252)).noGravity = true;
		}
		if (base.Projectile.timeLeft >= 170)
		{
			return;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (Vector2.Distance(player.Center, base.Projectile.Center) < 24f && player.wingTime > 0f)
			{
				player.wingTime -= 4f;
			}
		}
	}
}
