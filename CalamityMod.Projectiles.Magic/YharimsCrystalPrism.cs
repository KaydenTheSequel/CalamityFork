using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class YharimsCrystalPrism : ModProjectile, ILocalizedModType, IModType
{
	public const int NumBeams = 6;

	public const float MaxCharge = 180f;

	public const float DamageStart = 30f;

	private const float DustStart = 30f;

	private const float AimResponsiveness = 0.89f;

	private const int SoundInterval = 20;

	private const float MaxManaConsumptionDelay = 15f;

	private const float MinManaConsumptionDelay = 5f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float FrameCounter => ref base.Projectile.ai[0];

	public ref float ManaConsumptionFrame => ref base.Projectile.ai[1];

	public ref float ManaConsumptionDelay => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 22;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		base.Projectile.damage = ((player.HeldItem != null) ? player.GetWeaponDamage(player.HeldItem) : 0);
		FrameCounter++;
		float chargeRatio = MathHelper.Clamp(FrameCounter / 180f, 0f, 1f);
		base.Projectile.frameCounter++;
		int framesPerAnimationUpdate = ((FrameCounter >= 180f) ? 2 : ((FrameCounter >= 118.8f) ? 3 : 4));
		if (base.Projectile.frameCounter >= framesPerAnimationUpdate)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= 6)
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.soundDelay <= 0)
		{
			base.Projectile.soundDelay = 20;
			if (FrameCounter > 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item15, base.Projectile.Center);
			}
		}
		if (FrameCounter > 30f && Main.rand.NextFloat() < chargeRatio)
		{
			SpawnEjectionDust(chargeRatio);
		}
		UpdatePlayerVisuals(player, rrp);
		if (base.Projectile.owner == Main.myPlayer)
		{
			float speedTimesScale = player.HeldItem.shootSpeed * base.Projectile.scale;
			UpdateAim(rrp, speedTimesScale);
			bool allowContinuedUse = !ShouldConsumeMana() || player.CheckMana(player.HeldItem, -1, pay: true);
			bool crystalStillInUse = !player.CantUseHoldout() & allowContinuedUse;
			if (crystalStillInUse && FrameCounter == 1f)
			{
				Vector2 beamVelocity = Vector2.Normalize(base.Projectile.velocity);
				if (beamVelocity.HasNaNs())
				{
					beamVelocity = -Vector2.UnitY;
				}
				int damage = base.Projectile.damage;
				float kb = base.Projectile.knockBack;
				for (int b = 0; b < 6; b++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, beamVelocity, ModContent.ProjectileType<YharimsCrystalBeam>(), damage, kb, base.Projectile.owner, b, Projectile.GetByUUID(base.Projectile.owner, base.Projectile.whoAmI));
				}
				base.Projectile.netUpdate = true;
			}
			else if (!crystalStillInUse)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.timeLeft = 2;
	}

	private bool ShouldConsumeMana()
	{
		if (ManaConsumptionDelay == 0f)
		{
			ManaConsumptionFrame = (ManaConsumptionDelay = 15f);
			return true;
		}
		bool num = FrameCounter == ManaConsumptionFrame;
		if (num)
		{
			ManaConsumptionDelay = MathHelper.Clamp(ManaConsumptionDelay - 1f, 5f, 15f);
			ManaConsumptionFrame += ManaConsumptionDelay;
		}
		return num;
	}

	private void UpdateAim(Vector2 source, float speed)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = Vector2.Normalize(Main.MouseWorld - source);
		if (aimVector.HasNaNs())
		{
			aimVector = -Vector2.UnitY;
		}
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.89f));
		aimVector *= speed;
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	private void UpdatePlayerVisuals(Player player, Vector2 rrp)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = rrp;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	private void SpawnEjectionDust(float charge)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		Vector2 projDir = Vector2.Normalize(base.Projectile.velocity);
		int dustType = 90;
		float dustAngle = (float)Math.PI * 19f / 25f * (Main.rand.NextBool() ? 1f : (-1f));
		float scale = Main.rand.NextFloat(0.9f, 1.2f);
		float speed = 18f * charge;
		Vector2 dustVel = projDir.RotatedBy(dustAngle) * speed;
		float dustForwardOffset = 11f;
		Dust dust = Dust.NewDustDirect(base.Projectile.Center + dustForwardOffset * projDir, 1, 1, dustType, dustVel.X, dustVel.Y);
		dust.position += Main.rand.NextVector2Circular(2f, 2f);
		dust.noGravity = true;
		dust.scale = scale;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects eff = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int texYOffset = frameHeight * base.Projectile.frame;
		Vector2 sheetInsertVec = (base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition).Floor();
		Main.spriteBatch.Draw(tex, sheetInsertVec, (Rectangle?)new Rectangle(0, texYOffset, tex.Width, frameHeight), Color.White, base.Projectile.rotation, new Vector2((float)tex.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, eff, 0f);
		return false;
	}
}
