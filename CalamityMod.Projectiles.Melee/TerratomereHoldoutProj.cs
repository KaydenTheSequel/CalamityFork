using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerratomereHoldoutProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public int Direction => base.Projectile.velocity.X.DirectionalSign();

	public float SwingCompletion => MathHelper.Clamp(Time / 54f, 0f, 1f);

	public float SwingCompletionAtStartOfTrail => MathHelper.Clamp(SwingCompletion - 0.2f, SwingCompletionRatio, 1f);

	public float SwordRotation
	{
		get
		{
			float swordRotation = InitialRotation + GetSwingOffsetAngle(SwingCompletion) * (float)base.Projectile.spriteDirection + (float)Math.PI / 4f;
			if (base.Projectile.spriteDirection == -1)
			{
				swordRotation += (float)Math.PI / 2f;
			}
			return swordRotation;
		}
	}

	public Vector2 SwordDirection
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			return SwordRotation.ToRotationVector2() * (float)Direction;
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public ref float InitialRotation => ref base.Projectile.ai[1];

	public static float SwingCompletionRatio => 0.37f;

	public static float RecoveryCompletionRatio => 0.84f;

	public static CalamityUtils.CurveSegment AnticipationWait => new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, -1.67f, 0f);

	public static CalamityUtils.CurveSegment Anticipation => new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.14f, AnticipationWait.EndingHeight, -1.05f, 2);

	public static CalamityUtils.CurveSegment Swing => new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, SwingCompletionRatio, Anticipation.EndingHeight, 4.43f, 5);

	public static CalamityUtils.CurveSegment Recovery => new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, RecoveryCompletionRatio, Swing.EndingHeight, 0.97f, 3);

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Terratomere";

	public static float GetSwingOffsetAngle(float completion)
	{
		return CalamityUtils.PiecewiseAnimation(completion, AnticipationWait, Anticipation, Swing, Recovery);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 100;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 66;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.timeLeft = 54;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 7;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (InitialRotation == 0f)
		{
			InitialRotation = base.Projectile.velocity.ToRotation();
			base.Projectile.netUpdate = true;
		}
		base.Projectile.scale = Utils.GetLerpValue(0f, 0.13f, SwingCompletion, clamped: true) * Utils.GetLerpValue(1f, 0.87f, SwingCompletion, clamped: true) * 0.7f + 0.3f;
		AdjustPlayerValues();
		StickToOwner();
		CreateProjectiles();
		if (SwingCompletion > SwingCompletionRatio + 0.2f && SwingCompletion < RecoveryCompletionRatio)
		{
			CreateSlashSparkleDust();
		}
		base.Projectile.rotation = SwordRotation;
		Time++;
	}

	public void AdjustPlayerValues()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = Direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = ((float)base.Projectile.direction * base.Projectile.velocity).ToRotation();
		float armRotation = SwordRotation - (float)Direction * 1.67f;
		Owner.SetCompositeArmFront(Math.Abs(armRotation) > 0.01f, Player.CompositeArmStretchAmount.Full, armRotation);
	}

	public void StickToOwner()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) + SwordDirection * new Vector2(7f, 16f) * base.Projectile.scale;
		Projectile projectile = base.Projectile;
		projectile.Center -= base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * new Vector2(66f, 54f + base.Projectile.scale * 8f);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.SetDummyItemTime(2);
		Owner.ChangeDir(Direction);
	}

	public void CreateProjectiles()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		if (Time == (float)(int)(54f * (SwingCompletionRatio + 0.15f)))
		{
			SoundEngine.PlaySound(in Terratomere.SwingSound, base.Projectile.Center);
		}
		_ = ref Time;
		_ = RecoveryCompletionRatio;
		if (Main.myPlayer == base.Projectile.owner && Time == (float)(int)(54f * (SwingCompletionRatio + 0.34f)))
		{
			Vector2 bigSlashVelocity = base.Projectile.SafeDirectionTo(Main.MouseWorld) * Owner.HeldItem.shootSpeed / 2f;
			if (bigSlashVelocity.AngleBetween(InitialRotation.ToRotationVector2()) > 1.456f)
			{
				bigSlashVelocity = InitialRotation.ToRotationVector2() * ((Vector2)(ref bigSlashVelocity)).Length();
			}
			int totalBeams = 4;
			float randomVelocityLimit = ((Vector2)(ref bigSlashVelocity)).Length() * 0.2f;
			for (int i = 0; i < totalBeams; i++)
			{
				Vector2 randomVelocity = Main.rand.NextVector2CircularEdge(randomVelocityLimit, randomVelocityLimit);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center - bigSlashVelocity * 0.4f, bigSlashVelocity + randomVelocity, ModContent.ProjectileType<TerratomereSwordBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (Main.myPlayer == base.Projectile.owner && Time == (float)(int)(54f * RecoveryCompletionRatio) + 5f)
		{
			Vector2 bigSlashVelocity2 = InitialRotation.ToRotationVector2() * Owner.HeldItem.shootSpeed / 6f;
			Vector2 bigSlashSpawnPosition = base.Projectile.Center + bigSlashVelocity2.SafeNormalize(Vector2.UnitY) * 64f;
			int slash = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), bigSlashSpawnPosition, bigSlashVelocity2, ModContent.ProjectileType<TerratomereMeleeSlash>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (Main.projectile.IndexInRange(slash))
			{
				Main.projectile[slash].ai[0] = ((float)Direction == 1f).ToInt();
				Main.projectile[slash].ModProjectile<TerratomereMeleeSlash>().ControlPoints = GenerateSlashPoints().ToArray();
			}
		}
	}

	public void CreateSlashSparkleDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 initialDirection = InitialRotation.ToRotationVector2();
		Vector2 position = base.Projectile.Center + (GetSwingOffsetAngle(SwingCompletion) * (float)Direction + InitialRotation).ToRotationVector2() * Main.rand.NextFloat(8f, 66f) + initialDirection * 76f;
		int dustID = (Main.rand.NextBool() ? 267 : 264);
		Dust dust = Dust.NewDustPerfect(position, dustID, Vector2.Zero);
		dust.color = Color.Lerp(Terratomere.TerraColor1, Terratomere.TerraColor2, Main.rand.NextFloat());
		dust.color = Color.Lerp(dust.color, Color.Yellow, (float)Math.Pow(Main.rand.NextFloat(), 1.63));
		dust.fadeIn = Main.rand.NextFloat(1f, 2f);
		dust.scale = 0.4f;
		dust.velocity = initialDirection * Main.rand.NextFloat(0.5f, 15f);
		dust.noLight = true;
		dust.noGravity = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		DrawSlash();
		DrawBlade(lightColor);
		return false;
	}

	public float SlashWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 22f;
	}

	public Color SlashColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lime * Utils.GetLerpValue(0.9f, 0.4f, completionRatio, clamped: true) * base.Projectile.Opacity;
	}

	public IEnumerable<Vector2> GenerateSlashPoints()
	{
		for (int i = 0; i < 20; i++)
		{
			float completion = MathHelper.Lerp(SwingCompletion, SwingCompletionAtStartOfTrail, (float)i / 20f);
			float reelBackAngle = Math.Abs(base.Projectile.oldRot[0] - base.Projectile.oldRot[1]) * 0.8f;
			if (SwingCompletion > RecoveryCompletionRatio)
			{
				reelBackAngle = 0.21f;
			}
			float offsetAngle = (GetSwingOffsetAngle(completion) - reelBackAngle) * (float)Direction + InitialRotation;
			yield return offsetAngle.ToRotationVector2() * base.Projectile.scale * 54f;
		}
	}

	public void DrawSlash()
	{
		Main.spriteBatch.EnterShaderRegion();
		PrepareSlashShader(Direction == 1);
		if (SwingCompletionAtStartOfTrail > SwingCompletionRatio)
		{
			PrimitiveRenderer.RenderTrail(GenerateSlashPoints().ToArray(), new PrimitiveSettings(SlashWidthFunction, SlashColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Center;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladeSlash"]), 95);
		}
		Main.spriteBatch.ExitShaderRegion();
	}

	public void DrawBlade(Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = texture.Size() * Vector2.UnitY;
		if (base.Projectile.spriteDirection == -1)
		{
			origin.X += texture.Width;
		}
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction, 0f);
	}

	public static void PrepareSlashShader(bool flipped)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ExobladeSlash"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:ExobladeSlash"].UseColor(Terratomere.TerraColor1);
		GameShaders.Misc["CalamityMod:ExobladeSlash"].UseSecondaryColor(Terratomere.TerraColor2);
		EffectParameter obj = GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["fireColor"];
		Color terraColor = Terratomere.TerraColor1;
		obj.SetValue(((Color)(ref terraColor)).ToVector3());
		GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["flipped"].SetValue(flipped);
		GameShaders.Misc["CalamityMod:ExobladeSlash"].Apply();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		float point = 0f;
		Vector2 direction = (InitialRotation + GetSwingOffsetAngle(SwingCompletion)).ToRotationVector2() * new Vector2((float)base.Projectile.spriteDirection, 1f);
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + direction * (float)base.Projectile.height * base.Projectile.scale, (float)base.Projectile.width * 0.25f, ref point);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		Owner.DoLifestealDirect(target, (int)Math.Round((double)hit.Damage * 0.025), 0.75f);
		int slashCreatorID = ModContent.ProjectileType<TerratomereSlashCreator>();
		if (Owner.ownedProjectileCounts[slashCreatorID] < 4)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, slashCreatorID, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, target.whoAmI, Main.rand.NextFloat((float)Math.PI * 2f));
			Owner.ownedProjectileCounts[slashCreatorID]++;
		}
	}
}
