using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class StomperSlam : ModProjectile, ILocalizedModType, IModType
{
	public float scaleFromFall;

	public float damageScaleFromFall;

	public int timeLeft = 60;

	public bool ableToHit = true;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 160;
		base.Projectile.height = 160;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 60;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft <= 40)
		{
			ableToHit = false;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			scaleFromFall = base.Projectile.ai[0] / 20f + 0.5f;
			damageScaleFromFall = base.Projectile.ai[0] / 40f;
			base.Projectile.damage = (int)(300f * damageScaleFromFall + 300f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/GravistarSlam");
			style.Volume = 0.75f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int particleCount = (int)(10f * scaleFromFall);
			for (int i = 0; i < particleCount; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SquareParticle(base.Projectile.Center + Main.rand.NextVector2Circular(scaleFromFall * 74f, scaleFromFall * 74f), Main.rand.NextVector2Circular(2.5f, 2.5f), affectedByGravity: false, 120, 3.5f + Main.rand.NextFloat(0.6f), Color.Lerp(Color.Cyan, Color.LightCyan, 0.75f)));
			}
			base.Projectile.localAI[0]++;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		Texture2D telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		GameShaders.Misc["CalamityMod:CircularGradientWithEdge"].UseOpacity(0.75f * (float)base.Projectile.timeLeft / (float)timeLeft);
		GameShaders.Misc["CalamityMod:CircularGradientWithEdge"].UseColor(Color.Lerp(Color.Cyan, Color.LightCyan, 0.5f));
		GameShaders.Misc["CalamityMod:CircularGradientWithEdge"].UseSecondaryColor(Color.White);
		GameShaders.Misc["CalamityMod:CircularGradientWithEdge"].UseSaturation(scaleFromFall);
		GameShaders.Misc["CalamityMod:CircularGradientWithEdge"].Apply();
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, lightColor, 0f, telegraphBase.Size() / 2f, scaleFromFall * 156f, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override bool? CanDamage()
	{
		if (!ableToHit)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, scaleFromFall * 78f, targetHitbox);
	}
}
