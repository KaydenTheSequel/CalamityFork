using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ClimaxProj : ModProjectile, ILocalizedModType, IModType
{
	public bool firedBeam;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 48;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 48)
		{
			base.Projectile.frame = Main.rand.Next(0, 5);
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.86f;
		float spinTheta = 0.11f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = (Main.rand.NextBool() ? (0f - spinTheta) : spinTheta);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 4)
			{
				base.Projectile.frame = 0;
			}
		}
		NPC target = base.Projectile.Center.ClosestNPCAt(800f);
		base.Projectile.ai[0]--;
		if (base.Projectile.ai[0] < 0f && !firedBeam && target != null)
		{
			firedBeam = true;
			CalamityUtils.MagnetSphereHitscan(base.Projectile, Vector2.Distance(base.Projectile.Center, target.Center), 8f, 0f, 5, ModContent.ProjectileType<ClimaxBeam>());
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 30)
		{
			float alphaTimer = (float)base.Projectile.timeLeft / 30f;
			base.Projectile.alpha = (int)(255f - 255f * alphaTimer);
		}
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
