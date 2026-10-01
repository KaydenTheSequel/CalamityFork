using System;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LiliesOfFinalityAoE : ModProjectile, ILocalizedModType, IModType
{
	private Projectile Ariane;

	private const int TimeToFullScale = 120;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private ref float ArianeID => ref base.Projectile.ai[0];

	private ref float Timer => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = LiliesOfFinality.Ariane_AoESize);
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / 2f, targetHitbox);
	}

	public override void AI()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		if (Ariane == null)
		{
			Ariane = Main.projectile[(int)ArianeID];
		}
		if (Ariane != null && Ariane.active && Ariane.owner == base.Projectile.owner && Ariane.type == ModContent.ProjectileType<LiliesOfFinalityAriane>() && Ariane.ModProjectile<LiliesOfFinalityAriane>().State == LiliesOfFinalityAriane.AIState.Attack)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.Center = Ariane.Center;
		if (!Main.dedServ)
		{
			float currentScale = Utils.Remap(Timer, 0f, 120f, 0f, (float)base.Projectile.width / 2f);
			int circleDustAmount = (int)Utils.Remap(Timer, 0f, 120f, 5f, 40f) + Main.rand.Next(20);
			for (int i = 0; i < circleDustAmount; i++)
			{
				if (Main.rand.NextBool(20))
				{
					Vector2 position = ((float)Math.PI * 2f / (float)circleDustAmount * (float)i).ToRotationVector2() * (currentScale + Main.rand.NextFloat(10f));
					GeneralParticleHandler.SpawnParticle(new ArianeFakeDust(base.Projectile, position + Main.rand.NextVector2Circular(15f, 15f), Vector2.Zero, Color.Red, Main.rand.NextFloat(1.5f, 2.5f), 120));
				}
			}
			int eyeDustAmount = (int)Utils.Remap(Timer, 0f, 120f, 0f, 20f);
			for (int j = 0; j < eyeDustAmount; j++)
			{
				if (Main.rand.NextBool(15))
				{
					float interpolator = Main.rand.NextFloat();
					float maxOffsetX = Utils.Remap(Timer, 0f, 120f, 0f, 450f);
					float maxOffsetY = Utils.Remap(Timer, 0f, 120f, 0f, 200f);
					Vector2 xPosition = Vector2.UnitX * MathHelper.Lerp(0f - maxOffsetX, maxOffsetX, interpolator);
					Vector2 yPosition = Vector2.UnitY * MathHelper.Lerp(0f, maxOffsetY, MathF.Pow(CalamityUtils.Convert01To010(interpolator), 0.7f)) * (float)Main.rand.NextBool().ToDirectionInt();
					GeneralParticleHandler.SpawnParticle(new ArianeFakeDust(base.Projectile, xPosition + yPosition + Main.rand.NextVector2Circular(15f, 15f), Vector2.Zero, Color.Crimson * 0.8f, Main.rand.NextFloat(2f, 2.5f), 120));
				}
			}
			Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(currentScale, currentScale);
			int commonDustID = LiliesOfFinality.CommonDustID;
			Vector2? velocity = Main.rand.NextVector2Circular(3f, 3f);
			float scale = Main.rand.NextFloat(0.8f, 1.2f);
			Dust.NewDustPerfect(position2, commonDustID, velocity, 0, default(Color), scale).noGravity = true;
		}
		Timer++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= LiliesOfFinality.Ariane_AoEDMGMultiplier;
	}
}
