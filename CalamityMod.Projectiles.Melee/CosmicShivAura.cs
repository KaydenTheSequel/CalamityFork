using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicShivAura : ModProjectile, ILocalizedModType, IModType
{
	public readonly int SwordsAverageDelay = 40;

	public readonly int SwordsRandomOffset = 15;

	public int CurrentSwordTimer;

	public NPC target;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 240;
	}

	public override void OnSpawn(IEntitySource source)
	{
		if (Main.npc[(int)base.Projectile.ai[0]] != null || Main.npc[(int)base.Projectile.ai[0]].active)
		{
			target = Main.npc[(int)base.Projectile.ai[0]];
		}
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (target == null || !target.active)
		{
			base.Projectile.Kill();
		}
		base.Projectile.Center = Main.npc[(int)base.Projectile.ai[0]].Center;
		if (base.Projectile.ai[1] == (float)CurrentSwordTimer)
		{
			Vector2 randomDirection = Main.rand.NextFloat(0f, (float)Math.PI * 2f).ToRotationVector2();
			((Vector2)(ref randomDirection)).Normalize();
			int randomDistance = Main.rand.Next(200, 426);
			Vector2 spawnPos = base.Projectile.Center + randomDirection * (float)randomDistance;
			Vector2 velocity = Vector2.Normalize(base.Projectile.Center - spawnPos) * Utils.GetLerpValue(-100f, 426f, randomDistance, clamped: true) * 20f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos.X, spawnPos.Y, velocity.X, velocity.Y, ModContent.ProjectileType<CosmicShivBlade>(), base.Projectile.damage, base.Projectile.knockBack * 0.8f, base.Projectile.owner, randomDistance);
			base.Projectile.ai[1] = 0f;
			CurrentSwordTimer = Main.rand.Next(SwordsAverageDelay - SwordsRandomOffset, SwordsAverageDelay + SwordsRandomOffset);
		}
		base.Projectile.ai[1]++;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
