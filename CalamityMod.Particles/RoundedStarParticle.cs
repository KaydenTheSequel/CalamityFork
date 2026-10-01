using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class RoundedStarParticle : Particle
{
	public float RotationSpeed;

	public float Deceleration;

	public bool UseSpiralAI;

	private int OwnerIndex;

	private float InitialRadius;

	private float OrbitalAngle;

	public override string Texture => "CalamityMod/Particles/RoundedStar";

	public override bool UseCustomDraw => true;

	public RoundedStarParticle(Vector2 position, Vector2 velocity, Color color, float scale, int lifetime, float rotationSpeed, float deceleration, bool useSpiralAI, Vector2 spiralTarget, int ownerIndex)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Color = color;
		Scale = scale;
		Lifetime = lifetime;
		RotationSpeed = rotationSpeed;
		Deceleration = deceleration;
		UseSpiralAI = useSpiralAI;
		OwnerIndex = ownerIndex;
		if (UseSpiralAI)
		{
			Vector2 directionToPlayer = Position - Main.player[OwnerIndex].Center;
			InitialRadius = ((Vector2)(ref directionToPlayer)).Length();
			OrbitalAngle = directionToPlayer.ToRotation();
		}
	}

	public override void Update()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (UseSpiralAI)
		{
			OrbitalAngle += RotationSpeed;
			float fadeScale = 1f - (float)Time / (float)Lifetime;
			float currentRadius = InitialRadius * fadeScale;
			Vector2 playerCenter = Main.player[OwnerIndex].Center;
			Vector2 newRelativePosition = OrbitalAngle.ToRotationVector2() * currentRadius;
			Position = playerCenter + newRelativePosition;
		}
		else
		{
			Velocity *= Deceleration;
			Position += Velocity;
		}
		Rotation += RotationSpeed;
		Time++;
		if (Time >= Lifetime)
		{
			Kill();
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		float fade = 1f - (float)Time / (float)Lifetime;
		_ = Scale;
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * fade, Rotation, tex.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
