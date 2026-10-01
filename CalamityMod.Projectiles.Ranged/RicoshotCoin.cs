using System;
using CalamityMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class RicoshotCoin : ModProjectile, ILocalizedModType, IModType
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

	public static float CoinTossForce = 7.33f;

	public static float MaxIntraCoinRicoshotDistance = 1000f;

	public static readonly float RicoshotSearchDistance = 2000f;

	public static float SuperpredictionRatio = 0.1f;

	public static float CopperBonus = 0.7f;

	public static float CopperMulticoinBonus = 0.2f;

	public static float SilverBonus = 1.5f;

	public static float SilverMulticoinBonus = 0.5f;

	public static float GoldBonus = 2f;

	public static float GoldMulticoinBonus = 0.85f;

	internal static readonly int UpdateCount = 4;

	internal static readonly int CoinLifetime = UpdateCount * CalamityUtils.SecondsToFrames(7);

	private static readonly int FadeoutTime = UpdateCount * 30;

	private static readonly float ForceFadeDistance = 2000f;

	public static int CritDelayFrames = 22;

	internal static int RicochetPause = UpdateCount * 22;

	public new string LocalizationCategory => "Projectiles.Ranged";

	internal static int CritDelayTime => UpdateCount * CritDelayFrames;

	internal ref float CoinType => ref base.Projectile.ai[0];

	internal ref float ShotFreezeTimer => ref base.Projectile.ai[1];

	public bool HasBeenShot => base.Projectile.localAI[0] > 0f;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 60;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.MaxUpdates = UpdateCount;
		base.Projectile.timeLeft = CoinLifetime;
		base.Projectile.scale = 1.1f;
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
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		if (ShotFreezeTimer > 0f)
		{
			ShotFreezeTimer--;
			if (ShotFreezeTimer <= 0f)
			{
				base.Projectile.Kill();
				return;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		int dustID = 244;
		float coinType = CoinType;
		Color newColor;
		if (coinType != 1f)
		{
			if (coinType == 2f)
			{
				Vector2 center = base.Projectile.Center;
				newColor = Color.Goldenrod;
				Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.2f);
				dustID = 246;
			}
			else
			{
				Vector2 center2 = base.Projectile.Center;
				newColor = Color.DarkOrange;
				Lighting.AddLight(center2, ((Color)(ref newColor)).ToVector3() * 0.11f);
			}
		}
		else
		{
			Vector2 center3 = base.Projectile.Center;
			newColor = Color.White;
			Lighting.AddLight(center3, ((Color)(ref newColor)).ToVector3() * 0.14f);
			dustID = 245;
		}
		if (Main.rand.NextBool(10))
		{
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int type = dustID;
			newColor = default(Color);
			Dust.NewDustDirect(position, width, height, type, 0f, 0f, 0, newColor).noGravity = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.5f;
		if (base.Projectile.FinalExtraUpdate())
		{
			float coinGravity = Player.defaultGravity / (float)base.Projectile.MaxUpdates;
			base.Projectile.velocity.Y += coinGravity;
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

	public override void OnKill(int timeLeft)
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		bool spawnRefundCoin = !HasBeenShot;
		if (!spawnRefundCoin)
		{
			float coinType = CoinType;
			bool flag = coinType != 0f && ((coinType == 1f) ? (base.Projectile.localAI[0] == 1f && Main.rand.NextFloat() < 0.5f) : (coinType == 2f && base.Projectile.localAI[0] == 1f));
			spawnRefundCoin = flag;
		}
		if (spawnRefundCoin)
		{
			float coinType = CoinType;
			short num = (short)((coinType == 1f) ? 72 : ((coinType != 2f) ? 71 : 73));
			int itemID = num;
			int coin = Item.NewItem(base.Projectile.GetSource_DropAsItem(), base.Projectile.Center, Vector2.One, itemID);
			if (Main.item[coin].TryGetGlobalItem<GrabRangeGlobalItem>(out var grabRangeItem))
			{
				grabRangeItem.grabRangeMultiplier = UsedCoinGrabRangeMultiplier;
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(21, -1, -1, null, coin, 1f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		int numFrames = Main.projFrames[base.Type];
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector((int)CoinType * tex.Width / 3, base.Projectile.frame * tex.Height / numFrames, tex.Width / 3 - 2, tex.Height / numFrames - 2);
		float fadingOpacity = Math.Clamp((float)base.Projectile.timeLeft / (float)FadeoutTime, 0f, 1f);
		Color alphaColor = base.Projectile.GetAlpha(lightColor) * fadingOpacity;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, frame, alphaColor, base.Projectile.rotation, frame.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float x = Math.Clamp((float)(CoinLifetime - base.Projectile.timeLeft) / (float)CritDelayTime, 0f, 2f);
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
			float coinType = CoinType;
			Color val = ((coinType == 1f) ? Color.Silver : ((coinType != 2f) ? Color.DarkOrange : Color.Goldenrod));
			Color shineColor = val;
			Main.EntitySpriteDraw(bloomTex, base.Projectile.Center - Main.screenPosition, null, shineColor * sheenOpacity * 0.3f, (float)Math.PI / 2f, bloomTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(shineTex, base.Projectile.Center - Main.screenPosition, null, shineColor * sheenOpacity, (float)Math.PI / 2f, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}
}
