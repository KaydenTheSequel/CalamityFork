using System;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SHPS : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	private float AIState;

	private const float HomingRange = 560f;

	private NPC Target;

	private Projectile ToSuckTowards;

	public float RandomAnglingStrength;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[1];

	public bool IsAPickupSoul
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = value.ToInt();
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 14;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
	}

	public override bool? CanDamage()
	{
		return Timer >= 25f && !IsAPickupSoul;
	}

	public override void AI()
	{
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		if (Timer >= 25f)
		{
			if (!IsAPickupSoul)
			{
				float npcDistCheck = 560f;
				int index = -1;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(base.Projectile))
					{
						float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
						if (currentNPCDist < npcDistCheck)
						{
							npcDistCheck = currentNPCDist;
							index = n.whoAmI;
						}
					}
				}
				if (index == -1)
				{
					AIState = 0f;
				}
				else
				{
					Target = Main.npc[index];
					AIState = 1f;
				}
			}
			else
			{
				ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Projectile p = enumerator2.Current;
					if (p.type == ModContent.ProjectileType<SHPV>() && p.Colliding(p.Hitbox, base.Projectile.Hitbox))
					{
						if (AIState != 2f)
						{
							AIState = 2f;
							base.Projectile.ExpandHitboxBy(70);
						}
						ToSuckTowards = p;
						break;
					}
					AIState = 0f;
				}
			}
		}
		float aIState = AIState;
		if (aIState != 0f)
		{
			if (aIState != 1f)
			{
				if (aIState == 2f)
				{
					Vector2 idealVelocity = base.Projectile.SafeDirectionTo(ToSuckTowards.ModProjectile<SHPV>().TipPosition + Main.player[base.Projectile.owner].velocity) * 30f;
					base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.1f);
					if (Vector2.Distance(base.Projectile.Center, ToSuckTowards.ModProjectile<SHPV>().TipPosition) < 70f)
					{
						ToSuckTowards.ModProjectile<SHPV>().SoulColors.Add(base.Projectile.ai[0]);
						base.Projectile.Kill();
					}
				}
			}
			else
			{
				base.Projectile.extraUpdates = 1;
				float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(Target.Center).ToRotation(), 0.15f).ToRotationVector2() * speed;
			}
		}
		else
		{
			base.Projectile.extraUpdates = 0;
			if (Timer % 30f == 1f)
			{
				RandomAnglingStrength = Main.rand.NextFloat(-0.16f, 0.16f);
			}
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(RandomAnglingStrength);
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 2.75f && IsAPickupSoul)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.96f;
			}
		}
		if (Main.rand.NextBool(12))
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f);
				Vector2 dustVelocity = base.Projectile.velocity * -1.2f;
				Dust obj = Dust.NewDustDirect(Scale: Main.rand.NextFloat(0.6f, 0.8f), Position: position, Width: 1, Height: 1, Type: 43, SpeedX: dustVelocity.X, SpeedY: dustVelocity.Y, Alpha: 0, newColor: SHPB.FindColorForSoul((int)base.Projectile.ai[0]));
				obj.noGravity = true;
				obj.noLight = false;
				obj.noLightEmittence = false;
			}
		}
		if (Main.rand.NextBool(6) && IsAPickupSoul)
		{
			for (int j = 0; j < 3; j++)
			{
				Vector2 smokeVelocity = Main.rand.NextVector2Circular(1f, 1f) * 0.65f;
				int smokeLifetime = Main.rand.Next(30, 45);
				float smokeScale = Main.rand.NextFloat(0.15f, 0.3f);
				float smokeOpacity = Main.rand.NextFloat(0.75f, 0.9f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeVelocity, SHPB.FindColorForSoul((int)base.Projectile.ai[0]), smokeLifetime, smokeScale, smokeOpacity, 0.02f, glowing: true));
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		BezierCurve curve = new BezierCurve(base.Projectile.oldPos);
		for (int i = 0; i < 35; i++)
		{
			Vector2 position = curve.Evaluate(Main.rand.NextFloat());
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 3f;
			Dust obj = Dust.NewDustDirect(Scale: Main.rand.NextFloat(1.2f, 1.8f), Position: position, Width: 1, Height: 1, Type: 43, SpeedX: dustVelocity.X, SpeedY: dustVelocity.Y, Alpha: 0, newColor: SHPB.FindColorForSoul((int)base.Projectile.ai[0]));
			obj.noGravity = true;
			obj.noLight = false;
			obj.noLightEmittence = false;
		}
		for (int j = 0; j < 12; j++)
		{
			Vector2 dustVelocity2 = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(1.8f, 2.4f);
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity2.X, dustVelocity2.Y, 0, SHPB.FindColorForSoul((int)base.Projectile.ai[0]), dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public float SoulWidthFunction(float completion, Vector2 _)
	{
		float maxBodyWidth = base.Projectile.scale * 24f;
		float curveRatio = 0.15f;
		if (completion < curveRatio)
		{
			return MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio;
		}
		return Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f);
	}

	public Color SoulColorFunction(float completion, Vector2 _)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Color tipColor = Color.Lerp(SHPB.FindColorForSoul((int)base.Projectile.ai[0]), Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(SHPB.FindColorForSoul((int)base.Projectile.ai[0]), tipColor, completion);
	}

	public float SoulCoreWidthFunction(float completion, Vector2 _)
	{
		float maxBodyWidth = base.Projectile.scale * 14f;
		float curveRatio = 0.15f;
		if (completion < curveRatio)
		{
			return MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio;
		}
		return Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f);
	}

	public Color SoulCoreColorFunction(float completion, Vector2 _)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Color tipColor = Color.Lerp(Color.White, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(Color.White, tipColor, completion);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(SoulWidthFunction, SoulColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length * 2);
		Vector2[] soulCoreLength = base.Projectile.oldPos.Take(8).ToArray();
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(soulCoreLength, new PrimitiveSettings(SoulCoreWidthFunction, SoulCoreColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), soulCoreLength.Length * 2);
	}
}
