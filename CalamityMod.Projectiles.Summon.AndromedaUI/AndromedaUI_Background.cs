using System.IO;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.AndromedaUI;

public class AndromedaUI_Background : ModProjectile, ILocalizedModType, IModType
{
	public int FadeoutTime;

	public Vector2 PlayerOffset;

	public static readonly int FadeoutTimeMax;

	public static readonly Vector2 LeftBracketOffset;

	public static readonly Vector2 RightBracketOffset;

	public static readonly Vector2 TopBracketOffset;

	public new string LocalizationCategory => "Projectiles.Summon";

	public GiantIbanRobotOfDoom AttachedRobot => Main.projectile[(int)base.Projectile.localAI[0]].ModProjectile<GiantIbanRobotOfDoom>();

	public bool LeftBracketActive
	{
		get
		{
			return AttachedRobot.LeftBracketActive;
		}
		set
		{
			AttachedRobot.LeftBracketActive = value;
		}
	}

	public bool RightBracketActive
	{
		get
		{
			return AttachedRobot.RightBracketActive;
		}
		set
		{
			AttachedRobot.RightBracketActive = value;
		}
	}

	public bool BottomBracketActive
	{
		get
		{
			return AttachedRobot.BottomBracketActive;
		}
		set
		{
			AttachedRobot.BottomBracketActive = value;
		}
	}

	public bool LeftIconActive
	{
		get
		{
			return AttachedRobot.LeftIconActive;
		}
		set
		{
			AttachedRobot.LeftIconActive = value;
		}
	}

	public int RightIconCooldown
	{
		get
		{
			return AttachedRobot.RightIconCooldown;
		}
		set
		{
			AttachedRobot.RightIconCooldown = value;
		}
	}

	public bool TopIconActive
	{
		get
		{
			return AttachedRobot.TopIconActive;
		}
		set
		{
			AttachedRobot.TopIconActive = value;
		}
	}

