using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class StardustElementalBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override float MaxScale => 0.85f;

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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner && base.Time == 5f)
		{
			int type = ModContent.ProjectileType<BeamStar>();
			int damage = (int)((double)base.Projectile.damage * 0.75);
			for (int i = 0; i < 2; i++)
			{
				Vector2 starSpeed = base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)i) * 5f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, starSpeed, type, damage, base.Projectile.knockBack, base.Projectile.owner);
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
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		DrawBeamWithColor(Color.Lerp(Color.CornflowerBlue * 1.1f, Color.Transparent, 0.3f), base.Projectile.scale);
		DrawBeamWithColor(Color.Lerp(Color.Cyan, Color.Transparent, 0.3f), base.Projectile.scale * 0.5f);
		DrawBeamWithColor(Color.Lerp(Color.White, Color.Transparent, 0.3f), base.Projectile.scale * 0.2f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
		if (Main.rand.NextBool(7))
		{
			float spawnOffsetSpread = Main.rand.NextFloat(MathHelper.ToRadians(36f), MathHelper.ToRadians(64f));
			float baseOffsetAngle = Main.rand.NextFloat(-0.6f, 0.6f);
			int type = ModContent.ProjectileType<BeamStar>();
			hit.Damage = (int)((double)base.Projectile.damage * 0.7);
			for (int i = 0; i < 4; i++)
			{
				float spawnOffsetAngle = MathHelper.Lerp(spawnOffsetSpread * -0.5f, spawnOffsetSpread * 0.5f, (float)i / 4f) + baseOffsetAngle;
				Vector2 spawnPosition = target.Top - Vector2.UnitY.RotatedBy(spawnOffsetAngle) * 65f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, -Vector2.UnitY.RotatedBy(spawnOffsetAngle) * 2f, type, hit.Damage, hit.Knockback, base.Projectile.owner);
			}
		}
	}
}
