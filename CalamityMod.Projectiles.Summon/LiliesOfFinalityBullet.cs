using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LiliesOfFinalityBullet : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.timeLeft = 300;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = LiliesOfFinality.Elster_BulletMaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
			Vector2? velocity = base.Projectile.velocity * Main.rand.NextFloat(0.01f, 0.05f);
			float scale = Main.rand.NextFloat(1f, 1.2f);
			Dust.NewDustPerfect(position, 64, velocity, 0, default(Color), scale).noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.timeLeft = 10;
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float trailPoint = 0.1f;
		if (!(completionRatio > trailPoint))
		{
			return Utils.Remap(completionRatio, trailPoint, 0f, 16f, 0f);
		}
		return Utils.Remap(completionRatio, trailPoint, 1f, 16f, 0f);
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.DarkGoldenrod, Color.LightGoldenrodYellow, completionRatio);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:TrailStreak"]));
	}
}
