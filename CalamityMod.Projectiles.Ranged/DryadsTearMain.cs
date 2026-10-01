using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DryadsTearMain : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 5;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.LimeGreen;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.25f);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			Vector2 center2 = base.Projectile.Center;
			int type = (Main.rand.NextBool(5) ? 131 : 294);
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.45f, 0.65f);
			if (dust.type == 131)
			{
				dust.scale = Main.rand.NextFloat(0.35f, 0.55f);
			}
			else
			{
				dust.fadeIn = 0.5f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int b = 0; b < 2; b++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<DryadsTearSplit>(), (int)((double)base.Projectile.damage * 0.1), 0f, base.Projectile.owner);
			}
			for (int i = 0; i < 3; i++)
			{
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0);
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center + velocity2, velocity2 * Main.rand.NextFloat(0.5f, 1f), affectedByGravity: false, 11, 0.65f, Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime));
			}
		}
		SoundEngine.PlaySound(SoundID.Item118 with
		{
			Pitch = 0.5f
		}, base.Projectile.Center);
	}
}
