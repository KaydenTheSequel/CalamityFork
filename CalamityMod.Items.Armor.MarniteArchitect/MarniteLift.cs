using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.MarniteArchitect;

public class MarniteLift : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.jumpHeight = 0;
		base.MountData.acceleration = 0.2f;
		base.MountData.jumpSpeed = 0f;
		base.MountData.blockExtraJumps = true;
		base.MountData.constantJump = false;
		base.MountData.fallDamage = 1f;
		base.MountData.runSpeed = 2f;
		base.MountData.dashSpeed = 2f;
		base.MountData.flightTimeMax = 0;
		base.MountData.fatigueMax = 0;
		base.MountData.usesHover = false;
		base.MountData.buff = ModContent.BuffType<MarniteLiftBuff>();
		base.MountData.spawnDust = 6;
		base.MountData.totalFrames = 1;
		base.MountData.heightBoost = 0;
		base.MountData.playerYOffsets = Enumerable.Repeat(33, 1).ToArray();
		base.MountData.xOffset = 0;
		base.MountData.yOffset = 0;
		base.MountData.bodyFrame = 0;
		base.MountData.playerHeadOffset = 4;
		if (!Main.dedServ)
		{
			base.MountData.frontTextureGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Armor/MarniteArchitect/MarniteLiftFire", (AssetRequestMode)2);
		}
	}

	public override void UpdateEffects(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = player.Bottom + Vector2.UnitY * 10f;
		Color newColor = Color.DeepSkyBlue;
		Lighting.AddLight(position, ((Color)(ref newColor)).ToVector3());
		float centerDistance = player.GetModPlayer<MarniteArchitectPlayer>().RaycastGround(centerOnly: true).Y;
		if (centerDistance <= MarniteArchitectHeadgear.MaxLiftHeight && Main.rand.NextFloat() > centerDistance / MarniteArchitectHeadgear.MaxLiftHeight * 0.6f && Main.rand.NextBool())
		{
			float scale = 1.2f - centerDistance / MarniteArchitectHeadgear.MaxLiftHeight * 0.7f;
			Vector2 position2 = player.Bottom + Vector2.UnitY * centerDistance + Vector2.UnitX * (float)Main.rand.Next(-16, 16);
			Vector2? velocity = new Vector2(Main.rand.NextFloat(-8f, 8f) * scale - player.velocity.X, Main.rand.NextFloat(-1f, 1f));
			float scale2 = scale * 1.5f;
			newColor = default(Color);
			Dust.NewDustPerfect(position2, 31, velocity, 120, newColor, scale2);
		}
		if (Main.rand.NextBool(3))
		{
			float speedRotation = player.velocity.X * 0.03f;
			Vector2 position3 = player.Bottom + Vector2.UnitX.RotatedBy(speedRotation) * (float)Main.rand.Next(-6, 6) + Vector2.UnitY.RotatedBy(speedRotation) * -2f;
			Vector2? velocity2 = Vector2.UnitY.RotatedBy(speedRotation) * Main.rand.NextFloat(1f, 3f);
			float scale2 = Main.rand.NextFloat(0.6f, 1f);
			newColor = default(Color);
			Dust.NewDustPerfect(position3, 229, velocity2, 120, newColor, scale2).noGravity = true;
		}
	}

	public void DustEffects(Player player)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		for (int j = 0; j < 17; j++)
		{
			Vector2 direction = Main.rand.NextVector2CircularEdge(1f, 1f);
			int dustType = (Main.rand.NextBool() ? 240 : 236);
			Dust.NewDustPerfect(player.Bottom + direction * 3f + Vector2.UnitY * 10f, dustType, direction.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f) - 1.57f) * (float)Main.rand.Next(1, 3), 0, new Color(255, 255, 60) * 0.8f, 0.6f);
		}
	}

	public override void SetMount(Player player, ref bool skipDust)
	{
		DustEffects(player);
		skipDust = true;
	}

	public override void Dismount(Player player, ref bool skipDust)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in MarniteArchitectHeadgear.LiftGoAwaySound, player.Center);
		DustEffects(player);
		skipDust = true;
	}

	public static Vector2 GetFireSquish(float timeOffset, float timeSpeed)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2((float)Math.Sin(Main.GlobalTimeWrappedHourly * timeSpeed + timeOffset) * 0.1f + 1f, 1.2f - 0.3f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * timeSpeed + timeOffset));
	}

	public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		rotation = MathHelper.Clamp(drawPlayer.velocity.X * 0.03f, 0f - MathHelper.ToRadians(7f), MathHelper.ToRadians(7f));
		drawPlayer.fullRotation = rotation;
		if (drawType == 0)
		{
			Texture2D platformTex = base.MountData.frontTexture.Value;
			Texture2D fireTex = base.MountData.frontTextureGlow.Value;
			Vector2 fireOrigin = default(Vector2);
			((Vector2)(ref fireOrigin))._002Ector((float)fireTex.Width / 2f, 0f);
			Color fireColor = Color.White;
			for (int i = 0; i < 3; i++)
			{
				fireColor = Color.White;
				Vector2 fireScale = GetFireSquish((float)i * 0.6f, 14f);
				((Color)(ref fireColor)).A = (byte)(210 - i * 70);
				fireColor *= 0.3f + 0.35f * (float)i;
				fireScale *= drawScale * (1.5f - 0.25f * (float)i);
				playerDrawData.Add(new DrawData(fireTex, drawPosition + new Vector2(0f, 8f), (Rectangle?)new Rectangle(0, 0, fireTex.Width, fireTex.Height), fireColor, drawPlayer.fullRotation, fireOrigin, fireScale * drawScale, (SpriteEffects)0, 0f));
			}
			playerDrawData.Add(new DrawData(platformTex, drawPosition, (Rectangle?)new Rectangle(0, 0, platformTex.Width, platformTex.Height), drawColor, drawPlayer.fullRotation, platformTex.Size() / 2f, drawScale, (SpriteEffects)0, 0f));
		}
		return false;
	}
}
