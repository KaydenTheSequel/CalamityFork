using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class Brick : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ThrowingBrick";

	public override void SetDefaults()
	{
		base.Projectile.width = 19;
		base.Projectile.aiStyle = -1;
		base.Projectile.height = 19;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = (base.Projectile.Calamity().stealthStrike ? 1 : 0);
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(12f), 0f), (double)(Vector2.Zero.AngleTo(base.Projectile.velocity) + MathHelper.ToRadians(Main.rand.NextFloat(-20f, 20f))), default(Vector2)), affectedByGravity: false, 10, (base.Projectile.ai[0] == 1f) ? 0.4f : 0.3f, (base.Projectile.ai[0] == 1f) ? Color.OrangeRed : Color.SaddleBrown, AddativeBlend: false, affectedByLight: true));
		}
		base.Projectile.ai[1]++;
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		base.Projectile.velocity.X *= 0.98f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + MathHelper.Clamp(base.Projectile.ai[1] / 40f, 0f, 0.6f);
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (Main.rand.NextBool(13))
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 22, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 150, default(Color), 0.9f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = (base.Projectile.Calamity().stealthStrike ? 1 : 0);
		SoundEngine.PlaySound(in SoundID.Item50, base.Projectile.position);
		SoundEngine.PlaySound(SoundID.Dig.WithPitchOffset(Main.rand.NextFloat(0.5f, 1f)), base.Projectile.position);
		SoundEngine.PlaySound(SoundID.Dig.WithPitchOffset(Main.rand.NextFloat(-1f, -0.5f)), base.Projectile.position);
		for (int dust_splash = 0; dust_splash < 9; dust_splash++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(15f), 0f), 6.2831854820251465), affectedByGravity: false, 10, (base.Projectile.ai[0] == 1f) ? 1.2f : 0.6f, (base.Projectile.ai[0] == 1f) ? Color.OrangeRed : Color.SaddleBrown, AddativeBlend: false, affectedByLight: true));
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 9, 0f, 0f, 0, default(Color), 0.5f);
		}
		if (base.Projectile.ai[0] == 1f)
		{
			for (int dust_splash = 0; dust_splash < 9; dust_splash++)
			{
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(24f), 0f), 6.2831854820251465), affectedByGravity: false, 6, 0.6f, Color.SaddleBrown, AddativeBlend: false, affectedByLight: true));
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), Color.SaddleBrown, Color.SaddleBrown, Main.rand.NextFloat(1f, 1.5f), 150f, 0f, affectedByLight: true));
			}
			for (int split = 0; split < 5; split++)
			{
				Vector2 shardspeed = Utils.RotatedBy(new Vector2((float)Main.rand.Next(3, 8), 0f), (double)Main.rand.NextFloat((float)Math.PI * 2f), default(Vector2));
				Vector2 speedAdd = -base.Projectile.velocity;
				((Vector2)(ref speedAdd)).Normalize();
				shardspeed += speedAdd * 9f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position + shardspeed, shardspeed, ModContent.ProjectileType<BrickFragment>(), base.Projectile.damage / 2, base.Projectile.knockBack / 2f, base.Projectile.owner);
			}
		}
	}
}
