using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PlaguedFuelPackCloud : ModProjectile, ILocalizedModType, IModType
{
	public int dir;

	public float intensity;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 390;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ArmorPenetration = 10;
	}

	public override void AI()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 50)
		{
			base.Projectile.alpha += 5;
		}
		if (dir == 0)
		{
			base.Projectile.timeLeft -= Main.rand.Next(5, 21);
			dir = ((!Main.rand.NextBool()) ? 1 : (-1));
			intensity = Main.rand.NextFloat(0.2f, 1.2f);
		}
		if (Main.rand.NextBool(150))
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, base.Projectile.velocity.X * 0.4f, base.Projectile.velocity.Y * 0.4f, 100, default(Color), 2f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 1.2f;
			Main.dust[dust].velocity.Y -= 0.15f;
		}
		if (base.Projectile.timeLeft < 290)
		{
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, base.Projectile.Center.ClosestNPCAt(1000f), ignoreTiles: true, 0.65f * intensity, 6f, 0.98f, 0.995f, accelerate: true);
		}
		else
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.006f * (float)dir * intensity) * Main.rand.NextFloat(0.985f, 1f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft != 0)
		{
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, base.Projectile.velocity.X * 0.4f, base.Projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 1.2f;
				Main.dust[dust].velocity.Y -= 0.15f;
			}
		}
	}
}
