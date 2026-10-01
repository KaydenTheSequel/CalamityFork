using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SageNeedle : ModProjectile, ILocalizedModType, IModType
{
	public const int OnDeathHealValue = 1;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 150;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(48f);
		base.Projectile.tileCollide = base.Projectile.velocity.Y > 0f;
		if (base.Projectile.velocity.Y < 12f)
		{
			base.Projectile.velocity.Y += 0.16f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int sagePoisonDamage = (int)(SagePoison.debuffData.EnemyLostRegen * (float)Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<SageSpirit>()]);
		target.AddBuff(ModContent.BuffType<SagePoison>(), 300);
		target.Calamity().sagePoisonDamage = sagePoisonDamage;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 2, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f);
				dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 3f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.IndianRed, Color.MediumVioletRed, Main.rand.NextFloat());
			}
		}
	}
}
