using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Shortswords;

public class CosmicShivProj : BaseShortswordProjectile
{
	public bool MeleeEffect;

	public int NumHits;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<CosmicShiv>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/CosmicShiv";

	public override Action<Projectile> EffectBeforePullback => delegate
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 3f, ModContent.ProjectileType<CosmicShivTrail>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
	};

	public override void SetDefaults()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Size = new Vector2(24f);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 1f;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.timeLeft = 360;
		base.Projectile.extraUpdates = 1;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void SetVisualOffsets()
	{
		int HalfProjWidth = base.Projectile.width / 2;
		int HalfProjHeight = base.Projectile.height / 2;
		base.DrawOriginOffsetX = 0f;
		base.DrawOffsetX = -(24 - HalfProjWidth);
		base.DrawOriginOffsetY = -(24 - HalfProjHeight);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffect();
		if (NumHits < 3)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, Vector2.Zero, ModContent.ProjectileType<CosmicShivAura>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, target.whoAmI);
			NumHits++;
		}
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffect();
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 240);
	}

	public void OnHitEffect()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		float rand2PI = Main.rand.NextFloat((float)Math.PI * 2f);
		int petalCount = Main.rand.Next(5, 8);
		float speed = Main.rand.Next(18, 24);
		if (!MeleeEffect && Main.rand.NextBool())
		{
			for (float k = 0f; k < (float)Math.PI * 2f; k += 0.08f)
			{
				float scale = Main.rand.NextFloat(1.5f, 1.9f);
				float randomWhitingValue = Main.rand.NextFloat(0f, 0.2f);
				Color color = Color.Lerp(CosmicShivTrail.DustColors[Main.rand.Next(0, CosmicShivTrail.DustColors.Count)], Color.White, randomWhitingValue);
				Vector2 velocity = StarPolarEquation(petalCount, k, rand2PI) * speed * 2f;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity, 0, color, scale);
				dust.noGravity = true;
				dust.fadeIn = -1f;
				Vector2 velocity2 = StarPolarEquation(petalCount, k - 0.04f, rand2PI) * speed * 2f * 0.9f;
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity2, 0, color, scale);
				dust2.noGravity = true;
				dust2.fadeIn = -1f;
			}
		}
	}

	public Vector2 StarPolarEquation(int pointCount, float angle, float offset)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		float sqrt3 = 1.732051f;
		float numerator = MathF.Cos((float)Math.PI * ((float)pointCount + 1f) / (float)pointCount);
		float denominator = MathF.Cos((MathF.Asin(MathF.Cos((float)pointCount * angle + offset)) * 2f + (float)Math.PI / 2f * (float)pointCount) / ((float)pointCount * 2f));
		return angle.ToRotationVector2() * numerator / denominator / sqrt3;
	}
}
