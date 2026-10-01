using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CalamityMod.DataStructures;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class WulfrumPackPlayer : ModPlayer
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_QuickGrapple _003C0_003E__QuickGrapple_UseCustomGrapple;
	}

	public bool WulfrumPackEquipped;

	public Item PackItem;

	public int Grapple = -1;

	public float SwingLength;

	public int hookCache = -1;

	public int hookCooldown;

	public Vector2 CurrentPosition;

	public Vector2 OldPosition;

	public List<VerletSimulatedSegment> Segments;

	public const float BaseMaxLength = 600f;

	public const float BaseLaunchVelocity = 17f;

	public const float BaseReturnVelocity = 5f;

	public const float HookSpeedFactor = 0.5f;

	public const float ReelSpeed = 4f;

	public const float StaticHookReelSpeed = 6f;

	public const float AntiGravSwingModifier = 2f;

	public const int SimulationResolution = 5;

	public const int HookUpdates = 3;

	public const float MaxHopVelocity = 3f;

	public const int SafetySteps = 3;

	public const float SafetyHookAngle = 1.8849558f;

	public const float SafetyHookAngleResolution = 50f;

	public bool AutoGrappleActivated
	{
		get
		{
			if (!WulfrumPackEquipped || Grappled || base.Player.noFallDmg || base.Player.equippedWings != null || base.Player.controlDown || base.Player.velocity.Y * base.Player.gravDir < 0f || (base.Player.fallStart >= (int)(base.Player.position.Y / 16f) && base.Player.gravDir > 0f) || (base.Player.fallStart > (int)(base.Player.position.Y / 16f) && base.Player.gravDir < 0f) || base.Player.mount.Active || base.Player.webbed || base.Player.stoned || base.Player.frozen || base.Player.vortexDebuff)
			{
				return false;
			}
			return true;
		}
	}

	public bool Grappled
	{
		get
		{
			if (WulfrumPackEquipped && Grapple > -1 && Main.projectile[Grapple].active && Main.projectile[Grapple].ModProjectile is WulfrumHook hook)
			{
				return hook.State == WulfrumHook.HookState.Grappling;
			}
			return false;
		}
	}

	public bool GrappleMovementDisabled
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			if (!Grappled)
			{
				return false;
			}
			if (!PlayerOnGround || !obeyGravity)
			{
				return false;
			}
			Vector2 val = base.Player.Center - Main.projectile[Grapple].Center;
			if (((Vector2)(ref val)).Length() > SwingLength)
			{
				return false;
			}
			return true;
		}
	}

	public bool obeyGravity => base.Player.miscEquips[4].type != 2800;

	public bool strongerReel => base.Player.miscEquips[4].type == 3623;

	public float ActualLaunchVelocity
	{
		get
		{
			float hookSpeed = 0f;
			Item hook = base.Player.miscEquips[4];
			if (!hook.IsAir)
			{
				hookSpeed = hook.shootSpeed;
			}
			return 17f + (-11.5f + hookSpeed) * 0.5f;
		}
	}

	public float HookSpeedRatio => ActualLaunchVelocity / 17f;

	public float ActualMaxLength => 600f * HookSpeedRatio;

	public float ActualReturnVelocity => 5f * HookSpeedRatio;

	public bool PlayerOnGround
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			return Collision.SolidCollision(base.Player.position + Vector2.UnitY * 2f * base.Player.gravDir, base.Player.width, base.Player.height, acceptTopSurfaces: false);
		}
	}

	public override void Load()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		base.Load();
		object obj = _003C_003EO._003C0_003E__QuickGrapple_UseCustomGrapple;
		if (obj == null)
		{
			hook_QuickGrapple val = QuickGrapple_UseCustomGrapple;
			_003C_003EO._003C0_003E__QuickGrapple_UseCustomGrapple = val;
			obj = (object)val;
		}
		On_Player.QuickGrapple += (hook_QuickGrapple)obj;
	}

	private static void QuickGrapple_UseCustomGrapple(orig_QuickGrapple orig, Player self)
	{
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		if (!self.TryGetModPlayer<WulfrumPackPlayer>(out var mp) || !mp.WulfrumPackEquipped)
		{
			orig.Invoke(self);
		}
		else
		{
			if (mp.hookCooldown > 0 || self.frozen || self.tongued || self.webbed || self.stoned || self.dead)
			{
				return;
			}
			if (PlayerInput.GrappleAndInteractAreShared)
			{
				if (Main.HoveringOverAnNPC || Main.SmartInteractShowingGenuine || Main.SmartInteractShowingFake || (WiresUI.Settings.DrawToolModeUI && PlayerInput.UsingGamepad))
				{
					return;
				}
				bool controlUseTile = self.controlUseTile;
				bool ctrlItem = self.controlUseItem;
				if (!controlUseTile && !ctrlItem)
				{
					return;
				}
				Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
				bool flag = tile.HasTile;
				if (flag)
				{
					bool flag2;
					switch (tile.TileType)
					{
					case 4:
					case 33:
					case 49:
					case 174:
					case 372:
					case 646:
						flag2 = true;
						break;
					default:
						flag2 = false;
						break;
					}
					flag = flag2;
				}
				if (flag || (self.HeldItem.type == 3384 && PlayerInput.UsingGamepad))
				{
					return;
				}
			}
			if (self.noItems)
			{
				return;
			}
			if (self.mount.Active)
			{
				self.mount.Dismount(self);
			}
			self.UpdateBlacklistedTilesForGrappling();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.owner == self.whoAmI && proj.aiStyle == 7 && proj.type != ModContent.ProjectileType<WulfrumHook>())
				{
					proj.Kill();
				}
			}
			if (Main.projectile.Count((Projectile n) => n.active && n.owner == self.whoAmI && n.type == ModContent.ProjectileType<WulfrumHook>()) <= 1)
			{
				SoundEngine.PlaySound(in WulfrumAcrobaticsPack.ShootSound, self.Center);
				Vector2 velocity = (Main.MouseWorld - self.Center).SafeNormalize(Vector2.One) * mp.ActualLaunchVelocity;
				Projectile.NewProjectile(self.GetSource_ItemUse(mp.PackItem), self.Center, velocity, ModContent.ProjectileType<WulfrumHook>(), 0, 0f, self.whoAmI);
				float angleToRightBelow = velocity.AngleBetween(Vector2.UnitY);
				if (angleToRightBelow < (float)Math.PI / 2f)
				{
					int extraCooldown = (int)(Utils.GetLerpValue((float)Math.PI / 2f, 0f, angleToRightBelow) * 15f);
					mp.hookCooldown = 15 + extraCooldown;
				}
			}
		}
	}

	public override void ResetEffects()
	{
		WulfrumPackEquipped = false;
		PackItem = null;
		if (Grapple >= 0)
		{
			base.Player.GoingDownWithGrapple = true;
		}
		if (hookCooldown > 0)
		{
			hookCooldown--;
		}
	}

	public void SetSegments(Vector2 endPoint)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (Segments == null)
		{
			Segments = new List<VerletSimulatedSegment>();
		}
		Segments.Clear();
		for (int i = 0; i <= 5; i++)
		{
			float progress = (float)i / 5f;
			VerletSimulatedSegment segment = new VerletSimulatedSegment(Vector2.Lerp(endPoint, base.Player.Center, progress));
			if (i == 0)
			{
				segment.locked = true;
			}
			if (i == 5)
			{
				segment.oldPosition = base.Player.oldPosition + new Vector2((float)base.Player.width, (float)base.Player.height) * 0.5f;
			}
			Segments.Add(segment);
		}
	}

	public override void PostUpdateRunSpeeds()
	{
		if (hookCache != -1)
		{
			base.Player.grappling[0] = hookCache;
			base.Player.grapCount = 1;
		}
		hookCache = -1;
	}

	public override void PreUpdateMovement()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (hookCache > -1)
		{
			base.Player.grappling[0] = hookCache;
			base.Player.grapCount = 1;
		}
		hookCache = -1;
		if (!obeyGravity)
		{
			base.Player.gravity = -1f;
		}
		if (Grappled)
		{
			Vector2 val = Main.projectile[Grapple].Center - base.Player.Center;
			if (((Vector2)(ref val)).Length() > SwingLength + 80f)
			{
				SoundEngine.PlaySound(in WulfrumAcrobaticsPack.ReleaseSound, Main.projectile[Grapple].Center);
				Main.projectile[Grapple].Kill();
			}
			else
			{
				SimulateMovement(Main.projectile[Grapple]);
			}
		}
		else
		{
			Grapple = -1;
		}
	}

	public void SimulateMovement(Projectile grapple)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		Segments = VerletSimulatedSegment.SimpleSimulation(Segments, SwingLength / 5f, 50, obeyGravity ? (0.3f * base.Player.gravDir) : 0f);
		foreach (VerletSimulatedSegment segment in Segments)
		{
			Vector2 CurrentPosition = segment.position;
		}
		if (!GrappleMovementDisabled)
		{
			if (obeyGravity)
			{
				Vector2 CurrentPosition = Segments[5].position;
				base.Player.velocity = CurrentPosition - base.Player.Center;
				if (base.Player.gravDir * (base.Player.Center.Y - Segments[0].position.Y) > 0f)
				{
					float swing = 0f;
					if (Math.Sign(base.Player.velocity.X) < 0)
					{
						if (base.Player.controlLeft)
						{
							swing -= 0.1f;
						}
						else if (base.Player.controlRight)
						{
							swing += 0.1f;
						}
					}
					else if (Math.Sign(base.Player.velocity.X) > 0)
					{
						if (base.Player.controlRight)
						{
							swing += 0.1f;
						}
						else if (base.Player.controlLeft)
						{
							swing -= 0.1f;
						}
					}
					base.Player.velocity.X += swing;
				}
				else if (Math.Abs(base.Player.Center.X - Segments[0].position.X) < 30f && Math.Abs(base.Player.velocity.X) < 1f)
				{
					base.Player.velocity.X = ((base.Player.velocity.X == 0f) ? 1.5f : (1.5f * (float)Math.Sign(base.Player.velocity.X)));
				}
			}
			else if (base.Player.grappling[0] > -1)
			{
				Projectile Hook = Main.projectile[base.Player.grappling[0]];
				if (Hook != null)
				{
					if (base.Player.controlRight)
					{
						base.Player.velocity.X += 0.3f;
					}
					else if (base.Player.controlLeft)
					{
						base.Player.velocity.X -= 0.3f;
					}
					Player player = base.Player;
					player.velocity -= base.Player.Center.DirectionTo(Hook.Center);
					Vector2 estimatedPos = base.Player.Center + base.Player.velocity;
					Vector2 estimatedDir = Hook.Center.DirectionTo(estimatedPos);
					Vector2 goalPos = Hook.Center + estimatedDir * SwingLength;
					Vector2 estimatedPosLocked = Hook.Center + Hook.Center.DirectionTo(base.Player.Center) * SwingLength;
					if (estimatedPosLocked.Distance(estimatedPos) < 2f)
					{
						estimatedPos = estimatedPosLocked;
					}
					float dis = Hook.Center.Distance(estimatedPos);
					if (dis > SwingLength && dis < 1000f)
					{
						float dis2 = Math.Min(base.Player.Center.Distance(goalPos), SwingLength * 3f);
						if (dis2 > 0f)
						{
							base.Player.velocity = base.Player.Center.DirectionTo(goalPos) * dis2;
						}
					}
					if (((Vector2)(ref base.Player.velocity)).Length() < 1.5f)
					{
						base.Player.direction = ((Hook.Center.X > base.Player.Center.X) ? 1 : (-1));
					}
				}
			}
		}
		if (Grappled)
		{
			for (int i = 1; i < Segments.Count; i++)
			{
				Vector2 position = Segments[i].position;
				Color val = Color.Lerp(Color.DeepSkyBlue, Color.GreenYellow, (float)i / 5f);
				Lighting.AddLight(position, ((Color)(ref val)).ToVector3());
			}
		}
		Segments[5].oldPosition = base.Player.Center;
	}

	public override void PostUpdate()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center;
		if (Grappled)
		{
			Segments[5].position = base.Player.Center;
			if (!GrappleMovementDisabled)
			{
				bool playerCrossedSides = Math.Sign(Segments[5].oldPosition.X - Segments[0].position.X) != Math.Sign(Segments[5].position.X - Segments[0].position.X);
				center = Segments[5].oldPosition - Segments[5].position;
				float swingSpeed = ((Vector2)(ref center)).Length();
				if ((swingSpeed > 6f) & playerCrossedSides)
				{
					SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/LoudSwingWoosh");
					soundStyle.Volume = Math.Clamp((swingSpeed - 6f) / 12f, 0f, 1f);
					SoundStyle swing = soundStyle;
					SoundEngine.PlaySound(in swing, base.Player.Center);
				}
			}
		}
		else
		{
			if (!AutoGrappleActivated)
			{
				return;
			}
			Vector2 checkedPlayerPosition = base.Player.position;
			bool imminentDanger = false;
			for (int i = 0; i < 3; i++)
			{
				Vector2 collisionVector = Collision.TileCollision(checkedPlayerPosition, base.Player.velocity, base.Player.width, base.Player.height, fallThrough: false, fall2: false, (int)base.Player.gravDir);
				if (collisionVector.Y < base.Player.velocity.Y)
				{
					imminentDanger = true;
					checkedPlayerPosition += collisionVector;
					break;
				}
				checkedPlayerPosition += collisionVector;
			}
			if (!imminentDanger)
			{
				return;
			}
			int num = (int)(checkedPlayerPosition.Y / 16f) - base.Player.fallStart;
			int fallDmgThreshold = 25 + base.Player.extraFall;
			if (!((float)num * base.Player.gravDir > (float)fallDmgThreshold))
			{
				return;
			}
			float halfSpread = 0.9424779f;
			Point bestGrapplePos = Point.Zero;
			float bestGrappleScore = 0f;
			for (float angle = 0f - halfSpread; angle < halfSpread; angle += 0.037699115f)
			{
				for (int j = 0; j < (int)(ActualMaxLength / 16f); j++)
				{
					Vector2 center2 = base.Player.Center;
					Vector2 spinningpoint = -Vector2.UnitY * base.Player.gravDir * (float)j * 16f;
					double radians = angle;
					center = default(Vector2);
					Vector2 checkSpot = center2 + spinningpoint.RotatedBy(radians, center);
					Point tilePos = checkSpot.ToSafeTileCoordinates();
					Tile tile = Main.tile[tilePos];
					if (tile.HasUnactuatedTile && tile.CanTileBeLatchedOnTo() && !base.Player.IsBlacklistedForGrappling(tilePos))
					{
						float num2 = bestGrappleScore;
						center = checkSpot - base.Player.Center;
						if (num2 < EvaluatePotentialSafetyHookPos(((Vector2)(ref center)).Length(), angle))
						{
							bestGrapplePos = tilePos;
							center = checkSpot - base.Player.Center;
							bestGrappleScore = EvaluatePotentialSafetyHookPos(((Vector2)(ref center)).Length(), angle);
						}
						break;
					}
				}
			}
			if (!(bestGrapplePos != Point.Zero))
			{
				return;
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.owner == base.Player.whoAmI && p.type == ModContent.ProjectileType<WulfrumHook>() && p.ModProjectile is WulfrumHook)
				{
					SoundEngine.PlaySound(in WulfrumAcrobaticsPack.ReleaseSound, p.Center);
					p.Kill();
				}
			}
			base.Player.fallStart = (int)(base.Player.position.Y / 16f);
			if (base.Player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Player.GetSource_ItemUse(PackItem), bestGrapplePos.ToWorldCoordinates(), Vector2.Zero, ModContent.ProjectileType<WulfrumHook>(), 3, 0f, base.Player.whoAmI);
			}
		}
	}

	public float EvaluatePotentialSafetyHookPos(float distance, float angle)
	{
		float score = 0.0001f;
		score = ((!(distance < 2f * ActualMaxLength / 3f)) ? (score + (1f - (distance - 2f * ActualMaxLength / 3f) / (ActualMaxLength / 3f))) : (score + distance / (2f * ActualMaxLength / 3f)));
		return score + (1f - Math.Abs(angle) / 0.9424779f) * 0.5f;
	}

	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		if (!WulfrumPackEquipped)
		{
			return;
		}
		bool setControlUpFalse = false;
		if (Grappled && triggersSet.Up)
		{
			SwingLength -= (strongerReel ? 6f : 4f);
			base.Player.controlUp = false;
			setControlUpFalse = true;
		}
		if (triggersSet.Down)
		{
			float length = ActualMaxLength;
			if (SwingLength < length)
			{
				SwingLength += 4f;
				if (SwingLength > length)
				{
					SwingLength = length;
				}
			}
		}
		if (triggersSet.Jump && base.Player.releaseJump)
		{
			WulfrumAcrobaticsJumpSync.Data syncData = default(WulfrumAcrobaticsJumpSync.Data);
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.owner != base.Player.whoAmI || p.type != ModContent.ProjectileType<WulfrumHook>() || !(p.ModProjectile is WulfrumHook { State: WulfrumHook.HookState.Grappling }))
				{
					continue;
				}
				bool canJumpOffHook = (base.Player.Center - p.Center).AngleBetween(-Vector2.UnitY) > (float)Math.PI / 2f || base.Player.Distance(p.Center) < 38f;
				syncData = syncData with
				{
					CanJumpOffHook = canJumpOffHook
				};
				if (canJumpOffHook)
				{
					Vector2 velocityBoost = Vector2.Zero;
					if ((Math.Sign(base.Player.velocity.X) < 0 && base.Player.controlLeft) || (Math.Sign(base.Player.velocity.X) > 0 && base.Player.controlRight))
					{
						velocityBoost += base.Player.velocity * 0.15f;
						if (velocityBoost.Y < 0f)
						{
							velocityBoost.Y *= 2f;
						}
					}
					if ((((Vector2)(ref base.Player.velocity)).Length() < 3f && ((Vector2)(ref base.Player.velocity)).Length() > 0.0001f) || PlayerOnGround)
					{
						velocityBoost -= Vector2.UnitY * Player.jumpSpeed * (1f - (float)Math.Pow(((Vector2)(ref base.Player.velocity)).Length() / 3f, 5.0));
					}
					if (((Vector2)(ref base.Player.velocity)).Length() < 8f)
					{
						base.Player.jump = Player.jumpHeight / 2;
					}
					Player player = base.Player;
					player.velocity += velocityBoost;
					base.Player.releaseJump = false;
				}
				else
				{
					base.Player.releaseJump = false;
				}
				SoundEngine.PlaySound(in WulfrumAcrobaticsPack.ReleaseSound, p.Center);
				p.Kill();
				base.Player.grapCount = 0;
			}
			if (Main.netMode == 1)
			{
				WulfrumAcrobaticsJumpSync.Send(syncData with
				{
					PlayerWhoAmI = (byte)base.Player.whoAmI,
					SwingLength = SwingLength,
					PlayerVelocity = base.Player.velocity,
					PlayerJump = base.Player.jump,
					SetControlUpFalse = setControlUpFalse
				});
			}
		}
		else if (Main.netMode == 1)
		{
			WulfrumAcrobaticsLengthSync.Send(new WulfrumAcrobaticsLengthSync.Data((byte)base.Player.whoAmI, SwingLength, setControlUpFalse));
		}
	}

	public override void FrameEffects()
	{
		if (base.Player.grappling[0] >= 0 && GrappleMovementDisabled && Main.projectile[base.Player.grappling[0]].type == ModContent.ProjectileType<WulfrumHook>())
		{
			hookCache = base.Player.grappling[0];
			base.Player.grappling[0] = -1;
			base.Player.grapCount = 0;
		}
	}
}
