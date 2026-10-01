using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PristineSecondary : ModProjectile, ILocalizedModType, IModType
{
	public float LightPower;

	public Color FogColor;

	public float FogRotation;

	public int boomTime;

	public bool Ignited;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Magic/RancorFog";

	public ref float ScaleFactor => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 150);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 230;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 2;
	}

	public override void AI()
	{
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == (float)boomTime)
		{
			Ignited = true;
		}
		if (Ignited)
		{
			if (base.Projectile.ai[2] == (float)boomTime)
			{
				base.Projectile.damage *= 7;
				FogColor = Color.Lerp(Color.OrangeRed, Color.Goldenrod, Main.rand.NextFloat());
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastImpact");
				style.Volume = 0.4f;
				style.Pitch = Main.rand.NextFloat(0.5f, 0.6f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			FogColor = Color.Lerp(Color.OrangeRed, Color.Goldenrod, Main.rand.NextFloat(0.5f, 1f));
			if (base.Projectile.timeLeft > boomTime + 1)
			{
				base.Projectile.timeLeft = (int)((float)base.Projectile.timeLeft * 0.95f);
			}
			base.Projectile.scale *= 1.12f;
			ScaleFactor *= 1.06f;
			if (base.Projectile.ai[2] > 1f)
			{
				base.Projectile.ai[2]--;
			}
		}
		if (FogRotation == 0f)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.62f, 1.15f);
			FogRotation = Main.rand.NextFloat((float)Math.PI * 2f);
			ref Color fogColor = ref FogColor;
			((Color)(ref fogColor)).G = (byte)(((Color)(ref fogColor)).G + (byte)Main.rand.Next(10, 81));
		}
		ScaleFactor += 0.014f;
		ScaleFactor = MathHelper.Clamp(ScaleFactor, 0f, base.Projectile.scale);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref FogColor)).ToVector3() * ScaleFactor);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + FogRotation;
		Projectile projectile = base.Projectile;
		projectile.velocity *= Main.rand.NextFloat(0.95f, 0.99f);
		base.Projectile.Opacity = Utils.GetLerpValue(280f, 135f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(0f, 90f, base.Projectile.timeLeft, clamped: true);
		if (Main.dedServ)
		{
			return;
		}
		Color newColor = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16 + 6);
		Vector3 val = ((Color)(ref newColor)).ToVector3();
		float lightPowerBelow = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
		LightPower = MathHelper.Lerp(LightPower, lightPowerBelow, 0.15f);
		if (base.Projectile.timeLeft < 220)
		{
			Vector2 vel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(7f, 25f) * base.Projectile.Opacity * base.Projectile.scale;
			if (Main.rand.NextBool(40))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + vel * 4f * base.Projectile.Opacity, vel * 0.05f, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 25, Main.rand.NextFloat(0.9f, 1.2f) * (Ignited ? 1.8f : 1f), FogColor * base.Projectile.Opacity * 0.4f, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, Ignited ? 0f : Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: false, Ignited ? 0.5f : 0f));
			}
			if (Main.rand.NextBool(30))
			{
				Vector2 center = base.Projectile.Center;
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity = vel * 0.25f * (Ignited ? 2f : 1f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.55f, 0.95f) * base.Projectile.Opacity * (Ignited ? 2f : 1f);
				dust.color = FogColor;
			}
		}
		if (!(base.Projectile.Opacity > 0.5f) || !Ignited)
		{
			return;
		}
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile2 = Main.projectile[x];
			if (Vector2.Distance(base.Projectile.Center, projectile2.Center) <= 200f * base.Projectile.scale && projectile2.active && projectile2.type == ModContent.ProjectileType<PristineSecondary>() && base.Projectile.ai[2] == 2f && projectile2.ai[2] == 0f && projectile2.Opacity > 0.5f && projectile2 != base.Projectile && base.Projectile.ai[1] < 100f)
			{
				projectile2.ai[2] = boomTime;
				base.Projectile.ai[1] = base.Projectile.ai[1] + 1f;
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		return !(base.Projectile.Opacity < 0.3f) && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * ScaleFactor * 0.5f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Ignited)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 1200);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float opacity = Utils.GetLerpValue(0f, 0.08f, LightPower, clamped: true) * base.Projectile.Opacity * 0.7f;
		Color drawColor = FogColor * opacity;
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, base.Projectile.rotation, texture.Size() * 0.5f, ScaleFactor, (SpriteEffects)0);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}

	public PristineSecondary()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		FogColor = Color.Orchid;
		boomTime = PristineFury.boomTime;
		base._002Ector();
	}
}
