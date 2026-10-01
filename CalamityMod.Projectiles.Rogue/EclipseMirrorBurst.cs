using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EclipseMirrorBurst : ModProjectile, ILocalizedModType, IModType
{
	private int frameCounter;

	private int frameX;

	private int frameY;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 752;
		base.Projectile.height = 752;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 150;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		frameCounter++;
		if (frameCounter > 3)
		{
			frameCounter = 0;
			frameY++;
			if (frameY > 1)
			{
				frameX++;
				frameY = 0;
			}
		}
		if (frameX > 0 && frameY > 0)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(frameX * 752, frameY * 752, 752, 752), Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
