using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class DeathhailBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	private Color startingColor;

	private Color secondColor;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override float MaxScale => 0.7f;

	public override float MaxLaserLength => 1600f;

	public override float Lifetime => 20f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.ColorSwap(startingColor, secondColor, 0.9f);
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return LaserOverlayColor;
		}
	}

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override bool PreAI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f && Main.rand.NextBool())
		{
			secondColor = new Color(119, 210, 255);
			startingColor = new Color(247, 119, 255);
		}
		return true;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public DeathhailBeam()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		startingColor = new Color(119, 210, 255);
		secondColor = new Color(247, 119, 255);
		base._002Ector();
	}
}
