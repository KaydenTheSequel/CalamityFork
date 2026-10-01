using System;
using CalamityMod.Events;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HomingGasBulb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item17, base.Projectile.Center);
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.2f, 0.5f);
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		int closestPlayer = Player.FindClosest(base.Projectile.Center, 1, 1);
		Vector2 velocity = Main.player[closestPlayer].Center - base.Projectile.Center;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= (death ? 0f : 30f))
		{
			if (base.Projectile.ai[0] < (death ? 210f : 150f))
			{
				float scaleFactor2 = ((Vector2)(ref base.Projectile.velocity)).Length();
				((Vector2)(ref velocity)).Normalize();
				velocity *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 22f + velocity) / (death ? 12f : 15f);
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= scaleFactor2;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
			{
				base.Projectile.tileCollide = true;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.02f;
			}
		}
		if (base.Projectile.ai[0] % (death ? 10f : 20f) != 0f)
		{
			return;
		}
		int dustType = 73;
		int totalDust = 12;
		float radians = (float)Math.PI * 2f / (float)totalDust;
		Vector2 spinningPoint = default(Vector2);
		((Vector2)(ref spinningPoint))._002Ector(0f, -1f);
		for (int k = 0; k < totalDust; k++)
		{
			Vector2 projectileVelocity = spinningPoint.RotatedBy(radians * (float)k);
			Vector2 spawnOffset = base.Projectile.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 10f;
			float randomSpeed = Main.rand.NextFloat(0.8f, 1.2f);
			Vector2 dustVelocity = projectileVelocity * randomSpeed;
			for (int l = 0; l < 2; l++)
			{
				Dust.NewDust(spawnOffset, 2, 2, dustType, dustVelocity.X, dustVelocity.Y);
			}
		}
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			int type = ModContent.ProjectileType<HomingGasBulbSporeGas>();
			float ai0 = Main.rand.Next(3);
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Normalize(base.Projectile.velocity) * 0.2f, type, PlanteraAI.PinkCloudDamage, 0f, Main.myPlayer, ai0);
			Main.projectile[proj].timeLeft = 180;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
		for (int i = 0; i < 15; i++)
		{
			if (!Main.rand.NextBool(3))
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 166);
			}
			else
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 167);
			}
		}
	}
}
