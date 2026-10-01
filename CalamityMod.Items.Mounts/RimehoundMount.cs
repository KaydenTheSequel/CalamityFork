using System;
using CalamityMod.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class RimehoundMount : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 192;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<RimehoundBuff>();
		base.MountData.runSpeed = 7.5f;
		base.MountData.swimSpeed = 3f;
		base.MountData.acceleration = 0.24f;
		base.MountData.fallDamage = 0f;
		base.MountData.jumpHeight = 16;
		base.MountData.jumpSpeed = 7f;
		base.MountData.totalFrames = 13;
		base.MountData.heightBoost = 32;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			array[l] = 30;
		}
		array[1] = 28;
		array[4] = 28;
		array[7] = 28;
		array[10] = 28;
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 38;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = -6;
		base.MountData.yOffset = 13;
		base.MountData.standingFrameCount = 6;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = base.MountData.standingFrameCount;
		base.MountData.runningFrameDelay = 36;
		base.MountData.runningFrameStart = base.MountData.standingFrameCount;
		base.MountData.inAirFrameCount = 1;
		base.MountData.inAirFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.inAirFrameStart = base.MountData.standingFrameDelay;
		base.MountData.idleFrameCount = base.MountData.standingFrameCount;
		base.MountData.idleFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.idleFrameStart = base.MountData.standingFrameStart;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.inAirFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		bool num = Math.Abs(velocity.X) > mountedPlayer.mount.RunSpeed / 2f;
		float direction = Math.Sign(mountedPlayer.velocity.X);
		float dustYOffset = 12f;
		float dustXOffset = 40f;
		if (!num)
		{
			mountedPlayer.basiliskCharge = 0f;
		}
		else
		{
			mountedPlayer.basiliskCharge = Utils.Clamp(mountedPlayer.basiliskCharge + 1f / 180f, 0f, 1f);
		}
		if ((double)mountedPlayer.position.Y > Main.worldSurface * 16.0 + 160.0)
		{
			Lighting.AddLight(mountedPlayer.Center, 0.2f, 0.25f, 0.25f);
		}
		if (num && velocity.Y == 0f)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 192)];
				obj.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat() * -2f);
				obj.noLight = true;
				obj.scale = 0.2f + Main.rand.NextFloat() * 0.8f;
				obj.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
				obj.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
			}
			if (mountedPlayer.cMount == 0)
			{
				mountedPlayer.position += new Vector2(direction * 24f, 0f);
				mountedPlayer.FloorVisuals(Falling: true);
				mountedPlayer.position -= new Vector2(direction * 24f, 0f);
			}
		}
		if (direction == (float)mountedPlayer.direction)
		{
			for (int j = 0; j < (int)(3f * mountedPlayer.basiliskCharge); j++)
			{
				Dust dust = Main.dust[Dust.NewDust(mountedPlayer.BottomLeft, mountedPlayer.width, 6, 67)];
				Vector2 dustVel = mountedPlayer.Center + new Vector2(direction * dustXOffset, dustYOffset);
				dust.position = mountedPlayer.Center + new Vector2(direction * (dustXOffset - 2f), dustYOffset - 6f + Main.rand.NextFloat() * 12f);
				dust.velocity = (dust.position - dustVel).SafeNormalize(Vector2.Zero) * (3.5f + Main.rand.NextFloat() * 0.5f);
				if (dust.velocity.Y < 0f)
				{
					dust.velocity.Y *= 1f + 2f * Main.rand.NextFloat();
				}
				dust.velocity += mountedPlayer.velocity * 0.55f;
				dust.velocity *= ((Vector2)(ref mountedPlayer.velocity)).Length() / mountedPlayer.mount.RunSpeed;
				dust.velocity *= mountedPlayer.basiliskCharge;
				dust.noGravity = true;
				dust.noLight = true;
				dust.scale = 0.2f + Main.rand.NextFloat() * 0.8f;
				dust.fadeIn = 0.5f + Main.rand.NextFloat() * 1f;
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
			}
		}
		return true;
	}
}
