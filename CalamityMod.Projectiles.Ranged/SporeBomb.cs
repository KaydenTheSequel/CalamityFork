using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SporeBomb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.light = 0.2f;
	}

	public override void AI()
	{
		base.Projectile.alpha -= 2;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale += 0.05f;
			if (base.Projectile.scale > 1.2f)
			{
				base.Projectile.localAI[0] = 1f;
			}
		}
		else
		{
			base.Projectile.scale -= 0.05f;
			if (base.Projectile.scale < 0.8f)
			{
				base.Projectile.localAI[0] = 0f;
			}
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 20f && base.Projectile.ai[0] < 40f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.3f;
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.98f;
		}
		else if (base.Projectile.ai[0] >= 40f && base.Projectile.ai[0] < 60f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.3f;
			base.Projectile.velocity.X = base.Projectile.velocity.X * 1.02f;
		}
		else if (base.Projectile.ai[0] >= 60f)
		{
			base.Projectile.ai[0] = 0f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, 203, 103, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		for (int d = 0; d < 25; d++)
		{
			int index = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 157, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103));
			Main.dust[index].noGravity = true;
			Dust obj = Main.dust[index];
			obj.velocity *= 1.5f;
			Main.dust[index].scale = 1.5f;
		}
		int sporeAmt = Main.rand.Next(3, 7);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int s = 0; s < sporeAmt; s++)
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, 569 + Main.rand.Next(3), (int)((double)base.Projectile.damage * 0.25), 0f, base.Projectile.owner);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = DamageClass.Ranged;
				Main.projectile[proj].usesLocalNPCImmunity = true;
				Main.projectile[proj].usesIDStaticNPCImmunity = false;
				Main.projectile[proj].localNPCHitCooldown = 30;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
