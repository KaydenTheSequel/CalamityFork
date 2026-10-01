using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class RavagerFlame : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.coldDamage = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.aiStyle = 1;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(1f), 6.300000190734863), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 0.25f, Color.SkyBlue, new Vector2(1f)));
		for (int i = 0; i < 2; i++)
		{
			int icyFlame = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[icyFlame].noGravity = true;
			Dust obj = Main.dust[icyFlame];
			obj.velocity *= 0f;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(44, 180);
		}
	}
}
