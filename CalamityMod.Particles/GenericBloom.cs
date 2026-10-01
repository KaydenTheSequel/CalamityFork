using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class GenericBloom : Particle
{
	public bool UseAltVisual;

	private float opacity;

	private Color BaseColor;

	private bool ProduceLight;

	public override string Texture => "CalamityMod/Particles/Light";

	public override bool UseAdditiveBlend => UseAltVisual;

	public override bool SetLifetime => true;

	public GenericBloom(Vector2 position, Vector2 velocity, Color color, float scale, int lifeTime, bool produceLight = true, bool AddativeBlend = true)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		UseAltVisual = true;
		base._002Ector();
		Position = position;
		Velocity = velocity;
		BaseColor = color;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		ProduceLight = produceLight;
		UseAltVisual = AddativeBlend;
	}

	public override void Update()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		opacity = (float)Math.Sin(base.LifetimeCompletion * (float)Math.PI);
		Color = BaseColor * opacity;
		if (ProduceLight)
		{
			Lighting.AddLight(Position, (float)(int)((Color)(ref Color)).R / 255f, (float)(int)((Color)(ref Color)).G / 255f, (float)(int)((Color)(ref Color)).B / 255f);
		}
		Velocity *= 0.95f;
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
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color * opacity, Rotation, tex.Size() / 2f, Scale, (SpriteEffects)0, 0f);
	}
}
