using CalamityMod.Systems.Graphic.LiquidSystem;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public abstract class ModLavaStyle : ModTexturedType
{
	public int Slot { get; private set; } = -1;

	public override string Name => base.Name;

	public override string Texture => base.Texture;

	public virtual string BlockTexture => Texture + "_Block";

	public virtual string SlopeTexture => Texture + "_Slope";

	public virtual string WaterfallTexture => Texture + "_Waterfall";

	protected sealed override void Register()
	{
		Slot = ModLavaStyleLoader.Register(this);
	}

	public sealed override void SetupContent()
	{
		SetStaticDefaults();
	}

	public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.55f;
		g = 0.33f;
		b = 0.11f;
	}

	public virtual void DrawColor(int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
	}

	public virtual int GetSplashDust()
	{
		return 35;
	}

	public virtual int GetDropletGore()
	{
		return 716;
	}

	public virtual bool IsLavaActive()
	{
		return false;
	}

	public virtual bool LavafallGlowmask()
	{
		return true;
	}

	public virtual void InflictDebuff(Player player, int onfireDuration)
	{
	}
}
