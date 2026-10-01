using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MadAlchemistsCocktailBlue : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		base.Projectile.rotation += Math.Abs(base.Projectile.velocity.X) * 0.04f * (float)base.Projectile.direction;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 90f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.4f;
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Center);
		if (!Main.dedServ)
		{
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, -base.Projectile.oldVelocity * 0.2f, 704);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, -base.Projectile.oldVelocity * 0.2f, 705);
		}
		int numClouds = 9;
		int cloudDamage = base.Projectile.damage / 2;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 v = default(Vector2);
			for (int i = 0; i < numClouds; i++)
			{
				((Vector2)(ref v))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				((Vector2)(ref v)).Normalize();
				v *= (float)Main.rand.Next(10, 201) * 0.01f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, v.X, v.Y, ModContent.ProjectileType<MadAlchemistsCocktailGasCloud>(), cloudDamage, 0f, base.Projectile.owner, 0f, Main.rand.Next(-45, 1));
			}
		}
	}
}
