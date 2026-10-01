using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class DestroyerReticleTelegraph : Particle
{
	private NPC NPCToFollow;

	private float OriginalScale;

	private float FinalScale;

	private float Opacity;

	private int RotationDirection;

	public override string Texture => "CalamityMod/Particles/DestroyerReticleTelegraph";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public override bool Important => true;

	public DestroyerReticleTelegraph(NPC npcToFollow, Color color, float originalScale, float finalScale, int lifeTime)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		NPCToFollow = npcToFollow;
		Color = color;
		OriginalScale = originalScale;
		Scale = originalScale;
		FinalScale = finalScale;
		Lifetime = lifeTime;
		RotationDirection = Main.rand.NextBool().ToDirectionInt();
	}

	public override void Update()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		float pulseProgress = CalamityUtils.PiecewiseAnimation(base.LifetimeCompletion, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
		Scale = MathHelper.Lerp(OriginalScale, FinalScale, pulseProgress);
		Opacity = pulseProgress;
		Rotation += MathHelper.ToRadians(8f) * (1f - pulseProgress) * (float)RotationDirection;
		if (NPCToFollow != null && NPCToFollow.active && !Main.tile[NPCToFollow.Center.ToSafeTileCoordinates()].IsTileSolid())
		{
			Position = NPCToFollow.Center;
			if (NPCToFollow.ai[2] == 1f)
			{
				Kill();
			}
		}
		else
		{
			Kill();
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * Opacity, Rotation, tex.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
