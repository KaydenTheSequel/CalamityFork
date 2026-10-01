using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MagnomalyRocket : ModProjectile, ILocalizedModType, IModType
{
	private bool spawnedAura;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.25f / 255f, (float)Main.DiscoG * 0.25f / 255f, (float)Main.DiscoB * 0.25f / 255f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 7)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		int dustType = (Main.rand.NextBool() ? 107 : 234);
		if (Main.rand.NextBool(4))
		{
			dustType = 269;
		}
		if (base.Projectile.owner == Main.myPlayer && !spawnedAura)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MagnomalyAura>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack * 0.5f, base.Projectile.owner, base.Projectile.identity);
			spawnedAura = true;
		}
		float dustOffsetX = base.Projectile.velocity.X * 0.5f;
		float dustOffsetY = base.Projectile.velocity.Y * 0.5f;
		if (Main.rand.NextBool())
		{
			int exo = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + dustOffsetX, base.Projectile.position.Y + 3f + dustOffsetY) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, dustType, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[exo].scale *= (float)Main.rand.Next(10) * 0.1f;
			Dust obj = Main.dust[exo];
			obj.velocity *= 0.2f;
			Main.dust[exo].noGravity = true;
			Main.dust[exo].noLight = true;
		}
		else
		{
			int exo2 = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + dustOffsetX, base.Projectile.position.Y + 3f + dustOffsetY) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, dustType, 0f, 0f, 100, default(Color), 0.25f);
			Main.dust[exo2].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
			Dust obj2 = Main.dust[exo2];
			obj2.velocity *= 0.05f;
			Main.dust[exo2].noGravity = true;
			Main.dust[exo2].noLight = true;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 300f, 12f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		base.Projectile.ExpandHitboxBy(192);
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MagnomalyExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		int dustType = (Main.rand.NextBool() ? 107 : 234);
		if (Main.rand.NextBool(4))
		{
			dustType = 269;
		}
		for (int d = 0; d < 30; d++)
		{
			int exo = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100);
			Dust obj = Main.dust[exo];
			obj.velocity *= 3f;
			Main.dust[exo].noGravity = true;
			Main.dust[exo].noLight = true;
			if (Main.rand.NextBool())
			{
				Main.dust[exo].scale = 0.5f;
				Main.dust[exo].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 40; i++)
		{
			int exo2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[exo2].noGravity = true;
			Main.dust[exo2].noLight = true;
			Dust obj2 = Main.dust[exo2];
			obj2.velocity *= 5f;
			exo2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 0.75f);
			Dust obj3 = Main.dust[exo2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = base.Projectile.Center;
		int goreAmt = 9;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			ModContent.GetInstance<CalamityMod>();
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X++;
			obj4.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj5 = Main.gore[smoke];
			obj5.velocity *= velocityMult;
			obj5.velocity.X--;
			obj5.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj6 = Main.gore[smoke];
			obj6.velocity *= velocityMult;
			obj6.velocity.X++;
			obj6.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj7 = Main.gore[smoke];
			obj7.velocity *= velocityMult;
			obj7.velocity.X--;
			obj7.velocity.Y--;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects();
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects();
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	private void OnHitEffects()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			float angleOffset = Main.rand.NextFloat(-30f, 30f);
			for (int i = 0; i < 8; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - (MathHelper.ToRadians(67.5f + angleOffset) - base.Projectile.velocity.ToRotation())).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<MagnomalyBeam>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack * 0.25f, base.Projectile.owner, 0f, 1f);
			}
		}
	}
}
