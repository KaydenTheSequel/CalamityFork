using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class AltTransfusionTrail : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float opacity = 0.3f;

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.HealingProjectile((int)base.Projectile.ai[1], (int)base.Projectile.ai[0], 15f, MathHelper.Clamp(80f - (float)time, 5f, 80f));
		if (time == 1)
		{
			bool direction = Main.rand.NextBool();
			base.Projectile.velocity = (base.Projectile.velocity * 10f).RotatedBy(direction ? Main.rand.NextFloat(0.6f, 1.8f) : Main.rand.NextFloat(-0.6f, -1.8f)) * 2f;
		}
		GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.1f), affectedByGravity: false, 5, 0.6f, Color.White * opacity));
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? 63 : 91, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.25f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.75f, 0.9f);
			dust.alpha = 100;
			dust.noLight = true;
		}
		if (base.Projectile.timeLeft < 300)
		{
			base.Projectile.extraUpdates = 12;
			if (opacity > 0f)
			{
				opacity -= 0.01f;
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		for (int i = 0; i < 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HealingPlus(Owner.Center - new Vector2((float)Main.rand.Next(-4, 25), 2f), Main.rand.NextFloat(0.2f, 0.5f), new Vector2(0f, Main.rand.NextFloat(-2f, -4.5f)) + Owner.velocity, Color.SlateGray * 0.75f, Color.White * 0.75f, Main.rand.Next(15, 20)));
		}
	}
}
