using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicIceBurst : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 60;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1] += 0.01f;
		base.Projectile.scale = base.Projectile.ai[1];
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= (float)(3 * Main.projFrames[base.Type]))
		{
			base.Projectile.Kill();
			return;
		}
		if (++base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.hide = true;
			}
		}
		base.Projectile.alpha -= 63;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.517f, (float)Main.DiscoG / 300f, 0.85f);
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = (int)(52f * base.Projectile.scale));
			base.Projectile.Center = base.Projectile.position;
			base.Projectile.Damage();
			SoundEngine.PlaySound(in SoundID.Item62, base.Projectile.position);
			for (int i = 0; i < 2; i++)
			{
				int cosmicDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 187, 0f, 0f, 100, new Color(150, 255, 255));
				Main.dust[cosmicDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			}
			for (int j = 0; j < 3; j++)
			{
				int iceDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 200, new Color(150, 255, 255));
				Main.dust[iceDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				Main.dust[iceDust].noGravity = true;
				Dust obj = Main.dust[iceDust];
				obj.velocity *= 3f;
				iceDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, new Color(150, 255, 255), 0.6f);
				Main.dust[iceDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				Dust obj2 = Main.dust[iceDust];
				obj2.velocity *= 2f;
				Main.dust[iceDust].noGravity = true;
				Main.dust[iceDust].fadeIn = 2.5f;
			}
			for (int k = 0; k < 2; k++)
			{
				int icyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 0, new Color(150, 255, 255));
				Main.dust[icyDust].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
				Main.dust[icyDust].noGravity = true;
				Dust obj3 = Main.dust[icyDust];
				obj3.velocity *= 3f;
			}
			for (int l = 0; l < 3; l++)
			{
				int freezeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 187, 0f, 0f, 0, new Color(150, 255, 255), 0.6f);
				Main.dust[freezeDust].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
				Main.dust[freezeDust].noGravity = true;
				Dust obj4 = Main.dust[freezeDust];
				obj4.velocity *= 3f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		Lighting.GetColor((int)(base.Projectile.position.X + (float)base.Projectile.width * 0.5f) / 16, (int)((base.Projectile.position.Y + (float)base.Projectile.height * 0.5f) / 16f));
		if (base.Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[base.Type])
		{
			Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
		}
		TextureAssets.Projectile[base.Type].Value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[2] != 1f)
		{
			target.AddBuff(ModContent.BuffType<Nightwither>(), 420);
		}
		base.Projectile.direction = Main.player[base.Projectile.owner].direction;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.ai[2] != 1f)
		{
			target.AddBuff(ModContent.BuffType<Nightwither>(), 420);
		}
		base.Projectile.direction = Main.player[base.Projectile.owner].direction;
	}
}
