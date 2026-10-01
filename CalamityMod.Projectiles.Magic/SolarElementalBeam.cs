using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SolarElementalBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1000f;

	public override float Lifetime => 30f;

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 13;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = (int)Lifetime;
	}

	public override void ExtraBehavior()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ || base.Time != 5f)
		{
			return;
		}
		int starPoints = 8;
		for (int i = 0; i < starPoints; i++)
		{
			float angle = (float)Math.PI * 2f * (float)i / (float)starPoints;
			for (int j = 0; j < 12; j++)
			{
				float starSpeed = MathHelper.Lerp(2f, 10f, (float)j / 12f);
				Color dustColor = Color.Lerp(Color.White, Color.Yellow, (float)j / 12f);
				float dustScale = MathHelper.Lerp(1.6f, 0.85f, (float)j / 12f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 6);
				dust.velocity = angle.ToRotationVector2() * starSpeed;
				dust.color = dustColor;
				dust.scale = dustScale;
				dust.noGravity = true;
			}
		}
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = (float)base.Projectile.timeLeft / Lifetime * MaxScale;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		DrawBeamWithColor(Color.Lerp(Color.OrangeRed, Color.Transparent, 0.25f), base.Projectile.scale);
		DrawBeamWithColor(Color.Lerp(Color.Yellow * 1.1f, Color.Transparent, 0.25f), base.Projectile.scale * 0.4f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
		int type = ModContent.ProjectileType<FuckYou>();
		int boomDamage = (int)((double)hit.Damage * 1.1);
		int boom = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, type, boomDamage, hit.Knockback, base.Projectile.owner, 0f, Main.rand.NextFloat(0.85f, 2f));
		Main.projectile[boom].DamageType = DamageClass.Magic;
	}
}
