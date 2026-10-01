using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CragmawVibeCheckChain : ModProjectile, ILocalizedModType, IModType
{
	public bool ReelingPlayer;

	public const int Lifetime = 360;

	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(ReelingPlayer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		ReelingPlayer = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offsetDrawVector = default(Vector2);
		((Vector2)(ref offsetDrawVector))._002Ector(0f, 30f);
		base.Projectile.alpha -= 15;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		int toTarget = (int)base.Projectile.ai[1];
		if (!Main.npc[(int)base.Projectile.ai[0]].active)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 drawPosition = (ReelingPlayer ? Main.player[toTarget].Center : base.Projectile.Center);
		base.Projectile.rotation = (Main.npc[(int)base.Projectile.ai[0]].Top - drawPosition + offsetDrawVector).ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 10f + base.Projectile.SafeDirectionTo(Main.player[toTarget].Center) * 9f) / 11f;
			if ((float)base.Projectile.timeLeft <= 180f)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.netUpdate = true;
			}
			if (base.Projectile.WithinRange(Main.player[toTarget].Center, 16f))
			{
				if (Main.zenithWorld)
				{
					CombatText.NewText(Main.player[toTarget].getRect(), Color.Red, CalamityUtils.GetTextValue("Misc.CragmawVibeCheck"), dramatic: true);
				}
				base.Projectile.localAI[0] = 1f;
				ReelingPlayer = true;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			if (ReelingPlayer)
			{
				Main.player[toTarget].velocity = Vector2.Lerp(Main.player[toTarget].velocity, base.Projectile.velocity, 0.024f);
			}
			if (Main.player[toTarget].dead)
			{
				ReelingPlayer = false;
			}
			if (!Main.npc[(int)base.Projectile.ai[0]].WithinRange(Main.player[toTarget].Center, 6f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * 16f + Main.player[toTarget].SafeDirectionTo(Main.npc[(int)base.Projectile.ai[0]].Center) * 19f) / 17f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D chainTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Enemy/CragmawVibeCheckMid", (AssetRequestMode)2).Value;
		Vector2 drawPosition = (ReelingPlayer ? Main.player[(int)base.Projectile.ai[1]].Center : base.Projectile.Center);
		Vector2 distanceVectorToStart = Main.npc[(int)base.Projectile.ai[0]].Top + Vector2.UnitY * 30f - drawPosition;
		float distanceToStart = ((Vector2)(ref distanceVectorToStart)).Length();
		Vector2 directionToStart = Vector2.Normalize(distanceVectorToStart);
		Rectangle frameRectangle = value.Frame();
		frameRectangle.Height /= 4;
		frameRectangle.Y += base.Projectile.frame * frameRectangle.Height;
		Main.EntitySpriteDraw(value, drawPosition - Main.screenPosition, frameRectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frameRectangle.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		distanceToStart -= (float)(frameRectangle.Height / 2 + chainTexture.Height) * base.Projectile.scale;
		Vector2 chainDrawPosition = drawPosition;
		chainDrawPosition += directionToStart * base.Projectile.scale * (float)frameRectangle.Height / 2f;
		if (distanceToStart > 0f)
		{
			float distanceMoved = 0f;
			Rectangle chainTextureFrameRectangle = default(Rectangle);
			((Rectangle)(ref chainTextureFrameRectangle))._002Ector(0, 0, chainTexture.Width, chainTexture.Height);
			while (distanceMoved + 1f < distanceToStart)
			{
				if (distanceToStart - distanceMoved < (float)chainTextureFrameRectangle.Height)
				{
					chainTextureFrameRectangle.Height = (int)(distanceToStart - distanceMoved);
				}
				Point chainPositionTileCoords = chainDrawPosition.ToTileCoordinates();
				Color colorAtChainPosition = Lighting.GetColor(chainPositionTileCoords.X, chainPositionTileCoords.Y);
				colorAtChainPosition = Color.Lerp(colorAtChainPosition, Color.White, 0.3f);
				Main.EntitySpriteDraw(chainTexture, chainDrawPosition - Main.screenPosition, chainTextureFrameRectangle, base.Projectile.GetAlpha(colorAtChainPosition), base.Projectile.rotation, chainTextureFrameRectangle.Bottom(), base.Projectile.scale, (SpriteEffects)0);
				distanceMoved += (float)chainTextureFrameRectangle.Height * base.Projectile.scale;
				chainDrawPosition += directionToStart * (float)chainTextureFrameRectangle.Height * base.Projectile.scale;
			}
		}
		return false;
	}
}
