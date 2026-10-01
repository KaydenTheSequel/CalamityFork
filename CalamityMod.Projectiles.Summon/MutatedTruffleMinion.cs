using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

[LegacyName(new string[] { "YoungDuke" })]
public class MutatedTruffleMinion : BaseMinionProjectile
{
	public enum AIState
	{
		Idle,
		Dashing,
		Toothball,
		Vortex
	}

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<MutatedTruffleMinion>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<MutatedTruffleBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.MutatedTruffleBool;

	public override float MinionSlots => 3f;

	public override float EnemyDistanceDetection => MutatedTruffle.EnemyDistanceDetection;

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public ref float AITimer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[base.Type] = 16;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = (base.Projectile.height = 82);
	}

	public override void MinionAI()
	{
		switch (State)
		{
		case AIState.Idle:
			IdleState();
			break;
		case AIState.Dashing:
			DashingState();
			break;
		case AIState.Toothball:
			ToothballState();
			break;
		case AIState.Vortex:
			VortexState();
			break;
		}
	}

	public override void DoAnimation()
	{
		base.DoAnimation();
		if (State != AIState.Idle)
		{
			if (base.Projectile.frame <= 7)
			{
				base.Projectile.frame = 8;
			}
		}
		else if (base.Projectile.frame >= 7)
		{
			base.Projectile.frame = 0;
		}
	}

	private void IdleState()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.WithinRange(base.Owner.Center, 128f))
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.875f;
			SyncVariables();
		}
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
		base.Projectile.spriteDirection = MathF.Sign(base.Projectile.velocity.X);
		if (base.Target != null)
		{
			SwitchState(AIState.Dashing);
		}
	}

	private void DashingState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			if (!base.Projectile.WithinRange(base.Target.Center, 480f))
			{
				float inertia = 4f;
				base.Projectile.velocity = (base.Projectile.velocity * inertia + CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, base.Target, MutatedTruffle.DashSpeed)) / (inertia + 1f);
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
				base.Projectile.spriteDirection = MathF.Sign(base.Target.Center.X - base.Projectile.Center.X);
				SyncVariables();
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < MutatedTruffle.DashSpeed - 15f)
			{
				base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, base.Target, MutatedTruffle.DashSpeed - 10f);
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
				base.Projectile.spriteDirection = MathF.Sign(base.Target.Center.X - base.Projectile.Center.X);
				SyncVariables();
			}
			AITimer++;
			if (AITimer > (float)MutatedTruffle.DashTime)
			{
				SwitchState(AIState.Toothball);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void ToothballState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Vector2 targetDirection = base.Projectile.SafeDirectionTo(base.Target.Center);
			float targettingSpeed = 30f;
			float inertia = 18f;
			if (!base.Projectile.WithinRange(base.Target.Center, 480f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * inertia + targetDirection * targettingSpeed) / (inertia + 1f);
				SyncVariables();
			}
			else if (base.Projectile.WithinRange(base.Target.Center, 400f))
			{
				base.Projectile.velocity = (base.Projectile.velocity * inertia + -targetDirection * targettingSpeed) / (inertia + 1f);
				SyncVariables();
			}
			if (AITimer % (float)MutatedTruffle.ToothballFireRate == 0f)
			{
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, base.Target, MutatedTruffle.ToothballSpeed), ModContent.ProjectileType<MutatedTruffleToothball>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Target.whoAmI);
				}
				if (!Main.dedServ)
				{
					Dust.NewDustPerfect(base.Projectile.Right.RotatedBy(base.Projectile.rotation), 7, base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.39269909262657166) * Main.rand.NextFloat(1f, 3f));
					SoundStyle style = SoundID.NPCDeath13 with
					{
						Volume = 0.5f,
						PitchVariance = 0.1f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				SyncVariables();
			}
			AITimer++;
			base.Projectile.rotation = base.Projectile.SafeDirectionTo(base.Target.Center).ToRotation();
			base.Projectile.spriteDirection = MathF.Sign(base.Target.Center.X - base.Projectile.Center.X);
			if (AITimer > (float)(MutatedTruffle.ToothballsUntilNextState * MutatedTruffle.ToothballFireRate))
			{
				SwitchState(AIState.Vortex);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void VortexState()
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.type == ModContent.ProjectileType<MutatedTruffleVortex>() && proj.owner == base.Owner.whoAmI && proj.timeLeft >= MutatedTruffle.VortexTimeUntilNextState)
				{
					Vector2 spinPosition = proj.Center + -Vector2.UnitY.RotatedBy(Main.GlobalTimeWrappedHourly * 6f) * 400f;
					base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(spinPosition) * 50f, 0.2f);
				}
			}
			AITimer++;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			if (AITimer >= (float)MutatedTruffle.VortexTimeUntilNextState)
			{
				SwitchState(AIState.Dashing);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void SwitchState(AIState state)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		State = state;
		AITimer = 0f;
		base.Projectile.spriteDirection = 1;
		if (state == AIState.Idle)
		{
			base.Projectile.rotation = 0f;
			if (!Main.dedServ)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff");
				style.Volume = 0.5f;
				style.Pitch = 0.1f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		if ((state == AIState.Dashing || state == AIState.Toothball) && !Main.dedServ)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeRoar");
			style.Volume = 0.3f;
			style.Pitch = 0.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (state == AIState.Vortex)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Target.Center, Vector2.Zero, ModContent.ProjectileType<MutatedTruffleVortex>(), base.Projectile.damage, base.Projectile.knockBack, base.Owner.whoAmI);
			}
			if (!Main.dedServ)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVomit");
				style.Volume = 0.4f;
				style.Pitch = 0.1f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		SyncVariables();
	}

	private void SyncVariables()
	{
		base.Projectile.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}

	public override bool MinionContactDamage()
	{
		return State == AIState.Dashing;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 1.25f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1 && State != AIState.Idle) ? ((float)Math.PI) : 0f);
		SpriteEffects effects = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		if (CalamityClientConfig.Instance.Afterimages && (State == AIState.Dashing || State == AIState.Vortex))
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Color green = Color.Green;
				((Color)(ref green)).A = 25;
				Color afterimageDrawColor = green * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, drawRotation, origin, base.Projectile.scale, effects);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, origin, base.Projectile.scale, effects);
		return false;
	}
}
