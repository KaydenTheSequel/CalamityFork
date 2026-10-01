using CalamityMod.Buffs.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class BrimroseChair : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 235;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<BrimroseMount>();
		base.MountData.runSpeed = 12f;
		base.MountData.dashSpeed = 12f;
		base.MountData.acceleration = 0.2f;
		base.MountData.fallDamage = 0f;
		base.MountData.fatigueMax = int.MaxValue;
		base.MountData.flightTimeMax = int.MaxValue;
		base.MountData.jumpSpeed = 4f;
		base.MountData.blockExtraJumps = true;
		base.MountData.usesHover = true;
		base.MountData.totalFrames = 4;
		base.MountData.heightBoost = 0;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			array[l] = 0;
		}
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 18;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 0;
		base.MountData.yOffset = 6;
		base.MountData.standingFrameCount = 4;
		base.MountData.standingFrameDelay = 4;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 4;
		base.MountData.runningFrameDelay = 16;
		base.MountData.runningFrameStart = 0;
		base.MountData.flyingFrameCount = 4;
		base.MountData.flyingFrameDelay = 4;
		base.MountData.flyingFrameStart = 0;
		base.MountData.inAirFrameCount = 4;
		base.MountData.inAirFrameDelay = 4;
		base.MountData.inAirFrameStart = 0;
		base.MountData.idleFrameCount = 4;
		base.MountData.idleFrameDelay = 8;
		base.MountData.idleFrameStart = 0;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = 4;
		base.MountData.swimFrameDelay = 4;
		base.MountData.swimFrameStart = 0;
		if (!Main.dedServ)
		{
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}
}
