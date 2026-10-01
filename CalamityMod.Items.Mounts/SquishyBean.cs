using CalamityMod.Buffs.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class SquishyBean : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.buff = ModContent.BuffType<SquishyBeanBuff>();
		base.MountData.runSpeed = 5f;
		base.MountData.dashSpeed = 8f;
		base.MountData.acceleration = 0.1f;
		base.MountData.fallDamage = 0f;
		base.MountData.jumpHeight = 20;
		base.MountData.jumpSpeed = 15f;
		base.MountData.constantJump = true;
		base.MountData.totalFrames = 4;
		base.MountData.heightBoost = 42;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			switch (l)
			{
			case 0:
				array[l] = 46;
				break;
			case 1:
				array[l] = 48;
				break;
			case 2:
				array[l] = 50;
				break;
			case 3:
				array[l] = 50;
				break;
			}
		}
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 30;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 0;
		base.MountData.yOffset = 11;
		base.MountData.standingFrameCount = 1;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 4;
		base.MountData.runningFrameDelay = 24;
		base.MountData.runningFrameStart = 0;
		base.MountData.flyingFrameCount = 0;
		base.MountData.flyingFrameDelay = 0;
		base.MountData.flyingFrameStart = 0;
		base.MountData.inAirFrameCount = 1;
		base.MountData.inAirFrameDelay = 12;
		base.MountData.inAirFrameStart = 0;
		base.MountData.idleFrameCount = 4;
		base.MountData.idleFrameDelay = 12;
		base.MountData.idleFrameStart = 0;
		base.MountData.idleFrameLoop = true;
		base.MountData.flyingFrameCount = 4;
		base.MountData.flyingFrameDelay = 12;
		base.MountData.flyingFrameStart = 0;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.inAirFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override void UpdateEffects(Player player)
	{
		if (player.velocity.Y > 0f || player.controlDown)
		{
			player.gravity = 1f;
			player.maxFallSpeed = 20f;
		}
	}
}
