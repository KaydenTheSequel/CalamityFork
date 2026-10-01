using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SarosSunfire : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	private NPC target;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 24;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void OnSpawn(IEntitySource source)
	{
	}

	public override void AI()
	{
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] > 1f)
		{
			base.Projectile.ai[0]--;
			base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
		}
		if (target != null && base.Projectile.localNPCImmunity[target.whoAmI] <= 0 && target.active && !target.dontTakeDamage)
		{
			base.Projectile.Calamity().HomingTarget = target.whoAmI;
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleLerp(base.Projectile.DirectionTo(target.Center).ToRotation(), 0.5f * (1f - base.Projectile.ai[0] / 120f)).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		else
		{
			target = GetTargetInRange(2000f);
		}
		if (base.Projectile.damage <= 0)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 4f;
			base.Projectile.timeLeft = (int)MathHelper.Min(8f, (float)base.Projectile.timeLeft);
		}
	}

	private NPC GetTargetInRange(float range)
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player.HasMinionAttackTargetNPC && Main.npc[player.MinionAttackTargetNPC].CanBeChasedBy() && base.Projectile.localNPCImmunity[player.MinionAttackTargetNPC] <= 0 && base.Projectile.IsInRangeOfMeOrMyOwner(Main.npc[player.MinionAttackTargetNPC], range, out var _, out var _, out var _))
		{
			return Main.npc[player.MinionAttackTargetNPC];
		}
		NPC gotTarget = null;
		float currentDistance = range;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (base.Projectile.localNPCImmunity[npc.whoAmI] <= 0)
			{
				float myDistance2 = npc.Distance(base.Projectile.Center);
				if (npc.CanBeChasedBy() && myDistance2 < currentDistance)
				{
					currentDistance = myDistance2;
					gotTarget = npc;
				}
			}
		}
		return gotTarget;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target != this.target)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		for (int i = 0; i < 12; i++)
		{
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(3f, 5f);
			Color dustColor = Color.Lerp(Color.OrangeRed, Color.Gold, Main.rand.NextFloat(0.5f, 1f));
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = true;
		}
	}

	public override bool? CanDamage()
	{
		return base.Projectile.ai[0] <= 90f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public float FireWidthFunction(float completion, Vector2 vertexPos)
	{
		float maxBodyWidth = 48f * base.Projectile.scale;
		float curveRatio = 0.2f;
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

	public Color FireColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(new Color(238, 226, 153), Color.Transparent, completion);
	}

	public float FireCoreWidthFunction(float completion, Vector2 vertexPos)
	{
		float maxBodyWidth = base.Projectile.scale * 32f;
		float curveRatio = 0.25f;
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

	public Color FireCoreColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 191, 73);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length + 32);
		Vector2[] fireCoreLength = base.Projectile.oldPos.Take(8).ToArray();
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(fireCoreLength, new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), fireCoreLength.Length + 24);
	}
}
