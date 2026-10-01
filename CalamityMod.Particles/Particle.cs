using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace CalamityMod.Particles;

public abstract class Particle
{
	public int Type;

	public int Time;

	public int Lifetime;

	public Vector2 RelativeOffset;

	public Vector2 Position;

	public Vector2 Velocity;

	public Vector2 Origin;

	public Color Color;

	public float Rotation;

	public float Scale;

	public int Variant;

	public bool AffectedByLight;

	public bool Pixelate;

	public GeneralDrawLayer DrawLayer = GeneralDrawLayer.AfterDusts;

	public float LifetimeCompletion
	{
		get
		{
			if (Lifetime == 0)
			{
				return 0f;
			}
			return (float)Time / (float)Lifetime;
		}
	}

	public virtual string Texture => "";

	public virtual AssetRequestMode TextureRequestMode => (AssetRequestMode)2;

	public virtual int FrameVariants => 1;

	public virtual bool Important => false;

	public virtual bool SetLifetime => false;

	public virtual bool UseAdditiveBlend => false;

	public virtual bool UseHalfTransparency => false;

	public virtual bool UseCustomDraw => false;

	public virtual Effect CustomShader => null;

	public virtual void CustomDraw(SpriteBatch spriteBatch)
	{
	}

	public virtual void CustomDraw(SpriteBatch spriteBatch, Vector2 basePosition)
	{
	}

	public virtual void PrepareCustomShader(Effect shader)
	{
	}

	public virtual void Update()
	{
	}

	public void Kill()
	{
		GeneralParticleHandler.RemoveParticle(this);
	}
}
