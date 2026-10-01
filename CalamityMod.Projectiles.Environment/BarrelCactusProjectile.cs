using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class BarrelCactusProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.CloneDefaults(727);
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.damage = 70;
		base.Projectile.knockBack = 6f;
		base.Projectile.friendly = false;
		base.Projectile.hostile = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Projectile.type].Value;
		Rectangle frame = value.Bounds;
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: lightColor, rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
