using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DepthCrusherSplitProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int randTimer;

	public int dustType1 = 104;

	public int dustType2 = 96;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == 1)
		{
			randTimer = Main.rand.Next(200, 261);
			base.Projectile.timeLeft = randTimer;
		}
		if (Time > 20 && Time < randTimer - 70)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 384f, MathHelper.Clamp(1f + (float)Time * 0.12f, 1f, 11f), 20f);
		}
		else if (Time >= randTimer - 70)
		{
			if (base.Projectile.velocity.Y < 10f)
			{
				base.Projectile.velocity.Y += 0.4f;
			}
			base.Projectile.velocity.X *= 0.97f;
		}
		if (Time % 2 == 0)
		{
			Color smokeColor = Color.MediumBlue;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(-0.2f, -0.6f), smokeColor, 30, Main.rand.NextFloat(0.35f, 0.5f), 0.3f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: false, 0f, required: true));
		}
		for (int i = 0; i < 3; i++)
		{
			Vector2 center = base.Projectile.Center;
			int dustType = (Main.rand.NextBool(3) ? dustType1 : dustType2);
			Dust dust = Dust.NewDustPerfect(center, dustType);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
			dust.velocity = Utils.RotatedByRandom(new Vector2(0.5f, 0.5f), 100.0) * Main.rand.NextFloat(0.2f, 1.1f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 180);
		SoundStyle style = SoundID.ShimmerWeak1 with
		{
			Pitch = 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}
}
