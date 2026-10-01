using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NebulaNova : ModProjectile, ILocalizedModType, IModType
{
	private const int TotalXFrames = 2;

	private const int TotalYFrames = 7;

	private const int FrameTimer = 4;

	public int frameX;

	public int frameY;

	public new string LocalizationCategory => "Projectiles.Magic";

	public int CurrentFrame
	{
		get
		{
			return frameX * 7 + frameY;
		}
		set
		{
			frameX = value / 7;
			frameY = value % 7;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 190;
		base.Projectile.height = 168;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 56;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 4 == 0)
		{
			CurrentFrame++;
			if (frameX >= 2)
			{
				CurrentFrame = 0;
			}
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.95f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = value.Size() / new Vector2(2f, 7f) * 0.5f;
		Rectangle frame = value.Frame(2, 7, frameX, frameY);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection != 1), texture: value, position: position, sourceRectangle: frame, color: Color.White, rotation: base.Projectile.rotation, origin: origin, scale: base.Projectile.scale);
		return false;
	}
}
