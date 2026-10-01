using System;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismTooth : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 80;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float ShootReach => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public ref float CanBreakTrees => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 90;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 52;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 4;
		base.Projectile.timeLeft = 80;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				base.Projectile.oldPos[i] = base.Projectile.position;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI * Time / 80f;
		if (Main.rand.NextBool(6) && (Time < 35f || Time > 45f))
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f), base.Projectile.velocity * Main.rand.NextFloat(0.6f, 0.9f), affectedByGravity: false, 22, Main.rand.NextFloat(0.3f, 0.9f), Main.hslToRgb((Time / 40f + Main.rand.NextFloat(-0.1f, 0.1f)) % 1f, 0.95f, 0.8f)));
		}
		Vector2 baseDirection = ((float)Math.PI * 2f * Time / 80f - (float)Math.PI / 2f).ToRotationVector2();
		baseDirection.X *= 0.25f;
		baseDirection.Y = baseDirection.Y * 0.5f + 0.5f;
		Vector2 positionOffset = baseDirection * ShootReach;
		if (Math.Abs(positionOffset.X) > 45f)
		{
			positionOffset.X = (float)Math.Sign(baseDirection.X) * 45f;
		}
		positionOffset = positionOffset.RotatedBy(base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f);
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter) + base.Projectile.velocity * 42f + positionOffset;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 12f, Time, clamped: true) * Utils.GetLerpValue(80f, 68f, 80 - base.Projectile.timeLeft, clamped: true);
		if (CanBreakTrees == 1f)
		{
			for (int j = 0; j < 20; j++)
			{
				Point pointToCheck = (base.Projectile.oldPos[j] + base.Projectile.Size * 0.5f).ToTileCoordinates();
				AbsolutelyFuckingAnnihilateTrees(pointToCheck.X, pointToCheck.Y);
			}
		}
		Lighting.AddLight(base.Projectile.Center, Vector3.One * 0.7f);
		Time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public void AbsolutelyFuckingAnnihilateTrees(int x, int y)
	{
		Tile tileAtPosition = CalamityUtils.ParanoidTileRetrieval(x, y);
		if (tileAtPosition.HasTile && Main.tileAxe[tileAtPosition.TileType] && WorldGen.CanKillTile(x, y))
		{
			AchievementsHelper.CurrentlyMining = true;
			WorldGen.KillTile(x, y);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 0, x, y);
			}
			AchievementsHelper.CurrentlyMining = false;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 24f * (1f - Utils.GetLerpValue(0.7f, 1f, completionRatio, clamped: true)) + 1f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		float hue = ((float)base.Projectile.identity % 9f / 9f + completionRatio * 0.7f) % 1f;
		return Color.Lerp(Color.White, Main.hslToRgb(hue, 0.95f, 0.55f), 0.35f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (Time <= 5f)
		{
			return true;
		}
		Vector2 generalOffset = base.Projectile.rotation.ToRotationVector2().RotatedBy(1.5707963705062866) * 15f;
		generalOffset += base.Projectile.rotation.ToRotationVector2() * -5f * (float)Math.Sin(base.Projectile.rotation);
		generalOffset *= Vector2.Zero;
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:PrismaticStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		int numPointsRendered = 22;
		int numPointsProvided = 80;
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos.Take(numPointsProvided).ToArray(), new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + generalOffset;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:PrismaticStreak"]), numPointsRendered);
		Main.spriteBatch.ExitShaderRegion();
		return true;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
