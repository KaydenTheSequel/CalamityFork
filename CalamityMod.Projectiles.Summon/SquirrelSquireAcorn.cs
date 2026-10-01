using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SquirrelSquireAcorn : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.timeLeft = SquirrelSquireStaff.ProjectileTimeAlive;
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < SquirrelSquireStaff.ProjectileTimeAlive - SquirrelSquireStaff.TimeBeforeFalling)
		{
			base.Projectile.velocity.Y += SquirrelSquireStaff.ProjectileGravity;
		}
		base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.velocity.X * 3f);
		if (!Main.dedServ && Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 7 : 79);
			dust.noGravity = true;
			dust.scale = 0.85f;
			dust.velocity = base.Projectile.velocity * 0.4f;
			dust.noLight = true;
			dust.noLightEmittence = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(SquirrelSquireStaff.ProjectileAoERadiusSize * 2);
		if (Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.Damage();
		}
		if (!Main.dedServ)
		{
			for (int k = 0; k < 6; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 7 : 167);
				dust.scale = Main.rand.NextFloat(0.6f, 1.1f);
				dust.velocity = Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f);
				dust.noGravity = false;
				GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.4f, 0.9f), affectedByGravity: true, 15, 0.7f, Color.Lerp(Color.White, Color.Brown, Main.rand.NextFloat(0.3f, 0.7f)) * 0.5f));
			}
		}
	}
}
