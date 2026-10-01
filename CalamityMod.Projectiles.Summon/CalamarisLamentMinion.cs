using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CalamarisLamentMinion : BaseMinionProjectile
{
	public enum AIState
	{
		Idle,
		Shooting,
		Latching
	}

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<CalamarisLamentMinion>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<CalamarisLamentBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.CalamarisLament;

	public override float EnemyDistanceDetection => CalamarisLament.EnemyDistanceDetection;

	public override bool PreventTargettingUntilTargetHit => false;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = (float)value;
		}
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = (base.Projectile.height = 31);
	}

	public override void MinionAI()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			if (Main.rand.NextBool(750))
			{
				SoundEngine.PlaySound(in CalamarisLament.GFB, base.Projectile.Center);
			}
		}
		else if (Main.rand.NextBool(800))
		{
			SoundStyle glubNoise = (Main.rand.NextBool() ? SoundID.Zombie35 : SoundID.Zombie34);
			SoundStyle trollBirdChirpingSound = SoundID.Zombie16;
			SoundEngine.PlaySound(Main.rand.NextBool(2000) ? trollBirdChirpingSound : glubNoise, base.Projectile.Center);
		}
		switch (State)
		{
		case AIState.Idle:
			IdleState();
			break;
		case AIState.Shooting:
			ShootingState();
			break;
		case AIState.Latching:
			LatchingState();
			break;
		}
	}

	private void IdleState()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(base.Owner.Center, 320f))
		{
			base.Projectile.velocity = (base.Projectile.velocity + base.Projectile.SafeDirectionTo(base.Owner.Center)) * 0.9f;
			SyncVariables();
		}
		if (!base.Projectile.WithinRange(base.Owner.Center, 1200f))
		{
			base.Projectile.Center = base.Owner.Center;
			SyncVariables();
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(MathHelper.ToRadians(base.Projectile.velocity.X * 2f), 0.1f);
		base.Projectile.MinionAntiClump(0.5f);
		if (base.Target != null)
		{
			SwitchState(AIState.Shooting);
		}
	}

	private void ShootingState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Vector2 toTargetDirection = base.Projectile.SafeDirectionTo(base.Target.Center);
			if (!base.Projectile.WithinRange(base.Owner.Center, 320f))
			{
				float inertia = 8f;
				base.Projectile.velocity = (base.Projectile.velocity * inertia + base.Projectile.SafeDirectionTo(base.Owner.Center) * 25f) / (inertia + 1f);
				SyncVariables();
			}
			ShootingTimer += ((!Main.rand.NextBool(30)) ? 1 : 2);
			if (ShootingTimer >= (float)CalamarisLament.ShootingFireRate && Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (toTargetDirection * (CalamarisLament.ShootingProjectileSpeed + 10f)).RotatedByRandom(0.7853981852531433), ModContent.ProjectileType<CalamarisLamentProjectile>(), base.Projectile.damage, 1f, base.Projectile.owner, base.Target.whoAmI);
				Projectile projectile = base.Projectile;
				projectile.velocity -= toTargetDirection * 3f;
				for (int i = 0; i < 15; i++)
				{
					Vector2 position = base.Projectile.Center + (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)base.Projectile.height / 2f;
					Vector2? velocity = toTargetDirection.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 7f);
					float scale = Main.rand.NextFloat(0.5f, 1.5f);
					Dust.NewDustPerfect(position, 109, velocity, 127, default(Color), scale).noGravity = true;
				}
				SoundEngine.PlaySound(in SoundID.Item111, base.Projectile.Center);
				ShootingTimer = 0f;
				SyncVariables();
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(toTargetDirection.ToRotation() - (float)Math.PI / 2f, 0.1f);
			base.Projectile.MinionAntiClump(0.5f);
			if (base.Owner.WithinRange(base.Target.Center, CalamarisLament.LatchingDistanceRequired))
			{
				SwitchState(AIState.Latching);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void LatchingState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Vector2 toTargetDirection = base.Projectile.SafeDirectionTo(base.Target.Center);
			Rectangle rect = base.Projectile.getRect();
			if (!((Rectangle)(ref rect)).Intersects(base.Target.getRect()))
			{
				float inertia = 10f;
				base.Projectile.velocity = (base.Projectile.velocity * inertia + toTargetDirection * (((Vector2)(ref base.Target.velocity)).Length() + CalamarisLament.LatchingExtraTargettingSpeed)) / (inertia + 1f);
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(toTargetDirection.ToRotation() - (float)Math.PI / 2f, 0.3f);
				base.Projectile.MinionAntiClump(0.5f);
				SyncVariables();
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.2f;
				SyncVariables();
			}
			if (!base.Owner.WithinRange(base.Target.Center, CalamarisLament.LatchingDistanceRequired))
			{
				SwitchState(AIState.Shooting);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void SwitchState(AIState state)
	{
		State = state;
		SyncVariables();
	}

	private void SyncVariables()
	{
		base.Projectile.ForceNetUpdate();
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base.IFrames = 30;
		int dustAmount = 40;
		for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
		{
			Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * 8f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 109, velocity);
			dust.noGravity = true;
			dust.noLight = true;
		}
	}

	public override bool MinionContactDamage()
	{
		return State == AIState.Latching;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 240);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= CalamarisLament.LatchingDamageMultiplier;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
