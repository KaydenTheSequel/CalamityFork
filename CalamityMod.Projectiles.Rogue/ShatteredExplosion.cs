using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ShatteredExplosion : ModProjectile, ILocalizedModType, IModType
{
	private bool dust = true;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 15;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.ai[0] = 0f;
			dust = false;
		}
		base.Projectile.localAI[0]++;
		if (dust && base.Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 5; i++)
			{
				int num469 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num469].noGravity = true;
				Dust obj = Main.dust[num469];
				obj.velocity *= 0f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}
}
