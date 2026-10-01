using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NebulaElementalBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public const float UniversalAngularSpeed = 0.007853982f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override float MaxScale => 1.4f;

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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		base.RotationalSpeed = 0.007853982f;
		if (!Main.dedServ && base.Time == 5f)
		{
			int totalBubbles = 24;
			for (int i = 0; i < totalBubbles; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 242);
				dust.velocity = Main.rand.NextVector2Circular(6f, 6f);
				dust.scale = Main.rand.NextFloat(2f, 3f);
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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		DrawBeamWithColor(new Color(190, 29, 209), base.Projectile.scale);
		DrawBeamWithColor(Color.Lerp(new Color(254, 126, 229), Color.Transparent, 0.35f), base.Projectile.scale * 0.6f);
		DrawBeamWithColor(Color.Lerp(new Color(254, 190, 243), Color.Transparent, 0.35f), base.Projectile.scale * 0.6f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
	}
}
