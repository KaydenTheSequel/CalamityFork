using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PlanetaryAnnihilationProj : ModProjectile, ILocalizedModType, IModType
{
	private int dustType;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.arrow = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		switch ((int)base.Projectile.ai[1])
		{
		case 0:
			dustType = 15;
			break;
		case 1:
			dustType = 74;
			break;
		case 2:
			dustType = 73;
			break;
		case 3:
			dustType = 162;
			break;
		case 4:
			dustType = 90;
			break;
		case 5:
			dustType = 173;
			break;
		case 6:
			dustType = 57;
			break;
		}
		int addedDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 2.2f);
		Main.dust[addedDust].noGravity = true;
		Dust obj = Main.dust[addedDust];
		obj.velocity *= 0f;
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 200f, 12f, 20f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		int height = 40;
		Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * (float)base.Projectile.MaxUpdates;
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = height);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 2;
		base.Projectile.Damage();
		for (int i = 0; i < 20; i++)
		{
			int dustID = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 200, default(Color), 2.1f);
			Main.dust[dustID].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Main.dust[dustID].noGravity = true;
			Dust obj = Main.dust[dustID];
			obj.velocity *= 3f;
			Dust obj2 = Main.dust[dustID];
			obj2.velocity += dustVel * Main.rand.NextFloat();
			dustID = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 1.1f);
			Main.dust[dustID].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Dust obj3 = Main.dust[dustID];
			obj3.velocity *= 2f;
			Main.dust[dustID].noGravity = true;
			Main.dust[dustID].fadeIn = 1f;
			Dust obj4 = Main.dust[dustID];
			obj4.velocity += dustVel * Main.rand.NextFloat();
		}
		for (int j = 0; j < 10; j++)
		{
			int dustID2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[dustID2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
			Main.dust[dustID2].noGravity = true;
			Dust obj5 = Main.dust[dustID2];
			obj5.velocity *= 0.5f;
			Dust obj6 = Main.dust[dustID2];
			obj6.velocity += dustVel * (0.6f + 0.6f * Main.rand.NextFloat());
		}
	}
}
