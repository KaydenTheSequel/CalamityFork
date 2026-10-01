using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AuroraAustralis : ModProjectile, ILocalizedModType, IModType
{
	private static float CosFrequency = 0.05f;

	private static float CosAmplitude = 0.008f;

	public int[] dustTypes = new int[2]
	{
		ModContent.DustType<AstralBlue>(),
		ModContent.DustType<AstralOrange>()
	};

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		CosFrequency = 0.15f;
		CosAmplitude = 0.06f;
	}

	public override void AI()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.localAI[0] = base.Projectile.velocity.X;
			base.Projectile.localAI[1] = base.Projectile.velocity.Y;
		}
		base.Projectile.tileCollide = base.Projectile.ai[0] > 2f;
		Vector2 originalVelocity = default(Vector2);
		((Vector2)(ref originalVelocity))._002Ector(base.Projectile.localAI[0], base.Projectile.localAI[1]);
		ApplyCosVelocity(originalVelocity);
		float currentSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		float maxSpeed = 1.4f * ((Vector2)(ref originalVelocity)).Length();
		if (currentSpeed > maxSpeed)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= maxSpeed / currentSpeed;
		}
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		int rainbow = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
		Dust obj = Main.dust[rainbow];
		obj.velocity *= 0.1f;
		obj.velocity += base.Projectile.velocity * 0.2f;
		obj.position.X = base.Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
		obj.position.Y = base.Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
		obj.noGravity = true;
		if (base.Projectile.timeLeft % 10 == 0 && Main.myPlayer == base.Projectile.owner)
		{
			IEntitySource source = base.Projectile.GetSource_FromThis();
			if (Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<AstralStarMagic>()] < 30)
			{
				float dmgKBMult = Main.rand.NextFloat(0.25f, 0.75f);
				Projectile projectile2 = CalamityUtils.ProjectileRain(source, base.Projectile.Center, base.Projectile.velocity.X, 100f, 500f, 800f, Main.rand.NextFloat(10f, 20f), ModContent.ProjectileType<AstralStarMagic>(), (int)((float)base.Projectile.damage * dmgKBMult), base.Projectile.knockBack * dmgKBMult, base.Projectile.owner);
				projectile2.timeLeft = 120;
				projectile2.ai[0] = 1f;
			}
		}
		base.Projectile.ai[0]++;
	}

	private void ApplyCosVelocity(Vector2 baseVelocity)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		float radians = 0f - (-(float)Math.PI / 2f + CosFrequency * base.Projectile.ai[0]);
		Projectile projectile = base.Projectile;
		projectile.velocity += CosAmplitude * baseVelocity.RotatedBy(radians);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 5; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}
}
