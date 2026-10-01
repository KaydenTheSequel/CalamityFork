using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DormantBrimseekerSummoner : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/DormantBrimseeker";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(-(float)Math.PI / 4f, 0.045f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		if (base.Projectile.ai[0]++ == 110f)
		{
			SoundEngine.PlaySound(in SoundID.Item100, base.Projectile.Center);
		}
		if (base.Projectile.ai[0]++ >= 90f)
		{
			for (int i = 0; i < (180 - (int)base.Projectile.ai[0]) / 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 235);
				dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(1f, 4f);
				dust.noGravity = true;
			}
			base.Projectile.alpha = (int)(255f * (base.Projectile.ai[0] - 90f) / 90f);
		}
		if (base.Projectile.ai[0] >= 180f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitY * 7f, ModContent.ProjectileType<DormantBrimseekerBab>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Projectile.originalDamage;
		}
	}
}
