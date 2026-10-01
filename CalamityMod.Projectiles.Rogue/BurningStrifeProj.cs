using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BurningStrifeProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.timeLeft = 720;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f * (float)base.Projectile.direction;
		base.Projectile.velocity.Y += 0.05f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (base.Projectile.ai[0] >= 25f)
		{
			Dust.NewDust(base.Projectile.Center, 1, 1, 27, (0f - base.Projectile.velocity.X) * 0.3f, (0f - base.Projectile.velocity.Y) * 0.3f, 0, default(Color), 1.1f);
			base.Projectile.ai[0] = 0f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.7f;
		}
		base.Projectile.velocity.X *= 0.9f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 180);
		OnHitEffect(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Shadowflame>(), 180);
		OnHitEffect(target);
	}

	private void OnHitEffect(Entity target)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item103, base.Projectile.Center);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BurningStrifeExplosion>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int i = 0; i < 4; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Main.rand.NextVector2CircularEdge(10f, 10f), ModContent.ProjectileType<BurningTentacle>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, Main.rand.NextFloat(-0.1f, 0.1f), Main.rand.NextFloat(-0.1f, 0.1f));
			}
		}
	}
}
