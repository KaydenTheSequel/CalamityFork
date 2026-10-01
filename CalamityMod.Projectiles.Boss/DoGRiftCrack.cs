using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGRiftCrack : ModProjectile, ILocalizedModType, IModType
{
	public Vector2[] CrackPoints;

	public ref float Timer => ref base.Projectile.ai[0];

	public ref float MaxWidth => ref base.Projectile.ai[1];

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		float minDistanceBetweenPoints = 25f;
		float maxDistanceBetweenPoints = 50f;
		if (CrackPoints == null)
		{
			CrackPoints = (Vector2[])(object)new Vector2[25];
			Vector2 startingPosition = base.Projectile.Center;
			for (int i = 0; i < CrackPoints.Length; i++)
			{
				switch (i)
				{
				case 0:
					CrackPoints[i] = startingPosition;
					break;
				case 1:
				{
					Vector2 nextPointPosition = startingPosition + base.Projectile.velocity * Main.rand.NextFloat(0.8f, 1.2f) + Main.rand.NextVector2Circular(30f, 30f);
					CrackPoints[i] = nextPointPosition;
					break;
				}
				default:
				{
					Vector2 previousPoint = CrackPoints[i - 2];
					float distanceFromLastPoint = Main.rand.NextFloat(minDistanceBetweenPoints, maxDistanceBetweenPoints);
					if (i == CrackPoints.Length - 1)
					{
						distanceFromLastPoint = Main.rand.NextFloat(minDistanceBetweenPoints, maxDistanceBetweenPoints) * 0.5f;
					}
					Vector2 newPoint = CrackPoints[i - 1] + (previousPoint.DirectionTo(CrackPoints[i - 1]) * distanceFromLastPoint).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-5f, 5f) * (float)i * 0.25f));
					CrackPoints[i] = newPoint;
					break;
				}
				}
				if (Main.rand.NextBool(9))
				{
					ref Vector2 reference = ref CrackPoints[i];
					reference += Main.rand.NextVector2Circular(10f, 10f);
				}
			}
		}
		base.Projectile.scale = MathHelper.Lerp(1f, 0f, Timer / 30f);
		Timer++;
	}

	public float CrackWidthFunction(float completion, Vector2 vertexPos)
	{
		return base.Projectile.scale * MathHelper.Lerp(MaxWidth, 0f, completion);
	}

	public Color CrackColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.GetAlpha(Color.White);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (CrackPoints != null)
		{
			PrimitiveRenderer.RenderTrail(CrackPoints, new PrimitiveSettings(CrackWidthFunction, CrackColorFunction, null, smoothen: false));
		}
		return false;
	}
}
