using System;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class VeriumBoltProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/VeriumBolt";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.aiStyle = 1;
		base.Projectile.arrow = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, 2, Color.Plum);
		return false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 223 : 252, (Vector2?)new Vector2(base.Projectile.velocity.X * Main.rand.NextFloat(-0.1f, 0.1f), base.Projectile.velocity.Y * Main.rand.NextFloat(-0.1f, 0.1f)), 0, default(Color), 1f);
		dust.noGravity = true;
		if (dust.type == 223)
		{
			dust.scale = Main.rand.NextFloat(0.4f, 0.66f);
		}
		else
		{
			dust.scale = Main.rand.NextFloat(0.65f, 0.9f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.LightSkyBlue;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		CalamityGlobalNPC modNPC = target.Calamity();
		if (!modNPC.veriumDoomMarked)
		{
			modNPC.veriumDoomMarked = true;
			modNPC.veriumDoomTimer = 90;
		}
		modNPC.veriumDoomStacks++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 4; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 223, base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 0.9f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
		}
	}
}
