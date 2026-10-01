using System.Collections.Generic;
using CalamityMod.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class DraedonGamerChairMount : ModMount
{
	public const float MovementSpeed = 12f;

	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 182;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<DraedonGamerChairBuff>();
		base.MountData.runSpeed = 12f;
		base.MountData.dashSpeed = 12f;
		base.MountData.swimSpeed = 12f;
		base.MountData.acceleration = 12f;
		base.MountData.fallDamage = 0f;
		base.MountData.fatigueMax = int.MaxValue;
		base.MountData.flightTimeMax = int.MaxValue;
		base.MountData.jumpSpeed = 8f;
		base.MountData.blockExtraJumps = true;
		base.MountData.usesHover = true;
		base.MountData.totalFrames = 5;
		base.MountData.heightBoost = 0;
		int[] verticalOffsets = new int[base.MountData.totalFrames];
		for (int l = 0; l < verticalOffsets.Length; l++)
		{
			verticalOffsets[l] = 0;
		}
		base.MountData.playerYOffsets = verticalOffsets;
		base.MountData.playerHeadOffset = 3;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 2;
		base.MountData.yOffset = 28;
		base.MountData.standingFrameCount = 5;
		base.MountData.standingFrameDelay = 5;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 5;
		base.MountData.runningFrameDelay = 5;
		base.MountData.runningFrameStart = 0;
		base.MountData.flyingFrameCount = 5;
		base.MountData.flyingFrameDelay = 5;
		base.MountData.flyingFrameStart = 0;
		base.MountData.inAirFrameCount = 5;
		base.MountData.inAirFrameDelay = 5;
		base.MountData.inAirFrameStart = 0;
		base.MountData.idleFrameCount = 5;
		base.MountData.idleFrameDelay = 5;
		base.MountData.idleFrameStart = 0;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = 5;
		base.MountData.swimFrameDelay = 5;
		base.MountData.swimFrameStart = 0;
		if (!Main.dedServ)
		{
			base.MountData.frontTextureGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/DraedonGamerChairMount_Glowmask", (AssetRequestMode)2);
			base.MountData.textureWidth = base.MountData.frontTexture.Width();
			base.MountData.textureHeight = base.MountData.frontTexture.Height();
		}
	}

	public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		rotation = drawPlayer.velocity.X * 0.004f;
		drawPlayer.fullRotation = rotation;
		frame = texture.Frame(1, 5, 0, (int)(Main.GlobalTimeWrappedHourly * 13f) % 5);
		return true;
	}
}
