using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GenesisHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private Color _003CEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<Genesis>();

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
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 2.5f * (float)base.Projectile.spriteDirection;
		}
	}

	public override float MaxOffsetLengthFromArm => 10f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float MaxFirerateShots => ref base.Projectile.ai[1];

	public bool FireYBeam
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = (value ? 1f : 0f);
		}
	}

	public float Windup { get; set; }

	public Color EffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CEffectsColor_003Ek__BackingField = value;
		}
	}

	public override void KillHoldoutLogic()
	{
		if (base.HeldItem.type != base.Owner.HeldItem.type || base.Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (ShootingTimer >= Genesis.FireRate)
		{
			if (base.Owner.CheckMana(base.HeldItem, -1, pay: true))
			{
				MaxFirerateShots++;
				if (MaxFirerateShots == 6f)
				{
					FireYBeam = true;
					Windup = 60f;
				}
				if (FireYBeam)
				{
					Shoot(yBeam: true);
					MaxFirerateShots = 0f;
				}
				else
				{
					Shoot(yBeam: false);
				}
				ShootingTimer = 0f;
				if (Windup > 10f)
				{
					if (!FireYBeam)
					{
						Windup -= 12f;
					}
				}
				else
				{
					Windup = 10f;
				}
				FireYBeam = false;
			}
			else
			{
				SoundStyle style = SoundID.MaxMana with
				{
					Pitch = -0.5f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.Kill();
			}
		}
		ShootingTimer++;
	}

	public void Shoot(bool yBeam)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		(shootDirection * 8f).RotatedBy(0.1f * Utils.GetLerpValue(10f, 55f, Windup, clamped: true));
		(shootDirection * 8f).RotatedBy(-0.1f * Utils.GetLerpValue(10f, 55f, Windup, clamped: true));
		Vector2 firingVelocity3 = shootDirection * 10f;
		if (yBeam)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong");
			style.Volume = 0.35f;
			style.Pitch = 1f;
			style.PitchVariance = 0.15f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3, ModContent.ProjectileType<GenesisBeam>(), base.Projectile.damage * 9, base.Projectile.knockBack, base.Projectile.owner);
			}
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 18f, affectedByGravity: false, 6, 0.057f, EffectsColor, new Vector2(1.7f, 0.8f), quickShrink: true));
			for (int i = 0; i < 8; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(GunTipPosition + Main.rand.NextVector2Circular(10f, 10f), firingVelocity3 * Main.rand.NextFloat(0.7f, 1.3f), affectedByGravity: false, Main.rand.Next(20, 30), Main.rand.NextFloat(0.4f, 0.55f), EffectsColor));
			}
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot");
			style.Volume = 0.25f;
			style.Pitch = 1f;
			style.PitchVariance = 0.35f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int j = 0; j < 4; j++)
			{
				firingVelocity3 = (shootDirection * 10f).RotatedBy(0.05f * (float)(j + 1) * Utils.GetLerpValue(0f, 55f, Windup, clamped: true));
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3 * (1f - (float)j * 0.1f), ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
				}
			}
			for (int k = 0; k < 4; k++)
			{
				firingVelocity3 = (shootDirection * 10f).RotatedBy(-0.05f * (float)(k + 1) * Utils.GetLerpValue(0f, 55f, Windup, clamped: true));
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3 * (1f - (float)k * 0.1f), ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
				}
			}
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 18f, affectedByGravity: false, 6, 0.057f, EffectsColor, new Vector2(1.7f, 0.8f), quickShrink: true));
		}
		if (!Main.dedServ)
		{
			for (int l = 0; l < 10; l++)
			{
				Vector2 shootVel = (shootDirection * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust = Dust.NewDustPerfect(GunTipPosition, Main.rand.NextBool(4) ? 267 : 66, shootVel);
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Lerp(EffectsColor, Color.White, 0.5f) : EffectsColor);
			}
			if (yBeam)
			{
				base.OffsetLengthFromArm -= 27f;
			}
			else
			{
				base.OffsetLengthFromArm -= 5f;
			}
		}
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(Windup);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		Windup = reader.ReadSingle();
	}

	public GenesisHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Windup = Genesis.StarterWindup;
		EffectsColor = Color.MediumSlateBlue;
		base._002Ector();
	}
}
