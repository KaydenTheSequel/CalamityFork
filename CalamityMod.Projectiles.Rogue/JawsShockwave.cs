using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class JawsShockwave : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 320;
		base.Projectile.height = 320;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 5)
		{
			return;
		}
		Vector2 dustVelocity = default(Vector2);
		for (int i = 0; i < 50; i++)
		{
			int dustToUse = Main.rand.Next(0, 4);
			int dustType = 0;
			switch (dustToUse)
			{
			case 0:
				dustType = 33;
				break;
			case 1:
				dustType = 101;
				break;
			case 2:
				dustType = 111;
				break;
			case 3:
				dustType = 180;
				break;
			}
			((Vector2)(ref dustVelocity))._002Ector(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
			((Vector2)(ref dustVelocity)).Normalize();
			dustVelocity *= 16f;
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, dustVelocity.X, dustVelocity.Y, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 240);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 160f, targetHitbox);
	}
}
