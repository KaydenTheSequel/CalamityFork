using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BallisticPoisonBombSpike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.aiStyle = 93;
		base.AIType = 514;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha -= 10;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 4f)
		{
			int dustt = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, default(Color), 0.75f);
			Main.dust[dustt].noGravity = true;
			Dust obj = Main.dust[dustt];
			obj.velocity *= 0f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 2; i++)
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.2f);
			Dust obj = Main.dust[smoke];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[smoke].scale = 0.5f;
				Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 2; j++)
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.7f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103));
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
	}
}
