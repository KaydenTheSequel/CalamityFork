using System;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GemTechYellowShard : ModProjectile, ILocalizedModType, IModType
{
	public const int IntangibleFrames = 12;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/Typeless/GemTechYellowGem";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 45;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
			Vector2 initialVelocity = (Main.rand.NextFloatDirection() * 0.11f).ToRotationVector2() * Main.rand.NextFloat(2f, 3.3f);
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = initialVelocity * (float)i / 4f;
				dust.scale = 1.225f;
				dust.color = Color.Yellow;
				dust.noGravity = true;
				DustExtensions.BetterCloneDust(dust).velocity = initialVelocity.RotatedBy(2.0923008918762207) * (float)i / 4f;
				DustExtensions.BetterCloneDust(dust).velocity = initialVelocity.RotatedBy(-2.0923008918762207) * (float)i / 4f;
			}
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.0015707964776083827);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Opacity = (float)Math.Sqrt((float)base.Projectile.timeLeft / 45f);
	}

	public override bool? CanDamage()
	{
		return Time >= 12f;
	}
}
