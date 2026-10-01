using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EonBolt : ModProjectile, ILocalizedModType, IModType
{
	public NPC target;

	private Particle Head;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/GalaxiaBolt";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Hue => ref base.Projectile.ai[0];

	public ref float HomingStrenght => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 80;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Head == null)
		{
			Head = new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.White, Main.hslToRgb(Hue, 100f, 50f), 1.2f, 2, 0.06f, 3f, needed: true);
			GeneralParticleHandler.SpawnParticle(Head);
		}
		else
		{
			Head.Position = base.Projectile.Center + base.Projectile.velocity * 0.5f;
			Head.Time = 0;
			Head.Scale += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.02f * base.Projectile.scale;
		}
		if (target == null)
		{
			target = base.Projectile.Center.ClosestNPCAt(812f);
		}
		else if (base.Projectile.velocity.AngleBetween(target.Center - base.Projectile.Center) < (float)Math.PI)
		{
			float idealDirection = base.Projectile.AngleTo(target.Center);
			float updatedDirection = base.Projectile.velocity.ToRotation().AngleTowards(idealDirection, HomingStrenght);
			base.Projectile.velocity = updatedDirection.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * 0.995f;
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		if (Main.rand.NextBool(2))
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Color.Lerp(Color.DodgerBlue, Color.MediumVioletRed, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 20, Main.rand.NextFloat(0.6f, 1.2f) * base.Projectile.scale, 0.28f, 0f, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Main.hslToRgb(Hue, 1f, 0.7f), 15, Main.rand.NextFloat(0.4f, 0.7f) * base.Projectile.scale, 0.8f, 0f, glowing: true, 0.05f, required: true));
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		float fadeToEnd = MathHelper.Lerp(0.65f, 1f, (float)Math.Cos((0f - Main.GlobalTimeWrappedHourly) * 3f) * 0.5f + 0.5f);
		float fadeOpacity = Utils.GetLerpValue(1f, 0.64f, completionRatio, clamped: true) * base.Projectile.Opacity;
		Color endColor = Color.Lerp(Main.hslToRgb(Hue, 1f, 0.8f), Color.PaleTurquoise, (float)Math.Sin(completionRatio * (float)Math.PI * 1.6f - Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
		return Color.Lerp(Color.White, endColor, fadeToEnd) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float expansionCompletion = (float)Math.Pow(1f - completionRatio, 3.0);
		return MathHelper.Lerp(0f, 22f * base.Projectile.scale * base.Projectile.Opacity, expansionCompletion);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GalaxiaBolt", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, 0.5f), base.Projectile.rotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
