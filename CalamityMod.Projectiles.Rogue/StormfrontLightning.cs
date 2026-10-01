using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Rogue;

public class StormfrontLightning : ModProjectile, ILocalizedModType, IModType
{
	private int noTileHitCounter = 81;

	public bool HasPlayedSound;

	public const int Lifetime = 45;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float BaseTurnAngleRatio => ref base.Projectile.ai[1];

	public ref float AccumulatedXMovementSpeeds => ref base.Projectile.localAI[0];

	public ref float BranchingIteration => ref base.Projectile.localAI[1];

	public ref float InitialVelocityAngle => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/LightningProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 50;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 35;
		base.Projectile.height = 35;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 4;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 45;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AccumulatedXMovementSpeeds);
		writer.Write(BranchingIteration);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AccumulatedXMovementSpeeds = reader.ReadSingle();
		BranchingIteration = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		noTileHitCounter--;
		if (noTileHitCounter == 0)
		{
			base.Projectile.tileCollide = true;
		}
		if (Main.rand.NextBool(10))
		{
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[d].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
		}
		base.Projectile.frameCounter++;
		base.Projectile.oldPos[1] = base.Projectile.oldPos[0];
		float adjustedTimeLife = base.Projectile.timeLeft / base.Projectile.MaxUpdates;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 9f, adjustedTimeLife, clamped: true) * Utils.GetLerpValue(45f, 42f, adjustedTimeLife, clamped: true);
		base.Projectile.scale = base.Projectile.Opacity;
		if (!HasPlayedSound)
		{
			SoundStyle style = CommonCalamitySounds.LightningSound with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style, Main.player[base.Projectile.owner].Center);
			HasPlayedSound = true;
		}
		Vector2 center = base.Projectile.Center;
		Color val = new Color(Main.rand.Next(20, 100), 204, 250);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3());
		if (base.Projectile.frameCounter < base.Projectile.extraUpdates * 2)
		{
			return;
		}
		base.Projectile.frameCounter = 0;
		float originalSpeed = MathHelper.Min(20f, ((Vector2)(ref base.Projectile.velocity)).Length());
		UnifiedRandom unifiedRandom = new UnifiedRandom((int)BaseTurnAngleRatio);
		int turnTries = 0;
		Vector2 newBaseDirection = -Vector2.UnitY;
		do
		{
			BaseTurnAngleRatio = unifiedRandom.Next() % 100;
			Vector2 potentialBaseDirection = (BaseTurnAngleRatio / 100f * ((float)Math.PI * 2f)).ToRotationVector2();
			potentialBaseDirection.Y = 0f - Math.Abs(potentialBaseDirection.Y);
			bool canChangeLightningDirection = true;
			if (potentialBaseDirection.Y > -0.2f)
			{
				canChangeLightningDirection = false;
			}
			if (potentialBaseDirection.X < -0.2f || potentialBaseDirection.X > 0.2f)
			{
				canChangeLightningDirection = false;
			}
			if (canChangeLightningDirection)
			{
				newBaseDirection = potentialBaseDirection;
			}
			turnTries++;
		}
		while (turnTries < 20);
		if (base.Projectile.velocity != Vector2.Zero)
		{
			base.Projectile.velocity = newBaseDirection.RotatedBy(InitialVelocityAngle + (float)Math.PI / 2f) * originalSpeed;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return CalamityUtils.Convert01To010(completionRatio) * base.Projectile.scale * (float)base.Projectile.width;
	}

	public Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.MulticolorLerp((float)Math.Sin((float)base.Projectile.identity / 3f + completionRatio * 20f + Main.GlobalTimeWrappedHourly * 1.1f) * 0.5f + 0.5f, new Color(Main.rand.Next(20, 100), 204, 250), new Color(Main.rand.Next(20, 100), 204, 250));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item93, base.Projectile.position);
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 150);
		Sparks();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item93, base.Projectile.position);
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 150);
		Sparks();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].Apply();
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.3f;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"]), 10);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit53, base.Projectile.Center);
		Sparks();
	}

	public void Sparks()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 sparkS = default(Vector2);
			for (int index = 0; index < 4; index++)
			{
				CalamityUtils.RandomVelocity(-100f, 10f, 200f, 0.01f);
				((Vector2)(ref sparkS))._002Ector(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-10f, 0f));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkS, ModContent.ProjectileType<Stormfrontspark>(), 0, 0f, base.Projectile.owner);
			}
		}
	}
}
