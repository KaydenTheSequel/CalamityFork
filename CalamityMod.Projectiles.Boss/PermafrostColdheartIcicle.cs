using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PermafrostColdheartIcicle : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 80;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => ModContent.GetInstance<ColdheartIcicle>().Texture;

	public NPC Permafrost
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[2]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[2]];
		}
	}

	public ref float ShootReach => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 36;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 52;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 80;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		if (Permafrost == null || !Permafrost.active)
		{
			base.Projectile.Kill();
			return;
		}
		int permafrostBulletHellCounter = Permafrost.ModNPC<SupremeCalamitas>().bulletHellCounter2;
		if ((permafrostBulletHellCounter <= 1800 || permafrostBulletHellCounter >= 2700) && (permafrostBulletHellCounter <= 3600 || permafrostBulletHellCounter >= 4500))
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI * Time / 80f;
		Vector2 baseDirection = ((float)Math.PI * 2f * Time / 80f - (float)Math.PI / 2f).ToRotationVector2();
		baseDirection.X *= 0.25f;
		baseDirection.Y = baseDirection.Y * 0.5f + 0.5f;
		Vector2 positionOffset = baseDirection * ShootReach;
		if (Math.Abs(positionOffset.X) > 45f)
		{
			positionOffset.X = (float)Math.Sign(baseDirection.X) * 45f;
		}
		positionOffset = positionOffset.RotatedBy(base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f);
		Vector2 permafrostRotatedPosition = Permafrost.Center;
		float rotation = Permafrost.rotation;
		Vector2 vector = Permafrost.Bottom + new Vector2(0f, Permafrost.gfxOffY);
		Vector2 vector2 = new Vector2(0f, -4f) + Utils.RotatedBy(new Vector2(0f, 4f), (double)rotation, default(Vector2));
		permafrostRotatedPosition.Y += Permafrost.gfxOffY;
		permafrostRotatedPosition = vector + (permafrostRotatedPosition - vector).RotatedBy(rotation) + vector2;
		base.Projectile.Center = permafrostRotatedPosition + base.Projectile.velocity * 42f + positionOffset;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 12f, Time, clamped: true) * Utils.GetLerpValue(80f, 68f, 80 - base.Projectile.timeLeft, clamped: true);
		for (int i = 0; i < 20; i++)
		{
			Point pointToCheck = (base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f).ToTileCoordinates();
			AbsolutelyFuckingAnnihilateTrees(pointToCheck.X, pointToCheck.Y);
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
		return Color.Cyan;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 24f * (1f - Utils.GetLerpValue(0.7f, 1f, completionRatio, clamped: true)) + 1f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.Cyan * base.Projectile.Opacity;
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
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (Time <= 5f)
		{
			return true;
		}
		Vector2 generalOffset = base.Projectile.rotation.ToRotationVector2().RotatedBy(1.5707963705062866) * 15f;
		generalOffset += base.Projectile.rotation.ToRotationVector2() * -5f * (float)Math.Sin(base.Projectile.rotation);
		Vector2 oldPosition = base.Projectile.oldPos[1];
		base.Projectile.oldPos[1] = base.Projectile.oldPos[0] - base.Projectile.rotation.ToRotationVector2() * Vector2.Distance(base.Projectile.oldPos[0], base.Projectile.oldPos[1]);
		if (base.Projectile.oldPos[1].HasNaNs())
		{
			base.Projectile.oldPos[1] = oldPosition;
		}
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:PrismaticStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + generalOffset;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:PrismaticStreak"]), 65);
		Main.spriteBatch.ExitShaderRegion();
		return true;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(47, 300);
	}
}
