using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HydrothermicFlare : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 105 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.25f, 0f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
			base.Projectile.localAI[0]++;
		}
		for (int i = 0; i < 6; i++)
		{
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[d].noGravity = true;
			Dust obj = Main.dust[d];
			obj.velocity *= 0.5f;
			Dust obj2 = Main.dust[d];
			obj2.velocity += base.Projectile.velocity * 0.1f;
		}
		if (base.Projectile.timeLeft < 105)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 450f, 12f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
		target.AddBuff(323, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
		target.AddBuff(323, 180);
	}
}
