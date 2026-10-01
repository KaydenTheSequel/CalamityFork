using System;
using System.Linq;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LucreciaBolt : ModProjectile, ILocalizedModType, IModType
{
	public static float MaxWidth = 30f;

	public static Asset<Texture2D> TrailTex;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 13;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 19;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 0;
		base.Projectile.timeLeft = 420;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 12;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color whiteSmoke = Color.WhiteSmoke;
		Lighting.AddLight(center, ((Color)(ref whiteSmoke)).ToVector3() * 0.4f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.01f;
		base.Projectile.scale = Utils.GetLerpValue(0f, 0.1f, (float)base.Projectile.timeLeft / 600f, clamped: true);
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				base.Projectile.oldPos[i] = base.Projectile.position;
			}
			base.Projectile.localAI[0] = 1f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.damage = 0;
			base.Projectile.Opacity -= 0.04f;
			if (base.Projectile.Opacity <= 0f)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			float variance = Main.rand.NextFloat(-0.6f, 0.6f);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(target.Center, dustStyle);
			dust.scale = Main.rand.NextFloat(1.2f, 1.6f) - Math.Abs(variance);
			dust.velocity = (base.Projectile.velocity * 1.5f).RotatedBy(variance) * Main.rand.NextFloat(1.2f, 1.5f) * (1f - Math.Abs(variance));
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, Main.rand.NextFloat(0f, 1f));
		}
		base.Projectile.ai[1] = 1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, 0.5f);
		((Color)(ref val)).A = 0;
		return val * base.Projectile.Opacity;
	}

	public float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(1f, 0.4f, completionRatio, clamped: true) * (float)Math.Sin(Math.Acos(1f - Utils.GetLerpValue(0f, 0.08f, completionRatio, clamped: true))) * Utils.GetLerpValue(0f, 0.1f, (float)base.Projectile.timeLeft / 600f, clamped: true) * (MaxWidth * 0.265f);
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, completionRatio) * 0.2f * base.Projectile.Opacity;
	}

	public Color MiniTrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, completionRatio) * base.Projectile.Opacity;
	}

	public float MiniTrailWidth(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return TrailWidth(completionRatio, vertexPos) * 5.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 412)
		{
			return false;
		}
		Color mainColor = Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, ((float)Main.timeForVisualEffects * 0.5f + (float)base.Projectile.whoAmI * 0.12f) % 1f);
		Color secondaryColor = Color.Lerp(Color.MediumPurple, Color.CornflowerBlue, ((float)Main.timeForVisualEffects * 0.5f + (float)base.Projectile.whoAmI * 0.12f + 0.2f) % 1f);
		Main.spriteBatch.EnterShaderRegion();
		if (TrailTex == null)
		{
			TrailTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2);
		}
		GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(TrailTex);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
		Vector2 offset = base.Projectile.Size * 0.5f;
		Vector2[] positions = base.Projectile.oldPos.Select(delegate(Vector2 p)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return p - offset;
		}).ToArray();
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(TrailWidth, TrailColor, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(MiniTrailWidth, MiniTrailColor, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