	public static Rectangle MouseRectangle
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 2, 2);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 58;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(FadeoutTime);
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		FadeoutTime = reader.ReadInt32();
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		if (FadeoutTime > 0)
		{
			base.Projectile.alpha = (int)MathHelper.Lerp(0f, 255f, 1f - (float)FadeoutTime / (float)FadeoutTimeMax);
			FadeoutTime--;
		}
		else if (FadeoutTime == 0)
		{
			base.Projectile.Kill();
		}
		if (Main.projectile[(int)base.Projectile.localAI[0]].type != ModContent.ProjectileType<GiantIbanRobotOfDoom>() || !Main.projectile[(int)base.Projectile.localAI[0]].active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] < 40f)
		{
			base.Projectile.alpha = (int)MathHelper.Lerp(255f, 0f, base.Projectile.localAI[1] / 40f);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (PlayerOffset == Vector2.Zero)
			{
				PlayerOffset = Main.player[base.Projectile.owner].Center - base.Projectile.Center;
			}
			base.Projectile.Center = Main.player[base.Projectile.owner].Center - PlayerOffset;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Matrix perspective = Main.GameViewMatrix.ZoomMatrix;
			if (Main.LocalPlayer.gravDir == -1f)
			{
				Matrix val = perspective;
				Matrix val2 = Matrix.CreateScale(1f, -1f, 1f);
				Viewport viewport = ((Game)Main.instance).GraphicsDevice.Viewport;
				perspective = val * Matrix.Invert(val2 * Matrix.CreateTranslation(0f, (float)((Viewport)(ref viewport)).Height, 0f));
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, Main.Rasterizer, (Effect)null, perspective);
			Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White * base.Projectile.Opacity, 0f, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
			DrawBrackets(Main.spriteBatch);
			DrawIcons(Main.spriteBatch);
			Main.spriteBatch.ExitShaderRegion();
			if (!Main.blockMouse && Main.mouseLeft && FadeoutTime == -1)
			{
				FadeoutTime = FadeoutTimeMax;
			}
		}
		return false;
	}

	public void DrawBrackets(SpriteBatch spriteBatch)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		Texture2D leftBracketTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/LeftBracket" + (LeftBracketActive ? "Lit" : ""), (AssetRequestMode)2).Value;
		Texture2D leftBracketTextureHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/LeftBracketHovered", (AssetRequestMode)2).Value;
		Texture2D rightBracketTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/RightBracket" + (RightBracketActive ? "Lit" : ""), (AssetRequestMode)2).Value;
		Texture2D rightBracketTextureHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/RightBracketHovered", (AssetRequestMode)2).Value;
		Texture2D topBracketTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/TopBracket" + (BottomBracketActive ? "Lit" : ""), (AssetRequestMode)2).Value;
		Texture2D topBracketTextureHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/TopBracketHovered", (AssetRequestMode)2).Value;
		Vector2 topBracketPosition = base.Projectile.Bottom + TopBracketOffset;
		Rectangle topBracketFrame = default(Rectangle);
		((Rectangle)(ref topBracketFrame))._002Ector((int)topBracketPosition.X - topBracketTexture.Width / 2, (int)topBracketPosition.Y - 24, topBracketTexture.Width - 18, topBracketTexture.Height - 12);
		Rectangle mouseRectangle = MouseRectangle;
		bool topBracketSelect = ((Rectangle)(ref mouseRectangle)).Intersects(topBracketFrame) && !BottomBracketActive;
		Main.EntitySpriteDraw(topBracketSelect ? topBracketTextureHovered : topBracketTexture, topBracketPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Vector2 leftBracketPosition = base.Projectile.Center + LeftBracketOffset;
		Rectangle leftBracketFrame = default(Rectangle);
		((Rectangle)(ref leftBracketFrame))._002Ector((int)leftBracketPosition.X - leftBracketTexture.Width / 2, (int)leftBracketPosition.Y - 24, leftBracketTexture.Width - 12, leftBracketTexture.Height - 18);
		mouseRectangle = MouseRectangle;
		bool leftBracketSelect = ((Rectangle)(ref mouseRectangle)).Intersects(leftBracketFrame) && !LeftBracketActive;
		Main.EntitySpriteDraw(leftBracketSelect ? leftBracketTextureHovered : leftBracketTexture, leftBracketPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Vector2 rightBracketPosition = base.Projectile.Center + RightBracketOffset + (float)(base.Projectile.width / 2 - 1) * Vector2.UnitX;
		Rectangle rightBracketFrame = default(Rectangle);
		((Rectangle)(ref rightBracketFrame))._002Ector((int)rightBracketPosition.X - rightBracketTexture.Width / 2, (int)rightBracketPosition.Y - 24, rightBracketTexture.Width - 12, rightBracketTexture.Height - 18);
		mouseRectangle = MouseRectangle;
		bool rightBracketSelect = ((Rectangle)(ref mouseRectangle)).Intersects(rightBracketFrame) && !RightBracketActive;
		Main.EntitySpriteDraw(rightBracketSelect ? rightBracketTextureHovered : rightBracketTexture, rightBracketPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		if (!(leftBracketSelect | rightBracketSelect | topBracketSelect))
		{
			return;
		}
		Main.blockMouse = true;
		if (Main.mouseLeft && Main.projectile[(int)base.Projectile.localAI[0]].ai[0] <= 0f)
		{
			if (leftBracketSelect)
			{
				LeftBracketActive = true;
				bool rightBracketActive = (BottomBracketActive = false);
				RightBracketActive = rightBracketActive;
			}
			if (rightBracketSelect && FlamsteedRing.SpaceForLargeMech(Main.LocalPlayer))
			{
				RightBracketActive = true;
				bool rightBracketActive = (BottomBracketActive = false);
				LeftBracketActive = rightBracketActive;
				LeftIconActive = false;
			}
			if (topBracketSelect)
			{
				BottomBracketActive = true;
				bool rightBracketActive = (RightBracketActive = false);
				LeftBracketActive = rightBracketActive;
				TopIconActive = false;
			}
			Main.projectile[(int)base.Projectile.localAI[0]].ai[0] = 30f;
		}
	}

	public void DrawIcons(SpriteBatch spriteBatch)
	{
		DrawLeftIcon(spriteBatch);
		DrawRightIcon(spriteBatch);
		DrawTopIcon(spriteBatch);
	}

	public void DrawLeftIcon(SpriteBatch spriteBatch)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		Texture2D smallIndicator = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/SmallIcon", (AssetRequestMode)2).Value;
		Texture2D smallIndicatorLocked = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/Lock", (AssetRequestMode)2).Value;
		Texture2D textureToDraw = smallIndicator;
		Vector2 iconOffset = default(Vector2);
		((Vector2)(ref iconOffset))._002Ector(-14f, 11f);
		Vector2 drawPosition = base.Projectile.Center + iconOffset;
		if ((!LeftBracketActive && !BottomBracketActive) || RightBracketActive)
		{
			textureToDraw = smallIndicatorLocked;
			drawPosition = base.Projectile.Center + iconOffset;
		}
		bool wasActive = LeftIconActive;
		LeftIconActive = textureToDraw == smallIndicator;
		if (LeftIconActive != wasActive && !Main.dedServ)
		{
			Player player = Main.player[base.Projectile.owner];
			for (int i = 0; i < 45; i++)
			{
				Dust dust = Dust.NewDustPerfect(player.Center + Main.rand.NextVector2Circular(60f, 90f), 26);
				dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.35f);
			}
			for (int j = 0; j < 4; j++)
			{
				Utils.PoofOfSmoke(player.Center + Main.rand.NextVector2Circular(20f, 30f));
			}
		}
		Main.EntitySpriteDraw(textureToDraw, drawPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, textureToDraw.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}

	public void DrawRightIcon(SpriteBatch spriteBatch)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D thunderIndicator = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/ThunderIcon", (AssetRequestMode)2).Value;
		Texture2D thunderIndicatorHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/ThunderIconHovered", (AssetRequestMode)2).Value;
		Texture2D thunderIndicatorCharge = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/ThunderIconCharge", (AssetRequestMode)2).Value;
		Texture2D thunderIndicatorLocked = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/Lock", (AssetRequestMode)2).Value;
		Texture2D textureToDraw = thunderIndicatorLocked;
		Vector2 iconOffset = default(Vector2);
		((Vector2)(ref iconOffset))._002Ector(14f, 11f);
		if ((RightBracketActive || BottomBracketActive) && !LeftBracketActive)
		{
			textureToDraw = thunderIndicator;
		}
		Vector2 drawPosition = base.Projectile.Center + iconOffset;
		if (textureToDraw != thunderIndicatorLocked && RightIconCooldown <= 0)
		{
			Rectangle iconFrame = Utils.CenteredRectangle(drawPosition, textureToDraw.Size());
			textureToDraw = thunderIndicator;
			Rectangle mouseRectangle = MouseRectangle;
			if (((Rectangle)(ref mouseRectangle)).Intersects(iconFrame))
			{
				textureToDraw = thunderIndicatorHovered;
				Main.blockMouse = true;
				if (Main.mouseLeft && Main.projectile[(int)base.Projectile.localAI[0]].ai[0] <= 0f)
				{
					FadeoutTime = FadeoutTimeMax;
					RightIconCooldown = 960;
					Main.projectile[(int)base.Projectile.localAI[0]].ai[0] = 30f;
					if (!Main.dedServ)
					{
						for (int i = 0; i < 80; i++)
						{
							Dust dust = Dust.NewDustPerfect(Main.projectile[(int)base.Projectile.localAI[0]].Center, Utils.SelectRandom<int>(Main.rand, 226, 263));
							dust.velocity = Main.rand.NextVector2Circular(14f, 14f);
							dust.fadeIn = 1.1f;
							dust.noGravity = true;
						}
					}
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/MechGaussRifle"), base.Projectile.Center);
				}
			}
		}
		float chargeCompletionRatio = 1f - (float)RightIconCooldown / 960f;
		Main.EntitySpriteDraw(thunderIndicatorCharge, drawPosition + Vector2.UnitY - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, thunderIndicatorCharge.Width, (int)(chargeCompletionRatio * (float)thunderIndicatorCharge.Height)), Color.White * base.Projectile.Opacity, 0f, thunderIndicatorCharge.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
		Main.EntitySpriteDraw(textureToDraw, drawPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, textureToDraw.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0, 1f);
	}

	public void DrawTopIcon(SpriteBatch spriteBatch)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D meleeIndicator = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/MeleeIcon", (AssetRequestMode)2).Value;
		Texture2D rangedIndicator = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/RangedIcon", (AssetRequestMode)2).Value;
		Texture2D attackIndicatorLocked = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/Lock", (AssetRequestMode)2).Value;
		Texture2D meleeIndicatorHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/MeleeIconHovered", (AssetRequestMode)2).Value;
		Texture2D rangedIndicatorHovered = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaUI/RangedIconHovered", (AssetRequestMode)2).Value;
		Texture2D textureToDraw = (TopIconActive ? meleeIndicator : rangedIndicator);
		Vector2 iconOffset = default(Vector2);
		((Vector2)(ref iconOffset))._002Ector(0f, -12f);
		Vector2 drawPosition = base.Projectile.Center + iconOffset;
		bool eitherBracketActive = LeftBracketActive || RightBracketActive;
		if (!eitherBracketActive || BottomBracketActive)
		{
			textureToDraw = attackIndicatorLocked;
			drawPosition = base.Projectile.Center + iconOffset;
		}
		else if (eitherBracketActive)
		{
			Rectangle iconFrame = Utils.CenteredRectangle(drawPosition, textureToDraw.Size());
			Rectangle mouseRectangle = MouseRectangle;
			if (((Rectangle)(ref mouseRectangle)).Intersects(iconFrame))
			{
				textureToDraw = (TopIconActive ? meleeIndicatorHovered : rangedIndicatorHovered);
				Main.blockMouse = true;
				if (Main.mouseLeft && Main.projectile[(int)base.Projectile.localAI[0]].ai[0] <= 0f)
				{
					TopIconActive = !TopIconActive;
					Main.projectile[(int)base.Projectile.localAI[0]].ai[0] = 30f;
				}
			}
		}
		Main.EntitySpriteDraw(textureToDraw, drawPosition - Main.screenPosition, null, Color.White * base.Projectile.Opacity, 0f, textureToDraw.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public AndromedaUI_Background()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		FadeoutTime = -1;
		PlayerOffset = Vector2.Zero;
		base._002Ector();
	}

	static AndromedaUI_Background()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		FadeoutTimeMax = 40;
		LeftBracketOffset = new Vector2(-8f, -6f);
		RightBracketOffset = new Vector2(8f, -6f);
		TopBracketOffset = new Vector2(2f, 27f);
	}
}
