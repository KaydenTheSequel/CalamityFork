using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AstralCrystal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Boss/AstralFlame";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		bool blue = Main.rand.NextBool();
		float angleStart = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		for (float angle = 0f; angle < (float)Math.PI * 2f; angle += 0.05f)
		{
			blue = !blue;
			Vector2 velocity = angle.ToRotationVector2() * (2f + (float)(Math.Sin(angleStart + angle * 3f) + 1.0) * 2.5f) * Main.rand.NextFloat(0.95f, 1.05f);
			Dust.NewDustPerfect(base.Projectile.Center, blue ? ModContent.DustType<AstralBlue>() : ModContent.DustType<AstralOrange>(), velocity).customData = 0.025f;
		}
		for (int i = 0; i < Main.rand.Next(5, 9); i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<AstralChunkDust>());
		}
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (float i2 = 0f; i2 < (float)Math.PI * 2f; i2 += (float)Math.PI / 8f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, i2.ToRotationVector2() * 9f, ModContent.ProjectileType<AstralCrystalInvisibleExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Math.Max((int)((Color)(ref lightColor)).R, 150), Math.Max((int)((Color)(ref lightColor)).G, 150), Math.Max((int)((Color)(ref lightColor)).B, 150));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.Kill();
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.Center.Y > base.Projectile.ai[0])
		{
			base.Projectile.tileCollide = true;
		}
		Vector2 vect = base.Projectile.velocity;
		((Vector2)(ref vect)).Normalize();
		vect *= 32f;
		Vector2 val = base.Projectile.Center + vect;
		Vector2 perp = default(Vector2);
		((Vector2)(ref perp))._002Ector(base.Projectile.velocity.Y, 0f - base.Projectile.velocity.X);
		((Vector2)(ref perp)).Normalize();
		bool flag = Main.time % 2.0 == 0.0;
		int blue = ModContent.DustType<AstralBlue>();
		int orange = ModContent.DustType<AstralOrange>();
		base.Projectile.ai[1] += 0.3141f;
		Vector2 posOff = perp * (float)Math.Sin(base.Projectile.ai[1]) * 6f;
		Dust d1 = Dust.NewDustPerfect(val + posOff, flag ? blue : orange, perp * Main.rand.NextFloat(2.3f, 3.5f));
		Dust d2 = Dust.NewDustPerfect(val - posOff, flag ? orange : blue, -perp * Main.rand.NextFloat(2.3f, 3.5f));
		d1.customData = (d2.customData = 0.035f);
		if (Main.rand.NextBool(30))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<AstralChunkDust>());
			dust.velocity *= 0.3f;
		}
	}
}
