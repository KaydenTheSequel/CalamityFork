using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoHydraRay : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 10;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 265;
		base.Projectile.coldDamage = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 100;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 5f)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 spawnPosition = base.Projectile.position;
				spawnPosition -= base.Projectile.velocity * (float)i * 0.25f;
				int idx = Dust.NewDust(spawnPosition, 1, 1, 113, 0f, 0f, 0, default(Color), 1.25f);
				Main.dust[idx].position = spawnPosition;
				Main.dust[idx].scale = Main.rand.NextFloat(0.71f, 0.93f);
				Dust obj = Main.dust[idx];
				obj.velocity *= 0.1f;
				Main.dust[idx].noGravity = true;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}
}
