using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BlueFlamePillar : ModProjectile, ILocalizedModType, IModType
{
	public int frameX;

	public int frameY;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public int currentFrame => frameY + frameX * 6;

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 322;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 7 == 6)
		{
			frameY++;
			if (frameY >= 6)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX >= 3)
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.position.Y -= base.Projectile.height / 2;
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameX * 80, frameY * 322, 80, 322);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
		return false;
	}
}
