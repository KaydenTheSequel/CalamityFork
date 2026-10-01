using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class StealthNimbusCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Magic/ShadeNimbusCloud";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 25;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[0] == 1f)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/StealthNimbusCloud2", (AssetRequestMode)2).Value;
		}
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AI()
	{
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<StealthNimbus>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
