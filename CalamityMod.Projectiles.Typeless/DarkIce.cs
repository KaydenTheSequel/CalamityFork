using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DarkIce : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Melee/DarkIceZero";

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.npcProj = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] != 1f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item8, base.Projectile.Center);
		}
		if (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y) < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.04f;
		}
		int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 172, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.25f);
		Main.dust[index2].noGravity = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(198, 197, 246);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int i = 0; i < 30; i++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 172, 0f, 0f, 0, default(Color), Main.rand.NextFloat(1f, 2f));
			Main.dust[index2].noGravity = true;
			Dust obj = Main.dust[index2];
			obj.velocity *= 4f;
		}
	}
}
