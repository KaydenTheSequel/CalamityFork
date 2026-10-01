using System;
using System.IO;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class SnakeEyesProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool HasHitEnemy;

	public bool HasRedirected;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC TargetShot => Main.npc[(int)TargetID];

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(SnakeEyes.EnemyDistanceDetection, Owner);
		}
	}

	public ref float MinionID => ref base.Projectile.ai[0];

	public ref float TargetID => ref base.Projectile.ai[1];

	public ref float TimerToRedirect => ref base.Projectile.ai[2];

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
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HasHitEnemy);
		writer.Write(HasRedirected);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HasHitEnemy = reader.ReadBoolean();
		HasRedirected = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		if (HasHitEnemy && TimerToRedirect < SnakeEyes.TimeToRedirect)
		{
			TimerToRedirect++;
		}
		if (TimerToRedirect >= SnakeEyes.TimeToRedirect && !HasRedirected)
		{
			if (TargetShot != null && TargetShot.active)
			{
				TargetEnemy(TargetShot);
			}
			else if (Target != null)
			{
				TargetEnemy(Target);
			}
		}
		if (HasRedirected)
		{
			if (TargetShot != null && TargetShot.active)
			{
				FollowEnemy(TargetShot);
			}
			else if (Target != null)
			{
				FollowEnemy(Target);
			}
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int type = (HasRedirected ? 261 : 226);
			float scale = (HasRedirected ? 2f : 0.5f);
			Dust dust = Dust.NewDustDirect(position, width, height, type, 0f, 0f, 0, default(Color), scale);
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
		}
	}

	private void TargetEnemy(NPC target)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center) * SnakeEyes.ProjectileSpeed;
		HasRedirected = true;
		SoundStyle style = SoundID.Item92 with
		{
			Volume = 0.8f,
			Pitch = 0.5f,
			PitchVariance = 0.1f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		base.Projectile.netUpdate = true;
	}

	private void FollowEnemy(NPC target)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(target.Center) * SnakeEyes.ProjectileSpeed, 0.2f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		HasHitEnemy = true;
		if (HasRedirected && TargetShot == target)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (HasRedirected)
		{
			base.Projectile.ExpandHitboxBy(300);
			base.Projectile.Damage();
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 261, 0f, 0f, 0, default(Color), 2f).noGravity = true;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.White, Vector2.One, 0f, 0.05f, 1f + Main.rand.NextFloat(0.2f), 20));
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		}
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float arrowheadCutoff = 0.36f;
		float width = 39f;
		float minHeadWidth = 0.02f;
		float maxHeadWidth = width;
		if (completionRatio <= arrowheadCutoff)
		{
			width = MathHelper.Lerp(minHeadWidth, maxHeadWidth, Utils.GetLerpValue(0f, arrowheadCutoff, completionRatio, clamped: true));
		}
		return width;
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		float endFadeRatio = 0.41f;
		float completionRatioFactor = 2.7f;
		float globalTimeFactor = 5.3f;
		float endFadeFactor = 3.2f;
		float endFadeTerm = Utils.GetLerpValue(0f, endFadeRatio * 0.5f, completionRatio, clamped: true) * endFadeFactor;
		float startingInterpolant = (float)Math.Cos(completionRatio * completionRatioFactor - Main.GlobalTimeWrappedHourly * globalTimeFactor + endFadeTerm) * 0.5f + 0.5f;
		float colorLerpFactor = 0.6f;
		_003F val;
		Color val2;
		if (!HasRedirected)
		{
			val = new Color(0, 0, 0, 0);
		}
		else
		{
			val2 = Color.White;
			((Color)(ref val2)).A = 50;
			val = val2;
		}
		Color val3;
		if (!HasRedirected)
		{
			val2 = Color.Cyan;
			((Color)(ref val2)).A = 25;
			val3 = val2;
		}
		else
		{
			val2 = Color.White;
			((Color)(ref val2)).A = 100;
			val3 = val2;
		}
		Color val4 = Color.Lerp((Color)val, val3, startingInterpolant * colorLerpFactor);
		Color val5;
		if (!HasRedirected)
		{
			val2 = Color.DarkCyan;
			((Color)(ref val2)).A = 50;
			val5 = val2;
		}
		else
		{
			val5 = Color.White;
		}
		return Color.Lerp(val4, val5, MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, endFadeRatio, completionRatio, clamped: true)));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 92);
		return false;
	}
}
