using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeraphimAngelicLight2 : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 95;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public bool FireSmallLaser => base.Projectile.ai[0] == 1f;

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/Rogue/SeraphimAngelicLight";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 82;
		base.Projectile.height = 82;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 95;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = CalamityUtils.Convert01To010((float)base.Projectile.timeLeft / 95f);
		base.Projectile.Opacity = (float)Math.Sqrt(base.Projectile.scale);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		base.Projectile.frame = (int)(Utils.GetLerpValue(95f, 0f, base.Projectile.timeLeft, clamped: true) * (float)Main.projFrames[base.Type]);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 16; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
			dust.color = Color.Lerp(Color.Gold, Color.White, Main.rand.NextFloat(0.5f, 1f));
			dust.velocity = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * 5f;
			dust.scale = 0.9f;
			dust.noGravity = true;
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1300f, ignoreTiles: true, bossPriority: true);
		if (Main.myPlayer == base.Projectile.owner && potentialTarget != null)
		{
			int damage = base.Projectile.damage;
			Vector2 laserDirection = base.Projectile.SafeDirectionTo(potentialTarget.Center);
			int laser = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, laserDirection, ModContent.ProjectileType<SeraphimBeamLarge>(), damage, 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(laser))
			{
				Main.projectile[laser].scale = 0.5f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 255 - base.Projectile.alpha);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
