using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MiasmaGas : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.penetrate = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.alpha = Main.rand.Next(35, 75);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 360f)
		{
			base.Projectile.ai[1] = 1f;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9865f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.0003f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		base.Projectile.ai[1] = 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		base.Projectile.ai[1] = 1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 25; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, 48, 30, 189);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(39f));
			dust.alpha = 127;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
