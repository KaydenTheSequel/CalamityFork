using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class SnakeEyesSummon : ModProjectile, ILocalizedModType, IModType
{
	public enum AIState
	{
		Idle,
		Targetting,
		Redirecting
	}

	public bool HasShot;

	public bool HasTeleported;

	public Vector2 RandomPosition;

	public new string LocalizationCategory => "Projectiles.Misc";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(SnakeEyes.EnemyDistanceDetection, Owner);
		}
	}

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

	public ref float EyeAngle => ref base.Projectile.localAI[0];

	public ref float EyeOutwardness => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.minionSlots = 1f;
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		EyeAngle = base.Projectile.SafeDirectionTo((Target != null) ? Target.Center : Main.MouseWorld).ToRotation();
		EyeOutwardness = Utils.Remap(base.Projectile.Distance((Target != null) ? Target.Center : Main.MouseWorld), 0f, 300f, 0f, 5f);
		if (Main.rand.NextBool(100))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.DarkCyan, Vector2.One, 0f, 0.05f, 0.4f + Main.rand.NextFloat(0.2f), 30));
		}
		switch (State)
		{
		case AIState.Idle:
			IdleState();
			break;
		case AIState.Targetting:
			TargettingState();
			break;
		case AIState.Redirecting:
			RedirectingState();
			break;
		}
	}

	private void IdleState()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		AITimer += 0.01f;
		base.Projectile.velocity.Y -= MathF.Cos(AITimer) / 350f;
		if (!base.Projectile.WithinRange(Owner.Center, 400f))
		{
			base.Projectile.Center = Owner.Center + Main.rand.NextVector2Circular(300f, 300f);
			DoVFXPulse();
		}
		if (Target != null)
		{
			SwitchAIState(AIState.Targetting);
		}
	}

	private void TargettingState()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			if (!HasTeleported)
			{
				RandomPosition = Target.Center + Main.rand.NextVector2CircularEdge(400f, 400f);
				base.Projectile.Center = RandomPosition;
				DoVFXPulse();
				HasTeleported = true;
				base.Projectile.netUpdate = true;
				return;
			}
			base.Projectile.Center = RandomPosition;
			if (!HasShot && AITimer >= SnakeEyes.TimeToShoot)
			{
				ShootProjectile();
			}
			if (HasShot)
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile proj = enumerator.Current;
					if (proj.type == ModContent.ProjectileType<SnakeEyesProjectile>() && proj.owner == Owner.whoAmI && proj.ModProjectile<SnakeEyesProjectile>().MinionID == (float)base.Projectile.whoAmI && proj.ModProjectile<SnakeEyesProjectile>().HasRedirected)
					{
						base.Projectile.Center = proj.Center;
						SwitchAIState(AIState.Redirecting);
					}
				}
			}
			if (AITimer >= SnakeEyes.TimeToShoot + 120f)
			{
				SwitchAIState(AIState.Targetting);
			}
			AITimer++;
		}
		else
		{
			SwitchAIState(AIState.Idle);
		}
	}

	private void ShootProjectile()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, Target, SnakeEyes.ProjectileSpeed), ModContent.ProjectileType<SnakeEyesProjectile>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI, base.Projectile.whoAmI, Target.whoAmI);
			DoVFXPulse();
			SoundStyle style = SoundID.Item91 with
			{
				Volume = 0.8f,
				Pitch = 0.5f,
				PitchVariance = 0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			HasShot = true;
			base.Projectile.netUpdate = true;
		}
	}

	private void RedirectingState()
	{
		if (Target != null)
		{
			if (AITimer < SnakeEyes.TimeToRestart)
			{
				AITimer++;
			}
			else
			{
				SwitchAIState(AIState.Targetting);
			}
		}
		else
		{
			SwitchAIState(AIState.Idle);
		}
	}

	private void SwitchAIState(AIState state)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		State = state;
		AITimer = 0f;
		HasTeleported = false;
		HasShot = false;
		RandomPosition = Vector2.Zero;
		base.Projectile.velocity = Vector2.Zero;
		DoVFXPulse(state == AIState.Redirecting);
		base.Projectile.netUpdate = true;
	}

	private void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<SnakeEyesBuff>(), 2);
		if (base.Type == ModContent.ProjectileType<SnakeEyesSummon>())
		{
			if (Owner.dead)
			{
				ModdedOwner.snakeEyes = false;
			}
			if (ModdedOwner.snakeEyes)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	private void DoVFXPulse(bool empowered = false)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		int dustAmount = 40;
		for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
		{
			Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * 9f;
			Vector2 center = base.Projectile.Center;
			int type = (empowered ? 261 : 226);
			Vector2? velocity2 = velocity;
			float scale = (empowered ? 2f : 0.5f);
			Dust.NewDustPerfect(center, type, velocity2, 0, default(Color), scale).noGravity = true;
		}
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, empowered ? Color.White : (Color.DarkCyan * 1.2f), Vector2.One, 0f, 0.05f, 0.5f + Main.rand.NextFloat(0.2f), 20));
	}

	public override void OnSpawn(IEntitySource source)
	{
		DoVFXPulse();
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
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		Texture2D eyeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/SnakeEye", (AssetRequestMode)2).Value;
		Vector2 eyeDrawPosition = base.Projectile.Center - Main.screenPosition + EyeAngle.ToRotationVector2() * EyeOutwardness;
		Main.EntitySpriteDraw(eyeTexture, eyeDrawPosition, null, Color.White, 0f, eyeTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		return false;
	}
}
