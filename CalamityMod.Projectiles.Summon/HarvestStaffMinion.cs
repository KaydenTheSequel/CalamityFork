using System;
using System.IO;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HarvestStaffMinion : ModProjectile, ILocalizedModType, IModType
{
	public enum AIState
	{
		Still,
		Idle,
		Attack
	}

	public enum AnimationState
	{
		None = -1,
		Grow,
		Rise,
		Idle,
		Run,
		Jump
	}

	public static SoundStyle GrowSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinEmerge", 3);

	public static SoundStyle IdleSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinIdle", 4);

	public static SoundStyle IdleRareSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinRareIdle")
	{
		Volume = 0.2f
	};

	public static SoundStyle ScreamSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinScream", 2)
	{
		Volume = 0.5f
	};

	public static SoundStyle JumpSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinJump");

	public static SoundStyle BoomSound = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinExplode", 2)
	{
		Volume = 0.6f
	};

	public static SoundStyle BoomSoundGFB = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinExplodeGFB", 2);

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Variant => ref base.Projectile.ai[0];

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[1];
		}
		set
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			base.Projectile.ai[1] = (float)value;
			switch (value)
			{
			case AIState.Attack:
				Animation = AnimationState.Run;
				SoundEngine.PlaySound(in ScreamSound, base.Projectile.Center);
				break;
			case AIState.Idle:
				Animation = AnimationState.Idle;
				break;
			}
		}
	}

	public int IdleWalkingTime { get; set; }

	public int IdleWalkingTimer { get; set; }

	public int IdleWalkingDirection { get; set; }

	public int IdleJumpCount { get; set; }

	public int IdleJumpCooldown { get; set; }

	public AnimationState Animation
	{
		get
		{
			return (AnimationState)base.Projectile.ai[2];
		}
		set
		{
			if (value != Animation)
			{
				base.Projectile.frame = 0;
				base.Projectile.frameCounter = 0;
			}
			base.Projectile.ai[2] = (float)value;
			switch (value)
			{
			case AnimationState.Grow:
				AnimationFrames = ((Variant == 0f) ? 6 : 4);
				FramesUntilNextAnimationFrame = 6;
				break;
			case AnimationState.Rise:
				AnimationFrames = 10;
				FramesUntilNextAnimationFrame = 5;
				break;
			case AnimationState.Idle:
			case AnimationState.Jump:
				AnimationFrames = 1;
				FramesUntilNextAnimationFrame = 0;
				break;
			case AnimationState.Run:
				AnimationFrames = 6;
				FramesUntilNextAnimationFrame = 5;
				break;
			}
		}
	}

	public int AnimationFrames { get; set; }

	public int FramesUntilNextAnimationFrame { get; set; } = 1;

	public bool CompletedAnimation => base.Projectile.frame >= AnimationFrames - 1;

	public int Direction
	{
		get
		{
			return base.Projectile.spriteDirection;
		}
		set
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = value);
		}
	}

	public Player Owner { get; set; }

	public Player AnyPlayer
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			for (int i = 0; i < 255; i++)
			{
				Player p = Main.player[i];
				if (p != null && p.active && !p.dead && base.Projectile.Center.WithinRange(p.Center, 64f))
				{
					return p;
				}
			}
			return null;
		}
	}

	public NPC Target { get; set; }

	public Projectile MySentry
	{
		get
		{
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && proj.owner == base.Projectile.owner && proj.type == ModContent.ProjectileType<HarvestStaffSentry>())
				{
					return proj;
				}
			}
			return null;
		}
	}

	public override void AI()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		base.Projectile.width = (base.Projectile.height = ((Variant == 0f) ? 28 : ((Variant == 1f) ? 22 : 20)));
		if (Animation == AnimationState.Grow && (MySentry == null || base.Projectile.Distance(MySentry.Center) > 600f) && Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.Kill();
			return;
		}
		Target = base.Projectile.Center.MinionHoming((State == AIState.Still) ? HarvestStaff.PlantedEnemyDistanceDetection : HarvestStaff.NormalEnemyDistanceDetection, Owner, ignoreTiles: false);
		switch (State)
		{
		case AIState.Still:
			StillState();
			break;
		case AIState.Idle:
			IdleState();
			break;
		case AIState.Attack:
			AttackState();
			break;
		}
		if (IdleJumpCooldown > 0)
		{
			IdleJumpCooldown--;
		}
		base.Projectile.timeLeft = 2;
		DoGravity();
		DoAnimation();
		base.Projectile.ForceNetUpdate();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (Animation == AnimationState.None)
		{
			Animation = AnimationState.Grow;
		}
		if (State == AIState.Idle && base.Projectile.velocity.Y == 0f)
		{
			if (AnyPlayer != null && base.Projectile.WithinRange(AnyPlayer.Center, 64f) && IdleJumpCooldown == 0)
			{
				NearOwnerJump();
			}
			else
			{
				Animation = (((float)IdleWalkingTimer == 0f) ? AnimationState.Idle : AnimationState.Run);
			}
		}
		if (Target != null && State == AIState.Attack && base.Projectile.velocity.Y == 0f)
		{
			if (MathF.Abs(Target.Center.X - base.Projectile.Center.X) < 160f && Target.Top.Y < base.Projectile.Bottom.Y)
			{
				if (PlatformBetweenMinionAndTarget(out var platformPosition))
				{
					JumpTowards(platformPosition - Vector2.UnitY * 32f);
				}
				else
				{
					JumpTowards(Target.Top);
				}
			}
			else
			{
				Animation = AnimationState.Run;
			}
		}
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		fallThrough = Target != null && base.Projectile.Bottom.Y < Target.Top.Y;
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		if (State != AIState.Attack)
		{
			return;
		}
		base.Projectile.ExpandHitboxBy(4f);
		if (Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.Damage();
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int i = 0; i < (int)Utils.Remap(Variant, 0f, 2f, 4f, 2f); i++)
		{
			Vector2 velocity = ((float)Math.PI / 2f * (float)i).ToRotationVector2() * Main.rand.NextFloat(3f, 5f);
			Gore.NewGoreDirect(base.Projectile.GetSource_Death(), base.Projectile.Center, velocity, base.Mod.Find<ModGore>($"PumpkinGore{Main.rand.Next(6) + 1}").Type, Utils.Remap(Variant, 0f, 2f, 1f, 0.5f)).timeLeft = 15;
		}
		for (int j = 0; j < 20; j++)
		{
			if (BirthdayParty.PartyIsUp)
			{
				Dust.NewDustPerfect(base.Projectile.Center, Main.rand.Next(139, 143), Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f) + new Vector2(0f, -0.75f)).scale = 0.8f;
				continue;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 278 : 51, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f) + new Vector2(0f, -0.75f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.8f, 1.4f);
			dust.color = Color.Chocolate;
		}
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in BoomSoundGFB, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in BoomSound, base.Projectile.Center);
		}
	}

	public void StillState()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			goto IL_0030;
		}
		if (AnyPlayer != null)
		{
			Rectangle rect = base.Projectile.getRect();
			if (((Rectangle)(ref rect)).Intersects(AnyPlayer.getRect()))
			{
				goto IL_0030;
			}
		}
		goto IL_0064;
		IL_0030:
		if (Animation == AnimationState.Grow && CompletedAnimation)
		{
			Animation = AnimationState.Rise;
			SoundEngine.PlaySound(in GrowSound, base.Projectile.Center);
			return;
		}
		goto IL_0064;
		IL_0064:
		if (Animation == AnimationState.Rise && CompletedAnimation)
		{
			State = AIState.Idle;
		}
	}

	public void IdleState()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			State = AIState.Attack;
			return;
		}
		if ((float)IdleWalkingTimer == 0f && Main.rand.NextBool(400))
		{
			int idleWalkingTime = (IdleWalkingTimer = Main.rand.Next(60, 180));
			IdleWalkingTime = idleWalkingTime;
			IdleWalkingDirection = ((MySentry != null && !base.Projectile.WithinRange(MySentry.Center, 960f)) ? MathF.Sign(MySentry.Center.X - base.Projectile.Center.X) : ((!Main.rand.NextBool()) ? 1 : (-1)));
		}
		else if ((float)IdleWalkingTimer != 0f)
		{
			base.Projectile.velocity.X = MathHelper.Lerp(0f, 3f, CalamityUtils.Convert01To010(Utils.GetLerpValue(0f, IdleWalkingTime, IdleWalkingTimer))) * (float)IdleWalkingDirection;
			Direction = MathF.Sign(base.Projectile.velocity.X);
			IdleWalkingTimer--;
			if ((float)IdleWalkingTimer == 0f)
			{
				base.Projectile.velocity.X = 0f;
				Animation = AnimationState.Idle;
			}
		}
		Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
		if (Main.rand.NextBool(700))
		{
			SoundEngine.PlaySound(Main.rand.NextBool(20) ? IdleRareSound : IdleSound, base.Projectile.Center);
		}
	}

	public void AttackState()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			MoveToTarget();
			Direction = MathF.Sign(base.Projectile.velocity.X);
			Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
			if (!Main.dedServ)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + new Vector2(0f, (float)(-base.Projectile.height) * 0.5f), 6, Utils.RotatedBy(new Vector2(0f, -4f), (double)(0.7f * (float)(-base.Projectile.direction)), default(Vector2)) * Main.rand.NextFloat(0.1f, 0.8f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.4f, 0.95f);
			}
		}
		else
		{
			base.Projectile.velocity.X = 0f;
			State = AIState.Idle;
		}
	}

	public void NearOwnerJump()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y = -8f;
		IdleJumpCount++;
		if (IdleJumpCount == 2)
		{
			IdleJumpCount = 0;
			IdleJumpCooldown = 30;
		}
		Direction = MathF.Sign(AnyPlayer.Center.X - base.Projectile.Center.X);
		Animation = AnimationState.Jump;
		SoundEngine.PlaySound(JumpSound with
		{
			Pitch = Utils.Remap(Variant, 0f, 2f, -0.3f, 0.3f)
		}, base.Projectile.Center);
	}

	public void MoveToTarget()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		float maxVelocity = Utils.Remap(Variant, 0f, 2f, 5f, 8f);
		float acceleration = Utils.Remap(Variant, 0f, 2f, 0.1f, 0.3f);
		float accelerationDirection = MathF.Sign(Target.Center.X - base.Projectile.Center.X);
		base.Projectile.velocity.X += acceleration * accelerationDirection;
		if (MathF.Abs(base.Projectile.velocity.X) > maxVelocity)
		{
			base.Projectile.velocity.X = maxVelocity * accelerationDirection;
		}
	}

	public void JumpTowards(Vector2 destination)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y = 0f - MathF.Sqrt(-2f * HarvestStaff.PumpkinGravityStrength * (destination.Y - base.Projectile.Bottom.Y));
		Animation = AnimationState.Jump;
		SoundStyle style = JumpSound with
		{
			Pitch = Utils.Remap(Variant, 0f, 2f, -0.3f, 0.3f)
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public bool PlatformBetweenMinionAndTarget(out Vector2 tilePosition)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Point val = base.Projectile.Center.ToSafeTileCoordinates();
		Point targetPosition = Target.Center.ToSafeTileCoordinates();
		for (int coordY = val.Y; coordY > targetPosition.Y; coordY--)
		{
			if (Main.tile[targetPosition.X, coordY].IsTileSolidGround())
			{
				tilePosition = new Vector2((float)targetPosition.X, (float)coordY) * 16f;
				return true;
			}
		}
		tilePosition = Vector2.Zero;
		return false;
	}

	public void DoGravity()
	{
		float speed = base.Projectile.velocity.Y;
		if (speed < HarvestStaff.PumpkinMaxGravity)
		{
			speed = MathF.Min(speed + HarvestStaff.PumpkinGravityStrength, HarvestStaff.PumpkinMaxGravity);
		}
		base.Projectile.velocity.Y = speed;
	}

	public void DoAnimation()
	{
		if (Animation == AnimationState.None || Animation == AnimationState.Idle || Animation == AnimationState.Jump)
		{
			return;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= FramesUntilNextAnimationFrame)
		{
			base.Projectile.frame = Math.Min(base.Projectile.frame + 1, AnimationFrames - 1);
			base.Projectile.frameCounter = 0;
			if (Animation == AnimationState.Run && CompletedAnimation)
			{
				base.Projectile.frame = 0;
			}
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 68;
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(Direction);
		writer.Write7BitEncodedInt(IdleWalkingTime);
		writer.Write7BitEncodedInt(IdleWalkingTimer);
		writer.Write7BitEncodedInt(IdleJumpCount);
		writer.Write7BitEncodedInt(IdleJumpCooldown);
		writer.Write7BitEncodedInt(IdleWalkingDirection);
		writer.Write7BitEncodedInt(AnimationFrames);
		writer.Write7BitEncodedInt(FramesUntilNextAnimationFrame);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Direction = reader.Read7BitEncodedInt();
		IdleWalkingTime = reader.Read7BitEncodedInt();
		IdleWalkingTimer = reader.Read7BitEncodedInt();
		IdleJumpCount = reader.Read7BitEncodedInt();
		IdleJumpCooldown = reader.Read7BitEncodedInt();
		IdleWalkingDirection = reader.Read7BitEncodedInt();
		AnimationFrames = reader.Read7BitEncodedInt();
		FramesUntilNextAnimationFrame = reader.Read7BitEncodedInt();
	}

	public override bool? CanDamage()
	{
		if (State != AIState.Attack)
		{
			return false;
		}
		return null;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= Utils.Remap(Variant, 0f, 2f, 1.5f, 0.5f);
	}

	public override void OnSpawn(IEntitySource source)
	{
		Direction = ((!Main.rand.NextBool()) ? 1 : (-1));
		Animation = AnimationState.None;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (Animation == AnimationState.None)
		{
			return false;
		}
		Asset<Texture2D> obj = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Vector2 drawPosition = base.Projectile.Bottom - Vector2.UnitY * (24f + base.Projectile.gfxOffY) - Main.screenPosition;
		Rectangle frame = obj.Frame(15, 10, (int)((int)Variant * 5 + Animation), base.Projectile.frame);
		Main.EntitySpriteDraw(color: base.Projectile.GetAlpha(lightColor), origin: frame.Size() * 0.5f, effects: (SpriteEffects)(Direction == -1), texture: obj.Value, position: drawPosition, sourceRectangle: frame, rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}
}
