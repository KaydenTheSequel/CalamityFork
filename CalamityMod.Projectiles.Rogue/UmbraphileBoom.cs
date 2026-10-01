using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class UmbraphileBoom : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 35;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 58);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 35;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5;
		if (base.Projectile.frameCounter > 35)
		{
			base.Projectile.Kill();
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}
}
