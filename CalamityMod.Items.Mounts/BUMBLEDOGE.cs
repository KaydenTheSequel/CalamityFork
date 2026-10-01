using CalamityMod.Buffs.Mounts;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class BUMBLEDOGE : ModMount
{
	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 60;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<BumbledogeMount>();
		base.MountData.abilityCooldown = 12;
		base.MountData.runSpeed = 10f;
		base.MountData.dashSpeed = 14.15f;
		base.MountData.acceleration = 0.2f;
		base.MountData.fallDamage = 0f;
		base.MountData.flightTimeMax = 600;
		base.MountData.jumpSpeed = 4f;
		base.MountData.totalFrames = 12;
		base.MountData.heightBoost = 32;
		int[] array = new int[base.MountData.totalFrames];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 28;
		}
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = base.MountData.heightBoost;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 0;
		base.MountData.yOffset = -6;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 5;
		base.MountData.runningFrameDelay = 20;
		base.MountData.runningFrameStart = 1;
		base.MountData.flyingFrameCount = 4;
		base.MountData.flyingFrameDelay = 7;
		base.MountData.flyingFrameStart = 7;
		base.MountData.inAirFrameCount = 1;
		base.MountData.inAirFrameDelay = 11;
		base.MountData.inAirFrameStart = 8;
		base.MountData.idleFrameCount = 1;
		base.MountData.idleFrameDelay = 10;
		base.MountData.idleFrameStart = 0;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.inAirFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		base.MountData.dashingFrameCount = base.MountData.flyingFrameCount;
		base.MountData.dashingFrameDelay = 5;
		base.MountData.dashingFrameStart = base.MountData.flyingFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override void UpdateEffects(Player player)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != player.whoAmI)
		{
			return;
		}
		NPC Target = player.Center.MinionHoming(800f, player, ignoreTiles: false);
		if (player.mount._abilityCooldown == 0 && (Main.rand.NextBool(150) || Target != null))
		{
			player.mount._abilityCooldown = base.MountData.abilityCooldown;
			Vector2 pos = player.Center + Main.rand.NextVector2Circular(20f, 4f) + Vector2.UnitX * 18f * (float)player.direction;
			Vector2 vel = Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(24f)) * Main.rand.NextFloat(-8f, -6f);
			int damage = (int)player.GetBestClassDamage().ApplyTo(180f);
			float kb = 1f;
			Projectile birb = Projectile.NewProjectileDirect(new EntitySource_Mount(player, base.Type), pos, vel, ModContent.ProjectileType<MiniatureFolly>(), damage, kb, player.whoAmI);
			if (birb.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				birb.DamageType = DamageClass.Generic;
				birb.ai[2] = 1f;
			}
		}
	}
}
