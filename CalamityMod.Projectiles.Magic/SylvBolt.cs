using System;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SylvBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float HueInterpolant => ref base.Projectile.ai[0];

	public int Time
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public bool Vanishing
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = value.ToInt();
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 23;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 2;
		base.Projectile.penetrate = 12;
		base.Projectile.timeLeft = base.Projectile.extraUpdates * 90;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 0;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.004f;
		if (Vanishing)
		{
			Vanish();
		}
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		base.Projectile.scale = Utils.GetLerpValue(0f, 7f, Time, clamped: true);
		CreateMagicDust();
		Lighting.AddLight(base.Projectile.Center, Vector3.One * base.Projectile.Opacity * 0.45f);
	}

	private void Vanish()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 0f, 0.24f);
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.tileCollide = false;
		if (base.Projectile.Opacity <= 0.1f)
		{
			base.Projectile.Kill();
		}
	}

	private void CreateMagicDust()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 264);
			dust.color = ColorFunction(0f, Vector2.Zero);
			dust.scale *= 0.56f;
			dust.noGravity = true;
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (1f - completionRatio) * base.Projectile.Opacity;
		return CalamityUtils.MulticolorLerp(HueInterpolant, new Color(255, 193, 255), Color.White, new Color(127, 242, 255)) * opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - MathF.Pow(1f - Utils.GetLerpValue(0.05f, 0.2f, completionRatio * base.Projectile.Opacity, clamped: true), 2f)) * base.Projectile.Opacity * base.Projectile.scale * 22f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		MiscShaderData boltShader = GameShaders.Misc["CalamityMod:SylvestaffProjectile"];
		boltShader.SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: false, pixelate: false, boltShader), 80);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (!Vanishing && base.Projectile.penetrate < 2)
		{
			Vanishing = true;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.02f;
			base.Projectile.extraUpdates = 0;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (!Vanishing)
		{
			Vanishing = true;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.extraUpdates = 0;
			base.Projectile.netUpdate = true;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return !Vanishing;
	}
}
