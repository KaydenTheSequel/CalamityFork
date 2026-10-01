using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MirrorBladeSlash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/ExobeamSlash";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 512;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 2;
		base.Projectile.Opacity = 1f;
		base.Projectile.timeLeft = 35;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.scale = 0.75f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Opacity = (float)base.Projectile.timeLeft / 35f;
		if (base.Projectile.timeLeft != 34)
		{
			return;
		}
		Vector2 vel = Utils.RotatedByRandom(new Vector2(0.1f, 0.1f), 100.0);
		GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(base.Projectile.Center, vel, affectedByGravity: false, 9, Main.rand.NextFloat(0.25f, 0.35f), Main.rand.NextBool() ? Color.Silver : Color.BlueViolet));
		for (int j = -1; j <= 1; j += 2)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), vel.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(2f, 12.5f) * (float)j);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.7f);
				dust.color = (Main.rand.NextBool() ? Color.SteelBlue : Color.BlueViolet);
				dust.noLightEmittence = true;
				dust.fadeIn = 1f;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.67f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 120);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override bool ShouldUpdatePosition()
	{
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp((base.Projectile.ai[2] == 0f) ? Color.Blue : Color.White, (base.Projectile.ai[2] == 0f) ? Color.DarkBlue : Color.White, (float)base.Projectile.identity / 7f % 1f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
