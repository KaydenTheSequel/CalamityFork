using System;
using System.Collections.Generic;
using CalamityMod.Buffs.Mounts;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class ExoTank : ModMount
{
	public class ExoTankData
	{
		internal bool Hovering;

		internal int HoverTime;

		internal int MinigunFrame;

		internal int MinigunFrameCounter;

		internal float MinigunRotation;

		public ExoTankData()
		{
			Hovering = false;
			HoverTime = MaxHoverTime;
			MinigunFrame = 0;
			MinigunFrameCounter = 0;
			MinigunRotation = 0f;
		}
	}

	public static int DashDamage = 6000;

	public static int MaxHoverTime = 600;

	public static int MissileDamage = 1000;

	public static int MissileAttackRate = 4;

	public static int MissileLauncherFrameCount = 8;

	public static int MissileReuseDelay = 28;

	public static readonly SoundStyle MissileLaunchSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisApolloDash")
	{
		Volume = 0.5f
	};

	public static int MinigunDamage = 400;

	public static int MinigunAttackRate = 4;

	public static int MinigunFrameCount = 4;

	public override void SetStaticDefaults()
	{
		base.MountData.spawnDust = 84;
		base.MountData.spawnDustNoGravity = true;
		base.MountData.buff = ModContent.BuffType<ExoTankBuff>();
		base.MountData.runSpeed = 10f;
		base.MountData.dashSpeed = 22.5f;
		base.MountData.swimSpeed = 10f;
		base.MountData.acceleration = 0.5f;
		base.MountData.fallDamage = 0f;
		base.MountData.jumpHeight = 10;
		base.MountData.jumpSpeed = 12f;
		base.MountData.totalFrames = 3;
		base.MountData.heightBoost = 80;
		int[] array = new int[base.MountData.totalFrames];
		for (int l = 0; l < array.Length; l++)
		{
			array[l] = 76;
		}
		base.MountData.playerYOffsets = array;
		base.MountData.playerHeadOffset = 80;
		base.MountData.bodyFrame = 3;
		base.MountData.xOffset = 0;
		base.MountData.yOffset = -16;
		base.MountData.standingFrameCount = 1;
		base.MountData.standingFrameDelay = 12;
		base.MountData.standingFrameStart = 0;
		base.MountData.runningFrameCount = 3;
		base.MountData.runningFrameDelay = 24;
		base.MountData.runningFrameStart = 0;
		base.MountData.flyingFrameCount = 0;
		base.MountData.flyingFrameDelay = 0;
		base.MountData.flyingFrameStart = 0;
		base.MountData.inAirFrameCount = 1;
		base.MountData.inAirFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.inAirFrameStart = 0;
		base.MountData.idleFrameCount = 1;
		base.MountData.idleFrameDelay = base.MountData.standingFrameDelay;
		base.MountData.idleFrameStart = 0;
		base.MountData.idleFrameLoop = true;
		base.MountData.swimFrameCount = base.MountData.inAirFrameCount;
		base.MountData.swimFrameDelay = base.MountData.inAirFrameDelay;
		base.MountData.swimFrameStart = base.MountData.inAirFrameStart;
		if (!Main.dedServ)
		{
			base.MountData.backTextureExtra = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/ExoTank_BackGun", (AssetRequestMode)2);
			base.MountData.backTextureExtraGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/ExoTank_BackGunGlow", (AssetRequestMode)2);
			base.MountData.frontTextureGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/ExoTank_FrontGlow", (AssetRequestMode)2);
			base.MountData.frontTextureExtra = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/ExoTank_FrontLayer", (AssetRequestMode)2);
			base.MountData.frontTextureExtraGlow = ModContent.Request<Texture2D>("CalamityMod/Items/Mounts/ExoTank_FrontLayerGlow", (AssetRequestMode)2);
			base.MountData.textureWidth = base.MountData.backTexture.Width();
			base.MountData.textureHeight = base.MountData.backTexture.Height();
		}
	}

	public override void SetMount(Player player, ref bool skipDust)
	{
		player.mount._mountSpecificData = new ExoTankData();
	}

	public override void Dismount(Player player, ref bool skipDust)
	{
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.owner == player.whoAmI && proj.type == ModContent.ProjectileType<ExoTankHoverThrust>())
			{
				proj.Kill();
				break;
			}
		}
	}

	public override void UpdateEffects(Player player)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != player.whoAmI)
		{
			return;
		}
		Mount tank = player.mount;
		ExoTankData data = (ExoTankData)player.mount._mountSpecificData;
		ref float minigunRotation = ref data.MinigunRotation;
		float range = 960f;
		int targetNPC = -1;
		bool canFireLasers = true;
		Vector2 gunPosition = player.Center + Vector2.UnitX * 52f * (float)player.direction - Vector2.UnitY * 10f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (!target.CanBeChasedBy(tank))
			{
				continue;
			}
			Vector2 targetDif = target.Center - gunPosition;
			float angle = MathF.Abs(targetDif.ToRotation());
			if (player.direction == 1)
			{
				if (angle > MathHelper.ToRadians(60f) || (canFireLasers && angle > MathHelper.ToRadians(18f)))
				{
					continue;
				}
			}
			else if (angle < MathHelper.ToRadians(120f) || (canFireLasers && angle < MathHelper.ToRadians(162f)))
			{
				continue;
			}
			float distance = ((Vector2)(ref targetDif)).Length();
			if (distance < range && Collision.CanHitLine(gunPosition, 0, 0, target.position, target.width, target.height))
			{
				range = distance;
				targetNPC = target.whoAmI;
				canFireLasers = ((player.direction == 1) ? (angle <= MathHelper.ToRadians(18f)) : (angle >= MathHelper.ToRadians(162f)));
			}
		}
		EntitySource_Mount source = new EntitySource_Mount(player, base.Type);
		if ((int)tank._frameExtraCounter % MissileAttackRate == 0 && tank._frameExtra > 0 && tank._frameExtra < 4)
		{
			if (tank._frameExtra == 1)
			{
				SoundEngine.PlaySound(in MissileLaunchSound, player.Center);
			}
			for (int i = 0; i < 3; i++)
			{
				Vector2 rocketPos = player.Center - Vector2.UnitX * (60f - 4f * (float)i - 6f * (float)tank._frameExtra) * (float)player.direction - Vector2.UnitY * (72f - 8f * (float)i);
				Vector2 rocketVel = (Vector2.UnitX * (float)player.direction).RotatedBy(-(float)Math.PI / 4f * (float)player.direction) * 10f;
				int rocketDamage = (int)player.GetBestClassDamage().ApplyTo(MissileDamage);
				float rocketKB = 1f;
				Projectile.NewProjectile(source, rocketPos, rocketVel, ModContent.ProjectileType<ExoTankMissile>(), rocketDamage, rocketKB, player.whoAmI);
			}
		}
		if (targetNPC != -1)
		{
			tank._aiming = true;
			if (!canFireLasers)
			{
				minigunRotation = 0f;
				return;
			}
			float idealRotation = (Main.npc[targetNPC].Center - gunPosition).SafeNormalize(Vector2.UnitX * (float)player.direction).ToRotation();
			minigunRotation = idealRotation + ((player.direction == 1) ? 0f : ((float)Math.PI));
			if (data.MinigunFrameCounter % MinigunAttackRate == 0)
			{
				SoundStyle style = CommonCalamitySounds.ExoLaserShootSound with
				{
					Volume = 0.32f
				};
				SoundEngine.PlaySound(in style, player.Center);
				Vector2 bulletPos = player.Center + Vector2.UnitX * 28f * (float)player.direction - Vector2.UnitY * 10f + (idealRotation * ((player.direction == 1) ? 1.33f : 1f)).ToRotationVector2() * 38f;
				Vector2 bulletVel = (Main.npc[targetNPC].Center - bulletPos).SafeNormalize(Vector2.UnitX * (float)player.direction) * Main.rand.NextFloat(15f, 16f);
				int bulletDamage = (int)player.GetBestClassDamage().ApplyTo(MinigunDamage);
				float bulletKB = 1f;
				Projectile.NewProjectile(source, bulletPos, bulletVel, ModContent.ProjectileType<ExoTankLaser>(), bulletDamage, bulletKB, player.whoAmI);
				Vector2 flashPos = bulletPos + bulletVel.SafeNormalize(Vector2.Zero) * 36f * ((player.direction == 1) ? 1f : 1.33f);
				for (int j = 0; j < 2; j++)
				{
					Dust dust = Dust.NewDustPerfect(flashPos, 278);
					dust.noGravity = true;
					dust.noLight = true;
					dust.velocity = bulletVel.RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.5f, 1f);
					dust.color = Color.Lerp(Color.OrangeRed, Color.Red, Main.rand.NextFloat(0f, 0.8f));
					dust.scale = Main.rand.NextFloat(0.5f, 0.8f);
				}
				Projectile.NewProjectile(source, flashPos, bulletVel, ModContent.ProjectileType<ExoTankMuzzleFlash>(), 0, 0f, player.whoAmI);
			}
		}
		else
		{
			tank._aiming = false;
			minigunRotation = 0f;
		}
	}

	public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		Mount tank = mountedPlayer.mount;
		ExoTankData data = (ExoTankData)tank._mountSpecificData;
		ref int hoverTime = ref data.HoverTime;
		ref bool hovering = ref data.Hovering;
		if (state <= 1)
		{
			hoverTime = MaxHoverTime;
			hovering = false;
		}
		else if (mountedPlayer.controlJump && mountedPlayer.TryingToHoverDown && hoverTime > 0)
		{
			EntitySource_Mount source = new EntitySource_Mount(mountedPlayer, base.Type);
			if (mountedPlayer.ownedProjectileCounts[ModContent.ProjectileType<ExoTankHoverThrust>()] < 1)
			{
				Projectile.NewProjectile(source, mountedPlayer.Bottom, Vector2.Zero, ModContent.ProjectileType<ExoTankHoverThrust>(), 0, 0f, mountedPlayer.whoAmI);
			}
			hoverTime--;
			hovering = true;
			mountedPlayer.velocity.Y -= 0.4f * mountedPlayer.gravDir;
			mountedPlayer.velocity.Y *= 0.9f;
			if (MathF.Abs(mountedPlayer.velocity.Y) < 0.05f)
			{
				mountedPlayer.velocity.Y = 1E-05f;
			}
		}
		else
		{
			hovering = false;
		}
		if (state == 1 && Math.Abs(velocity.X) > mountedPlayer.mount.RunSpeed)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustDirect(mountedPlayer.BottomLeft - Vector2.UnitX * 40f, mountedPlayer.width + 80, 6, 84);
				dust.velocity = new Vector2(velocity.X * 0.15f, Main.rand.NextFloat(-2f, 0f));
				dust.noLight = true;
				dust.scale = Main.rand.NextFloat(0.2f, 0.8f);
				dust.fadeIn = Main.rand.NextFloat(1f, 1.5f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(mountedPlayer.cMount, mountedPlayer);
			}
			if (mountedPlayer.cMount == 0)
			{
				float direction = Math.Sign(velocity.X);
				mountedPlayer.position += new Vector2(direction * 24f, 0f);
				mountedPlayer.FloorVisuals(Falling: true);
				mountedPlayer.position -= new Vector2(direction * 24f, 0f);
			}
		}
		if (state != 0)
		{
			state = 1;
		}
		if (tank._aiming || tank._frameExtraCounter >= (float)MissileAttackRate)
		{
			tank._frameExtraCounter++;
		}
		else if (tank._frameExtraCounter > 0f)
		{
			tank._frameExtraCounter = 0f;
		}
		int totalUseTime = MissileReuseDelay + MissileAttackRate * MissileLauncherFrameCount;
		if (tank._frameExtraCounter >= (float)totalUseTime)
		{
			tank._frameExtraCounter = 0f;
			tank._frameExtra = 0;
		}
		tank._frameExtra = (int)Utils.Remap(tank._frameExtraCounter, MissileReuseDelay, totalUseTime, 0f, MissileLauncherFrameCount);
		ref int minigunFrame = ref data.MinigunFrame;
		ref int minigunFrameCounter = ref data.MinigunFrameCounter;
		if (tank._aiming || minigunFrameCounter > 0)
		{
			minigunFrameCounter++;
		}
		if (minigunFrameCounter >= MinigunAttackRate * MinigunFrameCount)
		{
			minigunFrameCounter = 0;
			minigunFrame = 0;
		}
		minigunFrame = minigunFrameCounter / MinigunAttackRate;
		return true;
	}

	public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (drawType == 1)
		{
			ExoTankData data = (ExoTankData)drawPlayer.mount._mountSpecificData;
			frame = texture.Frame(1, MinigunFrameCount, 0, data.MinigunFrame);
			drawPosition += new Vector2((drawPlayer.direction == 1) ? 15f : (-15f), 7f);
			spriteEffects = (SpriteEffects)(drawPlayer.direction != 1);
			rotation = data.MinigunRotation;
			drawOrigin = new Vector2((drawPlayer.direction == 1) ? 0f : 80f, 12f);
		}
		if (drawType == 2)
		{
			frame = texture.Frame(1, MissileLauncherFrameCount, 0, drawPlayer.mount._frameExtra);
		}
		return true;
	}
}
