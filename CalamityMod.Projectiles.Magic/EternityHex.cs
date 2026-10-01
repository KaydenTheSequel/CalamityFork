using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EternityHex : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 310;

	public const float BossLifeMaxDamageMult = 0.0028571428f;

	public const float NormalEnemyLifeMaxDamageMult = 0.01f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public int TargetNPCIndex
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float LemniscateAngle
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public float Time
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public int BookProjectileIndex
	{
		get
		{
			return (int)base.Projectile.localAI[1];
		}
		set
		{
			base.Projectile.localAI[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 63;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (TargetNPCIndex >= Main.npc.Length || TargetNPCIndex < 0)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		NPC target = Main.npc[TargetNPCIndex];
		if (BookProjectileIndex >= Main.projectile.Length || BookProjectileIndex < 0 || Time < 0f)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		if (!target.active)
		{
			NPC potentialTarget = player.ClampedMouseWorld().ClosestNPCAt(4400f, ignoreTiles: true, bossPriority: true);
			if (potentialTarget == null)
			{
				DeathDust();
				base.Projectile.Kill();
				return;
			}
			ChooseNewTarget(potentialTarget);
			target = potentialTarget;
		}
		if (!Main.projectile[BookProjectileIndex].active)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		Time++;
		for (int i = 0; i < 3; i++)
		{
			LemniscateAngle += (float)Math.PI / 100f;
			DetermineLemniscatePosition(target);
		}
		if (Time >= (float)(310 * base.Projectile.MaxUpdates))
		{
			base.Projectile.Kill();
			return;
		}
		float effectRate = MathHelper.Lerp(0.4f, 1f, Time / (float)(310 * base.Projectile.MaxUpdates - 40));
		float random = Main.rand.NextFloat();
		base.Projectile.Opacity = Utils.GetLerpValue(310 * base.Projectile.MaxUpdates, 250f * (float)base.Projectile.MaxUpdates, Time, clamped: true);
		if (random <= effectRate)
		{
			SpawnSwirlingDust(target);
		}
		if (Main.myPlayer == base.Projectile.owner && random <= effectRate / 20f && !target.immortal && !target.dontTakeDamage && !target.townNPC)
		{
			int damage = (int)player.GetTotalDamage<MagicDamageClass>().ApplyTo(2520f);
			int strike = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, base.Projectile.owner, target.whoAmI);
			if (Main.projectile.IndexInRange(strike))
			{
				Main.projectile[strike].DamageType = DamageClass.Magic;
			}
		}
		if ((int)Time % 30 == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<EternityHoming>()] < 40)
		{
			int homerCount = 6;
			int damage2 = (int)player.GetTotalDamage<MagicDamageClass>().ApplyTo(672f);
			for (int j = 0; j < homerCount; j++)
			{
				Vector2 velocity = Vector2.UnitY.RotatedBy((float)Math.PI * 2f / (float)homerCount * (float)j).RotatedByRandom(0.30000001192092896) * 10f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center + velocity * 4f, velocity, ModContent.ProjectileType<EternityHoming>(), damage2, 0f, base.Projectile.owner, TargetNPCIndex);
			}
		}
	}

	public void DeathDust()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Color newColor = default(Color);
		for (int i = 0; i < 44; i++)
		{
			Vector2 center = base.Projectile.Center;
			((Color)(ref newColor))._002Ector(245, 112, 218);
			Dust dust = Dust.NewDustPerfect(center, 16, null, 0, newColor);
			dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 6f);
			dust.noGravity = true;
		}
	}

	public void DetermineLemniscatePosition(NPC target)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		float num = 2f / (3f - (float)Math.Cos(2f * LemniscateAngle));
		float outwardMultiplier = MathHelper.Lerp(4f, 220f, Utils.GetLerpValue(0f, 120f, Time, clamped: true));
		Vector2 lemniscateOffset = num * new Vector2((float)Math.Cos(LemniscateAngle), (float)Math.Sin(2f * LemniscateAngle) / 2f);
		base.Projectile.Center = target.Center + lemniscateOffset * outwardMultiplier;
	}

	public void ChooseNewTarget(NPC newTarget)
	{
		TargetNPCIndex = newTarget.whoAmI;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.owner == base.Projectile.owner && (proj.type == ModContent.ProjectileType<EternityCrystal>() || proj.type == ModContent.ProjectileType<EternityCircle>()))
			{
				proj.ai[0] = TargetNPCIndex;
				DeathDust();
			}
		}
	}

	public static void SpawnSwirlingDust(NPC target)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			float randomAngle = Main.rand.NextFloat() * ((float)Math.PI * 2f);
			float outwardnessFactor = Main.rand.NextFloat();
			Vector2 position = target.Center + randomAngle.ToRotationVector2() * MathHelper.Lerp(70f, 430f, outwardnessFactor);
			Vector2 velocity = (randomAngle - (float)Math.PI * 3f / 8f).ToRotationVector2() * (10f + 9f * Main.rand.NextFloat() + 4f * outwardnessFactor);
			Dust dust = Dust.NewDustPerfect(position, 267, velocity, 0, Main.rand.NextBool(3) ? Eternity.BlueColor : Eternity.PinkColor, 1.4f);
			dust.scale = 1.2f;
			dust.fadeIn = 0.25f + outwardnessFactor * 0.1f;
			dust.noGravity = true;
		}
	}

	public Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		float leftoverTimeScale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f;
		leftoverTimeScale *= 0.5f;
		Color val = Color.Lerp(Color.Black, Color.Magenta, 0.1f);
		Color tailColor = Color.Lerp(Color.Magenta, Color.Cyan, completionRatio * 0.5f + leftoverTimeScale);
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.61f, completionRatio, clamped: true), 0.4) * base.Projectile.Opacity;
		float fadeToMagenta = MathHelper.SmoothStep(0f, 1f, (float)Math.Pow(completionRatio, 0.6));
		return Color.Lerp(val, tailColor, fadeToMagenta) * opacity;
	}

	public static float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float widthInterpolant = Utils.GetLerpValue(0f, 0.12f, completionRatio, clamped: true);
		return MathHelper.SmoothStep(1f, 10f, widthInterpolant);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/EternityStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 84);
		return false;
	}
}
