using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BloodBreath : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 40;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 9;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, base.Projectile.Opacity * 0.77f, base.Projectile.Opacity * 0.15f, base.Projectile.Opacity * 0.08f);
		Time++;
		if (Time > 7f)
		{
			float dustScale = 1f;
			switch ((int)Time)
			{
			case 8:
				dustScale = 0.25f;
				break;
			case 9:
				dustScale = 0.5f;
				break;
			case 10:
				dustScale = 0.75f;
				break;
			}
			Time++;
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, (!ChildSafety.Disabled) ? 16 : 5, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 3f;
					dust.velocity *= 2f;
				}
				else
				{
					dust.scale *= 1.5f;
				}
				dust.velocity *= 1.2f;
				dust.scale *= dustScale;
			}
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 120);
		target.AddBuff(ModContent.BuffType<Laceration>(), 120);
	}
}
