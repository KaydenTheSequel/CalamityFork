using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CoralSpoutHoldout : ModProjectile
{
	public static float MaxCharge = 50f;

	public static int ShotProjectiles = 5;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<CoralSpout>();

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Charge => ref base.Projectile.ai[0];

	public float ChargeProgress => MathHelper.Clamp(Charge, 0f, MaxCharge) / MaxCharge;

	public float FullChargeProgress => MathHelper.Clamp(Charge, 0f, MaxCharge * 1.5f) / (MaxCharge * 1.5f);

	public float Spread => (float)Math.PI / 2f * (1f - (float)Math.Pow(ChargeProgress, 1.5) * 0.95f);

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.channel)
		{
			base.Projectile.timeLeft = 2;
			Owner.itemTime = 25;
			Owner.itemAnimation = 25;
			Owner.heldProj = base.Projectile.whoAmI;
		}
		float pointingRotation = (Owner.Calamity().mouseWorld - Owner.MountedCenter).ToRotation();
		base.Projectile.Center = Owner.MountedCenter + pointingRotation.ToRotationVector2() * 40f;
		if (base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = CoralSpout.ChargeSound with
			{
				Pitch = 0.5f * ChargeProgress
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
			base.Projectile.soundDelay = 10;
		}
		if (Charge == (float)(int)(MaxCharge * 1.5f) && Owner.whoAmI == Main.myPlayer)
		{
			SoundStyle style = SoundID.Item30 with
			{
				Volume = SoundID.Item30.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
		}
		Charge++;
		if (Charge % 4f == 0f && Owner.GetModPlayer<CoralSpoutPlayer>().Symbiosis)
		{
			Charge++;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		float angle = (Owner.Calamity().mouseWorld - Owner.MountedCenter).ToRotation();
		float blinkage = 0f;
		if (Charge >= MaxCharge * 1.5f)
		{
			blinkage = (float)Math.Sin(MathHelper.Clamp((Charge - MaxCharge * 1.5f) / 15f, 0f, 1f) * ((float)Math.PI / 2f) + (float)Math.PI / 2f);
		}
		Effect effect = Filters.Scene["CalamityMod:SpreadTelegraph"].GetShader().Shader;
		effect.Parameters["centerOpacity"].SetValue(0.7f);
		effect.Parameters["mainOpacity"].SetValue((float)Math.Sqrt(ChargeProgress));
		effect.Parameters["halfSpreadAngle"].SetValue(Spread / 2f);
		EffectParameter obj = effect.Parameters["edgeColor"];
		Color val = Color.Lerp(Color.DeepSkyBlue, Color.Coral, blinkage);
		obj.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj2 = effect.Parameters["centerColor"];
		val = Color.Lerp(Color.DodgerBlue, Color.Coral, blinkage);
		obj2.SetValue(((Color)(ref val)).ToVector3());
		effect.Parameters["edgeBlendLength"].SetValue(0.07f);
		effect.Parameters["edgeBlendStrength"].SetValue(8f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(texture, Owner.MountedCenter - Main.screenPosition, null, Color.White, angle, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), 700f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		float mainAngle = (base.Projectile.Center - Owner.MountedCenter).ToRotation();
		if (FullChargeProgress < 1f)
		{
			SoundStyle style = SoundID.Item167 with
			{
				Volume = SoundID.Item167.Volume * 0.4f + 0.2f * ChargeProgress
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
			for (int i = 0; i < ShotProjectiles; i++)
			{
				float angleOffset = MathHelper.Lerp(Spread * -0.5f, Spread * 0.5f, (float)i / ((float)ShotProjectiles - 1f));
				Vector2 direction = (mainAngle + angleOffset).ToRotationVector2();
				int realDamage = base.Projectile.damage + (int)((double)CoralSpout.FullChargeExtraDamage * Math.Pow(ChargeProgress, CoralSpout.ChargeDamageBoostSteepness));
				if (Owner.GetModPlayer<CoralSpoutPlayer>().Symbiosis)
				{
					realDamage += CoralSpout.SymbiosisDamageBuff;
				}
				if (Owner.whoAmI == Main.myPlayer)
				{
					float speed = 10f + 15f * ChargeProgress;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.MountedCenter + direction * 30f, direction * speed, ModContent.ProjectileType<CoralSpike>(), realDamage, base.Projectile.knockBack, Owner.whoAmI, ChargeProgress);
				}
				Color pulseColor = (Main.rand.NextBool() ? Color.Coral : Color.DeepSkyBlue);
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Owner.MountedCenter + direction * 44f, Vector2.Zero, pulseColor, new Vector2(0.5f, 1f), direction.ToRotation(), 0.04f, 0.2f, 30));
			}
		}
		else
		{
			SoundEngine.PlaySound(in SoundID.Item42, Owner.MountedCenter);
			Vector2 direction2 = mainAngle.ToRotationVector2();
			if (Owner.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.MountedCenter + direction2 * 30f, direction2 * 35f, ModContent.ProjectileType<ManaChargedCoral>(), base.Projectile.damage * (ShotProjectiles + 1), base.Projectile.knockBack, Owner.whoAmI);
			}
			Color pulseColor2 = (Main.rand.NextBool() ? Color.Coral : Color.DeepSkyBlue);
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, pulseColor2, new Vector2(0.5f, 1f), direction2.ToRotation(), 0.05f, 0.34f + Main.rand.NextFloat(0.3f), 30));
		}
	}
}
