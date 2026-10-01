using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TerraSigilLargeRock : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 255;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		Time++;
		base.Projectile.rotation += 0.05f;
		if (base.Projectile.timeLeft > 247)
		{
			base.Projectile.scale *= 1.05f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Asset<Texture2D> ghostTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/TerraSigilLargeRockGhost", (AssetRequestMode)2);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), drawColor, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		if (Time < 8f)
		{
			float fadeFactor = Utils.GetLerpValue(0f, 8f, Time, clamped: true);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive);
			Main.EntitySpriteDraw(ghostTexture.Value, base.Projectile.Center - Main.screenPosition, texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), Color.White * (1f - fadeFactor), base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact with
			{
				Volume = 0.8f,
				Pitch = -0.4f + (float)i * 0.25f,
				MaxInstances = 2
			}, base.Projectile.Center);
		}
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.numHits > 0)
		{
			int numRocks = 5;
			float spreadAngle = (float)Math.PI * 2f / (float)numRocks;
			for (int j = 0; j < numRocks; j++)
			{
				float rotation = spreadAngle * (float)j;
				Vector2 launchVelocity = Utils.RotatedBy(new Vector2(1f, 0f), (double)rotation, default(Vector2)) * 20f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, launchVelocity, ModContent.ProjectileType<TerraSigilMediumRock>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		for (int k = 0; k < 28; k++)
		{
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? ModContent.DustType<TerraSigilDust>() : 262, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f));
				dust.noGravity = false;
				dust.scale = Main.rand.NextFloat(0.85f, 1.25f);
				dust.noGravity = true;
				dust.fadeIn = 0.3f;
				dust.velocity *= 2.2f;
				dust.alpha = 100;
			}
			else
			{
				Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (Vector2.One * Main.rand.NextFloat(3f, 8f)).RotatedByRandom(6.2831854820251465), "CalamityMod/Particles/SmallSmoke", affectedByGravity: true, Main.rand.Next(15, 31), base.Projectile.scale * Main.rand.NextFloat(0.05f, 0.1f) * 4f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 1f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-2f, 2f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.5f, 0.5f)));
			}
		}
	}
}
