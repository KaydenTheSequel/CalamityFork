using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ValkyrieRayStaff : ModProjectile, ILocalizedModType, IModType
{
	private const float AimResponsiveness = 0.66f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Items/Weapons/Magic/ValkyrieRay";

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 52;
		base.Projectile.friendly = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 900;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 28 / player.HeldItem.useTime;
		}
		base.Projectile.ai[0] += base.Projectile.localAI[0];
		int maxTime = 28;
		if (base.Projectile.ai[0] > (float)maxTime)
		{
			base.Projectile.Kill();
			return;
		}
		float chargeLevel = MathHelper.Clamp(base.Projectile.ai[0] / 18f, 0f, 1f);
		UpdatePlayerVisuals(player, rrp);
		float angle = base.Projectile.rotation - (float)Math.PI / 2f;
		Vector2 gemOffset = Vector2.One * 18f * 1.4142f;
		Vector2 gemPos = base.Projectile.Center + gemOffset.RotatedBy(angle);
		if (chargeLevel >= 1f && base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			FiringEffects(gemPos);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), gemPos, base.Projectile.velocity, ModContent.ProjectileType<ValkyrieRayBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner).Center = gemPos;
			}
		}
		else if (base.Projectile.ai[1] == 0f)
		{
			UpdateAim(rrp, ((Vector2)(ref base.Projectile.velocity)).Length());
			ChargingEffects(gemPos, chargeLevel);
		}
	}

	private void UpdatePlayerVisuals(Player player, Vector2 rrp)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = rrp;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
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
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = Vector2.Normalize(Main.MouseWorld - source);
		if (aimVector.HasNaNs())
		{
			aimVector = -Vector2.UnitY;
		}
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.66f));
		aimVector *= 30f;
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	private void ChargingEffects(Vector2 center, float chargeLevel)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Color newColor = ValkyrieRay.LightColor;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * chargeLevel);
		int numDust = 2;
		int dustID = 73;
		float incomingRadius = 9f;
		for (int i = 0; i < numDust; i++)
		{
			Vector2 offsetUnit = Main.rand.NextVector2Unit();
			Vector2 position = center + offsetUnit * incomingRadius;
			newColor = default(Color);
			Dust dust = Dust.NewDustDirect(position, 0, 0, dustID, 0f, 0f, 0, newColor);
			dust.velocity = offsetUnit * (0f - Main.rand.NextFloat(2f, 3.5f));
			dust.scale = Main.rand.NextFloat(0.4f, 1f);
			dust.noGravity = true;
		}
	}

	private void FiringEffects(Vector2 center)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item28 with
		{
			Volume = 0.7f
		};
		SoundEngine.PlaySound(in style, center);
		style = SoundID.Item60 with
		{
			Volume = 0.7f
		};
		SoundEngine.PlaySound(in style, center);
		int numDust = 36;
		int dustID = 73;
		for (int i = 0; i < numDust; i++)
		{
			Dust dust = Dust.NewDustDirect(center, 0, 0, dustID);
			dust.velocity = ((float)i * ((float)Math.PI * 2f) / (float)numDust).ToRotationVector2() * 2.2f;
			dust.scale = 1.4f;
			dust.noGravity = true;
		}
	}
}
