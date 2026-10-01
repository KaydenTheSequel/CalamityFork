using System;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LamentationsOfTheChained : ModProjectile, ILocalizedModType, IModType
{
	private NPC[] excludedTargets = new NPC[4];

	private const float MaxTangleReach = 400f;

	public Vector2 whip1;

	public Vector2 whip2;

	public Vector2 whip3;

	public Particle smear;

	public Particle smear2;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChained";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float ChainSwapTimer => ref base.Projectile.ai[0];

	public ref float SnapCoyoteTime => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = OmegaBiomeBlade.FlailBladeAttunement_LocalIFrames;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		if (ChainSwapTimer < (float)Math.Max(OmegaBiomeBlade.FlailBladeAttunement_FlailTime, OmegaBiomeBlade.FlailBladeAttunement_LocalIFrames))
		{
			return false;
		}
		GenerateCurve(whip1.Y, whip1.X.ToRotationVector2(), out var control, out var control2, out var control3, out var control13, ChainSwapTimer % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime);
		GenerateCurve(whip2.Y, whip2.X.ToRotationVector2(), out control3, out control2, out control, out var control23, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		GenerateCurve(whip3.Y, whip3.X.ToRotationVector2(), out control, out control2, out control3, out var control33, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime * 2f / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		if (Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), control13 - Vector2.One * 25f, Vector2.One * 50f) || Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), control23 - Vector2.One * 25f, Vector2.One * 50f) || Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), control33 - Vector2.One * 25f, Vector2.One * 50f))
		{
			return true;
		}
		float CollisionPoint = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.MountedCenter, control13, 20f, ref CollisionPoint) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.MountedCenter, control13, 20f, ref CollisionPoint) || Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.MountedCenter, control13, 20f, ref CollisionPoint))
		{
			return true;
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		GenerateCurve(whip1.Y, whip1.X.ToRotationVector2(), out var control, out var control2, out var control3, out var control13, ChainSwapTimer % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime);
		GenerateCurve(whip2.Y, whip2.X.ToRotationVector2(), out control3, out control2, out control, out var control23, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		GenerateCurve(whip3.Y, whip3.X.ToRotationVector2(), out control, out control2, out control3, out var control33, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime * 2f / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		if (Collision.CheckAABBvAABBCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), control13 - Vector2.One * 25f, Vector2.One * 50f) || Collision.CheckAABBvAABBCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), control23 - Vector2.One * 25f, Vector2.One * 50f) || Collision.CheckAABBvAABBCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), control33 - Vector2.One * 25f, Vector2.One * 50f))
		{
			if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.FlailBladeAttunement_BladeProc)
			{
				sword.OnHitProc = true;
			}
			modifiers.SetCrit();
			for (int i = 0; i < 2; i++)
			{
				Vector2 sparkSpeed = Owner.DirectionTo(target.Center).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * 9f;
				GeneralParticleHandler.SpawnParticle(new CritSpark(target.Center, sparkSpeed, Color.White, Color.Turquoise, 1f + Main.rand.NextFloat(0f, 1f), 30, 0.4f));
			}
			Vector2 sliceDirection = Main.rand.NextVector2CircularEdge(50f, 100f);
			GeneralParticleHandler.SpawnParticle(new LineVFX(target.Center - sliceDirection, sliceDirection * 2f, 0.2f, Color.PaleTurquoise * 0.6f)
			{
				Lifetime = 6
			});
		}
		else
		{
			if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword2 && Main.rand.NextFloat() <= OmegaBiomeBlade.FlailBladeAttunement_ChainProc)
			{
				sword2.OnHitProc = true;
			}
			modifiers.SourceDamage *= OmegaBiomeBlade.FlailBladeAttunement_ChainDamageReduction;
			modifiers.DisableCrit();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (!hit.Crit)
		{
			return;
		}
		SoundEngine.PlaySound(in CommonCalamitySounds.SwiftSliceSound, base.Projectile.Center);
		excludedTargets[0] = target;
		for (int i = 0; i < 3; i++)
		{
			NPC potentialTarget = TargetNext(target.Center, i);
			if (potentialTarget == null)
			{
				break;
			}
			if (Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<GhastlyChain>(), (int)((float)damageDone * OmegaBiomeBlade.FlailBladeAttunement_GhostChainDamageReduction), 0f, Owner.whoAmI, target.whoAmI, potentialTarget.whoAmI).ModProjectile is GhastlyChain chain)
			{
				chain.Gravity = Main.rand.NextFloat(30f, 50f);
			}
		}
		Array.Clear(excludedTargets, 0, 3);
	}

	public NPC TargetNext(Vector2 hitFrom, int index)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		float longestReach = 400f;
		NPC target = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!Enumerable.Contains(excludedTargets, npc) && npc.CanBeChasedBy() && !npc.friendly && !npc.townNPC)
			{
				float distance = Vector2.Distance(hitFrom, npc.Center);
				if (distance < longestReach)
				{
					longestReach = distance;
					target = npc;
				}
			}
		}
		if (index < 3)
		{
			excludedTargets[index + 1] = target;
		}
		return target;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
		((Vector2)(ref base.Projectile.velocity)).Normalize();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = Owner.Center + base.Projectile.velocity * 60f;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		base.Projectile.timeLeft = 2;
		if (ChainSwapTimer % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime == 1f)
		{
			SoundEngine.PlaySound(in SoundID.DD2_OgreSpit, base.Projectile.Center);
			Vector2 smearPos = Owner.Center + whip1.X.ToRotationVector2() * (float)OmegaBiomeBlade.FlailBladeAttunement_Reach * Main.rand.NextFloat(0.7f, 1.1f);
			Vector2 squish = default(Vector2);
			((Vector2)(ref squish))._002Ector(Main.rand.NextFloat(3f, 4f), Main.rand.NextFloat(0.5f, 1f));
			if (smear == null)
			{
				smear = new SemiCircularSmearVFX(smearPos, Color.PowderBlue * 0.5f, whip1.X + (float)Math.PI, base.Projectile.scale * 1.5f, squish)
				{
					Lifetime = 2
				};
				GeneralParticleHandler.SpawnParticle(smear);
			}
			else
			{
				smear.Rotation = whip1.X + (float)Math.PI;
				smear.Position = smearPos;
				(smear as SemiCircularSmearVFX).Squish = squish;
			}
			smearPos = Owner.Center + whip1.X.ToRotationVector2() * (float)OmegaBiomeBlade.FlailBladeAttunement_Reach * Main.rand.NextFloat(0.8f, 1.3f);
			((Vector2)(ref squish))._002Ector(Main.rand.NextFloat(2f, 3f), Main.rand.NextFloat(0.9f, 1.4f));
			if (smear2 == null)
			{
				smear2 = new SemiCircularSmearVFX(smearPos, Color.PowderBlue * 0.5f, whip2.X + (float)Math.PI, base.Projectile.scale * 1.5f, squish)
				{
					Lifetime = 2
				};
				GeneralParticleHandler.SpawnParticle(smear2);
			}
			else
			{
				smear2.Rotation = whip2.X + (float)Math.PI;
				smear2.Position = smearPos;
				(smear2 as SemiCircularSmearVFX).Squish = squish;
			}
		}
		if (smear != null)
		{
			smear.Rotation = smear.Rotation.AngleTowards(base.Projectile.velocity.ToRotation(), 0.01f);
			smear.Time = 0;
			(smear as SemiCircularSmearVFX).Squish.Y *= 0.985f;
			(smear as SemiCircularSmearVFX).Squish.X *= 1.01f;
		}
		if (smear2 != null)
		{
			smear2.Rotation = smear2.Rotation.AngleTowards(base.Projectile.velocity.ToRotation(), 0.01f);
			smear2.Time = 0;
			(smear2 as SemiCircularSmearVFX).Squish.Y *= 0.985f;
			(smear2 as SemiCircularSmearVFX).Squish.X *= 1.01f;
		}
		ChainSwapTimer++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChained", (AssetRequestMode)2).Value;
		CalculateChains(out var chainPositions1, out var chainPositions2, out var chainPositions3);
		Vector2 drawPos = base.Projectile.Center - base.Projectile.velocity * 55f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		Main.EntitySpriteDraw(handle, drawPos - Main.screenPosition, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedFlail", (AssetRequestMode)2).Value;
		Rectangle bladeFrame = default(Rectangle);
		((Rectangle)(ref bladeFrame))._002Ector(0, 0, 24, 48);
		Vector2 flailOrigin = default(Vector2);
		((Vector2)(ref flailOrigin))._002Ector((float)(bladeFrame.Width / 2), (float)bladeFrame.Height);
		Main.EntitySpriteDraw(rotation: (chainPositions1[^2] - chainPositions1[^3]).ToRotation() + (float)Math.PI / 2f, texture: value, position: chainPositions1[^2] - Main.screenPosition, sourceRectangle: bladeFrame, color: Color.White, origin: flailOrigin, scale: 1f, effects: (SpriteEffects)0);
		Main.EntitySpriteDraw(rotation: (chainPositions2[^2] - chainPositions2[chainPositions1.Length - 3]).ToRotation() + (float)Math.PI / 2f, texture: value, position: chainPositions2[^2] - Main.screenPosition, sourceRectangle: bladeFrame, color: Color.White, origin: flailOrigin, scale: 1f, effects: (SpriteEffects)0);
		Main.EntitySpriteDraw(rotation: (chainPositions3[^2] - chainPositions3[^3]).ToRotation() + (float)Math.PI / 2f, texture: value, position: chainPositions3[^2] - Main.screenPosition, sourceRectangle: bladeFrame, color: Color.White, origin: flailOrigin, scale: 1f, effects: (SpriteEffects)0);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		Main.EntitySpriteDraw(blade, drawPos - Main.screenPosition, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	private void GenerateCurve(float seed, Vector2 direction, out Vector2 control0, out Vector2 control1, out Vector2 control2, out Vector2 control3, float angleShift = 0f, float necessaryOrientation = 1f)
	{
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		float randomNumber = 0.5f + ((float)Math.Sin((double)seed * 17.07947) + (float)Math.Sin((double)(seed * 0.2f) * 25.13274)) * 0.25f;
		float seed2 = 0.5f + ((float)Math.Sin((double)(randomNumber * 100f) * 17.07947) + (float)Math.Sin((double)(randomNumber * 100f * 0.2f) * 25.13274)) * 0.25f;
		float seed3 = 0.5f + ((float)Math.Sin((double)(seed2 * 50f) * 17.07947) + (float)Math.Sin((double)(seed2 * 50f * 0.2f) * 25.13274)) * 0.25f;
		float seed4 = 0.5f + ((float)Math.Sin((double)seed3 * 17.07947) + (float)Math.Sin((double)(seed3 * 0.2f) * 25.13274)) * 0.25f;
		if ((necessaryOrientation == -1f && (double)randomNumber >= 0.5) || (necessaryOrientation == 1f && (double)randomNumber < 0.5))
		{
			randomNumber = 1f - randomNumber;
		}
		randomNumber += angleShift * 0.1f * necessaryOrientation;
		float flip = (((double)randomNumber >= 0.5) ? 1f : (-1f));
		control0 = Owner.MountedCenter + direction.RotatedBy(MathHelper.ToRadians(MathHelper.Lerp(0.23561947f * flip, (float)Math.PI / 4f * flip, randomNumber))) * MathHelper.Lerp(50f, 130f, (float)Math.Sin(randomNumber * (float)Math.PI));
		float easedShift = ((angleShift == 1f) ? 1f : (1f - (float)Math.Pow(2.0, -10f * angleShift)));
		float Reach = (float)OmegaBiomeBlade.FlailBladeAttunement_Reach * (0.75f + 0.75f * seed3 - 0.05f * easedShift);
		control3 = Owner.MountedCenter + (direction * Reach).RotatedBy(MathHelper.Lerp(-0.01f * flip, 0.01f * flip, easedShift));
		Vector2 point1 = control3 + direction.RotatedBy(1.5707963705062866) * 50f;
		Vector2 point2 = control3 + direction.RotatedBy(1.5707963705062866) * 50f + direction * 250f;
		Vector2 point3 = control3 + direction.RotatedBy(-1.5707963705062866) * 50f + direction * 250f;
		Vector2 point4 = control3 + direction.RotatedBy(-1.5707963705062866) * 50f;
		BezierCurve curve = new BezierCurve(point1, point2, point3, point4);
		control3 = curve.Evaluate(randomNumber);
		Vector2 directionFromHead = direction.RotatedBy(MathHelper.ToRadians(MathHelper.Lerp(0f, 160f * flip, (float)Math.Sin(randomNumber * (float)Math.PI)))) * MathHelper.Lerp(130f, 200f, MathHelper.Clamp((float)Math.Sin(randomNumber * (float)Math.PI) - 0.5f, 0f, 1f) * 2f);
		control2 = control3 + directionFromHead;
		Vector2 directionFromSecondToLastPoint = directionFromHead.RotatedBy((float)Math.PI - MathHelper.ToRadians(MathHelper.Lerp(80f * flip, 110f * flip, (float)Math.Sin(randomNumber * (float)Math.PI)))).SafeNormalize(Vector2.Zero) * MathHelper.Lerp(120f, 280f, (float)Math.Sin(randomNumber * (float)Math.PI));
		control1 = control2 + directionFromSecondToLastPoint;
		control3 += Vector2.UnitX.RotatedBy((float)Math.PI * 2f * seed2) * seed4 * 30f;
		control2 += Vector2.UnitX.RotatedBy((float)Math.PI * 2f * seed3) * seed2 * 30f;
		control1 += Vector2.UnitX.RotatedBy((float)Math.PI * 2f * seed4) * seed3 * 30f;
	}

	private void CalculateChains(out Vector2[] chainPositions1, out Vector2[] chainPositions2, out Vector2[] chainPositions3)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		if (ChainSwapTimer % 6f == 0f || whip1.Y == 0f)
		{
			whip1.X = base.Projectile.velocity.RotatedByRandom(0.04908738657832146).ToRotation();
			whip1.Y = Main.rand.NextFloat(0.2f, 50f);
			ChainSwapTimer++;
		}
		if ((ChainSwapTimer - 2f) % 6f == 0f || whip2.Y == 0f)
		{
			whip2.X = base.Projectile.velocity.RotatedByRandom(0.09817477315664291).ToRotation();
			whip2.Y = Main.rand.NextFloat(0.2f, 50f);
			ChainSwapTimer++;
		}
		if ((ChainSwapTimer - 4f) % 6f == 0f || whip3.Y == 0f)
		{
			whip3.X = base.Projectile.velocity.RotatedByRandom(0.13089969754219055).ToRotation();
			whip3.Y = Main.rand.NextFloat(0.2f, 50f);
			ChainSwapTimer++;
		}
		GenerateCurve(whip1.Y, whip1.X.ToRotationVector2(), out var control0, out var control1, out var control2, out var control3, ChainSwapTimer % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime);
		DrawChain(out chainPositions1, control0, control1, control2, control3);
		GenerateCurve(whip2.Y, whip2.X.ToRotationVector2(), out control0, out control1, out control2, out control3, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		DrawChain(out chainPositions2, control0, control1, control2, control3);
		GenerateCurve(whip3.Y, whip3.X.ToRotationVector2(), out control0, out control1, out control2, out control3, (ChainSwapTimer - (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime * 2f / 3f) % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime / (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime, -1f);
		DrawChain(out chainPositions3, control0, control1, control2, control3);
	}

	private void DrawChain(out Vector2[] chainPositions, Vector2 control0, Vector2 control1, Vector2 control2, Vector2 control3)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedFlail", (AssetRequestMode)2).Value;
		Rectangle chainFrame = default(Rectangle);
		((Rectangle)(ref chainFrame))._002Ector(0, 70, 24, 14);
		Rectangle guardFrame = default(Rectangle);
		((Rectangle)(ref guardFrame))._002Ector(0, 50, 24, 18);
		BezierCurve curve = new BezierCurve(Owner.MountedCenter, control0, control1, control2, control3);
		int numPoints = 40;
		chainPositions = curve.GetPoints(numPoints).ToArray();
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		Vector2 origin2 = default(Vector2);
		for (int i = 1; i < numPoints; i++)
		{
			Vector2 position = chainPositions[i];
			float rotation = (chainPositions[i] - chainPositions[i - 1]).ToRotation() + (float)Math.PI / 2f;
			Color chainLightColor = Lighting.GetColor((int)position.X / 16, (int)position.Y / 16);
			if (i < numPoints - 1)
			{
				float yScale = Vector2.Distance(chainPositions[i], chainPositions[i - 1]) / (float)chainFrame.Height;
				float segmentProgress = (((float)i < 30f) ? ((float)i / 30f) : (1f - ((float)i - 30f) / 10f));
				float xScale = 1f + 0.75f * (float)Math.Sin(segmentProgress * ((float)Math.PI / 2f));
				((Vector2)(ref scale))._002Ector(xScale, yScale);
				((Vector2)(ref origin))._002Ector((float)(chainFrame.Width / 2), (float)chainFrame.Height);
				float chainOpacity = MathHelper.Clamp((float)i / (float)numPoints * 3f, 0f, 1f);
				Main.EntitySpriteDraw(tex, chainPositions[i] - Main.screenPosition, chainFrame, chainLightColor * chainOpacity, rotation, origin, scale, (SpriteEffects)0);
			}
			else
			{
				((Vector2)(ref origin2))._002Ector((float)(guardFrame.Width / 2), (float)guardFrame.Height);
				Main.EntitySpriteDraw(tex, chainPositions[i] - Main.screenPosition, guardFrame, chainLightColor, rotation, origin2, 1f, (SpriteEffects)0);
				if (ChainSwapTimer % (float)OmegaBiomeBlade.FlailBladeAttunement_FlailTime == 1f && Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(chainPositions[i], Vector2.Zero, Color.PaleTurquoise, Color.MediumTurquoise, 1f + Main.rand.NextFloat(0f, 1f), 30, 0.4f, 0.2f));
				}
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(whip1);
		writer.WriteVector2(whip2);
		writer.WriteVector2(whip3);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		whip1 = reader.ReadVector2();
		whip2 = reader.ReadVector2();
		whip3 = reader.ReadVector2();
	}
}
