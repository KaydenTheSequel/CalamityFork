using System.IO;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChromaticEruptionHoldout : BaseGunHoldoutProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<ChromaticEruption>();

	public override string Texture => "CalamityMod/Projectiles/Ranged/ChromaticEruptionHoldout";

	public override float MaxOffsetLengthFromArm => 40f;

	public override float OffsetXUpwards => -10f;

	public override float BaseOffsetY => -12f;

	public override float OffsetYDownwards => 10f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.45f;
		}
	}

	public ref float ShotCooldown => ref base.Projectile.ai[0];

	public ref float ShotsFired => ref base.Projectile.ai[1];

	public ref float ShootTimer => ref base.Projectile.ai[2];

	public int FireBlobs { get; set; }

	public override void KillHoldoutLogic()
	{
		base.KillHoldoutLogic();
		if (ShotsFired >= 24f)
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		int projToShoot = Main.rand.Next(4);
		Color effectcolor = (Color)(projToShoot switch
		{
			0 => Color.DeepSkyBlue, 
			1 => Color.MediumSpringGreen, 
			2 => Color.DarkOrange, 
			_ => Color.Violet, 
		});
		ShootTimer++;
		if (ShootTimer >= 60f)
		{
			if (ShotCooldown == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
				base.Owner.PickAmmo(base.Owner.HeldItem, out projToShoot, out var _, out var damage, out var knockback, out var _);
				if (Main.myPlayer == base.Projectile.owner)
				{
					for (int i = 0; i < 2; i++)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, (base.Projectile.velocity * 10f).RotatedByRandom(0.11999999731779099), ModContent.ProjectileType<ChromaticFire>(), damage, knockback, base.Projectile.owner);
					}
				}
				ShotsFired++;
				ShotCooldown = base.HeldItem.useTime;
				if (FireBlobs == 0 && Main.myPlayer == base.Projectile.owner)
				{
					Vector2 newVel = base.Projectile.velocity * 9f;
					Vector2 newPos = GunTipPosition + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 36f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), newPos, newVel, ModContent.ProjectileType<ChromaticFlare>(), damage, knockback, base.Projectile.owner, ((Vector2)(ref newVel)).Length(), -1f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), newPos, newVel, ModContent.ProjectileType<ChromaticFlare>(), damage, knockback, base.Projectile.owner, ((Vector2)(ref newVel)).Length(), 1f);
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
			for (int j = 0; j < 2; j++)
			{
				int dustType = (Main.rand.NextBool() ? 66 : 247);
				float rotMulti = Main.rand.NextFloat(0.3f, 1f);
				Dust dust = Dust.NewDustPerfect(GunTipPosition, dustType);
				dust.scale = Main.rand.NextFloat(1.2f, 1.8f) * (ShootTimer * 0.025f) - rotMulti * 0.1f;
				dust.noGravity = true;
				dust.velocity = Utils.RotatedByRandom(new Vector2(0f, -2f), rotMulti * 0.3f) * (Main.rand.NextFloat(1f, 3.2f) - rotMulti) * (ShootTimer * 0.025f);
				dust.alpha = Main.rand.Next(90, 150);
				dust.color = effectcolor;
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

	public override void PostDraw(Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)2;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/ChromaticEruptionHoldoutGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}
}
