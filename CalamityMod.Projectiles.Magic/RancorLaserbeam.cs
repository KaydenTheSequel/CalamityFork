using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class RancorLaserbeam : ModProjectile, ILocalizedModType, IModType
{
	public const float MaxLaserLength = 3330f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public Projectile MagicCircle
	{
		get
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if ((float)p.identity == base.Projectile.ai[0] && p.owner == base.Projectile.owner)
				{
					return p;
				}
			}
			return null;
		}
	}

	public ref float LaserLength => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.channel || Owner.noItems || Owner.CCed || MagicCircle == null)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale + 0.15f, 0.05f, 2f);
		base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction);
		base.Projectile.Center = MagicCircle.Center;
		float[] laserLengthSamplePoints = new float[24];
		Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, base.Projectile.scale * 8f, 3330f, laserLengthSamplePoints);
		LaserLength = laserLengthSamplePoints.Average();
		UpdateAim();
		base.Projectile.damage = (int)Owner.GetTotalDamage<MagicDamageClass>().ApplyTo(MagicCircle.damage);
		if (Main.myPlayer == base.Projectile.owner && Main.rand.NextBool(8))
		{
			CreateArmsOnSurfaces();
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			CreateTileHitEffects();
		}
		Color darkViolet = Color.DarkViolet;
		DelegateMethods.v3_1 = ((Color)(ref darkViolet)).ToVector3() * base.Projectile.scale * 0.4f;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * LaserLength, (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public void UpdateAim()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 newAimDirection = MagicCircle.velocity.SafeNormalize(Vector2.UnitY);
			if (newAimDirection != base.Projectile.velocity)
			{
				base.Projectile.ForceNetUpdate();
			}
			base.Projectile.velocity = newAimDirection;
		}
	}

	public void CreateArmsOnSurfaces()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		Vector2 idealCenter = base.Projectile.Center + base.Projectile.velocity * LaserLength + Main.rand.NextVector2Circular(80f, 8f);
		if (WorldUtils.Find(idealCenter.ToTileCoordinates(), Searches.Chain(new Searches.Down(5), new CustomConditions.SolidOrPlatform()), out var result))
		{
			idealCenter = result.ToWorldCoordinates();
		}
		Point endOfLaserTileCoords = idealCenter.ToTileCoordinates();
		Tile endTile = CalamityUtils.ParanoidTileRetrieval(endOfLaserTileCoords.X, endOfLaserTileCoords.Y);
		if (endTile.HasUnactuatedTile && (Main.tileSolid[endTile.TileType] || Main.tileSolidTop[endTile.TileType]) && !endTile.IsHalfBlock && endTile.Slope == SlopeType.Solid)
		{
			Vector2 armSpawnPosition = endOfLaserTileCoords.ToWorldCoordinates();
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), armSpawnPosition, Vector2.Zero, ModContent.ProjectileType<RancorArm>(), base.Projectile.damage * 2 / 3, 0f, base.Projectile.owner);
		}
	}

	public void CreateTileHitEffects()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		Vector2 endOfLaser = base.Projectile.Center + base.Projectile.velocity * (LaserLength - Main.rand.NextFloat(12f, 72f));
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), endOfLaser, Main.rand.NextVector2Circular(4f, 8f), ModContent.ProjectileType<RancorFog>(), 0, 0f, base.Projectile.owner, 0f, 1f);
		if (Main.rand.NextBool())
		{
			int type = ModContent.ProjectileType<RancorSmallCinder>();
			int damage = 0;
			float cinderSpeed = Main.rand.NextFloat(2f, 6f);
			if (Main.rand.NextBool(11))
			{
				type = ModContent.ProjectileType<RancorLargeCinder>();
				damage = base.Projectile.damage / 3;
				cinderSpeed *= 1.2f;
			}
			Vector2 cinderVelocity = Vector2.Lerp(-base.Projectile.velocity, -Vector2.UnitY, 0.45f).RotatedByRandom(0.7200000286102295) * cinderSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), endOfLaser, cinderVelocity, type, damage, 0f, base.Projectile.owner);
		}
		RancorLavaMetaball.SpawnParticle(endOfLaser + Main.rand.NextVector2Circular(10f, 10f) + base.Projectile.velocity * 40f, 135f);
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 20f;
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.Blue, Color.Red, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.67f - completionRatio / LaserLength * 29f) * 0.5f + 0.5f);
		float opacity = base.Projectile.Opacity * Utils.GetLerpValue(0.97f, 0.9f, completionRatio, clamped: true) * Utils.GetLerpValue(0f, MathHelper.Clamp(15f / LaserLength, 0f, 0.5f), completionRatio, clamped: true) * (float)Math.Pow(Utils.GetLerpValue(60f, 270f, LaserLength, clamped: true), 3.0);
		return Color.Lerp(val, Color.White, 0.5f) * opacity * 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		Vector2[] basePoints = (Vector2[])(object)new Vector2[24];
		for (int i = 0; i < basePoints.Length; i++)
		{
			basePoints[i] = base.Projectile.Center + base.Projectile.velocity * (float)i / ((float)basePoints.Length - 1f) * LaserLength;
		}
		PrimitiveRenderer.RenderTrail(basePoints, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 92);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * LaserLength);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.Calamity().ashesOnDeath = 10;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}
}
