using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AstralScytheProjectile : ModProjectile, ILocalizedModType, IModType
{
	private int tileCounter = 5;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Enemy/MantisRing";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 8;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (tileCounter > 0)
		{
			tileCounter--;
		}
		if (tileCounter <= 0)
		{
			base.Projectile.tileCollide = true;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.03f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame > 2)
			{
				base.Projectile.frame = 0;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
		for (int i = 0; i < 60; i++)
		{
			Vector2 angleVec = ((float)Math.PI * 2f * Main.rand.NextFloat(0f, 1f)).ToRotationVector2();
			float distance = Main.rand.NextFloat(14f, 36f);
			Vector2 off = angleVec * distance;
			off.Y *= (float)base.Projectile.height / (float)base.Projectile.width;
			Dust.NewDustPerfect(base.Projectile.Center + off, ModContent.DustType<AstralBlue>(), angleVec * Main.rand.NextFloat(2f, 4f)).customData = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
