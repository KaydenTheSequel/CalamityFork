using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DeadSunExplosion : ModProjectile, ILocalizedModType, IModType
{
	public Color color1;

	public Color color2;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float ExplosionRadius => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		DetailedExplosion detailedExplosion = new DetailedExplosion(base.Projectile.Center, Vector2.Zero, color1, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, ExplosionRadius * 0.0065f + 0.1f, Main.rand.Next(15, 22));
		GeneralParticleHandler.SpawnParticle(detailedExplosion);
		detailedExplosion.DrawLayer = GeneralDrawLayer.AfterEverything;
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.Black, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, ExplosionRadius * 0.0045f + 0.1f, Main.rand.Next(15, 22), UseAdditiveBlend: false));
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.Black, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, ExplosionRadius * 0.003f + 0.1f, Main.rand.Next(15, 22), UseAdditiveBlend: false));
		for (int i = 0; i < 4; i++)
		{
			CustomPulse customPulse = new CustomPulse(base.Projectile.Center, Vector2.Zero, color1, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, ExplosionRadius * 0.005f + 0.05f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			GeneralParticleHandler.SpawnParticle(customPulse);
			customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
		}
		float numberOfDusts = ExplosionRadius * 0.1f + 10f;
		float rotFactor = 360f / numberOfDusts;
		for (int j = 0; (float)j < numberOfDusts; j++)
		{
			float rot = MathHelper.ToRadians((float)j * rotFactor);
			Vector2 offset = (Vector2.UnitX * Main.rand.NextFloat(ExplosionRadius * 0.2f, 3.1f)).RotatedBy(rot * Main.rand.NextFloat(1.1f, 9.1f));
			Vector2 velOffset = (Vector2.UnitX * Main.rand.NextFloat(ExplosionRadius * 0.2f, 3.1f)).RotatedBy(rot * Main.rand.NextFloat(1.1f, 9.1f));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, Main.rand.NextBool(4) ? ModContent.DustType<LightDust>() : ((base.Projectile.ai[1] == 5f) ? 278 : ModContent.DustType<VoidDustInverted>()), velOffset);
			dust.noGravity = dust.type != 278;
			dust.color = color1;
			dust.velocity = velOffset;
			dust.scale = ((dust.type == 278) ? Main.rand.NextFloat(0.7f, 1.3f) : Main.rand.NextFloat(1.6f, 2.2f));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, ExplosionRadius + 20f, targetHitbox);
	}

	public DeadSunExplosion()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		color1 = Color.LightGreen;
		color2 = Color.Black;
		base._002Ector();
	}
}
