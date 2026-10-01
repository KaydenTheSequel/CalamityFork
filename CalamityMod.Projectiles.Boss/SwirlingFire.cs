using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SwirlingFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float AngularTurnSpeed => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.CooldownSlot = 1;
		base.Projectile.ai[0] = MathHelper.ToRadians(Main.rand.NextFloat(-3f, 3f));
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		Color c = ProvUtils.GetProjectileColor(255);
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, base.Projectile.velocity / 2f, affectedByGravity: false, 10, 0.5f * base.Projectile.ai[2], c));
		GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Vector2.Zero, Color.LightSlateGray, Color.DarkSlateGray, 0.5f * base.Projectile.ai[2], 150f, Main.rand.NextFloat(-0.01f, 0.01f)));
		base.Projectile.ai[2] *= 0.98f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.97f;
		if (base.Projectile.ai[2] < 0.2f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(AngularTurnSpeed);
		Time++;
	}
}
