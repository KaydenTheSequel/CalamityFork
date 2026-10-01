using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MirrorBladeProjectile : BaseSwordHoldoutProjectile, ILocalizedModType, IModType
{
	public bool SpawnShards = true;

	public List<int> reflectedProjectiles = new List<int>();

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public override int swingWidth => 200;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<MirrorBlade>()).Item;

	public override int AfterImageLength => 10;

	public override int OffsetDistance => 90;

	public override bool drawSwordTrail => false;

	public override Color[] trailColors
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			return (Color[])(object)new Color[3]
			{
				Color.Red,
				Color.MediumPurple,
				Color.Purple
			};
		}
	}

	public override int StartupTime { get; set; }

	public override int CooldownTime { get; set; }

	public override bool AlternateSwings => false;

	public override bool useMeleeSpeed => true;

	public override int swingTime { get; set; } = 8;

	public override SoundStyle? UseSound => SoundID.Item71 with
	{
		Volume = 0.9f
	};

	public override float trailOffset => 28f;

	public override int trailLength => 40;

	public override void Defaults()
	{
		base.Projectile.extraUpdates = 3;
	}

	public override void Spawn()
	{
		BaseSwordHoldoutPlayer modPlayer = Main.player[base.Projectile.owner].GetModPlayer<BaseSwordHoldoutPlayer>();
		StartupTime = 15;
		CooldownTime = 15;
		swingTime -= StartupTime + CooldownTime;
		modPlayer.swingNum = (modPlayer.swingNum + 1) % 2;
		base.Projectile.timeLeft = 600;
	}

	public override void AdditionalAI()
	{
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		if (base.inStartup)
		{
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.75f, 1f, base.StartupCompletion);
		}
		else if (base.inCooldown)
		{
			base.Projectile.Opacity = 1f;
			base.Projectile.scale = baseScale * MathHelper.Lerp(1f, 0.75f, base.CooldownCompletion);
		}
		else
		{
			base.Projectile.Opacity = 1f;
		}
		if (!base.inStartup && !base.inCooldown)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.type == ModContent.ProjectileType<DoGLaserWalls>() && proj.ModProjectile<DoGLaserWalls>().canDamage && !reflectedProjectiles.Contains(proj.whoAmI))
				{
					reflectedProjectiles.Add(proj.whoAmI);
					for (int i = 0; i < 3; i++)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), proj.Center, proj.velocity * -1f, ModContent.ProjectileType<MirrorBlast>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
						SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact, base.Projectile.Center);
					}
					continue;
				}
				Rectangle hitbox = proj.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(base.Projectile.Hitbox) && proj.hostile && !reflectedProjectiles.Contains(proj.whoAmI))
				{
					reflectedProjectiles.Add(proj.whoAmI);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), proj.Center, proj.velocity * -1f, ModContent.ProjectileType<MirrorBlast>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
					SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact, base.Projectile.Center);
				}
			}
		}
		Lighting.AddLight(Main.player[base.Projectile.owner].Center, 0.96f, 0.91f, 1f);
	}

	public override float SwingFunction()
	{
		if (base.inStartup)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.7f, (float)(-swingWidth) * 0.4f, 1f - MathF.Pow(base.StartupCompletion, 0.5f)));
		}
		if (base.inCooldown)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)swingWidth * 0.5f, 360f - (float)swingWidth * 0.4f, MathF.Pow(base.CooldownCompletion, 0.5f)));
		}
		return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.5f, (float)swingWidth * 0.5f, base.SwingCompletion));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Nightwither>(), 300);
		if (SpawnShards)
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<MirrorBlast>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
			}
			SpawnShards = false;
		}
		int slashCreatorID = ModContent.ProjectileType<MirrorBladeSlashCreator>();
		int damage = (int)((float)base.Projectile.damage * 0.5f);
		float knockback = base.Projectile.knockBack * 0.5f;
		if (Owner.ownedProjectileCounts[slashCreatorID] < 4)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, slashCreatorID, damage, knockback, base.Projectile.owner, target.whoAmI, base.Projectile.rotation, 1f);
			Owner.ownedProjectileCounts[slashCreatorID]++;
		}
		SoundEngine.PlaySound(CommonCalamitySounds.SwiftSliceSound with
		{
			Volume = CommonCalamitySounds.SwiftSliceSound.Volume * 0.3f
		}, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact, base.Projectile.Center);
	}

	public override float trailWidth(float completion, Vector2 vertexPos)
	{
		return 60f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.White;
		return base.PreDraw(ref lightColor);
	}
}
