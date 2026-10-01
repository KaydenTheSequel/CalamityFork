using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AncientStar : ModProjectile, ILocalizedModType, IModType
{
	private const float MaxTime = 120f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public float Timer => 120f - (float)base.Projectile.timeLeft;

	public ref float Shine => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		if (Timer == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Vector2.Zero, Color.White, Color.Cyan, Main.rand.NextFloat(2.3f, 2.6f), 20, 0.1f, 1.5f, 0.02f));
			for (int i = 0; i < 4; i++)
			{
				Vector2 particleSpeed = base.Projectile.velocity.RotatedByRandom(0.6283185482025146) * 0.1f;
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, particleSpeed, Main.rand.NextFloat(0.8f, 1.4f), Color.Cyan, 60, 0.3f, 1.5f, 3f, 0.02f));
			}
		}
		if (Timer / 120f > 0.25f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		int dustType = Main.rand.Next(3);
		Dust.NewDust(base.Projectile.Center, 14, 14, dustType switch
		{
			1 => 57, 
			0 => 15, 
			_ => 58, 
		}, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 150, default(Color), 1.3f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
		if (Main.rand.NextBool(48) && !Main.dedServ)
		{
			int starGore = Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.Center, new Vector2(base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f), 16);
			Gore obj = Main.gore[starGore];
			obj.velocity *= 0.66f;
			Gore obj2 = Main.gore[starGore];
			obj2.velocity += base.Projectile.velocity * 0.3f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 2f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 256f, 12f, 20f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		if (Shine == 1f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D starTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
			Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			float properBloomSize = (float)starTexture.Height / (float)bloomTexture.Height;
			Color color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.85f);
			float rotation = Main.GlobalTimeWrappedHourly * 8f;
			Vector2 sparkCenter = base.Projectile.Center - Main.screenPosition;
			Main.EntitySpriteDraw(bloomTexture, sparkCenter, null, color * 0.5f, 0f, bloomTexture.Size() / 2f, 2f * properBloomSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(starTexture, sparkCenter, null, color * 0.5f, rotation + (float)Math.PI / 4f, starTexture.Size() / 2f, 0.75f, (SpriteEffects)0);
			Main.EntitySpriteDraw(starTexture, sparkCenter, null, Color.White, rotation, starTexture.Size() / 2f, 1f, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.DD2_WitherBeastDeath with
		{
			Volume = SoundID.DD2_WitherBeastDeath.Volume * 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 5; i++)
		{
			Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(5.2f, 4.3f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, particleSpeed, Color.White, Color.Cyan, Main.rand.NextFloat(1.3f, 1.6f), 40, 0.1f, 3.5f, 0.02f));
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < 3; j++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
			}
		}
	}
}
