using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DuststormInABottleProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DuststormInABottle";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 180;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		bool stealth = base.Projectile.Calamity().stealthStrike;
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Center);
		double cloudAmt = Main.rand.Next(40, 50);
		if (stealth)
		{
			for (int dustexplode = 0; dustexplode < 180; dustexplode++)
			{
				Vector2 dustd = Utils.RotatedBy(new Vector2(DuststormInABottle.DustRadius, DuststormInABottle.DustRadius), (double)MathHelper.ToRadians((float)(dustexplode * 2)), default(Vector2));
				GeneralParticleHandler.SpawnParticle(new SandyDustParticle(base.Projectile.Center, dustd * Main.rand.NextFloat(0.25f, 1f), Color.Beige, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(30, 50)));
				int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(5) ? 32 : 85, dustd.X, dustd.Y, 50, default(Color), 2f);
				Main.dust[d].noGravity = true;
				Dust obj = Main.dust[d];
				obj.velocity *= Main.rand.NextFloat(0.25f, 1f);
				Main.dust[d].scale *= Main.rand.NextFloat(0.5f, 0.8f);
			}
			cloudAmt *= 2.75;
			cloudAmt = Math.Round(cloudAmt);
		}
		else
		{
			for (int i = 0; i < 120; i++)
			{
				Vector2 dustd2 = Utils.RotatedBy(new Vector2(DuststormInABottle.DustRadius, DuststormInABottle.DustRadius - 2f), (double)MathHelper.ToRadians((float)(i * 3)), default(Vector2));
				GeneralParticleHandler.SpawnParticle(new SandyDustParticle(base.Projectile.Center, dustd2 * Main.rand.NextFloat(0.25f, 1f), Color.Beige, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(30, 50)));
			}
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int index = 0; (double)index < cloudAmt; index++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 10f, 200f, 0.01f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, stealth ? (velocity * 1.2f) : velocity, ModContent.ProjectileType<DuststormCloud>(), 0, 0f, base.Projectile.owner, stealth ? 1f : 0f, Main.rand.Next(-45, 1));
			}
			int hitbox = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DuststormCloudHitbox>(), base.Projectile.damage, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			if (hitbox.WithinBounds(Main.maxProjectiles) && base.Projectile.Calamity().stealthStrike)
			{
				Main.projectile[hitbox].ai[1] = 1f;
			}
		}
	}
}
