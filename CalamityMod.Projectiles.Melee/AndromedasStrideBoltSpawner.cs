using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AndromedasStrideBoltSpawner : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target => Main.npc[(int)base.Projectile.ai[0]];

	public ref float ChargeLevel => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.hide = true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.timeLeft = 15 * (int)ChargeLevel;
			base.Projectile.localAI[0] = 1f;
		}
		if (!Target.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Target.Center;
		if (base.Projectile.timeLeft < 59 && Main.rand.NextBool(3))
		{
			Vector2 flyDirection = -Vector2.UnitY.RotatedByRandom(0.39269909262657166) * Main.rand.NextFloat(15f, 35f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, flyDirection, Color.Lerp(Color.MidnightBlue, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(0.4f, 1.3f) * base.Projectile.scale, 0.8f, 0f, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, flyDirection, Color.Red, 20, Main.rand.NextFloat(0.1f, 0.7f) * base.Projectile.scale, 0.8f, 0f, glowing: true, 0.01f, required: true));
			}
		}
		if (base.Projectile.timeLeft % 15 != 5)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalDryadTouch, base.Projectile.Center);
		Vector2 starPos = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			if (Owner.whoAmI == Main.myPlayer)
			{
				((Vector2)(ref starPos))._002Ector(base.Projectile.Center.X + Main.rand.NextFloat(-250f, 250f), base.Projectile.Center.Y - Main.rand.NextFloat(650f, 750f));
				Vector2 starVel = (base.Projectile.Center - starPos).SafeNormalize(Vector2.UnitY) * 27f;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), starPos, starVel, ModContent.ProjectileType<GalaxiaBolt>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI, 0.75f, (float)Math.PI / 20f).scale = 2f;
			}
		}
		for (int j = 0; j < 4; j++)
		{
			Vector2 hitPositionDisplace = -Vector2.UnitY * Main.rand.NextFloat(10f);
			Vector2 flyDirection2 = -Vector2.UnitY.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(5f, 15f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + hitPositionDisplace, flyDirection2, Color.Lerp(Color.MidnightBlue, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(1f, 1.6f) * base.Projectile.scale, 0.8f, 0f, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + hitPositionDisplace, flyDirection2, Color.Red, 20, Main.rand.NextFloat(1.1f, 1.4f) * base.Projectile.scale, 0.8f, 0f, glowing: true, 0.005f, required: true));
			}
		}
	}
}
