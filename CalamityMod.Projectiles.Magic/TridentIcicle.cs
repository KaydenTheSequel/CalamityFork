using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TridentIcicle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.friendly = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.coldDamage = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 88);
		Main.dust[index2].noGravity = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 88);
			Main.dust[index2].noGravity = true;
			Dust obj = Main.dust[index2];
			obj.velocity *= 2f;
		}
	}
}
