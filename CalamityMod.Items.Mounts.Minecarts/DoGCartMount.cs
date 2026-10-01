using System;
using System.Collections.Generic;
using CalamityMod.Buffs.Mounts;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts.Minecarts;

public class DoGCartMount : ModMount
{
	public const int SegmentCount = 18;

	public override void SetStaticDefaults()
	{
		MountID.Sets.Cart[base.Type] = true;
		Mount.SetAsMinecart(base.MountData, ModContent.BuffType<TheCartofGodsBuff>(), base.MountData.frontTexture);
		base.MountData.delegations.MinecartDust = CreateSparkDust;
		base.MountData.spawnDust = 173;
		base.MountData.xOffset = 2;
		base.MountData.yOffset = 12;
		base.MountData.idleFrameCount = 1;
		base.MountData.idleFrameDelay = 10;
		base.MountData.idleFrameStart = 0;
		base.MountData.runSpeed = 20f;
		base.MountData.acceleration = 0.1f;
		base.MountData.MinecartUpgradeRunSpeed = 22f;
		base.MountData.MinecartUpgradeAcceleration = 0.16f;
		if (!Main.dedServ)
		{
			base.MountData.textureWidth = 74;
			base.MountData.textureHeight = 114;
		}
	}

	public static void CreateSparkDust(Vector2 dustPosition)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offsetDirection = DelegateMethods.Minecart.rotation.ToRotationVector2();
		offsetDirection *= new Vector2((float)Main.rand.NextBool().ToDirectionInt(), 1f) * 13f;
		dustPosition += offsetDirection;
		Dust spark = Dust.NewDustPerfect(dustPosition, 234);
		spark.velocity = Main.rand.NextVector2Circular(4f, 4f);
		spark.velocity.X *= Main.rand.NextFloat(0.25f, 1f);
		spark.velocity.Y -= Main.rand.NextFloat(1.25f, 3f);
		spark.position.Y -= 4f;
		spark.scale = Main.rand.NextFloat(0.9f, 1.4f);
		spark.noGravity = true;
		if (Main.rand.NextBool(8))
		{
			spark.scale *= 1.5f;
		}
		if (Main.rand.NextBool(8))
		{
			spark.scale *= 0.667f;
		}
		if (Main.rand.NextBool(3))
		{
			spark.scale *= 0.65f;
		}
		else
		{
			spark.noGravity = false;
		}
	}

	public static float CalculateIdealWormRotation(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		int direction = (player.velocity.SafeNormalize(Vector2.UnitX * (float)player.direction).X > 0f).ToDirectionInt();
		if (player.velocity.X == 0f)
		{
			direction = player.direction;
		}
		return ((direction == 1) ? 0f : ((float)Math.PI)) - DelegateMethods.Minecart.rotation * 0.1f;
	}

	public static Vector2 CalculateSegmentWaveOffset(int index, Player player)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		return (CalculateIdealWormRotation(player) - (float)Math.PI / 2f + DelegateMethods.Minecart.rotation).ToRotationVector2() * (float)Math.Sin((float)Math.PI * (float)index / (float)player.Calamity().DoGCartSegments.Length + (float)Main.GameUpdateCount / 14f) * 5f * Utils.GetLerpValue(10f, 4f, ((Vector2)(ref player.velocity)).Length(), clamped: true);
	}

	public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		if (drawPlayer.Calamity().DoGCartSegments == null || drawPlayer.Calamity().DoGCartSegments[0] == null)
		{
			return true;
		}
		List<Vector2> rotationAdjustedPositions = new List<Vector2> { drawPlayer.Calamity().DoGCartSegments[0].Center };
		for (int i = 1; i < drawPlayer.Calamity().DoGCartSegments.Length; i++)
		{
			rotationAdjustedPositions.Add(drawPlayer.Center - (drawPlayer.Center - drawPlayer.Calamity().DoGCartSegments[i].Center).RotatedBy(0f - drawPlayer.Calamity().SmoothenedMinecartRotation));
		}
		for (int j = 0; j < 18; j++)
		{
			DoGCartSegment segment = drawPlayer.Calamity().DoGCartSegments[j];
			if (segment == null)
			{
				break;
			}
			Texture2D segmentTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/Minecarts/DoGCartBody", (AssetRequestMode)2).Value;
			if (j == 17)
			{
				segmentTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/Minecarts/DoGCartTail", (AssetRequestMode)2).Value;
			}
			Vector2 segmentDrawPosition = rotationAdjustedPositions[j] + CalculateSegmentWaveOffset(j, drawPlayer);
			Color segmentColor = Lighting.GetColor((int)segmentDrawPosition.X / 16, (int)segmentDrawPosition.Y / 16);
			Vector2 origin = segmentTexture.Size() * 0.5f;
			float segmentRotation = segment.Rotation - DelegateMethods.Minecart.rotation;
			DrawData drawData = new DrawData(segmentTexture, segmentDrawPosition - Main.screenPosition, null, segmentColor, segmentRotation, origin, Vector2.One, (SpriteEffects)0);
			drawData.shader = Mount.currentShader;
			DrawData segmentDrawData = drawData;
			playerDrawData.Add(segmentDrawData);
		}
		return true;
	}
}
