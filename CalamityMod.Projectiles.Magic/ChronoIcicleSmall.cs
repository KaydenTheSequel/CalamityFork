using CalamityMod.Buffs.StatDebuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ChronoIcicleSmall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.aiStyle = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		base.Projectile.velocity.Y += 0.05f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item27 with
		{
			Volume = SoundID.Item12.Volume * 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		target.AddBuff(ModContent.BuffType<TimeDistortion>(), 30);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}
}
