using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LiliesOfFinalityBolt : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeBeforeHoming = 10;

	private const int TimeDying = 15;

	private SlotId LoopingSound;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private ref float RandomColorOffset => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.timeLeft = 10 + LiliesOfFinality.Ariane_BoltTimeHoming + 15;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.MinionHoming(LiliesOfFinality.MaxEnemyDistanceDetection, Main.player[base.Projectile.owner]);
		if (target != null && base.Projectile.timeLeft > 15 && base.Projectile.timeLeft < LiliesOfFinality.Ariane_BoltTimeHoming)
		{
			float turnRate = Utils.Remap(base.Projectile.timeLeft, LiliesOfFinality.Ariane_BoltTimeHoming, 15f, LiliesOfFinality.Ariane_MinTurnRate, LiliesOfFinality.Ariane_MaxTurnRate);
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(target.Center).ToRotation(), turnRate).ToRotationVector2() * LiliesOfFinality.Ariane_BoltProjectileSpeed;
		}
		if (base.Projectile.timeLeft < 14)
		{
			base.Projectile.velocity = Vector2.Zero;
			if (!Main.dedServ)
			{
				for (int i = 0; i < 2; i++)
				{
					Vector2 position = base.Projectile.position;
					int width = base.Projectile.width;
					int height = base.Projectile.height;
					int commonDustID = LiliesOfFinality.CommonDustID;
					float scale = Main.rand.NextFloat(0.8f, 1.2f);
					Dust.NewDustDirect(position, width, height, commonDustID, 0f, 0f, 0, default(Color), scale).noGravity = true;
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (!Main.dedServ)
		{
			if (Main.rand.NextBool())
			{
				Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f);
				int commonDustID2 = LiliesOfFinality.CommonDustID;
				Vector2? velocity = base.Projectile.velocity * Main.rand.NextFloat(0.01f, 0.05f);
				float scale = Main.rand.NextFloat(0.4f, 0.6f);
				Dust.NewDustPerfect(position2, commonDustID2, velocity, 0, default(Color), scale).noGravity = true;
			}
			if (SoundEngine.TryGetActiveSound(LoopingSound, out ActiveSound sound) && sound.IsPlaying)
			{
				sound.Position = base.Projectile.Center;
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		RandomColorOffset = Main.rand.NextFloat(100f);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ArianeShot")
		{
			Volume = 0.2f
		};
		LoopingSound = SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = 15;
		SoundStyle style = SoundID.Item70 with
		{
			Volume = 0.5f,
			Pitch = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (SoundEngine.TryGetActiveSound(LoopingSound, out ActiveSound sound))
		{
			sound.Stop();
		}
	}

	private float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(0f, 32f, MathF.Pow(completionRatio, 0.4f));
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		float offsetTime = Main.GlobalTimeWrappedHourly + RandomColorOffset;
		float fadeToEnd = MathHelper.Lerp(0.65f, 1f, (float)Math.Cos((0f - offsetTime) * 3f) * 0.5f + 0.5f);
		float fadeOpacity = Utils.GetLerpValue(1f, 0.64f, completionRatio, clamped: true) * base.Projectile.Opacity;
		Color endColor = Color.Lerp(Color.Fuchsia, Color.Red, (float)Math.Sin(completionRatio * (float)Math.PI * 1.6f - offsetTime * 4f) * 0.5f + 0.5f);
		return Color.Lerp(Color.White, endColor, fadeToEnd) * fadeOpacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 32);
		return false;
	}
}
