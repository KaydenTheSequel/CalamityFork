using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NanoPurgeLaser : ModProjectile, ILocalizedModType, IModType
{
	private const float LaserLength = 40f;

	private const float LaserLengthChangeRate = 1.5f;

	public bool HasBounced;

	public int bounceTimer;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.7f, 0.1f);
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.localAI[0] += 1.5f;
			if (base.Projectile.localAI[0] > 40f)
			{
				base.Projectile.localAI[0] = 40f;
			}
		}
		else
		{
			base.Projectile.localAI[0] -= 1.5f;
			if (base.Projectile.localAI[0] <= 0f)
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.timeLeft % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 4, 0.02f, Color.Lime, new Vector2(0.6f, 1.4f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.95f));
		}
		if (bounceTimer > 0)
		{
			bounceTimer--;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		bounceTimer = 10;
		for (int i = 0; i < 2; i++)
		{
			Color color = ((i == 0) ? Color.White : Color.Lime);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, oldVelocity * 0.1f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, Main.rand.NextFloat(0.15f, 0.25f) * (float)((i == 0) ? 2 : 3), color, new Vector2(0.7f, 1.1f)));
		}
		for (int j = 0; j < 5; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.8f, 2.5f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.45f, 1.1f);
			dust.color = Color.Lime;
			dust.noLightEmittence = true;
		}
		Player owner = Main.player[base.Projectile.owner];
		if (!HasBounced)
		{
			HasBounced = true;
			float npcDistCheck = 640f;
			int index = -1;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float currentNPCDist = Vector2.Distance(n.Center, owner.ClampedMouseWorld());
					if (currentNPCDist < npcDistCheck)
					{
						npcDistCheck = currentNPCDist;
						index = n.whoAmI;
					}
				}
			}
			if (index != -1)
			{
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Main.npc[index], owner.HeldItem.shootSpeed, 3);
			}
			else
			{
				if (base.Projectile.velocity.X != oldVelocity.X)
				{
					base.Projectile.velocity.X = 0f - oldVelocity.X;
				}
				if (base.Projectile.velocity.Y != oldVelocity.Y)
				{
					base.Projectile.velocity.Y = 0f - oldVelocity.Y;
				}
			}
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
			return false;
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(96, 255, 96, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (bounceTimer == 0)
		{
			base.Projectile.DrawBeam(40f, 2f, lightColor);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.3f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 14, 0.018f, Color.Lime, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.65f));
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.4f, 0.9f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.45f, 1.1f);
			dust.color = Color.Lime;
			dust.noLightEmittence = true;
		}
	}
}
