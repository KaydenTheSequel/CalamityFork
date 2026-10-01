using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SwordsmithsPrideAstralEnergy : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		if (Timer >= 40f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 480f, 13f, 20f);
		}
		base.Projectile.ai[1] += 0.18f;
		float f = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float pulse = (float)Math.Sin(base.Projectile.ai[1]);
		float radius = 9f;
		Vector2 offset = f.ToRotationVector2() * pulse * radius;
		Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<AstralOrange>(), Vector2.Zero, 0, default(Color), 0.75f);
		Dust.NewDustPerfect(base.Projectile.Center - offset, ModContent.DustType<AstralBlue>(), Vector2.Zero, 0, default(Color), 0.75f);
		Color partColor = Color.Lerp(Color.Orange, Color.Blue, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3.5f));
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Vector2.Zero, affectedByGravity: false, 2, 0.8f, partColor));
	}
}
