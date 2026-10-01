using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AnahitaTelegraph : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 30;

	public static Asset<Texture2D> WaterSpearTex;

	public static Asset<Texture2D> FrostMistTex;

	public static Asset<Texture2D> TrebleClefTex;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float TeleType => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			WaterSpearTex = ModContent.Request<Texture2D>("CalamityMod/Particles/PointParticle", (AssetRequestMode)2);
			FrostMistTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
			TrebleClefTex = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 15);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 30;
		base.Projectile.alpha = 255;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.rotation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.rotation = reader.ReadSingle();
	}

	private Color DetermineColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		Color returnColor = Color.Black;
		float teleType = TeleType;
		if (teleType != 0f)
		{
			if (teleType != 1f)
			{
				if (teleType == 2f)
				{
					((Color)(ref returnColor))._002Ector(199, 90, 67);
				}
			}
			else
			{
				returnColor = Color.White * 0.9f;
			}
		}
		else
		{
			((Color)(ref returnColor))._002Ector(55, 70, 240);
		}
		return returnColor;
	}

	public override void AI()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		Player target = Main.player[(int)base.Projectile.ai[0]];
		float teleType;
		if (base.Projectile.timeLeft == 30)
		{
			teleType = TeleType;
			if (teleType != 0f)
			{
				if (teleType == 2f)
				{
					base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
				}
			}
			else
			{
				base.Projectile.rotation = (target.Center - base.Projectile.Center).ToRotation();
			}
		}
		if ((double)base.Projectile.timeLeft > Math.Ceiling(19.80000114440918))
		{
			base.Projectile.alpha -= 25;
		}
		if ((double)base.Projectile.timeLeft <= Math.Ceiling(9.90000057220459))
		{
			base.Projectile.alpha += 25;
		}
		Projectile projectile = base.Projectile;
		projectile.Center += target.velocity;
		if (base.Projectile.timeLeft % 3 == 0)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 43, 0f, 0f, 0, DetermineColor(), 0.75f);
		}
		teleType = TeleType;
		if (teleType != 0f)
		{
			if (teleType != 1f)
			{
				if (teleType == 2f)
				{
					base.Projectile.rotation += 0.165f;
				}
			}
			else
			{
				int accFactor = 30 - base.Projectile.timeLeft;
				Projectile projectile2 = base.Projectile;
				projectile2.Center += Vector2.Normalize(target.Center - base.Projectile.Center) * (float)accFactor * 0.73f;
			}
		}
		else
		{
			Projectile projectile3 = base.Projectile;
			projectile3.Center += Vector2.Normalize(target.Center - base.Projectile.Center) * 8.5f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		Color telegraphColor = DetermineColor();
		float teleType = TeleType;
		if (teleType != 0f)
		{
			if (teleType != 1f)
			{
				if (teleType == 2f)
				{
					Asset<Texture2D> tex = TrebleClefTex;
					Main.spriteBatch.SetBlendState(BlendState.Additive);
					Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, telegraphColor * base.Projectile.Opacity * 0.9f, base.Projectile.rotation, tex.Value.Size() / 2f, base.Projectile.scale * 2f, (SpriteEffects)0);
					Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, telegraphColor * base.Projectile.Opacity * 0.9f, 0f - base.Projectile.rotation, tex.Value.Size() / 2f, base.Projectile.scale * 2f, (SpriteEffects)0);
					Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
				}
			}
			else
			{
				Asset<Texture2D> tex = FrostMistTex;
				Main.spriteBatch.SetBlendState(BlendState.Additive);
				Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, telegraphColor * base.Projectile.Opacity * 0.75f, 0f, tex.Value.Size() / 2f, base.Projectile.scale * 0.7f, (SpriteEffects)0);
				Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
			}
		}
		else
		{
			Asset<Texture2D> tex = WaterSpearTex;
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, telegraphColor * base.Projectile.Opacity, base.Projectile.rotation + (float)Math.PI / 2f, tex.Value.Size() / 2f, new Vector2(0.75f, 1.75f), (SpriteEffects)0);
		}
		return false;
	}
}
