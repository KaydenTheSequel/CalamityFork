using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class AndromedaMechLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0f)
		{
			return false;
		}
		return drawInfo.drawPlayer.Calamity().andromedaState != AndromedaPlayerState.Inactive;
	}

	public static void DrawTheStupidFuckingRobot(ref PlayerDrawSet drawInfo)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		drawInfo.hidesBottomSkin = true;
		drawInfo.hidesTopSkin = true;
		drawInfo.armorHidesArms = true;
		drawInfo.armorHidesHands = true;
		drawInfo.cShield = 0;
		drawInfo.hideCompositeShoulders = true;
		drawInfo.DrawDataCache.Clear();
		int robot = -1;
		int andromedaMechID = ModContent.ProjectileType<GiantIbanRobotOfDoom>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == andromedaMechID && p.owner == drawPlayer.whoAmI)
			{
				robot = p.whoAmI;
				break;
			}
		}
		if (robot == -1)
		{
			drawPlayer.Calamity().andromedaState = AndromedaPlayerState.Inactive;
			return;
		}
		SpriteEffects direction = (SpriteEffects)(Main.projectile[robot].spriteDirection == -1);
		if (drawPlayer.gravDir == -1f)
		{
			direction = (SpriteEffects)(direction | 2);
		}
		GiantIbanRobotOfDoom robotEntityInstance = (GiantIbanRobotOfDoom)Main.projectile[robot].ModProjectile;
		Rectangle frame = default(Rectangle);
		switch (drawPlayer.Calamity().andromedaState)
		{
		case AndromedaPlayerState.SpecialAttack:
		{
			Texture2D dashTexture = ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/AndromedaBolt", (AssetRequestMode)2).Value;
			frame = dashTexture.Frame(1, 4, 0, robotEntityInstance.RightIconCooldown / 4 % 4);
			DrawData drawData = new DrawData(dashTexture, drawPlayer.Center + new Vector2(0f, drawPlayer.gravDir * -8f) - Main.screenPosition, frame, Color.White, Main.projectile[robot].rotation, drawPlayer.Size / 2f, 1f, direction, 1f);
			drawData.shader = drawPlayer.cBody;
			drawInfo.DrawDataCache.Add(drawData);
			break;
		}
		case AndromedaPlayerState.LargeRobot:
		{
			Texture2D robotTexture = ModContent.Request<Texture2D>(robotEntityInstance.Texture, (AssetRequestMode)2).Value;
			((Rectangle)(ref frame))._002Ector(robotEntityInstance.FrameX * robotTexture.Width / 3, robotEntityInstance.FrameY * robotTexture.Height / 7, robotTexture.Width / 3, robotTexture.Height / 7);
			DrawData drawData = new DrawData(ModContent.Request<Texture2D>(Main.projectile[robot].ModProjectile.Texture, (AssetRequestMode)2).Value, Main.projectile[robot].Center + Vector2.UnitY * drawPlayer.gravDir * 6f - Main.screenPosition, frame, Color.White, Main.projectile[robot].rotation, Main.projectile[robot].Size / 2f, 1f, direction, 1f);
			drawData.shader = drawPlayer.cBody;
			drawInfo.DrawDataCache.Add(drawData);
			break;
		}
		case AndromedaPlayerState.SmallRobot:
		{
			Texture2D robotTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AndromedaSmall", (AssetRequestMode)2).Value;
			((Rectangle)(ref frame))._002Ector(0, robotEntityInstance.CurrentFrame * 54, robotTexture.Width, robotTexture.Height / 21);
			DrawData drawData = new DrawData(robotTexture, drawPlayer.Center + new Vector2((float)((drawPlayer.direction == 1) ? (-24) : (-10)), drawPlayer.gravDir * -8f) - Main.screenPosition, frame, Color.White, 0f, drawPlayer.Size / 2f, 1f, direction, 1f);
			drawData.shader = drawPlayer.cBody;
			drawInfo.DrawDataCache.Add(drawData);
			break;
		}
		}
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		DrawTheStupidFuckingRobot(ref drawInfo);
	}
}
