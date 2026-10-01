using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AtlasMunitionsLaser : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.Opacity = 0f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color cyan = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref cyan)).ToVector3() * base.Projectile.Opacity * 0.67f);
		base.Projectile.Opacity = Utils.GetLerpValue(240f, 235f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 5)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.02f;
		}
	}
}
