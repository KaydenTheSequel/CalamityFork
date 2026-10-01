using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SevensStrikerBrick : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ThrowingBrick";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.3f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (Main.rand.NextBool(13))
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 22, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 150, default(Color), 0.9f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item50, base.Projectile.position);
		for (int dust_splash = 0; dust_splash < 9; dust_splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 9, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.15f, 120, default(Color), 1.5f);
		}
	}
}
