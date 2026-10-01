using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TerraSigil : ModProjectile, ILocalizedModType, IModType
{
	private bool spawnedProjectile;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 74);
		base.Projectile.friendly = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		Projectile parent = Main.projectile[(int)base.Projectile.ai[0]];
		bool parentActive = parent != null && parent.active && parent.type == ModContent.ProjectileType<SigilSet>();
		if (!parentActive && base.Projectile.ai[2] == 0f)
		{
			base.Projectile.Kill();
			return;
		}
		int i = (int)base.Projectile.ai[1];
		float dist = ((i % 3 == 0) ? 270f : 280f);
		float extraRot = ((i % 3 == 0) ? 0f : ((i % 3 == 1) ? MathHelper.ToRadians(-3.33f) : MathHelper.ToRadians(3.33f)));
		Vector2 sigilPos = parent.Center + (Vector2.UnitX.RotatedBy(MathHelper.Lerp(0f, (float)Math.PI * 2f, (float)i / 6f)) * dist).RotatedBy(parent.rotation + extraRot);
		base.Projectile.Center = sigilPos;
		if (base.Projectile.ai[2] > 0f)
		{
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] >= 35f && !spawnedProjectile)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
				style.Volume = 0.45f;
				style.PitchVariance = 0.1f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				_ = Main.player[base.Projectile.owner];
				Vector2 targetDirection = base.Projectile.Center.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX);
				spawnedProjectile = true;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, targetDirection * 32f, ModContent.ProjectileType<TerraSigilLargeRock>(), base.Projectile.damage * 2, base.Projectile.knockBack, base.Projectile.owner);
			}
			if (base.Projectile.localAI[0] >= 50f)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			base.Projectile.scale = parent.scale;
			base.Projectile.rotation = 0f;
			base.Projectile.alpha = parent.alpha;
		}
		if (parentActive)
		{
			base.Projectile.timeLeft = parent.timeLeft;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D mainTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D blankTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlankSigil", (AssetRequestMode)2).Value;
		float finalScale = base.Projectile.scale;
		float alphaOpacity = 1f - (float)base.Projectile.alpha / 255f;
		float maskOpacity = 0f;
		if (base.Projectile.ai[2] > 0f)
		{
			float animationTime = base.Projectile.localAI[0];
			maskOpacity = ((!(animationTime <= 24f)) ? 1f : Utils.GetLerpValue(0f, 24f, animationTime, clamped: true));
			if (animationTime >= 35f)
			{
				float scaleFactor = Utils.GetLerpValue(35f, 50f, animationTime, clamped: true);
				finalScale = MathHelper.Lerp(base.Projectile.scale, 0f, scaleFactor);
				alphaOpacity = MathHelper.Lerp(alphaOpacity, 0f, scaleFactor);
			}
		}
		Main.EntitySpriteDraw(mainTexture, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * alphaOpacity, base.Projectile.rotation, mainTexture.Size() / 2f, finalScale, (SpriteEffects)0);
		if (base.Projectile.ai[2] > 0f)
		{
			Main.EntitySpriteDraw(blankTexture, base.Projectile.Center - Main.screenPosition, null, Color.White * maskOpacity, base.Projectile.rotation, blankTexture.Size() / 2f, finalScale, (SpriteEffects)0);
		}
		return false;
	}
}
