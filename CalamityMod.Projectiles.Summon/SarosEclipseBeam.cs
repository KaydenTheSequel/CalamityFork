using System;
using System.Collections.Generic;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SarosEclipseBeam : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Player Owner => Main.player[base.Projectile.owner];

	private Vector2 SarosPos
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center + Vector2.UnitY * (Owner.gfxOffY + Owner.gravDir * -24f);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 10;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SarosPossession.SpawnSound with
		{
			pitch = -0.5f,
			MaxInstances = 5,
			SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
		};
		SoundEngine.PlaySound(in style, Owner.Center);
		base.Projectile.rotation = SarosPos.DirectionTo(Owner.Calamity().mouseWorld).ToRotation();
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.miscCounter % 10 == 0)
		{
			SoundStyle style = SoundID.DD2_BetsyFlameBreath with
			{
				Volume = 0.2f
			};
			SoundEngine.PlaySound(in style, Owner.Center);
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleLerp(SarosPos.DirectionTo(Owner.Calamity().mouseWorld).ToRotation(), 0.1f);
		base.Projectile.Center = SarosPos + base.Projectile.rotation.ToRotationVector2() * Utils.Remap(base.Projectile.timeLeft, 5f, 10f, 1600f, 0f);
		if (base.Projectile.timeLeft == 5 && Owner.channel)
		{
			base.Projectile.timeLeft++;
		}
		Owner.Calamity().sarosEclipseBeamUsage += 2;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), SarosPos, base.Projectile.Center, 80f, ref _);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= Utils.Remap(Owner.Calamity().sarosEclipseBeamUsage, 0f, 300f, 2f, 1f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 12; i++)
		{
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(3f, 5f);
			Color dustColor = Color.Lerp(Color.OrangeRed, Color.Gold, Main.rand.NextFloat(0.5f, 1f));
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public float FireWidthFunction(float completion, Vector2 vertexPos)
	{
		return MathHelper.Min(Utils.Remap(completion, 0f, 0.1f, 0f, 96f), Utils.Remap(completion, 0.9f, 1f, 96f, 0f)) * Utils.Remap(base.Projectile.timeLeft, 0f, 5f, 0f, 1f) * (0.75f + MathF.Pow(1f - (float)Owner.Calamity().sarosEclipseBeamUsage / 300f, 3f)) * ((base.Projectile.timeLeft > 5) ? (1f - (float)(base.Projectile.timeLeft - 5) / 5f) : 1f);
	}

	public Color FireColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(new Color(238, 226, 153), new Color(255, 191, 73), (MathF.Sin(completion * ((float)Math.PI * 2f) + Main.GlobalTimeWrappedHourly * 5f) + 1f) * 0.5f) * MathF.Pow(1f - completion, 0.5f);
	}

	public float FireCoreWidthFunction(float completion, Vector2 vertexPos)
	{
		return MathHelper.Min(Utils.Remap(completion, 0f, 0.1f, 0f, 32f), Utils.Remap(completion, 0.9f, 1f, 32f, 0f)) * Utils.Remap(base.Projectile.timeLeft, 0f, 5f, 0f, 1f) * (0.75f + MathF.Pow(1f - (float)Owner.Calamity().sarosEclipseBeamUsage / 300f, 3f)) * ((base.Projectile.timeLeft > 5) ? (1f - (float)(base.Projectile.timeLeft - 5) / 5f) : 1f);
	}

	public Color FireCoreColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return Color.Black * MathF.Pow(1f - completion, 0.5f);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> posList = new List<Vector2>();
		for (int i = 0; i <= 10; i++)
		{
			posList.Add(Vector2.Lerp(SarosPos, base.Projectile.Center, (float)i / 10f));
		}
		Vector2[] pos = posList.ToArray();
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(pos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, null, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), pos.Length + 32);
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(pos, new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, null, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), pos.Length + 24);
	}
}
