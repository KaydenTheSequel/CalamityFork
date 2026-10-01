using System;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ScarletDevilProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float ShootTimer => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ScarletDevil";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 45;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 45;
		base.Projectile.width = 108;
		base.Projectile.height = 108;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.55f, 0.25f, 0f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (!Main.dedServ)
		{
			for (int i = 0; i < ((!base.Projectile.Calamity().stealthStrike || !Main.rand.NextBool()) ? 1 : 2); i++)
			{
				Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 130, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 0, new Color(255, 255, 255), 0.85f);
			}
		}
		ShootTimer++;
		if (!base.Projectile.Calamity().stealthStrike && base.Projectile.oldPos.Length != 6)
		{
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[6];
		}
		if ((ShootTimer %= 5f) == 0f && !base.Projectile.Calamity().stealthStrike && base.Projectile.owner == Main.myPlayer)
		{
			GenerateSideBullets(2, MathHelper.ToRadians(15f));
		}
	}

	internal void GenerateSideBullets(int totalBullets, float rotationalOffset)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < totalBullets; i++)
		{
			Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2((0f - base.Projectile.velocity.X) / 3f, (0f - base.Projectile.velocity.Y) / 3f), (double)MathHelper.Lerp(0f - rotationalOffset, rotationalOffset, (float)(i / (totalBullets - 1))), default(Vector2));
			for (int j = 0; j < 2; j++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<ScarletDevilBullet>(), (int)((double)base.Projectile.damage * 0.03), 0f, base.Projectile.owner);
				perturbedSpeed *= 1.05f;
			}
		}
	}

	internal void SpawnOnStealthStrikeBullets()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		float starSpeed = 25f;
		for (int i = 0; i < 40; i++)
		{
			Vector2 shootVelocity = ((float)Math.PI * 2f * (float)i / 40f).ToRotationVector2() * starSpeed;
			int bullet = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + shootVelocity, shootVelocity, ModContent.ProjectileType<ScarletDevilBullet>(), (int)((double)base.Projectile.damage * 0.01), 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(bullet))
			{
				Main.projectile[bullet].Calamity().stealthStrike = true;
			}
		}
		int pointsOnStar = 6;
		for (int k = 0; k < 2; k++)
		{
			for (int j = 0; j < pointsOnStar; j++)
			{
				float f = 4.712389f - (float)j * ((float)Math.PI * 2f) / (float)pointsOnStar;
				float nextAngle = 4.712389f - (float)((j + 3) % pointsOnStar) * ((float)Math.PI * 2f) / (float)pointsOnStar;
				if (k == 1)
				{
					nextAngle = 4.712389f - (float)(j + 2) * ((float)Math.PI * 2f) / (float)pointsOnStar;
				}
				Vector2 start = f.ToRotationVector2();
				Vector2 end = nextAngle.ToRotationVector2();
				int pointsOnStarSegment = 18;
				for (int l = 0; l < pointsOnStarSegment; l++)
				{
					Vector2 shootVelocity2 = Vector2.Lerp(start, end, (float)l / (float)pointsOnStarSegment) * starSpeed;
					int bullet2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + shootVelocity2, shootVelocity2, ModContent.ProjectileType<ScarletDevilBullet>(), (int)((double)base.Projectile.damage * 0.01), 0f, base.Projectile.owner);
					if (Main.projectile.IndexInRange(bullet2))
					{
						Main.projectile[bullet2].Calamity().stealthStrike = true;
					}
				}
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(250, 250, 250);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item122, base.Projectile.position);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(150);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ScarletBlast>(), (int)((double)base.Projectile.damage * 0.0075), 0f, base.Projectile.owner);
		if (base.Projectile.Calamity().stealthStrike)
		{
			SpawnOnStealthStrikeBullets();
			Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, 305, (int)Math.Round((double)hit.Damage * 0.005), 0.5f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(150);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ScarletBlast>(), (int)((double)base.Projectile.damage * 0.0075), 0f, base.Projectile.owner);
		if (base.Projectile.Calamity().stealthStrike)
		{
			SpawnOnStealthStrikeBullets();
		}
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float widthRatio = Utils.GetLerpValue(0f, 0.1f, completionRatio, clamped: true);
		return MathHelper.Lerp(0f, 110f, widthRatio) * MathHelper.Clamp(1f - (float)Math.Pow(completionRatio, 0.4), 0.37f, 1f);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		float colorIncrement = (float)Math.Pow(completionRatio, 0.5);
		if (float.IsNaN(colorIncrement))
		{
			return Color.DarkRed;
		}
		float colorFade = 1f - Utils.GetLerpValue(0.6f, 0.98f, completionRatio, clamped: true);
		return Color.Lerp(CalamityUtils.MulticolorLerp(colorIncrement, Color.White, Color.DarkRed, Color.Wheat, Color.IndianRed) * MathHelper.Lerp(0f, 1.4f, colorFade), Color.DarkRed, (float)Math.Pow(completionRatio, 3.0));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.Calamity().stealthStrike)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], new Color(100, 100, 100));
			return true;
		}
		GameShaders.Misc["CalamityMod:OverpoweredTouhouSpearShader"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 86f;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:OverpoweredTouhouSpearShader"]), 60);
		Texture2D spearTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < 7; i++)
		{
			Color drawColor = Color.Lerp(lightColor, Color.White, 0.8f) * 0.2f;
			((Color)(ref drawColor)).A = 0;
			Vector2 drawOffset = ((float)i / 7f * ((float)Math.PI * 2f)).ToRotationVector2() * 2f;
			Main.EntitySpriteDraw(spearTexture, base.Projectile.Center - Main.screenPosition + drawOffset, null, drawColor, base.Projectile.rotation, spearTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
