using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class LeviatitanAberration : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Summon/GastricBelcher";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.AIType = 14;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 500f, 16f, 8f);
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(360f) * (float)base.Projectile.direction;
		if (base.Projectile.frameCounter++ % 6 == 0)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		for (int i = 0; i < 3; i++)
		{
			int bloodDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height / 2, 5, 0f, 0f, 100);
			Main.dust[bloodDust].noGravity = true;
			Dust obj = Main.dust[bloodDust];
			obj.velocity *= 0.5f;
			Dust obj2 = Main.dust[bloodDust];
			obj2.velocity += base.Projectile.velocity * 0.1f;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 36; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 323);
			dust.velocity = ((float)Math.PI * 2f * (float)i / 36f).ToRotationVector2() * 9f;
			dust.scale = Main.rand.NextFloat(1.4f, 1.6f);
			dust.noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 120);
		SoundStyle style = SoundID.NPCDeath1 with
		{
			Volume = SoundID.NPCDeath1.Volume * 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 15; d++)
		{
			int idx = Dust.NewDust(base.Projectile.Center - Vector2.One * 10f, 50, 50, 5, 0f, -2f);
			Dust obj = Main.dust[idx];
			obj.velocity /= 2f;
		}
	}
}
