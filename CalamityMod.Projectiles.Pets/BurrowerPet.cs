using System;
using System.Collections.Generic;
using CalamityMod.Effects;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Deconstructors;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Systems;
using CalamityMod.Systems.Mechanic;
using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class BurrowerPet : BaseWormProjectile, ILocalizedModType, IModType
{
	public enum AttackState
	{
		Idle,
		Mining,
		Shocked
	}

	public Vector2 TargetVector;

	public Vector2 SecondaryVector;

	public float StoredValue;

	public override string Texture => "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Head";

	public override List<string> SegmentTextures => new List<string> { "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Body", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt1", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt2", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1Tail" };

	public override List<string?> GlowTextures => new List<string> { null, "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyGlow", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt1Glow", "CalamityMod/NPCs/Deconstructors/DeconstructorMK1BodyAlt2Glow" };

	public override int SegmentCount => 3;

	public override List<float> SegmentTypePositionOffsets => new List<float> { 32f, 32f, 32f, 32f, 32f };

	public new string LocalizationCategory => "Projectiles.Pets";

	public Player Owner => Main.player[base.Projectile.owner];

	public AttackState ActiveAttackState
	{
		get
		{
			return (AttackState)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = (float)value;
		}
	}

	public float MainTimer
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float AttackSubstate
	{
		get
		{
			return base.Projectile.ai[2];
		}
		set
		{
			base.Projectile.ai[2] = value;
		}
	}

	public float StateChangeCounter
	{
		get
		{
			return base.Projectile.ai[3];
		}
		set
		{
			base.Projectile.ai[3] = value;
		}
	}

	public float VelocityRotation
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity.ToRotation();
		}
		set
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			base.Projectile.velocity = value.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 1600;
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		for (int i = 0; i < SegmentCount - 1; i++)
		{
			Segments.Add(new BaseWormSegment(this, i % 3));
		}
		Segments.Add(new BaseWormSegment(this, 3));
	}

	public bool VerifyOwnerIsPresent()
	{
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return true;
		}
		if (Owner.dead)
		{
			Owner.Calamity().burrowerPet = false;
		}
		if (Owner.Calamity().burrowerPet)
		{
			base.Projectile.timeLeft = 2;
		}
		return false;
	}

	public void SwitchAttackState(AttackState State, float Substate = 0f, bool resetVector = true)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.netUpdate = true;
		ActiveAttackState = State;
		AttackSubstate = Substate;
		MainTimer = 0f;
		if (resetVector)
		{
			TargetVector = Vector2.Zero;
		}
	}

	public override void AI()
	{
		if (!VerifyOwnerIsPresent())
		{
			HandleAIStates();
			MainTimer++;
			UpdateSegments();
		}
	}

	private void LowerTargetToGround()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Point pointToCheck = TargetVector.ToTileCoordinates();
		for (int i = 0; i < 50; i++)
		{
			if (pointToCheck.X < 0)
			{
				break;
			}
			if (pointToCheck.X >= Main.maxTilesX)
			{
				break;
			}
			if (pointToCheck.Y < 0)
			{
				break;
			}
			if (pointToCheck.Y >= Main.maxTilesY)
			{
				break;
			}
			Tile targetTile = Main.tile[pointToCheck];
			if (targetTile == null || !targetTile.HasTile || !targetTile.IsTileSolidGround())
			{
				pointToCheck.Y++;
				continue;
			}
			TargetVector = pointToCheck.ToWorldCoordinates() - new Vector2(0f, 16f);
			break;
		}
	}

	public void HandleAIStates()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		SegmentMaxRotation = 0.65f;
		SegmentRigidity = 0.2f;
		base.Projectile.Distance(player.Center);
		bool onScreen = true;
		onScreen = ((Main.netMode != 0) ? Collision.CheckAABBvAABBCollision(base.Projectile.position, base.Projectile.Size, player.Center - new Vector2(960f, 600f), new Vector2(1920f, 1200f)) : Collision.CheckAABBvAABBCollision(base.Projectile.position, base.Projectile.Size, Main.screenPosition, Main.ScreenSize.ToVector2()));
		bool noGravity = !onScreen || base.Projectile.wet || Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height, acceptTopSurfaces: true);
		switch (ActiveAttackState)
		{
		case AttackState.Idle:
		{
			if (TargetVector == Vector2.Zero || MainTimer > 300f || (MainTimer > 120f && base.Projectile.Distance(TargetVector) < 64f))
			{
				if (Main.rand.NextBool())
				{
					List<List<Point>> veins = Burrower.FindOreVeins(base.Projectile.Center.ToTileCoordinates());
					while (veins.Count > 0)
					{
						List<Point> targetVein = veins[Main.rand.Next(veins.Count)];
						(Point, Point)? foundTarget = Burrower.FindTargetFromVein(targetVein);
						if (foundTarget.HasValue)
						{
							TargetVector = foundTarget.Value.Item1.ToWorldCoordinates();
							SecondaryVector = foundTarget.Value.Item2.ToWorldCoordinates();
							if (base.Projectile.Distance(TargetVector) > 160f)
							{
								GeneralParticleHandler.SpawnParticle(new EmoteExpressionParticle(base.Projectile.Top, -Vector2.UnitY * 5f, 2f, ArsenalEffects.ArsenalGaussColor, 60, EmoteExpressionParticle.EmoteType.Exclamation));
							}
							SwitchAttackState(AttackState.Mining, 0f, resetVector: false);
							return;
						}
						veins.Remove(targetVein);
					}
				}
				TargetVector = player.Center;
				LowerTargetToGround();
				MainTimer = 0f;
			}
			float playerDistance = base.Projectile.Distance(Owner.Center);
			float speed = 0.06f;
			if (playerDistance > (float)150.TilesToPixels())
			{
				base.Projectile.Center = Owner.Center - base.Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 200f;
				base.Projectile.velocity = base.Projectile.velocity.ClampMagnitude(1f, 8f);
				break;
			}
			if (playerDistance < 600f && !noGravity)
			{
				base.Projectile.velocity.Y += 0.5f;
			}
			if (playerDistance > 600f)
			{
				speed = 0.5f;
			}
			if (playerDistance > 200f)
			{
				speed = 0.4f;
			}
			else if (playerDistance > 140f)
			{
				speed = 0.2f;
			}
			if (playerDistance > 100f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity += base.Projectile.DirectionTo(Owner.Center) * speed;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() > 2f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.9f;
			}
			base.Projectile.velocity = base.Projectile.velocity.ClampMagnitude(0f, 16f);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			break;
		}
		case AttackState.Mining:
		{
			Projectile projectile4 = base.Projectile;
			projectile4.velocity += base.Projectile.DirectionTo(SecondaryVector).SafeNormalize(Vector2.UnitY);
			Projectile projectile5 = base.Projectile;
			projectile5.velocity *= 0.9f;
			if (MainTimer > 600f)
			{
				SwitchAttackState(AttackState.Idle);
			}
			if (base.Projectile.Distance(SecondaryVector) < 4f)
			{
				Vector2 dir = SecondaryVector.DirectionTo(TargetVector);
				if (Main.tile[TargetVector.ToTileCoordinates()].TileType == ModContent.TileType<AuricOre>())
				{
					base.Projectile.velocity = -base.Projectile.DirectionTo(TargetVector) * 16f;
					base.Projectile.Center = SecondaryVector + base.Projectile.velocity;
					base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
					SwitchAttackState(AttackState.Shocked, 300f);
					AuricOre.Animate = true;
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/TeslaShoot1"), base.Projectile.Center);
					break;
				}
				SegmentRigidity = 0f;
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.rotation = SecondaryVector.DirectionTo(TargetVector).ToRotation() + (float)Math.PI / 2f;
				if (Main.netMode != 2 && !BurrowerPingTileEffect.Instance.Active)
				{
					TilePingerSystem.AddPing(BurrowerPingTileEffect.Instance, base.Projectile.Center, player);
				}
				for (int i = 0; i < 1; i++)
				{
					int sparkLifetime = Main.rand.Next(10, 20);
					float sparkScale = Main.rand.NextFloat(0.8f, 1f);
					Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
					sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());
					if (Main.rand.NextBool(10))
					{
						sparkScale *= 2f;
					}
					Vector2 sparkVelocity = dir.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(6f, 16f);
					GeneralParticleHandler.SpawnParticle(new SparkParticle((TargetVector + SecondaryVector) * 0.5f, -sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
					if (MainTimer < 520f)
					{
						MainTimer = 520f;
					}
				}
				SoundEngine.PlaySound(SoundID.NPCHit18 with
				{
					Volume = 0.2f
				}, base.Projectile.Center);
			}
			else
			{
				base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
			}
			break;
		}
		case AttackState.Shocked:
			if (AttackSubstate > 0f)
			{
				AttackSubstate--;
				if (noGravity)
				{
					SegmentRigidity = 0f;
					if (AttackSubstate < 295f)
					{
						Projectile projectile = base.Projectile;
						projectile.velocity *= 0.55f;
					}
					{
						foreach (BaseWormSegment item in Segments)
						{
							if (!Collision.SolidCollision(item.Center - new Vector2(19f, 17f), 38, 38, acceptTopSurfaces: true))
							{
								item.Center.Y += 2f;
							}
						}
						break;
					}
				}
				base.Projectile.velocity.Y += 0.5f;
				base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
			}
			else
			{
				SwitchAttackState(AttackState.Idle);
			}
			break;
		}
	}

	public BurrowerPet()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		TargetVector = Vector2.Zero;
		SecondaryVector = Vector2.Zero;
		base._002Ector();
	}
}
