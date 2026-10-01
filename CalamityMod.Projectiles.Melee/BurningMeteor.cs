using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BurningMeteor : ModProjectile, ILocalizedModType, IModType
{
	private int noTileHitCounter = 120;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.alpha = 150;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 180;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		int randomToSubtract = Main.rand.Next(1, 4);
		noTileHitCounter -= randomToSubtract;
		if (noTileHitCounter == 0)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
			}
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 30f)
		{
			base.Projectile.localAI[0] = 0f;
			for (int l = 0; l < 12; l++)
			{
				Vector2 dustRotate = Vector2.UnitX * (0f - (float)base.Projectile.width) / 2f;
				dustRotate += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				dustRotate = dustRotate.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
				int burntDust = Dust.NewDust(base.Projectile.Center, 0, 0, 244, 0f, 0f, 160);
				Main.dust[burntDust].scale = 1.1f;
				Main.dust[burntDust].noGravity = true;
				Main.dust[burntDust].position = base.Projectile.Center + dustRotate;
				Main.dust[burntDust].velocity = base.Projectile.velocity * 0.1f;
				Main.dust[burntDust].velocity = Vector2.Normalize(base.Projectile.Center - base.Projectile.velocity * 3f - Main.dust[burntDust].position) * 1.25f;
			}
		}
		base.Projectile.alpha -= 15;
		int alphaControl = 150;
		if (base.Projectile.Center.Y >= base.Projectile.ai[1])
		{
			alphaControl = 0;
		}
		if (base.Projectile.alpha < alphaControl)
		{
			base.Projectile.alpha = alphaControl;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 244);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 175)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
