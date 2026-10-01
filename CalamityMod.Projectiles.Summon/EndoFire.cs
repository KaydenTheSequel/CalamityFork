using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoFire : ModProjectile, ILocalizedModType, IModType
{
	public bool speedXChoice;

	public bool speedYChoice;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 15;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 5;
		base.Projectile.timeLeft = 450;
		base.Projectile.tileCollide = false;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		float speedX = 1f;
		float speedY = 1f;
		if (!speedXChoice)
		{
			speedX = (Main.rand.NextBool() ? 1.03f : 0.97f);
			speedXChoice = true;
		}
		if (!speedYChoice)
		{
			speedY = (Main.rand.NextBool() ? 1.03f : 0.97f);
			speedYChoice = true;
		}
		base.Projectile.velocity.X *= speedX;
		base.Projectile.velocity.X *= speedY;
		if (base.Projectile.ai[0] > 20f)
		{
			float dustScale = 1f;
			if (base.Projectile.ai[0] == 21f)
			{
				dustScale = 0.25f;
			}
			else if (base.Projectile.ai[0] == 22f)
			{
				dustScale = 0.5f;
			}
			else if (base.Projectile.ai[0] == 23f)
			{
				dustScale = 0.75f;
			}
			base.Projectile.ai[0]++;
			int dustType = (Main.rand.NextBool() ? 68 : 67);
			if (Main.rand.NextBool(4))
			{
				dustType = 80;
			}
			if (Main.rand.NextBool())
			{
				int endoDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100, default(Color), 0.8f);
				Dust dust = Main.dust[endoDust];
				if (Main.rand.NextBool(3))
				{
					dust.scale *= 1.5f;
					dust.velocity.X *= 1.2f;
					dust.velocity.Y *= 1.2f;
				}
				else
				{
					dust.scale *= 0.85f;
				}
				dust.noGravity = true;
				dust.velocity.X *= 0.8f;
				dust.velocity.Y *= 0.8f;
				dust.scale *= dustScale;
				dust.velocity += base.Projectile.velocity;
				dust.noLight = true;
			}
		}
		else
		{
			base.Projectile.ai[0]++;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}
}
