using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DeathsAscensionSwing : ModProjectile
{
	public int frameX;

	public int frameY;

	public static Asset<Texture2D> glowTexture;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DeathsAscension>();

	public int CurrentFrame
	{
		get
		{
			return frameX * 6 + frameY;
		}
		set
		{
			frameX = value / 6;
			frameY = value % 6;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			glowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 159;
		base.Projectile.height = 230;
		base.Projectile.scale = 1.15f;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.frameCounter = 0;
	}

	public override void AI()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 2)
		{
			CurrentFrame++;
			if (frameX >= 2)
			{
				CurrentFrame = 0;
			}
			if (frameX == 0 && frameY == 3)
			{
				SoundEngine.PlaySound(in SoundID.Item71, base.Projectile.position);
			}
			base.Projectile.frameCounter = 0;
		}
		if ((frameX == 0 && frameY >= 3) || (frameX == 1 && frameY <= 1))
		{
			base.Projectile.idStaticNPCHitCooldown = 8;
		}
		else if (frameX == 1 && frameY > 1)
		{
			base.Projectile.idStaticNPCHitCooldown = 12;
		}
		Vector2 playerRotatedPoint = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.CantUseHoldout())
			{
				HandleChannelMovement(playerRotatedPoint);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (base.Projectile.direction == 1)
		{
			base.Projectile.Left = Owner.MountedCenter;
		}
		else
		{
			base.Projectile.Right = Owner.MountedCenter;
		}
		base.Projectile.position.X += ((base.Projectile.spriteDirection == -1) ? 26f : (-26f));
		base.Projectile.position.Y -= base.Projectile.scale * 2f;
		Owner.ChangeDir(base.Projectile.direction);
		base.Projectile.timeLeft = 2;
		bool ownerFacingRight = Owner.direction == 1;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, GetArmRotation() * (float)(ownerFacingRight ? 1 : (-1)) - (float)Math.PI / 2f + (ownerFacingRight ? 0f : ((float)Math.PI)));
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		base.Projectile.ai[2]--;
	}

	public float GetArmRotation()
	{
		int num = frameX;
		int num2 = frameY;
		switch (num)
		{
		case 0:
			switch (num2)
			{
			case 0:
				return -2.12f;
			case 1:
				return -2.59f;
			case 2:
				return -2.29f;
			case 3:
				return -0.97f;
			case 4:
				return 0.75f;
			case 5:
				return 1.07f;
			}
			break;
		case 1:
			switch (num2)
			{
			case 0:
				return 1.4f;
			case 1:
				return 1.14f;
			case 2:
				return 0.85f;
			case 3:
				return 0.4f;
			case 4:
				return -0.28f;
			case 5:
				return -1.8f;
			}
			break;
		}
		return 0f;
	}

	public void HandleChannelMovement(Vector2 playerRotatedPoint)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newVelocity = Vector2.UnitX * (float)(Main.MouseWorld.X > playerRotatedPoint.X).ToDirectionInt();
		if (base.Projectile.velocity.X != newVelocity.X || base.Projectile.velocity.Y != newVelocity.Y)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = newVelocity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition + ((base.Projectile.spriteDirection == -1) ? new Vector2(60f, 0f) : new Vector2(-60f, 0f)) - base.Projectile.velocity;
		Vector2 origin = value.Size() / new Vector2(2f, 6f) * 0.5f;
		Rectangle frame = value.Frame(2, 6, frameX, frameY);
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.EntitySpriteDraw(color: Lighting.GetColor(Main.LocalPlayer.position.ToTileCoordinates()), texture: value, position: position, sourceRectangle: frame, rotation: base.Projectile.rotation, origin: origin, scale: base.Projectile.scale, effects: spriteEffects);
		Main.EntitySpriteDraw(glowTexture.Value, position, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 170);
	}

	public override bool? CanDamage()
	{
		return (frameX == 0 && frameY >= 3) || frameX == 1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		int maxRifts = 1;
		if (base.Projectile.ai[2] <= 0f && Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<DeathsAscensionRift>()] < maxRifts)
		{
			int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center + Main.rand.NextVector2Circular(28f, 28f), Vector2.Zero, ModContent.ProjectileType<DeathsAscensionRift>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, Main.rand.NextFloat(0f, 3f));
			Main.projectile[p].rotation = Main.rand.NextFloat((float)Math.PI * -2f, (float)Math.PI * 2f);
			SoundStyle style = SoundID.Item165 with
			{
				Pitch = -1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.ai[2] = 40f;
			float screenShakePower = 3f * Utils.GetLerpValue(1300f, 0f, target.Distance(Main.LocalPlayer.Center), clamped: true);
			Main.LocalPlayer.SetScreenshake(screenShakePower);
		}
	}
}
