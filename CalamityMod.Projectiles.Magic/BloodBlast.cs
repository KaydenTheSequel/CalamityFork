using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BloodBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 2;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0f, 0f);
		for (int i = 0; i < 2; i++)
		{
			float shortXVel = base.Projectile.velocity.X / 3f * (float)i;
			float shortYVel = base.Projectile.velocity.Y / 3f * (float)i;
			int fourConst = 4;
			int bloody = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)fourConst, base.Projectile.position.Y + (float)fourConst), base.Projectile.width - fourConst * 2, base.Projectile.height - fourConst * 2, 5, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[bloody];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.1f;
			obj.position.X -= shortXVel;
			obj.position.Y -= shortYVel;
		}
		if (Main.rand.NextBool(5))
		{
			int otherFourConst = 4;
			int graphicContent = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)otherFourConst, base.Projectile.position.Y + (float)otherFourConst), base.Projectile.width - otherFourConst * 2, base.Projectile.height - otherFourConst * 2, 5, 0f, 0f, 100, default(Color), 0.6f);
			Dust obj2 = Main.dust[graphicContent];
			obj2.velocity *= 0.25f;
			Dust obj3 = Main.dust[graphicContent];
			obj3.velocity += base.Projectile.velocity * 0.5f;
		}
		if (base.Projectile.ai[1] >= 20f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		else
		{
			base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}
}
