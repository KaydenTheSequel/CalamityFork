using System;
using System.Collections.Generic;
using CalamityMod.DataStructures;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MeteorFistProj : ModProjectile, ILocalizedModType, IModType
{
	public List<VerletSimulatedSegment> Wire;

	private const int Lifetime = 210;

	private const int WireSegments = 15;

	private const float MaxSpeed = 22.5f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 210;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Timer++;
		if (Wire == null || Wire.Count < 15)
		{
			Wire = new List<VerletSimulatedSegment>(15);
			for (int i = 0; i < 15; i++)
			{
				VerletSimulatedSegment segment = new VerletSimulatedSegment(Owner.Center + Vector2.UnitY * (float)i * 10f);
				Wire.Add(segment);
			}
			Wire[0].locked = true;
			Wire[Wire.Count - 1].locked = true;
		}
		if (base.Projectile.numHits == 0)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position - base.Projectile.velocity * 0.5f, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 0.5f);
			dust.scale *= 2f + Main.rand.NextFloat();
			dust.velocity *= 0.2f;
			dust.noGravity = true;
			if (((Vector2)(ref base.Projectile.velocity)).Length() >= 7f)
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 10f, Vector2.UnitX.RotatedBy(base.Projectile.rotation + (float)Math.PI) * 4f, Color.Orange, 5, 0.33f, 0.75f));
			}
		}
		if (base.Projectile.numHits == 0)
		{
			(Owner.ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			float guidedSpeed = ((Timer < 60f && !base.Projectile.Calamity().stealthStrike) ? 4f : ((Timer + (base.Projectile.Calamity().stealthStrike ? 60f : 0f)) / 7.5f));
			if (guidedSpeed > 22.5f)
			{
				guidedSpeed = 22.5f;
			}
			float guidedTurnStrength = MathHelper.Lerp(0.05f, base.Projectile.Calamity().stealthStrike ? 0.32f : 0.18f, Timer / 210f);
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(Owner.ClampedMouseWorld()).ToRotation(), guidedTurnStrength).ToRotationVector2() * guidedSpeed * Owner.Calamity().rogueVelocity;
			if (base.Projectile.timeLeft == 2)
			{
				SetUpLeftoverWireEffect();
			}
		}
		else
		{
			base.Projectile.velocity.X *= 0.9f;
			base.Projectile.velocity.Y += 0.2f;
			if (base.Projectile.velocity.Y > 10f)
			{
				base.Projectile.velocity.Y = 10f;
			}
		}
		base.Projectile.rotation = ((base.Projectile.numHits > 0) ? 0f : (base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f));
		Wire[0].oldPosition = Wire[0].position;
		Wire[0].position = Owner.Center;
		Wire[Wire.Count - 1].oldPosition = Wire[0].position;
		Wire[Wire.Count - 1].position = base.Projectile.Center + Vector2.UnitY.RotatedBy(base.Projectile.rotation) * ((base.Projectile.numHits > 0) ? 14f : 8f);
		Wire = VerletSimulatedSegment.SimpleSimulation(Wire, 10f, 1);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (base.Projectile.numHits == 0)
		{
			SetUpLeftoverWireEffect();
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits != 0)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(24, 120);
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.Calamity().stealthStrike)
		{
			Vector2 meteorSpawn = target.Center + new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(-650f, -750f));
			Vector2 meteorVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(meteorSpawn, target, 15f, 2);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), meteorSpawn, meteorVel, ModContent.ProjectileType<MeteorFistMeteorite>(), base.Projectile.damage, base.Projectile.knockBack * 2f, base.Projectile.owner, target.whoAmI);
		}
		SetUpLeftoverWireEffect();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 120);
	}

	private void SetUpLeftoverWireEffect()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.numHits++;
		Timer = 0f;
		base.Projectile.timeLeft = 90;
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		for (int k = 0; k < 30; k++)
		{
			int boomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Main.dust[boomDust2].noGravity = true;
			Dust obj = Main.dust[boomDust2];
			obj.velocity *= 5f;
			boomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Dust obj2 = Main.dust[boomDust2];
			obj2.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(base.Projectile.Center.X - 24f, base.Projectile.Center.Y - 24f);
		for (int g = 1; g <= 3; g++)
		{
			float velocityMult = (float)g * 0.33f;
			for (int spawn = 0; spawn < 2; spawn++)
			{
				int type = Main.rand.Next(61, 64);
				int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj3 = Main.gore[smoke];
				obj3.velocity *= velocityMult;
			}
		}
	}

	public float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 0.4f;
	}

	public Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		float fadeThreshold = 60f;
		float opacity = ((base.Projectile.numHits > 0 && Timer > fadeThreshold) ? MathHelper.Lerp(0.75f, 0f, (Timer - fadeThreshold) / 30f) : 0.75f);
		return Color.Orange * opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> wirePoints = new List<Vector2>();
		if (Wire != null && Wire.Count > 0)
		{
			for (int i = 0; i < Wire.Count; i++)
			{
				wirePoints.Add(Wire[i].position);
			}
		}
		PrimitiveRenderer.RenderTrail(wirePoints, new PrimitiveSettings(WidthFunction, ColorFunction), 75);
		return base.Projectile.numHits == 0;
	}
}
