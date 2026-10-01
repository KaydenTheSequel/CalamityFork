using System;
using CalamityMod.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class Crysthamyr : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 173;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<GazeOfCrysthamyrBuff>();
		base.MountData.runSpeed = 5f;
		base.MountData.dashSpeed = 10f;
		base.MountData.swimSpeed = 6f;
		base.MountData.acceleration = 1f;
		base.MountData.fallDamage = 0f;
		base.MountData.flightTimeMax = 750;
		base.MountData.jumpSpeed = 12f;
		base.MountData.blockExtraJumps = true;
		base.MountData.totalFrames = 16;
		base.MountData.heightBoost = 32;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			array[l] = 30;
		}
		array[1] = 28;
		array[5] = 40;
		array[6] = 40;
		array[7] = 40;
		array[8] = 38;
		array[9] = 40;
		array[10] = 40;
		array[12] = 28;
		array[14] = 28;
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 38;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = -56;
		base.MountData.yOffset = -22;
		base.MountData.standingFrameCount = 5;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = base.MountData.standingFrameCount;
		base.MountData.runningFrameDelay = 48;
		base.MountData.runningFrameStart = 11;
		base.MountData.flyingFrameCount = 6;
		base.MountData.flyingFrameDelay = base.MountData.flyingFrameCount;
		base.MountData.flyingFrameStart = base.MountData.standingFrameCount;
		base.MountData.inAirFrameCount = base.MountData.flyingFrameCount;
		base.MountData.inAirFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.inAirFrameStart = base.MountData.standingFrameCount;
		base.MountData.idleFrameCount = base.MountData.standingFrameCount;
		base.MountData.idleFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.idleFrameStart = base.MountData.standingFrameStart;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.runningFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.frontTextureExtra = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/CrysthamyrExtra", (AssetRequestMode)2);
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override void UpdateEffects(Player player)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1f, 0f, 1f);
		if (Math.Abs(player.velocity.X) > 6f || Math.Abs(player.velocity.Y) > 6f)
		{
			int rand = Main.rand.Next(3);
			switch (rand)
			{
			case 0:
				rand = 173;
				break;
			case 1:
				rand = 109;
				break;
			case 2:
				rand = 70;
				break;
			}
			Rectangle rect = player.getRect();
			((Rectangle)(ref rect)).Inflate(50, 20);
			if (Math.Abs(player.velocity.X) > 14f || Math.Abs(player.velocity.Y) > 14f)
			{
				for (int i = 0; i < 2; i++)
				{
					int dust = Dust.NewDust(new Vector2((float)rect.X, (float)rect.Y), rect.Width, rect.Height, rand);
					Main.dust[dust].noGravity = true;
				}
			}
			int dust2 = Dust.NewDust(new Vector2((float)rect.X, (float)rect.Y), rect.Width, rect.Height, rand);
			Main.dust[dust2].noGravity = true;
		}
		if (player.controlJump && player.TryingToHoverUp)
		{
			player.velocity.Y -= 0.4f * player.gravDir;
			if (player.gravDir == 1f)
			{
				if (player.velocity.Y > 0f)
				{
					player.velocity.Y--;
				}
				else if (player.velocity.Y > 0f - base.MountData.jumpSpeed)
				{
					player.velocity.Y -= 0.2f;
				}
				if (player.velocity.Y < (0f - base.MountData.jumpSpeed) * 3f)
				{
					player.velocity.Y = (0f - base.MountData.jumpSpeed) * 3f;
				}
			}
			else
			{
				if (player.velocity.Y < 0f)
				{
					player.velocity.Y++;
				}
				else if (player.velocity.Y < base.MountData.jumpSpeed)
				{
					player.velocity.Y += 0.2f;
				}
				if (player.velocity.Y > base.MountData.jumpSpeed * 3f)
				{
					player.velocity.Y = base.MountData.jumpSpeed * 3f;
				}
			}
		}
		if (player.velocity.Y == 0f)
		{
			return;
		}
		if (player.mount.PlayerOffset == 38)
		{
			if (!player.flapSound)
			{
				SoundEngine.PlaySound(in SoundID.Item32, player.Center);
			}
			player.flapSound = true;
		}
		else
		{
			player.flapSound = false;
		}
	}
}
