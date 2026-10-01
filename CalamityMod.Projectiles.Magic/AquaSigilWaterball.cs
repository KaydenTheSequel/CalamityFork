using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AquaSigilWaterball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 108;
		base.Projectile.height = 94;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 10f, Time, clamped: true);
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter % 4 == 3)
			{
				base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			}
			return;
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.scale += 0.05f;
		if (base.Projectile.scale >= 1.25f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.timeLeft > 5)
		{
			base.Projectile.timeLeft = 5;
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
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Asset<Texture2D> ghostTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AquaSigilWaterballGhost", (AssetRequestMode)2);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), drawColor, base.Projectile.rotation, new Vector2(49f, 160f), base.Projectile.scale, (SpriteEffects)0);
		if (base.Projectile.ai[1] > 0f)
		{
			float fadeFactor = Utils.GetLerpValue(5f, 0f, base.Projectile.timeLeft, clamped: true);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive);
			Main.EntitySpriteDraw(ghostTexture.Value, base.Projectile.Center - Main.screenPosition, texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), Color.White * fadeFactor, base.Projectile.rotation, new Vector2(49f, 160f), base.Projectile.scale * (1f + fadeFactor * 0.2f), (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
		base.Projectile.ai[1] = 1f;
		base.Projectile.penetrate = -1;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/AquaSigilExplosion");
		style.Volume = 1f;
		style.PitchVariance = 0.1f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 40; i++)
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.85f, 1.2f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-22f, -30f), Main.rand.NextFloat(-4f, 4f)), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), Main.rand.NextBool(5) ? 187 : 180, vel * Main.rand.NextFloat(0.1f, 0.9f) + new Vector2(0f, -2f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(1.1f, 1.7f);
		}
		for (int j = 0; j < 8; j++)
		{
			Vector2 vel2 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.85f, 1.2f);
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-22f, -30f), Main.rand.NextFloat(-4f, 4f)), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), vel2 * 0.8f, ModContent.ProjectileType<AquaSigilWaterdroplet>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Utils.RotatedBy(new Vector2(-26f, 0f), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), Vector2.Zero, ModContent.ProjectileType<AquaSigilWaterballExplosion>(), (int)((float)base.Projectile.damage * 3.85f), base.Projectile.knockBack, base.Projectile.owner);
			projectile.ai[1] = 260f;
			projectile.localAI[1] = Main.rand.NextFloat(0.1f, 0.2f);
			projectile.netUpdate = true;
		}
	}
}
