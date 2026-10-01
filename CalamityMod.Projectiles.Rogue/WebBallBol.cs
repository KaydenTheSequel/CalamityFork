using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WebBallBol : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/WebBall";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.aiStyle = 14;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(12))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 30, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(149, 60);
		}
		else
		{
			target.AddBuff(149, 30);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(149, 60);
		}
		else
		{
			target.AddBuff(149, 30);
		}
	}
}
