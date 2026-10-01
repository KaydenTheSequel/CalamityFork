using System;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PhaseslayerBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 180;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = 64;
		base.Projectile.height = 120;
		base.Projectile.scale = 0.5f;
		Projectile projectile = base.Projectile;
		projectile.Size *= base.Projectile.scale;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.localNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1000f);
		if (potentialTarget != null && base.Projectile.Distance(potentialTarget.Center) > 40f && base.Projectile.timeLeft > 120)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 7f + base.Projectile.SafeDirectionTo(potentialTarget.Center, -Vector2.UnitY) * 24f) / 8f;
		}
		base.Projectile.frameCounter++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Texture2D bladeTexture = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Rectangle frame = bladeTexture.Frame(1, 5, 0, base.Projectile.frameCounter / 4 % 5);
		DrawData drawData2 = new DrawData(bladeTexture, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, bladeTexture.Size() / new Vector2(1f, 5f) * 0.5f, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		CalamityShaders.LightDistortionShader.Value.CurrentTechnique.Passes[0].Apply();
		drawData2.Draw(Main.spriteBatch);
		return false;
	}

	public override void PostDraw(Color drawColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
	}
}
