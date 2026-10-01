using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NeedlerProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 2;
		base.Projectile.aiStyle = 93;
		base.AIType = 514;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 1;
	}

	public override void AI()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha -= 10;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 6f && base.Projectile.numHits < 1)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 207 : 256, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f));
			dust.scale = Main.rand.NextFloat(0.6f, 0.9f);
			dust.noGravity = true;
		}
		if (base.Projectile.localAI[1] == 4f)
		{
			for (int i = 0; i <= 8; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 40 : 207, (base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.65f)).RotatedByRandom(0.4000000059604645));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.9f, 1.6f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 90);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 90);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 48);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int j = 0; j < 4; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 46, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.3f, 1.3f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.8f, 1.8f);
			if (Main.rand.NextBool())
			{
				dust.fadeIn = 0.5f;
			}
		}
		for (int k = 0; k < 9; k++)
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? 44 : 39, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.3f, 1.3f));
			dust2.noGravity = false;
			dust2.scale = Main.rand.NextFloat(0.8f, 1.8f);
			dust2.fadeIn = 0.5f;
		}
	}
}
