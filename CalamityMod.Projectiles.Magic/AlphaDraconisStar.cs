using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AlphaDraconisStar : ModProjectile, ILocalizedModType, IModType
{
	private NPC target;

	private Player closestPlayer;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 5;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
		base.Projectile.scale = 0.5f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 300;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void AI()
	{
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Vector2 goalPos = default(Vector2);
		((Vector2)(ref goalPos))._002Ector(base.Projectile.ai[0], base.Projectile.ai[1]);
		float velLength = ((Vector2)(ref base.Projectile.velocity)).Length();
		if (base.Projectile.ai[2] == 0f)
		{
			float maxDist = 57600f;
			if (base.Projectile.FinalExtraUpdate())
			{
				target = null;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC item = enumerator.Current;
					if (item.CanBeChasedBy(item) && item.DistanceSQ(goalPos) < maxDist)
					{
						maxDist = item.DistanceSQ(goalPos);
						target = item;
					}
				}
			}
			if (target != null)
			{
				if (target.active)
				{
					goalPos = target.Center;
					base.Projectile.ai[0] = goalPos.X;
					base.Projectile.ai[1] = goalPos.Y;
					base.Projectile.Calamity().HomingTarget = target.whoAmI;
				}
				else
				{
					target = null;
				}
			}
			else
			{
				base.Projectile.Calamity().HomingTarget = -1;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity += base.Projectile.DirectionTo(goalPos);
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			if (base.Projectile.velocity.HasNaNs())
			{
				base.Projectile.velocity = Vector2.UnitY;
			}
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= velLength;
			if (base.Projectile.Center.Y > goalPos.Y + 100f)
			{
				base.Projectile.ai[2] = 1f;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.Calamity().HomingTarget = -1;
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.98f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 1f && base.Projectile.ai[2] == 1f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.ai[2] == 2f)
			{
				base.Projectile.rotation += 0.05f;
				base.Projectile.scale = MathHelper.Min(base.Projectile.scale + 0.02f, 1f);
				if (base.Projectile.FinalExtraUpdate())
				{
					closestPlayer = null;
					float closestDis = 102400f;
					ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						Player player = enumerator2.Current;
						float dis = base.Projectile.DistanceSQ(player.Center);
						if (dis < closestDis)
						{
							closestDis = dis;
							closestPlayer = player;
						}
					}
				}
				if (closestPlayer != null)
				{
					Projectile projectile4 = base.Projectile;
					projectile4.velocity += base.Projectile.DirectionTo(closestPlayer.Center);
					if (base.Projectile.Distance(closestPlayer.Center) < 32f)
					{
						closestPlayer.Calamity().StratusStarburst++;
						base.Projectile.Kill();
					}
				}
			}
		}
		if (Main.rand.NextBool(10))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPlasmaDust, -base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(0.4f, 1f);
			dust.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
			dust.noGravity = true;
			dust.color = Color.CadetBlue;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 0f)
		{
			if (Main.rand.NextFloat() <= 0.33f)
			{
				base.Projectile.ai[2] = 2f;
				base.Projectile.timeLeft = 600 * base.Projectile.MaxUpdates;
			}
			else
			{
				base.Projectile.ai[2] = 1f;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= Main.rand.NextFloat(0.9f, 1.1f);
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] != 2f)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDustPixelated>(), -base.Projectile.velocity);
				dust.scale = Main.rand.NextFloat(0.4f, 0.6f);
				dust.velocity = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.2f, 1f);
				dust.color = Color.CadetBlue;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		Texture2D mainTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		Vector2 drawOrigin = mainTexture.Size() / 2f;
		float drawScale = base.Projectile.scale;
		float drawRotation = base.Projectile.rotation;
		float glowSine = ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 14f) + 1f) / 2f;
		float pulse = MathHelper.Lerp(0.25f, 0.8f, glowSine);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		for (int i = 0; i < 12; i++)
		{
			Vector2 val = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2();
			Vector2 innerOffset = val * (1.4f * pulse);
			SpriteBatch spriteBatch = Main.spriteBatch;
			Vector2 val2 = drawPosition + innerOffset;
			Color val3 = Color.SkyBlue;
			((Color)(ref val3)).A = 0;
			spriteBatch.Draw(mainTexture, val2, (Rectangle?)null, val3 * 0.225f, drawRotation, drawOrigin, drawScale, spriteEffects, 0f);
			Vector2 outerOffset = val * (1.75f * pulse);
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Vector2 val4 = drawPosition + outerOffset;
			val3 = Color.DeepSkyBlue;
			((Color)(ref val3)).A = 0;
			spriteBatch2.Draw(mainTexture, val4, (Rectangle?)null, val3 * 0.1f, drawRotation, drawOrigin, drawScale, spriteEffects, 0f);
		}
		Main.spriteBatch.End(out var ss);
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		using RenderTargetLease lease = RenderTargetPool.Shared.Rent(device, Main.screenWidth / 2, Main.screenHeight / 2, RenderTargetDescriptor.Default);
		using (lease.Scope(preserveContents: true, Color.Transparent))
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 1f)
			{
				GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
				PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, delegate
				{
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					//IL_0010: Unknown result type (might be due to invalid IL or missing references)
					return base.Projectile.Size * 0.5f;
				}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), base.Projectile.oldPos.Length + 32);
				Vector2[] fireCoreLength = base.Projectile.oldPos.Take(8).ToArray();
				GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
				PrimitiveRenderer.RenderTrail(fireCoreLength, new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, delegate
				{
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					//IL_0010: Unknown result type (might be due to invalid IL or missing references)
					return base.Projectile.Size * 0.5f;
				}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), fireCoreLength.Length + 24);
			}
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 2f, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(in ss);
		return false;
	}

	public float FireWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = 32f * base.Projectile.scale;
		float curveRatio = 0.05f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Pow(completion / curveRatio, 0.5f) * maxBodyWidth));
		float pulseInterpolant = MathF.Cos((float)Math.PI * completion - Main.GlobalTimeWrappedHourly * 20f) * 0.5f + 0.5f;
		float additionalPulseWidth = MathHelper.Lerp(0f, 12f, pulseInterpolant);
		return (width + additionalPulseWidth) * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireColorFunction(float completion, Vector2 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.CadetBlue * 1.1f;
		Color endColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(val, endColor, completion) * base.Projectile.Opacity;
	}

	public float FireCoreWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = base.Projectile.scale * 8f;
		float curveRatio = 0.1f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio));
		return width * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireCoreColorFunction(float completion, Vector2 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Color cadetBlue = Color.CadetBlue;
		Color tipColor = Color.Lerp(cadetBlue, Color.Transparent, Utils.GetLerpValue(0.2f, 1f, completion, clamped: true));
		return Color.Lerp(Color.Lerp(cadetBlue, tipColor, completion), Color.White, 0.175f) * base.Projectile.Opacity;
	}
}
