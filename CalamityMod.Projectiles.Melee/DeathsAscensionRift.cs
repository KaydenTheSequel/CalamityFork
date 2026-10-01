using CalamityMod.CalPlayer;
using CalamityMod.Graphics.Metaballs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DeathsAscensionRift : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		int ballAmt = 20;
		for (int i = 0; i < ballAmt; i++)
		{
			float offset = i * 5;
			Vector2 rotationOffset = Vector2.UnitY.RotatedBy(base.Projectile.rotation) * (float)i;
			float sizeRandomness = MathHelper.Lerp(10f, 0f, (float)i / (float)ballAmt);
			float positionRandomness = 20f;
			float scale = (MathHelper.Lerp(90f, 10f, (float)i / (float)ballAmt) + Main.rand.NextFloat(0f - sizeRandomness, sizeRandomness)) * Utils.GetLerpValue(600f, 590f, base.Projectile.timeLeft, clamped: true);
			StreamGougeMetaball.SpawnParticle(base.Projectile.Center + Vector2.UnitY * (offset + Main.rand.NextFloat(0f - positionRandomness, positionRandomness)) + rotationOffset, Vector2.Zero, scale);
			StreamGougeMetaball.SpawnParticle(base.Projectile.Center + Vector2.UnitY * (0f - (offset + Main.rand.NextFloat(0f - positionRandomness, positionRandomness))) - rotationOffset, Vector2.Zero, scale);
		}
		if (base.Projectile.ai[2] == 0f)
		{
			int scytheAmt = 4;
			float speed = 30f;
			for (int j = 0; j < scytheAmt; j++)
			{
				Vector2 scytheVelocity = Vector2.UnitY.RotatedBy(MathHelper.Lerp(0f, 4.712389f, (float)j / (float)(scytheAmt - 1)) + base.Projectile.ai[1]) * speed;
				if (base.Projectile.owner == Main.myPlayer)
				{
					int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, scytheVelocity, ModContent.ProjectileType<DeathsAscensionProjectile>(), (int)((float)base.Projectile.damage * 0.4f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 1f);
					Main.projectile[p].penetrate = -1;
					Main.projectile[p].timeLeft = 600;
				}
			}
			base.Projectile.ai[2] = 1f;
		}
		if (base.Projectile.ai[0] == 10f)
		{
			SoundStyle style = SoundID.Item104 with
			{
				Pitch = 0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = SoundID.Item71 with
			{
				Pitch = -0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Vector2 direction = base.Projectile.Center.DirectionTo(Main.MouseWorld) * 12f;
			int spreadfactor = 9;
			for (int index = 0; index < 4; index++)
			{
				float SpeedX = direction.X + Main.rand.NextFloat(-spreadfactor, spreadfactor + 1);
				float SpeedY = direction.Y + Main.rand.NextFloat(-spreadfactor, spreadfactor + 1);
				if (base.Projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, SpeedX, SpeedY, ModContent.ProjectileType<DeathsAscensionProjectile>(), (int)((float)base.Projectile.damage * 0.125f), base.Projectile.knockBack, base.Projectile.owner);
				}
			}
		}
		base.Projectile.ai[1] += 0.1f;
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in CalamityPlayer.DrownSound, base.Projectile.Center);
	}
}
