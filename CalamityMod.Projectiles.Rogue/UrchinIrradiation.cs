using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class UrchinIrradiation : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.4f, 0.15f);
		if (base.Projectile.timeLeft != 60)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int randDust;
			switch (Main.rand.Next(4))
			{
			case 0:
			case 1:
				randDust = 33;
				break;
			default:
				randDust = 75;
				break;
			case 3:
				randDust = 89;
				break;
			}
			Dust burst = Dust.NewDustPerfect(base.Projectile.Center, randDust, Main.rand.NextVector2Circular(7f, 7f), 100, default(Color), 1.5f);
			if (randDust == 89)
			{
				burst.scale *= 0.7f;
			}
			burst.noGravity = true;
		}
		GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Vector2.Zero, new Color(105, 255, 122), Color.Green, 1.2f, 200f, 0.15f * (float)Main.rand.NextBool().ToDirectionInt()));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 360);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 360);
	}
}
