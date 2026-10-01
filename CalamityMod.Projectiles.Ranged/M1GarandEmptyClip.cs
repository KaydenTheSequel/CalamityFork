using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class M1GarandEmptyClip : ModProjectile, ILocalizedModType, IModType
{
	internal static readonly SoundStyle BlingSound = new SoundStyle("CalamityMod/Sounds/Custom/Ultrabling")
	{
		PitchVariance = 0.5f
	};

	internal static readonly SoundStyle BlingHitSound = new SoundStyle("CalamityMod/Sounds/Custom/UltrablingHit")
	{
		PitchVariance = 0.5f
	};

	private static Asset<Texture2D> sheenAsset;

	private static Asset<Texture2D> bloomAsset;

	public static float UsedCoinGrabRangeMultiplier = 5f;

	public static float ClipTossForce = 7.33f;

	public float midAirRot;

	public static float MaxIntraClipRicoshotDistance = 1000f;

	public static readonly float RicoshotSearchDistance = 2000f;

	public static float SuperpredictionRatio = 0.1f;

	public static float ClipBonus = 2.5f;

	public static float ClipMulticlipBonus = 0.9f;

	internal static readonly int UpdateCount = 4;

	internal static readonly int ClipLifetime = UpdateCount * CalamityUtils.SecondsToFrames(7);

	private static readonly int FadeoutTime = UpdateCount * 30;

	private static readonly float ForceFadeDistance = 2000f;

	public static int CritDelayFrames = 22;

	internal static int RicochetPause = UpdateCount * 22;

	public new string LocalizationCategory => "Projectiles.Ranged";

	internal static int CritDelayTime => UpdateCount * CritDelayFrames;

	internal ref float ShotFreezeTimer => ref base.Projectile.ai[1];

	public bool HasBeenShot => base.Projectile.localAI[0] > 0f;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 60;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 1;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.MaxUpdates = UpdateCount;
		base.Projectile.timeLeft = ClipLifetime;
		base.Projectile.scale = 1.5f;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return ShotFreezeTimer <= 0f;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		if (ShotFreezeTimer > 0f)
		{
			ShotFreezeTimer--;
			if (ShotFreezeTimer <= 0f)
			{
				base.Projectile.Kill();
				return;
			}
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.LightGray;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.2f);
		if (Main.rand.NextBool(10))
		{
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			newColor = default(Color);
			Dust.NewDustDirect(position, width, height, 31, 0f, 0f, 0, newColor).noGravity = true;
		}
		midAirRot += 0.05f;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.5f + midAirRot;
		if (base.Projectile.FinalExtraUpdate())
		{
			float clipGravity = Player.defaultGravity / (float)base.Projectile.MaxUpdates;
			base.Projectile.velocity.Y += clipGravity / 1.75f;
		}
		if (base.Projectile.timeLeft > FadeoutTime && base.Projectile.Center.Distance(Owner.MountedCenter) > ForceFadeDistance)
		{
			base.Projectile.timeLeft = FadeoutTime;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		return base.OnTileCollide(oldVelocity);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame();
		float fadingOpacity = Math.Clamp((float)base.Projectile.timeLeft / (float)FadeoutTime, 0f, 1f);
		Main.EntitySpriteDraw(color: base.Projectile.GetAlpha(lightColor) * fadingOpacity, texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, rotation: base.Projectile.rotation, origin: frame.Size() / 2f, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		float x = Math.Clamp((float)(ClipLifetime - base.Projectile.timeLeft) / (float)CritDelayTime, 0f, 2f);
		float sheenOpacity = Math.Clamp(Math.Min(MathF.Pow(x + 0.1f, 10f), MathF.Pow(x - 2.1f, 10f)), 0f, 2f);
		if (sheenOpacity > 0f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			if (sheenAsset == null)
			{
				sheenAsset = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2);
			}
			Texture2D shineTex = sheenAsset.Value;
			if (bloomAsset == null)
			{
				bloomAsset = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
			}
			Texture2D bloomTex = bloomAsset.Value;
			Vector2 shineScale = default(Vector2);
			((Vector2)(ref shineScale))._002Ector(1f, 3f - sheenOpacity * 2f);
			Color shineColor = Color.LightGray;
			Main.EntitySpriteDraw(bloomTex, base.Projectile.Center - Main.screenPosition, null, shineColor * sheenOpacity * 0.3f, (float)Math.PI / 2f, bloomTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(shineTex, base.Projectile.Center - Main.screenPosition, null, shineColor * sheenOpacity, (float)Math.PI / 2f, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}
}
