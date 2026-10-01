using System.IO;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class WildfireBloomHoldout : BaseGunHoldoutProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<WildfireBloom>();

	public override string Texture => "CalamityMod/Projectiles/Ranged/WildfireBloomHoldout";

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 17f - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 7f * (float)base.Projectile.spriteDirection * base.Owner.gravDir;
		}
	}

	public override float MaxOffsetLengthFromArm => 20f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public ref float ShotCooldown => ref base.Projectile.ai[0];

	public ref float ShotsFired => ref base.Projectile.ai[1];

	public ref float ShootTimer => ref base.Projectile.ai[2];

	public int FireBlobs { get; set; }

	public override void KillHoldoutLogic()
	{
		base.KillHoldoutLogic();
		if (ShotsFired >= 16f)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		ShootTimer++;
		if (ShootTimer >= 60f)
		{
			if (ShotCooldown == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
				base.Owner.PickAmmo(base.Owner.HeldItem, out var _, out var _, out var damage, out var knockback, out var _);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, (base.Projectile.velocity * 9f).RotatedByRandom(0.07999999821186066), ModContent.ProjectileType<WildfireBloomFire>(), damage, knockback, base.Projectile.owner);
				}
				ShotsFired++;
				ShotCooldown = base.HeldItem.useTime;
				if (FireBlobs == 0 && Main.myPlayer == base.Projectile.owner)
				{
					float randAngle = Main.rand.NextFloat(8f, 15f);
					Vector2 newVel = (base.Projectile.velocity * 9f).RotatedBy(MathHelper.ToRadians(randAngle)) * 2f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, newVel, ModContent.ProjectileType<WildfireBloomFlare>(), damage, knockback, base.Projectile.owner);
					newVel = (base.Projectile.velocity * 9f).RotatedBy(MathHelper.ToRadians(0f - randAngle)) * 2f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, newVel, ModContent.ProjectileType<WildfireBloomFlare>(), damage, knockback, base.Projectile.owner);
					FireBlobs = 3;
				}
				else
				{
					FireBlobs--;
				}
			}
			else
			{
				ShotCooldown--;
			}
		}
		else
		{
			ShotsFired = 0f;
			for (int i = 0; i < 2; i++)
			{
				float rotMulti = Main.rand.NextFloat(0.3f, 1f);
				Dust dust = Dust.NewDustPerfect(GunTipPosition, Main.rand.NextBool(5) ? 135 : 107);
				dust.noGravity = true;
				dust.velocity = Utils.RotatedByRandom(new Vector2(0f, -2f), rotMulti * 0.3f) * (Main.rand.NextFloat(1f, 2.9f) - rotMulti);
				dust.scale = Main.rand.NextFloat(1.2f, 1.8f) * (ShootTimer * 0.015f);
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		base.OnSpawn(source);
		SoundStyle style = SoundID.Item73 with
		{
			Volume = 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(FireBlobs);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		FireBlobs = reader.ReadInt32();
	}
}
