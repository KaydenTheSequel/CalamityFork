using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DevourerofCodsBobber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 61;
		base.Projectile.bobber = true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/DevourerofCodsGlow", (AssetRequestMode)2).Value;
		float xOffset = (float)(glowmask.Width - base.Projectile.width) * 0.5f + (float)base.Projectile.width * 0.5f;
		Vector2 drawPos = base.Projectile.position - Main.screenPosition;
		drawPos.X += xOffset;
		drawPos.Y += (float)base.Projectile.height / 2f + base.Projectile.gfxOffY;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, glowmask.Width, glowmask.Height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(xOffset, (float)base.Projectile.height / 2f);
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		if (base.Projectile.ai[0] <= 1f)
		{
			Main.spriteBatch.Draw(glowmask, drawPos, (Rectangle?)frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
		}
	}

	public override bool PreDrawExtras()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.35f, 0f, 0.25f);
		return true;
	}
}
