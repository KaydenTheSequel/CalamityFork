using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class Snowball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = 1;
		base.Projectile.coldDamage = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
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
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			int snow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68);
			Main.dust[snow].noGravity = true;
			Dust obj = Main.dust[snow];
			obj.velocity *= 2f;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int j = 0; j < 6; j++)
			{
				float SpeedX = (0f - base.Projectile.velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				float SpeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + SpeedX, base.Projectile.Center.Y + SpeedY, SpeedX, SpeedY, 90, base.Projectile.damage / 2, 0f, base.Projectile.owner);
				Main.projectile[p].DamageType = DamageClass.Magic;
			}
		}
	}
}
