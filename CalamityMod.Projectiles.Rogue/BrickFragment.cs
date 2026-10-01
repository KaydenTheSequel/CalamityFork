using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BrickFragment : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 16;
		base.Projectile.scale = Main.rand.NextFloat(0.4f, 0.8f);
		base.Projectile.height = 16;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		base.Projectile.rotation += MathHelper.ToRadians(3f) * (float)base.Projectile.direction;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.27f + MathHelper.Clamp(base.Projectile.ai[0] / 40f, 0f, 0.5f);
		base.Projectile.velocity.X *= 0.97f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(SoundID.Dig.WithPitchOffset(Main.rand.NextFloat(0.5f)).WithVolumeScale(0.6f), base.Projectile.position);
		for (int splash = 0; splash < 4; splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 9, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.1f, 150, default(Color), 0.9f);
		}
		for (int dust_splash = 0; dust_splash < 9; dust_splash++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(8f), 0f), 6.2831854820251465), affectedByGravity: false, 10, 0.4f, Color.SaddleBrown, AddativeBlend: false, affectedByLight: true));
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 9, 0f, 0f, 0, default(Color), 0.5f);
		}
	}
}
