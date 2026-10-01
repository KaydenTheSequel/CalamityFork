using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HydrothermicFlareRogue : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 285 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.25f, 0f);
		int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 2f);
		Main.dust[d].noGravity = true;
		Dust obj = Main.dust[d];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[d];
		obj2.velocity += base.Projectile.velocity * 0.1f;
		if (base.Projectile.timeLeft < 285)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 250f, 11f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 180);
	}
}
