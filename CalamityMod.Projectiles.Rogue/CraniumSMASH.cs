using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CraniumSMASH : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -2;
	}

	public override void AI()
	{
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.ExpandHitboxBy(300);
		}
		if (base.Projectile.ai[0] == 0f)
		{
			SpawnExplosionDust(base.Projectile.width);
			base.Projectile.ai[0] = 1f;
		}
	}

	private void SpawnExplosionDust(int size)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (!Main.dedServ)
		{
			int goreAmt = 3;
			Vector2 source = base.Projectile.Center - new Vector2(24f);
			for (int goreIndex = 1; goreIndex <= goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f * (float)goreIndex;
				int type = Main.rand.Next(61, 64);
				Gore gore = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore.velocity *= velocityMult;
				type = Main.rand.Next(61, 64);
				Gore gore2 = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore2.velocity *= velocityMult;
			}
		}
		for (int i = 0; i < 30; i++)
		{
			float edgeOffset = Main.rand.NextFloat((float)size * 0.35f, size / 2) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			float randOffset = Main.rand.NextFloat(-size / 2, size / 2);
			Dust.NewDustPerfect(base.Projectile.Center + ((i % 2 == 0) ? new Vector2(edgeOffset, randOffset) : new Vector2(randOffset, edgeOffset)), 135, Vector2.Zero, 100, default(Color), 2f).noGravity = true;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.1f, 1.1f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}
}
