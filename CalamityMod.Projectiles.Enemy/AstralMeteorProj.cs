using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class AstralMeteorProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 360;
		base.Projectile.Opacity = 0f;
	}

	public override void AI()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.1f, 0f, 1f);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.04f;
		int dustID = ModContent.DustType<AstralOrange>();
		Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, default(Color), 1.2f);
		dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
		dust.noGravity = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath14, base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(60);
		for (int i = 0; i < 15; i++)
		{
			int dustID = (Main.rand.NextBool(3) ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
			Dust astralParticle = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, default(Color), 1.2f);
			astralParticle.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				astralParticle.scale = 0.5f;
				astralParticle.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		if (Main.dedServ)
		{
			return;
		}
		int goreCount = 3;
		Vector2 goreSource = base.Projectile.Center;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreCount; goreIndex++)
		{
			float smokeSpeed = 0.33f;
			if (goreIndex < goreCount / 3)
			{
				smokeSpeed = 0.66f;
			}
			if (goreIndex >= 2 * goreCount / 3)
			{
				smokeSpeed = 1f;
			}
			int goreID = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), goreID);
			Gore obj = Main.gore[smoke];
			obj.velocity *= smokeSpeed;
			obj.velocity.X++;
			obj.velocity.Y++;
			goreID = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), goreID);
			Gore obj2 = Main.gore[smoke];
			obj2.velocity *= smokeSpeed;
			obj2.velocity.X--;
			obj2.velocity.Y++;
			goreID = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), goreID);
			Gore obj3 = Main.gore[smoke];
			obj3.velocity *= smokeSpeed;
			obj3.velocity.X++;
			obj3.velocity.Y--;
			goreID = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), goreID);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= smokeSpeed;
			obj4.velocity.X--;
			obj4.velocity.Y--;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 45);
		}
	}
}
