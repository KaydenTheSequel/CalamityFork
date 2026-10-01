using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class OnyxExcavator : ModMount
{
	public static List<int> OnyxExcavatorImmuneTiles;

	public override void SetStaticDefaults()
	{
		int num = 2;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = 26;
		num2++;
		span[num2] = 466;
		OnyxExcavatorImmuneTiles = list;
		base.MountData.spawnDust = 145;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<OnyxExcavatorBuff>();
		base.MountData.runSpeed = 4.5f;
		base.MountData.swimSpeed = 0.5f;
		base.MountData.acceleration = 0.1f;
		base.MountData.jumpHeight = 5;
		base.MountData.jumpSpeed = 3f;
		base.MountData.blockExtraJumps = true;
		base.MountData.totalFrames = 6;
		base.MountData.heightBoost = 10;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			array[l] = 6;
		}
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 10;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 10;
		base.MountData.yOffset = -1;
		base.MountData.standingFrameCount = 1;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 6;
		base.MountData.runningFrameDelay = 12;
		base.MountData.runningFrameStart = base.MountData.standingFrameStart;
		base.MountData.inAirFrameCount = base.MountData.standingFrameCount;
		base.MountData.inAirFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.inAirFrameStart = base.MountData.standingFrameStart;
		base.MountData.idleFrameCount = base.MountData.standingFrameCount;
		base.MountData.idleFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.idleFrameStart = base.MountData.standingFrameStart;
		base.MountData.idleFrameLoop = false;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.inAirFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.frontTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/OnyxExcavatorExtra2", (AssetRequestMode)2);
			base.MountData.frontTextureExtra = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/OnyxExcavatorExtra", (AssetRequestMode)2);
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override void Unload()
	{
		OnyxExcavatorImmuneTiles = null;
	}

	public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		bool num = Math.Abs(velocity.X) > mountedPlayer.mount.RunSpeed / 2f;
		float direction = Math.Sign(mountedPlayer.velocity.X);
		Lighting.AddLight(mountedPlayer.Center, 0.5f, 0.5f, 0.4f);
		if (num && velocity.Y == 0f)
		{
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 95);
				dust.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat() * -2f);
				dust.noLight = true;
				dust.scale = 0.2f + Main.rand.NextFloat() * 0.8f;
				dust.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
			}
			if (mountedPlayer.cMount == 0)
			{
				mountedPlayer.position += new Vector2(direction * 24f, 0f);
				mountedPlayer.FloorVisuals(Falling: true);
				mountedPlayer.position -= new Vector2(direction * 24f, 0f);
			}
		}
		return true;
	}

	private static bool OnyxExcavateTile(Player player, Point targetPos, int pickPower)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int x = targetPos.X;
		int y = targetPos.Y;
		Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
		if (!tile.HasTile || player.noBuilding || Main.tileContainer[tile.TileType])
		{
			return false;
		}
		if (OnyxExcavatorImmuneTiles.Contains(tile.TileType))
		{
			return false;
		}
		player.PickTile(x, y, pickPower);
		return true;
	}

	public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
	{
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != player.whoAmI || !Main.mouseLeft || player.mouseInterface || Main.blockMouse)
		{
			return;
		}
		bool canDrillHorizontally = player.controlLeft || player.controlRight;
		bool canDrillDown = player.controlDown;
		if (!canDrillHorizontally && !canDrillDown)
		{
			return;
		}
		Item obj = player.GetBestPick() ?? ContentSamples.ItemsByType[3509];
		int pickPower = obj.pick;
		int digCadence = obj.useTime;
		if (player.Calamity().universalFrameTimer % (ulong)digCadence != 0L)
		{
			return;
		}
		Point[] drillTargets = null;
		Rectangle hitbox;
		if (canDrillHorizontally)
		{
			float xVel = player.velocity.X;
			float num = Math.Abs(xVel);
			int direction = ((num < 0.1f) ? player.direction : Math.Sign(xVel));
			int xTileOffset = ((!(num > 0.5f)) ? 1 : 2);
			int num2;
			if (direction != -1)
			{
				hitbox = player.Hitbox;
				num2 = ((Rectangle)(ref hitbox)).Right - 2;
			}
			else
			{
				hitbox = player.Hitbox;
				num2 = ((Rectangle)(ref hitbox)).Left + 2;
			}
			hitbox = player.Hitbox;
			int bottomTileCenterY = ((Rectangle)(ref hitbox)).Bottom - 8;
			int drillLeadingEdgeX = num2 + direction * 16 * xTileOffset;
			Vector2 drillOrigin = default(Vector2);
			((Vector2)(ref drillOrigin))._002Ector((float)drillLeadingEdgeX, (float)bottomTileCenterY);
			Vector2 drillOneUp = drillOrigin + new Vector2(0f, -16f);
			Vector2 drillTwoUp = drillOrigin + new Vector2(0f, -32f);
			Vector2 drillThreeUp = drillOrigin + new Vector2(0f, -48f);
			Vector2 drillFrontLower = drillOrigin + new Vector2(16f * (float)direction, -16f);
			Vector2 drillFrontUpper = drillOrigin + new Vector2(16f * (float)direction, -32f);
			drillTargets = (Point[])(object)new Point[6]
			{
				drillOrigin.ToSafeTileCoordinates(),
				drillOneUp.ToSafeTileCoordinates(),
				drillTwoUp.ToSafeTileCoordinates(),
				drillThreeUp.ToSafeTileCoordinates(),
				drillFrontLower.ToSafeTileCoordinates(),
				drillFrontUpper.ToSafeTileCoordinates()
			};
		}
		else if (canDrillDown)
		{
			hitbox = player.Hitbox;
			int playerLeftEdgeX = ((Rectangle)(ref hitbox)).Left + 2;
			hitbox = player.Hitbox;
			int playerRightEdgeX = ((Rectangle)(ref hitbox)).Right - 2;
			hitbox = player.Hitbox;
			int drillLeadingEdgeY = ((Rectangle)(ref hitbox)).Bottom + 8;
			Vector2 drillLeftNear = default(Vector2);
			((Vector2)(ref drillLeftNear))._002Ector((float)playerLeftEdgeX, (float)drillLeadingEdgeY);
			Vector2 drillRightNear = default(Vector2);
			((Vector2)(ref drillRightNear))._002Ector((float)playerRightEdgeX, (float)drillLeadingEdgeY);
			Vector2 drillLeftFar = drillLeftNear + new Vector2(-16f, 0f);
			Vector2 drillRightFar = drillRightNear + new Vector2(16f, 0f);
			Vector2 mountLeftNear = drillLeftNear + new Vector2(0f, -16f);
			Vector2 mountRightNear = drillRightNear + new Vector2(0f, -16f);
			Vector2 mountLeftFar = drillLeftFar + new Vector2(0f, -16f);
			Vector2 mountRightFar = drillRightFar + new Vector2(0f, -16f);
			drillTargets = (Point[])(object)new Point[8]
			{
				mountLeftNear.ToSafeTileCoordinates(),
				mountRightNear.ToSafeTileCoordinates(),
				mountLeftFar.ToSafeTileCoordinates(),
				mountRightFar.ToSafeTileCoordinates(),
				drillLeftNear.ToSafeTileCoordinates(),
				drillRightNear.ToSafeTileCoordinates(),
				drillLeftFar.ToSafeTileCoordinates(),
				drillRightFar.ToSafeTileCoordinates()
			};
		}
		if (drillTargets != null)
		{
			Point[] array = drillTargets;
			foreach (Point p in array)
			{
				OnyxExcavateTile(player, p, pickPower);
			}
		}
	}
}
