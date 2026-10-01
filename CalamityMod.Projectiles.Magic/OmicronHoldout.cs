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

public class OmicronHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private Color _003CEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<Omicron>();

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
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 4f * (float)base.Projectile.spriteDirection;
		}
	}

	public override float MaxOffsetLengthFromArm => 10f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float PostFireCooldown => ref base.Projectile.ai[1];

	public ref float MaxFireRateShots => ref base.Projectile.ai[2];

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
		if (base.Owner.CantUseHoldout() && PostFireCooldown <= 0f)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (PostFireCooldown > 0f)
		{
			PostFiringCooldown();
		}
		if (base.Owner.Calamity().mouseRight && PostFireCooldown <= 0f)
		{
			if (base.Owner.CheckMana(base.Owner.HeldItem, (int)((float)base.HeldItem.mana * base.Owner.manaCost) * 13, pay: true))
			{
				PostFireCooldown = 100f;
				Shoot(yBeam: true);
				ShootingTimer = 0f;
			}
			else if (base.Projectile.soundDelay <= 0)
			{
				SoundStyle style = SoundID.MaxMana with
				{
					Pitch = -0.5f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.soundDelay = 50;
				ShootingTimer = 0f;
			}
		}
		else if (ShootingTimer >= Omicron.FireRate)
		{
			if (base.Owner.CheckMana(base.Owner.HeldItem, -1, pay: true) && PostFireCooldown <= 0f)
			{
				MaxFireRateShots++;
				if (MaxFireRateShots == 5f)
				{
					Windup = 60f;
					MaxFireRateShots = 1f;
				}
				Shoot(yBeam: false);
				ShootingTimer = 0f;
				if (Windup > 10f && MaxFireRateShots > 0f)
				{
					Windup -= 12f;
				}
				else
				{
					Windup = 10f;
				}
			}
			else if (PostFireCooldown <= 0f)
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
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		(shootDirection * 8f).RotatedBy(0.1f * Utils.GetLerpValue(10f, 55f, Windup, clamped: true));
		(shootDirection * 8f).RotatedBy(-0.1f * Utils.GetLerpValue(10f, 55f, Windup, clamped: true));
		Vector2 firingVelocity3 = shootDirection * 10f;
		if (yBeam)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/OmicronBeam");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int k = 0; k < 6; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 28f, affectedByGravity: false, 8, 0.087f, EffectsColor, new Vector2(2.3f, 0.9f), quickShrink: true));
			}
			base.Owner.SetScreenshake(6.5f);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3, ModContent.ProjectileType<OmicronBeam>(), base.Projectile.damage * 32, base.Projectile.knockBack, base.Projectile.owner);
			}
			for (int i = 0; i < 8; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(GunTipPosition + Main.rand.NextVector2Circular(10f, 10f), firingVelocity3 * Main.rand.NextFloat(0.7f, 1.3f), affectedByGravity: false, Main.rand.Next(20, 30), Main.rand.NextFloat(0.4f, 0.55f), EffectsColor));
			}
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserBigShot");
			style.Volume = 0.2f;
			style.Pitch = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				for (int j = 0; j < 5; j++)
				{
					firingVelocity3 = (shootDirection * 10f).RotatedBy(0.035f * (float)(j + 1) * Utils.GetLerpValue(0f, 55f, Windup, clamped: true));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3 * (1f - (float)j * 0.1f), ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
				}
				for (int l = 0; l < 5; l++)
				{
					firingVelocity3 = (shootDirection * 10f).RotatedBy(-0.035f * (float)(l + 1) * Utils.GetLerpValue(0f, 55f, Windup, clamped: true));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, firingVelocity3 * (1f - (float)l * 0.1f), ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
				}
			}
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition, shootDirection * 18f, affectedByGravity: false, 6, 0.057f, EffectsColor, new Vector2(1.7f, 0.8f), quickShrink: true));
		}
		if (!Main.dedServ)
		{
			for (int m = 0; m < 10; m++)
			{
				Vector2 shootVel = (shootDirection * 15f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust = Dust.NewDustPerfect(GunTipPosition, Main.rand.NextBool(4) ? 267 : 66, shootVel);
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Lerp(EffectsColor, Color.White, 0.5f) : EffectsColor);
			}
			if (yBeam)
			{
				base.OffsetLengthFromArm -= 32f;
			}
			else
			{
				base.OffsetLengthFromArm -= 6f;
			}
		}
	}

	public void PostFiringCooldown()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.channel = true;
		if (PostFireCooldown > 0f && Main.rand.NextBool())
		{
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel, EffectsColor, Main.rand.Next(30, 51), Main.rand.NextFloat(0.1f, 0.4f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
			Dust dust = Dust.NewDustPerfect(GunTipPosition, 303, smokeVel.RotatedByRandom(0.10000000149011612), 80, default(Color), Main.rand.NextFloat(0.2f, 0.8f));
			dust.noGravity = false;
			dust.color = EffectsColor;
		}
		ShootingTimer = 0f;
		PostFireCooldown--;
	}

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(Windup);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		Windup = reader.ReadSingle();
	}

	public OmicronHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Windup = Omicron.StarterWinup;
		EffectsColor = Color.MediumVioletRed;
		base._002Ector();
	}
}
