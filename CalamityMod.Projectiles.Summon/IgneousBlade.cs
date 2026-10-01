using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IgneousBlade : ModProjectile, ILocalizedModType, IModType
{
	public enum AIState
	{
		CircleOwner,
		TransitionToLaunch,
		LaunchAtPos
	}

	public int BladeIndex;

	public VertexStrip TrailDrawer;

	public Vector2 ChargeStartingPosition;

	public Vector2 ChargeTargetPos;

	public Vector2 ChargeStartPos;

	public new string LocalizationCategory => "Projectiles.Summon";

	public float BladeHoverOffsetAngle
	{
		get
		{
			float projectileCounts = Owner.ownedProjectileCounts[base.Type];
			if (projectileCounts <= 1f)
			{
				projectileCounts = 1f;
			}
			return MathHelper.WrapAngle((float)Math.PI * 2f * (float)BladeIndex / projectileCounts + (float)Math.PI * 2f * ((float)(Owner.miscCounter % 60) / 60f)) * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1));
		}
	}

	public AIState CurrentState
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

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AITimer => ref base.Projectile.ai[1];

	public ref float DistanceTimer => ref base.Projectile.ai[2];

	public ref float BladeGleamInterpolant => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 45;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		base.Projectile.width = 84;
		base.Projectile.height = 84;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(BladeIndex);
		writer.WriteVector2(ChargeStartingPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		BladeIndex = reader.ReadInt32();
		ChargeStartingPosition = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		HandleMinionBools();
		base.Projectile.MaxUpdates = 1;
		BladeGleamInterpolant = MathHelper.Lerp(BladeGleamInterpolant, 0f, 0.1f);
		if (BladeGleamInterpolant <= 0.02f)
		{
			BladeGleamInterpolant = 0f;
		}
		switch (CurrentState)
		{
		case AIState.CircleOwner:
			CircleOwner();
			break;
		case AIState.TransitionToLaunch:
			Owner.Calamity().mouseWorldListener = true;
			ChargeTargetPos = Owner.Calamity().mouseWorldDeltaFromPlayer;
			ChargeStartPos = Owner.Center;
			CurrentState = AIState.LaunchAtPos;
			AITimer = 0f;
			base.Projectile.penetrate = 15;
			LaunchAtTargetPos();
			break;
		case AIState.LaunchAtPos:
			LaunchAtTargetPos();
			break;
		}
		if (CurrentState == AIState.CircleOwner)
		{
			if (Owner.HeldItem.type == ModContent.ItemType<IgneousExaltation>())
			{
				AITimer++;
			}
			else
			{
				AITimer--;
			}
			AITimer = MathHelper.Clamp(AITimer, (float)(-IgneousExaltation.ChargeCooldown), 0f);
		}
		else
		{
			AITimer++;
		}
		DistanceTimer++;
	}

	public void CircleOwner()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 hoverDestination = Owner.Center + BladeHoverOffsetAngle.ToRotationVector2() * ((DistanceTimer < 0f) ? MathHelper.Lerp(200f, 100f, 1f - (0f - DistanceTimer) / (float)IgneousExaltation.ChargeCooldown) : 100f);
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.04f).MoveTowards(hoverDestination, 24f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		if (!base.Projectile.WithinRange(Owner.Center, 3200f))
		{
			base.Projectile.Center = hoverDestination;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
	}

	public void LaunchAtTargetPos()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		int chargeTime = IgneousExaltation.ChargeDuration * 2;
		int chargeLength = 2000;
		float circleWidth = Math.Min(75, Owner.ownedProjectileCounts[base.Type] * 4);
		Vector2 CurrentPlace = Vector2.Lerp(ChargeStartPos, ChargeStartPos + ChargeTargetPos.SafeNormalize(Vector2.UnitX) * (float)chargeLength, MathF.Pow(AITimer / (float)chargeTime, 3f));
		base.Projectile.Center = CurrentPlace + BladeHoverOffsetAngle.ToRotationVector2() * Math.Max(MathHelper.Lerp(100f, 25f, AITimer / 10f), circleWidth);
		base.Projectile.rotation = base.Projectile.AngleFrom(CurrentPlace) + (float)Math.PI / 2f;
		if (AITimer >= (float)chargeTime)
		{
			base.Projectile.damage = base.Projectile.originalDamage;
			AITimer = -IgneousExaltation.ChargeCooldown;
			DistanceTimer = -IgneousExaltation.ChargeCooldown;
			CurrentState = AIState.CircleOwner;
			base.Projectile.penetrate = -1;
		}
	}

	public void HandleMinionBools()
	{
		Owner.AddBuff(ModContent.BuffType<IgneousExaltationBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<IgneousBlade>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().igneousExaltation = false;
			}
			if (Owner.Calamity().igneousExaltation)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.penetrate >= 10)
		{
			modifiers.SourceDamage *= 2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.penetrate >= 10 && CurrentState != AIState.CircleOwner && Main.myPlayer == base.Projectile.owner)
		{
			for (int i = 0; i < 1; i++)
			{
				Vector2 spawnPosition = base.Projectile.Center - Utils.RotatedByRandom(new Vector2(0f, 550f), 6.2831854820251465);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, Vector2.Normalize(base.Projectile.Center - spawnPosition) * 24f, ModContent.ProjectileType<IgneousBladeStrike>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			for (int j = 0; j < Main.rand.Next(28, 41); j++)
			{
				Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(10f), 6, Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 4f));
			}
			base.Projectile.netUpdate = true;
		}
		base.OnHitNPC(target, hit, damageDone);
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * 0.48f;
		return Color.Lerp(new Color(166, 46, 61), new Color(64, 51, 66), MathHelper.Clamp(completionRatio * 1.4f, 0f, 1f)) * opacity;
	}

	public float TrailWidthFunction(float completionRatio)
	{
		return (float)base.Projectile.height * (1f - completionRatio) * 0.3f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(((base.Projectile.spriteDirection == 1) ^ Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : 0);
		if (TrailDrawer == null)
		{
			TrailDrawer = new VertexStrip();
		}
		GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_201");
		GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_193");
		GameShaders.Misc["EmpressBlade"].UseShaderSpecificData(new Vector4(1f, 0f, 0f, 0.6f));
		GameShaders.Misc["EmpressBlade"].Apply();
		TrailDrawer.PrepareStrip(base.Projectile.oldPos, base.Projectile.oldRot, TrailColorFunction, TrailWidthFunction, base.Projectile.Size * 0.5f - Main.screenPosition, base.Projectile.oldPos.Length, includeBacksides: true);
		TrailDrawer.DrawTrail();
		Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		float outlineOpacity = 1f;
		float outlineWidth = 1f;
		if (CurrentState == AIState.CircleOwner && AITimer < 0f)
		{
			outlineWidth = Math.Clamp(MathF.Pow(1f - AITimer / (float)(-IgneousExaltation.ChargeCooldown), 3f), 0f, 100f);
			outlineOpacity = 0.75f;
		}
		Texture2D bladeOutlineTex = IgneousExaltation.GetBladeOutlineTex();
		float rotation = base.Projectile.rotation - (float)Math.PI / 4f * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1));
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(2f, 0f) * outlineWidth, frame, new Color(166, 46, 61) * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(0f, 2f) * outlineWidth, frame, new Color(166, 46, 61) * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(-2f, 0f) * outlineWidth, frame, new Color(166, 46, 61) * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(0f, -2f) * outlineWidth, frame, new Color(166, 46, 61) * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, direction);
		Texture2D shineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		Vector2 shineScale = new Vector2(1.67f, 3f) * base.Projectile.scale;
		shineScale *= MathHelper.Lerp(0.9f, 1.1f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 7.4f + (float)base.Projectile.identity) * 0.5f + 0.5f);
		Vector2 lensFlareWorldPosition = base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * (float)base.Projectile.width * base.Projectile.scale * 0.88f;
		Color val = Color.Lerp(Color.LimeGreen, Color.Yellow, 0.23f);
		((Color)(ref val)).A = 0;
		Color lensFlareColor = val * BladeGleamInterpolant;
		Main.EntitySpriteDraw(shineTex, lensFlareWorldPosition - Main.screenPosition, null, lensFlareColor, 0f, shineTex.Size() * 0.5f, shineScale * 0.6f, (SpriteEffects)0);
		Main.EntitySpriteDraw(shineTex, lensFlareWorldPosition - Main.screenPosition, null, lensFlareColor, (float)Math.PI / 2f, shineTex.Size() * 0.5f, shineScale, (SpriteEffects)0);
		GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_209");
		GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_210");
		return false;
	}

	public IgneousBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		ChargeTargetPos = Vector2.Zero;
		ChargeStartPos = Vector2.Zero;
		base._002Ector();
	}
}
