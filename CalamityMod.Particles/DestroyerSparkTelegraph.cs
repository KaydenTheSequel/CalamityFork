using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class DestroyerSparkTelegraph : Particle
{
	private NPC NPCToFollow;

	private float Spin;

	private float Opacity;

	private Color Bloom;

	private float BloomScale;

	public override string Texture => "CalamityMod/Particles/Sparkle2";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public override bool Important => true;

	public DestroyerSparkTelegraph(NPC npcToFollow, Color color, Color bloom, float scale, int lifeTime, float rotationSpeed = 0f, float bloomScale = 1f)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		NPCToFollow = npcToFollow;
		Color = color;
		Bloom = bloom;
		Scale = scale;
		Lifetime = lifeTime;
		Spin = rotationSpeed;
		BloomScale = bloomScale;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
	}

	public override void Update()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Opacity = MathF.Sin(base.LifetimeCompletion * (float)Math.PI);
		Rotation += Spin * ((Velocity.X > 0f) ? 1f : (-1f));
		if (NPCToFollow != null && NPCToFollow.active && !Main.tile[NPCToFollow.Center.ToSafeTileCoordinates()].IsTileSolid())
		{
			Position = NPCToFollow.Center;
		}
		else
		{
			Kill();
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D starTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)starTexture.Height / (float)bloomTexture.Height;
		spriteBatch.Draw(bloomTexture, Position - Main.screenPosition, (Rectangle?)null, Bloom * Opacity * 0.5f, 0f, bloomTexture.Size() / 2f, Scale * BloomScale * properBloomSize, (SpriteEffects)0, 0f);
		spriteBatch.Draw(starTexture, Position - Main.screenPosition, (Rectangle?)null, Color * Opacity * 0.5f, Rotation + (float)Math.PI / 4f, starTexture.Size() / 2f, Scale * 0.75f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(starTexture, Position - Main.screenPosition, (Rectangle?)null, Color * Opacity, Rotation, starTexture.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
