using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseMassiveExplosionProjectile : ModProjectile
{
	public ref float CurrentRadius => ref base.Projectile.ai[0];

	public ref float MaxRadius => ref base.Projectile.ai[1];

	public virtual bool UsesScreenshake { get; }

	public abstract int Lifetime { get; }

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public virtual float GetScreenshakePower(float pulseCompletionRatio)
	{
		return 0f;
	}

	public virtual float Fadeout(float completion)
	{
		return (1f - (float)Math.Sqrt(completion)) * 0.7f;
	}

	public abstract Color GetCurrentExplosionColor(float pulseCompletionRatio);

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (UsesScreenshake)
		{
			float screenShakePower = GetScreenshakePower((float)base.Projectile.timeLeft / (float)Lifetime) * Utils.GetLerpValue(1300f, 0f, base.Projectile.Distance(Main.LocalPlayer.Center), clamped: true);
			Main.LocalPlayer.SetScreenshake(screenShakePower);
		}
		CurrentRadius = MathHelper.Lerp(CurrentRadius, MaxRadius, 0.25f);
		base.Projectile.scale = MathHelper.Lerp(1.2f, 5f, Utils.GetLerpValue(Lifetime, 0f, base.Projectile.timeLeft, clamped: true));
		base.Projectile.ExpandHitboxBy((int)(CurrentRadius * base.Projectile.scale), (int)(CurrentRadius * base.Projectile.scale));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
		float pulseCompletionRatio = Utils.GetLerpValue(Lifetime, 0f, base.Projectile.timeLeft, clamped: true);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(1.5f, 1f);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + base.Projectile.Size * scale * 0.5f;
		Rectangle drawArea = default(Rectangle);
		((Rectangle)(ref drawArea))._002Ector(0, 0, base.Projectile.width, base.Projectile.height);
		Color fadeoutColor = default(Color);
		((Color)(ref fadeoutColor))._002Ector(new Vector4(Fadeout(pulseCompletionRatio)) * base.Projectile.Opacity);
		DrawData drawData = new DrawData(ModContent.Request<Texture2D>("Terraria/Images/Misc/Perlin", (AssetRequestMode)2).Value, drawPosition, drawArea, fadeoutColor, base.Projectile.rotation, base.Projectile.Size, scale, (SpriteEffects)0);
		GameShaders.Misc["ForceField"].UseColor(GetCurrentExplosionColor(pulseCompletionRatio));
		GameShaders.Misc["ForceField"].Apply(drawData);
		drawData.Draw(Main.spriteBatch);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}
}
