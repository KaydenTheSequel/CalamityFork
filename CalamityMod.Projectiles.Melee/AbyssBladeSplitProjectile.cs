using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AbyssBladeSplitProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int randTimer;

	public int dustType1 = 104;

	public int dustType2 = 29;

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
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time == 1)
		{
			randTimer = Main.rand.Next(240, 301);
			base.Projectile.timeLeft = randTimer;
		}
		if (Time > 20 && Time < randTimer - 70)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 350f, MathHelper.Clamp(1f + (float)Time * 0.075f, 1f, 10f), 20f);
		}
		else if (Time >= randTimer - 70)
		{
			if (base.Projectile.velocity.Y < 10f)
			{
				base.Projectile.velocity.Y += 0.4f;
			}
			base.Projectile.velocity.X *= 0.97f;
		}
		if (Time % 3 == 0)
		{
			Color smokeColor = Color.MediumBlue;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(0.6f, 0.8f), smokeColor, 30, Main.rand.NextFloat(0.35f, 0.5f), 0.3f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: false, 0f, required: true));
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 center = base.Projectile.Center;
			int dustType = (Main.rand.NextBool(3) ? dustType1 : dustType2);
			Dust dust = Dust.NewDustPerfect(center, dustType);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.95f, 1.7f);
			dust.velocity = base.Projectile.velocity + Utils.RotatedByRandom(new Vector2(0.5f, 0.5f), 100.0) * Main.rand.NextFloat(0.2f, 1.1f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
		SoundStyle style = SoundID.ShimmerWeak1 with
		{
			Pitch = 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}
}
