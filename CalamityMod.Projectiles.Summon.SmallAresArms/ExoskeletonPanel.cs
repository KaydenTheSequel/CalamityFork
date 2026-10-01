using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class ExoskeletonPanel : ModProjectile, ILocalizedModType, IModType
{
	public enum IconType
	{
		Inactive,
		Plasma,
		Tesla,
		Laser,
		Gauss
	}

	public class IconState
	{
		public int PanelFlashTimer;

		public int MousePressFrameCountdown;

		public bool BeingHoveredOver;

		public bool PlacedInPanel;

		public IconType CurrentState;

		public Texture2D IconTexture
		{
			get
			{
				Texture2D plasmaTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPanelPlasma", (AssetRequestMode)2).Value;
				Texture2D teslaTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPanelTesla", (AssetRequestMode)2).Value;
				Texture2D laserTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPanelLaser", (AssetRequestMode)2).Value;
				Texture2D gaussTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPanelGauss", (AssetRequestMode)2).Value;
				return (Texture2D)(CurrentState switch
				{
					IconType.Plasma => plasmaTexture, 
					IconType.Tesla => teslaTexture, 
					IconType.Laser => laserTexture, 
					IconType.Gauss => gaussTexture, 
					_ => null, 
				});
			}
		}

		public Rectangle Frame
		{
			get
			{
				//IL_0069: Unknown result type (might be due to invalid IL or missing references)
				//IL_005b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Unknown result type (might be due to invalid IL or missing references)
				int frame = 0;
				if (BeingHoveredOver)
				{
					frame = ((!PlacedInPanel) ? 1 : 3);
				}
				if (MousePressFrameCountdown >= 1)
				{
					frame = 2;
				}
				if (PanelFlashTimer >= 1)
				{
					frame = (int)Math.Round(MathHelper.Lerp(4f, 6f, (float)PanelFlashTimer / 16f));
				}
				return (Rectangle)(((_003F?)IconTexture?.Frame(1, 7, 0, frame)) ?? default(Rectangle));
			}
		}

		public IconState(bool hover, bool panel, IconType state)
		{
			BeingHoveredOver = hover;
			PlacedInPanel = panel;
			CurrentState = state;
		}

		public void Update()
		{
			if (PanelFlashTimer >= 1)
			{
				PanelFlashTimer++;
			}
			if (PanelFlashTimer >= 16)
			{
				PanelFlashTimer = 0;
			}
			if (MousePressFrameCountdown > 0)
			{
				MousePressFrameCountdown--;
			}
		}
	}

	public IconType ClickedIcon;

	public IconState[] SelectionIcons = new IconState[4]
	{
		new IconState(hover: false, panel: false, IconType.Plasma),
		new IconState(hover: false, panel: false, IconType.Tesla),
		new IconState(hover: false, panel: false, IconType.Laser),
		new IconState(hover: false, panel: false, IconType.Gauss)
	};

	public IconState[] PanelIcons = new IconState[4]
	{
		new IconState(hover: false, panel: true, IconType.Inactive),
		new IconState(hover: false, panel: true, IconType.Inactive),
		new IconState(hover: false, panel: true, IconType.Inactive),
		new IconState(hover: false, panel: true, IconType.Inactive)
	};

	public bool ShouldDeleteArmIndex;

	public int ArmIDToSpawn = -1;

	public int ArmIndex = -1;

	public Vector2 PlayerOffset;

	public new string LocalizationCategory => "Projectiles.Summon";

	public bool FadeOut => base.Projectile.ai[0] == 1f;

	public ref float Time => ref base.Projectile.ai[1];

	public static Rectangle MouseRectangle
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 2, 2);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 9999999;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		int plasmaCannonID = ModContent.ProjectileType<ExoskeletonPlasmaCannon>();
		int teslaCannonID = ModContent.ProjectileType<ExoskeletonTeslaCannon>();
		int laserCannonID = ModContent.ProjectileType<ExoskeletonLaserCannon>();
		int gaussNukeID = ModContent.ProjectileType<ExoskeletonGaussNukeCannon>();
		int[] arms = new int[4] { plasmaCannonID, teslaCannonID, laserCannonID, gaussNukeID };
		if (PlayerOffset == Vector2.Zero)
		{
			PlayerOffset = Main.MouseWorld - Main.LocalPlayer.Center;
		}
		for (int i = 0; i < 4; i++)
		{
			PanelIcons[i].CurrentState = IconType.Inactive;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (arms.Contains(p.type) && p.owner == base.Projectile.owner)
			{
				IconType stateFromID = IconType.Inactive;
				if (p.type == plasmaCannonID)
				{
					stateFromID = IconType.Plasma;
				}
				if (p.type == teslaCannonID)
				{
					stateFromID = IconType.Tesla;
				}
				if (p.type == laserCannonID)
				{
					stateFromID = IconType.Laser;
				}
				if (p.type == gaussNukeID)
				{
					stateFromID = IconType.Gauss;
				}
				PanelIcons[(int)p.ai[0]].CurrentState = stateFromID;
			}
		}
		if (Time >= 95f)
		{
			base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity - (float)FadeOut.ToDirectionInt() * 0.0225f, 0f, 1f);
		}
		if (FadeOut && base.Projectile.Opacity <= 0f)
		{
			base.Projectile.Kill();
		}
		if (Main.player[base.Projectile.owner].dead || !Main.player[base.Projectile.owner].active)
		{
			base.Projectile.Kill();
		}
		Time++;
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		if (ArmIDToSpawn >= 0)
		{
			bool armAlreadyExists = false;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p2 = enumerator2.Current;
				if (arms.Contains(p2.type) && p2.owner == base.Projectile.owner && p2.ai[0] == (float)ArmIndex)
				{
					armAlreadyExists = true;
					break;
				}
			}
			if (!armAlreadyExists)
			{
				SoundEngine.PlaySound(in SoundID.Zombie66, base.Projectile.Center);
				int cannon = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, Vector2.Zero, ArmIDToSpawn, base.Projectile.damage, 0f, base.Projectile.owner, ArmIndex);
				if (Main.projectile.IndexInRange(cannon))
				{
					Main.projectile[cannon].originalDamage = base.Projectile.originalDamage;
				}
			}
		}
		if (ShouldDeleteArmIndex)
		{
			SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
			ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Projectile p3 = enumerator3.Current;
				if (arms.Contains(p3.type) && p3.owner == base.Projectile.owner && p3.ai[0] == (float)ArmIndex)
				{
					p3.Kill();
				}
			}
		}
		ShouldDeleteArmIndex = false;
		ArmIDToSpawn = -1;
		ArmIndex = -1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return false;
		}
		Texture2D panelTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPanelPlasma", (AssetRequestMode)2).Value;
		Texture2D arrowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/Arrow", (AssetRequestMode)2).Value;
		Vector2 area = value.Frame(1, 7).Size();
		Vector2 drawPosition = (Main.LocalPlayer.Center + PlayerOffset - Main.screenPosition).Floor();
		Rectangle[] selectionIconAreas = (Rectangle[])(object)new Rectangle[4]
		{
			Utils.CenteredRectangle(drawPosition + new Vector2(-62f, -70f) * base.Projectile.scale, area * base.Projectile.scale),
			Utils.CenteredRectangle(drawPosition + new Vector2(-22f, -70f) * base.Projectile.scale, area * base.Projectile.scale),
			Utils.CenteredRectangle(drawPosition + new Vector2(22f, -70f) * base.Projectile.scale, area * base.Projectile.scale),
			Utils.CenteredRectangle(drawPosition + new Vector2(62f, -70f) * base.Projectile.scale, area * base.Projectile.scale)
		};
		bool hoveringOverAnySlot = false;
		bool clickedAnIconOnPanel = false;
		bool sufficientSlots = (float)Main.LocalPlayer.maxMinions > 3f;
		for (int i = 0; i < 4; i++)
		{
			Texture2D selectionIconTexture = SelectionIcons[i].IconTexture;
			SelectionIcons[i].BeingHoveredOver = false;
			PanelIcons[i].BeingHoveredOver = false;
			if (((Rectangle)(ref selectionIconAreas[i])).Intersects(MouseRectangle))
			{
				hoveringOverAnySlot = true;
				SelectionIcons[i].BeingHoveredOver = true;
				if (Main.mouseLeft && Main.mouseLeftRelease && base.Projectile.Opacity >= 1f)
				{
					ClickedIcon = SelectionIcons[i].CurrentState;
					SelectionIcons[i].MousePressFrameCountdown = 15;
				}
			}
			Rectangle panelArea = selectionIconAreas[i];
			panelArea.Y += (int)(base.Projectile.scale * 78f);
			if (((Rectangle)(ref panelArea)).Intersects(MouseRectangle))
			{
				hoveringOverAnySlot = true;
				PanelIcons[i].BeingHoveredOver = true;
				if ((Main.mouseLeft && Main.mouseLeftRelease && base.Projectile.Opacity >= 1f) & sufficientSlots)
				{
					clickedAnIconOnPanel = true;
					PanelIcons[i].CurrentState = ClickedIcon;
					PanelIcons[i].PanelFlashTimer = 1;
					switch (ClickedIcon)
					{
					case IconType.Plasma:
						ArmIDToSpawn = ModContent.ProjectileType<ExoskeletonPlasmaCannon>();
						break;
					case IconType.Tesla:
						ArmIDToSpawn = ModContent.ProjectileType<ExoskeletonTeslaCannon>();
						break;
					case IconType.Laser:
						ArmIDToSpawn = ModContent.ProjectileType<ExoskeletonLaserCannon>();
						break;
					case IconType.Gauss:
						ArmIDToSpawn = ModContent.ProjectileType<ExoskeletonGaussNukeCannon>();
						break;
					case IconType.Inactive:
						ShouldDeleteArmIndex = true;
						PanelIcons[i].PanelFlashTimer = 0;
						break;
					}
					ArmIndex = i;
				}
			}
			Main.EntitySpriteDraw(selectionIconTexture, ((Rectangle)(ref selectionIconAreas[i])).Center.ToVector2(), SelectionIcons[i].Frame, base.Projectile.GetAlpha(Color.White), 0f, area * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(panelTexture, drawPosition, null, base.Projectile.GetAlpha(Color.White), 0f, panelTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		for (int j = 0; j < 4; j++)
		{
			Rectangle panelArea2 = selectionIconAreas[j];
			panelArea2.Y += (int)(base.Projectile.scale * 78f);
			Texture2D panelIconTexture = PanelIcons[j].IconTexture;
			if (panelIconTexture != null)
			{
				Main.EntitySpriteDraw(panelIconTexture, ((Rectangle)(ref panelArea2)).Center.ToVector2(), PanelIcons[j].Frame, base.Projectile.GetAlpha(Color.White), 0f, area * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
			SelectionIcons[j].Update();
			PanelIcons[j].Update();
		}
		if (ClickedIcon != IconType.Inactive)
		{
			Vector2 arrowDrawPosition = ((Rectangle)(ref selectionIconAreas[(int)(ClickedIcon - 1)])).Center.ToVector2();
			Vector2 arrowDirection = (Main.MouseScreen - arrowDrawPosition).SafeNormalize(Vector2.UnitY);
			arrowDrawPosition += arrowDirection * base.Projectile.scale * 24f;
			Main.EntitySpriteDraw(arrowTexture, arrowDrawPosition, null, base.Projectile.GetAlpha(Color.White), arrowDirection.ToRotation(), arrowTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		if (hoveringOverAnySlot && !sufficientSlots)
		{
			Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, this.GetLocalizedValue("NoSlots"), Main.MouseScreen.X + 20f, Main.MouseScreen.Y + 12f, Color.Cyan, Color.Black, new Vector2(0f, 0.5f));
		}
		if (!hoveringOverAnySlot && Main.mouseLeft && Main.mouseLeftRelease)
		{
			ClickedIcon = IconType.Inactive;
		}
		if (hoveringOverAnySlot)
		{
			Main.blockMouse = true;
			Main.LocalPlayer.mouseInterface = true;
		}
		if (clickedAnIconOnPanel)
		{
			ClickedIcon = IconType.Inactive;
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
