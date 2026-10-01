using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GastricBelcherBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.timeLeft = 180;
		base.Projectile.aiStyle = 72;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		_ = base.Projectile.Center;
		for (int dustIndex = 0; dustIndex < 10; dustIndex++)
		{
			int scalar = (int)(10f * base.Projectile.ai[1]);
			int bubble = Dust.NewDust(base.Projectile.Center - Vector2.One * (float)scalar, scalar * 2, scalar * 2, 212);
			Dust dust = Main.dust[bubble];
			Vector2 dustVec = Vector2.Normalize(dust.position - base.Projectile.Center);
			dust.position = base.Projectile.Center + dustVec * (float)scalar * base.Projectile.scale;
			dust.velocity = ((dustIndex >= 30) ? (dustVec * (float)Main.rand.Next(45, 91) / 10f) : (dustVec * ((Vector2)(ref dust.velocity)).Length()));
			dust.color = Main.hslToRgb(0.4f + Main.rand.NextFloat() * 0.2f, 0.9f, 0.5f);
			dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
			dust.noGravity = true;
			dust.scale = 0.7f;
		}
	}
}
