using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class PinkJellyAura : ModProjectile, ILocalizedModType, IModType
{
	public int ShinkGrow;

	public int Framecounter;

	public int PulseOnce = 1;

	public int PulseOnce2 = 1;

	public int PulseOnce3 = 1;

	public static readonly SoundStyle Spawnsound = new SoundStyle("CalamityMod/Sounds/Custom/OrbHeal1")
	{
		Volume = 0.5f
	};

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 336);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = LifeJelly.AuraLifetime + 10;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		Framecounter++;
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			Player player = Main.player[playerIndex];
			if (Vector2.Distance(player.Center, base.Projectile.Center) < 165f)
			{
				player.AddBuff(ModContent.BuffType<PinkJellyRegen>(), 300);
			}
		}
		if (ShinkGrow == 0)
		{
			if (PulseOnce == 1)
			{
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.HotPink, new Vector2(1f, 1f), 0f, 0f, 0.152f, 10));
				SoundStyle style = Spawnsound with
				{
					Pitch = -0.9f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				PulseOnce = 0;
			}
			if (Framecounter == 10)
			{
				ShinkGrow = 1;
			}
		}
		if (ShinkGrow == 1)
		{
			if (PulseOnce2 == 1)
			{
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.HotPink, new Vector2(1f, 1f), 0f, 0.152f, 0.152f, 1790));
				PulseOnce2 = 0;
			}
			for (int i = 0; i < 1; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(155f, 155f), 242);
				dust.scale = Main.rand.NextFloat(2.2f, 3.3f);
				dust.noGravity = true;
			}
			for (int j = 0; j < 1; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(150f, 150f), 242);
				dust2.scale = Main.rand.NextFloat(0.8f, 1.3f);
				dust2.noGravity = true;
			}
			if (Framecounter == LifeJelly.AuraLifetime)
			{
				ShinkGrow = 2;
			}
		}
		if (ShinkGrow == 2 && PulseOnce3 == 1)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.HotPink, new Vector2(1f, 1f), 0f, 0.152f, 0f, 10));
			PulseOnce3 = 0;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
